---
title: "ArrangeAnimation"
page-title: "ArrangeAnimation - Views Animations"
order: 2
---
# ArrangeAnimation

[ArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation) represents a default implementation of [IArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.IArrangeAnimation) that allows the animation of a panel's child elements to be easily configured and/or customized.

## Overview

[ArrangeAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation) provides several built-in animations and exposes several configuration options. `ArrangeAnimation` can be used directly, as the basis for a custom class, or completely replaced.

Elements can have one of several arrange status in its current state, represented by [ArrangeStatus](xref:@ActiproUIRoot.Controls.Views.ArrangeStatus).  The properties on `ArrangeAnimation` used to create a `Storyboard` for an element vary based on its status. Such as the duration of the storyboard, which is controlled by one of the following properties:

| Member | Description |
|-----|-----|
| [EnterDuration](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.EnterDuration) Property | Gets or sets the duration of the storyboard applied to elements entering the panel (i.e. `ArrangeStatus.Entering`) |
| [LeaveDuration](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.LeaveDuration) Property | Gets or sets the duration of the storyboard applied to elements leaving the panel (i.e. `ArrangeStatus.Leaving`) |
| [ArrangeUpdateDuration](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.ArrangeUpdateDuration) Property | Gets or sets the duration of the storyboard applied to elements changing position/size in the panel due to rearrangement (i.e. `ArrangeStatus.ArrangeUpdating`) |
| [LayoutUpdateAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.LayoutUpdateAnimation) Property | Gets or sets the duration of the storyboard applied to elements changing position/size in the panel due to a layout logic change (i.e. `ArrangeStatus.LayoutUpdating`) |

There are several animations that apply to certain arrange statuses, which are described in detail below and include:

| Animation | Entering | Leaving | ArrangeUpdating | LayoutUpdating |
|-----|-----|-----|-----|-----|
| Fade | Yes | Yes | -   | -   |
| Move | Yes | Yes | Yes | Yes |
| Rotate | Yes | Yes | -   | -   |
| Scale | Yes | Yes | -   | -   |
| Size | -   | -   | Yes | Yes |
| Translate | Yes | Yes | -   | -   |

The animations that are applied to each arrange status are configured using the following properties.  Each property can be set to zero or more enumeration flag values as defined by [ArrangeAnimations](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimations).  For example, the `EnterAnimation` can be set to `"Fade,Scale"` in XAML to have elements entering the panel to fade and scale in.

If one of the following properties includes a flag that is not supported (per the table above), then it will simply be ignored.

| Member | Description |
|-----|-----|
| [EnterAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.EnterAnimation) Property | Gets or sets a value indicating the animation that should be applied to elements entering the panel. |
| [LeaveAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.LeaveAnimation) Property | Gets or sets a value indicating the animation that should be applied to elements leaving the panel. |
| [ArrangeUpdateAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.ArrangeUpdateAnimation) Property | Gets or sets a value indicating the animation that should be applied to elements changing position/size in the panel due to rearrangement. |
| [LayoutUpdateAnimation](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.LayoutUpdateAnimation) Property | Gets or sets a value indicating the animation that should be applied to elements changing position/size in the panel due to a layout logic change. |

## Animation Settings

Several "animation settings" properties are provided, which can be used to set properties directly on the `DoubleAnimation` (or similar) objects created. Represented by the [ArrangeAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimationSettings) class, it currently allows the acceleration and deceleration ratios to be set.

## Fade Animation

The `ArrangeAnimations.Fade` flag supports animating the `Opacity` value of the child elements that are entering or leaving the panel.  The following properties can be used to configure the resulting animation:

| Member | Description |
|-----|-----|
| [FadeEnterAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.FadeEnterAnimationSettings) Property | Gets or sets the fade animation settings used for elements entering the panel. |
| [FadeEnterOpacity](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.FadeEnterOpacity) Property | Gets or sets the fade animation starting opacity for elements entering the panel. |
| [FadeLeaveAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.FadeLeaveAnimationSettings) Property | Gets or sets the fade animation settings used for elements leaving the panel. |
| [FadeLeaveOpacity](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.FadeLeaveOpacity) Property | Gets or sets the fade animation ending opacity for elements leaving the panel. |

## Move Animation

The `ArrangeAnimations.Move` flag supports animating the `X` and `Y` values of the arrange rectangle of the child elements.  The following properties can be used to configure the resulting animation:

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

[MoveArrangeUpdateAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.MoveArrangeUpdateAnimationSettings) Property

