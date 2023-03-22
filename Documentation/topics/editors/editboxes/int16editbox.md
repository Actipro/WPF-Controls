---
title: "Int16EditBox"
page-title: "Int16EditBox - Editors Edit Boxes"
order: 16
---
# Int16EditBox

The [Int16EditBox](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox) control allows for the input of an `Int16` (16-bit integer) value.  It uses the [Int16Picker](../pickers/int16picker.md) control in its popup.

![Screenshot](../images/int32editbox-opened.png)

## Common Capabilities

Each of the features listed in the table below describe functionality that is common to most edit boxes.  Please see the [Edit Box Basics](parteditboxbase.md) topic for details on each of these options and how to set them.

<table>
<thead>

<tr>
<th>Feature</th>
<th>Description</th>
</tr>

</thead>
<tbody>

@if (winrt) {
<tr>
<td>Has a clear button</td>
<td>Yes, and can be hidden.</td>
</tr>
}

@if (wpf) {
<tr>
<td>Has a spinner</td>
<td>Yes, and can be hidden or optionally displayed only when the control is active.</td>
</tr>
}

<tr>
<td>Has a popup</td>
<td>Yes, and can be hidden or its picker appearance customized.</td>
</tr>

<tr>
<td>Null value allowed</td>
<td>Yes, and can be prevented.</td>
</tr>

<tr>
<td>Read-only mode supported</td>
<td>Yes.</td>
</tr>

<tr>
<td>Non-editable mode supported</td>
<td>Yes.</td>
</tr>

<tr>
<td>Has multiple parts</td>
<td>No.</td>
</tr>

<tr>
<td>Placeholder text supported</td>
<td>Yes, and overlays the control.</td>
</tr>

<tr>
<td>Header content supported</td>
<td>Yes, and appears above the control.</td>
</tr>

<tr>
<td>Default spin behavior</td>
<td>No wrap.</td>
</tr>

</tbody>
</table>

## Number Formats

[Standard .NET numeric formats](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) are supported via the [Format](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.Format) property and affect the textual value display.  These formats are recommended:

- C0 (currency without decimals)
- D
- Dx, where x is the number of zero-padded digits (e.g., D3)
- G
- N0
- X (uppercase hexadecimal)
- x (lowercase hexadecimal)

Basic custom numeric formats are also supported, such as:

- 0' days'

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.Maximum) and [Minimum](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Parts and Incrementing/Decrementing

This edit box has a single part.

When the caret is over a part, the part value may be incremented or decremented.  Please see the [Edit Box Basics](parteditboxbase.md) topic for information on how to do this.

Small value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.SmallChange) property.  Large value changes alter the current number component by `5`, which is the default for the [LargeChange](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.LargeChange) property.

The [DefaultValue](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.DefaultValue) property sets the value that will be set when incrementing or decrementing from a null value.

## Built-in Picker Kinds

This edit box has multiple built-in picker kinds that can be set via the [Int16EditBox](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox).[PickerKind](xref:@ActiproUIRoot.Controls.Editors.Int16EditBox.PickerKind) property, which is of type [Int16EditBoxPickerKind](xref:@ActiproUIRoot.Controls.Editors.Int16EditBoxPickerKind).

@if (wpf) {

The default value in WPF is `Calculator`. 

}

@if (winrt) {

The default value in UWP is `Default`. 

}

The `Default` picker kind renders using a radial slider, while the `Calculator` picker kind utilizes the [Calculator](../other-controls/calculator.md) control.

![Screenshot](../images/int32editbox-opened-calculator.png)

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:Int16EditBox Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
