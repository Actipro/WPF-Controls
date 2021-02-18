---
title: "Tick Marks and Labels"
page-title: "Tick Marks and Labels - LinearGauge Features"
order: 5
---
# Tick Marks and Labels

Tick marks and tick labels can be included and make it easier to visually determine the current value of a pointer.

![Screenshot](../images/linear-gauge-ticks.gif)

*A LinearGauge with various tick marks and tick labels*

## Tick Marks

Tick marks are indicators or shapes rendered at a given interval along a value range. Both the interval and the value range are defined on the associated [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet) element. See the [Tick Sets](tick-sets.md) topic for more information.

There are three types of tick marks: Major, Minor, and Custom.

### Major Tick Marks

Major tick marks are rendered at the [MajorInterval](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickSetBase.MajorInterval) specified on the associated [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet). By default, major tick marks are larger than minor tick marks.

![Screenshot](../images/linear-tick-mark-major.gif)

*A LinearGauge with the major tick marks highlighted*

Major tick marks can be included by adding an instance of [LinearTickMarkMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor) to the [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet).[Ticks](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet.Ticks) collection. A single instance of [LinearTickMarkMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor) is typically used to render all of the major tick marks.

Which tick marks are rendered by an instance of [LinearTickMarkMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor) can be restricted using the following properties:

