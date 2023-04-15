---
title: "Selection"
page-title: "Selection - Tree Control Features"
order: 5
---
# Selection

Both single and multi-selection modes are supported.  Various properties and events allow for the selection of certain items to be blocked.

## Single or Multiple Selection

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[SelectionMode](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.SelectionMode) property indicates the kind of selection supported by the control.  The default is `Single`.

| Mode | Description |
|-----|-----|
| Single | The user can select only one item at a time. |
| Multiple | The user can select multiple items without holding down a modifier key. |
| Extended | The user can select multiple consecutive items while holding down the <kbd>Shift</kbd> key or can toggle selection on items while holding the <kbd>Ctrl</kbd> key.  This is a more common selection mode for multi-select over the `Multiple` option. |

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[SelectedItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.SelectedItem) property gets and sets the primary selected item, while the [SelectedItems](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.SelectedItems) property contains the collection of currently-selected items.

## Returning Selectability

The control calls the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetIsSelectable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsSelectable*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) to return whether an item is capable of being selected.  This method returns `true` by default but can return `false` for items that aren't currently capable of being selected, such as for temporary placeholder items.

A `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsSelectableBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsSelectableBinding) property to tell how an item should retrieve the path segment.  The [GetIsSelectable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsSelectable*) method will use that binding if it is supplied, and will otherwise return `null`.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetIsSelectable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsSelectable*) method instead with custom logic to retrieve the appropriate value.

This example code shows how to override the method for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override bool GetIsSelectable(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.IsSelectable : true);
}
```

## Multiple Selection Filtering

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[MultiSelectionKind](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.MultiSelectionKind) property (of type [TreeMultiSelectionKind](xref:@ActiproUIRoot.Controls.Grids.TreeMultiSelectionKind)) can be used when multi-selection is enabled.  It can apply restrictions to items being added to an existing selection.  The default is `Any`, meaning any items can be added to the selection.  This table lists the other options:

| Mode | Description |
|-----|-----|
| Any | Any node can be selected. |
| SameType | Any node that is the same type can be selected. |
| SameDepth | Any node at the same depth can be selected. |
| Siblings | Any node that shares the same parent can be selected. |

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemSelecting](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemSelecting) event is also raised prior to any selection being made.  The event is passed arguments of type [TreeListBoxItemEventArgs](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs), which specifies the related [Item](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Item) and its UI [Container](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Container).  If you wish to prevent a certain item from being selected, set the event args' `Cancel` property to `true`.

## Syncing Item Selection State

The control needs to two-way communicate with items regarding their selection state.  This is accomplished by calls to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetIsSelected](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsSelected*) and [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[SetIsSelected](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsSelected*) methods (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)).

A two-way `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsSelectedBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsSelectedBinding) property to tell how an item should retrieve the selection state.  The [GetIsSelected](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsSelected*) method will use that binding if it is supplied, and will otherwise return `false`.  The [SetIsSelected](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsSelected*) method will also use that binding if it is supplied.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetIsSelected](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsSelected*) and [SetIsSelected](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsSelected*) methods instead with custom logic to retrieve/set the appropriate values.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsSelectedPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsSelectedPath) property specifies the property name to watch for in case your item implements `INotifyPropertyChanged`.  When a change to that property is detected, the updated value will be requested by the control.  Note that even though a binding can be used via the [IsSelectedBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsSelectedBinding) property, for performance reasons, it is just used temporarily to get/set the value in the adapter methods' default implementations.  The binding doesn't persist, and that's why the [IsSelectedPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsSelectedPath) property is required for property change notifications.

This example code shows how to override the methods for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override bool GetIsSelected(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.IsSelected : false);
}

public override void SetIsSelected(object item, bool value) {
	var model = item as TreeNodeModel;
	if (model != null)
		model.IsSelected = value;
}
```

## Selection Events

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[SelectionChanged](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.SelectionChanged) event is raised whenever the selection (such the values stored in the [SelectedItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.SelectedItem) and [SelectedItems](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.SelectedItems) properties) has changed.

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemSelecting](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemSelecting) event is raised before an item is about to be selected.  The `Cancel` property on the event arguments can be set to `true` to prevent the item from being selected.
