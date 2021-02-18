using System.Collections.Generic;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.Grids;
using ActiproSoftware.Windows.Controls.Shell;

namespace ActiproSoftware.ProductSamples.ShellSamples.QuickStart.CustomShellObjects {
	
	/// <summary>
	/// Represents a custom <see cref="ShellObjectItemAdapter"/> implementation.
	/// </summary>
	public class CustomShellObjectItemAdapter : ShellObjectItemAdapter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the <c>ContextMenu</c> for the specified collection of view-models.
		/// </summary>
		/// <param name="ownerControl">The owner control.</param>
		/// <param name="viewModels">The view-models to examine.</param>
		/// <returns>The <c>ContextMenu</c> for the specified collection of view-models.</returns>
		public override ContextMenu GetItemContextMenu(TreeListBox ownerControl, IList<ShellObjectViewModel> viewModels) {
			var menu = base.GetItemContextMenu(ownerControl, viewModels);
			if (menu != null)
				return menu;

			if (viewModels.Count == 1) {
				var shellObject = viewModels[0].Model as CustomShellObject;
				if (shellObject != null) {
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

}