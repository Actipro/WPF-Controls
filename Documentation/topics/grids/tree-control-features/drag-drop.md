---
title: "Drag and Drop"
page-title: "Drag and Drop - Tree Control Features"
order: 11
---
# Drag and Drop

This control supports the standard system drag and drop functionality and even has built-in features for displaying an optional drop indicator over items when dragging over the control.  Full control over the drag and drop capabilities and drop result is available.

## Dragging Support

Dragging is not enabled by default and must be activated by setting the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[CanDragItems](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.CanDragItems) property to `true`.

When dragging is enabled and dragging over collapsed items, they will expand after a delay.  Similarly, dragging around the very top and bottom of the control will scroll the control.

## Multiple Item Dragging and Filtering

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[MultiDragKind](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.MultiDragKind) property (of type [TreeMultiDragKind](xref:@ActiproUIRoot.Controls.Grids.TreeMultiDragKind)) can be used when multiple item drag is enabled.  It can apply restrictions so that certain items cannot be dragged together.  The default is `None`, meaning only single item selection drags are permitted.  This table lists the other options:

| Mode | Description |
|-----|-----|
| None | Drags are not allowed when multiple items are selected.  Only single item selection drags are permitted. |
| Any | Any node can be dragged. |
| SameType | Any node that is the same type can be dragged. |
| SameDepth | Any node at the same depth can be dragged. |
| Siblings | Any node that shares the same parent can be dragged. |

## Initializing a DataObject

When items are about to be dragged, the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[InitializeDataObject](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.InitializeDataObject*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) is called to initialize the drag data.

@if (winrt) {

By default, the [InitializeDataObject](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.InitializeDataObject*) method does nothing and needs an override implementation for drag support. 

}

@if (wpf) {

By default, the [InitializeDataObject](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.InitializeDataObject*) method simply returns `DragDropEffects.None` and needs an override implementation for drag support. 

}

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemDataFormat](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemDataFormat) format can be used to store custom data referencing each item being dragged.  We recommend storing a string object in that format where each line in the string is a full path to the item.  See the [Item Paths](item-paths.md) topic for more information on paths.  Each full path should be able to uniquely identify the item.

Other formats can be added to the drag data.  For instance, if one item is being dragged, it's common to set a text value that contains the primary text displayed for the item.  This text value could then be dropped onto other external controls like `TextBox` controls.

> [!NOTE]
> A full sample of drag and drop functionality is included in the product QuickStarts.  It contains fairly extensive logic and should be referred to for better understanding of how drag and drop works.

## Dropping Support

Dropping is not enabled by default.  Set the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).AllowDrop property to `true` to allow dropping.  When dropping is allowed, various notification methods will be called on the item adapter as described below.  Dropped items can be sourced from the same control or from external controls as well.

## Handling OnDragOver and OnDrop Notifications

@if (winrt) {

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[OnDragOverAsync](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.OnDragOverAsync) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) notifies that a drag event occurred over a target item, requests that appropriate updates are made to the supplied `DragEventArgs`, and requests that the allowed drop area is returned for visual target feedback. 

}

@if (wpf) {

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[OnDragOver](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.OnDragOver*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) notifies that a drag event occurred over a target item, requests that appropriate updates are made to the supplied `DragEventArgs`, and requests that the allowed drop area is returned for visual target feedback. 

}

The default implementation of this method flags that dropping is not allowed and returns [TreeItemDropArea](xref:@ActiproUIRoot.Controls.Grids.TreeItemDropArea).[None](xref:@ActiproUIRoot.Controls.Grids.TreeItemDropArea.None), meaning no visual feedback should be given.  Override this method if you wish to support dropping and add logic to properly handle the dragged data.

The target item (which can be null if over the whitespace area below the last item in the control) and a [TreeItemDropArea](xref:@ActiproUIRoot.Controls.Grids.TreeItemDropArea) based on hit testing over the target item are passed in as arguments.  If dropping is to be supported and a drop indicator should be displayed, the [TreeItemDropArea](xref:@ActiproUIRoot.Controls.Grids.TreeItemDropArea) returned can be the same value, or coerced to another value.  The resulting value will affect the displayed drop indicator.  Possible values are `None` (no indicator), `Before` (small bar at the top of the target item), `On` (a highlight under the item), or `After` (small bar at the bottom of the target item).  Some implementations of drag/drop may wish to keep the drop indicators simple by only returning `None` or `On` (coercing `Before` and `After` to `On`).

@if (winrt) {

The `DragEventArgs.AcceptedOperation` property should also be set appropriately. 

}

@if (wpf) {

The `DragEventArgs.Effects` property should also be set appropriately. 

}

@if (winrt) {

Finally if a drop does occur, the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[OnDropAsync](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.OnDropAsync) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) is called and requests that appropriate updates are made to the supplied `DragEventArgs`. 

}

@if (wpf) {

Finally if a drop does occur, the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[OnDrop](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.OnDrop*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) is called and requests that appropriate updates are made to the supplied `DragEventArgs`. 

}

The default implementation of this method flags that no drop occurred and takes no further action.  Override this method if you wish to support dropping and add logic to properly handle the dragged data.

Here again, the target item (which can be null if over the whitespace area below the last item in the control) and a [TreeItemDropArea](xref:@ActiproUIRoot.Controls.Grids.TreeItemDropArea) based on hit testing over the target item are passed in as arguments.  An override of this method takes appropriate action to update tree items.  This could involve moving items, copying items, inserting new items, etc.

> [!NOTE]
> A full sample of drag and drop functionality is included in the product QuickStarts.  It contains fairly extensive logic and should be referred to for better understanding of how drag and drop works.

## Expanding an Item on Drag/Hover

By default, when a drag occurs over an item and the pointer hovers for a brief time, the item will expand if it is currently collapsed and expandable.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[OnDragHover](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.OnDragHover*) method is called when this scenario occurs, allowing you to return a [TreeItemExpandability](xref:@ActiproUIRoot.Controls.Grids.TreeItemExpandability).  The default implementation of that method simply returns `TreeItemExpandability.Auto`.

This method can be overridden in scenarios where you don't wish to allow drag/hover to auto-expand collapsed items.  The related `DragEventArgs` and target item are passed in.  Logic can be used to determine if expansion is supported based on the dragged data and target item combination.  Return `Never` for target items that shouldn't support expansion on drag/hover.
