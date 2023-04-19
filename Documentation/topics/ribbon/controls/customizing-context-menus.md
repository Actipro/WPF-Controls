---
title: "Customizing Context Menus"
page-title: "Customizing Context Menus - Ribbon Controls"
order: 9
---
# Customizing Context Menus

Ribbon allows for the dynamic creation/updating of context menus as well as the same for the [QuickAccessToolBar](miscellaneous/quickaccesstoolbar.md)`Customize` button's menu.

## Customizing a Context Menu

Actipro Ribbon auto-generates context menus for most of the controls in it.  The auto-generated menu will be created if the control doesn't already have a `ContextMenu` assigned to it, and contains items for toggling the visibility of the Quick Access ToolBar (QAT), minimizing the ribbon, etc.

However, many times it is useful to add custom menu items to a specific control, or a certain type of controls.  You can achieve this programmatically by intercepting the `ContextMenuOpening` event for one or more controls and dynamically updating the `ContextMenu` that for those controls.

Here is some code showing how to create a class event handler to process the `ContextMenuOpening` event for each `UIElement`.

```csharp
EventManager.RegisterClassHandler(typeof(UIElement), FrameworkElement.ContextMenuOpeningEvent, new ContextMenuEventHandler(OnContextMenuOpeningEvent));
```

Here is some code to handle the `ContextMenuOpening` event and add a custom menu item (via a `AddCustomMenuItem` method that you would create) to the child [Menu](miscellaneous/menu.md) control if one is found.

```csharp
private void OnContextMenuOpeningEvent(object sender, ContextMenuEventArgs e) {
	RibbonControls.Primitives.ButtonBase button = sender as RibbonControls.Primitives.ButtonBase;
	if ((button != null) && (button.ContextMenu != null) && (button.ContextMenu.Items.Count > 0)) {
		RibbonControls.Menu menu = button.ContextMenu.Items[0] as RibbonControls.Menu;
		if (menu != null)
			this.AddCustomMenuItem(menu);
	}
}
```

## Customizing the QuickAccessToolBar Customize Button's Menu

The same sort of concept as described above works for the [QuickAccessToolBar](miscellaneous/quickaccesstoolbar.md)`Customize` button's menu.

However, the click of that button shows a popup instead of a context menu.  Therefore, you should create a class handler for the [PopupControlService](xref:@ActiproUIRoot.Controls.Ribbon.UI.PopupControlService).[PopupOpeningEvent](xref:@ActiproUIRoot.Controls.Ribbon.UI.PopupControlService.PopupOpeningEvent) event.  In your event handler, look to see if the sender is a [QuickAccessToolBarCustomizeButton](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.QuickAccessToolBarCustomizeButton) and if it is, you can customize its [PopupContent](xref:@ActiproUIRoot.Controls.Ribbon.Controls.Primitives.PopupButtonBase.PopupContent).

In the case of the `Customize` button, the popup content will be a [Menu](miscellaneous/menu.md) with its default items already populated.
