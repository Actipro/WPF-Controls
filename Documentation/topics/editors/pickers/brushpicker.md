---
title: "BrushPicker"
page-title: "BrushPicker - Editors Pickers"
order: 2
---
# BrushPicker

The [BrushPicker](xref:@ActiproUIRoot.Controls.Editors.BrushPicker) control makes it easy for end users to select a `Brush` via touch or a mouse.  It is generally intended for display within a popup, such as for the [BrushEditBox](../editboxes/brusheditbox.md) control.

![Screenshot](../images/brushpicker.png)

It combines a horizontal list for picking the brush kind, along with [ColorPicker](colorpicker.md) and [GradientStopSlider](../other-controls/gradientstopslider.md) controls for modifying the brush value.

Alpha transparency and gradients are also optionally supported.

## Alpha Transparency

The [BrushPicker](xref:@ActiproUIRoot.Controls.Editors.BrushPicker).[IsAlphaEnabled](xref:@ActiproUIRoot.Controls.Editors.BrushPicker.IsAlphaEnabled) property governs whether alpha transparency is supported.

When `false`, the edit box will only allow selection of RGB colors instead of ARGB colors.

## Gradient Support

The [BrushPicker](xref:@ActiproUIRoot.Controls.Editors.BrushPicker).[IsGradientAllowed](xref:@ActiproUIRoot.Controls.Editors.BrushPicker.IsGradientAllowed) property can be set to `false` to prevent gradient brushes from being supported.

When `false`, the picker will hide all brush kind options related to gradients.

## Null Support

The [BrushPicker](xref:@ActiproUIRoot.Controls.Editors.BrushPicker).[IsNullAllowed](xref:@ActiproUIRoot.Controls.Editors.BrushPicker.IsNullAllowed) property can be set to `false` to always require that a valid `Brush` value is present, and that `null` is not supported.

When `false`, the picker will hide the null brush kind option.

## Reusing Brushes

By default, the brush instance will generally be reused when a component like gradient stop is updated.  But this prevents the [ValueChanged](xref:@ActiproUIRoot.Controls.Editors.BrushPicker.ValueChanged) event from firing on certain component changes, and also bindings to the [Value](xref:@ActiproUIRoot.Controls.Editors.BrushPicker.Value) property from updating when using value converters.  The [CanReuseBrush](xref:@ActiproUIRoot.Controls.Editors.BrushPicker.CanReuseBrush) property can be set to `false` to force a new brush to be created any time a component is updated, which works around those issues.

## Sample XAML

This control can be placed within any other XAML container control, such as a `Page` or `Panel` with this sort of XAML:

```xaml
<editors:BrushPicker Value="{Binding Path=YourVMProperty, Mode=TwoWay}" />
```
