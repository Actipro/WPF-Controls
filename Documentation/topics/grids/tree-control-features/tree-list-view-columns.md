---
title: "Columns (TreeListView)"
page-title: "Columns (TreeListView) - Tree Control Features"
order: 3
---
# Columns (TreeListView)

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) control supports one or more columns of cells that can be displayed within each item (row).  Columns can be resized, reordered, hidden, frozen, and much more.

Columns are created by adding [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn) instances to the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[Columns](xref:@ActiproUIRoot.Controls.Grids.TreeListView.Columns) collection.  Properties set on the column instance affect all cells within that column.

## Header and Cell Appearance

### Header Appearance

For a basic text column header, simply set the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[Header](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.Header) property to a text value.

If you need to display additional content such as images, you can set the [HeaderTemplate](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderTemplate) property to a custom `DataTemplate`.  The [HeaderTemplateSelector](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderTemplateSelector) property can be used if you have a `DataTemplateSelector` instance for multiple columns that is responsible for supplying a `DataTemplate` for each column based on the column's [Header](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.Header) content.

The [TreeListViewColumnHeader](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumnHeader) control that is used to render the chrome of a column header can have an additional style applied via the [HeaderContainerStyle](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderContainerStyle) property.  This allows you to customize the appearance of the header chrome.

The [HeaderHorizontalAlignment](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderHorizontalAlignment) property lets you set the horizontal alignment of content within the header cell.  This is useful in cases such as numeric columns where you may wish to right-align text.

The [HeaderCellPadding](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderCellPadding) property controls the amount of padding between the header cell's border and its content.

The [HeaderCellBorderThickness](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderCellBorderThickness) property sets the thickness of the border around the header cell.

### Cell Appearance

Each column has [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CellTemplate](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellTemplate) and [CellTemplateSelector](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellTemplateSelector) properties that can be set to generate templated content for each cell within that column.  See the [Getting Started](getting-started.md) topic for some examples of setting the cell template properties.

The [CellHorizontalAlignment](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellHorizontalAlignment) and [CellVerticalAlignment](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellVerticalAlignment) properties let you set the horizontal and vertical alignments of content within the item cells.  This is useful in cases such as numeric columns where you may wish to right-align text.  The default horizontal alignment is `Stretch` and the default vertical alignment is `Center`.

The [CellPadding](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellPadding) property controls the amount of padding between each item cell's border and its content.

The [CellBorderThickness](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellBorderThickness) property sets the thickness of the border around each item cell.

## Widths

A column's width is specified via the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[Width](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.Width) property.  This property accepts a `GridLength` value and effectively works the same as a `Grid` panel's column width specification where a value can be a specific pixel length, `Auto`, or can be a star (*) sized length.

In the case of auto and star-sized columns, the [MinWidth](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.MinWidth) and [MaxWidth](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.MaxWidth) values are also examined.  These properties set minimum and maximum values for any calculated widths.

The current pixel width of the column is returned by the [ActualWidth](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.ActualWidth) property.

### Pixel Width

Pixel width columns will always render at the specified width value.

Here's an example of specifying a pixel width for a column:

```xaml
<grids:TreeListView>
	<grids:TreeListView.Columns>
		<grids:TreeListViewColumn Header="Name" Width="150" />
		<grids:TreeListViewColumn Header="Description" Width="250" />
	</grids:TreeListView.Columns>
</grids:TreeListView>
```

### Auto Width

Auto width columns will auto-size to their content.  The [AutoWidthMode](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.AutoWidthMode) property governs whether the auto-size includes header-only, items-only, or both (the default).  It can be set to another [TreeListViewColumnAutoWidthMode](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumnAutoWidthMode) value to alter this behavior.  It is common to set minimum and/or maximum width values on these sorts of columns.

Here's an example of specifying an auto width (which is also the default) for a column, ensuring it is at least `50px`, and indicating that only item cells should be used for auto-width calculation:

```xaml
<grids:TreeListView>
	<grids:TreeListView.Columns>
		<grids:TreeListViewColumn Header="Name" Width="Auto" MinWidth="50" AutoWidthMode="ItemsOnly" />
		<grids:TreeListViewColumn Header="Description" Width="250" />
	</grids:TreeListView.Columns>
</grids:TreeListView>
```

### Star Width

Star-sized columns fill the remaining available space.  It is generally a good idea to set minimum and/or maximum width values on these sorts of columns.

Here's an example of specifying star-sized widths for columns (the second column will be twice as wide as the first):

```xaml
<grids:TreeListView>
	<grids:TreeListView.Columns>
		<grids:TreeListViewColumn Header="Name" Width="*" MinWidth="50" />
		<grids:TreeListViewColumn Header="Description" Width="2*" MinWidth="50" />
	</grids:TreeListView.Columns>
</grids:TreeListView>
```

## Resizing

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[CanColumnsResize](xref:@ActiproUIRoot.Controls.Grids.TreeListView.CanColumnsResize) property (defaults to `true`) is a global property that indicates whether columns can resize by default.  This value can be overridden on an instance basis by setting the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CanResize](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CanResize) property to a non-null value.

When a column is allowed to resize, the end user can click and drag a gripper on the right side of the column's header to resize it.  Note that this feature is only available for star-sized columns when there is another visible star-sized column after it in the same control.

