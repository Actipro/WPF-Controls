using Microsoft.Win32;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningRangeBased;

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
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnOpenButtonClick(object sender, RoutedEventArgs e) {
		var dialog = new OpenFileDialog {
			CheckFileExists = true,
			Multiselect = false,
			Filter = "Code files (*.js)|*.js|All files (*.*)|*.*"
		};
		if (dialog.ShowDialog() == true) {
			// Open a document
			editor.Document.LoadFile(dialog.FileName);
		}

		// Focus the editor
		editor.Focus();
	}

}
