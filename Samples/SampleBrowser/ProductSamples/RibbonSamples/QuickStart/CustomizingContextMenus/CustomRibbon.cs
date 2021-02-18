using ActiproSoftware.Windows.Controls.Ribbon;
using System.Windows;
using System.Windows.Controls;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.CustomizingContextMenus {

	/// <summary>
	/// A custom ribbon that can update context menus.
	/// </summary>
	public class CustomRibbon : Ribbon {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
	
		/// <summary>
		/// Creates a context menu for the specified <see cref="FrameworkElement"/>.
		/// </summary>
		/// <param name="element">The <see cref="FrameworkElement"/> for which to create a context menu.</param>
		/// <returns>The context menu that was created.</returns>
		protected override ContextMenu CreateContextMenu(FrameworkElement element) {
			var contextMenu = base.CreateContextMenu(element);
			if (contextMenu != null) {
				var button = element as RibbonControls.Primitives.ButtonBase;
				if ((button != null) && (contextMenu.Items.Count > 0)) {
					var menu = contextMenu.Items[0] as RibbonControls.Menu;
					if (menu != null)
						MainControl.AddCustomMenuItem(button, menu);
				}
			}

			return contextMenu;
		}

	}

}
