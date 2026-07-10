using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.TextChangesCancelling;

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

	private void OnEditorDocumentTextChanging(object sender, EditorSnapshotChangingEventArgs e) {
		e.Cancel = (cancelCheckBox.IsChecked == true);

		if (e.Cancel) {
			if (alternateTextCheckBox.IsChecked == true) {
				// Temporarily turn off cancel and insert date/time instead
				cancelCheckBox.IsChecked = false;
				editor.ActiveView.ReplaceSelectedText(TextChangeTypes.Custom, DateTime.Now.ToString());
				cancelCheckBox.IsChecked = true;
				MessageBox.Show("Text change cancelled, current date/time inserted instead.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else {
				// Simple cancel
				MessageBox.Show("Text change cancelled.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
	}

}
