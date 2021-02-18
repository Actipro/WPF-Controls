---
title: "PropertyGrid"
page-title: "PropertyGrid - Editors Interoperability"
order: 2
---
# PropertyGrid

The controls provided in the Editors product can be easily integrated into the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) control, through the use of custom property editors.  In order to keep both products segregated, an Interop assembly is provided that ties the two products together.

![Screenshot](../images/propertygrid-brusheditbox-open.png)

*PropertyGrid with BrushEditBox integrated*

## Interop Assembly

The Interop functionality is provided through a separate assembly, which is called `ActiproSoftware.Editors.Interop.Grids.Wpf.dll`.  Therefore, a reference to this assembly must be added in order to leverage the property editors it provides.  This assembly should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## Property Editors

The [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) uses "property editors" to tie a properties to controls, by property name and/or property type.  For example, you can tell the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) that all properties of the type `DateTime` should use `MyControlA` and all properties of the type `String` should use `MyControlB`.  This gives you a lot of control over what control is used for the various properties.  In reality, the "control" is actually a `DataTemplate` since it will most likely be used multiple times.

Property editors are defined via an instance of the [PropertyEditor](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyEditor) class.  Derivations of this class can provide default values, such as a known `DataTemplate` or property `Type`.  In addition, derivations are required in order to decorate properties with `EditorAttribute`. `EditorAttribute` only allows the editor `Type` to be specified, therefore that `Type` needs to provide a default `DataTemplate` (via a derivation of [PropertyEditor](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyEditor)).

> [!NOTE]
> Property editors can actually provide a `DataTemplate` for the value cell and the name cell, but typically the name cell does not need to be altered.

See the [Property Editors](../../grids/propertygrid-features/property-editors.md) topic under Actipro PropertyGrid for more information.

The Interop assembly provides a set of classes that derive from [PropertyEditor](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyEditor) and provide default values for the `DataTemplate` used for the value cell and the property `Type`.  These classes include:

