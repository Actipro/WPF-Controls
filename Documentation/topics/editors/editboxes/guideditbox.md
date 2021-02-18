---
title: "GuidEditBox"
page-title: "GuidEditBox - Editors Edit Boxes"
order: 14
---
# GuidEditBox

The [GuidEditBox](xref:ActiproSoftware.Windows.Controls.Editors.GuidEditBox) control allows for the input of a `Guid` (unique ID) value.

![Screenshot](../images/guideditbox.png)

## Common Capabilities

Each of the features listed in the table below describe functionality that is common to most edit boxes.  Please see the [Edit Box Basics](parteditboxbase.md) topic for details on each of these options and how to set them.

| Feature | Description |
|-----|-----|
| Has a spinner | No. |
| Has a popup | No. |
| Null value allowed | Yes, and can be prevented. |
| Read-only mode supported | Yes. |
| Non-editable mode supported | Yes. |
| Has multiple parts | No. |
| Placeholder text supported | Yes, and overlays the control. |
| Header content supported | Yes, and appears above the control. |
| Default spin behavior | No wrap. |

## Generating a New GUID

The plus button in the edit box can be used to generate a new GUID value.

## Formats

Standard GUID formats are supported via the [Format](xref:ActiproSoftware.Windows.Controls.Editors.GuidEditBox.Format) property and affect the textual value display.  These formats are allowed:

- N - 32 digits (uppercase)
- n - 32 digits (lowercase)
- D - 32 digits separated by hyphens (uppercase)
- d - 32 digits separated by hyphens (lowercase)
- B - 32 digits separated by hyphens, enclosed in braces (uppercase)
- b - 32 digits separated by hyphens, enclosed in braces (lowercase)
- P - 32 digits separated by hyphens, enclosed in parentheses (uppercase)
- p - 32 digits separated by hyphens, enclosed in parentheses (lowercase)

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:GuidEditBox Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
