---
title: "Embedding Controls"
page-title: "Embedding Controls - Gauge Advanced Features"
order: 2
---
# Embedding Controls

Any `UIElement` can be embedded and precisely placed inside any of the gauge controls.

## Adding/Removing UIElements

`UIElement` objects can be added or removed from the gauge controls using the [Items](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.GaugeBase.Items) collection property.

This code shows how a `TextBlock` can be added to a [DigitalGauge](xref:ActiproSoftware.Windows.Controls.Gauge.DigitalGauge):

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:DigitalGauge Width="300" Height="100" Value="Actipro">
	<gauge:DigitalGauge.Items>
		<TextBlock Text="WPF" Foreground="White" />
	</gauge:DigitalGauge.Items>
</gauge:DigitalGauge>
```

## Positioning UIElements

By default, `UIElement` objects will be centered in the associated gauge control, but it's position can be specified using the [XProperty](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.GaugeBase.XProperty), [YProperty](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.GaugeBase.YProperty), [OriginProperty](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.GaugeBase.OriginProperty), and [ZIndexProperty](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.GaugeElement.ZIndexProperty) attached properties.

Continuing from the example above, this code shows how the `TextBlock` can be positioned 10-pixels to the right and 10-pixels down from the top-left corner:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:DigitalGauge Width="300" Height="100" Value="Actipro">
	<gauge:DigitalGauge.Items>
		<TextBlock gauge:DigitalGauge.Origin="TopLeft" gauge:DigitalGauge.X="10" gauge:DigitalGauge.Y="10" Text="WPF" Foreground="White" />
	</gauge:DigitalGauge.Items>
</gauge:DigitalGauge>
```

See the [Coordinate System](../coordinate-system.md) topic for more information on positioning controls, including setting the z-index.

The gauge controls use the `HorizontalAlignment` and `VerticalAlignment` properties of `FrameworkElement`-derived objects to further control the positioning. The tables below describe the behavior used for the various alignment values.

| HorizontalAlignment | Description |
|-----|-----|
| `Center` | Indicates that the center of the `FrameworkElement` will be aligned with the point specified along the X-axis. This is the default alignment and is always used for non-`FrameworkElement`-derived objects. |
| `Left` | Indicates that the left side of the `FrameworkElement` will be aligned with the point specified along the X-axis. |
| `Right` | Indicates that the right side of the `FrameworkElement` will be aligned with the point specified along the X-axis. |
| `Stretch` | See `Center`. |

| VerticalAlignment | Description |
|-----|-----|
| `Bottom` | Indicates that the bottom side of the `FrameworkElement` will be aligned with the point specified along the Y-axis. |
| `Center` | Indicates that the center of the `FrameworkElement` will be aligned with the point specified along the Y-axis. This is the default alignment and is always used for non-`FrameworkElement`-derived objects. |
| `Top` | Indicates that the top side of the `FrameworkElement` will be aligned with the point specified along the Y-axis. |
| `Stretch` | See `Center`. |
