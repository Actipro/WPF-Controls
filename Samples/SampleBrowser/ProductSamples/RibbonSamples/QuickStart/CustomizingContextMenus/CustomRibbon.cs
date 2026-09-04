using ActiproSoftware.Windows.Controls.Ribbon;
using RibbonControls = ActiproSoftware.Windows.Controls.Ribbon.Controls;

namespace ActiproSoftware.ProductSamples.RibbonSamples.QuickStart.CustomizingContextMenus;

/// <summary>
/// A custom ribbon that can update context menus.
/// </summary>
public class CustomRibbon : Ribbon {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override ContextMenu? CreateContextMenu(FrameworkElement element) {
		var contextMenu = base.CreateContextMenu(element);
		if (
			contextMenu is { Items.Count: > 0 }
			&& element is RibbonControls.Primitives.ButtonBase button
			&& contextMenu.Items[0] is RibbonControls.Menu menu
		) {
			MainControl.AddCustomMenuItem(button, menu);
		}

		return contextMenu;
	}

}
