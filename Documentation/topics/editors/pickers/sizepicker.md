---
title: "SizePicker"
page-title: "SizePicker - Editors Pickers"
order: 26
---
# SizePicker

The [SizePicker](xref:ActiproSoftware.Windows.Controls.Editors.SizePicker) control allows for the input of a `Size` (width, height) value.  It is generally intended for display within a popup, such as for the [SizeEditBox](../editboxes/sizeeditbox.md) control.

![Screenshot](../images/sizepicker.png)

The toggle at the top determines whether the embedded [DoublePicker](doublepicker.md) is currently modifying the `Width` or `Height` component of the `Size`.  See the [DoublePicker](doublepicker.md) for a description of how its UI functions.

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:ActiproSoftware.Windows.Controls.Editors.SizePicker.Maximum) and [Minimum](xref:ActiproSoftware.Windows.Controls.Editors.SizePicker.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Incrementing/Decrementing

Value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:ActiproSoftware.Windows.Controls.Editors.SizePicker.SmallChange) property.

## Rounding Decimal Places

The [RoundingDecimalPlace](xref:ActiproSoftware.Windows.Controls.Editors.SizePicker.RoundingDecimalPlace) property determines the maximum decimal place at which to round floating-point numbers.  It defaults to `8`, but can be set to any value in the range `0` to `15`.  Or set the value to `null` to prevent rounding.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:SizePicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
