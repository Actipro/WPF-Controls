---
title: "Expandability"
page-title: "Expandability - Tree Control Features"
order: 4
---
# Expandability

Everything from when children are queried to expander display is configurable, and events allow for expand/collapse operations to be canceled.  Child items can be loaded asynchronously while a progress spinner shows that activity is occurring.

## Syncing Item Expansion State

The control can two-way communicate with items regarding their expansion state.  This is accomplished by calls to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsExpanded*) and [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[SetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsExpanded*) methods (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)).

A two-way `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsExpandedBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsExpandedBinding) property to tell how an item should retrieve the selection state.  The [GetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsExpanded*) method will use that binding if it is supplied, and will otherwise return `false`.  The [SetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsExpanded*) method will also use that binding if it is supplied.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsExpanded*) and [SetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsExpanded*) methods instead with custom logic to retrieve/set the appropriate values.

> [!NOTE]
> The [GetIsExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsExpanded*) method is generally called much more than other item adapter methods and as such, its performance if using bindings per above can cause UI sluggishness.  That being said, for any small to medium size trees, using bindings is generally fine.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsExpandedPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsExpandedPath) property specifies the property name to watch for in case your item implements `INotifyPropertyChanged`.  When a change to that property is detected, the updated value will be requested by the control.  Note that even though a binding can be used via the [IsExpandedBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsExpandedBinding) property, for performance reasons, it is just used temporarily to get/set the value in the adapter methods' default implementations.  The binding doesn't persist, and that's why the [IsExpandedPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsExpandedPath) property is required for property change notifications.

This example code shows how to override the methods for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override bool GetIsExpanded(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.IsExpanded : false);
}

public override void SetIsExpanded(object item, bool value) {
	var model = item as TreeNodeModel;
	if (model != null)
		model.IsExpanded = value;
}
```

## Expand and Collapse Events

There are several events on [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) that are raised when items are expanding and collapsing.  All of them are passed an event arguments object of type [TreeListBoxItemExpansionEventArgs](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemExpansionEventArgs), which specifies the related [Item](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Item) and its UI [Container](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Container).

In addition, the event arguments indicate the kind of expand/collapse (`Single`, `Branch`, or `All`) via the [Kind](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemExpansionEventArgs.Kind) property and also the root item where the event occurred via the [SourceItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemExpansionEventArgs.SourceItem) property.  This source item is the same as the [Item](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Item) when affecting a single item, specifies the branch root item when affecting a branch of items, and is null when affecting all items.  Branch and all expand/collapse scenarios occur when pressing `*`, `Ctrl+*`, `/`, and `Ctrl+/`.  See the [Keyboard Shortcuts](keyboard-shortcuts.md) topic for more information.

The [ItemExpanding](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemExpanding) event is raised immediately before an item is expanded.  Set its `Cancel` property to `true` to prevent the expand.  The [ItemExpanded](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemExpanded) event is raised after the expand.

The [ItemCollapsing](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemCollapsing) event is raised immediately before an item is collapsed.  Set its `Cancel` property to `true` to prevent the collapse.  The [ItemCollapsed](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemCollapsed) event is raised after the collapse.

## Querying Children on Expansion

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[ChildrenQueryModeDefault](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ChildrenQueryModeDefault) property (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) returns a [TreeItemChildrenQueryMode](xref:@ActiproUIRoot.Controls.Grids.TreeItemChildrenQueryMode) value that determines when children are queried.  The default value is `OnDisplay`, meaning that any time an item first becomes visible, a query will be made for its child items.  If no child items are found, the item's expander will be hidden.

If the property is changed to `OnExpansion` instead, a query will only be made for the child items when it is first expanded.  Again, if no child items are found, the item's expander will be hidden.

## Expander Display

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetExpandability](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetExpandability*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) returns a [TreeItemExpandability](xref:@ActiproUIRoot.Controls.Grids.TreeItemExpandability) value that indicates if a given item can be expandable, assuming it has child items.  The default logic for this method will return `Never` if the specified item is top-level and the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[TopLevelExpandabilityDefault](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.TopLevelExpandabilityDefault) property value is `Never`.  Otherwise, the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[ExpandabilityDefault](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ExpandabilityDefault) property value is returned.

