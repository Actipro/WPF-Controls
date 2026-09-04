using ActiproSoftware.Text;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GoToLine;

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

	private void OnGoToLineButtonClick(object sender, RoutedEventArgs e) {
		// Validate
		if ((!int.TryParse(lineNumberTextBox.Text, out var lineIndex)) || (lineIndex < 1) || (lineIndex > editor.ActiveView.CurrentSnapshot.Lines.Count)) {
			MessageBox.Show(string.Format("Please enter a valid line number (1-{0}).", editor.ActiveView.CurrentSnapshot.Lines.Count));
			return;
		}

		// Set caret position (make zero-based)
		editor.ActiveView.Selection.CaretPosition = new TextPosition(lineIndex - 1, 0);
		editor.ActiveView.Scroller.ScrollLineToVisibleMiddle();

		// Focus the editor
		editor.Focus();
	}

}
