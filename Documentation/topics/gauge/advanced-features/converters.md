---
title: "Converters"
page-title: "Converters - Gauge Advanced Features"
order: 3
---
# Converters

Actipro Gauge comes with a few built-in converters that provide additional features for use with the gauge controls.

## Average Value

The [DoubleAverageConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleAverageConverter) class can be used to calculate the average of any number of values. It returns the running average of all the `Double` values that it has been used to convert. When calculating the running average, the entire set of previous values is not required. Therefore, calculating the running average only requires a few bytes of storage in memory.

[DoubleAverageConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleAverageConverter) can be configured to only use the last `N` items, where `N` is controlled by [ValueCount](xref:@ActiproUIRoot.Controls.Gauge.DoubleAverageConverter.ValueCount).

This code shows how a [DoubleAverageConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleAverageConverter) can be declared and used to calculate the average of the last 100 values of the bar pointer:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<Window.Resources>

	<!-- Converters -->
	<gauge:DoubleAverageConverter x:Key="DoubleAverageConverter" ValueCount="100" />

</Window.Resources>

...
						
<gauge:LinearGauge Width="200" Height="50">
	<gauge:LinearScale>
		<gauge:LinearTickSet>
			<gauge:LinearTickSet.Pointers>
				<gauge:LinearPointerBar x:Name="bar" />
				<gauge:LinearPointerMarker MarkerType="TriangleSharp" ScalePlacement="Inside" 
						Value="{Binding ElementName=bar, Path=Value, Converter={StaticResource DoubleAverageConverter}}" />
			</gauge:LinearTickSet.Pointers>
		</gauge:LinearTickSet>
	</gauge:LinearScale>
</gauge:LinearGauge>
```

## Maximum Value

The [DoubleMaximumConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleMaximumConverter) class can be used to calculate the maximum value of any number of values.

[DoubleMaximumConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleMaximumConverter) can be configured to only use the last `N` items, where `N` is controlled by [ValueCount](xref:@ActiproUIRoot.Controls.Gauge.DoubleMaximumConverter.ValueCount).

This code shows how a [DoubleMaximumConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleMaximumConverter) can be declared and used to calculate the maximum value of the last 100 values of the bar pointer:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<Window.Resources>

	<!-- Converters -->
	<gauge:DoubleMaximumConverter x:Key="DoubleMaximumConverter" ValueCount="100" />

</Window.Resources>

...
						
<gauge:LinearGauge Width="200" Height="50">
	<gauge:LinearScale>
		<gauge:LinearTickSet>
			<gauge:LinearTickSet.Pointers>
				<gauge:LinearPointerBar x:Name="bar" />
				<gauge:LinearPointerMarker MarkerType="TriangleSharp" ScalePlacement="Inside" 
						Value="{Binding ElementName=bar, Path=Value, Converter={StaticResource DoubleMaximumConverter}}" />
			</gauge:LinearTickSet.Pointers>
		</gauge:LinearTickSet>
	</gauge:LinearScale>
</gauge:LinearGauge>
```

## Minimum Value

The [DoubleMinimumConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleMinimumConverter) class can be used to calculate the minimum value of any number of values.

[DoubleMinimumConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleMinimumConverter) can be configured to only use the last `N` items, where `N` is controlled by [ValueCount](xref:@ActiproUIRoot.Controls.Gauge.DoubleMinimumConverter.ValueCount).

This code shows how a [DoubleMinimumConverter](xref:@ActiproUIRoot.Controls.Gauge.DoubleMinimumConverter) can be declared and used to calculate the minimum value of the last 100 values of the bar pointer:

```xaml
xmlns:gauge="http://schemas.actiprosoftware.com/winfx/xaml/gauge"     
...

<Window.Resources>

	<!-- Converters -->
	<gauge:DoubleMinimumConverter x:Key="DoubleMinimumConverter" ValueCount="100" />

</Window.Resources>

...
						
<gauge:LinearGauge Width="200" Height="50">
	<gauge:LinearScale>
		<gauge:LinearTickSet>
			<gauge:LinearTickSet.Pointers>
				<gauge:LinearPointerBar x:Name="bar" />
				<gauge:LinearPointerMarker MarkerType="TriangleSharp" ScalePlacement="Inside" 
						Value="{Binding ElementName=bar, Path=Value, Converter={StaticResource DoubleMinimumConverter}}" />
			</gauge:LinearTickSet.Pointers>
		</gauge:LinearTickSet>
	</gauge:LinearScale>
</gauge:LinearGauge>
```

## Boolean to LedState

The [BooleanToLedStateConverter](xref:@ActiproUIRoot.Controls.Gauge.BooleanToLedStateConverter) class can be used to bind an [LedState](xref:@ActiproUIRoot.Controls.Gauge.LedState) property to a `Boolean` property.

`BooleanToLedStateConverter` contains two properties: [TrueState](xref:@ActiproUIRoot.Controls.Gauge.BooleanToLedStateConverter.TrueState) and [FalseState](xref:@ActiproUIRoot.Controls.Gauge.BooleanToLedStateConverter.FalseState), which indicate what `LedState` enumeration value to return when convert to/from `true` and `false` (and `null` for `bool?` types), respectively.
