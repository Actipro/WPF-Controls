---
title: "Troubleshooting"
page-title: "Troubleshooting - Grids Reference"
order: 40
---
# Troubleshooting

This topic contains troubleshooting data specific to the Grids product.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as other products, please see the more general [TroubleShooting](../troubleshooting.md) topic.

## TreeListBox/TreeListView Data Not Binding Correctly

In some usage scenarios (particularly if data is late-bound) the core `ItemsControl` recycling logic might not be able to properly identify when data items change for a container.  This often happens if `DataTemplate` bindings don't give enough data to indicate a difference between bound data items.  Symptoms of this scenario are visible when you see items rendering with incorrect bound data, especially when scrolling, since in that case the item containers aren't rebinding to data or reapplying styles/templates properly while recycling.

When you see odd symptoms like this happening, simply set the `VirtualizingPanel.VirtualizationMode="Standard"` attached attribute on the root [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) (or derived) control.  "Standard" virtualization isn't quite as fast as "Recycling" virtualization, but it should solve the problem because it doesn't reuse containers.

@if (wpf) {

## Bindings in TreeListViewColumn Properties Fail

Bindings within a [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn)'s property may fail since the column object isn't a UI element and isn't within the visual tree.

This is an example of trying to bind a `ColumnName` property from a view model as the header for a column:

```xaml
<grids:TreeListViewColumn x:Name="nameColumn" Header="{Binding ColumnName}" />
```

The above scenario will likely trigger a `"Cannot find governing FrameworkElement or FrameworkContentElement for target element."` warning in Visual Studio **Output**.  Use of `ElementName` will not work.

One workaround for this is to add the binding in code instead, so that the binding source can be specified.  Here's an example:

```csharp
BindingOperations.SetBinding(nameColumn, TreeListViewColumn.HeaderProperty, new Binding("DataContext.ColumnName") { Source = treeListView });
```

}

@if (wpf) {

## Property Editor Values Might Not Be Committed When Pressing ToolBar Buttons

Many property editors use bindings that update on focus loss.  Consider a scenario where a `TextBox`-based property editor is being used to change a property value.  The user changes the value and clicks a toolbar button.  Since toolbars are in a different focus scope, no focus loss occurs in the `TextBox` so the pending value hasn't yet been committed to the bound property.  The toolbar button click handler needs the fully committed data value though.

This situation can also occur when the user is editing a property and presses a hotkey that invokes a command.  The command execute handler needs the fully committed data.

The [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[TryCommitPropertyValueEdit](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.TryCommitPropertyValueEdit*) method can be called to force a pending update to be committed, thereby ensuring that the data model is up-to-date.  We recommend that this is only done in situations where the data model must be up-to-date.

}

@if (wpf) {

## Adorner Shows on Top of Other Controls

When using data validation and the user inputs invalid data, the `Validation.ErrorTemplate` is displayed in an adorner layer. There are instances where the error template will show over top other controls, such as when the parent item is collapsed.  One known instance where this occurs is when using a `TabItem` in a category editor.

This can be solved by wrapping the content with a `AdornerDecorator`.  This allows the error template to be property hidden with its associated control.

}
