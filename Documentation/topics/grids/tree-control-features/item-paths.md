---
title: "Item Paths"
page-title: "Item Paths - Tree Control Features"
order: 7
---
# Item Paths

Sometimes it's handy to be able to supply a string path to obtain or work with an item.  Each item can provide its own path.  A full path can be constructed by appending a path separator delimiter and the item's path to its parent's full path.

## Providing an Item Path

An item's path segment is provided to the control by calls to the [TreeListBoxItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter).[GetPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.GetPath*) method (via [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[ItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.ItemAdapter)).

A `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter).[PathBinding](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.PathBinding) property to tell how an item should retrieve the path segment.  The [GetPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.GetPath*) method will use that binding if it is supplied, and will otherwise return null.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.GetPath*) method instead with custom logic to retrieve the appropriate value.

This example code shows how to override the method for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override string GetPath(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.Name: null);
}
```

## Setting the Path Separator

The [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[PathSeparator](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.PathSeparator) property is what defines the delimiter text that separates each item's path segment to build a full path.  Its default value is `\`.

## Getting an Item From a Full Path

If you know the full path of an item and wish to obtain the item itself, you can call the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[GetItemByFullPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.GetItemByFullPath*) method, passing in the full path as an argument.

## Getting an Item's Full Path

If you have an item reference and wish to obtain its full path, you can call the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[GetFullPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.GetFullPath*) method, passing in the item as an argument.

## Other Operations by Full Path

This methods can be executed by supplying a full path.

| Member | Description |
|-----|-----|
| [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[BringItemIntoViewByFullPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.BringItemIntoViewByFullPath*) | Brings the container for the item with the specified full path into view. |
| [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[FocusItemByFullPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.FocusItemByFullPath*) | Focuses the container for the node with the specified full path, scrolling it into view as needed. |
| [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[SelectItemByFullPath](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.SelectItemByFullPath*) | Sets the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[SelectedItem](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.SelectedItem) to the node with the specified full path. |
