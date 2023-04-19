---
title: "Getting Started"
page-title: "Getting Started - Micro Charts Reference"
order: 2
---
# Getting Started

It's easy to get started using Micro Charts.  Simply follow the steps below to build your first chart.

## Add Assembly References

First, add references to the *ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll* and *ActiproSoftware.MicroCharts.@@PlatformAssemblySuffix.dll* assemblies.  The assemblies should be located in the appropriate *Program Files* folders.  See the product's Readme for details on those locations.

## Add the MicroXYChart Control

Then find the parent element that will contain the micro chart.  This could be a `UserControl` or any other type of `UIElement`.

Next, add a [MicroXYChart](xref:@ActiproUIRoot.Controls.MicroCharts.MicroXYChart) control to the desired parent element.  In this sample we will add the [MicroXYChart](xref:@ActiproUIRoot.Controls.MicroCharts.MicroXYChart) to a `UserControl`:

```xaml
<UserControl
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:microcharts="http://schemas.actiprosoftware.com/winfx/xaml/microcharts"
	>
	<microcharts:MicroXYChart Width="100" Height="18">
	</microcharts:MicroXYChart>
</UserControl>
```

## Add a Series

Now we need to add a series, which renders a single set of data points on the chart.  In this sample we will add a [MicroAreaSeries](xref:@ActiproUIRoot.Controls.MicroCharts.MicroAreaSeries) series, but the other types are added in a similar manner.

```xaml
<UserControl
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:microcharts="http://schemas.actiprosoftware.com/winfx/xaml/microcharts"
	>
	<microcharts:MicroXYChart Width="100" Height="18">
		<microcharts:MicroAreaSeries ItemsSource="{Binding}" />
	</microcharts:MicroXYChart>
</UserControl>
```

Note that this sample assumes that the chart's `DataContext` is a list of numeric (i.e., `Double`, `Decimal`, etc) or `DateTime` objects.  With this configuration, the chart's X values will represent the index value in the list and the chart's Y values will represent the list's actual value at the related list index.

If our list contained custom objects and we wanted to pull the X and Y values from properties on that object, then we would have to specify the property path on the series.

```xaml
<UserControl
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:microcharts="http://schemas.actiprosoftware.com/winfx/xaml/microcharts"
	>
	<microcharts:MicroXYChart Width="100" Height="18">
		<microcharts:MicroAreaSeries ItemsSource="{Binding}" XPath="MyDateProperty" YPath="MyNumericProperty" />
	</microcharts:MicroXYChart>
</UserControl>
```

This sample assumes the objects in the list each contain a property named `MyDateProperty` and `MyNumericProperty`, which are then used for the associated axis.

> [!NOTE]
> If a list contains custom data objects but the index of each entry should be used as the X-values, leave out the `XPath` attribute and just specify the `YPath` to the data object's property that contains the value to display.

Additional series can be added, and they will all be rendered on the same chart using the same X and Y scales.

## Further Study

It's very easy to use Micro Charts and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough, and the sample project demonstrates almost every feature of Micro Charts.

If you require further assistance after looking through those, please visit our support forum for the product.
