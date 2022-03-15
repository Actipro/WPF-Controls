---
title: "Series Visibility"
page-title: "Series Visibility - Micro Charts Appearance Features"
order: 6
---
# Series Visibility

The visibility of any series can be set to dictate whether or not it is rendered on the chart.

## Usage

The visibility of a series can be changed in order to create the ability to show/hide the series on the chart.  With this feature several series can be contained within a chart then their visibilities can be manipulated to better compare just a couple at a time.  The visibility of a series can be customized with the [MicroSeriesBase](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroSeriesBase).`Visibility` property, which defaults to `Visible`.

In this example, the visibility property of the line series is bound to the IsChecked property of the checkbox so that the series is shown when checked and not shown when unchecked:

```xaml
<microcharts:MicroXYChart Width="100" Height="18" IsHotTrackingEnabled="True">
	<microcharts:MicroBarSeries Description="New York" ItemsSource="581415; 591966; 492003; 460123; 523962; 622962; 649196; 789123; 800124; 750126; 741612; 720556" LegendStringFormat="{}{5}: {0:C0}"/>
	<microcharts:MicroLineSeries Description="Los Angeles" ItemsSource="318624; 358185; 381725; 310128; 251929; 370125; 380120; 354501; 263105; 492031; 423123; 380125" LegendStringFormat="{}{5}: {0:C0}"
		Visibility="{Binding IsChecked, ElementName=seriesVisibleCheckBox, Converter={StaticResource BooleanToVisibilityConverter}}"/>
</microcharts:MicroXYChart>
...
<CheckBox x:Name="seriesVisibleCheckBox" Content="Los Angeles series visible" IsChecked="True"/>
```
