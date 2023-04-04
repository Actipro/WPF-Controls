---
title: "UI Automation"
page-title: "UI Automation - Navigation Layout, Globalization, and Accessibility Features"
order: 4
---
# UI Automation

Navigation controls follow the WPF accessibility model for UI automation.

## What is UI Automation?

Microsoft UI Automation is the new accessibility framework for Microsoft Windows.  It addresses the needs of assistive technology products and automated test frameworks by providing programmatic access to information about the user interface (UI).

Navigation implements automation peers for the following classes:

- The [ExpanderBar](xref:@ActiproUIRoot.Controls.Navigation.ExpanderBar) class, which is the base class of [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) and [ExplorerBar](xref:@ActiproUIRoot.Controls.Navigation.ExplorerBar). Also, the pane items of those controls are WPF `Expander` controls, which already have UI automation capabilities. These automation peers allow for programmatic access to those UI elements and their child controls.

- The [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb) class. Also the [AnimatedProgressBar](xref:@ActiproUIRoot.Controls.AnimatedProgressBar) class and [PopupButton](xref:@ActiproUIRoot.Controls.PopupButton) class, which are used in the [Breadcrumb](xref:@ActiproUIRoot.Controls.Navigation.Breadcrumb), have UI automation capabilities.
