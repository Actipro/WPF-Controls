namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionCustomActions {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : System.Windows.Controls.UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Register a custom completion provider on the language used by the editor
			editor.Document.Language.RegisterService(new CustomCompletionProvider());
		}

	}

}