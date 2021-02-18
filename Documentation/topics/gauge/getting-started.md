---
title: "Getting Started"
page-title: "Getting Started - Gauge Reference"
order: 2
---
# Getting Started

Getting up and running with Gauge controls is extremely easy, especially with the samples we include.

This topic's information will assume you are using Visual Studio to write your XAML code for a WPF `Window` that will contain one of the Gauge controls.

## Read the Documentation Topics

This documentation file contains a lot of information about using Actipro Gauge and its related controls to its fullest.  Whenever you have specific questions about a feature, please read through its documentation topic to search for answers.

## Add Assembly References

First, add references to the `ActiproSoftware.Shared.Wpf.dll` and `ActiproSoftware.Gauge.Wpf.dll` assemblies.  They should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## Getting Started with CircularGauge

This code shows the base XAML that you can use to create a simple [CircularGauge](xref:ActiproSoftware.Windows.Controls.Gauge.CircularGauge) with a few gauge elements:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:CircularGauge Width="200" Height="200" Radius="100">
	<gauge:CircularScale Radius="80">
		<gauge:CircularTickSet>
			<gauge:CircularTickSet.Ticks>
				<gauge:CircularTickMarkMinor />
				<gauge:CircularTickMarkMajor />
				<gauge:CircularTickLabelMajor ScalePlacement="Inside" />
			</gauge:CircularTickSet.Ticks>
			<gauge:CircularTickSet.Pointers>
				<gauge:CircularPointerNeedle Value="25" Background="Yellow" />
			</gauge:CircularTickSet.Pointers>
		</gauge:CircularTickSet>
	</gauge:CircularScale>
</gauge:CircularGauge>
```

## Getting Started with DigitalGauge

This code shows the base XAML that you can use to create a simple [DigitalGauge](xref:ActiproSoftware.Windows.Controls.Gauge.DigitalGauge) to display a string:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:DigitalGauge Width="200" Height="100" Value="Testing" />
```

## Getting Started with Led

This code shows the base XAML that you can use to create a simple [Led](xref:ActiproSoftware.Windows.Controls.Gauge.Led) with a green light:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:Led Width="100" Height="100" LedForeground="Green" />
```

## Getting Started with LinearGauge

This code shows the base XAML that you can use to create a simple [LinearGauge](xref:ActiproSoftware.Windows.Controls.Gauge.LinearGauge) with a few gauge elements:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:LinearGauge Width="200" Height="100">
	<gauge:LinearScale BarExtent="85%">
		<gauge:LinearTickSet>
			<gauge:LinearTickSet.Ticks>
				<gauge:LinearTickMarkMinor />
				<gauge:LinearTickMarkMajor />
				<gauge:LinearTickLabelMajor ScalePlacement="Inside" />
			</gauge:LinearTickSet.Ticks>
			<gauge:LinearTickSet.Pointers>
				<gauge:LinearPointerMarker Value="25" Background="Yellow" />
			</gauge:LinearTickSet.Pointers>
		</gauge:LinearTickSet>
	</gauge:LinearScale>
</gauge:LinearGauge>
```

## Getting Started with ToggleSwitch

This code shows the base XAML that you can use to create a simple [ToggleSwitch](xref:ActiproSoftware.Windows.Controls.Gauge.ToggleSwitch):

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<gauge:ToggleSwitch Width="100" Height="100" IsChecked="true" />
```

## Further Study

It's very easy to use the various Gauge controls and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of the controls.

If you require further assistance after looking through those, please visit our support forum for the product.
