---
title: "Expandability"
page-title: "Expandability - PropertyGrid Features"
order: 18
---
# Expandability

Certain complex properties are capable of being expandable in the property grid.  Categories will auto-expand by default, while expandable properties will not auto-expand by default.  This behavior can be changed in several ways.

## Determining Property Expandability

The [PropertyExpandability](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyExpandability) option helps guide if and when a property is deemed expandable.  The [PropertyExpandability](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.PropertyExpandability) enumeration returned by that property has these values:

| Value | Description |
|-----|-----|
| `Default` | Indicates that properties will be expandable if the associated type converter allows sub-properties to be retrieved. |
| `None` | Indicates that properties will never be expandable. |
| `ForceSimple` | Indicates that properties will be forced to be expandable if a custom type converter has not been specified. |
| `ForceAlways` | Indicates that properties will be forced to be expandable even if a custom type converter has been specified.  If a custom type converter has been specified, then it will only be overridden if it does not allow the property to be expandable. |

Most properties aren't expandable, however they can be expandable if their current value has sub-properties of its own.  In that case, the expandability option above governs if a property is expandable.

For the `Default` case, assigning a `TypeConverter` that is or inherits `ExpandableTypeConverter` onto the object type will allow it to be expandable based on the expandability setting.

This code shows how to mark a class as expandable via `ExpandableTypeConverter`:

```csharp
[TypeConverter(typeof(ExpandableObjectConverter))]
public class Foo {
	...
}
```

## Category Auto-Expansion

Categories auto-expand by default, revealing the properties within them.  The [AreCategoriesAutoExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.AreCategoriesAutoExpanded) property can be set to `false` to not auto-expand categories.

## Property Auto-Expansion

Expandable properties don't auto-expand by default.  The [ArePropertiesAutoExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ArePropertiesAutoExpanded) property can be set to `true` to auto-expand all expandable properties.

## Selectively Expanding Categories or Properties

Data factories generate the data models (categories, properties, etc.) that the property grid consumes when building its user interface.  The [IDataModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IDataModel).[IsExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IDataModel.IsExpanded) property determines if a data model is expanded.  It defaults to false, but as mentioned above, the [AreCategoriesAutoExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.AreCategoriesAutoExpanded) and [ArePropertiesAutoExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ArePropertiesAutoExpanded) properties can alter the defaults for categories and properties respectively.

There may be certain scenarios where you wish to only expand certain categories or properties.  In these cases, set [AreCategoriesAutoExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.AreCategoriesAutoExpanded) or and [ArePropertiesAutoExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.ArePropertiesAutoExpanded) (whichever is appropriate) to `false` and then make a [custom data factory](data-models.md).

In the WPF platform, the custom data factory should inherit [TypeDescriptorFactory](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.TypeDescriptorFactory).  Override the [CreatePropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.TypeDescriptorFactory.CreatePropertyModel*) or [CreateCollectionPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.TypeDescriptorFactory.CreateCollectionPropertyModel*) methods as appropriate and call their base methods.  If the [IPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel) result is for a property that should be expanded, set its [IsExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IDataModel.IsExpanded) property to `true`.

Note that you could alternatively add this logic in a more generalized [GetDataModels](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.DataFactoryBase.GetDataModels*) method override.  In that case, call the base method and iterate through the [IPropertyModel](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IPropertyModel) results.  Look for properties that should be expanded and set their [IsExpanded](xref:ActiproSoftware.Windows.Controls.Grids.PropertyData.IDataModel.IsExpanded) properties to `true`.