</td>
<td>Gets or sets the move animation settings used for elements updating in the panel due to rearrangement.</td>
</tr>

<tr>
<td>

[MoveEnterAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.MoveEnterAnimationSettings) Property

</td>
<td>Gets or sets the move animation settings used for elements entering the panel.</td>
</tr>

<tr>
<td>

[MoveEnterPoint](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.MoveEnterPoint) Property

</td>
<td>

Gets or sets the move animation starting point for elements entering the panel.

If this property is set to ``, then the starting point will not be animated.  When non-null, the `X` and `Y` values determine the starting point using values relative to the panels bounds.

If `X` and `Y` have a value of `-1`, then the starting point will be the top-left corner of the panel.  If `X` and `Y` have a value of `0`, then the starting point will be the center of the panel.  If `X` and `Y` have a value of `1`, then the starting point will be the bottom-right corner of the panel.  If the values are greater than `1` or less than `-1`, then the starting point will be off screen by the relative difference.

If `X` or `Y` are set to `double.NaN`, then that value will not be animated.  For example, this can be used to slide items in from the left by specifying a point of `1.2,NaN`.

Finally, if `X` or `Y` are set to `double.PositiveInfinity` or `double.NegativeInfinity`, then that starting value will be determined randomly.

</td>
</tr>

<tr>
<td>

[MoveLayoutUpdateAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.MoveLayoutUpdateAnimationSettings) Property

</td>
<td>Gets or sets the move animation settings used for elements updating in the panel due to a layout logic change.</td>
</tr>

<tr>
<td>

[MoveLeaveAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.MoveLeaveAnimationSettings) Property

</td>
<td>Gets or sets the move animation settings used for elements leaving the panel.</td>
</tr>

<tr>
<td>

[MoveLeavePoint](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.MoveLeavePoint) Property

</td>
<td>

Gets or sets the move animation starting point for elements leaving the panel.

If this property is set to `null`, then the ending point will not be animated.  When non-null, the `X` and `Y` values determine the ending point using values relative to the panels bounds.

If `X` and `Y` have a value of `-1`, then the ending point will be the top-left corner of the panel.  If `X` and `Y` have a value of `0`, then the ending point will be the center of the panel.  If `X` and `Y` have a value of `1`, then the ending point will be the bottom-right corner of the panel.  If the values are greater than `1` or less than `-1`, then the ending point will be off screen by the relative difference.

If `X` or `Y` are set to `double.NaN`, then that value will not be animated.  For example, this can be used to slide items in from the left by specifying a point of `1.2,NaN`.

Finally, if `X` or `Y` are set to `double.PositiveInfinity` or `double.NegativeInfinity`, then that ending value will be determined randomly.

</td>
</tr>

</tbody>
</table>

## Rotate Animation

The `ArrangeAnimations.Rotate` flag supports animating the `RotateTransform.Angle` value in the `RenderTransform` of the child elements that are entering or leaving the panel. If the `RenderTransform` was explicitly set outside of the panel, it will be maintained as the rotation angle is animated.  The following properties can be used to configure the resulting animation:

| Member | Description |
|-----|-----|
| [RotateEnterAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.RotateEnterAnimationSettings) Property | Gets or sets the rotation animation settings used for elements entering the panel. |
| [RotateEnterTotalAngle](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.RotateEnterTotalAngle) Property | Gets or sets the animation's total angle of rotation for elements entering the panel. |
| [RotateLeaveAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.RotateLeaveAnimationSettings) Property | Gets or sets the rotation animation settings used for elements leaving the panel. |
| [RotateLeaveTotalAngle](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.RotateLeaveTotalAngle) Property | Gets or sets the animation's total angle of rotation for elements leaving the panel. |

## Scale Animation

The `ArrangeAnimations.Scale` flag supports animating the `ScaleTransform.ScaleX` and `ScaleTransform.ScaleY` values in the `RenderTransform` of the child elements that are entering or leaving the panel. If the `RenderTransform` was explicitly set outside of the panel, it will be maintained as the scale is animated.  The following properties can be used to configure the resulting animation:

| Member | Description |
|-----|-----|
| [ScaleEnterAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.ScaleEnterAnimationSettings) Property | Gets or sets the scale animation settings used for elements entering the panel. |
| [ScaleEnterUniformScale](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.ScaleEnterUniformScale) Property | Gets or sets the scale animation starting value for elements entering the panel. |
| [ScaleLeaveAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.ScaleLeaveAnimationSettings) Property | Gets or sets the scale animation settings used for elements leaving the panel. |
| [ScaleLeaveUniformScale](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.ScaleLeaveUniformScale) Property | Gets or sets the scale animation ending value for elements leaving the panel. |

