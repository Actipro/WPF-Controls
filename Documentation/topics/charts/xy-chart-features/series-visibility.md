---
title: "Series Visibility"
page-title: "Series Visibility - XY Chart Features"
order: 12
---
# Series Visibility

The visibility of any series can be set to dictate whether or not it is rendered on the chart.

## Usage

The visibility of a series can be changed in order to create the ability to show/hide the series on the chart.  With this feature several series can be contained within a chart then their visibilities can be manipulated to better compare just a couple at a time.  The visibility of a series can be customized with the [SeriesBase](xref:ActiproSoftware.Windows.Controls.Charts.Primitives.SeriesBase).`Visibility` property, which defaults to `Visible`.

In this example, the visibility property of the line series is bound to the IsChecked property of the checkbox so that the series is shown when checked and not shown when unchecked:

```xaml
<charts:XYChart ...>
	<charts:BarSeries Description="New York" ... />
	<charts:LineSeries Description="Los Angeles" Visibility="{Binding IsChecked, ElementName=seriesVisibleCheckBox, Converter={StaticResource BooleanToVisibilityConverter}}" .../>
</charts:XYChart>
...
<CheckBox x:Name="seriesVisibleCheckBox" Content="Los Angeles series visible" IsChecked="True"/>
```
