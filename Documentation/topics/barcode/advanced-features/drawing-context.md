---
title: "Drawing to a DrawingContext"
page-title: "Drawing to a DrawingContext - Bar Code Advanced Features"
order: 2
---
# Drawing to a DrawingContext

A lot of times it is useful to be able to render a bar code directly onto a WPF `DrawingContext`.  This can be done simply by calling the [Render](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Render*) method of the desired symbology class.

## Measuring the Bar Code

You can retrieve a `Size` measurement of the bar code for a symbology by calling its [BarCodeSymbology](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology).[MeasureDesiredSize](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.MeasureDesiredSize*) method.

This sample code measures the bar code symbology in the variable `symbology`.

```csharp
Size desiredSize = symbology.MeasureDesiredSize(new Size(double.PositiveInfinity, double.PositiveInfinity));
```

## Rendering the Bar Code

You can render the bar code for a symbology by calling its [BarCodeSymbology](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology).[Render](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Render*) method and passing it a `DrawingContext`, location and size.

The `Size` should be the same size that is retrieved from the [MeasureDesiredSize](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.MeasureDesiredSize*) method.

This sample code renders the bar code symbology to a `DrawingContext`.

```csharp
symbology.Render(e.DrawingContext, new Point(0, 0), desiredSize);
```
