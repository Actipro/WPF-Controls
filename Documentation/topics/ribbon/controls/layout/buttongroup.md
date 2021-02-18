---
title: "ButtonGroup"
page-title: "ButtonGroup - Ribbon Controls"
order: 2
---
# ButtonGroup

The [ButtonGroup](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.ButtonGroup) class provides a container for buttons or other controls and is most often used within a [RowPanel](rowpanel.md) or `StatusBarItem`.

## Variants

This control supports numerous UI styles (called variants) based on its [Context](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.Context) and [VariantSize](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ControlBase.VariantSize) property settings.

| Context | Variant Size | Sample UI |
|-----|-----|-----|
| StatusBarItem | (any) | ![Screenshot](../../images/buttongroup-status-bar-item-medium.gif) |
| (any other) | (any) | ![Screenshot](../../images/buttongroup-medium.gif) |

## Hiding the Border

By default, a border is drawn around the items in the button group.  Sometimes when the group's items are controls like a [ComboBox](../interactive/combobox.md), you don't want to display the border.

Set the [HasBorder](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.ButtonGroup.HasBorder) property to `false` to hide it.
