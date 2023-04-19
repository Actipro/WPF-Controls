---
title: "Default Actions"
page-title: "Default Actions - Tree Control Features"
order: 6
---
# Default Actions

A default action occurs when an item is double-tapped or <kbd>Enter</kbd> is pressed.  This action can be fully customized to execute custom logic.

## Built-In Default Actions

The only built-in default action is for expandable items to toggle their expansion state.  You can cancel this default action by handling the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemDefaultActionExecuting](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemDefaultActionExecuting) event and setting the event args' `Cancel` property to `true`.

## Customizing Default Actions

In addition to blocking a default action from occurring as above, the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemDefaultActionExecuting](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemDefaultActionExecuting) event can be used for notifications of when to execute a custom action.  For instance, when including [check boxes on items](item-checkboxes.md), you might want to toggle the checked state of the item.  Take whatever custom action you wish to take in response to that event.  The event is passed arguments of type [TreeListBoxItemEventArgs](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs), which specifies the related [Item](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Item) and its UI [Container](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemEventArgs.Container).

If the [ItemDefaultActionExecuting](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemDefaultActionExecuting) event doesn't cancel the default action, then the control will look to see if a [TreeListBoxItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem).[DefaultActionCommand](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem.DefaultActionCommand) exists.  If so, it will try to execute that command.

@if (wpf) {

The [TreeListBoxItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem).[DefaultActionCommand](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem.DefaultActionCommand) can be set in a [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).`PrepareContainerForItemOverride` override.  It alternatively can be set via `Setter` binding in a [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).`ItemContainerStyle` style.

}

@if (winrt) {

The [TreeListBoxItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem).[DefaultActionCommand](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem.DefaultActionCommand) can be set in a [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).`PrepareContainerForItemOverride` override.

}

Otherwise, the built-in default action will execute, if appropriate.