A [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetExpandability](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetExpandability*) method result of `Never` will never show an expander and double-clicking items won't toggle their expansion state.  This is useful when you wish to display a full tree without letting the user toggle expansion.  A result of `Auto` means the children querying logic described above will determine if the expander is visible.

If an item doesn't initially have any children, thereby preventing the expander from displaying, and at a later time children are added while the item is collapsed, the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[InvalidateChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.InvalidateChildren*) method can be called to clear the cached state.

> [!NOTE]
> If you wish to always have top-level items expanded, ensure the top-level items are expanded and set [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[TopLevelExpandabilityDefault](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.TopLevelExpandabilityDefault) to `Never`.  It is also generally advised to set [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[TopLevelIndent](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.TopLevelIndent) to `0` in this case so there is no whitespace to the left of the top-level items.

## Optimizing Leaf Node Items

Sometimes certain items might be of a type that they are always leaves in the tree and can never have child items.  This is very applicable when using the [TreeItemChildrenQueryMode](xref:@ActiproUIRoot.Controls.Grids.TreeItemChildrenQueryMode).[OnExpansion](xref:@ActiproUIRoot.Controls.Grids.TreeItemChildrenQueryMode.OnExpansion) query mode.  In these cases, you can override the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[CanHaveChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.CanHaveChildren*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)) to return false.  It returns `true` by default, and should never return `false` if the item can have child items, but doesn't at the current time.  Optimizations are in place to not call [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetChildren*) when this method returns `true` for an item.

This example code shows how to override the method to return `true` if the item is of type `LeafTreeNodeModel`.

```csharp
public virtual bool CanHaveChildren(object item) {
	return (item is LeafTreeNodeModel);
}
```

## Async Loading of Children

Sometimes child item queries can be lengthy operations and it's not desirable to lock up the UI while they occur.  Scenarios where this is applicable are file system, web, or database queries. [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) fully supports async loading of child items.  While an item is flagged as loading, it will keep its expander visible.

When setting the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[ChildrenQueryModeDefault](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ChildrenQueryModeDefault) property to [TreeItemChildrenQueryMode](xref:@ActiproUIRoot.Controls.Grids.TreeItemChildrenQueryMode).[OnExpansion](xref:@ActiproUIRoot.Controls.Grids.TreeItemChildrenQueryMode.OnExpansion), you can add a [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemExpanding](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemExpanding) event handler.  In that event handler, inform the control that the item is doing async loading and start an async `Task` with your loading code.  The task's logic should execute your potentially lengthy loading code and end with a `Dispatcher.BeginInvoke` that creates the new child items, adds them to the parent item's children collection, and finally informs the control that the item is done async loading.

The loading state is provided to the control by calls to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetIsLoading](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsLoading*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)).

A `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsLoadingBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsLoadingBinding) property to tell how an item should retrieve the loading state.  The [GetIsLoading](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsLoading*) method will use that binding if it is supplied, and will otherwise return `false`.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetIsLoading](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsLoading*) method instead with custom logic to retrieve the appropriate value.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsLoadingPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsLoadingPath) property specifies the property name to watch for in case your item implements `INotifyPropertyChanged`.  When a change to that property is detected, the updated value will be requested by the control.  Note that even though a binding can be used via the [IsLoadingBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsLoadingBinding) property, for performance reasons, it is just used temporarily to get/set the value in the adapter methods' default implementations.  The binding doesn't persist, and that's why the [IsLoadingPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsLoadingPath) property is required for property change notifications.

This example code shows how to override the method for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override bool GetIsLoading(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.IsLoading : false);
}
```

> [!NOTE]
> It is a good idea to swap out the image for the item that is performing async loading and to replace it with a small [progress spinner](../../shared/windows-controls/progress-spinners.md) until the loading is complete.

A full sample of how everything described above works is in the Folder Browser demo.
