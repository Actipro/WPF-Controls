using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.LineCommenting;

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

		// Select the first line commenter
		lineCommentersComboBox.SelectedIndex = 0;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnLineCommenterSelectionChangedEvent(object sender, SelectionChangedEventArgs e) {
		// Change the type of line commenter to match the selection
		ILineCommenter commenter = lineCommentersComboBox.SelectedIndex switch {
			0 => new LineBasedLineCommenter() { StartDelimiter = "//" },
			_ => new RangeLineCommenter() { StartDelimiter = "/*", EndDelimiter = "*/" }
		};
		editor.Document.Language.RegisterLineCommenter(commenter);
	}

}
