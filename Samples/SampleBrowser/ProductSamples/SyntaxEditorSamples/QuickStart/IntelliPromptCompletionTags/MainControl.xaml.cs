using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionTags;

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
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Html.langdef");

		// Register a custom completion provider on the language used by the editor
		editor.Document.Language.RegisterService<ICompletionProvider>(new CustomCompletionProvider());
	}

}
