---
title: "UI Automation"
page-title: "UI Automation - Gauge Layout, Globalization, and Accessibility Features"
order: 4
---
# UI Automation

Actipro Gauge follows the WPF accessibility model for UI automation.

## What is UI Automation?

Microsoft UI Automation is the new accessibility framework for Microsoft Windows.  It addresses the needs of assistive technology products and automated test frameworks by providing programmatic access to information about the user interface (UI).

Gauge implements automation peers for the following classes:

- The [GaugeBase](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.GaugeBase) class, which is the base class of [CircularGauge](xref:ActiproSoftware.Windows.Controls.Gauge.CircularGauge), [DigitalGauge](xref:ActiproSoftware.Windows.Controls.Gauge.DigitalGauge), [Led](xref:ActiproSoftware.Windows.Controls.Gauge.Led), [DigitalGauge](xref:ActiproSoftware.Windows.Controls.Gauge.DigitalGauge), and [FlipSwitch](xref:ActiproSoftware.Windows.Controls.Gauge.FlipSwitch). This automation peer provides access to the child elements.

- The [DigitalGauge](xref:ActiproSoftware.Windows.Controls.Gauge.DigitalGauge) class exposes the currently displayed text to the automation framework using internal classes.  This is in addition to the functionality provided by the `GaugeBase` automation peer.

- The [Led](xref:ActiproSoftware.Windows.Controls.Gauge.Led) class exposes the current state of the led light to the automation framework.  This is in addition to the functionality provided by the `GaugeBase` automation peer.

- The [FlipSwitch](xref:ActiproSoftware.Windows.Controls.Gauge.FlipSwitch) class exposes the current state of the switch to the automation framework.  This is in addition to the functionality provided by the `GaugeBase` automation peer.

- The [PointerBase](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.PointerBase) class, which is the base class of [CircularPointerBar](xref:ActiproSoftware.Windows.Controls.Gauge.CircularPointerBar), [CircularPointerMarker](xref:ActiproSoftware.Windows.Controls.Gauge.CircularPointerMarker), [CircularPointerNeedle](xref:ActiproSoftware.Windows.Controls.Gauge.CircularPointerNeedle), [LinearPointerBar](xref:ActiproSoftware.Windows.Controls.Gauge.LinearPointerBar), and [LinearPointerMarker](xref:ActiproSoftware.Windows.Controls.Gauge.LinearPointerMarker). This automation peer provides access to the value of the pointer.

- The [ScaleBase](xref:ActiproSoftware.Windows.Controls.Gauge.Primitives.ScaleBase) class, which is the base class of [CircularScale](xref:ActiproSoftware.Windows.Controls.Gauge.CircularScale) and [LinearScale](xref:ActiproSoftware.Windows.Controls.Gauge.LinearScale). This automation peer provides access to the child elements.

> [!NOTE]
> [CircularPointerCap](xref:ActiproSoftware.Windows.Controls.Gauge.CircularPointerCap) does not implement an automation peer, since is it only used for aesthetics.
