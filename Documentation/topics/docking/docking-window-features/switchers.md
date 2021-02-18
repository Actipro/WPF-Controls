---
title: "Switchers"
page-title: "Switchers - Docking & MDI Docking Window Features"
order: 21
---
# Switchers

Switchers are used during `Ctrl+Tab`, etc. key presses to easily navigate between open documents and tool windows.

![Screenshot](../images/standard-switcher.png)

*The standard switcher*

These two built-in switcher options are included with Actipro Docking & MDI:

- Simple
- Standard

## Activation Key Gestures

If a switcher is available, these key gestures will activate it:

| Key | Description |
|-----|-----|
| `Ctrl+Tab` | Select the next document. |
| `Alt+F7` | Select the next tool window. |
| `Ctrl+Shift+Tab` | Select the previous document. |
| `Shift+Alt+F7` | Select the previous tool window. |

You can customize the key gestures above by changing these properties:

| Member | Description |
|-----|-----|
| [SelectNextDocumentKeyGesture](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase.SelectNextDocumentKeyGesture) Property | Gets or sets the `KeyGesture` that is used to select the next document.  The default value is `Ctrl+Tab`. |
| [SelectNextToolWindowKeyGesture](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase.SelectNextToolWindowKeyGesture) Property | Gets or sets the `KeyGesture` that is used to select the next tool window.  The default value is `Alt+F7`. |
| [SelectPreviousDocumentKeyGesture](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase.SelectPreviousDocumentKeyGesture) Property | Gets or sets the `KeyGesture` that is used to select the previous document.  The default value is `Ctrl+Shift+Tab`. |
| [SelectPreviousToolWindowKeyGesture](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase.SelectPreviousToolWindowKeyGesture) Property | Gets or sets the `KeyGesture` that is used to select the previous tool window.  The default value is `Shift+Alt+F7`. |

## Configuration

By default the [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) is assigned a [StandardSwitcher](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher) to its [Switcher](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.Switcher) property.

You can clear the [Switcher](xref:ActiproSoftware.Windows.Controls.Docking.DockSite.Switcher) property (set it to `{x:Null}` in XAML or to a null value in code-behind) to not use switchers, or can set that property to another [SwitcherBase](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase)-inherited class instance to use another switcher. [StandardSwitcher](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher) and [SimpleSwitcher](xref:ActiproSoftware.Windows.Controls.Docking.SimpleSwitcher) are implementations of [SwitcherBase](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase).

This sample XAML code shows how to use a simple switcher for a dock site.

```xaml
<docking:DockSite>
	<docking:DockSite.Switcher>
		<docking:SimpleSwitcher />
	</docking:DockSite.Switcher>
</docking:DockSite>
```

## The Simple Switcher

The simple switcher does not display a user interface.  Instead, pressing the switcher key gestures will toggle through the various documents and tool windows.

## The Standard Switcher

The standard switcher is more advanced and displays a user interface shown in the screenshot above.  It shows multiple columns of open tool windows and documents.  You also can use the arrow keys to navigate through the windows while the switcher is open.

There are many properties that allow you to completely customize the appearance of the switcher.  Everything from the appearance of items to the number of document columns can be set.

| Member | Description |
|-----|-----|
| [AreDocumentsVisible](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.AreDocumentsVisible) Property | Gets or sets a value indicating whether the documents are visible in the switcher.  The default value is `true`. |
| [AreToolWindowsVisible](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.AreToolWindowsVisible) Property | Gets or sets a value indicating whether the tool windows are visible in the switcher.  The default value is `true`. |
| [DocumentsColumnTitle](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.DocumentsColumnTitle) Property | Gets or sets the text to use for the documents column title. |
| [FooterTemplate](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.FooterTemplate) Property | Gets or sets the `DataTemplate` to use for the footer area of the switcher, whose content is the [SelectedWindow](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase.SelectedWindow). |
| [HeaderTemplate](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.HeaderTemplate) Property | Gets or sets the `DataTemplate` to use for the header area of the switcher, whose content is the [SelectedWindow](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.SwitcherBase.SelectedWindow). |
| [ItemContainerStyle](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ItemContainerStyle) Property | Gets or sets the `Style` to use for rendering items. |
| [ItemTemplate](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ItemTemplate) Property | Gets or sets the `DataTemplate` to use for rendering items. |
| [MaxDocumentColumnCount](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.MaxDocumentColumnCount) Property | Gets or sets the maximum number of document columns to display.  The default value is `3`. |
| [MaxRowCount](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.MaxRowCount) Property | Gets or sets the maximum number of item rows that can be displayed.  The default value is `15`. |
| [ScrollButtonStyle](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ScrollButtonStyle) Property | Gets or sets the `Style` to use for a scroll button. |
| [ScrollDownButtonContentTemplate](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ScrollDownButtonContentTemplate) Property | Gets or sets the `DataTemplate` to use for the scroll down button content. |
| [ScrollUpButtonContentTemplate](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ScrollUpButtonContentTemplate) Property | Gets or sets the `DataTemplate` to use for the scroll up button content. |
| [ShadowElevation](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ShadowElevation) Property | Gets or sets the shadow elevation.  The default value is `8`.  Set this property to `0` to disable the shadow. |
| [ToolWindowsColumnTitle](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.ToolWindowsColumnTitle) Property | Gets or sets the text to use for the tool windows column title. |

In scenarios where you don't use tool windows at all, set the [AreToolWindowsVisible](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.AreToolWindowsVisible) property to `false`.  Likewise, if you don't use MDI, set the [AreDocumentsVisible](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher.AreDocumentsVisible) property to `false`.
