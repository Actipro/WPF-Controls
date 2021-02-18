---
title: "GradientStopSlider"
page-title: "GradientStopSlider - Other Editors Controls"
order: 12
---
# GradientStopSlider

The [GradientStopSlider](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider) control allows for adding, changing, and removing the various gradient stops within a `GradientBrush`, such as a `LinearGradientBrush`.

![Screenshot](../images/gradientstopslider.png)

## Usage

The slider will display a thumb for each `GradientStop` in the gradient brush.  Each stop consists of an offset and `Color` pair.

Thumbs can be dragged to adjust their offsets and the color for the selected thumb can be changed.  When dragging a thumb far above or below the slider, it will turn semi-transparent meaning that it will be removed if it is released at that location.  Double click an empty area of the slider track to insert a new thumb.

The [GradientStopSlider](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider).[ActiveBrush](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.ActiveBrush) is the property that you apply a two-way binding to whichever `Brush`-based property that you wish to adjust using the slider.

You also need to add a two-way binding to the [SelectedColor](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.SelectedColor) property that binds to an external control for altering the color of the selected stop.  Any one of the coloring picking controls in this product can be used for this purpose.

## Stop Add/Remove Capabilities

The [CanAddStops](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.CanAddStops) and [CanRemoveStops](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.CanRemoveStops) properties can be used to restrict whether stops can be added to or removed from the gradient brush.  They are both `true` by default.

## Track Height

The [TrackHeight](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.TrackHeight) property sets the height of the track, which is the area in which the actual gradient brush is rendered.

## Commands for External Buttons

There are several properties that return `ICommand` and can be utilized by external buttons or menu items:

- [AddStopCommand](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.AddStopCommand)

- [RemoveSelectedStopCommand](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.RemoveSelectedStopCommand)

- [ReverseStopsCommand](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.ReverseStopsCommand)

Simply bind the `Command` property of an external button or menu item to the appropriate slider property to have them interact with the control.

## Reusing Brushes

By default, the brush instance will generally be reused when a gradient stop is added/updated/removed.  But this prevents bindings to the [ActiveBrush](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.ActiveBrush) property from updating when using value converters.  The [CanReuseBrush](xref:ActiproSoftware.Windows.Controls.Editors.GradientStopSlider.CanReuseBrush) property can be set to `false` to force a new brush to be created any time a gradient stop is added/updated/removed, which works around those issues.
