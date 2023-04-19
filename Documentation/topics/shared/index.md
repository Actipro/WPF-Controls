---
title: "Overview"
page-title: "Shared Library Reference"
order: 1
---
# Overview

The Actipro WPF Shared Library is a common control library referenced by all of our WPF .NET controls.  It contains a number of very useful controls and components that can be used in your projects.

The Shared Library is not a product that is sold on its own, but any developer who owns a license for one of our WPF control products is welcome to freely use any of the controls and components within the library.

## Features

### General Controls

- An [AdvancedTextBlock](windows-controls/advancedtextblock.md) control that can show a tooltip when overflowed and can highlight spans of text based on captured text ranges (i.e., filter match results).
- An [AnimatedExpander](windows-controls/animatedexpander.md) control, which supports animated expand/collapse with fade in/out.
- An [AnimatedProgressBar](windows-controls/animatedprogressbar.md) control, which is an enhanced Aero-like progressbar with smooth value change animations, multiple states, and animated highlights.
- A [CircularThumb](xref:@ActiproUIRoot.Controls.Primitives.CircularThumb) control, which is a thumb gripper with a circular shape and arrow adornment.
- A [CustomDrawElement](windows-controls/customdrawelement.md) element, which is an element that raises an event when it is being rendered, allowing for custom drawing.
- A [DropShadowChrome](windows-controls/dropshadowchrome.md) decorator, which can be used to render a drop shadow for a popup.
- A [HorizontalListBox](windows-controls/horizontallistbox.md) control, which allows for selection of items that are arranged horizontally with a uniform width.
- A [PixelSnapper](windows-controls/pixelsnapper.md) decorator, which helps prevent image and border blurring in WPF.
- A [PopupButton](windows-controls/popupbutton.md) control, which provides an implementation of a popup and split button that can display context menu popups or a popup containing any other WPF content.
- A [RadialSlider](windows-controls/radialslider.md) controls, which is a circular slider that can be used to input any scalar value.
- A [RadioButtonList](windows-controls/radiobuttonlist.md) control, which is a `ListBox` whose items render as radio buttons.
- A [RingSlice](windows-controls/ringslice.md) control, which renders a portion of a ring at designated angles and radius.
- A [RingSpinner](windows-controls/progress-spinners.md) control that is a circular busy indicator.
- A [ReflectionContentControl](windows-controls/reflectioncontentcontrol.md) control, which is a content wrapper that makes it possible to create dazzling reflection effects in seconds.
- A [ResizableContentControl](windows-controls/resizablecontentcontrol.md) control, which is a content wrapper that contains a gripper for resizing the content horizontally, vertically, or both.
- A [ToggleSwitch](windows-controls/toggle-switch.md) control, which is a modern alternative to a checkbox that is typically toggled between on/off states and optionally supports an indeterminate state.
- A [UserPromptControl](windows-controls/user-prompt.md) control that can be used to build rich user prompts and replace `MessageBox` usage with prompts that match the current theme.
- A [ZeroSizeContentControl](windows-controls/zerosizecontentcontrol.md) control, which is a content wrapper that can provide a zero-width or height during its measuring pass.

### Color Selection Features

- [SpectrumColorPicker](xref:@ActiproUIRoot.Controls.ColorSelection.SpectrumColorPicker) - A color hue spectrum-based color picker that can display initial and selected colors.
- [SpectrumSlice](xref:@ActiproUIRoot.Controls.ColorSelection.SpectrumSlice) - Displays a slice of saturation/brightness colors for a particular hue.
- [SpectrumSlider](xref:@ActiproUIRoot.Controls.ColorSelection.SpectrumSlider) - Displays a hue spectrum.
- [ColorComponentSlider](xref:@ActiproUIRoot.Controls.ColorSelection.ColorComponentSlider) - A slider capable of displaying and altering a single component (ARGB) of a color.
- [GradientBrushSlider](xref:@ActiproUIRoot.Controls.ColorSelection.GradientBrushSlider) - A slider capable of altering the stops of a linear or radial brush.

### Data

- Multiple [value converters classes](value-converters.md) for doing everything from conditional (if...else) results to string formatting.
- Data validation helpers.

### Document Management

- Multiple classes for maintaining [document references](windows-document-management.md) and tracking/persisting recently used documents.

### Media

- A [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure that provides an enhanced representation of a `Color` object that supports the RGB, HSB, and HLS color models, conversion between models, and numerous other color helper methods.
- A [VisualTreeHelperExtended](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended) class that provides several helper methods for working with visual trees that are not found in the VisualTreeHelper class.

### Media Animation (Transitions)

- Barn door wipe - A wipe transition between two pages that uses two straight bars with a configurable gradient spread.
- Bar wipe - A wipe transition between two pages that uses a straight bar with a configurable gradient spread.
- Box wipe - A wipe transition between two pages that uses a box.
- Fade - The old selected page fades into the new one with optional blur effect.
- Faded zoom - A crossfade transition between two elements that zooms the new content in as well.
- Four box wipe - A wipe transition between two pages that uses four boxes.
- Slide - A slide transition between two pages that moves one page over the other.
- Push - A slide transition between two pages that pushes one page out of the way for the other.
- Wedge wipe - A wipe transition between two pages that uses a wedge shape.

### Serialization

- A helper class for serializing objects to and deserializing directly from XAML.
- A framework for easily serializing a hierarchy of objects to XML, and later restoring it.

### Shapes

- Semi-ellipse, triangle, wave, and zig-zag [shape](shapes.md) classes.

*This product is written in 100% pure C#, and includes detailed documentation and samples.*
