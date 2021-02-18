---
title: "DateEditBox"
page-title: "DateEditBox - Editors Edit Boxes"
order: 8
---
# DateEditBox

The [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) control allows for the input of a `DateTime` value's date component.  It uses the [DatePicker](../pickers/datepicker.md) control in its popup.

![Screenshot](../images/dateeditbox-opened.png)

## Common Capabilities

Each of the features listed in the table below describe functionality that is common to most edit boxes.  Please see the [Edit Box Basics](parteditboxbase.md) topic for details on each of these options and how to set them.

| Feature | Description |
|-----|-----|
| Has a spinner | Yes, and can be hidden or optionally displayed only when the control is active. |
| Has a popup | Yes, and can be hidden or its picker appearance customized. |
| Null value allowed | Yes, and can be prevented. |
| Read-only mode supported | Yes. |
| Non-editable mode supported | Yes. |
| Has multiple parts | Yes, and supports optional arrow key navigation. |
| Placeholder text supported | Yes, and overlays the control. |
| Header content supported | Yes, and appears above the control. |
| Default spin behavior | Wrap. |

## Time Retention

When using separate [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) and [TimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeEditBox) controls that bind to the same `DateTime` object, it's sometimes helpful to have the [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) retain the time portion as selections are made, instead of reverting the time to midnight.  The [CanRetainTime](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox.CanRetainTime) property can be set to `true` to enable time retention.

Note that a [DateTimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox) can be used in place of separate [DateEditBox](xref:ActiproSoftware.Windows.Controls.Editors.DateEditBox) and [TimeEditBox](xref:ActiproSoftware.Windows.Controls.Editors.TimeEditBox) controls if dates and times require input.

## Formats

Standard date formats are supported via the [Format](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox.Format) property and affect the textual value display.  These formats are recommended:

- d
- D
- m
- y
- MM/dd/yyyy
- MM/dd/yy
- yyyy-MM-dd
- d MMMM yyyy
- d MMM yyyy
- dd.MM.yyyy
- d.M.yyyy

## Minimum and Maximum Values

Minimum and maximum values may be assigned via the [Maximum](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox.Maximum) and [Minimum](xref:ActiproSoftware.Windows.Controls.Editors.DateTimeEditBox.Minimum) properties.

No values can be committed that lay outside of the inclusive range created by those properties.

## Parts and Incrementing/Decrementing

This edit box has multiple parts:

- Year
- Month
- Day
- Hour
- Minute
- Second
- AM/PM

When the caret is over a part, the part value may be incremented or decremented.  Please see the [Edit Box Basics](parteditboxbase.md) topic for information on how to do this.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:DateEditBox Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
