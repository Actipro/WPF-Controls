---
title: "Item Conversion"
page-title: "Item Conversion - Breadcrumb Features"
order: 7
---
# Item Conversion

In order to support the various methods of setting the [selected item](item-selection.md), the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) requires that certain information about data objects to be provided upon request. There are two pieces of information that the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) may request: a path string representation of the data object and a top down list of how to find the data object (called a trail).

## Default Handling

When the items presented by the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) are defined using [BreadcrumbItem](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem), then the majority of the conversions are automatically handled. The only requirement is that the [BreadcrumbItem](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem).[PathEntry](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbItem.PathEntry) property be set to a `String` that uniquely identifies the item among its siblings. This behavior can be adjusted or completely replaced using the [ConvertItem](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ConvertItem) event described in the following section.

## Handling Conversion Requests

The [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) raises the [ConvertItem](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ConvertItem) event, when it needs a path or a trail.  The event arguments are an instance of [BreadcrumbConvertItemEventArgs](xref:@ActiproUIRoot.Controls.Navigation.BreadcrumbConvertItemEventArgs) and include information about the conversion request.

This sample code shows how to define an event handler for the [ConvertItem](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ConvertItem) event:

```xaml
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"
...
<navigation:Breadcrumb ConvertItem="OnBreadcrumbConvertItem"
                       ... />
```

```csharp
private void OnBreadcrumbConvertItem(object sender, BreadcrumbConvertItemEventArgs e) {
	// Set e.Path from e.Item, if e.TargetType == BreadcrumbConvertItemTargetType.Path
	// Set e.Trail from e.Item or e.Path, if e.TargetType == BreadcrumbConvertItemTargetType.Trail
	...
}
```

## Path Conversion

The path of a data object is displayed in the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) when in [edit mode](edit-mode.md) and is accessible through the [SelectedPath](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.SelectedPath) property. A path can be any string but should uniquely identify the given item. This works much like file system paths, where no two files have the same path.

For path conversions, the event arguments for the [ConvertItem](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ConvertItem) event will include the data object whose path is needed.

## Trail Conversion

The trail of a data object direct the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control to the data object in the hierarchy. The trail is needed when a user enters a path in edit mode, selects an item from the drop-down list, the [SelectedItem](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.SelectedItem) property is set programmatically, or the [SelectedPath](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.SelectedPath) property is set programmatically. Without the trail, the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) would not be able to find the data object and would be unable to select it.

The trail is simply an `IList`, which includes all the parent items of the specified data object and the specified data object. For example, the conversion for the path `"RootItem\\ChildItem\\GrandchildItem"` should return an `IList` whose first item is the data object for `"RootItem"`, second item is the data object for `"ChildItem"`, and third item is the data object for `"GrandchildItem"`.

For trail conversions, the event arguments for the [ConvertItem](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ConvertItem) event will include the data object, or its path, whose trail is needed.
