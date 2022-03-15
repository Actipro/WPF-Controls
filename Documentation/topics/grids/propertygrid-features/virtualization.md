---
title: "Virtualization"
page-title: "Virtualization - PropertyGrid Features"
order: 30
---
# Virtualization

The property grid control uses both UI virtualization and lazy loading techniques to achieve maximum performance.

## UI Virtualization

UI virtualization means that if many property grid items (categories, properties, etc.) are loaded in the property grid, containers are only generated for items that are visible or are close to being visible.  Without UI virtualization enabled, containers would be generated for all items in the control, regardless of whether they are (or are close to being) visible, and that can severely affect UI performance.  Especially since a property grid can contain a large number of items based on the data object(s) being examined.

Since property grid uses a selector to switch styles/templates for items (between properties, categories, etc.), "Standard" virtualization mode is used by default instead of "Recycling".

@if (wpf) {

> [!NOTE]
> "Recycling" virtualization mode is not used since it can run into display issues on older .NET framework versions.

}

For more information on UI virtualization and the related features inherited via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox), please see the tree control [Virtualization](../tree-control-features/virtualization.md) topic.

## Lazy Loading

The property grid is designed to lazy load items.  When property items are generated for a root data object, the list of root properties is first retrieved by a call to the [data factory](data-models.md).  Then additional calls are made to the data factory for each of the property value objects.  This is done to see if any of them have child properties, so that property grid knows if the root property should be marked as expandable.

No deeper calls to the data factory are made until one of those root properties is expanded, at which point each of the child properties of the expanded property's value object are examined in the same way to see if any of them are expandable.

This process ensures that if a large hierarchy of expandable objects is used with property grid, it's doing the minimum number of calls to the data factory to keep the property grid UI up-to-date.
