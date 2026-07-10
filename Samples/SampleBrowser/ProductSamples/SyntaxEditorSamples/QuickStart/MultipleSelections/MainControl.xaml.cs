using ActiproSoftware.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.MultipleSelections;

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
		var language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

		// Assign the language to the document
		editor.Document.Language = language;

		// Select three ranges programmatically
		editor.ActiveView.Selection.SelectRanges([
			new TextPositionRange(12, 13, 12, 21),
			new TextPositionRange(14, 16, 14, 24),
			new TextPositionRange(16, 14, 16, 22)
		], primaryIndex: 0);
	}

}
