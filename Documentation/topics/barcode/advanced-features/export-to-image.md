---
title: "Export to Image"
page-title: "Export to Image - Bar Code Advanced Features"
order: 3
---
# Export to Image

Bar codes can easily be exported to any image format supported by WPF, including PNG, BMP, GIF, JPEG, and TIFF image formats.

It's easy to generate a `BitmapSource` from a symbology, simply by calling its [ToBitmap](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.ToBitmap*) method.  When you have an image representation of a bar code, you can save it to a file or paste the image in other applications for usage.

## Sample Code

This sample code creates a `BitmapSource` from a Code 39 extended bar code.

```csharp
Code39ExtendedSymbology symbology = new Code39ExtendedSymbology();
symbology.Value = "ABC-123";
BitmapSource source = symbology.ToBitmap(96, 96);
```

This sample code takes the `BitmapSource` from the previous sample code and writes it to a PNG file whose path is assumed to be in `outputPath`.

```csharp
FileStream outStream = new FileStream(outputPath, FileMode.Create);
PngBitmapEncoder enc = new PngBitmapEncoder();
enc.Frames.Add(BitmapFrame.Create((BitmapSource)outputImage.Source));
enc.Save(outStream);
outStream.Dispose();
```
