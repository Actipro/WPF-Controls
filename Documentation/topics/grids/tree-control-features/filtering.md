---
title: "Filtering"
page-title: "Filtering - Tree Control Features"
order: 13
---
# Filtering

Filtering allows a tree to be narrowed down to only display items that match filter criteria.  A powerful data filtering mechanism is provided that uses string, boolean, and predicate-based logic to filter items.  The underlaying data model is not altered in any way, only the view is.

## Using Filters

To activate filtering functionality, set the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[IsFilterActive](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.IsFilterActive) property to `true`.  It is `false` by default.

When filtering is active, the control will iterate over every item in the tree and will call the [TreeListBoxItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter).[Filter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.Filter*) method for each item.  That method should return a [DataFilterResult](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterResult):

- **Included** - The item passed all filter conditions and is included in the filter.  Tree descendants will also be examined by the filter.

- **IncludedWithDescendants** - The item passed all filter conditions and is included in the filter.  Tree descendants will not be examined by the filter, but will all be included as well.

- **IncludedByDescendants** - The item did not pass all filter conditions and will only be included in the filter if one or more of its tree descendants are included.  Tree descendants will also be examined by the filter.

- **Excluded** - The item did not pass all filter conditions and will not be included in the filter.  None of its tree descendants will be examined by the filter.

> [!NOTE]
> When a filter includes an item, that item will be visible in the tree.  When a filter excludes an item, that item will not be visible in the tree.

The [Filter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.Filter*) method is virtual and at a low level can be overridden to use custom filter logic.

However, this generally is not necessary since there is a higher level filtering mechanism that is provided via the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[DataFilter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.DataFilter) property.  By default, the [TreeListBoxItemAdapter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter).[Filter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBoxItemAdapter.Filter*) method will get the [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) instance (if set) in the [DataFilter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.DataFilter) property and will call its [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter).[Filter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter.Filter*) method to get the filter result.  If no [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) is available, the method will fall back to returning `DataFilterResult.Included`.

The main benefit of this approach is that there are several built-in [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) implementations that can be plugged into the [DataFilter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.DataFilter) property.  Custom implementations can be made as well.  This keeps the filtering mechanism very modular.

Another benefit of using [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) objects for filtering is that each has an [IsEnabled](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter.IsEnabled) property that can be set to `false` to prevent that filter from being applied.  This is especially useful when building complex filter groups, as described below.  You still want to make sure the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[IsFilterActive](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.IsFilterActive) property is set back to `false` when no filtering is needed, since that will improve performance.

> [!TIP]
> It is most efficient to set and configure the [DataFilter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.DataFilter) property prior to setting the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[IsFilterActive](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.IsFilterActive) property to `true`.

## Automatically Including Descendants

The [DataFilterBase](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterBase) class, used as a base class for the default [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) implementations, has a [IncludedFilterResult](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterBase.IncludedFilterResult) property that returns the filter result to use when an item is included in the filter.  This defaults to `DataFilterResult.Included` but can alternatively be set to `DataFilterResult.IncludedWithDescendants` if you wish for all descendant items automatically be included without needing to pass the filter themselves.

All of the built-in filters return the [IncludedFilterResult](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterBase.IncludedFilterResult) property value when a filter includes an item.

## Predicate Filters

The [PredicateFilter](xref:ActiproSoftware.Windows.Data.Filtering.PredicateFilter) class, a built-in implementation of [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter), uses a custom `Predicate<object>` to return filter results.

Simply set the [Predicate](xref:ActiproSoftware.Windows.Data.Filtering.PredicateFilter.Predicate) property to the predicate to use.  If the predicate returns `true`, the [IncludedFilterResult](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterBase.IncludedFilterResult) property value will be returned by the filter.  Otherwise, `DataFilterResult.IncludedByDescendants` will be returned.

This example code shows how to set a predicate filter that assumes each item is a `TreeNodeModel` (your custom model type) and checks to see if the `Name` starts with "Foo".

