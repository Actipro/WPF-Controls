using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxThreeStateChecking;

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

	/// <summary>
	/// Occurs before the default action is executed for an item.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTreeListBoxItemDefaultActionExecuting(object sender, TreeListBoxItemEventArgs e) {
		if (e.Item is CheckableTreeNodeModel { IsCheckable: true, Children.Count: 0 } model) {
			e.Cancel = true;

			// Toggle the checked state
			model.IsChecked = !(model.IsChecked == true);
		}
	}

}