The [SizeToFitCommand](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.SizeToFitCommand) property returns a command that can be wired up to menu items to size the column to fit its visible contents.

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ColumnsResized](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ColumnsResized) event is raised after the end user resizes columns.

## Reordering

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[CanColumnsReorder](xref:@ActiproUIRoot.Controls.Grids.TreeListView.CanColumnsReorder) property (defaults to `false`) is a global property that indicates whether columns can be reordered by default.  This value can be overridden on an instance basis by setting the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CanReorder](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CanReorder) property to a non-null value.

When a column is allowed to be reordered, the end user can click and drag the column header to a new location.  Drop indicators will show where the drop will occur.

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ColumnReordered](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ColumnReordered) event is raised after the end user reorders a column.

## Visibility

A column's visibility can be controlled via the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[IsVisible](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.IsVisible) property.  The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ColumnIsVisibleChanged](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ColumnIsVisibleChanged) event is raised after a column's visibility changes.

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[CanColumnsToggleVisibility](xref:@ActiproUIRoot.Controls.Grids.TreeListView.CanColumnsToggleVisibility) property (defaults to `false`) is a global property that indicates whether columns can toggle their visibility by default.  This value can be overridden on an instance basis by setting the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CanToggleVisibility](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CanToggleVisibility) property to a non-null value.

When a column is allowed to have its visibility toggled, the end user can right-click the column header to show a context menu (see a section below for more on column header context menus) and the default context menu provides an option for toggling visibility.

The [ToggleVisibilityCommand](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.ToggleVisibilityCommand) property returns a command that can be wired up to menu items to toggle the visibility of the column.

## Frozen Columns

Frozen columns are one or more columns that appear fixed to the left side of the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView) control and are outside of the scrollable portion of the control.

Set the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[FrozenColumnCount](xref:@ActiproUIRoot.Controls.Grids.TreeListView.FrozenColumnCount) to a value larger than `0` to indicate the number of columns to freeze.  Make sure the columns you intend to freeze are the first columns in the [Columns](xref:@ActiproUIRoot.Controls.Grids.TreeListView.Columns) collection.  The count doesn't examine the current visibility state of the columns, only their index in the collection.

## Specifying the Expansion Column

Set the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ExpansionColumnIndex](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ExpansionColumnIndex) property to the index of the column that should contain the expander buttons and be indented based on nesting level.  This property defaults to `0`, meaning use the first column.

The index doesn't examine the current visibility state of the columns, so it is possible to hide the expansion column.  It's good practice to set the [CanToggleVisibility](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CanToggleVisibility) to `false` on the expansion column index if you are enabling column visibility toggling in general.

## Handling Column Header Taps

When a column header is tapped and is not being dragged at the time, the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ColumnHeaderTapped](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ColumnHeaderTapped) event is raised.  Handle this event to take some appropriate action, such as sorting items by the values presented in that column.

## Column Header Context Menus

When the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[IsDefaultColumnHeaderContextMenuEnabled](xref:@ActiproUIRoot.Controls.Grids.TreeListView.IsDefaultColumnHeaderContextMenuEnabled) property is set to `true` (the default), a default context menu will be generated when a column header is right-clicked.  The default context menus has size-to-fit options and column visibility options, all based on if the related features are enabled.

If you are using column header templates instead of setting the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[Header](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.Header) properties to string values, be sure to set the [Title](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.Title) property to a string that describes the column title.  This string will be used in the default column header context menu and/or other UI.

The [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ColumnHeaderMenuRequested](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ColumnHeaderMenuRequested) event is raised immediately before the menu is displayed.  This allows you to customize the menu as needed with custom logic, or prevent it from showing at all.

## Size-to-Fit

The [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[SizeToFit](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.SizeToFit*) method can be called to size a column to its visible contents based on the [AutoWidthMode](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.AutoWidthMode) setting.

A companion [SizeToFitCommand](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.SizeToFitCommand) property is available, returning a command that can be used in menu items to call the [SizeToFit](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.SizeToFit*) method.

## Grid Lines

While column headers have grid lines by default due to the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[HeaderCellBorderThickness](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.HeaderCellBorderThickness) property being `0,0,1,1`, item cells don't have grid lines by default.

Set the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[CellBorderThickness](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.CellBorderThickness) property to `0,0,1,1` to enable grid lines around the item cells too.

## Column Index

The index of the column within its owner control can be retrieved via the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[Index](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.Index) property.

## Sort Direction

If a custom external sorting mechanism is used, the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn).[SortDirection](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.SortDirection) property can display a glyph indicating ascending or descending sorting is active for the column.  The property is nullable and can be set to a null reference to remove the sort glyph.

Handle the [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView).[ColumnHeaderTapped](xref:@ActiproUIRoot.Controls.Grids.TreeListView.ColumnHeaderTapped) event to know when a column header is clicked, so that you can programmatically alter the [SortDirection](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn.SortDirection) property appropriately (which updates the sort arrow glyph) and manually adjust the order of item children in your item nodes.

@if (wpf) {

## Important Note on Data Bindings in Column Properties

Since the [TreeListViewColumn](xref:@ActiproUIRoot.Controls.Grids.TreeListViewColumn) class isn't a UI element, bindings specified in its properties may fail with a data error.  Please see the [Troubleshooting](../troubleshooting.md) topic for more information on the problem, including an example and a workaround.

}
