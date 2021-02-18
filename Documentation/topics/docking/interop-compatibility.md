---
title: "Interop Compatibility"
page-title: "Interop Compatibility - Docking & MDI Interoperability"
order: 20
---
# Interop Compatibility

There are several issues in general when embedding interop controls (such as WinForms, ActiveX, etc. controls) in docking window content.  This topic discusses some of the ways Actipro Docking & MDI has been designed to improve compatibility with interop controls.

The two primary issues are:

- Interop content is always rendered on top of WPF content in the same root window (a.k.a. the airspace issue).
- Interop content doesn't always raise WPF focus-related events properly.

## Quick Summary

If you don't wish to read the details below, the way to enable maximum compatibility with interop controls is to:

- Set the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[UseHostedPopups](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.UseHostedPopups) property to `false`.

- Set the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[IsLiveSplittingEnabled](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.IsLiveSplittingEnabled) property to `false`.

- Set the [InteropFocusTracking](xref:ActiproSoftware.Windows.Controls.Docking.InteropFocusTracking).`IsEnabled` attached property on all `HwndHost` instances (like `WindowsFormsHost`, etc.) to `true`.

Read the following sections for more information on interop compatibility.

## Using Non-Hosted Popups

Due to the airspace rendering issue mentioned above, if your application will host interop content, set the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[UseHostedPopups](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.UseHostedPopups) property to `false`.  Setting that to `false` will allow auto-hide popups and splitters to properly appear above interop content.

This will also allow interop content to appear properly in the auto-hide popups because it will disable auto-hide popup animation, which sometimes causes rendering glitches in interop controls.

If you do not plan on having interop content in your application, we recommend leaving the [UseHostedPopups](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.UseHostedPopups) property as its default value of `true`.

## Live Splitting

By default, live splitting is enabled meaning that there is no preview highlight when dragging a splitter.  The surrounding containers are immediately resized during splitter drags in this mode.

Sometimes this mode doesn't work well with interop controls since they could have glitchy rendering as they are resized.  It's recommended to disable live splitting by setting the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[IsLiveSplittingEnabled](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.IsLiveSplittingEnabled) property to `false`.  In that mode, the splitters in the dock site will only resize the content when the drag operation has been completed.  A preview of the split location is shown as the user drags to indicate the relative sizes of the content if the user releases the pointer.  This allows complex UI to only be rendered once, after the drag operation is completed.

## Handling Focus Issues

There are known issues in core WPF with how it tracks and reports focus movement in and out of `HwndHost`.  This makes it difficult to use standard WPF-based focus events when dealing with `HwndHost`-based scenarios, because the events don't fire properly.

We provide a workaround for most of these issues in the form of an [InteropFocusTracking](xref:ActiproSoftware.Windows.Controls.Docking.InteropFocusTracking).`IsEnabled` attached property that should be set on any `HwndHost`-based control, such as `WindowsFormsHost`, WPF `WebBrowser`, etc.  Set that attached property to `true` on such controls in your docking window hierarchy to enable our workarounds.

## How to Embed WinForms Content in a Docking Window

This sample XAML code shows how to create a document window that contains a Windows Forms `WebBrowser` control embedded in it.  Note that any WinForms controls must be wrapped with a `WindowsFormsHost`.  Set the attached [InteropFocusTracking](xref:ActiproSoftware.Windows.Controls.Docking.InteropFocusTracking).`IsEnabled` property on the `WindowsFormsHost`.

```xaml
xmlns:winforms="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
...
					
<docking:DockSite>
	<docking:Workspace>
		<docking:TabbedMdiHost>
			<docking:TabbedMdiContainer>
				<docking:DocumentWindow Title="Document1">
					<WindowsFormsHost docking:InteropFocusTracking.IsEnabled="True">
						<winforms:WebBrowser Url="http://www.actiprosoftware.com" />
					</WindowsFormsHost>
				</docking:DocumentWindow>
			</docking:TabbedMdiContainer>
		</docking:TabbedMdiHost>
	</docking:Workspace>
</docking:DockSite>
```

## Impact of Transparent Backgrounds on Performance

As a quick aside, we've noticed in certain scenarios that if you wrap a `WindowsFormsHost` with a control that has a `Transparent` background, core WPF can cause performance degradation in layout cycles due to how it's attempting to blend backgrounds with interop content.  This sample XAML code shows the scenario:

```xaml
xmlns:winforms="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
...
					
<docking:DocumentWindow Title="Document1">
	<Border Background="Transparent">
		<WindowsFormsHost docking:InteropFocusTracking.IsEnabled="True">
			<winforms:WebBrowser Url="http://www.actiprosoftware.com" />
		</WindowsFormsHost>
	</Border>
</docking:DocumentWindow>
```

If the `Background` is left its default null value, or is set to a solid color, performance is significantly better.  This can occur anywhere in WPF (not just the Docking/MDI product), and we wanted to pass along the tip.
