---
title: "Overview"
page-title: "Shared Library Controls"
order: 1
---
# Overview

The [ActiproSoftware.Windows.Controls](xref:ActiproSoftware.Windows.Controls) namespace contains various user interface controls that may be used in your applications.

## The AdvancedTextBlock Control

The [AdvancedTextBlock](advancedtextblock.md) control can show a tooltip when overflowed, and can highlight spans of text based on captured text ranges (i.e. filter match results).

## The AnimatedExpander Control

The [AnimatedExpander](animatedexpander.md) control is a regular `Expander` that provides optional animated expansion functionality.  The animation consists of crossfade and slide behavior.

## The AnimatedProgressBar Control

The [AnimatedProgressBar](animatedprogressbar.md) control includes all the features of the native WPF `ProgressBar`, additional features such as animated value updates, animated highlights, and multiple states.

## The CustomDrawElement FrameworkElement

The [CustomDrawElement](customdrawelement.md) element is a simple `FrameworkElement` that provides a [CustomDraw](xref:ActiproSoftware.Windows.Controls.CustomDrawElement.CustomDraw) event, allowing for easy, event-based custom rendering.

## The DropShadowChrome Decorator

The [DropShadowChrome](dropshadowchrome.md) decorator draws a drop-shadow or outer glow around its content.

## The DynamicImage Control

The [DynamicImage](xref:ActiproSoftware.Windows.Controls.DynamicImage) control is a drop-in replacement for `Image` that is the primary UI mechanism for interfacing with [ImageProvider](xref:ActiproSoftware.Windows.Media.ImageProvider) and its features, supporting:

- Chromatic adaptation (color shifting) for images, which allows images designed for light themes to be automatically adjusted for use in dark themes.
- Conversion of a monochrome vector image to render in the current foreground color.
- Dynamic loading of pre-defined high-DPI and/or theme-specific image variations for raster images.
- Automatic conversion of an image to grayscale and optional transparency when the control is disabled.
- Conversion of monochrome images to use the current foreground color.

## The EditableContentControl Control

The [EditableContentControl](editablecontentcontrol.md) control is a standard `ContentControl`, which also features an alternate editing mode that can be toggled to display a `TextBox` for editing a string.

## The HorizontalListBox Control

The [HorizontalListBox](horizontallistbox.md) control is a restyled native `ListBox` that arranges its items in a uniform width horizontally, and without a `ScrollBar`.

## The ImageTextInfo Class

The [ImageTextInfo](imagetextinfo.md) class is a simple helper class that can be used to store image and text data for databinding.

## The PixelSnapper Decorator

The [PixelSnapper](pixelsnapper.md) decorator snaps the measurement of its child content to integer values, thereby helping to prevent blurry images and borders that may appear after it.

## The PopupButton Control

The [PopupButton](popupbutton.md) control supports display of popups or context menus, and can render in multiple display modes.

## The RadialSlider Control

The [RadialSlider](radialslider.md) control is a circular slider that allows for quick selection of a degree value, which can easily be converted to some form of scalar value.

## The RadioButtonList Control

The [RadioButtonList](radiobuttonlist.md) control directly inherits `ListBox` and changes the template of items to look like radio buttons.

## The ReflectionContentControl Control

The [ReflectionContentControl](reflectioncontentcontrol.md) control is a regular `ContentControl` however it also renders a reflection effect of the content below the actual content.

## The ResizableContentControl Control

The [ResizableContentControl](resizablecontentcontrol.md) control is a regular `ContentControl` that contains a gripper on one of its sides or corners.  When it is dragged by the mouse, the content is resized.  The gripper may be double-clicked to reset its size back to the content's desired size.

## The RingSlice Control

The [RingSlice](ringslice.md) control renders a portion (or the entire circle) of a ring shape.  Its start/end angles, radius, thickness, and other stroke properties can be set.

## The ShadowChrome Decorator

The [ShadowChrome](shadowchrome.md) control draws a modern shadow around its content.

## The ZeroSizeContentControl Control

The [ZeroSizeContentControl](zerosizecontentcontrol.md) control is a regular `ContentControl` that can return a zero width or height during its measuring pass.  This is useful in scenarios where the content should take up some space but you don't want the WPF layout routines to consider the space that it will take up during its measuring pass.

## Progress Spinners

[Progress Spinners](progress-spinners.md) are used when some form of processing is occurring to tell the end user that something is happening.