| Class | Description |
|-----|-----|
| [BrushPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.BrushPropertyEditor) | Represents an property editor that uses a [BrushEditBox](xref:ActiproSoftware.Windows.Controls.Editors.BrushEditBox) for editing `Brush` property values. |
| [BytePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.BytePropertyEditor) | Represents an property editor that uses a [ByteEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ByteEditBox) for editing `Byte` property values. |
| [ColorPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.ColorPropertyEditor) | Represents an property editor that uses a [ColorEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ColorEditBox) for editing `Color` property values. |
| [CornerRadiusPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.CornerRadiusPropertyEditor) | Represents an property editor that uses a [CornerRadiusEditBox](xref:ActiproSoftware.Windows.Controls.Editors.CornerRadiusEditBox) for editing `CornerRadius` property values. |
| [DatePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor) | Represents an property editor that uses a [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) for editing `DateTime` (date-only) property values. |
| [DateTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor) | Represents an property editor that uses a [DateTimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox) for editing `DateTime` property values. |
| [DoublePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DoublePropertyEditor) | Represents an property editor that uses a [DoubleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox) for editing `Double` property values. |
| [EnumPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.EnumPropertyEditor) | Represents an property editor that uses a [EnumEditBox](xref:ActiproSoftware.Windows.Controls.Editors.EnumEditBox) for editing `Enum` property values. |
| [GuidPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.GuidPropertyEditor) | Represents an property editor that uses a [GuidEditBox](xref:ActiproSoftware.Windows.Controls.Editors.GuidEditBox) for editing `Guid` property values. |
| [Int16PropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.Int16PropertyEditor) | Represents an property editor that uses a [Int16EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int16EditBox) for editing `Int16` property values. |
| [Int32PropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.Int32PropertyEditor) | Represents an property editor that uses a [Int32EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int32EditBox) for editing `Int32` property values. |
| [Int32RectPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.Int32RectPropertyEditor) | Represents an property editor that uses a [Int32RectEditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int32RectEditBox) for editing `Int32Rect` property values. |
| [Int64PropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.Int64PropertyEditor) | Represents an property editor that uses a [Int64EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int64EditBox) for editing `Int64` property values. |
| [MaskedStringPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor) | Represents an property editor that uses a [MaskedTextBox](xref:ActiproSoftware.Windows.Controls.Editors.MaskedTextBox) for editing property values. |
| [NullableBytePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableBytePropertyEditor) | Represents an property editor that uses a [ByteEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ByteEditBox) for editing `Byte?` property values. |
| [NullableColorPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableColorPropertyEditor) | Represents an property editor that uses a [ColorEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ColorEditBox) for editing `Color?` property values. |
| [NullableCornerRadiusPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableCornerRadiusPropertyEditor) | Represents an property editor that uses a [CornerRadiusEditBox](xref:ActiproSoftware.Windows.Controls.Editors.CornerRadiusEditBox) for editing `CornerRadius?` property values. |
| [NullableDatePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableDatePropertyEditor) | Represents an property editor that uses a [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) for editing `DateTime?` (date-only) property values. |
| [NullableDateTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableDateTimePropertyEditor) | Represents an property editor that uses a [DateTimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox) for editing `DateTime?` property values. |
| [NullableDoublePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableDoublePropertyEditor) | Represents an property editor that uses a [DoubleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DoubleEditBox) for editing `Double?` property values. |
| [NullableGuidPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableGuidPropertyEditor) | Represents an property editor that uses a [GuidEditBox](xref:ActiproSoftware.Windows.Controls.Editors.GuidEditBox) for editing `Guid?` property values. |
| [NullableInt16PropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt16PropertyEditor) | Represents an property editor that uses a [Int16EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int16EditBox) for editing `Int16?` property values. |
| [NullableInt32PropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt32PropertyEditor) | Represents an property editor that uses a [Int32EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int32EditBox) for editing `Int32?` property values. |
| [NullableInt32RectPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt32RectPropertyEditor) | Represents an property editor that uses a [Int32RectEditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int32RectEditBox) for editing `Int32Rect?` property values. |
| [NullableInt64PropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableInt64PropertyEditor) | Represents an property editor that uses a [Int64EditBox](xref:ActiproSoftware.Windows.Controls.Editors.Int64EditBox) for editing `Int64?` property values. |
| [NullablePointPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullablePointPropertyEditor) | Represents an property editor that uses a [PointEditBox](xref:ActiproSoftware.Windows.Controls.Editors.PointEditBox) for editing `Point?` property values. |
| [NullableRectPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableRectPropertyEditor) | Represents an property editor that uses a [RectEditBox](xref:ActiproSoftware.Windows.Controls.Editors.RectEditBox) for editing `Rect?` property values. |
| [NullableSinglePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableSinglePropertyEditor) | Represents an property editor that uses a [SingleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.SingleEditBox) for editing `Single?` property values. |
| [NullableSizePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableSizePropertyEditor) | Represents an property editor that uses a [SizeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.SizeEditBox) for editing `Size?` property values. |
| [NullableThicknessPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableThicknessPropertyEditor) | Represents an property editor that uses a [ThicknessEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ThicknessEditBox) for editing `Thickness?` property values. |
| [NullableTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableTimePropertyEditor) | Represents an property editor that uses a [TimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeEditBox) for editing `DateTime?` (time-only) property values. |
| [NullableTimeSpanPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableTimeSpanPropertyEditor) | Represents an property editor that uses a [TimeSpanEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeSpanEditBox) for editing `TimeSpan?` property values. |
| [NullableVectorPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableVectorPropertyEditor) | Represents an property editor that uses a [VectorEditBox](xref:ActiproSoftware.Windows.Controls.Editors.VectorEditBox) for editing `Vector?` property values. |
| [PointPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.PointPropertyEditor) | Represents an property editor that uses a [PointEditBox](xref:ActiproSoftware.Windows.Controls.Editors.PointEditBox) for editing `Point` property values. |
| [RectPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.RectPropertyEditor) | Represents an property editor that uses a [RectEditBox](xref:ActiproSoftware.Windows.Controls.Editors.RectEditBox) for editing `Rect` property values. |
| [SinglePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.SinglePropertyEditor) | Represents an property editor that uses a [SingleEditBox](xref:ActiproSoftware.Windows.Controls.Editors.SingleEditBox) for editing `Single` property values. |
| [SizePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.SizePropertyEditor) | Represents an property editor that uses a [SizeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.SizeEditBox) for editing `Size` property values. |
| [ThicknessPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.ThicknessPropertyEditor) | Represents an property editor that uses a [ThicknessEditBox](xref:ActiproSoftware.Windows.Controls.Editors.ThicknessEditBox) for editing `Thickness` property values. |
| [TimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.TimePropertyEditor) | Represents an property editor that uses a [TimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeEditBox) for editing `DateTime` (time-only) property values. |
| [TimeSpanPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.TimeSpanPropertyEditor) | Represents an property editor that uses a [TimeSpanEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeSpanEditBox) for editing `TimeSpan` property values. |
| [VectorPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.VectorPropertyEditor) | Represents an property editor that uses a [VectorEditBox](xref:ActiproSoftware.Windows.Controls.Editors.VectorEditBox) for editing `Vector` property values. |

