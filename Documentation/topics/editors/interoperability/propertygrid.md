---
title: "PropertyGrid"
page-title: "PropertyGrid - Editors Interoperability"
order: 2
---
# PropertyGrid

The controls provided in the Editors product can be easily integrated into the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) control, through the use of custom property editors.  In order to keep both products segregated, an Interop assembly is provided that ties the two products together.

![Screenshot](../images/propertygrid-brusheditbox-open.png)

*PropertyGrid with BrushEditBox integrated*

@if (wpf) {

## Interop Assembly

The Interop functionality is provided through a separate assembly, which is called `ActiproSoftware.Editors.Interop.Grids.Wpf.dll`.  Therefore, a reference to this assembly must be added in order to leverage the property editors it provides.  This assembly should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

}

## Property Editors

The [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) uses "property editors" to tie a properties to controls, by property name and/or property type.  For example, you can tell the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) that all properties of the type `DateTime` should use `MyControlA` and all properties of the type `String` should use `MyControlB`.  This gives you a lot of control over what control is used for the various properties.  In reality, the "control" is actually a `DataTemplate` since it will most likely be used multiple times.

Property editors are defined via an instance of the [PropertyEditor](xref:@ActiproUIRoot.Controls.Grids.PropertyEditors.PropertyEditor) class.  Derivations of this class can provide default values, such as a known `DataTemplate` or property `Type`.  In addition, derivations are required in order to decorate properties with `EditorAttribute`. `EditorAttribute` only allows the editor `Type` to be specified, therefore that `Type` needs to provide a default `DataTemplate` (via a derivation of [PropertyEditor](xref:@ActiproUIRoot.Controls.Grids.PropertyEditors.PropertyEditor)).

> [!NOTE]
> Property editors can actually provide a `DataTemplate` for the value cell and the name cell, but typically the name cell does not need to be altered.

See the [Property Editors](../../grids/propertygrid-features/property-editors.md) topic under Actipro PropertyGrid for more information.

The Interop assembly provides a set of classes that derive from [PropertyEditor](xref:@ActiproUIRoot.Controls.Grids.PropertyEditors.PropertyEditor) and provide default values for the `DataTemplate` used for the value cell and the property `Type`.  These classes include:

<table>
<thead>

<tr>
<th>Class</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[BrushPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.BrushPropertyEditor)

</td>
<td>

Represents an property editor that uses a [BrushEditBox](xref:@ActiproUIRoot.Controls.Editors.BrushEditBox) for editing `Brush` property values.

</td>
</tr>

@if (wpf) {
<tr>
<td>

[BytePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.BytePropertyEditor)

</td>
<td>

Represents an property editor that uses a [ByteEditBox](xref:@ActiproUIRoot.Controls.Editors.ByteEditBox) for editing `Byte` property values.

</td>
</tr>
}

<tr>
<td>

[ColorPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.ColorPropertyEditor)

</td>
<td>

Represents an property editor that uses a [ColorEditBox](xref:@ActiproUIRoot.Controls.Editors.ColorEditBox) for editing `Color` property values.

</td>
</tr>

<tr>
<td>

[CornerRadiusPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.CornerRadiusPropertyEditor)

</td>
<td>

Represents an property editor that uses a [CornerRadiusEditBox](xref:@ActiproUIRoot.Controls.Editors.CornerRadiusEditBox) for editing `CornerRadius` property values.

</td>
</tr>

<tr>
<td>

[DatePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor)

</td>
<td>

Represents an property editor that uses a [DateEditBox](xref:@ActiproUIRoot.Controls.Editors.DateEditBox) for editing `DateTime` (date-only) property values.

</td>
</tr>

<tr>
<td>

[DateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor)

</td>
<td>

Represents an property editor that uses a [DateTimeEditBox](xref:@ActiproUIRoot.Controls.Editors.DateTimeEditBox) for editing `DateTime` property values.

</td>
</tr>

<tr>
<td>

[DoublePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DoublePropertyEditor)

</td>
<td>

Represents an property editor that uses a [DoubleEditBox](xref:@ActiproUIRoot.Controls.Editors.DoubleEditBox) for editing `Double` property values.

</td>
</tr>

<tr>
<td>

[EnumPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.EnumPropertyEditor)

</td>
<td>

Represents an property editor that uses a [EnumEditBox](xref:@ActiproUIRoot.Controls.Editors.EnumEditBox) for editing `Enum` property values.

</td>
</tr>

<tr>
<td>

[GuidPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.GuidPropertyEditor)

</td>
<td>

Represents an property editor that uses a [GuidEditBox](xref:@ActiproUIRoot.Controls.Editors.GuidEditBox) for editing `Guid` property values.

</td>
</tr>

<tr>
<td>

[Int16PropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.Int16PropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int16EditBox](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox) for editing `Int16` property values.

</td>
</tr>

<tr>
<td>

[Int32PropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.Int32PropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int32EditBox](xref:@ActiproUIRoot.Controls.Editors.Int32EditBox) for editing `Int32` property values.

</td>
</tr>

@if (wpf) {
<tr>
<td>

[Int32RectPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.Int32RectPropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int32RectEditBox](xref:@ActiproUIRoot.Controls.Editors.Int32RectEditBox) for editing `Int32Rect` property values.

</td>
</tr>
}

<tr>
<td>

[Int64PropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.Int64PropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int64EditBox](xref:@ActiproUIRoot.Controls.Editors.Int64EditBox) for editing `Int64` property values.

</td>
</tr>

<tr>
<td>

[MaskedStringPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor)

</td>
<td>

Represents an property editor that uses a [MaskedTextBox](xref:@ActiproUIRoot.Controls.Editors.MaskedTextBox) for editing property values.

</td>
</tr>

@if (wpf) {
<tr>
<td>

[NullableBytePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableBytePropertyEditor)

</td>
<td>

Represents an property editor that uses a [ByteEditBox](xref:@ActiproUIRoot.Controls.Editors.ByteEditBox) for editing `Byte?` property values.

</td>
</tr>
}

<tr>
<td>

[NullableColorPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableColorPropertyEditor)

</td>
<td>

Represents an property editor that uses a [ColorEditBox](xref:@ActiproUIRoot.Controls.Editors.ColorEditBox) for editing `Color?` property values.

</td>
</tr>

<tr>
<td>

[NullableCornerRadiusPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableCornerRadiusPropertyEditor)

</td>
<td>

Represents an property editor that uses a [CornerRadiusEditBox](xref:@ActiproUIRoot.Controls.Editors.CornerRadiusEditBox) for editing `CornerRadius?` property values.

</td>
</tr>

<tr>
<td>

[NullableDatePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableDatePropertyEditor)

</td>
<td>

Represents an property editor that uses a [DateEditBox](xref:@ActiproUIRoot.Controls.Editors.DateEditBox) for editing `DateTime?` (date-only) property values.

</td>
</tr>

<tr>
<td>

[NullableDateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableDateTimePropertyEditor)

</td>
<td>

Represents an property editor that uses a [DateTimeEditBox](xref:@ActiproUIRoot.Controls.Editors.DateTimeEditBox) for editing `DateTime?` property values.

</td>
</tr>

<tr>
<td>

[NullableDoublePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableDoublePropertyEditor)

</td>
<td>

Represents an property editor that uses a [DoubleEditBox](xref:@ActiproUIRoot.Controls.Editors.DoubleEditBox) for editing `Double?` property values.

</td>
</tr>

<tr>
<td>

[NullableGuidPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableGuidPropertyEditor)

</td>
<td>

Represents an property editor that uses a [GuidEditBox](xref:@ActiproUIRoot.Controls.Editors.GuidEditBox) for editing `Guid?` property values.

</td>
</tr>

<tr>
<td>

[NullableInt16PropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt16PropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int16EditBox](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox) for editing `Int16?` property values.

</td>
</tr>

<tr>
<td>

[NullableInt32PropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt32PropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int32EditBox](xref:@ActiproUIRoot.Controls.Editors.Int32EditBox) for editing `Int32?` property values.

</td>
</tr>

@if (wpf) {
<tr>
<td>

[NullableInt32RectPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt32RectPropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int32RectEditBox](xref:@ActiproUIRoot.Controls.Editors.Int32RectEditBox) for editing `Int32Rect?` property values.

</td>
</tr>
}

<tr>
<td>

[NullableInt64PropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt64PropertyEditor)

</td>
<td>

Represents an property editor that uses a [Int64EditBox](xref:@ActiproUIRoot.Controls.Editors.Int64EditBox) for editing `Int64?` property values.

</td>
</tr>

<tr>
<td>

[NullablePointPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullablePointPropertyEditor)

</td>
<td>

Represents an property editor that uses a [PointEditBox](xref:@ActiproUIRoot.Controls.Editors.PointEditBox) for editing `Point?` property values.

