using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using Microsoft.Win32;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.Demo.WebAddonJavaScriptEditor;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _documentNumber;
	private bool _hasPendingParseData;

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
		//   since it tells you how to set up an ambient parse request dispatcher and an ambient
		//   code repository within your application OnStartup code, and add related cleanup in your
		//   application OnExit code.  These steps are essential to having the add-on perform well.
		//

		UpdateUIFromParseData();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a new file.
	/// </summary>
	private void NewFile()
		=> OpenFile(string.Format("Document{0}.js", ++_documentNumber));

	private void OnCodeEditorDocumentParseDataChanged(object sender, EventArgs e) {
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
	private void OnCodeEditorUserInterfaceUpdate(object sender, RoutedEventArgs e) {
		// If there is a pending parse data change...
		if (_hasPendingParseData) {
			// Clear flag
			_hasPendingParseData = false;

			UpdateUIFromParseData();
		}
	}

	private void OnCodeEditorViewSelectionChanged(object sender, EditorViewSelectionEventArgs e) {
		// Quit if this event is not for the active view
		if (!e.View.IsActive)
			return;

		// Update line, col, and character display
		linePanel.Text = string.Format("Ln {0}", e.CaretPosition.DisplayLine);
		columnPanel.Text = string.Format("Col {0}", e.CaretDisplayCharacterColumn);
		characterPanel.Text = string.Format("Ch {0}", e.CaretPosition.DisplayCharacter);
	}

	private void OnErrorListViewDoubleClick(object sender, MouseButtonEventArgs e) {
		if (sender is ListBox { SelectedItem: IParseError error }) {
			if (error.PositionRange.HasValue)
				codeEditor.ActiveView.Selection.StartPosition = error.PositionRange.Value.StartPosition;

			codeEditor.Focus();
		}
	}

	private void OnNewFileButtonClick(object sender, RoutedEventArgs e)
		=> NewFile();

	private void OnOpenFileButtonClick(object sender, RoutedEventArgs e) {
		// Show a file open dialog
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "JavaScript files (*.js)|*.js|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Open a document (use dialog to help open the file because of possible security restrictions)
			using (var stream = dialog.OpenFile()) {
				// Read the file
				OpenFile(Path.GetFileName(dialog.FileName), stream);
			}
		}
	}

	/// <summary>
	/// Opens a file.
	/// </summary>
	/// <param name="fileName">The file name.</param>
	/// <param name="stream">The optional <see cref="Stream"/> to load.</param>
	private void OpenFile(string fileName, Stream? stream = null) {
		// Load the file
		if (stream is not null)
			codeEditor.Document.LoadFile(stream, Encoding.UTF8);
		else
			codeEditor.Document.SetText(null);

		// Set the filename
		codeEditor.Document.FileName = fileName;
	}

	/// <summary>
	/// Updates the UI from the current parse data.
	/// </summary>
	private void UpdateUIFromParseData() {
		var parseData = codeEditor.Document.ParseData as ILLParseData;
		if (parseData is not null) {
			if (codeEditor.Document.CurrentSnapshot.Length < 10000) {
				// Show the AST
				astOutputEditor.Document.SetText(parseData.Ast?.ToTreeString(0));
			}
			else
				astOutputEditor.Document.SetText("(Not displaying large AST for performance reasons)");

			// Output errors
			errorListView.ItemsSource = parseData.Errors;
		}
		else {
			// Clear UI
			astOutputEditor.Document.SetText(null);
			errorListView.ItemsSource = null;
		}
	}

}
