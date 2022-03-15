---
title: "AnimatedProgressBar"
page-title: "AnimatedProgressBar - Shared Library Controls"
order: 7
---
# AnimatedProgressBar

The [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) includes all the features of the native WPF `ProgressBar`, plus the following features:

- Support for multiple states
- Smooth animation of value changes
- Support for a continuous indicator (in themes that normally use a segmented indicator)

![Screenshot](../images/animatedprogressbar-aero-normal.gif)

![Screenshot](../images/animatedprogressbar-aero-paused.gif)

![Screenshot](../images/animatedprogressbar-aero-error.gif)

*The AnimatedProgressBar control using the Aero theme in the three possible states, Normal, Paused, and Error*

![Screenshot](../images/animatedprogressbar-luna-normal-color-segmented.gif)

![Screenshot](../images/animatedprogressbar-luna-normal-color-continuous.gif)

*The AnimatedProgressBar control using the Luna NormalColor theme showing a segmented indicator and a continuous indicator*

The [OperationState](xref:@ActiproUIRoot.Controls.OperationState) enumeration holds all the valid states, each of which is rendered using a distinct color.

By default, the [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) will animate `Value` changes. This differs from the native WPF `ProgressBar`, which "jumps" to the new `Value`. The smooth animation of the [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) can be disabled by setting [IsAnimationEnabled](xref:@ActiproUIRoot.Controls.AnimatedProgressBar.IsAnimationEnabled) to `false`. In which case, the [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) will also "jump" to the new `Value`.

The [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) class has these important members:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[DecreaseDuration](xref:@ActiproUIRoot.Controls.AnimatedProgressBar.DecreaseDuration) Property

</td>
<td>

Gets or sets the Duration of the decrease animation.  The default value is `500` milliseconds.

</td>
</tr>

<tr>
<td>

[IncreaseDuration](xref:@ActiproUIRoot.Controls.AnimatedProgressBar.IncreaseDuration) Property

</td>
<td>

Gets or sets the Duration of the increase animation.  The default value is `500` milliseconds.

</td>
</tr>

<tr>
<td>

[IsAnimationEnabled](xref:@ActiproUIRoot.Controls.AnimatedProgressBar.IsAnimationEnabled) Property

</td>
<td>

Gets or sets value indicating whether the progress bar should animate `Value` changes.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[IsContinuous](xref:@ActiproUIRoot.Controls.AnimatedProgressBar.IsContinuous) Property

</td>
<td>

Gets or sets value indicating whether the progress bar should render a continuous or segmented indicator.  The default value is `false`.

*Note: This is not supported in the Aero theme*

</td>
</tr>

<tr>
<td>

[State](xref:@ActiproUIRoot.Controls.AnimatedProgressBar.State) Property

</td>
<td>

Gets or sets the state of the progress bar.  The default value is `Normal`.

</td>
</tr>

</tbody>
</table>