Each property editor exposes some of the more common settings available on their associated control.  For example, the [MaskedStringPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor) has a [Mask](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor.Mask) property that allows you to set the associated [Mask](xref:ActiproSoftware.Windows.Controls.Editors.MaskedTextBox.Mask) on the underlying [MaskedTextBox](xref:ActiproSoftware.Windows.Controls.Editors.MaskedTextBox).  Remember that the controls are defined using a `DataTemplate`, so normally it is not easy to access the settings on the controls.  Using the settings available on the property editor, the underlying controls can be quickly configured.

## Adding to PropertyGrid

The property editors provided can be added to a [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) using several methods, which are described below.

### All Default Property Editors

All default Editors integration property editors can be easily injected into a single [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) instance by setting the attached [BuiltinPropertyEditors](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.BuiltinPropertyEditors).`IsEnabled` property to `true`.

> [!NOTE]
> Setting this attached property simply appends our Editors integration property editors to the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[PropertyEditors](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyEditors) collection.  You can add your own customized property editors to that collection through XAML too, and your custom property editors will be appended after our default Editors integration property editors.  This gives your custom property editors higher priority for selection over ours.

This can be done on the specific instance or via an implicit `Style` that targets all [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) instances in the app.

This code adds the required `xmlns` definitions:

```xaml
xmlns:grids="http://schemas.actiprosoftware.com/winfx/xaml/grids"
xmlns:gridseditors="http://schemas.actiprosoftware.com/winfx/xaml/gridseditors"
```

This code shows how add the default Editors integration property editor definitions to a single `PropertyGrid`:

```xaml
<grids:PropertyGrid ... gridseditors:BuiltinPropertyEditors.IsEnabled="True">
	...
</grids:PropertyGrid>
```

If you wish to apply the property editors provided to all property grid instances in the application, the attached [BuiltinPropertyEditors](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.BuiltinPropertyEditors).`IsEnabled` property can be set to `true` on a [PropertyGridPropertyEditorsModifier](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyGridPropertyEditorsModifier) object that you define in `Application.Resources`.  Please see the [Property Editors](../../grids/propertygrid-features/property-editors.md) topic under Actipro PropertyGrid for more information on defining property editors in XAML using [PropertyGridPropertyEditorsModifier](xref:ActiproSoftware.Windows.Controls.Grids.PropertyEditors.PropertyGridPropertyEditorsModifier).

> [!NOTE]
> There are several property editors that cannot be installed via the built-in editors property described above and must be manually added to a [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid).[PropertyEditors](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyEditors) collection:
> 
> - [DatePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DatePropertyEditor) - Targets `DateTime` and normally the [DateTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor) handles that.
> 
> - [MaskedStringPropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.MaskedStringPropertyEditor) - Targets `String` and requires that a `Mask` is set.
> 
> - [NullableDatePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableDatePropertyEditor) - Targets `DateTime?` and normally the [NullableDateTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableDateTimePropertyEditor) handles that.
> 
> - [NullableTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableTimePropertyEditor) - Targets `DateTime?` and normally the [NullableDateTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.NullableDateTimePropertyEditor) handles that.
> 
> - [TimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.TimePropertyEditor) - Targets `DateTime` and normally the [DateTimePropertyEditor](xref:ActiproSoftware.Windows.Controls.Editors.Interop.Grids.PropertyEditors.DateTimePropertyEditor) handles that.

### Specific Property Editors via PropertyGrid.PropertyEditors

Property editors can also be manually defined on a single instance of the [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) control by updating the [PropertyEditors](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid.PropertyEditors) collection.

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

A property editor can also be associated right on the data objects displayed by [PropertyGrid](xref:ActiproSoftware.Windows.Controls.Grids.PropertyGrid) using the `EditorAttribute`.

This code shows how associated a `ColorEditBox` property editor with a property:

```csharp
[Editor(typeof(ColorPropertyEditor), typeof(PropertyEditor))]
public Color MyColor {
	...
}
```

Since `EditorAttribute` only allows a `Type` to be specified, if you need to alter any default property values, create a class that inherits the desired property editor `Type` and set the property values in the class' constructor.
