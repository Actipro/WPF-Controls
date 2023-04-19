---
title: "Pointers"
page-title: "Pointers - CircularGauge Features"
order: 7
---
# Pointers

Pointers can be used to show the current value, or values, represented by a gauge. There are five supported pointer types: bars, caps, labels, markers, and needles.

Any number of pointers can be included by adding an instance of [CircularPointerBar](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerBar), [CircularPointerCap](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerCap), [CircularPointerLabel](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerLabel), [CircularPointerMarker](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerMarker), and/or [CircularPointerNeedle](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerNeedle) to the [CircularTickSet](xref:@ActiproUIRoot.Controls.Gauge.CircularTickSet).[Pointers](xref:@ActiproUIRoot.Controls.Gauge.CircularTickSet.Pointers) collection. In addition, each pointer can present a separate and specific value by setting the [Value](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.Value) property.

![Screenshot](../images/circular-gauge-pointers.gif)

*A CircularGauge showing all four pointer types*

## Bars

Bars show a continuous band to the current value, much like ranges. By default, the bars are shown from the minimum value to the current value (as seen in the images). It is possible to "anchor" the bar to zero or the maximum value by setting the [BarOrigin](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerBar.BarOrigin) property appropriately.

![Screenshot](../images/circular-pointer-bar.gif)

*A CircularGauge with the bar highlighted*

### Colors/Brushes

There are two brushes used by bars: [Background](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.Background) and [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush). The [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush) property is only used when the [BorderWidth](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderWidth) property is greater than `0`.

### Extents

Bars use the [PointerExtent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerExtent) property to determine the thickness of the band. The [PointerAscent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerAscent) property is not used.

## Caps

Caps are a special type of pointer that do not actually "point" to a specific value. Caps are typically used in conjunction with one or more needles to provide a cleaner and more realistic look.

![Screenshot](../images/circular-pointer-cap.gif)

*A CircularGauge with the cap highlighted*

### Types

Several cap types are supported and can be configured by setting the [CapType](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerCap.CapType) property. Each cap type supports a special effect.

### Colors/Brushes

There are two brushes used by caps: [Background](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.Background) and [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush). The [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush) property is only used when the [BorderWidth](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderWidth) property is greater than `0`.

### Extents

Caps use the [PointerExtent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerExtent) property to determine the radius of the cap. The [PointerAscent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerAscent) property is not used.

## Labels

Labels are pointers that render text at the specified value.

### Text

When [Text](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerLabel.Text) is non-`null`, then that sting is used.  When `null`, the text is determined by the [DisplayValue](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.DisplayValue), [RoundMode](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerLabel.RoundMode), and [TextFormat](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerLabel.TextFormat) properties.

### Font and Foreground

There are several properties for setting the appropriate font and/or foreground.

## Markers

Markers are indicators or shapes rendered at the specified value.

![Screenshot](../images/circular-pointer-marker.gif)

*A CircularGauge with the marker highlighted*

### Types

Several marker types are supported and can be configured by setting the [MarkerType](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerMarker.MarkerType) property.

### Colors/Brushes

There are two brushes used by markers: [Background](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.Background) and [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush). The [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush) property is only used when the [BorderWidth](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderWidth) property is greater than `0`.

### Extents/Ascents

Markers use the [PointerExtent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerExtent) and [PointerAscent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerAscent) properties to determine the overall bounding rectangle of the shape rendered.

## Needles

Needles are shapes that point to the specified value and are anchored (or pivot) around a central point.

![Screenshot](../images/circular-pointer-needle.gif)

*A CircularGauge with the needle highlighted*

### Types

Several needle types are supported and can be configured by setting the [NeedleType](xref:@ActiproUIRoot.Controls.Gauge.CircularPointerNeedle.NeedleType) property.

### Colors/Brushes

There are two brushes used by needles: [Background](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.Background) and [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush). The [BorderBrush](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderBrush) property is only used when the [BorderWidth](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.BorderWidth) property is greater than `0`.

### Extents/Ascents

Needles use the [PointerExtent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerExtent) and [PointerAscent](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.PointerAscent) properties to determine the overall bounding rectangle.

## Pointer Direction

When using a sweep angle of 360 degrees, the direction that the pointer rotates can be customized. By default, the pointer always moves between the minimum and maximum values, and it does not cross over the minimum/maximum value location.

This behavior can be changed by setting the [PointerDirection](xref:@ActiproUIRoot.Controls.Gauge.Primitives.CircularPointerBase.PointerDirection) property. The pointer can be set to only move clockwise or counterclockwise, or to always take the shortest path to the new value.

