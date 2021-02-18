---
title: "UI Automation"
page-title: "UI Automation - Ribbon Layout, Globalization, and Accessibility Features"
order: 9
---
# UI Automation

Ribbon controls follow the WPF accessibility model for UI automation.

## What is UI Automation?

Microsoft UI Automation is the new accessibility framework for Microsoft Windows.  It addresses the needs of assistive technology products and automated test frameworks by providing programmatic access to information about the user interface (UI).

Ribbon implements automation peers for the following classes:

- The [Ribbon](xref:ActiproSoftware.Windows.Controls.Ribbon.Ribbon) class, which provides access to the child elements.

- The [ButtonBase](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.Primitives.ButtonBase) class, which is the base class for several other controsl. This automation peer provides the ability to invoke and toggle (when checkable) the control.

- The [CheckBox](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.CheckBox) class, which provides the ability to toggle the checked state.

- The [ComboBox](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.ComboBox) class, which provides the ability to get or set the current text (when editable), expand or collapse the drop-down list, view selected item (when not editable), or get the items available in the drop-down list.

- The [PopupButton](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.PopupButton) class, which provides the ability to expand or collapse the popup.

- The [RadioButton](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.RadioButton) class, which provides the ability to toggle the checked state.

- The [SplitButton](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.SplitButton) class, which provides the ability to expand or collapse the popup and invoke the button.

- The [TextBox](xref:ActiproSoftware.Windows.Controls.Ribbon.Controls.TextBox) class, which provides the ability to get or set the current text.

- Several other Ribbon classes implement an automation peer that provides access to the child elements.
