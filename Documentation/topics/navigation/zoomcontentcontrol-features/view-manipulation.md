---
title: "View Manipulation"
page-title: "View Manipulation - ZoomContentControl Features"
order: 2
---
# View Manipulation

The [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) offers numerous way to manipulate the view, both interactively and programmatically. This topic covers these aspects of the control.

![Screenshot](../images/zoomcontentcontrol-teaser.png)

## Zooming

The content displayed by the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) will be scaled by [ZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevel), along both the X and Y axes. There are several ways to interactively and programmatically adjust the zoom level.

### Minimum/Maximum Zoom Level

The [ZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevel) is restricted to the range defined by the [MinZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.MinZoomLevel) and [MaxZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.MaxZoomLevel) properties.  Both of these properties must be greater than `0`, and the [MaxZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.MaxZoomLevel) must be larger than or equal to the [MinZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.MinZoomLevel).

If the zoom level is set to the value outside of this range, it will be coerced to fall within the range.

### Zoom Level Stops

The [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) property is used by several of the helper methods listed below to incrementally increase or decrease [ZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevel).  When specified, the `ZoomLevel` will be increment or decremented to the next stop value.  This allows the zoom in and out commands/methods to follow a predefined sets of zoom levels (e.g.. 10%, 25%, 33%, 50%, 75%, 100%, 200%, 300%, etc).

> [!NOTE]
> If an appropriate stop value cannot be found, then the minimum or maximum zoom level will be used, based on whether the user is zooming in or out.

By default, this property is set to `null` which indicates that the `ZoomStep` (described below) should be used.

### Zoom Step

As an alternative to the zoom level stops, the [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) property can provide a fixed value that derives exponential zoom level stop power increments.

If one or more zoom level stops are specified in the `ZoomLevelStops` collection, then the `ZoomStep` property is not used.

### Helper Methods

There are several methods that can be used to adjust the zoom level, which include:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[CenterAndZoomInToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterAndZoomInToPoint*) Method

</td>
<td>

Centers the view to the specified point and zooms the content in using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep).

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[CenterAndZoomOutFromPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterAndZoomOutFromPoint*) Method

</td>
<td>

Centers the view to the specified point and zooms the content out using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep).

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[ResetView](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ResetView*) Method

</td>
<td>

Resets the view to the [DefaultZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.DefaultZoomLevel) and [DefaultCenterPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.DefaultCenterPoint).

</td>
</tr>

<tr>
<td>

[StartZoomDrag](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.StartZoomDrag*) Method

</td>
<td>

Starts a zoom-drag operation, where the user can zoom-in by moving the mouse up and zoom-out by moving the mouse down.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can zoom the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomIn](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.StartZoomIn*) Method

</td>
<td>

Starts a zoom-in operation, where the user can continuously zoom-in by holding the mouse button down.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can continuously zoom-in the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomOut](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.StartZoomOut*) Method

</td>
<td>

Starts a zoom-out operation, where the user can continuously zoom-out by holding the mouse button down.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can continuously zoom-out the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomToRegion](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.StartZoomToRegion*) Method

</td>
<td>

Starts a zoom-to-region operation, where the user can draw a rectangle over the content to indicate the desired region.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can draw a rectangle over the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[ZoomIn](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomIn*) Method

</td>
<td>

Zooms the content in using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomIn](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomIn*).

</td>
</tr>

<tr>
<td>

[ZoomInToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomInToPoint*) Method

</td>
<td>

Zooms the content in using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) with the specific point as an anchor.

If no point is specified, then the current mouse position will be used.

</td>
</tr>

<tr>
<td>

[ZoomOut](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomOut*) Method

</td>
<td>

Zooms the content out using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep).

</td>
</tr>

<tr>
<td>

[ZoomOutFromPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomOutFromPoint*) Method

</td>
<td>

Zooms the content out using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) with the specific point as an anchor.

If no point is specified, then the current mouse position will be used.

</td>
</tr>

<tr>
<td>

[ZoomToFit](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomToFit*) Method

</td>
<td>

Zooms the content so that it fits with-in the bounds of the control and centers the view.

Various overloads can be used to zoom-to-fit horizontally and/or vertically, and optionally center the view.

</td>
</tr>

</tbody>
</table>

## Panning

The content displayed by the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) can be scrolled (or panned) in any direction. There are several ways to interactively and programmatically adjust the pan offsets.

### Center Point

The [CenterPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterPoint) property is used to track the current pan offsets. The center point is relative the content, not the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) control. Therefore, a point of `0,0` would indicate that the top-left corner of the content should be centered in the view.

The size of the content can be obtained from the [ChildSize](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ChildSize) property. If the center point falls outside of this range, it will be coerced to fall within the range.

