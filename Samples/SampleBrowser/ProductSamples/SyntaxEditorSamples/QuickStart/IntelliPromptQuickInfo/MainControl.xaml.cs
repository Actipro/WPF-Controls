using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptQuickInfo;

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

		// Register an IQuickInfoProvider service with the language so that the language can automatically generate
		//   quick info popups based on mouse/keyboard input
		editor.Document.Language.RegisterService(new CustomQuickInfoProvider());
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnShowQuickInfoButtonClick(object sender, RoutedEventArgs e) {
		// Ensure the editor has focus
		editor.Focus();

		// Get the IQuickInfoProvider that is registered with the language
		var provider = editor.Document.Language.GetService<CustomQuickInfoProvider>();

		// Create a context
		var context = provider?.GetContext(editor.ActiveView, editor.ActiveView.Selection.CaretOffset);
		if (context is not null) {
			// Request that a session is created based on the context, and disable mouse tracking since
			//   this request is initiated from a button click
			provider!.RequestSession(editor.ActiveView, context, canTrackPointer: false);
		}
	}

}
