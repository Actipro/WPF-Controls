---
title: "Supported Property Attributes"
page-title: "Supported Property Attributes - PropertyGrid Features"
order: 6
---
# Supported Property Attributes

The [data models and factories](data-models.md) are designed to support all the common property attributes found in namespaces like `System.ComponentModel` and `System.ComponentModel.DataAnnotations`.  These property attributes can help control everything from display name to description to visibility and much more.

## System.ComponentModel Attributes

The `System.ComponentModel` attributes in the table below are supported.  The more commonly-used attributes include usage examples.

<table>
<thead>

<tr>
<th>Attribute</th>
<th>Description</th>
</tr>

</thead>
<tbody>

@if (wpf) {
<tr>
<td>

`AttributeProviderAttribute`

</td>
<td>

Specifies another `Type` from which additional attributes should be queried.  This is called attribute redirection.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.attributeproviderattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`BrowsableAttribute`

</td>
<td>

Indicates whether a property should be displayed.  This attribute is compared to the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[BrowsableAttributes](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.BrowsableAttributes), which defaults to a `[Browsable(true)]` entry.

This code shows an example of applying `BrowsableAttribute` to hide the `Bar` property:

```csharp
public class Foo {
	[Browsable(false)]
	public int Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.browsableattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`CategoryAttribute`

</td>
<td>

Specifies the name of the category in which to group the property.

When [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[IsCategorized](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.IsCategorized) is set to `false` this attribute is ignored, as the properties are not categorized.  Properties that do not have an explicitly defined category name will be placed in a miscellaneous category, whose display name can be customized by changing the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[MiscCategoryName](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.MiscCategoryName) property.

This code shows an example of applying `CategoryAttribute` to set the property's category:

```csharp
public class Foo {
	[Category("Common")]
	public int Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.categoryattribute.aspx) for more information on this attribute.

</td>
</tr>
}

<tr>
<td>

`DefaultValueAttribute`

</td>
<td>

Specifies the default value for a property.

The default value can be used when determine if a property has been modified, or when resetting a property.  The value specified by this attribute takes precedence over the "Reset"/"ShouldSerialize" methods, if any.

This code shows an example of applying `DefaultValueAttribute` to set the property's default value:

```csharp
public class Foo {
	[DefaultValue(5)]
	public int Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.defaultvalueattribute.aspx) for more information on this attribute.

</td>
</tr>

@if (wpf) {
<tr>
<td>

`DescriptionAttribute`

</td>
<td>

Specifies a description for a property.

The description is displayed in the [summary area](summary-area.md) of a property grid.

This code shows an example of applying `DescriptionAttribute` to set the property's description:

```csharp
public class Foo {
	[Description("A description about Bar.")]
	public int Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.descriptionattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`DisplayNameAttribute`

</td>
<td>

Specifies the display name for a property.

The display name typically a user friendly version of the property name (e.g. "Display Name" versus "DisplayName").  This string is displayed in the name column and [summary area](summary-area.md) of a property grid.  If not specified, then the property name is used.

This code shows an example of applying `DisplayNameAttribute` to set the property's display name:

```csharp
public class Foo {
	[DisplayName("Bar Property")]
	public int Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.displaynameattribute.aspx) for more information on this attribute.

</td>
</tr>
}

<tr>
<td>

`EditorAttribute`

</td>
<td>

Specifies the editor to use to change a property.

In the context of property grid, this attribute can be used to specify the [property editor](property-editors.md) used for selecting a name cell or value editor cell `DataTemplate`.  This property editor would override any other default property editor that normally would have been used.

This code shows an example of applying `EditorAttribute` to set the property's editor to use a [DatePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor) instead of a default [DateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor) when [Editors interop](../../editors/interoperability/propertygrid.md) is being used:

```csharp
public class Foo {
	[Editor(typeof(DatePropertyEditor), typeof(PropertyEditor))]
	public DateTime Bar { get; set; }
}
```

See the [Property Editors](property-editors.md) for more information on how to use this attribute.

@if (winrt) {

> [!NOTE]
> `EditorAttribute` doesn't exist natively in the @@PlatformName platform, but a special implementation of it is included in the [ActiproSoftware.Compatibility](xref:ActiproSoftware.Compatibility) namespace.

}

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.editorattribute.aspx) for more information on this attribute.

</td>
</tr>

@if (wpf) {
<tr>
<td>

`ImmutableObjectAttribute`

</td>
<td>

Specifies that an object is immutable and has no child properties capable of being edited.  Child properties of immutable objects cannot be modified.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.immutableobjectattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`MergablePropertyAttribute`

</td>
<td>

Indicates whether a property can be combined with other properties, when multiple selected objects are being modified at once.

See the [Multiple Objects](multiple-objects.md) topic for more information on property merging.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.mergablepropertyattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`NotifyParentPropertyAttribute`

</td>
<td>

Indicates whether the parent property is notified when the value of the property that this attribute is applied to is modified.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.notifyparentpropertyattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`ParenthesizePropertyNameAttribute`

</td>
<td>

Indicates whether the name of a property is displayed within parentheses (e.g. "(Name)" versus "Name").

This attribute is typically used to sort more important properties to the top, when sorting alphabetically.  However other [sorting features](categorization-and-sorting.md) can alter sort order.  In addition, the parentheses make the property stand out slightly.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.parenthesizepropertynameattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`ReadOnlyAttribute`

</td>
<td>

Indicates whether a property is read-only or read/write.

Properties that do not have a `set` accessor are automatically considered read-only.  If a `set` accessor is available, then this attribute can be used to force the property to be displayed as read-only.

This code shows an example of applying `ReadOnlyAttribute` to force the property to be read-only when in a property grid:

```csharp
public class Foo {
	[ReadOnly(true)]
	public DateTime Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.readonlyattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`TypeConverterAttribute`

</td>
<td>

Specifies a `TypeConverter` class that should be used to convert values for a property.

`TypeConverter`-derived classes offer alot of functionality in allowing various types of input.  For example, the `EnumConverter` class allows a property, whose type is an enum, to be set to the string representation of one of the enum values.  Without `EnumConverter`, only actual enum values could be assigned to the property.

A custom `TypeConverter` can be used to validate or coerce input as needed.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.typeconverterattribute.aspx) for more information on this attribute.

</td>
</tr>
}

