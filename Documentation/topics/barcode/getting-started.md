---
title: "Getting Started"
page-title: "Getting Started - Bar Code Reference"
order: 3
---
# Getting Started

We've engineered bar code to be extremely easy to use.

## Add Assembly References

First, add references to the "ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll" and "ActiproSoftware.BarCode.@@PlatformAssemblySuffix.dll" assemblies.  They should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## Add the BarCode Control

Then find the parent element that will contain the bar code.  This could be a `Window`, a `UserControl`, a `FlowDocument`, a `FixedDocument`, or any other type of WPF `UIElement`.

Next, add a [BarCode](xref:@ActiproUIRoot.Controls.BarCode.BarCode) control to the desired parent element.  In this sample we will add the [BarCode](xref:@ActiproUIRoot.Controls.BarCode.BarCode) to a `FlowDocument`:

```xaml
<FlowDocument 
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:barCode="http://schemas.actiprosoftware.com/winfx/xaml/barcode" 
	FontFamily="Calibri" FontSize="14" TextAlignment="Left"	
	>
	<barCode:BarCode>
	</barCode:BarCode>
</FlowDocument>
```

## Assign a Symbology

Now we need to set the type of symbology that we wish to use.  In this sample we will use Code 39 Extended, which is one of the most common types of bar codes and supports all lower ASCII characters.

```xaml
<FlowDocument 
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:barCode="http://schemas.actiprosoftware.com/winfx/xaml/barcode" 
	FontFamily="Calibri" FontSize="14" TextAlignment="Left"	
	>
	<barCode:BarCode>
		<barCode:Code39ExtendedSymbology Value="123456" BarHeight="25" />
	</barCode:BarCode>
</FlowDocument>
```

Note that we have also set a couple options like the [BarCodeSymbology](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology).[Value](xref:@ActiproUIRoot.Controls.BarCode.BarCodeSymbology.Value) of the symbology (the value that is encoded) and the height of the bars (via [LinearBarCodeSymbology](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology).[BarHeight](xref:@ActiproUIRoot.Controls.BarCode.LinearBarCodeSymbology.BarHeight)).

That's all we need to do!  You can optionally configure many other settings for the bar code and/or its symbology.  See the other topics in this documentation to see which features are available.

## Further Study

It's very easy to use Bar Code and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of Bar Code.

If you require further assistance after looking through those, please visit our support forum for the product.
