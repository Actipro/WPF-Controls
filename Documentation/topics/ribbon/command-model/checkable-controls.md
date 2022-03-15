---
title: "Interaction with Checkable Controls"
page-title: "Interaction with Checkable Controls - Ribbon Command Model"
order: 2
---
# Interaction with Checkable Controls

Many controls (such as [Button](../controls/interactive/button.md)) supoort a checked state.  So the question is, how do you sync up the checked state of a control when there might be multiple control instances that toggle on the same criteria.

An example is the `ToggleBold` command.  There might be an instance of a `ToggleBold` button in the ribbon and another in the [Quick Access Toolbar](../controls/miscellaneous/quickaccesstoolbar.md).  They both need to be synced up so that when the selection in the document is bold, the two controls appear checked.

This is done via a [ICheckableCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.ICheckableCommandParameter).  By default, the built-in checkable controls assign a [CheckableCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.CheckableCommandParameter) instance (which implements [ICheckableCommandParameter](xref:@ActiproUIRoot.Controls.Ribbon.Input.ICheckableCommandParameter)) to their `CommandParameter` property.

When the `CanExecute` event of the command is handled in your code, set the [Handled](xref:@ActiproUIRoot.Controls.Ribbon.Input.ICheckableCommandParameter.Handled) property of the parameter to `true` and set the [IsChecked](xref:@ActiproUIRoot.Controls.Ribbon.Input.ICheckableCommandParameter.IsChecked) property to the checked state value.

This code shows how to handle a `ToggleBold` command's `CanExecute` handler to set the checked state of a ribbon control.  Note that in this sample code, the `SelectionBold` property is assumed to return whether the selection in the document is currently bold.

```csharp
private void OnToggleBoldCanExecute(object sender, CanExecuteRoutedEventArgs e) {
	ICheckableCommandParameter parameter = e.Parameter as ICheckableCommandParameter;
	if (parameter != null) {
		parameter.Handled = true;
		parameter.IsChecked = this.SelectionBold;
	}
}
```

Since the ribbon controls require the command's can-execute method logic to fire for updating of the checked state, it may be necessary to raise the command's `CanExecuteChanged` event following a programmatic change that affects the checked state, especially if the command is not a routed command.
