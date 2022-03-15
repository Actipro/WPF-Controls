---
title: "TimeSpanPicker"
page-title: "TimeSpanPicker - Editors Pickers"
order: 30
---
# TimeSpanPicker

The [TimeSpanPicker](xref:@ActiproUIRoot.Controls.Editors.TimeSpanPicker) control allows for the input of a `TimeSpan` (days, hours, minutes, seconds, milliseconds) value.  It is generally intended for display within a popup, such as for the [TimeSpanEditBox](../editboxes/timespaneditbox.md) control.

![Screenshot](../images/timespanpicker.png)

The toggle at the top determines whether the embedded [Int32Picker](int32picker.md) is currently modifying the `Days`, `Hours`, `Minutes`, `Seconds` or `Milliseconds` component of the `TimeSpan`.  See the [Int32Picker](int32picker.md) for a description of how its UI functions.

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:@ActiproUIRoot.Controls.Editors.TimeSpanPicker.Maximum) and [Minimum](xref:@ActiproUIRoot.Controls.Editors.TimeSpanPicker.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Incrementing/Decrementing

Value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:@ActiproUIRoot.Controls.Editors.TimeSpanPicker.SmallChange) property.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:TimeSpanPicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
