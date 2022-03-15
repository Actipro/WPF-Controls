---
title: "Progress Spinners"
page-title: "Progress Spinners - Shared Library Controls"
order: 40
---
# Progress Spinners

Progress spinners are used when some form of processing is occurring to tell the end user that something is happening.

![Screenshot](../images/ring-spinner-large.png)

*A large RingSpinner control.*

## RingSpinner Overview

The [RingSpinner](xref:@ActiproUIRoot.Controls.RingSpinner) control renders an animated ring where the two ring segment ends chase each other around the circle.

![Screenshot](../images/ring-spinner.png) ![Screenshot](../images/ring-spinner-pie.png)

*A couple RingSpinner controls.*

The [IsSpinning](xref:@ActiproUIRoot.Controls.RingSpinner.IsSpinning) property must be set to `true` to activate animated spinning.

The outer radius of the ring will be auto-calculated based on the `Width` and `Height` of the control.  They always should be set.  For instance if `Width` and `Height` are `16` and there is no `Padding` set, the ring's radius will be `8`.

The `BorderBrush` and `BorderThickness` properties specify the brush and thickness of the ring, extending inward from the outer radius.

> [!TIP]
> Use a `BorderThickness` value that is the same as the [Radius](xref:@ActiproUIRoot.Controls.RingSpinner.Radius) to render a pie.  The [LineCap](xref:@ActiproUIRoot.Controls.RingSpinner.LineCap) property (defaults to `Round`) should be set to `Flat` when rendering as a pie.