The resolved coerced value (that is the current true center point) is available from the [CenterPointResolved](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterPointResolved) property.

### Horizontal/Vertical Steps

The [HorizontalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollLineStep), [HorizontalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollPageStep), [VerticalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollLineStep), and [VerticalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollPageStep) properties are used by several of the helper methods listed below to incrementally increase or decrease the `X` or `Y` values of the [CenterPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterPoint).

These steps values are specified using a [Unit](xref:@ActiproUIRoot.Unit), which can represent a fixed number of pixels or a percentage of the visible area. For example, a value of `Unit.Percentage(100)` would scroll the view one full page either horizontally or vertically, assuming the content is large enough to allow scrolling.

### Helper Methods

There are several methods that can be used to adjust the zoom level, which include:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[CenterAndZoomInToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterAndZoomInToPoint*) Method

</td>
<td>

Centers the view to the specified point and zooms the content in by the [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) value.

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[CenterAndZoomOutFromPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterAndZoomOutFromPoint*) Method

</td>
<td>

Centers the view to the specified point and zooms the content out by the [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) value.

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[CenterToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterToPoint*) Method

</td>
<td>

Centers the view to the specified point.

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[CenterView](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.CenterView*) Method

</td>
<td>Centers the content in the view horizontally and/or vertically.</td>
</tr>

<tr>
<td>

[LineDown](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.LineDown*) Method

</td>
<td>

