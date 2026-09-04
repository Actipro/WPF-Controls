using ActiproSoftware.Text;
using ActiproSoftware.Text.Searching;
using ActiproSoftware.Windows.Controls.SyntaxEditor;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchFindResults;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private ISearchResultSet? _lastResultSet;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a search operation occurs in a view.
	/// </summary>
	private void OnEditorViewSearch(object sender, EditorViewSearchEventArgs e)
		=> UpdateResults(e.ResultSet);

	private void OnResultsTextBoxDoubleClick(object sender, MouseButtonEventArgs e) {
		// Quit if there is not result set stored yet
		if (_lastResultSet is null)
			return;

		var charIndex = resultsTextBox.GetCharacterIndexFromPoint(e.GetPosition(resultsTextBox), snapToText: true);
		var lineIndex = resultsTextBox.GetLineIndexFromCharacterIndex(charIndex);

		var resultIndex = lineIndex - 1;  // Account for first line in results displaying search info
		if ((0 <= resultIndex) && (resultIndex < _lastResultSet.Results.Count)) {
			// A valid result was clicked
			var result = _lastResultSet.Results[resultIndex];
			TextSnapshotRange? selectionSnapshotRange;
			if (result.ReplaceSnapshotRange.HasValue) {
				// Replace result
				selectionSnapshotRange = result.ReplaceSnapshotRange.Value.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default);
			}
			else {
				// Find result
				selectionSnapshotRange = result.FindSnapshotRange.TranslateTo(editor.ActiveView.CurrentSnapshot, TextRangeTrackingModes.Default);
			}

			// Select the range
			if (selectionSnapshotRange.HasValue)
				editor.ActiveView.Selection.SelectRange(selectionSnapshotRange.Value.TextRange);

			// Focus the editor
			editor.Focus();
		}
	}

	/// <summary>
	/// Updates the results.
	/// </summary>
	/// <param name="resultSet">The <see cref="ISearchResultSet"/> containing results.</param>
	private void UpdateResults(ISearchResultSet resultSet) {
		// Show the results
		resultsToolWindow.Title = string.Format("Find Results - {0} match{1}", resultSet.Results.Count, (resultSet.Results.Count == 1 ? string.Empty : "es"));
		resultsTextBox.Text = resultSet.ToString();

		// Save the result set
		_lastResultSet = resultSet;
	}

}
