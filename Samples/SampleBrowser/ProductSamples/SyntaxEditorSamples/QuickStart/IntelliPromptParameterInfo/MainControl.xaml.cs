using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptParameterInfo;

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
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("Simple-Advanced.langdef");

		// Register an IParameterInfoProvider service with the language so that the language can automatically generate
		//   parameter info popups
		editor.Document.Language.RegisterService<IParameterInfoProvider>(new CustomParameterInfoProvider());
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnShowParameterInfoButtonClick(object sender, RoutedEventArgs e) {
		// Focus the editor
		editor.ActiveView.Focus();

		// Get the IParameterInfoProvider that is registered with the language
		var provider = editor.Document.Language.GetService<IParameterInfoProvider>();
		// Request that a session is created
		provider?.RequestSession(editor.ActiveView);
	}

}
