---
title: "Getting Started"
page-title: "Getting Started - Tree Control Features"
order: 2
---
# Getting Started

This topic covers everything you need to get started, including high-level information about how item adapters work, how to bind to data, and how to provide customized UI for your [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) and [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) items.

After reading through this topic, see other topics to learn more about expansion, selection, and other advanced features offered by the tree controls.

## Item Adapters

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) and [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) controls are geared for MVVM usage, knowing that items can be any custom type and there could be many thousands of items.  A common practice in many third-party tree controls is to force items to implement a certain interface.  We specifically designed Actipro's tree controls so that your items would never have to implement an interface from our library.  After all, data libraries requiring dependencies on UI libraries are frowned upon.  In order to support a powerful API, keep performance very fast, and avoid any virtualization quirks that are present in some competitive libraries, the tree controls use an adapter design.

An item adapter is a class of type [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter) that can communicate between the control and items, and is set to the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[ItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.ItemAdapter) property.  It has methods that [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) and [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) call into to retrieve data and set various states, and is essential to activating many control features.

At a minimum, an item adapter must tell the control how to retrieve the child items of a particular item.  See the next section for an example of accomplishing that.  But an item adapter can do much more, such as getting/setting expansion, managing selection, triggering editing, handing drag/drop, etc.  An item adapter can also do things like return [search text](text-searching.md), [item paths](item-paths.md), manage [drag and drop](drag-drop.md), and more.

## Recommended Item and Adapter Samples

The "Common" folder in the Grids portion of samples contains full source sample implementations of tree node models (e.g., the `TreeNodeModel` class) that can be used as a starting point for your own items.  There also is a `DefaultTreeListBoxItemAdapter` class that implements method overrides for all adapter methods and provides optimal performance (no binding usage) when working with items of type `TreeNodeModel`.  Using those classes, you can load hundreds of thousands of items nearly instantly.

## Binding to Data

Every tree has a root and that root item should be set to the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[RootItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.RootItem) property.  The [RootItemChanged](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.RootItemChanged) event is raised when the root item is changed.

Do not ever set the `ItemsSource` property directly.  As the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[RootItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.RootItem) and its tree is examined by the item adapter, internal [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) code updates the control's `ItemsSource` property based on the currently-expanded items.  This design allows the control to quickly display a tree structure in a "flat" `ItemsControl`.

The root item is always expanded and can optionally be displayed in the tree (see below), but by default, it is not visible.  Whenever the control needs to query for child items, it will do so through the item adapter by a call to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[GetChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetChildren*) method.

A `Binding` can be set in XAML to the [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[ChildrenBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ChildrenBinding) property to tell how an item should retrieve child items.  The [GetChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetChildren*) method will use that binding if it is supplied, and will otherwise return `null`.  Please note that bindings aren't as performant as code, so for large trees or if you see performance issues, it is recommended to override the [GetChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetChildren*) method instead with custom logic to retrieve the appropriate value.

The [TreeListBoxItemAdapter](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter).[ChildrenPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ChildrenPath) property specifies the property name to watch for in case your item implements `INotifyPropertyChanged`.  When a change to that property is detected, the updated value will be requested by the control.  Note that even though a binding can be used via the [ChildrenBinding](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ChildrenBinding) property, for performance reasons, it is just used temporarily to get the value in the adapter method's default implementation.  The binding doesn't persist, and that's why the [ChildrenPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.ChildrenPath) property is required for property change notifications.  This is only needed if the children collection property itself will ever change.  The control will natively monitor any observable collection returned from [GetChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetChildren*) for changes to its items.

Here's an example specifying a `ChildrenBinding` right in XAML.  Note that this is not a recommended approach if you have very large trees or see any performance issues.  In those scenarios, it's better to override the [GetChildren](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItemAdapter.GetChildren*) method directly.

```xaml
<grids:TreeListBox>
	<grids:TreeListBox.ItemAdapter>
		<grids:TreeListBoxItemAdapter ChildrenBinding="{Binding Children}" ChildrenPath="Children" />
	</grids:TreeListBox.ItemAdapter>
</grids:TreeListBox>
```

This example code shows how to override the method for a custom item type `TreeNodeModel`.  This is not only the most performant way of providing a result, but also supports returning different results for different item types or based on item state.

```csharp
public override IEnumerable GetChildren(object item) {
	var model = item as TreeNodeModel;
	return (model != null ? model.Children : null);
}
```

## Item Templates

Since items can be any custom type, it is essential that you supply an item template containing UI for your items.

For the single-column [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) control, this can be done by setting a `DataTemplate` to the [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).`ItemTemplate` property.  The `DataTemplate` can contain any elements and those elements can be bound to properties on your item.  All of this UI will appear indented based on item depth level and with an expander to the left of it.

Here's an example of a common `DataTemplate` that includes an `Image` and a `TextBlock`.  This example assumes that you are using an item type that has `ImageSource` and `Name` properties.

```xaml
<grids:TreeListBox>
	<grids:TreeListBox.ItemTemplate>
		<DataTemplate>
			<StackPanel Orientation="Horizontal">
				<Image Width="16" Height="16" Source="{Binding ImageSource}" Stretch="None" VerticalAlignment="Center" />
				<TextBlock Text="{Binding Name}" VerticalAlignment="Center" />
			</StackPanel>
		</DataTemplate>
	</grids:TreeListBox.ItemTemplate>
</grids:TreeListBox>
```

For the multi-column [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) control, the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).`ItemTemplate` property is not used.  Instead, a `DataTemplate` can be set for each defined column via the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CellTemplate](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellTemplate) property .

