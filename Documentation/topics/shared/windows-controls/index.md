---
title: "Overview"
page-title: "Shared Library Controls"
order: 1
---
# Overview

The [ActiproSoftware.Windows.Controls](xref:@ActiproUIRoot.Controls) namespace contains various user interface controls that may be used in your applications.

## The AdvancedTextBlock Control

The [AdvancedTextBlock](advancedtextblock.md) control can show a tooltip when overflowed, and can highlight spans of text based on captured text ranges (i.e., filter match results).

## The AnimatedExpander Control

The [AnimatedExpander](animatedexpander.md) control is a regular `Expander` that provides optional animated expansion functionality.  The animation consists of crossfade and slide behavior.

## The AnimatedProgressBar Control

The [AnimatedProgressBar](animatedprogressbar.md) control includes all the features of the native WPF `ProgressBar`, additional features such as animated value updates, animated highlights, and multiple states.

## The Avatar Control

The [Avatar](avatar.md) control is used to represent people or objects.  They can render a full-size image, a centered glyph, a person's initials, or text.

## The AvatarGroup Control

The [AvatarGroup](avatar-group.md) control renders multiple [Avatar](avatar.md) controls.

## The Badge Control

The [Badge](badge.md) control is used to provide contextual information for other elements or can be used stand-alone.

## The Card Control

The [Card](card.md) control is typically used to present visually grouped information for a single subject.

## The CircularProgressBar Control

[CircularProgressBar](circular-progressbar.md) displays a ranged progress value using fluent animations.  It is similar to a native linear `ProgressBar`, except that it renders the progress in a ring shape.

## The CustomDrawElement FrameworkElement

The [CustomDrawElement](customdrawelement.md) element is a simple `FrameworkElement` that provides a [CustomDraw](xref:@ActiproUIRoot.Controls.CustomDrawElement.CustomDraw) event, allowing for easy, event-based custom rendering.

## The DropShadowChrome Decorator

The [DropShadowChrome](dropshadowchrome.md) decorator draws a drop-shadow or outer glow around its content.

## The DynamicImage Control

The [DynamicImage](xref:@ActiproUIRoot.Controls.DynamicImage) control is a drop-in replacement for `Image` that is the primary UI mechanism for interfacing with [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) and its features, supporting:

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

## The InfoBar Control

The [InfoBar](info-bar.md) control can be used to display essential information to a user without disrupting the user flow.

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

## The ToggleSwitch Control

The [ToggleSwitch](toggle-switch.md) is modern refinement of a checkbox control that is typically toggled between on/off states and optionally supports an indeterminate state.

## The User Prompt Controls

The [User Prompt](user-prompt/index.md) controls include [ThemedMessageBox](xref:@ActiproUIRoot.Controls.ThemedMessageBox) as a fully themed drop-in replacement for the native `MessageBox` and the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) for creating more advanced user prompts beyond the what is available from `MessageBox`.

## The ZeroSizeContentControl Control

The [ZeroSizeContentControl](zerosizecontentcontrol.md) control is a regular `ContentControl` that can return a zero width or height during its measuring pass.  This is useful in scenarios where the content should take up some space, but you don't want the WPF layout routines to consider the space that it will take up during its measuring pass.

## Progress Spinners

[Progress Spinners](progress-spinners.md) are used when some form of processing is occurring to tell the end user that something is happening.