## Size Animation

The `ArrangeAnimations.Size` flag supports animating the `Width` and `Height` values of the arrange rectangle of the child elements.  The following properties can be used to configure the resulting animation:

| Member | Description |
|-----|-----|
| [SizeArrangeUpdateAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.SizeArrangeUpdateAnimationSettings) Property | Gets or sets the size animation settings used for elements updating in the panel due to rearrangement. |
| [SizeLayoutUpdateAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.SizeLayoutUpdateAnimationSettings) Property | Gets or sets the size animation settings used for elements updating in the panel due to a layout logic change. |

## Translate Animation

The `ArrangeAnimations.Translate` flag supports animating the `TranslateTransform.X` and `TranslateTransform.Y` values in the `RenderTransform` of the child elements that are entering or leaving the panel. If the `RenderTransform` was explicitly set outside of the panel, it will be maintained as the translation is animated.  The following properties can be used to configure the resulting animation:

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

[TranslateEnterAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.TranslateEnterAnimationSettings) Property

</td>
<td>Gets or sets the translate animation settings used for elements entering the panel.</td>
</tr>

<tr>
<td>

[TranslateEnterPoint](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.TranslateEnterPoint) Property

</td>
<td>

Gets or sets the translate animation starting point for elements entering the panel.

The `X` and `Y` values determine the starting point using values relative to the panels bounds.  If `X` and `Y` have a value of `-1`, then the starting point will be the top-left corner of the panel.  If `X` and `Y` have a value of `0`, then the starting point will be the center of the panel.  If `X` and `Y` have a value of `1`, then the starting point will be the bottom-right corner of the panel.  If the values are greater than `1` or less than `-1`, then the starting point will be off screen by the relative difference.

If `X` or `Y` are set to `double.NaN`, then that value will not be animated.  For example, this can be used to slide items in from the left by specifying a point of `1.2,NaN`.

Finally, if `X` or `Y` are set to `double.PositiveInfinity` or `double.NegativeInfinity`, then that starting value will be determined randomly.

</td>
</tr>

<tr>
<td>

[TranslateLeaveAnimationSettings](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.TranslateLeaveAnimationSettings) Property

</td>
<td>Gets or sets the translate animation settings used for elements leaving the panel.</td>
</tr>

<tr>
<td>

[TranslateLeavePoint](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.TranslateLeavePoint) Property

</td>
<td>

Gets or sets the translate animation ending point for elements leaving the panel.

The `X` and `Y` values determine the ending point using values relative to the panels bounds.  If `X` and `Y` have a value of `-1`, then the ending point will be the top-left corner of the panel.  If `X` and `Y` have a value of `0`, then the ending point will be the center of the panel.  If `X` and `Y` have a value of `1`, then the ending point will be the bottom-right corner of the panel.  If the values are greater than `1` or less than `-1`, then the ending point will be off screen by the relative difference.

If `X` or `Y` are set to `double.NaN`, then that value will not be animated.  For example, this can be used to slide items in from the left by specifying a point of `1.2,NaN`.

Finally, if `X` or `Y` are set to `double.PositiveInfinity` or `double.NegativeInfinity`, then that ending value will be determined randomly.

</td>
</tr>

</tbody>
</table>

Both [TranslateEnterPoint](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.TranslateEnterPoint) and [TranslateLeavePoint](xref:@ActiproUIRoot.Controls.Views.ArrangeAnimation.TranslateLeavePoint) leverage a custom type converter that supports common terms. For example, instead of specifying the enter point a "0,0" (or the center), the string "Center,Center" can be used.  The following table lists common values and their associated names:

| Value | Supported Names |
|-----|-----|
| `double.NaN` | None, Fixed, NaN |
| `double.PositiveInfinity` | Random, PositiveInfinity, +Infinity |
| `double.NegativeInfinity ` | Random, NegativeInfinity, -Infinity |

The following table lists common values and their associated names that are valid for the `X` value:

| Value | Supported Names |
|-----|-----|
| `-1.5` | FarLeft |
| `-1.0` | Left |
| `0` | Center |
| `1.0` | Right |
| `1.5` | FarRight |

The following table lists common values and their associated names that are valid for the `Y` value:

| Value | Supported Names |
|-----|-----|
| `-1.5` | FarTop |
| `-1.0` | Top |
| `0` | Center |
| `1.0` | Bottom |
| `1.5` | FarBottom |
