---
title: "UI Automation"
page-title: "UI Automation - Wizard Layout, Globalization, and Accessibility Features"
order: 5
---
# UI Automation

Wizard follows the WPF accessibility model for UI automation.

## What is UI Automation?

Microsoft UI Automation is the new accessibility framework for Microsoft Windows.  It addresses the needs of assistive technology products and automated test frameworks by providing programmatic access to information about the user interface (UI).

Wizard implements automation peers for the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard), [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage), and [WizardButtonContainer](xref:ActiproSoftware.Windows.Controls.Wizard.WizardButtonContainer) controls.  These automation peers allow for programmatic access to those UI elements and their child controls.

[Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard) implements a selection control pattern and [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) implements a selection item pattern.  Selection change events raise the appropriate events on the automation peers.
