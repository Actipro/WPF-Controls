using ActiproSoftware.ProductSamples.GridsSamples.Common;
using ActiproSoftware.Windows.Controls.Grids;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.TreeListBoxChecking;

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

	private void OnCheckAllButtonClick(object sender, RoutedEventArgs e) {
		foreach (CheckableTreeNodeModel model in treeListBox.Items)
			SetIsCheckedRecursive(model, isChecked: true);
	}

	/// <summary>
	/// Occurs before the default action is executed for an item.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="e">The event data.</param>
	private void OnTreeListBoxItemDefaultActionExecuting(object sender, TreeListBoxItemEventArgs e) {
		var model = e.Item as CheckableTreeNodeModel;
		if (model?.IsCheckable == true) {
			e.Cancel = true;

			// Toggle the checked state
			model.IsChecked = !model.IsChecked;
		}
	}

	private void OnUncheckAllButtonClick(object sender, RoutedEventArgs e) {
		foreach (CheckableTreeNodeModel model in treeListBox.Items)
			SetIsCheckedRecursive(model, isChecked: false);
	}

	/// <summary>
	/// Recursively sets nodes as checked or unchecked.
	/// </summary>
	/// <param name="model">The <see cref="CheckableTreeNodeModel"/> to update.</param>
	/// <param name="isChecked">Whether the model is checked.</param>
	private static void SetIsCheckedRecursive(CheckableTreeNodeModel model, bool isChecked) {
		if (model.IsCheckable)
			model.IsChecked = isChecked;

		foreach (var childModel in model.Children.OfType<CheckableTreeNodeModel>())
			SetIsCheckedRecursive(childModel, isChecked);
	}

}
