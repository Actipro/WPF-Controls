using ActiproSoftware.ProductSamples.SyntaxEditorSamples.Common;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCodeSnippets;

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
		editor.Document.Language = SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("JavaScript.langdef");

		// Register a code snippet provider that has several snippets available
		var snippetFolder = SyntaxEditorHelper.LoadSampleJavascriptCodeSnippetsFromResources();
		editor.Document.Language.RegisterService(new CodeSnippetProvider() { RootFolder = snippetFolder });

		// Ensure all classification types and related styles have been registered
		//   since classification types are used for code snippet field display
		new BuiltInClassificationTypeProvider().RegisterAll();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnInsertSnippetButtonClick(object sender, RoutedEventArgs e)
		=> editor.ActiveView.IntelliPrompt.RequestInsertSnippetSession();

}
