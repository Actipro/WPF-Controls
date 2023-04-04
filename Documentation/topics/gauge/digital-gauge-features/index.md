---
title: "Overview"
page-title: "CircularGauge Features"
order: 1
---
# Overview

[DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) displays a specified value using segmented characters which are custom rendered.

## Value

The text displayed by the [DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) is obtained from a "value" object. If the value is a `String`, then the text is displayed as-is. If the value is a `Double`, then it is rounded down and then converted into a `String`. All other objects will be converted to a `String` using the `ToString` method.

## Frames

[DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) supports the same frame features as [LinearGauge](xref:@ActiproUIRoot.Controls.Gauge.LinearGauge), for more information see the [LinearGauge Frames](../linear-gauge-features/frames.md) topic.

## Characters

The characters displayed by [DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) have several options available to customize their look.

See the [Characters](characters.md) topic for more information.

## Refresh Rate

When using a [DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) to display real-time data, it is possible that the value displayed by the gauge will change to quickly for the user to read the individual values. The [RefreshRate](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge.RefreshRate) property can be used to limit the number of updates displayed to the user.

The refresh rate is specified as the amount of time to wait between updates. Therefore, if the refresh rate is set to `500` milliseconds, then there will be two updates to the display every second. If hundreds of value changes are made during that second, then only two of the values will actually be displayed.

## Text Scrolling

The text presented by the [DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) can automatically be scrolled to the left or right. Scrolling is configured using the [ScrollState](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge.ScrollState) property, which defaults to `None`.

The [ScrollInterval](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge.ScrollInterval) controls the amount of time to wait before shift the characters left or right.

## Led State

The LED lights rendered by the [DigitalGauge](xref:@ActiproUIRoot.Controls.Gauge.DigitalGauge) support three states: on (default), off, and blinking. This functionality mimics the states supported by the [Led](xref:@ActiproUIRoot.Controls.Gauge.Led) control.

See the [Led Features\Led Light](../led-features/led-light.md) topic for more information.
