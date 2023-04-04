---
title: "SizeEditBox"
page-title: "SizeEditBox - Editors Edit Boxes"
order: 26
---
# SizeEditBox

The [SizeEditBox](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox) control allows for the input of a `Size` (width, height) value.  It uses the [SizePicker](../pickers/sizepicker.md) control in its popup.

![Screenshot](../images/sizeeditbox-opened.png)

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
<td>Yes, and supports optional arrow key navigation.</td>
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

[Standard .NET numeric formats](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings) are supported via the [Format](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.Format) property and affect the textual value display.  These formats are recommended:

- `"F"`
- `"Fx"`, where x is the number of decimal places (e.g., `"F1"`)
- `"G"`

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.Maximum) and [Minimum](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Parts and Incrementing/Decrementing

This edit box has multiple parts:

- Width
- Height

When the caret is over a part, the part value may be incremented or decremented.  Please see the [Edit Box Basics](parteditboxbase.md) topic for information on how to do this.

Small value changes alter the current number component by `1`, which is the default for the [SmallChange](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.SmallChange) property.  Large value changes alter the current number component by `5`, which is the default for the [LargeChange](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.LargeChange) property.

The [DefaultValue](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.DefaultValue) property sets the value that will be set when incrementing or decrementing from a `null` value.

## Rounding Decimal Places

The [RoundingDecimalPlace](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.RoundingDecimalPlace) property determines the maximum decimal place at which to round floating-point numbers.  It defaults to `8` but can be set to any value in the range `0` to `15`.  Or set the value to `null` to prevent rounding.

## Allowing NaN or Infinity Values

Text entry of `NaN` (not-a-number) and infinity component values into the edit box is not allowed by default.

Set the [IsNaNAllowed](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.IsNaNAllowed) property to `true` to allow a `NaN` value to be entered by typing the letter <kbd>N</kbd>.

@if (wpf) {

Set the [IsNegativeInfinityAllowed](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.IsNegativeInfinityAllowed) property to `true` to allow a negative infinity value to be entered by typing a negative sign <kbd>-</kbd> and then the letter <kbd>I</kbd>.

}

Set the [IsPositiveInfinityAllowed](xref:@ActiproUIRoot.Controls.Editors.SizeEditBox.IsPositiveInfinityAllowed) property to `true` to allow a positive infinity value to be entered by typing the letter <kbd>I</kbd>.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:SizeEditBox Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
