---
title: "Auto-Hidden Tool Windows"
page-title: "Auto-Hidden Tool Windows - Docking & MDI Docking Window Features"
order: 10
---
# Auto-Hidden Tool Windows

Tool windows in [auto-hide state](docking-window-states.md) show tabs along a tray area on the side of their dock host.  When a tab is clicked, the related tool window's content is displayed in an animated popup.

![Screenshot](../images/state-auto-hide.png)

*The Solution Explorer tool window in AutoHide state with its popup displayed*

## Allowing Auto-Hide

Only tool windows can be auto-hidden.  A global [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[CanToolWindowsAutoHide](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.CanToolWindowsAutoHide) property (defaults to `true`) determines the default value for whether they have that capability, but it can be overridden at an instance-level by setting the [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow).[CanAutoHide](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow.CanAutoHide) property.

## Programmatically Auto-Hiding a Tool Window

A tool window can be programmatically auto-hidden by calling the [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow).[AutoHide](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow.AutoHide*) method.  An overload of the method allows you to specify the [Side](xref:ActiproSoftware.Windows.Controls.Side) against which to auto-hide.

## Behavior and Animation Options

These options control auto-hide functionality behavior and ensuing animation:

| Member | Description |
|-----|-----|
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHidePerContainer](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePerContainer) Property | Gets or sets whether auto-hide state toggles affect all the windows in the parent container.  The default value is `true`. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHidePopupCloseAnimationDuration](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupCloseAnimationDuration) Property | Gets or sets the time in milliseconds of the animation that is applied to an auto-hide popup close.  The default value is `150ms`. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHidePopupCloseDelay](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupCloseDelay) Property | Gets or sets the time in milliseconds of the delay between when the pointer leaves an auto-hide popup and when the popup closes if the mouse has not moved back over the popup.  This only applies when [AutoHidePopupOpensOnMouseHover](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupOpensOnMouseHover) is `true`.  The default value is `500ms`. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHidePopupOpenAnimationDuration](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupOpenAnimationDuration) Property | Gets or sets the time in milliseconds of the animation that is applied to an auto-hide popup open.  The default value is `150ms`. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHidePopupOpenDelay](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupOpenDelay) Property | Gets or sets the time in milliseconds of the delay between when the pointer moves over an auto-hide tab item and the auto-hide popup opens to display the tool window represented by the tab item.  This only applies when [AutoHidePopupOpensOnMouseHover](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupOpensOnMouseHover) is `true`.  The default value is `200ms`. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHidePopupOpensOnMouseHover](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHidePopupOpensOnMouseHover) Property | Gets or sets whether the auto-hide popup displays when the mouse hovers over an auto-hide tab item.  The default value is `false` for most themes, but may be `true` for some themes. |

## Appearance Options

These options control the appearance of auto-hide tabs:

| Member | Description |
|-----|-----|
| [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow).[AutoHideTabContextContentTemplate](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow.AutoHideTabContextContentTemplate) Property | Gets or sets the `DataTemplate` containing contextual content that should be rendered in an auto-hide tab for this window. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHideTabItemTemplate](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHideTabItemTemplate) Property | Gets or sets the `DataTemplate` to use for rendering the auto-hide tab items. |
| [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHideTabItemTemplateSelector](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHideTabItemTemplateSelector) Property | Gets or sets the `DataTemplateSelector` to use for rendering the auto-hide tab items. |

## Iterating Auto-Hidden Tool Windows

These [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) properties are the four collections of [ToolWindowContainer](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindowContainer) controls, one collection for each side of the dock site.

- [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHideLeftContainers](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHideLeftContainers)
- [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHideTopContainers](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHideTopContainers)
- [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHideRightContainers](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHideRightContainers)
- [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[AutoHideBottomContainers](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.AutoHideBottomContainers)

 Each of those accepts one or more [ToolWindowContainer](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindowContainer) controls, each of which can contain one or more [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) controls.

The properties above are simple wrappers for similar properties on the [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockHost) returned by the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[PrimaryDockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrimaryDockHost) property.  You can iterate through those collections to see which tool windows are on each side of the primary dock host.

You can also examine each floating [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockHost), if there are any, by iterating the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[FloatingDockHosts](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.FloatingDockHosts) collection and examining the [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockHost).[AutoHideLeftContainers](xref:ActiproSoftware.Windows.Controls.Docking.DockHost.AutoHideLeftContainers), etc. properties on them.

## Determining If an Auto-Hide Popup Is Displayed

The [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockHost).[IsAutoHidePopupOpen](xref:ActiproSoftware.Windows.Controls.Docking.DockHost.IsAutoHidePopupOpen) property returns `true` when there is an auto-hide popup displayed.

The [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockHost).[AutoHidePopupToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockHost.AutoHidePopupToolWindow) property returns the [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow) that is currently open in that popup.  The [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow).[IsAutoHidePopupOpen](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow.IsAutoHidePopupOpen) property of that tool window will return `true` while it is open in the popup.

## Preventing an Auto-Hide Popup From Closing

Auto-hide popups will close by default after a brief delay whenever the keyboard focus is moved outside of them.  This can be a problem in scenarios where a dialog `Window` is opened from the tool window displayed in the auto-hide popup.  In that scenario, the keyboard focus moves to the dialog and the auto-hide popup is closed.  However you may wish to keep the auto-hide popup visible while the dialog is displayed so that focus properly returns to it when the dialog is closed.

This code shows how to display a dialog and keep any current auto-hide popup open:

```csharp
try {
	dockSite.AutoHidePopupClosesOnLostFocus = false;
	window.ShowDialog();
}
finally {
	dockSite.AutoHidePopupClosesOnLostFocus = true;
}
```

## Dynamically Altering the Auto-Hide Side

The [WindowsAutoHiding](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsAutoHiding) event fires whenever one or more tool windows are auto-hidden, allowing for the side upon which they are being auto-hidden to be customized.  This is useful in scenarios like when you wish to restrict auto-hiding to only the left/right or top/bottom of a dock host.

The event is passed arguments of type [DockingWindowsAutoHidingEventArgs](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindowsAutoHidingEventArgs), which has a [Side](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindowsAutoHidingEventArgs.Side) property.  An event handler can examine the list of windows being auto-hidden and can choose to change the side to another side.

## Auto-Hide Popup Events

The [WindowAutoHidePopupOpened](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowAutoHidePopupOpened) and [WindowAutoHidePopupClosed](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowAutoHidePopupClosed) events on [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) fire whenever the auto-hide popup is opened or closed.  The event arguments specify which tool window was opened or closed.

## Notes on Interop Usage

If you use an interop (WinForms, ActiveX, etc.) control in your docking windows, auto-hide popups in their default configuration will not appear on top of the interop content due to WPF airspace issues with interop content in the same `Window`.  By setting the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[UseHostedPopups](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.UseHostedPopups) property to `false`, non-hosted popups will be used to display auto-hide content instead.  This uses a separate `Window` to render the popup and thereby allows WPF content in the popup to render above the interop content.  The only downside to setting this property is that you lose the popup open/close animation.

See the [Interop Compatibility](../interop-compatibility.md) topic for more information.
