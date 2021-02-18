---
title: "Context Menu Customization"
page-title: "Context Menu Customization - PropertyGrid Features"
order: 28
---
# Context Menu Customization

Property grid item (category, property, etc.) context menus displayed by this product can be customized or replaced entirely before they are shown to the end user.

## Default Category Menu

A default context menu can be pre-configured if the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[IsDefaultItemContextMenuEnabled](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.IsDefaultItemContextMenuEnabled) property is set to `true`.

The default context menu for a category has a menu item for toggling the display of the [summary area](summary-area.md), if that feature is enabled via [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[IsSummaryToggleAllowed](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.IsSummaryToggleAllowed).

## Default Property Menu

A default context menu can be pre-configured if the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[IsDefaultItemContextMenuEnabled](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.IsDefaultItemContextMenuEnabled) property is set to `true`.

The default context menu for a property has a menu item for resetting the value to its default, if the control determines the property has been modified.

When the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[AreClipboardMenuItemsEnabled](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.AreClipboardMenuItemsEnabled) property is `true`, several clipboard-related menu items are added.  One copies the display name of the property, while another copies the value to the clipboard.  Another allows for pasting in of a value from the clipboard.

The context menu also has a menu item for toggling the display of the [summary area](summary-area.md), if that feature is enabled via [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[IsSummaryToggleAllowed](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.IsSummaryToggleAllowed).

## Customizing the Menu or Items

The customization of context menus is handled via the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[ItemMenuRequested](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.ItemMenuRequested) event.  This event fires whenever a context menu is displayed for an item in the property grid.

The event arguments passed are of type [TreeListBoxItemMenuEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs), which indicates the [IDataModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IDataModel) for which the event was fired in its [Item](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemEventArgs.Item) property.  This [IDataModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IDataModel) could be an [ICategoryModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.ICategoryModel), [IPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel), or [ICategoryEditorModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.ICategoryEditorModel).

Any pre-configured default menu is passed in the [TreeListBoxItemMenuEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs).[Menu](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs.Menu) property.  Its menu items can be changed or removed, or new menu items added.

The default menu is built with all native controls such as `ContextMenu` and `MenuItem`.  This means that custom styles targeting those types can be applied.

If you wish to completely replace the default menu and supply a custom one, set the [TreeListBoxItemMenuEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs).[Menu](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs.Menu) property to a menu of your own.
