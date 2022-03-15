---
title: "Zooming"
page-title: "Zooming - SyntaxEditor Editor View Features"
order: 10
---
# Zooming

SyntaxEditor supports animated zooming in and out of views, which is a great feature to use when giving overhead presentations.  Only the scrollable area content of each view is zoomed, not the scrollbars.

## Mouse Wheel Zoom

If the flags-based [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ZoomModesAllowed](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomModesAllowed) enumeration property includes the `Mouse` value, mouse wheel zooming is enabled.

Mouse wheel zoom is activated at run-time by holding the `Ctrl` key and turning the mouse wheel up to zoom in (make text larger) and down to zoom out.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ZoomLevelIncrement](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomLevelIncrement) property determines the percentage amount by which to zoom with the mouse wheel.  It defaults to `0.25`, which means a 25% increment on each mouse wheel notch scroll.

## Keyboard Zoom

If the flags-based [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ZoomModesAllowed](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomModesAllowed) enumeration property includes the `Keyboard` value, keyboard zooming is enabled.

| Key | Action |
|-----|-----|
| Ctrl + + | Zoom in. |
| Ctrl + - | Zoom out. |
| Ctrl + 0 | Reset zoom to 100%. |

## Touch Zoom

If the flags-based [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ZoomModesAllowed](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomModesAllowed) enumeration property includes the `Touch` value, pinch-to-zoom is enabled.

Place two fingers on the editor view and spread them apart to zoom in, or bring them closer together to zoom out.

## Zoom Level

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomLevel) property gets and sets the current zoom level of the control.

This code sets the zoom level to 300%:

```csharp
editor.ZoomLevel = 3.0;
```

> [!TIP]
> You can bind the [ZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomLevel) property to a `Slider` control that is in the status bar to provide end users an additional way to zoom.

## Min/Max Zoom Levels

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[MinZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MinZoomLevel) and [MaxZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MaxZoomLevel) properties denote the  range of zoom levels over which zooming can occur.

By setting these properties you can prevent end users from zooming in too far, etc.

@if (wpf) {

## IntelliPrompt Zooming

By default, IntelliPrompt popups will automatically scale with the current editor zoom level, but only in the range of 100% - 300%.  The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[MinIntelliPromptZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MinIntelliPromptZoomLevel) and [MaxIntelliPromptZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MaxIntelliPromptZoomLevel) properties control the range of allowed zoom levels for IntelliPrompt popups.

To prevent IntelliPrompt popups from zooming at all, set the [MaxIntelliPromptZoomLevel](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.MaxIntelliPromptZoomLevel) property to `1.0`.  Since both the min/max will be 100%, no zooming will occur.

}

## Zoom Animation

Zoom animation occurs by default, meaning that zoom level changes smoothly animate, providing a subtle grow/shrink effect.

The animation duration can be set via the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ZoomAnimationDuration](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomAnimationDuration) property.  Set it to `0ms` to turn off animation.

@if (winrt wpf) {

## Adjusting the Text Area Font Size Without Zooming

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[TextAreaFontSize](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.TextAreaFontSize) property can adjust the default font size used in the editor's text area, without affecting any overlay panes or IntelliPrompt popups.  Whereas the normal `FontSize` property can affect the entire editor element hierarchy.

Keep the [TextAreaFontSize](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.TextAreaFontSize) property set to its default `0` to use the `FontSize` property value as the text area font size.

}
