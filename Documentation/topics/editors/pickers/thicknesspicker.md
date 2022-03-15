---
title: "ThicknessPicker"
page-title: "ThicknessPicker - Editors Pickers"
order: 27
---
# ThicknessPicker

The [ThicknessPicker](xref:@ActiproUIRoot.Controls.Editors.ThicknessPicker) control allows for the input of a `Thickness` (left, top, right, bottom) value.  It is generally intended for display within a popup, such as for the [ThicknessEditBox](../editboxes/thicknesseditbox.md) control.

![Screenshot](../images/thicknesspicker.png)

The toggle at the top determines whether the embedded [DoublePicker](doublepicker.md) is currently modifying the `Left`, `Top`, `Right` or `Bottom` component of the `Thickness`.  See the [DoublePicker](doublepicker.md) for a description of how its UI functions.

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:@ActiproUIRoot.Controls.Editors.ThicknessPicker.Maximum) and [Minimum](xref:@ActiproUIRoot.Controls.Editors.ThicknessPicker.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Incrementing/Decrementing

Value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:@ActiproUIRoot.Controls.Editors.ThicknessPicker.SmallChange) property.

## Rounding Decimal Places

The [RoundingDecimalPlace](xref:@ActiproUIRoot.Controls.Editors.ThicknessPicker.RoundingDecimalPlace) property determines the maximum decimal place at which to round floating-point numbers.  It defaults to `8`, but can be set to any value in the range `0` to `15`.  Or set the value to `null` to prevent rounding.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:ThicknessPicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
