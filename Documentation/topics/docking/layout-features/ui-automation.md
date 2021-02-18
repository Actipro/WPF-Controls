---
title: "UI Automation"
page-title: "UI Automation - Docking & MDI Layout, Globalization, and Accessibility Features"
order: 11
---
# UI Automation

Actipro Docking & MDI follows the accessibility model for UI automation.

## What is UI Automation?

Microsoft UI Automation is the new accessibility framework for Microsoft Windows.  It addresses the needs of assistive technology products and automated test frameworks by providing programmatic access to information about the user interface (UI).

Docking & MDI implements automation peers for the following classes:

- The [DockSite](xref:ActiproSoftware.Windows.Controls.Docking.DockSite) class, which is the manager of all docking functionality.

- The [DockHost](xref:ActiproSoftware.Windows.Controls.Docking.DockHost) class, which is the top-level container for all docking hierarchies.

- The [SplitContainer](xref:ActiproSoftware.Windows.Controls.Docking.SplitContainer) class, which contains two or more other containers in a docking hierarchy.

- The [DockingWindowContainerBase](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.DockingWindowContainerBase) class, which is the base class of [TabbedMdiContainer](xref:ActiproSoftware.Windows.Controls.Docking.TabbedMdiContainer) and [ToolWindowContainer](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindowContainer). This automation peer provides access to the child [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) elements.

- The [StandardMdiHost](xref:ActiproSoftware.Windows.Controls.Docking.StandardMdiHost) class. This automation peer provides access to the child [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) elements.

- The [DockingWindow](xref:ActiproSoftware.Windows.Controls.Docking.DockingWindow) class, which is the base class of [DocumentWindow](xref:ActiproSoftware.Windows.Controls.Docking.DocumentWindow) and [ToolWindow](xref:ActiproSoftware.Windows.Controls.Docking.ToolWindow).

- The [AutoHideTabGroup](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.AutoHideTabGroup) and [AutoHideTabItem](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.AutoHideTabItem) classes, which represent auto-hide tabs.

- The [StandardSwitcher](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher) and [StandardSwitcherItem](xref:ActiproSoftware.Windows.Controls.Docking.Primitives.StandardSwitcherItem) classes, which implement a standard switcher.

- The [WindowControl](xref:ActiproSoftware.Windows.Controls.Docking.WindowControl) class. This automation peer provides access to the child elements and programmatic access to move, resize, or close the window.

- The [AdvancedTabControl](xref:ActiproSoftware.Windows.Controls.Docking.AdvancedTabControl) and [AdvancedTabItem](xref:ActiproSoftware.Windows.Controls.Docking.AdvancedTabItem) classes, which implement an advanced tab control.
