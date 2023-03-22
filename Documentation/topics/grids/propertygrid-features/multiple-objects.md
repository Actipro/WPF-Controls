---
title: "Multiple Objects"
page-title: "Multiple Objects - PropertyGrid Features"
order: 20
---
# Multiple Objects

The property grid fully supports editing properties on multiple objects at the same time.  When in a multiple object scenario, only the mergeable properties of the same name and `Type` will be displayed in the property grid.

## Setting Multiple Objects

The [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) property can be set or bound to any collection that implements `IEnumerable`.

> [!NOTE]
> When the [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) property is updated, then the [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) property is automatically set to the first item in the list, or to `null` if there are no items.

This code shows how to the [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) property can be bound to the selected items of a `ListBox`:

```xaml
<grids:PropertyGrid DataObjects="{Binding SelectedItems, ElementName=listBox}" />
```

## Factory Support

The built-in [data factory](data-models.md) has special support for merging the properties of more than one data object.  The properties of all the data objects are retrieved, and all the common properties are located.  Common properties are those that have same name and `Type` and that exist on all the data objects.  A [MergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.MergedPropertyModel) is created for each merged property, which effectively wraps the related [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) for each of the data object's properties that were merged together into one.

When information about a merged property is requested, the [MergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.MergedPropertyModel) uses information from the wrapped property models to determine the best possible "merged information".  For example, if one wrapped property accessor is read-only, then the merged property is considered read-only.

> [!TIP]
> The `MergablePropertyAttribute` can be used on a property to prevent it from being merged.  Also, only one property from all the data objects needs to be marked as not mergable.

## Displaying Values

Because only a single value can be displayed in the property for single property, the values of multiple properties must be compared.  If all the values are equal, then that single value is used.  In all other instances, `null` is used.  The get method for the [MergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.MergedPropertyModel).[Value](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CachedPropertyModelBase.Value) property performs this comparison.

It is possible to get the unique values of all the selected objects using the get method for the [MergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.MergedPropertyModel).[Values](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CachedPropertyModelBase.Values) property.  A custom property editor could then be developed that presented each unique value, if so desired.

## Updating Values

When updating multiple objects, the specified value is set on each object in turn.  If any errors occur in the process of updating, then the remaining objects are not updated.
