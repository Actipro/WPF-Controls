---
title: "Grid Lines and Stripes"
page-title: "Grid Lines and Stripes - XY Chart Features"
order: 7
---
# Grid Lines and Stripes

Grid lines and stripes appear behind the chart data and allow the user to easily compare data against axis values.

## Line Visibility

Visibility of the grid lines is controlled using [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridLineMajorVisibility](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridLineMajorVisibility) and [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridLineMinorVisibility](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridLineMinorVisibility).  These properties can be set to [None](xref:@ActiproUIRoot.Controls.Charts.GridLineVisibility.None), [All](xref:@ActiproUIRoot.Controls.Charts.GridLineVisibility.All), [X](xref:@ActiproUIRoot.Controls.Charts.GridLineVisibility.X), or [Y](xref:@ActiproUIRoot.Controls.Charts.GridLineVisibility.Y).

![Screenshot](../images/appearance-grid-lines1.png)

This is an example of setting `GridLineMajorVisibility` to `Y`, in which grid lines are shown for each major tick on the Y axis.

```xaml
<charts:XYChart GridLineVisibility="Y" .../>
```

![Screenshot](../images/appearance-grid-lines2.png)

This is an example of setting `GridLineMajorVisibility` to `X`, in which grid lines are shown for each major tick on the X axis.

```xaml
<charts:XYChart GridLineVisibility="X" .../>
```

![Screenshot](../images/appearance-grid-lines3.png)

This is an example of setting `GridLineMajorVisibility` to `All`, in which grid lines are shown for each major tick on the X and Y axis.

```xaml
<charts:XYChart GridLineVisibility="All" .../>
```

## Stripe Visibility

Visibility of the grid stripes is controlled using [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridStripeVisibility](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridStripeVisibility).  This property can be set to [None](xref:@ActiproUIRoot.Controls.Charts.GridStripeVisibility.None), [X](xref:@ActiproUIRoot.Controls.Charts.GridStripeVisibility.X), or [Y](xref:@ActiproUIRoot.Controls.Charts.GridStripeVisibility.Y).

![Screenshot](../images/appearance-grid-lines4.png)

This is an example of setting `GridLineMajorVisibility` to `Y`, in which grid stripes are shown alternating between each major tick on the Y axis.

```xaml
<charts:XYChart GridLineVisibility="Y" .../>
```

![Screenshot](../images/appearance-grid-lines5.png)

This is an example of setting `GridLineMajorVisibility` to `X`, in which grid stripes are shown alternating between each major tick on the X axis.

```xaml
<charts:XYChart GridLineVisibility="X" .../>
```

## Line Customization

The color of major and minor grid lines can be controlled using [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridLineMajorBrush](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridLineMajorBrush) and [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridLineMinorBrush](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridLineMinorBrush), respectively.

![Screenshot](../images/appearance-grid-lines6.png)

This is an example of setting `GridLineMajorBrush` and `GridLineMinorBrush` to custom colors.

```xaml
<charts:XYChart .... GridLineMajorBrush="#338833" GridLineMinorBrush="#33bb33">
```

## Stripe Customization

The color of grid stripes and alternating grid stripes can be controlled using [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridStripeBrush](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridStripeBrush) and [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart).[GridStripeAlternatingBrush](xref:@ActiproUIRoot.Controls.Charts.XYChart.GridStripeAlternatingBrush).

![Screenshot](../images/appearance-grid-lines7.png)

This is an example of setting `GridStripeBrush` and `GridStripeAlternatingBrush` to custom colors.

```xaml
<charts:XYChart .... GridStripeBrush="#55338833" GridStripeAlternatingBrush="#2233bb33">
```
