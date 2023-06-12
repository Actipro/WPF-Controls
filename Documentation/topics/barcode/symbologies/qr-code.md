---
title: "QR Code"
page-title: "QR Code - Bar Code Symbologies"
order: 2
---
# QR Code

QR Code is a 2D symbology, originally used in automotive manufacturing, that is now used worldwide for a wide variety of purposes.

It is readable by most mobile devices with cameras and can be used to display text to a user, compose a message, and much more.

![Screenshot](../images/symbology-qr-code.png)

*A sample of this symbology*

This symbology can encode up to 7,089 numeric characters, 4,296 alpha numeric characters, or 2,953 bytes.  Encoding modes are automatically switched when it is most efficient to do so.  This symbology implementation also auto-calculates the QR Code symbol version and inserts error correction codewords.

> [!IMPORTANT]
> When using larger version numbers (i.e., version 25 and up), it may be necessary to lower the default cell size in order for QR code readers to properly read the value.

## Symbology Characteristics

The following table gives an overview of the characteristics of the symbology.

| Item | Details |
|-----|-----|
| Implementation Class | [QrCodeSymbology](xref:@ActiproUIRoot.Controls.BarCode.QrCodeSymbology) |
| Base Class | [Grid2DBarCodeSymbology](xref:@ActiproUIRoot.Controls.BarCode.Grid2DBarCodeSymbology) |
| Encodable Characters | Number characters, alphanumeric characters, and bytes. |
| Supports Error Correction | Yes.  The implementation will auto-calculate and insert error correction data. |
| Fixed Length Requirements | No.  The maximum length of the value varies depending on the type of data being encoded, the symbol version selected, and the error correction level selected. |

## Important Members

This symbology has these important members:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[AllowLowercase](xref:@ActiproUIRoot.Controls.BarCode.QrCodeSymbology.AllowLowercase) Property

</td>
<td>

Gets or sets a value indicating whether lowercase characters are allowed.

The QR code specification does not allow lowercase characters when using the alphanumeric encoding mode.  Therefore, when allowing lowercase characters the binary encoding mode must be used.

</td>
</tr>

<tr>
<td>

[Background](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Background) Property

</td>
<td>

Gets or sets the `Brush` to use for rendering the background.

</td>
</tr>

<tr>
<td>

[CellSize](xref:@ActiproUIRoot.Controls.BarCode.Grid2DBarCodeSymbology.CellSize) Property

</td>
<td>Gets or sets the size in pixels of each cell.</td>
</tr>

<tr>
<td>

[DisplayName](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.DisplayName) Property

</td>
<td>Gets the display name of the symbology.</td>
</tr>

<tr>
<td>

[EncodingMode](xref:@ActiproUIRoot.Controls.BarCode.QrCodeSymbology.EncodingMode) Property

</td>
<td>

Gets the mode in which the value will be encoded. The default value is `null`, which indicates the encoding mode will be auto-selected based on the specified value.

</td>
</tr>

<tr>
<td>

[ErrorCorrectionLevel](xref:@ActiproUIRoot.Controls.BarCode.QrCodeSymbology.ErrorCorrectionLevel) Property

</td>
<td>Gets the level of error correction that will be added to the encoded data. The higher the error correction level is, the lower the quantity of data that can be encoded is.</td>
</tr>

<tr>
<td>

[Foreground](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Foreground) Property

</td>
<td>

Gets or sets the `Brush` to use for rendering the foreground.

</td>
</tr>

<tr>
<td>

[MaskIndex](xref:@ActiproUIRoot.Controls.BarCode.QrCodeSymbology.MaskIndex) Property

</td>
<td>

Gets or sets the QR mask applied to the code. The default value is `null`, which indicates the mask will be auto-selected based on the specified value.

</td>
</tr>

<tr>
<td>

[MeasureDesiredSize](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.MeasureDesiredSize*) Method

</td>
<td>Measures the desired size of the symbology, based on the specified available size.</td>
</tr>

<tr>
<td>

[Render](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Render*) Method

</td>
<td>

Renders the symbology to the specified `DrawingContext`.

</td>
</tr>

<tr>
<td>

[ToBitmap](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.ToBitmap*) Method

</td>
<td>

Creates a `BitmapSource` based on the contents of the symbology.

</td>
</tr>

<tr>
<td>

[ValidateValue](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.ValidateValue*) Method

</td>
<td>Validates that the symbology can parse the specified value.</td>
</tr>

<tr>
<td>

[Value](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Value) Property

</td>
<td>Gets or sets the value to encode in the bar code.</td>
</tr>

<tr>
<td>

[QuietZoneThickness](xref:@ActiproUIRoot.Controls.BarCode.Grid2DBarCodeSymbology.QuietZoneThickness) Property

</td>
<td>Gets the thickness of the margin on each side of the rendered symbol.</td>
</tr>

<tr>
<td>

[Version](xref:@ActiproUIRoot.Controls.BarCode.QrCodeSymbology.Version) Property

</td>
<td>

Gets the symbol version that should be rendered. The default value is `null`, which indicates the version will be auto-selected based on the specified value.

</td>
</tr>

</tbody>
</table>

## Sample XAML

This sample XAML code shows how to create a [BarCode](xref:@ActiproUIRoot.Controls.BarCode.BarCode) using this symbology.

```xaml
<barCode:BarCode>
	<barCode:QrCodeSymbology Value="0123456789" />
</barCode:BarCode>
```

## Trademark

QR Code is a registered trademark of DENSO WAVE INCORPORATED.  The trademark only applies to the word "QR Code", and not for any QR Code pattern (image).
