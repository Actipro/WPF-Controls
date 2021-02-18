---
title: "Int32RectPicker"
page-title: "Int32RectPicker - Editors Pickers"
order: 19
---
# Int32RectPicker

The [Int32RectPicker](xref:ActiproSoftware.Windows.Controls.Editors.Int32RectPicker) control allows for the input of an `Int32Rect` (X, Y, width, height) value.  It is generally intended for display within a popup, such as for the [Int32RectEditBox](../editboxes/int32recteditbox.md) control.

![Screenshot](../images/rectpicker.png)

The toggle at the top determines whether the embedded [Int32Picker](int32picker.md) is currently modifying the `X`, `Y`, `Width` or `Height` component of the `Int32Rect`.  See the [Int32Picker](int32picker.md) for a description of how its UI functions.

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:ActiproSoftware.Windows.Controls.Editors.Int32RectPicker.Maximum) and [Minimum](xref:ActiproSoftware.Windows.Controls.Editors.Int32RectPicker.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Incrementing/Decrementing

Value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:ActiproSoftware.Windows.Controls.Editors.Int32RectPicker.SmallChange) property.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:Int32RectPicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