| Property | Description |
|-----|-----|
| [EndValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor.EndValue) | Specifies the last (or largest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no end value. |
| [MaximumTickVisibility](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor.MaximumTickVisibility) | Specifies when the tick mark for the maximum value should be rendered. |
| [MinimumTickVisibility](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor.MinimumTickVisibility) | Specifies when the tick mark for the minimum value should be rendered. |
| [SkipValues](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor.SkipValues) | Specifies 0 or more specific values that should not be rendered. |
| [StartValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor.StartValue) | Specifies the first (or smallest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no start value. |

> [!TIP]
> You can use multiple instances of [LinearTickMarkMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMajor) to show tick marks in a specific range using a unique look.
> 
> Using the images above as an example, one instance could be set to use a black background (as shown) and have an end value of 90. A second instance could be set to use a red background and have a start value of 100.

### Minor Tick Marks

Minor tick marks are rendered at the [MinorInterval](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickSetBase.MinorInterval) specified on the associated [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet). Additionally, minor tick marks are **not** rendered when they fall on the major interval.

![Screenshot](../images/linear-tick-mark-minor.gif)

*A LinearGauge with the minor tick marks highlighted*

Minor tick marks can be included by adding an instance of [LinearTickMarkMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor) to the [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet).[Ticks](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet.Ticks) collection. A single instance of [LinearTickMarkMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor) is typically used to render all of the minor tick marks.

Which tick marks are rendered by an instance of [LinearTickMarkMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor) can be restricted using the following properties:

| Property | Description |
|-----|-----|
| [EndValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor.EndValue) | Specifies the last (or largest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no end value. |
| [SkipValues](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor.SkipValues) | Specifies 0 or more specific values that should not be rendered. |
| [StartValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor.StartValue) | Specifies the first (or smallest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no start value. |

> [!TIP]
> You can use multiple instances of [LinearTickMarkMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkMinor) to show tick marks in a specific range using a unique look.
> 
> Using the images above as an example, one instance could be set to use a black background (as shown) and have an end value of 95. A second instance could be set to use a red background and have a start value of 105.

### Custom Tick Marks

When the major and minor tick marks do not offer enough customization, custom tick marks maybe used. Custom tick marks can be placed at a specifc value (shown at value 98.6 in the images above) by setting the [Value](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickMarkCustom.Value) property. Just like the major and minor tick marks, the look of the custom tick marks can be fully customized.

## Tick Labels

Tick labels are textual representation of a value rendered at a given interval along a value range. Both the interval and the value range are defined on the associated [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet) element. See the [Tick Sets](tick-sets.md) topic for more information.

There are three types of tick labels: Major, Minor, and Custom.

### Major Tick Labels

Major tick labels are rendered at the [MajorInterval](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickSetBase.MajorInterval) specified on the associated [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet).

![Screenshot](../images/linear-tick-label-major.gif)

*A LinearGauge with the major tick labels highlighted*

Major tick labels can be included by adding an instance of [LinearTickLabelMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor) to the [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet).[Ticks](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet.Ticks) collection. A single instance of [LinearTickLabelMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor) is typically used to render all of the major tick labels.

Which tick labels are rendered by an instance of [LinearTickLabelMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor) can be restricted using the following properties:

| Property | Description |
|-----|-----|
| [EndValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor.EndValue) | Specifies the last (or largest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no end value. |
| [MaximumTickVisibility](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor.MaximumTickVisibility) | Specifies when the tick label for the maximum value should be rendered. |
| [MinimumTickVisibility](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor.MinimumTickVisibility) | Specifies when the tick label for the minimum value should be rendered. |
| [SkipValues](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor.SkipValues) | Specifies 0 or more specific values that should not be rendered. |
| [StartValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor.StartValue) | Specifies the first (or smallest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no start value. |

> [!TIP]
> You can use multiple instances of [LinearTickLabelMajor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMajor) to show tick labels in a specific range using a unique look.
> 
> Using the images above as an example, one instance could be set to use a black foreground (as shown) and have an end value of 90. A second instance could be set to use a red foreground and have a start value of 100.

### Minor Tick Labels

Minor tick labels are rendered at the [MinorInterval](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickSetBase.MinorInterval) specified on the associated [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet). Additionally, minor tick labels are **not** rendered when they fall on the major interval.

![Screenshot](../images/linear-tick-label-minor.gif)

*A LinearGauge with the minor tick labels highlighted*

Minor tick labels can be included by adding an instance of [LinearTickLabelMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor) to the [LinearTickSet](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet).[Ticks](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickSet.Ticks) collection. A single instance of [LinearTickLabelMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor) is typically used to render all of the minor tick labels.

Which tick labels are rendered by an instance of [LinearTickLabelMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor) can be restricted using the following properties:

| Property | Description |
|-----|-----|
| [EndValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor.EndValue) | Specifies the last (or largest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no end value. |
| [SkipValues](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor.SkipValues) | Specifies 0 or more specific values that should not be rendered. |
| [StartValue](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor.StartValue) | Specifies the first (or smallest) value that should be rendered by the element. Set to `Double.NaN` to specify that there is no start value. |

> [!TIP]
> You can use multiple instances of [LinearTickLabelMinor](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelMinor) to show tick labels in a specific range using a unique look.
> 
> Using the images above as an example, one instance could be set to use a black foreground (as shown) and have an end value of 95. A second instance could be set to use a red foreground and have a start value of 105.

### Custom Tick Labels

When the major and minor tick labels do not offer enough customization, custom tick labels maybe used. Custom tick labels can be placed at a specifc value (shown at value 98.6 in the images above) by setting the [Value](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelCustom.Value) property. Additionally, the text displayed by the custom label can be specified using the [Text](xref:ActiproSoftware.Windows.Controls.Gauge.LinearTickLabelCustom.Text) property. Just like the major and minor tick labels, the look of the custom tick labes can be fully customized.

### Rounding and Text Format

By default, the tick labels will round the associated value(s) to the nearest whole number and convert that to a string, which is displayed on the gauge. Both the method of rounding and the text format can be customized using the [RoundMode](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.LinearTickLabelBase.RoundMode) and [TextFormat](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.LinearTickLabelBase.TextFormat) properties, respectively.

The rounding used is specified using the [RoundMode](xref:ActiproSoftware.Windows.Controls.RoundMode) enumeration, which offers several rounding methods.  See the [PixelSnapper](../../shared/windows-controls/pixelsnapper.md) topic for more information on the various round modes.

The format is used to convert `double` values to a string using the `String.Format` method, which uses the associated value as the only input parameter (e.g. `{0}`).

## Scale Placement

Tick marks and tick labels are positioned relative to the scale bar defined by the associated [LinearScale](xref:ActiproSoftware.Windows.Controls.Gauge.LinearScale) element. By default, the ticks will be overlayed and centered on the scale bar. The placement of the ticks can be altered using the [ScalePlacement](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickBase.ScalePlacement) and [ScaleOffset](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickBase.ScaleOffset) properties.

There are three possible values for the [ScalePlacement](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickBase.ScalePlacement) property:

| Value | Description |
|-----|-----|
| `Inside` | Indicates that the tick will be placed below, when oriented horizontally, or to the left, when oriented vertically, of the scale bar. The outer edge of the tick will be aligned with the inner edge of the scale bar. |
| `Outside` | Indicates that the tick will be placed above, when oriented horizontally, or to the right, when oriented vertically, of the scale bar. The inner edge of the tick will be aligned with the outer edge of the scale bar. |
| `Overlay` | Indicates that the tick will be centered over (or on top) of the scale bar. The center line of the tick will be aligned with the center line of the scale bar. |

In addition to the placement, the [ScaleOffset](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.TickBase.ScaleOffset) can be used to further customize the location of the ticks.
