---
title: "Inline Editing"
page-title: "Inline Editing - Tree Control Features"
order: 10
---
# Inline Editing

Items support inline editing, which means that a special UI mode can be entered where their text or other properties are in an editable state.

## Starting Editing

If an item is editable (see below for how to provide this), pressing <kbd>F2</kbd> while it is focused, or single tapping on an [EditableContentControl](../../shared/windows-controls/editablecontentcontrol.md) in the item's template will trigger edit mode.  This is often kept in sync with an item's `IsEditing` property.

> [!NOTE]
> When edit mode is active, no arrow key-based item navigation will be enabled.  Therefore, it is important to only mark items as editable (see below) that have a control in the item template that will respond to `IsEditing` property changes (from <kbd>F2</kbd> pressed or the item is single tapped) and that also exits the edit mode when appropriate (i.e., focus loss).  The default implementation of [EditableContentControl](../../shared/windows-controls/editablecontentcontrol.md) does all this for you if you two-way bind its [IsEditing](xref:@ActiproUIRoot.Controls.EditableContentControl.IsEditing) property.  If you wish to manage the `IsEditing` state on your own, do not mark the item as editable.

## Selecting EditableContentControl TextBox Text

The default [EditableContentControl](../../shared/windows-controls/editablecontentcontrol.md) template uses a `TextBox` when in edit mode.  The virtual [EditableContentControl](xref:@ActiproUIRoot.Controls.EditableContentControl).[SelectText](xref:@ActiproUIRoot.Controls.EditableContentControl.SelectText*) method is called as edit mode is entered, with its default implementation set to select all the text.

This method can be overridden to select a certain range of text instead.  A common scenario is when editing filenames, to only select the actual name (without extension) portion of the filename.

## Handling EditableContentControl Edited Text

When a value being edited in an [EditableContentControl](../../shared/windows-controls/editablecontentcontrol.md) is committed, the virtual [SetContentAfterEditing](xref:@ActiproUIRoot.Controls.EditableContentControl.SetContentAfterEditing*) method is called and the edited content is passed in.  The default implementation of this method simply sets the `Content` property to the specified edited content.

This method can be overridden to coerce the edited content as needed.  Or if the edited content is invalid, you could choose not to call the base method, which would effectively cancel the edit.  If you wish to keep edit mode active, set the `IsEditing` property back to `true`.  This is sometimes done if there is an invalid value.

## Using an Editable Item Template

When an item's text should be editable, use a [EditableContentControl](../../shared/windows-controls/editablecontentcontrol.md) in its item template instead of a `TextBlock` to display the text.  The text will display like a normal `TextBlock` by default but will switch to a `TextBox` for editing when in edit mode.

Here's an example of a `DataTemplate` that includes a `Image` and [EditableContentControl](../../shared/windows-controls/editablecontentcontrol.md).  This example assumes that you are using an item type that has `ImageSource`, `Name`, and `IsEditing` properties.  The `IsEditing` property has a two-way binding so that it can update the item's property as the end user exits edit mode.

```xaml
<DataTemplate>
	<StackPanel Orientation="Horizontal">
		<Image Width="16" Height="16" Source="{Binding ImageSource}" Stretch="None" VerticalAlignment="Center" />
		<shared:EditableContentControl Margin="2,0,0,0" Content="{Binding Name, Mode=TwoWay}" IsEditing="{Binding IsEditing, Mode=TwoWay}" />
	</StackPanel>
</DataTemplate>
```

By specifying a `ContentTemplate` on the content control, you can surround the bound text content with other text.  For instance, if `Name` has value `"Foo"`, instead of just showing `"Foo"` in the item template, you could display the text as `"Solution 'Foo'"`.  Here's an example:

```xaml
<DataTemplate>
    <StackPanel Orientation="Horizontal">
		<Image Width="16" Height="16" Source="{Binding ImageSource}" Stretch="None" VerticalAlignment="Center" />
		<shared:EditableContentControl Margin="2,0,0,0" Content="{Binding Name, Mode=TwoWay}" IsEditing="{Binding IsEditing, Mode=TwoWay}">
			<shared:EditableContentControl.ContentTemplate>
				<DataTemplate>
					<StackPanel Orientation="Horizontal">
						<TextBlock Text="Solution '" VerticalAlignment="Center" />
						<TextBlock Text="{Binding}" VerticalAlignment="Center" />
						<TextBlock Text="'" VerticalAlignment="Center" />
					</StackPanel>
				</DataTemplate>
			</shared:EditableContentControl.ContentTemplate>
		</shared:EditableContentControl>
    </StackPanel>
</DataTemplate>
```

When an item with the above item template is edited, the surrounding `"Solution '"` and `"'"` text disappears and the `TextBox` occupies the entire space, editing the `Name` value.

## Providing Whether an Item Is Editable

An item will only enter edit mode if the adapter returns that it is editable.  The editable state is provided to the control by calls to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetIsEditable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsEditable*) method (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)).

A `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsEditableBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsEditableBinding) property to tell how an item should retrieve the editable state.  The [GetIsEditable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsEditable*) method will use that binding if it is supplied, and will otherwise return the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsEditableDefault](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsEditableDefault) property value.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetIsEditable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsEditable*) method instead with custom logic to retrieve the appropriate value.

This example code shows how to override the method for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override bool GetIsEditing(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.IsEditing : false);
}
```

## Syncing Item Editing State

The control needs to two-way communicate with items regarding their editing state.  This is accomplished by calls to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetIsEditing](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsEditing*) and [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[SetIsEditing](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsEditing*) methods (via [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter)).

A two-way `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsEditingBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsEditingBinding) property to tell how an item should retrieve the editable state.  The [GetIsEditing](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsEditing*) method will use that binding if it is supplied, and will otherwise return `false`.  The [SetIsEditing](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsEditing*) method will also use that binding if it is supplied.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetIsEditable](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetIsEditable*) and [SetIsEditing](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.SetIsEditing*) methods instead with custom logic to retrieve/set the appropriate values.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[IsEditingPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsEditingPath) property specifies the property name to watch for in case your item implements `INotifyPropertyChanged`.  When a change to that property is detected, the updated value will be requested by the control.  Note that even though a binding can be used via the [IsEditingBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsEditingBinding) property, for performance reasons, it is just used temporarily to get/set the value in the adapter methods' default implementations.  The binding doesn't persist, and that's why the [IsEditingPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.IsEditingPath) property is required for property change notifications.

This example code shows how to override the methods for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override bool GetIsEditing(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.IsEditing : false);
}

public override void SetIsEditing(object item, bool value) {
	var model = item as TreeNodeModel;
	if (model != null)
		model.IsEditing = value;
}
```
