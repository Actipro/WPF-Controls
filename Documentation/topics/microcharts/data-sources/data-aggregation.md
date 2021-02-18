---
title: "Data Aggregation"
page-title: "Data Aggregation - Micro Charts Data Sources"
order: 3
---
# Data Aggregation

The chart control supports aggregating its data to reduce the number of data points displayed.

## Aggregation Factor

In order to increase performance, the chart control will automatically determine the optimal number of data points to display based on the size of the control.  This is determined using the [AggregationFactor](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.AggregationFactor) property, which defaults to `0.2`.

For example, if the chart control is `100` pixels wide and uses an aggregation factor of `0.1`, then the optimal number of data points would be `10`.  If the data source contained `1000` items, then the chart would create `10` groups, each with `100` items in it.  Then some sort of aggregation function would be performed on each group to generate a single data point for display.

![Screenshot](../images/data-aggregation-none.png)![Screenshot](../images/data-aggregation-average10.png)![Screenshot](../images/data-aggregation-average50.png)

*A line series with markers visible with no aggregation, 10% aggregation, and 50% aggregation*

Aggregation is enabled by default and can be disabled by setting [IsAggregationEnabled](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.IsAggregationEnabled) to `false`.

## Aggregation Kinds

There are various aggregation functions that can be performed on the items in a group.  The supported aggregation kinds are specified using the [PrimaryAggregationKind](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.PrimaryAggregationKind) and [SecondaryAggregationKind](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.SecondaryAggregationKind).

The `PrimaryAggregationKind` is typically used for the Y values, but for vertically-oriented series this will be used by the X values.  Similarly the `SecondaryAggregationKind` is used for X values, but will be used by Y values for vertically-oriented series.

The following table lists the supported aggregation kinds:

<table>
<thead>

<tr>
<th>Aggregation Kind</th>
<th>Description</th>
</tr>


</thead>
<tbody>

<tr>
<td>

`Average`

</td>
<td>

![Screenshot](../images/data-aggregation-average10.png)

Averages all the values in the group and uses that as the aggregated value.

</td>
</tr>

<tr>
<td>

`First`

</td>
<td>

![Screenshot](../images/data-aggregation-first10.png)

Uses the first value in the group as the aggregated value.

</td>
</tr>

<tr>
<td>

`Last`

</td>
<td>

![Screenshot](../images/data-aggregation-last10.png)

Uses the last value in the group as the aggregated value.

</td>
</tr>

<tr>
<td>

`Maximum`

</td>
<td>

![Screenshot](../images/data-aggregation-maximum10.png)

Uses the maximum value in the group as the aggregated value.

</td>
</tr>

<tr>
<td>

`Minimum`

</td>
<td>

![Screenshot](../images/data-aggregation-minimum10.png)

Uses the minimum value in the group as the aggregated value.

</td>
</tr>

</tbody>
</table>
