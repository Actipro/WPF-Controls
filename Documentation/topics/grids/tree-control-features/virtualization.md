---
title: "Virtualization"
page-title: "Virtualization - Tree Control Features"
order: 20
---
# Virtualization

The tree controls are lightning-fast because they have UI virtualization on by default.  Data virtualization is an additional option for scenarios where virtualized child collections are in use.

## UI Virtualization

UI virtualization means that if many items are loaded in the control, containers are only generated for items that are visible or are close to being visible.  Without UI virtualization enabled, containers would be generated for all items in the control, regardless of whether they are (or are close to being) visible, and that can severely affect UI performance.

The tree controls have "Recycling" UI virtualization active by default, and nothing needs to be done to configure it.  As the control is scrolled, containers for items that are scrolled out of view may be recycled and reused for other items that are being scrolled into view.  This recycling helps performance because it prevents UI elements from being created new for every container that is needed.

> [!NOTE]
>
> If you see issues with bound data showing up on incorrect containers or styles not being applied properly, especially while scrolling, please see the [Troubleshooting](../troubleshooting.md) topic.  That topic has a section that describes this scenario in more detail and talks about how to change the virtualization mode back to "Standard", which should solve the issue.  "Standard" virtualization is a little slower than "Recycling" virtualization but is still fast.
>
> The problems described above mostly tend to happen in scenarios where you are switching styles/templates for tree control items on the fly with a selector, based on data.  Most tree control usage scenarios don't use selectors, and thus shouldn't run into the problem.

## Data Virtualization

Normally, when the control goes to query for an item's children, it will do a for-each through all the items that are indicated as the children's `IEnumerable`.  In some special scenarios where a virtualizing list is used as the children collection, and that virtualizing list should only do paged lookups of data based on what is needed for display, then data virtualization can be enabled by setting the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[IsDataVirtualizationEnabled](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.IsDataVirtualizationEnabled) property to `true`.  Otherwise, just leave the property its default of `false`.

When data virtualization is enabled and an item's children are being queried, the control will first see if the children's `IEnumerable` is an `IList`.  If so, it will enter data virtualization mode and will populate a list of [ITreeItemVirtualPlaceholder](xref:@ActiproUIRoot.Controls.Grids.ITreeItemVirtualPlaceholder) objects, one for each child in the source list.  This process does not actually access any items in the source list.  It only checks the `Count` of the source list. [ITreeItemVirtualPlaceholder](xref:@ActiproUIRoot.Controls.Grids.ITreeItemVirtualPlaceholder) is a simple interface to represent a slot in a virtualized list, and just has an [Index](xref:@ActiproUIRoot.Controls.Grids.ITreeItemVirtualPlaceholder.Index) property.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[CreateVirtualPlaceholder](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.CreateVirtualPlaceholder*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) is called during the process of creating [ITreeItemVirtualPlaceholder](xref:@ActiproUIRoot.Controls.Grids.ITreeItemVirtualPlaceholder) objects.  The method is passed the parent item and the index of the child that needs a virtual placeholder.  Normally this method doesn't need to be overridden since it will return a default placeholder, but you can customize it to return a custom placeholder if you wish.

When a container is generated for an item that is a virtual placeholder, the control will access the parent item's children list and retrieve the child at the index specified by [ITreeItemVirtualPlaceholder](xref:@ActiproUIRoot.Controls.Grids.ITreeItemVirtualPlaceholder).[Index](xref:@ActiproUIRoot.Controls.Grids.ITreeItemVirtualPlaceholder.Index) via its indexer.  This is the point at which a paged lookup in the virtualized list can occur, so that a page of items around the specified item can be loaded if needed.
