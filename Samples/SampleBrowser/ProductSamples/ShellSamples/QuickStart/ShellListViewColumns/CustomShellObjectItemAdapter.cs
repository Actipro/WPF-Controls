using ActiproSoftware.Shell;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.ShellListViewColumns;

/// <summary>
/// Represents a custom <see cref="ShellObjectItemAdapter"/> implementation.
/// </summary>
public class CustomShellObjectItemAdapter : ShellObjectItemAdapter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override ShellObjectViewModel? CreateShellObjectViewModel(TreeListBox ownerControl, IShellObject shellObject) {
		var viewModel = base.CreateShellObjectViewModel(ownerControl, shellObject);

		// This sample uses the Tag property to store a boolean of whether the item is checked, so default it to false instead of null
		if (viewModel is not null)
			viewModel.Tag = false;

		return viewModel;
	}

}