```csharp
treeListBox.DataFilter = new PredicateFilter(i => ((TreeNodeModel)i).Name.StartsWith("Foo"));
```

## String Filters

The abstract [StringFilterBase](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase) class, a built-in implementation of [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter), can be used to easily construct a filter that is based on a string value comparison.  Regular expressions are even supported.  It has several properties that factor into the comparison.

### Target Value

The [Value](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.Value) property needs to be set to the operand value used by the filter.  The is the target value against which the comparison will be made.

### Operation

The [Operation](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.Operation) property is set to a [StringFilterOperation](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterOperation) value that determines the kind of comparison operation to perform.  The default value is `Contains`, but all of these operations are available: `Contains`, `NotContains`, `StartsWith`, `NotStartsWith`, `EndsWith`, `NotEndsWith`, `Equals`, `NotEquals`, `RegexMatch`, and `NotRegexMatch`.

### Culture and Case Sensitivity

The [StringComparison](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.StringComparison) property is set to a `StringComparison` value that determines how to compare the strings in terms of culture and case sensitivity.  The default value is `CurrentCultureIgnoreCase`, but can be set to other options if you would prefer ordinal or case sensitive comparisons.

### Regular Expressions

When [Operation](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.Operation) is set to `RegexMatch` or `NotRegexMatch`, regular expression matching will be used.  The regular expression should be specified in the [Value](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.Value) property.  If a [StringComparison](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.StringComparison) option is used that ignores case, then the regular expression matching will be told to ignore case as well.

If any other `RegexOptions` (matching options) need to be set, they can be assigned to the [RegexOptions](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase.RegexOptions) property.

### Implementing a String Filter

This example code shows how to create a `TreeNodeModelStringFilter` class that knows how to examine a `TreeNodeModel` (your custom model type) and check its `Name` property.

```csharp
public class TreeNodeModelStringFilter : StringFilterBase {
	public override bool Filter(object item, object context) {
		return (this.FilterByString(((TreeNodeModel)item).Name, this.Value) ? this.IncludedFilterResult : DataFilterResult.IncludedByDescendants);
	}
}
```

Once the string filter has been created, it can be implemented in XAML like this to filter all models whose `Name` starts with `Foo`:

```xaml
<grids:TreeListBox ...>
	<grids:TreeListBox.DataFilter>
		<local:TreeNodeModelStringFilter x:Name="stringFilter" Operation="StartsWith" Value="Foo" />
	</grids:TreeListBox.DataFilter>
	...
</grids:TreeListBox>
```

The filter's value or other properties can be dynamically updated to refresh the filter results.  For instance, with string filters it is common for a `TextBox` in the user interface to update a string filter's value as text is typed.

## Boolean Filters

The abstract [BooleanFilterBase](xref:ActiproSoftware.Windows.Data.Filtering.BooleanFilterBase) class, a built-in implementation of [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter), can be used to easily construct a filter that is based on a boolean value comparison.  It has several properties that factor into the comparison.

### Target Value

The [Value](xref:ActiproSoftware.Windows.Data.Filtering.BooleanFilterBase.Value) property needs to be set to the operand value used by the filter.  The is the target value against which the comparison will be made.

### Operation

The [Operation](xref:ActiproSoftware.Windows.Data.Filtering.BooleanFilterBase.Operation) property is set to a [BooleanFilterOperation](xref:ActiproSoftware.Windows.Data.Filtering.BooleanFilterOperation) value that determines the kind of comparison operation to perform.  The default value is `Equals`, but all of these operations are available: `Equals`, `NotEquals`, `And`, `Or` and `Xor`.

### Implementing a Boolean Filter

This example code shows how to create a `TreeNodeModelBooleanFilter` class that knows how to examine a `TreeNodeModel` (your custom model type) and check its `IsActive` property.

```csharp
public class TreeNodeModelBooleanFilter : BooleanFilterBase {
	public override bool Filter(object item, object context) {
		return (this.FilterByBoolean(((TreeNodeModel)item).IsActive) ? this.IncludedFilterResult : DataFilterResult.IncludedByDescendants);
	}
}
```