## Dampening

All four pointer types can be configured to animate value changes, using a dampening feature. The [DampeningMaximumDuration](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.DampeningMaximumDuration) determines the amount of time it should take the pointer to travel the entire length of the scale. The pointers will use a relative duration when only traveling a portion of the scale. For example, if the pointer needs to travel half the length of the scale, then it will take half of the [DampeningMaximumDuration](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.DampeningMaximumDuration) to animate to the new location.

When making small value changes then the relative duration may be too short, resulting in a pointer that "jumps". To ensure the duration is not too small, the [DampeningMinimumDuration](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.DampeningMinimumDuration) can be used to set a minimum duration.

## Refresh Rate

When using a [CircularGauge](xref:@ActiproUIRoot.Controls.Gauge.CircularGauge) to display real-time data, it is possible that the value displayed by the gauge will change to quickly for the user to read the individual values. The [PointerBase](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase).[RefreshRate](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.RefreshRate) property can be used to limit the number of updates displayed to the user.

The refresh rate is specified as the amount of time to wait between updates. Therefore, if the refresh rate is set to `500` milliseconds, then there will be two updates to the display every second. If several hundreds value changes are made during that second, then only two of the values will actually be displayed.

## Snapping

The values presented by the various pointers, can be any real value between the minimum and maximum values, including fractions.  Pointer values can be "snapped" to a configurable interval, by setting [IsSnappingEnabled](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.IsSnappingEnabled) to `true`. When snapping is enabled, the pointer value will be coerced so that it is evenly divisible by the snapping interval, which is specified by the [SnappingInterval](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.SnappingInterval) property.

For example, if snapping is enabled with an interval of `1`, then the pointer value will be automatically rounded to the nearest whole number.

> [!TIP]
> Snapping can be used in conjunction with interactive pointers.

The [SnappingMode](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.SnappingMode) property allows you to determine when snapping occurs.  Its default value will always apply snapping when snapping is enabled.  Other values allow you to only snap when dragging the pointer, or only when the value is changed programmatically.

## Interactive Pointers

Pointer values can be interctively changed, by the end-user, using the mouse when [CanDrag](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.CanDrag) is set to `true`. When enabled, the cursor is changed to `Cursors.Hand` when the mouse is hovered over the pointer. The cursor can be customized by setting [DragCursor](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.DragCursor).

> [!TIP]
> By setting `IsFocusable` to `true` on a pointer element, the end-user can press the <kbd>Esc</kbd> key to cancel a drag operation.

By default, the pointer is not animated when the value is changed using the mouse. This behavior can be altered by setting [IsDraggingAnimated](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.IsDraggingAnimated) to `true`.

## Scale Placement

Bar and marker pointers are positioned relative to the scale bar defined by the associated [CircularScale](xref:@ActiproUIRoot.Controls.Gauge.CircularScale) element. By default, these pointers will be overlayed and centered on the scale bar. The placement of these pointers can be altered using the [ScalePlacement](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.ScalePlacement) and [ScaleOffset](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.ScaleOffset) properties.

There are three possible values for the [ScalePlacement](xref:@ActiproUIRoot.Controls.Gauge.Primitives.RangeBase.ScalePlacement) property:

| Value | Description |
|-----|-----|
| `Inside` | Indicates that the bar or marker pointer will be placed inside (or closer to the center) of the scale bar. The outer edge of the bar or marker pointer will be aligned with the inner edge of the scale bar. |
| `Outside` | Indicates that the bar or marker pointer will be placed outside (or further from the center) of the scale bar. The inner edge of the bar or marker pointer will be aligned with the outer edge of the scale bar. |
| `Overlay` | Indicates that the bar or marker pointer will be centered over (or on top) of the scale bar. The center line of the bar or marker pointer will be aligned with the center line of the scale bar. |

In addition to the placement, the [ScaleOffset](xref:@ActiproUIRoot.Controls.Gauge.Primitives.RangeBase.ScaleOffset) can be used to further customize the location of the bar or marker pointers.

## Value Events

The [ValueChanging](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.ValueChanging) and [ValueChanged](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.ValueChanged) are raised before and after the pointer's value is changed, respectively.  Using the [IsValueChangingEventRaised](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.IsValueChangingEventRaised) and [IsValueChangedEventRaised](xref:@ActiproUIRoot.Controls.Gauge.Primitives.PointerBase.IsValueChangedEventRaised) properties these events can either be enabled or disabled, for performance reasons.  By default, the `ValueChanging` is disabled and the `ValueChanged` event is enabled.
