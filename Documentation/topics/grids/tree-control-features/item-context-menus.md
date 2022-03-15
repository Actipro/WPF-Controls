---
title: "Item Context Menus"
page-title: "Item Context Menus - Tree Control Features"
order: 9
---
# Item Context Menus

Context menus can be generated dynamically upon request by items.

## Dynamically Creating Context Menus

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemMenuRequested](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemMenuRequested) event is raised whenever a context menu is required for an item.  The event is passed arguments of type [TreeListBoxItemMenuEventArgs](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemMenuEventArgs), which specifies the related [Item](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Item) and its UI [Container](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Container).

No context menu is automatically created by default.  It is up to you to set the [TreeListBoxItemMenuEventArgs](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemMenuEventArgs).[Menu](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemMenuEventArgs.Menu) property to a menu you generate.  Responding to the event allows you to dynamically build a menu based on the context of the item.
