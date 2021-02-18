---
title: "Item Context Menus"
page-title: "Item Context Menus - Tree Control Features"
order: 9
---
# Item Context Menus

Context menus can be generated dynamically upon request by items.

## Dynamically Creating Context Menus

The [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[ItemMenuRequested](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.ItemMenuRequested) event is raised whenever a context menu is required for an item.  The event is passed arguments of type [TreeListBoxItemMenuEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs), which specifies the related [Item](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemEventArgs.Item) and its UI [Container](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemEventArgs.Container).

No context menu is automatically created by default.  It is up to you to set the [TreeListBoxItemMenuEventArgs](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs).[Menu](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemMenuEventArgs.Menu) property to a menu you generate.  Responding to the event allows you to dynamically build a menu based on the context of the item.
