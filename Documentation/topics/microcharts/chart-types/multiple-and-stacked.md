---
title: "Multiple and Stacked Series"
page-title: "Multiple and Stacked Series - Micro Chart Types"
order: 7
---
# Multiple and Stacked Series

The chart control supports multiple series which can be optionally stacked on top of the previous series.

## Multiple Series

Any number of series can be added to the chart, which will simply render one on top of each (in z-order) Simply add the series to the [MicroXYChart](xref:@ActiproUIRoot.Controls.MicroCharts.MicroXYChart).[Series](xref:@ActiproUIRoot.Controls.MicroCharts.MicroXYChart.Series) collection that you would like to be rendered.

In this example, we display a bar and line series in the same chart:

```xaml
<microcharts:MicroXYChart Width="100" Height="18" YAxisMinimum="0">
	<microcharts:MicroBarSeries ItemsSource="4;11;3;1;8;9;14" />
	<microcharts:MicroLineSeries ItemsSource="7;3;5;1;1;3;10" />
</microcharts:MicroXYChart>
```

This results in the following chart:

![Screenshot](../images/multiple-series1.png)

The chart will automatically align the multiple series along the secondary axis (X in this example).  So, if the series have data with disparate values, then it will automatically adjust to fit the data.

In this example, we add additional data points to the line series:

```xaml
<microcharts:MicroXYChart Width="100" Height="18" YAxisMinimum="0">
	<microcharts:MicroBarSeries ItemsSource="4;11;3;1;8;9;14" />
	<microcharts:MicroLineSeries ItemsSource="7;3;5;1;1;3;10;5;2;8;1;14" />
</microcharts:MicroXYChart>
```

This results in the following chart, which has extended the line series beyond the bar series since it has more data points:

![Screenshot](../images/multiple-series2.png)

## Normal Stacking

Two or more series can be stacked one on top of the other to convey cumlative values to the end user.

By setting [StackKind](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.StackKind) on the series to `Normal`, the values of each series will be added to the previous stacked series.

![Screenshot](../images/micro-area-series-stacked-normal.png)

In this sample the second series will be stacked on top of the first series.

```xaml
<microcharts:MicroXYChart Width="100" Height="18" >
	<microcharts:MicroAreaSeries ItemsSource="{Binding Data1}" StackKind="Normal" />
	<microcharts:MicroAreaSeries ItemsSource="{Binding Data2}" StackKind="Normal" />
</microcharts:MicroXYChart>
```

## Percentage Stacking

Two or more area series can be stacked one on top of the other to convey proportional values to the end user.  In this mode, the series render to fill the chart height.  At any given location, the height of each rendered series indicates its value percentage relative to the total.

By setting [StackKind](xref:@ActiproUIRoot.Controls.MicroCharts.Primitives.MicroXYSeriesBase.StackKind) on the series to `Percentage`, the values of each series will be added to the previous stacked series and a percentage of the total value will be used.

![Screenshot](../images/micro-area-series-stacked100.png)

In this sample the second series will be stacked on top of the first series.

```xaml
<microcharts:MicroXYChart Width="100" Height="18" >
	<microcharts:MicroAreaSeries ItemsSource="{Binding Data1}" StackKind="Percentage" />
	<microcharts:MicroAreaSeries ItemsSource="{Binding Data2}" StackKind="Percentage" />
</microcharts:MicroXYChart>
```
