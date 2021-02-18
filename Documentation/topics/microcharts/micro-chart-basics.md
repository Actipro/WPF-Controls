---
title: "Micro Chart Basics"
page-title: "Micro Chart Basics - Micro Charts Reference"
order: 3
---
# Micro Chart Basics

This topic discusses some of the basic concepts to know about using micro charts.

## Primary vs. Secondary Axis

The [MicroXYChart](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart) control supports two axes, X and Y.  The X axis runs horizontally along the control, and the Y axis runs vertically.  All series default to rendering horizontally, which means that Y is the primary axis and X is the secondary axis.

Certain series types, such as `Bar` and `Win/Loss`, support vertical orientations as well as the default horizontal orientation.  With vertically oriented series, X is the primary axis and Y is the secondary axis.

See the [Chart Types](chart-types/index.md) topic for additional information.

## Adding a Series

Each series type is implemented in a separate .NET type.  For instance, a `Line` series is implemented by the [MicroLineSeries](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroLineSeries) class.

Add any series you would like to display to the [MicroXYChart](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart).[Series](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart.Series) collection.  The chart will automatically scale the X and Y axes to fit the data of each series properly.

See the [Chart Types](chart-types/index.md) topic for a list of the available chart types.

## Specifying Items

Each series is bound to it's own data source, which is indicated by the [ItemsSource](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroSeriesBase.ItemsSource) property.  The data source can be a collection of numeric values (i.e. `Double`, `Decimal`, `Int32`, etc), `DateTime` values, or custom objects.

When the data source is a collection of numeric or `DateTime` values, then the series' [XPath](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.XPath) and [YPath](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.YPath) properties should not be set.  The series will automatically use the value from the collection as the data point's primary value (which is typically the `Y` value) and the index in the data source as the secondary value.

When the data source is a collection of custom objects, then the series' [XPath](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.XPath) and [YPath](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.YPath) need to be set appropriately.  The index in the data source can be used as the secondary value (which is typically the `X` value), in which case the associated path property can be left blank.  The path primary value must be specified and must indicate the name of a numeric or date/time property on the custom object.

See the [Specifying Items](data-sources/specifying-items.md) topic for additional information.
