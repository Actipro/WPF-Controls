---
title: "UPC-E"
page-title: "UPC-E - Bar Code Symbologies"
order: 6
---
# UPC-E

UPC-E is a linear symbology, used on smaller retail packages where UPC-A bar codes don't fit.

This symbology is also known as Universal Product Code version E.

![Screenshot](../images/symbology-upc-e.gif)

*A sample of this symbology*

The 8 character value is divided into three parts: system code, product code, and check digit.

## Symbology Characteristics

The following table gives an overview of the characteristics of the symbology.

| Item | Details |
|-----|-----|
| Implementation Class | [UpcESymbology](xref:ActiproSoftware.Windows.Controls.BarCode.UpcESymbology) |
| Base Class | [LinearBarCodeSymbology](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology) |
| Related ValidationRule Class | [UpcEValidationRule](xref:ActiproSoftware.Windows.Controls.BarCode.ValidationRules.UpcEValidationRule) |
| Encodable Characters | Number characters. |
| Supports Checksum | No.  This symbology uses a check digit, but it is not currently calculated automatically. |
| Has Special Start/Stop Characters | No. |
| Fixed Length Requirements | The value must be 8 characters, including the developer-calculated check digit. |

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
	<barCode:UpcESymbology Value="04252614" />
</barCode:BarCode>
```
