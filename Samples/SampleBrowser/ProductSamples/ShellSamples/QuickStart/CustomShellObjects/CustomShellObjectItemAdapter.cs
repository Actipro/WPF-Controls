using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects;

/// <summary>
/// Represents a custom <see cref="ShellObjectItemAdapter"/> implementation.
/// </summary>
public class CustomShellObjectItemAdapter : ShellObjectItemAdapter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override ContextMenu? GetItemContextMenu(TreeListBox ownerControl, IList<ShellObjectViewModel> viewModels) {
		var menu = base.GetItemContextMenu(ownerControl, viewModels);
		if (menu is not null)
			return menu;

		if (viewModels.Count == 1) {
			var shellObject = viewModels[0].Model as CustomShellObject;
			if (shellObject is not null) {
				menu = new ContextMenu();
				menu.Items.Add(new MenuItem() {
					Header = "Custom Menu Item"
				});
				return menu;
			}
		}

		return null;
	}

}
