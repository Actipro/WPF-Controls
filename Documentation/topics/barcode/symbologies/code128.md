---
title: "Code 128"
page-title: "Code 128 - Bar Code Symbologies"
order: 12
---
# Code 128

Code 128 is a high-density barcode symbology, used extensively worldwide in shipping and packaging industries.

It is especially efficient when encoding sequential numbers since they are merged into a single output character width.

![Screenshot](../images/symbology-code128.gif)

*A sample of this symbology*

## Symbology Characteristics

The following table gives an overview of the characteristics of the symbology.

| Item | Details |
|-----|-----|
| Implementation Class | [Code128Symbology](xref:ActiproSoftware.Windows.Controls.BarCode.Code128Symbology) |
| Base Class | [LinearBarCodeSymbology](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology) |
| Related ValidationRule Class | [Code128ValidationRule](xref:ActiproSoftware.Windows.Controls.BarCode.ValidationRules.Code128ValidationRule) |
| Encodable Characters | All lower 128 ASCII characters. |
| Supports Checksum | Yes.  This symbology implementation auto-calculates and inserts a check character. |
| Has Special Start/Stop Characters | No. |
| Fixed Length Requirements | No.  The symbology value may be any number of characters. |

## Important Members

This symbology has these important members:

| Member | Description |
|-----|-----|
| [Background](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Background) Property | Gets or sets the `Brush` to use for rendering the background. |
| [BarHeight](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.BarHeight) Property | Gets or sets the desired height of the bars. |
| [BarWidthRatio](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.BarWidthRatio) Property | Gets or sets the width ratio of wide lines to narrow lines. |
| [DisplayName](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.DisplayName) Property | Gets the display name of the symbology. |
| [DisplayValue](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.DisplayValue) Property | Gets or sets the value that is displayed if [ValueDisplayStyle](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle) is not `None`. |
| [Foreground](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Foreground) Property | Gets or sets the `Brush` to use for rendering the foreground. |
| [MeasureDesiredSize](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.MeasureDesiredSize*) Method | Measures the desired size of the symbology, based on the specified available size. |
| [MinBarHeightWidthRatio](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.MinBarHeightWidthRatio) Property | Gets or sets the minimum ratio that the height of the bar code must be in relation to its width. |
| [QuietZoneThickness](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.QuietZoneThickness) Property | Gets or sets the `Thickness` of the quiet zone. |
| [Render](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Render*) Method | Renders the symbology to the specified `DrawingContext`. |
| [ToBitmap](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.ToBitmap*) Method | Creates a `BitmapSource` based on the contents of the symbology. |
| [ValidateValue](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.ValidateValue*) Method | Validates that the symbology can parse the specified value. |
| [Value](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Value) Property | Gets or sets the value to encode in the bar code. |
| [ValueDisplayStyle](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle) Property | Gets or sets a [LinearBarCodeValueDisplayStyle](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeValueDisplayStyle) that indicates how the value should rendered. |
| [ValueIntrusionOffset](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.ValueIntrusionOffset) Property | Gets or sets the distance that the [Value](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Value) text intrudes into the bar code when [ValueDisplayStyle](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle) is not `None`. |

## Sample XAML

This sample XAML code shows how to create a [BarCode](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode) using this symbology.

```xaml
<barCode:BarCode>
	<barCode:Code128Symbology Value="ABC-123" />
</barCode:BarCode>
```
