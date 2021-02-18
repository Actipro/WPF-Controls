---
title: "Donut Chart"
page-title: "Donut Chart - Pie Chart Features"
order: 4
---
# Donut Chart

Donut charts act just like pie charts, except for an adjustable donut hole in the center of the chart. [DonutChart](xref:ActiproSoftware.Windows.Controls.Charts.DonutChart) is a subclass of [PieChart](xref:ActiproSoftware.Windows.Controls.Charts.PieChart), so all pie chart documentation also applies to donut chart.

## Specifying Hole Radius

The hole radius of a donut chart can be specified by changing [DonutChart](xref:ActiproSoftware.Windows.Controls.Charts.DonutChart).[HoleRadiusPercentage](xref:ActiproSoftware.Windows.Controls.Charts.DonutChart.HoleRadiusPercentage).  HoleRadiusPercentage determines what percentage of the DonutChart is consumed by the donut hole, from `0.0` (0%) to `1.0` (100%).

A donut chart with a donut hole radius at 80%:

```xaml
<charts:DonutChart HoleRadiusPercentage="0.8"/>
```

![Screenshot](../images/pie-donut1.png)
