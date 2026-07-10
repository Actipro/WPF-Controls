using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SearchResultHighlighting;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("JavaScript.langdef");

		// Ensure all classification types and related styles have been registered
		//   since classification types are used for the highlight display
		new BuiltInClassificationTypeProvider().RegisterAll();

		// Refresh highlights
		RefreshHighlights();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnEditorActiveViewChanged(object sender, EditorViewChangedEventArgs e) {
		// Clear search options from the inactive view
		if (e.OldValue is not null)
			e.OldValue.HighlightedResultSearchOptions = null;

		// Apply highlights to the newly active view
		RefreshHighlights();
	}

	private void OnFindWhatTextBoxGotFocus(object sender, RoutedEventArgs e)
		=> RefreshHighlights();

	private void OnFindWhatTextBoxTextChanged(object sender, TextChangedEventArgs e)
		=> RefreshHighlights();

	/// <summary>
	/// Refreshes the highlights.
	/// </summary>
	private void RefreshHighlights() {
		if (editor is null)
			return;

		var options = new EditorSearchOptions {
			FindText = findWhatTextBox.Text
		};
		editor.ActiveView.HighlightedResultSearchOptions = options;
	}

}
