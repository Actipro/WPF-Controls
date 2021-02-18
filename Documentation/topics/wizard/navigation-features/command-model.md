---
title: "Command Model"
page-title: "Command Model - Wizard Navigation Features"
order: 2
---
# Command Model

Wizard is designed to use the WPF command model for all wizard functions, which separates the action implementation from the user interface control that executes it.  This allows for multiple and disparate sources to invoke the same centralized command logic.

All of the wizard buttons are hooked up to the appropriate command.  For instance the Next button uses the [NextPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.NextPage).

## Command List

All of these commands are available via static properties on the [WizardCommands](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands) class:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>


</thead>
<tbody>

<tr>
<td>

[BacktrackToPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.BacktrackToPage) Property

</td>
<td>

Gets the command that is used to backtrack to the specified page.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[BacktrackToPage](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.BacktrackToPage*) method provides a wrapper for executing this command.

</td>
</tr>

<tr>
<td>

[Cancel](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.Cancel) Property

</td>
<td>

Gets the command that is used to cancel the wizard.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[ExecuteCancel](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.ExecuteCancel*) method provides a wrapper for executing this command.

</td>
</tr>

<tr>
<td>

[Finish](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.Finish) Property

</td>
<td>

Gets the command that is used to finish the wizard.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[ExecuteFinish](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.ExecuteFinish*) method provides a wrapper for executing this command.

</td>
</tr>

<tr>
<td>

[GoToPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.GoToPage) Property

</td>
<td>

Gets the command that is used to go directly to the specified page.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[GoToPage](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.GoToPage*) method provides a wrapper for executing this command.

</td>
</tr>

<tr>
<td>

[Help](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.Help) Property

</td>
<td>

Gets the command that is used to display help for the wizard.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[ExecuteHelp](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.ExecuteHelp*) method provides a wrapper for executing this command.

</td>
</tr>

<tr>
<td>

[NextPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.NextPage) Property

</td>
<td>

Gets the command that is used to advance to the next wizard page.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[GoToNextPage](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.GoToNextPage*) method provides a wrapper for executing this command.

</td>
</tr>

<tr>
<td>

[PreviousPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.PreviousPage) Property

</td>
<td>

Gets the command that is used to backtrack to the previous wizard page.

The [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[BacktrackToPreviousPage](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.BacktrackToPreviousPage*) method provides a wrapper for executing this command.

</td>
</tr>

</tbody>
</table>

## NextPage and PreviousPage

These commands are used by the wizard's Next and Back buttons.

The next page command follows these execution steps:

- If a [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[NextPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.NextPage) value is set on the selected page, that page is navigated to using forward progress and the command logic quits.

- Otherwise, the next enabled sequential page, if any, will be navigated to using forward progress.

The next page command examines the page-specific [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[NextButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.NextButtonEnabled) property to determine whether it is enabled.  If that property is `null`, the [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[NextPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.NextPage) property of the selected page will be examined to see if a specified next page is available.  If that property is `null`, a check will be made to see if there is another enabled wizard page in sequential order after the selected page.

The previous page command follows these execution steps:

- If a [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[PreviousPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.PreviousPage) value is set on the selected page, that page is navigated to using backward progress and the command logic quits.

- Otherwise, the previous enabled sequential page, if any, will be navigated to using backward progress.

The previous page command examines the page-specific [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[BackButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.BackButtonEnabled) property to determine whether it is enabled.  If that property is `null`, the [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[PreviousPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.PreviousPage) property of the selected page will be examined to see if a specified previous page is available.  If that property is `null`, a check will be made to see if there is another enabled wizard page in sequential order before the selected page.

Both commands raise the appropriate selection changed events when a page change occurs.

This XAML code shows how to use the [NextPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.NextPage) command on a `Button`:

```xaml
<Button Command="wizard:WizardCommands.Next">Next</Button>
```

## Finish and Cancel

These commands are used by the wizard's Finish and Cancel buttons.

The finish command follows these execution steps:

- Raises the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[PreviewFinish](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.PreviewFinish) event.

- Raises the [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[Finish](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.Finish) event on the selected page.

- Raises the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[Finish](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.Finish) event.

- If the event was not cancelled, the `DialogResult` of the containing `Window` will attempt to be set to `true`, and the `Window` will attempt to close.

The finish command resolves between the page-specific [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[FinishButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.FinishButtonEnabled) property and the wizard-default [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[FinishButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.FinishButtonEnabled) property to determine whether it is enabled.

The cancel command follows these execution steps:

- Raises the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[PreviewCancel](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.PreviewCancel) event.

- Raises the [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[Cancel](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.Cancel) event on the selected page.

- Raises the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[Cancel](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.Cancel) event.

- If the event was not cancelled, the `DialogResult` of the containing `Window` will attempt to be set to `false`, and the `Window` will attempt to close.

The cancel command resolves between the page-specific [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[CancelButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.CancelButtonEnabled) property and the wizard-default [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[CancelButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.CancelButtonEnabled) property to determine whether it is enabled.

This XAML code shows how to use the [Finish](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.Finish) command on a `Button`:

```xaml
<Button Command="wizard:WizardCommands.Finish">Finish</Button>
```

## Help

This command is used by the wizard's Help button.

The help command follows these execution steps:

- Raises the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[PreviewHelp](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.PreviewHelp) event.

- Raises the [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[Help](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.Help) event on the selected page.

- Raises the [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[Help](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.Help) event.

The help command resolves between the page-specific [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage).[HelpButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage.HelpButtonEnabled) property and the wizard-default [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).[HelpButtonEnabled](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard.HelpButtonEnabled) property to determine whether it is enabled.

This XAML code shows how to use the [Help](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.Help) command on a `Button`:

```xaml
<Button Command="wizard:WizardCommands.Help">Help</Button>
```

## GoToPage and BacktrackToPage

These commands make it extremely easy to navigate directly to a specific page by using forward or backward progress.

The go to page and backtrack to page commands both use a command parameter.  The command's parameter may be set to three different types of data:

- A reference to a [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) within the parent [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).

- The name of another [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) within the parent [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).

- An integer indicating the index of a [WizardPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardPage) within the parent [Wizard](xref:ActiproSoftware.Windows.Controls.Wizard.Wizard).

The go to page command follows these execution steps:

- If a valid command parameter has been passed that indicates a page (see above), that page is navigated to using forward progress.

The go to page command examines the supplied command parameter to determine whether it is enabled.  See above for a description of the allowed command parameter values.

The backtrack to page command follows these execution steps:

- If a valid command parameter has been passed that indicates a page (see above), that page is navigated to using backward progress.

The backtrack to page command examines the supplied command parameter to determine whether it is enabled.  See above for a description of the allowed command parameter values.

Both commands raise the appropriate selection changed events when a page change occurs.

This XAML code shows how to use the [GoToPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.GoToPage) command on a `Hyperlink` to automatically jump to a page with a `Name` of `targetPage`:

```xaml
<TextBlock><Hyperlink Command="wizard:WizardCommands.GoToPage" CommandParameter="targetPage">Go to the target page</Hyperlink></TextBlock>
```

This XAML code shows how to use the [BacktrackToPage](xref:ActiproSoftware.Windows.Controls.Wizard.WizardCommands.BacktrackToPage) command on a `Hyperlink` to automatically jump back to the first page in the wizard:

```xaml
<TextBlock><Hyperlink Command="wizard:WizardCommands.BacktrackToPage" CommandParameter="0">Go to the first page</Hyperlink></TextBlock>
```
