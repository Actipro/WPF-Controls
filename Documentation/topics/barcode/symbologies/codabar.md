---
title: "Codabar"
page-title: "Codabar - Bar Code Symbologies"
order: 13
---
# Codabar

Codabar is a linear symbology, developed in 1972 by Pitney Bowes, Inc.

It is most commonly used by libraries, blood banks, and on FedEx airbills.

![Screenshot](../images/symbology-codabar.gif)

*A sample of this symbology*

## Symbology Characteristics

The following table gives an overview of the characteristics of the symbology.

| Item | Details |
|-----|-----|
| Implementation Class | [CodabarSymbology](xref:@ActiproUIRoot.Controls.BarCode.CodabarSymbology) |
| Base Class | [LinearBarCodeSymbology](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology) |
| Related ValidationRule Class | [CodabarValidationRule](xref:@ActiproUIRoot.Controls.BarCode.ValidationRules.CodabarValidationRule) |
| Encodable Characters | Number and `-$:/.+` characters, along with 4 start/stop characters. |
| Supports Checksum | No.  This symbology is self-checking, so no checksum or check digit is required. |
| Has Special Start/Stop Characters | Yes.  Each encoded value must start and stop with `A`, `B`, `C`, or `D`. |
| Fixed Length Requirements | No.  The symbology value may be any number of characters. |

## Important Members

This symbology has these important members:

| Member | Description |
|-----|-----|
| [AreStartStopCharactersVisible](xref:@ActiproUIRoot.Controls.BarCode.CodabarSymbology.AreStartStopCharactersVisible) Property | Gets or sets whether the start/stop characters are visible in the displayed value. |
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
	<barCode:CodabarSymbology Value="A12345678A" />
</barCode:BarCode>
```