Scrolls the content down by [VerticalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollLineStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[LineLeft](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.LineLeft*) Method

</td>
<td>

Scrolls the content left by [HorizontalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollLineStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[LineRight](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.LineRight*) Method

</td>
<td>

Scrolls the content right by [HorizontalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollLineStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[LineUp](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.LineUp*) Method

</td>
<td>

Scrolls the content up by [VerticalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollLineStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[PageDown](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.PageDown*) Method

</td>
<td>

Scrolls the content down by [VerticalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollPageStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[PageLeft](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.PageLeft*) Method

</td>
<td>

Scrolls the content left by [HorizontalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollPageStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[PageRight](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.PageRight*) Method

</td>
<td>

Scrolls the content right by [HorizontalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollPageStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[PageUp](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.PageUp*) Method

</td>
<td>

Scrolls the content up by [VerticalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollPageStep), with an optional factor (which is multipled by the step value).

</td>
</tr>

<tr>
<td>

[Pan](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.Pan*) Method

</td>
<td>Pans the view by the specified x and y offsets.</td>
</tr>

<tr>
<td>

[ResetView](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ResetView*) Method

</td>
<td>

Resets the view to the [DefaultZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.DefaultZoomLevel) and [DefaultCenterPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.DefaultCenterPoint).

</td>
</tr>

<tr>
<td>

[StartPanDrag](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.StartPanDrag*) Method

</td>
<td>

Starts a pan-drag operation, where the user can move the content along with the mouse.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can pan the content until the mouse button is released.

</td>
</tr>

</tbody>
</table>

## Virtual Space

When the content can be fully displayed (either horizontally, vertically, or both) in the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) then it is centered, and the view cannot be panned.  Additionally, the content must always be aligned to the edges of the view. For example, the left edge of the content cannot be scrolled to the right of the left edge of the view.

By setting [IsVirtualSpaceEnabled](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.IsVirtualSpaceEnabled) to `true`, then any part of the content can be centered into the view. This setting adds "virtual space" around the control to allow, which increases the scrollable area.

## Mouse Behavior

The mouse behavior of the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) can be customized customized by updating the `InputBindings` collection.

See the [Input Bindings](input-bindings.md) topic for more information.

## Supported Commands

The commands supported by the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) are defined by the [ZoomContentControlCommands](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands) static class.

> [!NOTE]
> Several of the commands in [ZoomContentControlCommands](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands) return a command that is defined elsewhere. For example, the [LineDown](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.LineDown) command returns the command defined by `ScrollBar.LineDownCommand`.  This makes it easier to find the supported commands.

The following table describes the various commands.

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[CenterAndZoomInToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.CenterAndZoomInToPoint) Property

</td>
<td>

Centers the view to the point specified as the command parameter and zooms the content in using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep).

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[CenterAndZoomOutFromPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.CenterAndZoomOutFromPoint) Property

</td>
<td>

Centers the view to the point specified as the command parameter and zooms the content out using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep).

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[CenterToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.CenterToPoint) Property

</td>
<td>

Centers the view to the point specified as the command parameter.

If no point is specified, then the view will be centered to the current mouse position.

</td>
</tr>

<tr>
<td>

[LineDown](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.LineDown) Property

</td>
<td>

Scrolls the content down by [VerticalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollLineStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[LineLeft](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.LineLeft) Property

</td>
<td>

Scrolls the content left by [HorizontalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollLineStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[LineRight](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.LineRight) Property

</td>
<td>

Scrolls the content right by [HorizontalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollLineStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[LineUp](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.LineUp) Property

</td>
<td>

Scrolls the content up by [VerticalScrollLineStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollLineStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[PageDown](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.PageDown) Property

</td>
<td>

Scrolls the content down by [VerticalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollPageStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[PageLeft](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.PageLeft) Property

</td>
<td>

Scrolls the content left by [HorizontalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollPageStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[PageRight](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.PageRight) Property

</td>
<td>

Scrolls the content right by [HorizontalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollPageStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[PageUp](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.PageUp) Property

</td>
<td>

Scrolls the content up by [VerticalScrollPageStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollPageStep), with an optional factor (which is multipled by the step value) specified as the command parameter.

</td>
</tr>

<tr>
<td>

[ResetView](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.ResetView) Property

</td>
<td>

Resets the view to the [DefaultZoomLevel](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.DefaultZoomLevel) and [DefaultCenterPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.DefaultCenterPoint).

</td>
</tr>

<tr>
<td>

[StartPanDrag](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.StartPanDrag) Property

</td>
<td>

Starts a pan-drag operation, where the user can move the content along with the mouse.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can pan the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomDrag](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.StartZoomDrag) Property

</td>
<td>

Starts a zoom-drag operation, where the user can zoom-in by moving the mouse up and zoom-out by moving the mouse down.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can zoom the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomIn](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.StartZoomIn) Property

</td>
<td>

Starts a zoom-in operation, where the user can continuously zoom-in by holding the mouse button down.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can continuously zoom-in the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomOut](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.StartZoomOut) Property

</td>
<td>

Starts a zoom-out operation, where the user can continuously zoom-out by holding the mouse button down.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can continuously zoom-out the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[StartZoomToRegion](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.StartZoomToRegion) Property

</td>
<td>

Starts a zoom-to-region operation, where the user can draw a rectangle over the content to indicate the desired region.

The left, right, or middle mouse button must be pressed when this method is called.  Once the operation is started, the end-user can draw a rectangle over the content until the mouse button is released.

</td>
</tr>

<tr>
<td>

[ZoomIn](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.ZoomIn) Property

</td>
<td>

Zooms the content in using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomIn](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomIn*).

</td>
</tr>

<tr>
<td>

[ZoomInToPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.ZoomInToPoint) Property

</td>
<td>

Zooms the content in using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) with the point specified as the command parameter as an anchor.

If no point is specified, then the current mouse position will be used.

</td>
</tr>

<tr>
<td>

[ZoomOut](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.ZoomOut) Property

</td>
<td>

Zooms the content out using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep).

</td>
</tr>

<tr>
<td>

[ZoomOutFromPoint](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.ZoomOutFromPoint) Property

</td>
<td>

Zooms the content out using [ZoomLevelStops](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomLevelStops) or [ZoomStep](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomStep) with the point specified as the command parameter as an anchor.

If no point is specified, then the current mouse position will be used.

</td>
</tr>

<tr>
<td>

[ZoomToFit](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControlCommands.ZoomToFit) Property

</td>
<td>

Zooms the content so that it fits with-in the bounds of the control and centers the view.

An `Orientation` can be passed as the command parameter to only zoom-to-fit horizontally or vertically.

</td>
</tr>

</tbody>
</table>

## Scroll Bars

The [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) includes a `ScrollViewer` in the default control template. The visibility of associated scrollbars can be customized using the [HorizontalScrollBarVisibility](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollBarVisibility) and/or [VerticalScrollBarVisibility](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollBarVisibility) properties.

> [!TIP]
> If [HorizontalScrollBarVisibility](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.HorizontalScrollBarVisibility) or [VerticalScrollBarVisibility](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.VerticalScrollBarVisibility) is set to `ScrollBarVisibility.Auto` then the view will **shift** when the scrollbars are shown or hidden (as the viewport size will shrink or grow, respectively).

## Batch Updates

By default, the [ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) will immediately update the display when the zoom level or the center point is changed. Changes can be batched together, so that only a single update is made to the display.  To batch changes into a single update, the changes must be wrapped by calls to [BeginUpdate](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.BeginUpdate*) and [EndUpdate](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.EndUpdate*).

The [EndUpdate](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.EndUpdate*) includes an overload that can be used to prevent the changes from being animated.

## Animations

[ZoomContentControl](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl) supports smooth animations when zooming and panning. The duration of the animations are control by the [ZoomAnimationDuration](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.ZoomAnimationDuration) and [PanAnimationDuration](xref:@ActiproUIRoot.Controls.Navigation.ZoomContentControl.PanAnimationDuration) properties, respectively.
