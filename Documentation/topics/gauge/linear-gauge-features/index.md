---
title: "Overview"
page-title: "CircularGauge Features"
order: 1
---
# Overview

To allow for easy customization, most of the functionality found in the [LinearGauge](xref:ActiproSoftware.Windows.Controls.Gauge.LinearGauge) control is actually provided through child elements. This topic will briefly describe each aspect of the [LinearGauge](xref:ActiproSoftware.Windows.Controls.Gauge.LinearGauge) control starting from the top and working down.  Additional topics are provided for areas that require more detailed explanations.

## Orientation

Linear gauges can be oriented horizontally (default) and vertically by setting the [Orientation](xref:ActiproSoftware.Windows.Controls.Gauge.LinearGauge.Orientation) property.

## Extents and Ascents

Most of the [LinearGauge](xref:ActiproSoftware.Windows.Controls.Gauge.LinearGauge) elements provide one or more properties that control the size of the rendered content. For example, [TickMarkExtent](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.LinearTickMarkBase.TickMarkExtent) and [TickMarkAscent](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.LinearTickMarkBase.TickMarkAscent) control the width/height of the tick marks.

When the linear gauge is oriented horizontally, the extent equates to the width of the element and the ascent equates to the height. When oriented vertically, the extent and ascent change meaning so the extent equates to the height of the element and the ascent equates to the width.

Both extents and ascents can be specified as a percentage of the frame width/height, or as a fixed value. For example, `TickMarkExtent="5%"` will size the tick mark to be 5% of the frame width/height (depending on orientation), therefore if the frame width/height is changed the tick mark size will change proportionally.

## Frames

Several frames are support and are rendered in the background of the control.

See the [Frames](frames.md) topic for more information.

## Scales

[LinearGauge](xref:ActiproSoftware.Windows.Controls.Gauge.LinearGauge) uses [LinearScale](xref:ActiproSoftware.Windows.Controls.Gauge.LinearScale) elements to host the remaining elements described below and is used for overall positioning.

See the [Scales](scales.md) topic for more information.

## Tick Sets

The [LinearScale](xref:ActiproSoftware.Windows.Controls.Gauge.LinearScale) elements can contain one or more [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet) elements, which specify the minimum value, maximum value, and intervals used by the remaining elements.

See the [Tick Sets](tick-sets.md) topic for more information.

## Tick Marks and Labels

The [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet) elements can contain one or more [LinearTickMarkMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor), [LinearTickMarkMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor), [LinearTickMarkCustom](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkCustom), [LinearTickLabelMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor), [LinearTickLabelMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor), and/or [LinearTickLabelCustom](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelCustom) elements, which render the tick marks/labels along the scale bar.

See the [Tick Marks and Labels](tick-marks-and-labels.md) topic for more information.

## Ranges

The [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet) elements can contain one or more [LinearRange](xref:ActiproSoftware.Windows.Controls.Gauge.LinearRange) elements, which are used to highlight a value range of interest.

See the [Ranges](ranges.md) topic for more information.

## Pointers

The [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet) elements can contain one or more [LinearPointerBar](xref:ActiproSoftware.Windows.Controls.Gauge.LinearPointerBar), [LinearPointerLabel](xref:ActiproSoftware.Windows.Controls.Gauge.LinearPointerLabel), and/or [LinearPointerMarker](xref:ActiproSoftware.Windows.Controls.Gauge.LinearPointerMarker) elements, which provide visual feedback of a value.

See the [Pointers](pointers.md) topic for more information.
