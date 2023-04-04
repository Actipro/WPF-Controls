---
title: "UPC-A"
page-title: "UPC-A - Bar Code Symbologies"
order: 5
---
# UPC-A

UPC-A is a linear symbology, widely used in the United States and Canada for tracking trade items in stores.

This symbology is also known as Universal Product Code version A.

![Screenshot](../images/symbology-upc-a.gif)

*A sample of this symbology*

The 13-character value is divided into four parts: system code, manufacturer code, product code, and check digit.

## Symbology Characteristics

The following table gives an overview of the characteristics of the symbology.

| Item | Details |
|-----|-----|
| Implementation Class | [UpcASymbology](xref:@ActiproUIRoot.Controls.BarCode.UpcASymbology) |
| Base Class | [LinearBarCodeSymbology](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology) |
| Related `ValidationRule` Class | [UpcAValidationRule](xref:@ActiproUIRoot.Controls.BarCode.ValidationRules.UpcAValidationRule) |
| Encodable Characters | Number characters. |
| Supports Checksum | Yes.  This symbology implementation auto-calculates and inserts a check digit. |
| Has Special Start/Stop Characters | No. |
| Fixed Length Requirements | The value must be 11 characters with a 12th character, the check digit, automatically calculated and added by the symbology. |

## Important Members

This symbology has these important members:

| Member | Description |
|-----|-----|
| [Background](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Background) Property | Gets or sets the `Brush` to use for rendering the background. |
| [BarHeight](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.BarHeight) Property | Gets or sets the desired height of the bars. |
| [BarWidthRatio](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.BarWidthRatio) Property | Gets or sets the width ratio of wide lines to narrow lines. |
| [DisplayName](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.DisplayName) Property | Gets the display name of the symbology. |
| [DisplayValue](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.DisplayValue) Property | Gets or sets the value that is displayed if [ValueDisplayStyle](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle) is not `None`. |
| [Foreground](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Foreground) Property | Gets or sets the `Brush` to use for rendering the foreground. |
| [MeasureDesiredSize](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.MeasureDesiredSize*) Method | Measures the desired size of the symbology, based on the specified available size. |
| [MinBarHeightWidthRatio](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.MinBarHeightWidthRatio) Property | Gets or sets the minimum ratio that the height of the bar code must be in relation to its width. |
| [QuietZoneThickness](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.QuietZoneThickness) Property | Gets or sets the `Thickness` of the quiet zone. |
| [Render](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Render*) Method | Renders the symbology to the specified `DrawingContext`. |
| [ToBitmap](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.ToBitmap*) Method | Creates a `BitmapSource` based on the contents of the symbology. |
| [ValidateValue](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.ValidateValue*) Method | Validates that the symbology can parse the specified value. |
| [Value](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Value) Property | Gets or sets the value to encode in the bar code. |
| [ValueDisplayStyle](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle) Property | Gets or sets a [LinearBarCodeValueDisplayStyle](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeValueDisplayStyle) that indicates how the value should rendered. |
| [ValueIntrusionOffset](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.ValueIntrusionOffset) Property | Gets or sets the distance that the [Value](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Value) text intrudes into the bar code when [ValueDisplayStyle](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle) is not `None`. |

## Sample XAML

This sample XAML code shows how to create a [BarCode](xref:@ActiproUIRoot.Controls.BarCode.BarCode) using this symbology.

```xaml
<barCode:BarCode>
	<barCode:UpcASymbology Value="03600029145" />
</barCode:BarCode>
```
