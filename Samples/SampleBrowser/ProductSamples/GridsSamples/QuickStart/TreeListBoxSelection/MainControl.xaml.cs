using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxSelection;

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
	/// Occurs before an item is selected.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTreeListBoxItemSelecting(object sender, TreeListBoxItemEventArgs e) {
		if (CanBlockNames) {
			if (e.Item is TreeNodeModel { Name.Length: > 0 } model)
				e.Cancel = ("ABCDE".IndexOf(model.Name[0].ToString(), StringComparison.OrdinalIgnoreCase) != -1);
		}
	}

	/// <summary>
	/// Occurs when the selection has changed.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTreeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e) {
		selectedItemTextBlock.Text = string.Format("Selected item ({0} added, {1} removed, updated {2}):",
			e.AddedItems.Count, e.RemovedItems.Count, DateTime.Now.ToLongTimeString());
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether certain names can be blocked.
	/// </summary>
	public bool CanBlockNames { get; set; }

}
