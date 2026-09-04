using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.Xml;
using ActiproSoftware.Text.Languages.Xml.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using Microsoft.Win32;
using System.Reflection;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.WebAddonHtmlEditor;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private bool _hasPendingParseData;
	private ISearchResultSet? _lastResultSet;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		//
		// NOTE: Make sure that you've read through the add-on language's 'Getting Started' topic
		//   since it tells you how to set up an ambient parse request dispatcher within your
		//   application OnStartup code, and add related cleanup in your application OnExit code.
		//   These steps are essential to having the add-on perform well.
		//

		// Register the schema resolver service with the XML language (needed to support IntelliPrompt)
		var resolver = new XmlSchemaResolver();
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xhtml.xsd")) {
			if (stream is not null)
				resolver.AddSchemaFromStream(stream);
		}

		// Xml.xsd is also required for Xhtml.xsd
		using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(SyntaxEditorHelper.XmlSchemasPath + "Xml.xsd")) {
			if (stream is not null)
				resolver.AddSchemaFromStream(stream);
		}

		syntaxEditor.Document.Language.RegisterXmlSchemaResolver(resolver);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a search operation occurs in a view.
	/// </summary>
	private void OnEditorViewSearch(object sender, EditorViewSearchEventArgs e)
		=> UpdateResults(e.ResultSet);

	private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IParseError error }) {
			if (error.PositionRange.HasValue)
				syntaxEditor.ActiveView.Selection.StartPosition = error.PositionRange.Value.StartPosition;

			syntaxEditor.Focus();
		}
	}

	private void OnFindResultsTextBoxDoubleClick(object sender, MouseButtonEventArgs e) {
		// Quit if there is not result set stored yet
		if (_lastResultSet is null)
			return;

		var charIndex = findResultsTextBox.GetCharacterIndexFromPoint(e.GetPosition(findResultsTextBox), true);
		var lineIndex = findResultsTextBox.GetLineIndexFromCharacterIndex(charIndex);

		var resultIndex = lineIndex - 1;  // Account for first line in results displaying search info
		if ((0 <= resultIndex) && (resultIndex < _lastResultSet.Results.Count)) {
			// A valid result was clicked
			var result = _lastResultSet.Results[resultIndex];
			TextSnapshotRange? selectionSnapshotRange;
			if (result.ReplaceSnapshotRange.HasValue) {
				// Replace result
				selectionSnapshotRange = result.ReplaceSnapshotRange.Value.TranslateTo(syntaxEditor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default);
			}
			else {
				// Find result
				selectionSnapshotRange = result.FindSnapshotRange.TranslateTo(syntaxEditor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default);
			}

			// Select the range
			if (selectionSnapshotRange.HasValue)
				syntaxEditor.ActiveView.Selection.SelectRange(selectionSnapshotRange.Value.TextRange);

			// Focus the editor
			syntaxEditor.Focus();
		}
	}

	private void OnNewButtonClick(object sender, RoutedEventArgs e)
		=> syntaxEditor.Document.SetText(null);

	private void OnOpenButtonClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "XHTML files (*.html;*.xhtml)|*.html;*.xhtml|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Open a document
			syntaxEditor.Document.LoadFile(dialog.FileName);
		}
	}

	private void OnSyntaxEditorDocumentParseDataChanged(object sender, EventArgs e) {
		//
		// NOTE: The parse data here is generated in a worker thread... this event handler is called
		//   back in the UI thread immediately when the worker thread completes... it is best
		//   practice to delay UI updates until the end user stops typing... we will flag that
		//   there is a pending parse data change, which will be handled in the
		//   UserInterfaceUpdate event
		//

		_hasPendingParseData = true;
	}

	/// <summary>
	/// Occurs after a brief delay following any document text, parse data, or view selection update, allowing consumers to update the user interface during an idle period.
	/// </summary>
	private void OnSyntaxEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
		// If there is a pending parse data change...
		if (_hasPendingParseData) {
			// Clear flag
			_hasPendingParseData = false;

			var parseData = syntaxEditor.Document.ParseData as XmlParseData;
			if (parseData is not null) {
				// Output errors
				errorListView.ItemsSource = parseData.Errors;

				// Show well-formed state
				messagePanel.Content = string.Format("Well-formed: {0}", parseData.IsWellFormed ? "Yes" : "No");
			}
			else {
				// Clear UI
				errorListView.ItemsSource = null;
				messagePanel.Content = "Ready";
			}
		}
	}

	private void OnSyntaxEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		// Update line, col, and character display
		linePanel.Text = string.Format("Ln {0}", e.CaretPosition.DisplayLine);
		columnPanel.Text = string.Format("Col {0}", e.CaretDisplayCharacterColumn);
		characterPanel.Text = string.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
	}

	/// <summary>
	/// Updates the results.
	/// </summary>
	/// <param name="resultSet">The <see cref="ISearchResultSet"/> containing results.</param>
	private void UpdateResults(ISearchResultSet resultSet) {
		// Show the results
		findResultsToolWindow.Title = string.Format("Find Results - {0} match{1}", resultSet.Results.Count, (resultSet.Results.Count == 1 ? string.Empty : "es"));
		findResultsTextBox.Text = resultSet.ToString();

		switch (resultSet.OperationType) {
			case SearchOperationType.FindAll:
			case SearchOperationType.ReplaceAll:
				// Activate the find results tool window
				findResultsToolWindow.Activate(focus: false);
				break;
		}

		// Save the result set
		_lastResultSet = resultSet;
	}

}
