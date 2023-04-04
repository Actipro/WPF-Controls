---
title: "Selection Changes"
page-title: "Selection Changes - Wizard Navigation Features"
order: 4
---
# Selection Changes

The selected page in the Wizard can be changed programmatically and numerous cancelable selection changed events are raised whenever a page change occurs.

## Programmatically Getting or Setting the Selected Page

[Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard) has several members that are essential for getting or setting the currently selected page programmatically:

| Member | Description |
|-----|-----|
| [BacktrackToPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.BacktrackToPage*) Method | Selects the specified page using backward progress. |
| [BacktrackToPreviousPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.BacktrackToPreviousPage*) Method | Selects the previous page using backward progress. |
| [GoToNextPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.GoToNextPage*) Method | Selects the next page using forward progress. |
| [GoToPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.GoToPage*) Method | Selects the specified page using forward progress. |
| [SelectedIndex](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SelectedIndex) Property | Gets or sets the index of the selected page within the `Items` collection.  Setting this property uses a [WizardPageSelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardPageSelectionFlags) value of `Default`. |
| [SelectedPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SelectedPage) Property | Gets or sets the [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage) that is currently selected.  Setting this property uses a [WizardPageSelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardPageSelectionFlags) value of `Default`. |
| [SetSelectedIndex](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SetSelectedIndex*) Method | Sets the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[SelectedIndex](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SelectedIndex) property using the specified [WizardPageSelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardPageSelectionFlags) value. |
| [SetSelectedPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SetSelectedPage*) Method | Sets the [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[SelectedPage](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SelectedPage) property using the specified [WizardPageSelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardPageSelectionFlags) value. |

## Page Change Event Sequence

Whenever a selected page change occurs, regardless of what caused it, numerous events are raised.  The event sequence is designed so that you can process the selection change events at either the wizard or page-specific levels.

All of the events receive an informative event argument of type [WizardSelectedPageChangeEventArgs](xref:@ActiproUIRoot.Controls.Wizard.WizardSelectedPageChangeEventArgs) that has these important members:

| Member | Description |
|-----|-----|
| [Cancel](xref:@ActiproUIRoot.CancelRoutedEventArgs.Cancel) Property | Gets or sets whether to cancel the selected page change.  This property is only useful in the page changing events that are raised before the page change occurs. |
| [NewSelectedPage](xref:@ActiproUIRoot.Controls.Wizard.WizardSelectedPageChangeEventArgs.NewSelectedPage) Property | Gets the [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage) that is about to be selected. |
| [OldSelectedPage](xref:@ActiproUIRoot.Controls.Wizard.WizardSelectedPageChangeEventArgs.OldSelectedPage) Property | Gets the [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage) that is currently selected. |
| [SelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardSelectedPageChangeEventArgs.SelectionFlags) Property | Gets a [WizardPageSelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardPageSelectionFlags) indicating information about the page selection. |

The first events raised upon a page change request are page changing events.  These are cancellable events and if the `Cancel` property is set in these, then the page change will be cancelled, and no new selected page will be set.

Page changing events are raised in this order:

- [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[PreviewSelectedPageChanging](xref:@ActiproUIRoot.Controls.Wizard.Wizard.PreviewSelectedPageChanging)

- [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Unselecting](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Unselecting) on the old selected page

- [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Selecting](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Selecting) on the new selected page

- [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[SelectedPageChanging](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SelectedPageChanging)

If the page change is not cancelled, then the actual page change will occur, and a series of page changed events are raised.

Page changed events are raised in this order:

- [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[PreviewSelectedPageChanged](xref:@ActiproUIRoot.Controls.Wizard.Wizard.PreviewSelectedPageChanged)

- [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Unselected](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Unselected) on the old selected page

- [WizardPage](xref:@ActiproUIRoot.Controls.Wizard.WizardPage).[Selected](xref:@ActiproUIRoot.Controls.Wizard.WizardPage.Selected) on the new selected page

- [Wizard](xref:@ActiproUIRoot.Controls.Wizard.Wizard).[SelectedPageChanged](xref:@ActiproUIRoot.Controls.Wizard.Wizard.SelectedPageChanged)

Page changed events are a great place to add code to initialize a page that is about to be displayed.

## Page Selection Flags

The [WizardSelectedPageChangeEventArgs](xref:@ActiproUIRoot.Controls.Wizard.WizardSelectedPageChangeEventArgs).[SelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardSelectedPageChangeEventArgs.SelectionFlags) property of the event arguments gives you a lot of information about why the selected page change occurred.  This lets you better determine whether you may want to take some sort of programmatic action to override or cancel a page change.

The [WizardPageSelectionFlags](xref:@ActiproUIRoot.Controls.Wizard.WizardPageSelectionFlags) flags enumeration has these values:

| Value | Description |
|-----|-----|
| `BackwardProgress` | The page selection is backward progress. |
| `ForwardProgress` | The page selection is forward progress. |
| `Programmatic` | The page selection was made programmatically. |
| `NextPageCommand` | The page selection was made via the [WizardCommands](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands).[NextPage](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands.NextPage) command. |
| `PreviousPageCommand` | The page selection was made via the [WizardCommands](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands).[PreviousPage](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands.PreviousPage) command. |
| `GoToPageCommand` | The page selection was made via the [WizardCommands](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands).[GoToPage](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands.GoToPage) command. |
| `BacktrackToPageCommand` | The page selection was made via the [WizardCommands](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands).[BacktrackToPage](xref:@ActiproUIRoot.Controls.Wizard.WizardCommands.BacktrackToPage) command. |
| `Default` | The default flags of `Programmatic` and `ForwardProgress`. |

## Forward and Backward Progress

Wizard tracks a notion of forward and backward progress.  This is used in several internal areas such as [stack-based page sequencing](page-sequencing.md) and for [transition effects](../appearance-features/transition-effects.md).

When moving "forward" through pages, it is assumed you are doing things like clicking the **Next** button.  When moving "backward" through pages, it is assumed you are doing things like clicking the **Back** button.

Transitions use this information so that they can be set to perform their effects in one direction when moving forward, or in another direction when moving backward.

## Overriding Selected Page Changes to a Select a Different Page

Sometimes you may wish to abort default page sequencing and provide your own decision-based sequencing instead.

This code shows how to alter the page that will be selected next when a specific page is set and a `CheckBox` is checked.  The new selected page will be programmatically set in this scenario.  Note that if the `CheckBox` is not checked, the default page sequencing will be used.

```csharp
private void wizard_SelectedPageChanging(object sender, WizardSelectedPageChangeEventArgs e) {
	if (e.OldSelectedPage == decisionBasedPage) {
		if (alternatePathCheckBox.IsChecked == true) {
			e.NewSelectedPage = alternatePage;
		}
	}
}
```

## Canceling Selected Page Changes / Validation

As mentioned above, selected page changes may be canceled in any of the page changing events.  Functionality of cancelling and not moving to a new page is generally used for data validation purposes.

This code shows how to cancel a page change at the wizard level when a specific page is set and a name `TextBox` is empty.

```csharp
private void wizard_SelectedPageChanging(object sender, WizardSelectedPageChangeEventArgs e) {
	if (e.OldSelectedPage == dataEntryPage) {
		if (nameTextBox.Text.Length == 0) {
			MessageBox.Show("Please enter a name.");
			e.Cancel = true;
		}
	}
}
```
