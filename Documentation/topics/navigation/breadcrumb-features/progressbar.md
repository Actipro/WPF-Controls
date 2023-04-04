---
title: "Progress Bar"
page-title: "Progress Bar - Breadcrumb Features"
order: 11
---
# Progress Bar

The [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control contains an embedded [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) control, which can be used to show the progress of long running operations.

> [!NOTE]
> Check out the [AnimatedProgressBar](../../shared/windows-controls/animatedprogressbar.md) control topic for more information on its features.

![Screenshot](../images/breadcrumb-progressbar-normal-aero-normal-color.gif)

![Screenshot](../images/breadcrumb-progressbar-paused-aero-normal-color.gif)

![Screenshot](../images/breadcrumb-progressbar-error-aero-normal-color.gif)

*The Breadcrumb control using the Aero theme with the progress bar in the three possible states, Normal, Paused, and Error*

## Showing the Progress Bar

By default, the progress bar is hidden and must be explicitly shown by setting [IsProgressBarVisible](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.IsProgressBarVisible) to `true`. Also, the progress bar is only visible when the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control is not in edit-mode (when [IsEditing](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.IsEditing) is `false`).

> [!NOTE]
> Changes to [IsProgressBarVisible](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.IsProgressBarVisible) are animated, so the progress bar will fade in when this property is set to `true` and fade out when set to `false`.

![Screenshot](../images/breadcrumb-progressbar-normal-aero-normal-color.gif)

![Screenshot](../images/breadcrumb-progressbar-is-editing-aero-normal-color.gif)

*The Breadcrumb control using the Aero theme with the bottom control in edit mode*

The progress bar spans the entire height and width of the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) control, including the area reserved for the [action buttons](action-buttons.md). Therefore, if an action button does not have a transparent background, then it will hide a portion of the progress bar. The [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) uses [PopupButton](xref:@ActiproUIRoot.Controls.PopupButton) controls internally, and it is recommended that the [PopupButton](xref:@ActiproUIRoot.Controls.PopupButton) control (with its transparency mode enabled) be used for custom action buttons as well.

This sample code shows how to create a [PopupButton](xref:@ActiproUIRoot.Controls.PopupButton) which can be used as an action button:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...
<shared:PopupButton Content="New PopupButton"
                    DisplayMode="ButtonOnly"
                    IsRounded="false"
                    IsTransparencyModeEnabled="true" />
```

## Associated Members

The following [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) members are associated with the embedded progress bar:

| Member | Description |
|-----|-----|
| [IsProgressBarVisible](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.IsProgressBarVisible) Property | Gets or sets a value indicating whether the embedded progress bar is visible.  The default value is `false`. |
| [ProgressBarDecreaseDuration](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarDecreaseDuration) Property | Gets or sets the `Duration` of the decrease animation of the embedded progress bar.  The default value is `500` milliseconds. |
| [ProgressBarIncreaseDuration](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarIncreaseDuration) Property | Gets or sets the `Duration` of the increase animation of the embedded progress bar.  The default value is `500` milliseconds. |
| [ProgressBarIsAnimationEnabled](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarIsAnimationEnabled) Property | Gets or sets value indicating whether the embedded progress bar should animate `ProgressBarValue` changes.  The default value is `true`. |
| [ProgressBarIsIndeterminate](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarIsIndeterminate) Property | Gets or sets a value indicating whether the embedded progress bar shows actual values or generic, continuous progress feedback.  The default value is `false`. |
| [ProgressBarMaximum](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarMaximum) Property | Gets or sets the highest possible `ProgressBarValue` of the embedded progress bar.  The default value is `1.0`. |
| [ProgressBarMinimum](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarMinimum) Property | Gets or sets the lowest possible `ProgressBarValue` of the embedded progress bar.  The default value is `0.0`. |
| [ProgressBarState](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarState) Property | Gets or sets the state of the embedded progress bar.  The default value is `Normal`. |
| [ProgressBarValue](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb.ProgressBarValue) Property | Gets or sets the current value of the embedded progress bar.  The default value is `0.0`. |
