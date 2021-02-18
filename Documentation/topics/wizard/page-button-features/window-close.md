---
title: "Window Close On Finish/Cancel Button Clicks"
page-title: "Window Close On Finish/Cancel Button Clicks - Wizard Page and Button Features"
order: 6
---
# Window Close On Finish/Cancel Button Clicks

By default, Wizard will attempt to close the containing `Window` when the Finish or Cancel buttons are clicked.

Wizard can also be configured to set the `DialogResult` of the `Window` before it is closed (not enabled by default).

## Preventing The Finish/Cancel Buttons From Performing Their Default Behavior

Both the Finish and Cancel buttons raise numerous events when they are clicked.

The Finish button raises these cancellable events in sequential order:

- [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[PreviewFinish](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.PreviewFinish)

- [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[Finish](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.Finish) on the selected page

- [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[Finish](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.Finish)

If at the end of those events the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[FinishButtonClosesWindow](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.FinishButtonClosesWindow) property is set to `true`, the containing `Window`'s `DialogResult` will be set to `true` and the `Window` will be closed.

The Cancel button raises these cancellable events in sequential order:

- [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[PreviewCancel](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.PreviewCancel)

- [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[Cancel](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.Cancel) on the selected page

- [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[Cancel](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.Cancel)

If at the end of those events the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[CancelButtonClosesWindow](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.CancelButtonClosesWindow) property is set to `true`, the containing `Window`'s `DialogResult` will be set to `false` and the `Window` will be closed.

## Setting the Window DialogResult

By default, no `DialogResult` will be set on the parent `Window` when the Finish or Cancel buttons are clicked.  However by changing the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[IsWindowDialogResultUpdatingEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.IsWindowDialogResultUpdatingEnabled) property to `true`, the Finish button will set the `DialogResult` to `true` and the Cancel button will set it to `false`.

This feature should only be enabled when the wizard is on a modal `Window`.
