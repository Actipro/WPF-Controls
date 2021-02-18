---
title: "EAN-13"
page-title: "EAN-13 - Bar Code Symbologies"
order: 3
---
# EAN-13

EAN-13 is a linear symbology, based on the original 12-digit Universal Product Code (UPC) system developed in North America.

EAN-13 bar codes are used worldwide for marking retail goods.

![Screenshot](../images/symbology-ean13.gif)

*A sample of this symbology*

The 13 character value is divided into four parts: system code, manufacturer code, product code, and check digit.

## Symbology Characteristics

The following table gives an overview of the characteristics of the symbology.

| Item | Details |
|-----|-----|
| Implementation Class | [Ean13Symbology](xref:ActiproSoftware.Windows.Controls.BarCode.Ean13Symbology) |
| Base Class | [LinearBarCodeSymbology](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology) |
| Related ValidationRule Class | [Ean13ValidationRule](xref:ActiproSoftware.Windows.Controls.BarCode.ValidationRules.Ean13ValidationRule) |
| Encodable Characters | Number characters. |
| Supports Checksum | Yes.  This symbology implementation auto-calculates and inserts a check digit. |
| Has Special Start/Stop Characters | No. |
| Fixed Length Requirements | The value must be 12 characters with a 13th character, the check digit, automatically calculated and added by the symbology. |

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
	<barCode:Ean13Symbology Value="003600029145" />
</barCode:BarCode>
```
