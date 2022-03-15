---
title: "Getting Started"
page-title: "Getting Started - Charts Reference"
order: 2
---
# Getting Started

Getting started with Charts is simple. Follow the steps below to build your first chart.

## Add Assembly References

First, add references to the "ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll" and "ActiproSoftware.Charts.@@PlatformAssemblySuffix.dll" assemblies.  The assemblies should be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## Add the XYChart Control

Then find the parent element that will contain the chart.  This could be a `UserControl` or any other type of `UIElement`.

Next, add a [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart) control to the desired parent element.  In this sample we will add the [XYChart](xref:@ActiproUIRoot.Controls.Charts.XYChart) to a `UserControl`:

```xaml
<UserControl 
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:charts="http://schemas.actiprosoftware.com/winfx/xaml/charts">
	<charts:XYChart Width="300" Height="200"/>
</UserControl>
```

## Add a Series

Now we need to add a series, which renders a single set of data points on the chart.  In this sample we will add a [AreaSeries](xref:@ActiproUIRoot.Controls.Charts.AreaSeries) series, but the other types are added in a similar manner.

```xaml
<UserControl 
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:charts="http://schemas.actiprosoftware.com/winfx/xaml/charts">
	<charts:XYChart Width="300" Height="200">
		<charts:AreaSeries ItemsSource="{Binding}" />
	</charts:XYChart>
</UserControl>
```

Note that this sample assumes that the chart's `DataContext` is a list of numeric (i.e. `Double`, `Decimal`, etc) or `DateTime` objects.  With this configuration, the chart's X values will represent the index value in the list and the chart's Y values will represent the list's actual value at the related list index.

If our list contained custom objects and we wanted to pull the X and Y values from properties on that object, then we would have to specify the property path on the series.

```xaml
<UserControl 
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:charts="http://schemas.actiprosoftware.com/winfx/xaml/charts">
	<charts:XYChart Width="300" Height="200">
		<charts:AreaSeries ItemsSource="{Binding}" XPath="MyDateProperty" YPath="MyNumericProperty" />
	</charts:XYChart>
</UserControl>
```

This sample assumes the objects in the list each contain a property named `MyDateProperty` and `MyNumericProperty`, which are then used for the associated axis.

> [!NOTE]
> If a list contains custom data objects but the index of each entry should be used as the X-values, leave out the `XPath` attribute and just specify the `YPath` to the data object's property that contains the value to display.

Additional series can be added and they will all be rendered on the same chart using the same X and Y scales.

## Further Study

To familiarize yourself with all of the features in Charts, take a look at the feature documentation and also look at the numerous QuickStarts located in the sample project.

If you require further assistance after looking through those, please visit our Charts support forum.
