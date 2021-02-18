---
title: "BarCode Basics"
page-title: "BarCode Basics - Bar Code Reference"
order: 4
---
# BarCode Basics

The [BarCode](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode) control itself has several options like borders and a caption that can be used to wrap its symbology presentation.

## Border Around the Bar Code

The [BarCode](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode) inherits `Control` and therefore has several border-related properties such as `BorderBrush` and `BorderThickness`.  Set those properties to add a border around the bar code.

We also have added a [CornerRadius](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode.CornerRadius) property that can be used to round off one or more of the corners.  It accepts a standard `CornerRadius`.

## Caption Above the Bar Code

Normally the bar code's value is displayed below the bar code.  You can also display content above the bar code.

For instance, perhaps you wish to show some text indicating what the bar code is for.

The [Caption](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode.Caption) property can be set to add a caption.  The property accepts any object so you can use text or another `UIElement` and it will be centered above the bar code, but within the bar code's outer border, if any.

## Setting a Symbology

Each bar code symbology is implemented in a separate .NET type.  For instance, `Code 39` is implemented by the [Code39Symbology](xref:ActiproSoftware.Windows.Controls.BarCode.Code39Symbology) class.

Set the symbology that you would like to use for your bar code to the [BarCode](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode).[Symbology](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode.Symbology) property.

See the [Symbologies](symbologies/index.md) topic for a list of the available symbologies.

## Setting the Value to Encode

You set the value to encode directly on the [BarCodeSymbology](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology) class instance that you assign to the [BarCode](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode).  This is done via the [Value](xref:ActiproSoftware.Windows.Controls.BarCode.BarCodeSymbology.Value) property.

## Rotating or Scaling the Bar Code

Since [BarCode](xref:ActiproSoftware.Windows.Controls.BarCode.BarCode) is a `Control`, you can apply `RotateTransform` and `ScaleTransform` objects to its `LayoutTransform` and `RenderTransform` properties.

## Changing Symbology Options

Each symbology has its own options.  Although many share some common options such as [BarHeight](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.BarHeight) or [ValueDisplayStyle](xref:ActiproSoftware.Windows.Controls.BarCode.LinearBarCodeSymbology.ValueDisplayStyle), others like [Code39Symbology](xref:ActiproSoftware.Windows.Controls.BarCode.Code39Symbology) have extended options such as [AreStartStopCharactersVisible](xref:ActiproSoftware.Windows.Controls.BarCode.Code39Symbology.AreStartStopCharactersVisible) and [IsChecksumEnabled](xref:ActiproSoftware.Windows.Controls.BarCode.Code39Symbology.IsChecksumEnabled).

See the documentation for each [symbology](symbologies/index.md) to learn what options are available.
