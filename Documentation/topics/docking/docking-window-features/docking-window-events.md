---
title: "Docking Window Events"
page-title: "Docking Window Events - Docking & MDI Docking Window Features"
order: 5
---
# Docking Window Events

There are a number of events to which you can attach to know when various actions occur to docking windows.

## DockSite Events

The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) that owns the docking window receiving an action has these events:

| Member | Description |
|-----|-----|
| [FloatingWindowOpening](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.FloatingWindowOpening) Event | Occurs when a floating window is opening, allowing for customization before it is displayed. |
| [MdiKindChanged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.MdiKindChanged) Event | Occurs when the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[MdiKind](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.MdiKind) property is changed. |
| [MenuOpening](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.MenuOpening) Event | Occurs when a docking-related context menu is opening, allowing for customization before it is displayed. |
| [NewWindowRequested](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.NewWindowRequested) Event | Occurs when a new [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) is requested, generally via a user click on a new tab button. |
| [PrimaryDocumentChanged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrimaryDocumentChanged) Event | Occurs when the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite).[PrimaryDocument](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.PrimaryDocument) property has changed. |
| [WindowActivated](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowActivated) Event | Occurs after a [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) has been activated. |
| [WindowAutoHidePopupClosed](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowAutoHidePopupClosed) Event | Occurs after an auto-hide popup has been closed that displayed a [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow). |
| [WindowAutoHidePopupOpened](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowAutoHidePopupOpened) Event | Occurs after an auto-hide popup has been opened that displays a [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow). |
| [WindowDeactivated](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowDeactivated) Event | Occurs after a [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) has been deactivated. |
| [WindowDefaultLocationRequested](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowDefaultLocationRequested) Event | Occurs when a [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow)'s default location is requested. |
| [WindowRegistered](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowRegistered) Event | Occurs after a [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) has been registered. |
| [WindowsAutoHiding](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsAutoHiding) Event | Occurs before one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls are auto-hidden, allowing for side customization. |
| [WindowsClosed](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsClosed) Event | Occurs after one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls have been closed. |
| [WindowsClosing](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsClosing) Event | Occurs before one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls are closed, allowing for cancellation of the close. |
| [WindowsDockHostChanged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsDockHostChanged) Event | Occurs after one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls' [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow.DockHost) properties have changed. |
| [WindowsDragged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsDragged) Event | Occurs after one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls are dragged by the end user. |
| [WindowsDragging](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsDragging) Event | Occurs before one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls are dragged by the end user. |
| [WindowsDragOver](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsDragOver) Event | Occurs when one or more docking windows are dragged over a new dock target, allowing for certain dock guides to be hidden. |
| [WindowsOpened](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsOpened) Event | Occurs after one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls have been opened. |
| [WindowsOpening](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsOpening) Event | Occurs before one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls is opened. |
| [WindowsStateChanged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsStateChanged) Event | Occurs after one or more [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls' states have changed. |
| [WindowUnregistered](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowUnregistered) Event | Occurs after a [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) has been unregistered. |

Most of the event arguments pass references to the related [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) controls.

## Displaying a StatusBar Message During Drags

It is a common practice to display a status bar message during tool window drags to indicate to the end user that they can move over the dock guides to docks a tool window.

To accomplish this, handle the [WindowsDragging](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsDragging) and display a message like this in your status bar:

`Use the guide diamond to choose a docking location.  To prevent docking, hold down CTRL.`

Handle the [WindowsDragged](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.WindowsDragged) event and restore the status bar message back to something like `Ready`.

## Customizing Menus

The [MenuOpening](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.MenuOpening) event fires before any menu is displayed for a window or button, allowing you to customize it or replace it with your own.

See the [Menu Customization](menu-customization.md) topic for details on handling this event.
