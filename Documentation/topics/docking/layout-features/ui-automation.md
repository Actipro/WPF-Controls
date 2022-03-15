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

- The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) class, which is the manager of all docking functionality.

- The [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) class, which is the top-level container for all docking hierarchies.

- The [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) class, which contains two or more other containers in a docking hierarchy.

- The [DockingWindowContainerBase](xref:@ActiproUIRoot.Controls.Docking.Primitives.DockingWindowContainerBase) class, which is the base class of [TabbedMdiContainer](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiContainer) and [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer). This automation peer provides access to the child [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) elements.

- The [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost) class. This automation peer provides access to the child [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) elements.

- The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) class, which is the base class of [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).

- The [AutoHideTabGroup](xref:@ActiproUIRoot.Controls.Docking.Primitives.AutoHideTabGroup) and [AutoHideTabItem](xref:@ActiproUIRoot.Controls.Docking.Primitives.AutoHideTabItem) classes, which represent auto-hide tabs.

- The [StandardSwitcher](xref:@ActiproUIRoot.Controls.Docking.StandardSwitcher) and [StandardSwitcherItem](xref:@ActiproUIRoot.Controls.Docking.Primitives.StandardSwitcherItem) classes, which implement a standard switcher.

- The [WindowControl](xref:@ActiproUIRoot.Controls.Docking.WindowControl) class. This automation peer provides access to the child elements and programmatic access to move, resize, or close the window.

- The [AdvancedTabControl](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabControl) and [AdvancedTabItem](xref:@ActiproUIRoot.Controls.Docking.AdvancedTabItem) classes, which implement an advanced tab control.