@if (wpf) {
<tr>
<td>

`TypeDescriptionProviderAttribute`

</td>
<td>

Specifies the custom `TypeDescriptionProvider` for a class.

This is currently only supported by the `TypeDescriptorFactory`.

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.typedescriptionproviderattribute.aspx) for more information on this attribute.

</td>
</tr>
}

</tbody>
</table>

## System.ComponentModel.DataAnnotations Attributes

@if (wpf) {

The `System.ComponentModel.DataAnnotations.dll` assembly has other attributes that can be used by property data and factories. 

}

The `System.ComponentModel.DataAnnotations` attributes in the table below are supported.  The more commonly-used attributes include usage examples.

<table>
<thead>

<tr>
<th>Attribute</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

`DisplayAttribute`

</td>
<td>

Provides a general-purpose attribute that lets you specify [sort order](categorization-and-sorting.md) and localizable strings for category, display name, description.

@if (wpf) {

This attribute can be used in place of `CategoryAttribute`, `DisplayNameAttribute`, and `DescriptionAttribute`.  It is nicer since only one attribute needs to be used for all these data annoatations and the values returned are localizable with string resources. 

}

This code shows an example of applying `DisplayAttribute` to set sort order (via `Order`), category (via `GroupName`), display name (via `ShortName`), and description (via `Description`) for a property:

```csharp
public class Foo {
	[Display(Order = 4, GroupName = "Common", ShortName = "Bar Property", Description = "A description about Bar.")]
	public int Bar { get; set; }
}
```

See [MSDN (external)](http://msdn.microsoft.com/en-us/library/system.componentmodel.attributeproviderattribute.aspx) for more information on this attribute.

</td>
</tr>

<tr>
<td>

`EditableAttribute`

</td>
<td>

Indicates whether a property is editable.

@if (wpf) {

This attribute is similar to `ReadOnlyAttribute` but in opposite terms. 

}

When not specified, the property is editable by default if there is a `set` accessor available.  However if this attribute is supplied with its `AllowEdit` property set to `false`, the property will be displayed as read-only.

This code shows an example of applying `EditableAttribute` to force the property to be read-only when in a property grid:

```csharp
public class Foo {
	[Editable(false)]
	public int Bar { get; set; }
}
```

See [MSDN (external)](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.editableattribute) for more information on this attribute.

</td>
</tr>

</tbody>
</table>

## Browsable Properties

The properties returned by the built-in factories can be filtered based on a set of "browsable attributes". These attributes, which are defined in the [BrowsableAttributes](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.BrowsableAttributes) property, must be explicitly defined on a property or must be the default value for the given attribute `Type`. In addition, any settings stored by the attribute must be equal, such as the `BrowsableAttribute.Browsable`.

@if (wpf) {

The default value of `BrowsableAttributes` contains only one entry, which is `BrowsableAttribute.Yes`. This is a statically defined instance of `BrowsableAttribute` that indicates a property is browsable. If a property does not explicitly define a `BrowsableAttribute`, then it is assumed to be browsable. Therefore, the default setting `BrowsableAttributes` filters out properties that have been explicitly marked with `BrowsableAttribute.No`. 

}
