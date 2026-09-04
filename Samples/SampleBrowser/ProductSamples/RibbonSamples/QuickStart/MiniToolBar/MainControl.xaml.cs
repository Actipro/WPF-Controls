using ActiproSoftware.Windows.Controls.Ribbon.UI;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.MiniToolBar;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

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

	private void OnPreviewMouseUp(object sender, MouseButtonEventArgs e) {
		// If a selection was just made with the mouse...
		if ((e.ChangedButton == MouseButton.Left) && (!editor.Selection.IsEmpty)) {
			// Get the mini-toolbar in the resources of this UserControl
			var toolBar = (RibbonControls.MiniToolBar)FindResource("SimpleMiniToolBar");
			if (toolBar is not null) {
				// Show the mini-toolbar
				MiniToolBarService.Show(toolBar, editor, e.GetPosition(editor));
			}
		}
	}

}
