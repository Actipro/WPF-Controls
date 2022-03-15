---
title: "Item CheckBoxes"
page-title: "Item CheckBoxes - Tree Control Features"
order: 8
---
# Item CheckBoxes

`CheckBox` controls can be inserted into item templates to make items checkable, and default actions can be implemented to toggle checked states for double-taps and `Enter` key presses.

## Displaying CheckBoxes

`CheckBox` controls aren't built into the control itself but rather are part of the `DataTemplate` you supply for the items.

Here's an example of a `DataTemplate` that includes a `CheckBox`.  This example assumes that you are using an item type that has `IsThreeState`, `IsChecked`, and `Name` properties.  The `IsChecked` property has a two-way binding so that it can update the item's property as the end user toggles the `CheckBox`.

```xaml
<DataTemplate>
	<StackPanel Orientation="Horizontal">
		<CheckBox Margin="0,0,4,0" VerticalAlignment="Center"
				  Visibility="{Binding IsCheckable, Converter={StaticResource BooleanToVisibilityConverter}}" 
				  IsThreeState="{Binding IsThreeState}" IsChecked="{Binding IsChecked, Mode=TwoWay}" 
				  />
						
		<TextBlock Text="{Binding Name}" TextTrimming="CharacterEllipsis" VerticalAlignment="Center" />
	</StackPanel>
</DataTemplate>
```

## Using Default Actions to Toggle Checked State

Default actions are executed when an item is double-tapped or `Enter` is pressed.  When using `CheckBox` controls in item templates, it's generally good practice to handle item [default actions](default-actions.md) and toggle the item's `IsChecked` property appropriately.

This example code shows how to handle a default action (via the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemDefaultActionExecuting](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemDefaultActionExecuting) event) and toggle the checked state on a `CheckableTreeNodeModel` item type that has `IsCheckable`, `IsThreeState`, and `IsChecked` properties.

```csharp
private void OnTreeListBoxItemDefaultActionExecuting(object sender, TreeListBoxItemEventArgs e) {
	var model = e.Item as CheckableTreeNodeModel;
	if ((model != null) && (model.IsCheckable)) {
		e.Cancel = true;

		// Toggle the checked state
		switch (model.IsChecked) {
			case true:
				model.IsChecked = (model.IsThreeState ? (bool?)null : false);
				return;
			case null:
				model.IsChecked = false;
				return;
			default:
				model.IsChecked = true;
				return;
		}
	}
}
```
