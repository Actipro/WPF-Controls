---
title: "Input Bindings"
page-title: "Input Bindings - ZoomContentControl Features"
order: 3
---
# Input Bindings

The [ZoomContentControl](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl) supports interactive zooming and panning through the use of input bindings. This topic convers the default behavior, as well as how to customize the behavior.

## Mouse Gestures

The Windows Presentation Framework (WPF) includes support for defining input bindings on controls, through the `InputBindings` collection. This collection defines various gestures, which when executed will raise an associated command.

There are two native WPF gestures that can be used: `MouseGesture` and `KeyboardGesture`. `MouseGesture` encompasses actions like clicking (and double-clicking) mouse buttons, while `KeyboardGesture` captures pressing keys.  Both types support modifier keys, such as `Control`, `Shift`, etc. This tells the gesutre that the specifed modifier key must be pressed when the gesture is performed for the assocated command to be fired. For example, when using `MouseAction.LeftClick` with a modifier key of `ModifierKeys.Control`, then the associated command is only fired when the user click the left mouse button while holding down the control key.

The Shared Library adds an additional gesture that can bind mouse wheel actions to a command. [MouseWheelGesture](xref:ActiproSoftware.Windows.Input.MouseWheelGesture) supports positive or negative changes (i.e. up or down scrolling of the mouse wheel), and can also be used with the [ZoomContentControl](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl).

There are several helper types that make defining a gesture easier via XAML, whic include `KeyboardBinding`, `MouseBinding`, and [MouseWheelBinding](xref:ActiproSoftware.Windows.Input.MouseWheelBinding). Using these classes, the `Gesture` property can be set to a simple string value. For example, setting the `MouseBinding.Gesture` property to "Control+LeftClick" will automatically create the appropriate `MouseGesture`.

## Default Mouse Behavior

The [ZoomContentControl](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl) includes several default input bindings that use `MouseGesture` and [MouseWheelGesture](xref:ActiproSoftware.Windows.Input.MouseWheelGesture) with one of the commands defined by [ZoomContentControlCommands](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands).

The following table lists the default `MouseGesture` bindings and their associated command.

| Gesture | Command |
|-----|-----|
| `LeftClick` | [StartPanDrag](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartPanDrag) |
| `Control+LeftClick` | [StartZoomIn](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartZoomIn) |
| `Control+Shift+LeftClick` | [StartZoomOut](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartZoomOut) |
| `Shift+LeftClick` | [StartPanDrag](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartPanDrag) |

The following table lists the default [MouseWheelGesture](xref:ActiproSoftware.Windows.Input.MouseWheelGesture) bindings and their associated command.

| Gesture | Command |
|-----|-----|
| `PositiveDelta` | [LineUp](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.LineUp) |
| `NegativeDelta` | [LineDown](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.LineDown) |
| `Shift+PositiveDelta` | [LineLeft](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.LineLeft) |
| `Shift+NegativeDelta` | [LineRight](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.LineRight) |
| `Control+PositiveDelta` | [ZoomInToPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomInToPoint) |
| `Control+NegativeDelta` | [ZoomOutFromPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomOutFromPoint) |
| `Control+Shift+PositiveDelta` | [ZoomInToPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomInToPoint) |
| `Control+Shift+NegativeDelta` | [ZoomOutFromPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomOutFromPoint) |

The default input bindings can be disabled by setting [AreDefaultInputBindingsEnabled](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl.AreDefaultInputBindingsEnabled) to `false`. This allows the `InputBindings` to be completely customized from XAML.

## Mouse Cursor

The [ZoomContentControl](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl) will automatically update it's `Cursor` property based on the specified input bindings. Specifically, it looks for an input binding that uses a `MouseGesture` with a `MouseAction.LeftClick` action and specifies modifier keys that match the current key state.  For example, if the `Control` key is currently pressed, then it will search for a `MouseGesture` with a `MouseAction.LeftClick` action and with `ModifierKeys.Control` defined.

If an input binding is found that matches the current state, then the cursor will be updated based on the specified command.  The following table lists the commands and their associated cursor.

| Command | Cursor |
|-----|-----|
| [CenterAndZoomInToPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.CenterAndZoomInToPoint) | ![Screenshot](../images/zoomcontentcontrol-cursor-center-and-zoom-in-to-point.png) |
| [CenterAndZoomOutFromPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.CenterAndZoomOutFromPoint) | ![Screenshot](../images/zoomcontentcontrol-cursor-center-and-zoom-out-from-point.png) |
| [CenterToPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.CenterToPoint) | ![Screenshot](../images/zoomcontentcontrol-cursor-center-to-point.png) |
| [ResetView](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ResetView) | None |
| [StartPanDrag](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartPanDrag) | ![Screenshot](../images/zoomcontentcontrol-cursor-pan-drag-open.png) and ![Screenshot](../images/zoomcontentcontrol-cursor-pan-drag-closed.png) |
| [StartZoomDrag](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartZoomDrag) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-drag.png) |
| [StartZoomIn](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartZoomIn) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-in.png) |
| [StartZoomOut](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartZoomOut) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-out.png) |
| [StartZoomToRegion](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.StartZoomToRegion) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-to-region.png) |
| [ZoomIn](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomIn) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-in.png) |
| [ZoomInToPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomInToPoint) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-in.png) |
| [ZoomOut](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomOut) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-out.png) |
| [ZoomOutFromPoint](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomOutFromPoint) | ![Screenshot](../images/zoomcontentcontrol-cursor-zoom-out.png) |
| [ZoomToFit](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControlCommands.ZoomToFit) | None |

The mouse cursor can be customized as needed by overriding the [UpdateCursor](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl.UpdateCursor*) method.