Here's an example of how to specify the same item template as above, but for a [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) column:

```xaml
<grids:TreeListView>
	<grids:TreeListView.Columns>
		<grids:TreeListViewColumn Header="Name">
			<DataTemplate>
				<StackPanel Orientation="Horizontal">
					<Image Width="16" Height="16" Source="{Binding ImageSource}" Stretch="None" VerticalAlignment="Center" />
					<TextBlock Text="{Binding Name}" VerticalAlignment="Center" />
				</StackPanel>
			</DataTemplate>
		</grids:TreeListViewColumn>
	</grids:TreeListView.Columns>
</grids:TreeListView>
```

If you need your appearance to vary per item, see the next section.

## Per-Item Appearance Customization

For the single-column [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) control, the `ItemTemplateSelector` property may be set to a custom `DataTemplateSelector` instance that picks a `DataTemplate` to use based on the supplied item.  Use of template selectors can be important when different items require a different appearance.  For instance, one `DataTemplate` might just show an image and text, while another may show text and a micro chart.  Several examples of template selectors are found in the product QuickStarts.

For the multi-column [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) control, the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CellTemplateSelector](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellTemplateSelector) property can be set for each column, instead of the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).`ItemTemplateSelector` property.

You also can set the `ItemContainerStyleSelector` property to a custom `StyleSelector` instance that picks a [TreeListBoxItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBoxItem) class or [TreeListViewItem](xref:@ActiproUIRoot.Controls.Grids.TreeListViewItem)`Style` to use based on the supplied item.  Normally only template selectors are ever required when doing per-item appearance customization, but the higher-level style selection option is there as well.

## Root Item Visibility

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[IsRootItemVisible](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.IsRootItemVisible) property (defaulting to `false`) determines if the [RootItem](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.RootItem) is visible in the control.  When `true`, it will be the sole top-level item.  When `false`, its child items will be the top-level items.

## Ensuring An Item Is Visible

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[BringItemIntoView](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.BringItemIntoView*) and [BringItemIntoViewByFullPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.BringItemIntoViewByFullPath*) methods can be used to ensure that an item is visible and scrolled into view.

Both methods will expand any collapsed ancestor items so that the target item is visible.  The [BringItemIntoView](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.BringItemIntoView*) method can be a bottleneck in very large trees where the specified node hasn't yet been exposed to the control by its ancestor nodes all being expanded.  In that scenario, a full tree search is required to locate the node.  For that case, it is recommended to use the [BringItemIntoViewByFullPath](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.BringItemIntoViewByFullPath*) method that searches by full path instead, since that can be much faster.  See the [Item Paths](item-paths.md) topic for more information on working with paths.

## Item Indentation

There are two properties that determine the indentation of items for each depth level.  Expanders are generally rendered within the indentation space.

The primary property is [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[IndentIncrement](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.IndentIncrement), and that is the step indentation increment amount between depth levels.

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[TopLevelIndent](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.TopLevelIndent) is used for the top-level items only, to determine how far in they indent.  Set this property to zero when top-level items aren't expandable (see the [Expandability](expandability.md) topic for more information).  That ensures that the top-level items don't have empty white space on the left when they never show expanders.

## Tree Lines

The [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).[HasTreeLines](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.HasTreeLines) property can be set to `true` to show tree lines that connect items and reflect the tree hierarchy.