Once the boolean filter has been created, it can be implemented in XAML like this to filter all models whose `IsActive` property is `true`:

```xaml
<grids:TreeListBox ...>
	<grids:TreeListBox.DataFilter>
		<local:TreeNodeModelBooleanFilter x:Name="booleanFilter" Operation="Equals" Value="True" />
	</grids:TreeListBox.DataFilter>
	...
</grids:TreeListBox>
```

The filter's value or other properties can be dynamically updated to refresh the filter results.  For instance, with boolean filters it is common for a `CheckBox` in the user interface to update a boolean filter's value as its checked state changes.

## Filter Groups

The [DataFilterGroup](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterGroup) class, a built-in implementation of [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter), can be used to perform a logical AND or OR operation on any number of child filters.

> [!NOTE]
> A filter group can contain other filters or filter groups.  Combine that with the ability for filters to be individually enabled or disabled (via [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter).[IsEnabled](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter.IsEnabled)) and complex filter tree logic can be easily implemented.

### Child Filters

The [Children](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterGroup.Children) property is the collection of child filters to use.

### Operation

The [Operation](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterGroup.Operation) property is set to a [DataFilterGroupOperation](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterGroupOperation) value that determines the kind of comparison operation to perform.  The default value is `And` (all child filters must pass), but `Or` (any child filter must pass) can be chosen as well.

### Implementing a Filter Group

This example code shows how to use the filters described above in a filter group, where both must pass for an item to be included:

```xaml
<grids:TreeListBox ...>
	<grids:TreeListBox.DataFilter>
		<shared:DataFilterGroup>
			<local:TreeNodeModelStringFilter x:Name="stringFilter" Operation="StartsWith" Value="Foo" />
			<local:TreeNodeModelBooleanFilter x:Name="booleanFilter" Operation="Equals" Value="True" />
		</shared:DataFilterGroup>
	</grids:TreeListBox.DataFilter>
	...
</grids:TreeListBox>
```

## Custom Filters

While the built-in classes described above will suit most needs, custom [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) implementations can be written as well.  The easiest way to do this is to make a class that inherits [DataFilterBase](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterBase), as that class already implements most of the details of the [IDataFilter](xref:ActiproSoftware.Windows.Data.Filtering.IDataFilter) interface.  Your only task then is to implement the [Filter](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterBase.Filter*) method with custom logic.

Your custom filter can then be set to the [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[DataFilter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.DataFilter) property or can be used in a [DataFilterGroup](xref:ActiproSoftware.Windows.Data.Filtering.DataFilterGroup).

It is also possible to inherit the other base classes like [StringFilterBase](xref:ActiproSoftware.Windows.Data.Filtering.StringFilterBase) or [BooleanFilterBase](xref:ActiproSoftware.Windows.Data.Filtering.BooleanFilterBase) and augment them.

## Auto-Expanding to Filtered Items

The [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[AutoExpandItemsOnFilter](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.AutoExpandItemsOnFilter) property can be set to `true` to ensure that when filtering occurs and an item is included in the filter, all of its ancestor items will be expanded automatically.  The propertly's default value is `false`, meaning don't enable this behavior.

This feature is useful when you want to ensure that filtered items are always fully visible immediately and can't be hidden by collapsed ancestor items.

When this feature is enabled, as soon as filtering is deactivated, the expansion state of the items is restored to what it was before filtering activated.

## Empty Content

The [TreeListBox](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox).[EmptyContentTemplate](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.EmptyContentTemplate) property can be set to a `DataTemplate` that should be displayed in the control whenever there are no visible items.  If additional data needs to be passed into the data template, set it to the [EmptyContent](xref:ActiproSoftware.Windows.Controls.Grids.TreeListBox.EmptyContent) property.

This feature is useful when filters can possibly hide all items and a message should be displayed in their place.