</td>
</tr>

<tr>
<td>

[NullableRectPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableRectPropertyEditor)

</td>
<td>

Represents an property editor that uses a [RectEditBox](xref:@ActiproUIRoot.Controls.Editors.RectEditBox) for editing `Rect?` property values.

</td>
</tr>

<tr>
<td>

[NullableSinglePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableSinglePropertyEditor)

</td>
<td>

Represents an property editor that uses a [SingleEditBox](xref:@ActiproUIRoot.Controls.Editors.SingleEditBox) for editing `Single?` property values.

</td>
</tr>

<tr>
<td>

[NullableSizePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableSizePropertyEditor)

</td>
<td>

Represents an property editor that uses a [SizeEditBox](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox) for editing `Size?` property values.

</td>
</tr>

<tr>
<td>

[NullableThicknessPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableThicknessPropertyEditor)

</td>
<td>

Represents an property editor that uses a [ThicknessEditBox](xref:@ActiproUIRoot.Controls.Editors.ThicknessEditBox) for editing `Thickness?` property values.

</td>
</tr>

<tr>
<td>

[NullableTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableTimePropertyEditor)

</td>
<td>

Represents an property editor that uses a [TimeEditBox](xref:@ActiproUIRoot.Controls.Editors.TimeEditBox) for editing `DateTime?` (time-only) property values.

</td>
</tr>

<tr>
<td>

[NullableTimeSpanPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableTimeSpanPropertyEditor)

</td>
<td>

Represents an property editor that uses a [TimeSpanEditBox](xref:@ActiproUIRoot.Controls.Editors.TimeSpanEditBox) for editing `TimeSpan?` property values.

</td>
</tr>

@if (wpf) {
<tr>
<td>

[NullableVectorPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableVectorPropertyEditor)

</td>
<td>

Represents an property editor that uses a [VectorEditBox](xref:@ActiproUIRoot.Controls.Editors.VectorEditBox) for editing `Vector?` property values.

</td>
</tr>
}

<tr>
<td>

[PointPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.PointPropertyEditor)

</td>
<td>

Represents an property editor that uses a [PointEditBox](xref:@ActiproUIRoot.Controls.Editors.PointEditBox) for editing `Point` property values.

</td>
</tr>

<tr>
<td>

[RectPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.RectPropertyEditor)

</td>
<td>

Represents an property editor that uses a [RectEditBox](xref:@ActiproUIRoot.Controls.Editors.RectEditBox) for editing `Rect` property values.

</td>
</tr>

<tr>
<td>

[SinglePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.SinglePropertyEditor)

</td>
<td>

Represents an property editor that uses a [SingleEditBox](xref:@ActiproUIRoot.Controls.Editors.SingleEditBox) for editing `Single` property values.

</td>
</tr>

<tr>
<td>

[SizePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.SizePropertyEditor)

</td>
<td>

Represents an property editor that uses a [SizeEditBox](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox) for editing `Size` property values.

</td>
</tr>

<tr>
<td>

[ThicknessPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.ThicknessPropertyEditor)

</td>
<td>

Represents an property editor that uses a [ThicknessEditBox](xref:@ActiproUIRoot.Controls.Editors.ThicknessEditBox) for editing `Thickness` property values.

</td>
</tr>

<tr>
<td>

[TimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.TimePropertyEditor)

</td>
<td>

Represents an property editor that uses a [TimeEditBox](xref:@ActiproUIRoot.Controls.Editors.TimeEditBox) for editing `DateTime` (time-only) property values.

</td>
</tr>

<tr>
<td>

[TimeSpanPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.TimeSpanPropertyEditor)

</td>
<td>

Represents an property editor that uses a [TimeSpanEditBox](xref:@ActiproUIRoot.Controls.Editors.TimeSpanEditBox) for editing `TimeSpan` property values.

</td>
</tr>

@if (wpf) {
<tr>
<td>

[VectorPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.VectorPropertyEditor)

</td>
<td>

Represents an property editor that uses a [VectorEditBox](xref:@ActiproUIRoot.Controls.Editors.VectorEditBox) for editing `Vector` property values.

</td>
</tr>
}

</tbody>
</table>

Each property editor exposes some of the more common settings available on their associated control.  For example, the [MaskedStringPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor) has a [Mask](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor.Mask) property that allows you to set the associated [Mask](xref:@ActiproUIRoot.Controls.Editors.MaskedTextBox.Mask) on the underlying [MaskedTextBox](xref:@ActiproUIRoot.Controls.Editors.MaskedTextBox).  Remember that the controls are defined using a `DataTemplate`, so normally it is not easy to access the settings on the controls.  Using the settings available on the property editor, the underlying controls can be quickly configured.

