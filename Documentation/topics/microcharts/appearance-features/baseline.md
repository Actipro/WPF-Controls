---
title: "Baseline"
page-title: "Baseline - Micro Charts Appearance Features"
order: 2
---
# Baseline

A baseline value can be used to alter the look of area, bar, and win/loss series, and can be optionally displayed on the chart.

## Baseline Value

The baseline value determines what the chart considers "positive" and "negative" values.  Any value greater than or equal to the baseline value is considered positive, and any value less than the baseline value is considered negative.

The baseline value can be customized using the [MicroXYChart](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart).[AxisBaselineValue](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart.AxisBaselineValue) property, which defaults to `0` for numeric values and `DateTime.MinValue` for date/time.

This positive/negative distinction can affect which style is used for various elements in the chart.  For example, when enabled the [MarkerNegativeStyle](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.MarkerNegativeStyle) style will be used for any markers below the baseline value, while values above it will use the [MarkerStyle](xref:ActiproSoftware.Windows.Controls.MicroCharts.Primitives.MicroXYSeriesBase.MarkerStyle) style.

In addition, area, bar, and win/loss series will pivot their polygons around the baseline value, which alters their appearance.

In the following examples, the data rendered by the area chart uses the index postion along the X-axis and the fixed values `10, 20, 10, 20, 10, 20, 10` for the Y-axis.  The first image shows the series using `10` as the minimum value along the Y-axis, and `20` for the maximum.  Since our baseline is `0` (the default), the polygon is not pivoted.

![Screenshot](../images/micro-area-series-style1.png)

If we explicitly set our baseline value to `15`, then any values below the baseline will be considered "negative" and the polyon will appear to pivot around the baseline.

![Screenshot](../images/micro-area-series-style2.png)

Finally, if we enable the use of negative styles then the portions of the polygon below the baseline use a distinct style.

![Screenshot](../images/micro-area-series-style3.png)

## Baseline Visibility and Style

An actual line can be rendered at the specified baseline value to highlight its location in the chart.  The visibility of the line is configured using the [IsAxisBaselineVisible](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart.IsAxisBaselineVisible) property, which is `false` by default.

For example, the following line series uses the same fixed values from above (`10, 20, 10, 20, 10, 20, 10`) and a baseline value of `15`.  In this particular example, since the line is rendered with a single color, it is not possible to visually determine what is considered above or below the baseline.

![Screenshot](../images/baseline1.png)

If we set [IsAxisBaselineVisible](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart.IsAxisBaselineVisible) to `True`, then we can easily determine this information.

![Screenshot](../images/baseline2.png)

The style of the line can be customized using the [AxisBaselineStyle](xref:ActiproSoftware.Windows.Controls.MicroCharts.MicroXYChart.AxisBaselineStyle) property.  The associated `Style` should target the `Shape` type, which includes properties such as `Stroke` and `StrokeThickness`.
