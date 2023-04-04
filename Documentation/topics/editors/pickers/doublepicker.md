---
title: "DoublePicker"
page-title: "DoublePicker - Editors Pickers"
order: 11
---
# DoublePicker

The [DoublePicker](xref:@ActiproUIRoot.Controls.Editors.DoublePicker) control allows for the input of a `Double` (floating-point number) value.  It is generally intended for display within a popup, such as for the [DoubleEditBox](../editboxes/doubleeditbox.md) control, and is also used within many other [pickers](index.md).

![Screenshot](../images/doublepicker.png)

The control allows for selection of a `Double` value via the use of a radial slider and increment/decrement button combination.  The radial slider facilitates large changes to the value, while the increment/decrement buttons enable fine tuning.

The picker adjusts its radial slider functionality according to the range of specified minimum and maximum values.  Large ranges permit multiple slider rotation cycles and show an overall progress indicator towards the largest value.  Smaller ranges only allow for a single rotation cycle.

Negative values have a distinct appearance from positive values.

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:@ActiproUIRoot.Controls.Editors.DoublePicker.Maximum) and [Minimum](xref:@ActiproUIRoot.Controls.Editors.DoublePicker.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Incrementing/Decrementing

Value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:@ActiproUIRoot.Controls.Editors.DoublePicker.SmallChange) property.

## Rounding Decimal Places

The [RoundingDecimalPlace](xref:@ActiproUIRoot.Controls.Editors.DoublePicker.RoundingDecimalPlace) property determines the maximum decimal place at which to round floating-point numbers.  It defaults to `8` but can be set to any value in the range `0` to `15`.  Or set the value to `null` to prevent rounding.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:DoublePicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