## Adding to PropertyGrid

The property editors provided can be added to a [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) using several methods, which are described below.

### All Default Property Editors

All default Editors integration property editors can be easily injected into a single [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) instance by setting the attached [BuiltinPropertyEditors](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.BuiltinPropertyEditors).`IsEnabled` property to `true`.

> [!NOTE]
> Setting this attached property simply appends our Editors integration property editors to the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[PropertyEditors](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.PropertyEditors) collection.  You can add your own customized property editors to that collection through XAML too, and your custom property editors will be appended after our default Editors integration property editors.  This gives your custom property editors higher priority for selection over ours.

This can be done on the specific instance or via an implicit `Style` that targets all [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) instances in the app.

@if (wpf) {

This code adds the required `xmlns` definitions:

```xaml
xmlns:grids="http://schemas.actiprosoftware.com/winfx/xaml/grids"
xmlns:gridseditors="http://schemas.actiprosoftware.com/winfx/xaml/gridseditors"
```

}

This code shows how add the default Editors integration property editor definitions to a single `PropertyGrid`:

```xaml
<grids:PropertyGrid ... gridseditors:BuiltinPropertyEditors.IsEnabled="True">
	...
</grids:PropertyGrid>
```

If you wish to apply the property editors provided to all property grid instances in the application, the attached [BuiltinPropertyEditors](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.BuiltinPropertyEditors).`IsEnabled` property can be set to `true` on a [PropertyGridPropertyEditorsModifier](xref:@ActiproUIRoot.Controls.Grids.PropertyEditors.PropertyGridPropertyEditorsModifier) object that you define in `Application.Resources`.  Please see the [Property Editors](../../grids/propertygrid-features/property-editors.md) topic under Actipro PropertyGrid for more information on defining property editors in XAML using [PropertyGridPropertyEditorsModifier](xref:@ActiproUIRoot.Controls.Grids.PropertyEditors.PropertyGridPropertyEditorsModifier).

> [!NOTE]
> There are several property editors that cannot be installed via the built-in editors property described above and must be manually added to a [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[PropertyEditors](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.PropertyEditors) collection:
> 
> - [DatePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor) - Targets `DateTime` and normally the [DateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor) handles that.
> 
> - [MaskedStringPropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor) - Targets `String` and requires that a `Mask` is set.
> 
> - [NullableDatePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableDatePropertyEditor) - Targets `DateTime?` and normally the [NullableDateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableDateTimePropertyEditor) handles that.
> 
> - [NullableTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableTimePropertyEditor) - Targets `DateTime?` and normally the [NullableDateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.NullableDateTimePropertyEditor) handles that.
> 
> - [TimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.TimePropertyEditor) - Targets `DateTime` and normally the [DateTimePropertyEditor](xref:@ActiproUIRoot.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor) handles that.

### Specific Property Editors via PropertyGrid.PropertyEditors

Property editors can also be manually defined on a single instance of the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) control by updating the [PropertyEditors](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.PropertyEditors) collection.

Several property editors (described above) require this since they can't be automatically set up by the built-in editors property.  Note that you can apply the built-in editor and manually-defined editors on the same property grid.

This code shows how manually add several property editor definitions to a single `PropertyGrid`:

```xaml
<grids:PropertyGrid ... >
	<grids:PropertyGrid.PropertyEditors>
		<gridseditors:DatePropertyEditor PropertyName="DateLongValue" Format="D" />
		<gridseditors:MaskedStringPropertyEditor PropertyName="MaskedString" Mask="\w+([.]\w+)*@\w+([.]\w+)+" />
		<gridseditors:EnumPropertyEditor UseDisplayAttributes="True" />
	</grids:PropertyGrid.PropertyEditors>
</grids:PropertyGrid>
```

### Using EditorAttribute

A property editor can also be associated right on the data objects displayed by [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) using the `EditorAttribute`.

This code shows how associated a `ColorEditBox` property editor with a property:

```csharp
[Editor(typeof(ColorPropertyEditor), typeof(PropertyEditor))]
public Color MyColor {
	...
}
```

Since `EditorAttribute` only allows a `Type` to be specified, if you need to alter any default property values, create a class that inherits the desired property editor `Type` and set the property values in the class' constructor.
