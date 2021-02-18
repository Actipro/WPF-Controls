---
title: "RibbonWindow"
page-title: "RibbonWindow - Miscellaneous Features"
order: 11
---
# RibbonWindow

Actipro Ribbon includes a [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow) class, which is an implementation of the `Window` class that provides a custom window user interface much like that found in Office.

It is always recommended that a [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) control implementation be hosted within a [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow).  While a [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow) can be used without a [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) control at all, it's best to use [WindowChrome](../../themes/windowchrome.md) directly on normal `Window` objects instead of having your window inherit [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow) if you don't plan on having a [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) in the window.

![Screenshot](../images/ribbonwindow.png)

*A simple RibbonWindow*

Note that in the screenshot above, a simple `RichTextBox` is hosted within a [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow) and there is no [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) being used.

## Setting the RibbonWindow Title

The title in the [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow) is drawn in two parts, the application name and the document name.  Each is drawn using a separate color, which provides some distinction between the two.

The [ApplicationName](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.ApplicationName) property should be set to the name of the application.

The [DocumentName](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.DocumentName) property should be set to the name of the currently open document.  If there is no document open, leave this property `null`.

When both properties are specified, a hyphen will be inserted between the two.

## Full-Screen Mode (IsTitleBarVisible)

Say you have an application like PowerPoint where there normally is a [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) at the top of it however during a slide preview mode, you want to maximize the window, hide the ribbon, and show the content area in full-screen mode.

Normally, setting the `Window.WindowStyle` property to `None` would hide a title bar and allow full-screen mode.  However, [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow) is often forced to be in `None` style to achieve its custom UI.  Therefore setting this property doesn't hide the title bar area like it would in a normal `Window`.

To achieve a full-screen mode, set the [RibbonWindow](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow).[IsTitleBarVisible](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.IsTitleBarVisible) property to `false`.  This should only be done when the window is maximized and any child [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) is collapsed (hidden).

## Maximize/Minimize Button Visibility

The visibility of the `RibbonWindow`'s maximize and minimize buttons can be customized using the [IsMaximizeButtonVisible](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.IsMaximizeButtonVisible) and [IsMinimizeButtonVisible](xref:ActiproSoftware.Windows.Controls.Ribbon.RibbonWindow.IsMinimizeButtonVisible) properties, respectively.

## Focus Moves on Window Deactivation

By default, if a control in the ribbon has focus when the window is deactivated, focus will be moved out of the ribbon and back into the main content area of the window.  The [IsKeyboardFocusBlurredOnWindowDeactivation](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon.IsKeyboardFocusBlurredOnWindowDeactivation) property can be set to `false` to prevent this default behavior.

## WindowChrome Usage

Each `RibbonWindow` already has a pre-installed [WindowChrome](../../themes/windowchrome.md) instance that is configured to support the QAT and other Ribbon features, and will break functionality if changed.  The `WindowChrome` is what allows the `RibbonWindow` to have a customized user interface compared to a normal window.  You can get the `WindowChrome` instance for a `RibbonWindow` programmatically and alter its properties as needed via a call to [WindowChrome](xref:ActiproSoftware.Windows.Themes.WindowChrome).[GetChrome](xref:ActiproSoftware.Windows.Themes.WindowChrome.GetChrome*).
