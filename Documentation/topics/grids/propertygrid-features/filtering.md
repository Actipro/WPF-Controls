---
title: "Filtering"
page-title: "Filtering - PropertyGrid Features"
order: 17
---
# Filtering

The property grid includes advanced filtering support, which can be used to narrow down the items that are displayed.  Built-in filters can be applied to several properties and can be combined into groups.  Filter groups can perform a logical AND or OR operation on the child filters, and several levels of groups can be used for total control.  String filters offer several comparison options including regular expressions.

## Filter Infrastructure

The [Tree Controls Filtering](../tree-control-features/filtering.md) topic describes the core filtering mechanism used by property grid.  Please review that topic first since [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) indirectly inherits [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox) and harnesses the same core filtering mechanism for its own filtering.

Property grid comes with a couple built-in filters that are designed to filter common properties on property models.  These are described in the following sections.

> [!NOTE]
> To activate filtering functionality, set the [IsFilterActive](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.IsFilterActive) property to `true`.  It is `false` by default.  For optimal performance, keep it `false` unless you have a [DataFilter](xref:@ActiproUIRoot.Controls.Grids.TreeListBox.DataFilter) set and you are actively want to filter displayed properties.

## Built-in PropertyModelStringFilter

The [PropertyModelStringFilter](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelStringFilter) class is an [IDataFilter](xref:@ActiproUIRoot.Data.Filtering.IDataFilter) implementation for [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) string properties.  It inherits [StringFilterBase](xref:@ActiproUIRoot.Data.Filtering.StringFilterBase) and all of the information in the [Tree Controls Filtering](../tree-control-features/filtering.md) topic related to "String Filters" applies.  There are numerous comparison operators available, and even regular expressions are supported.

The [PropertyModelStringFilter](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelStringFilter) class introduces a [Source](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelStringFilter.Source) property that accepts a [PropertyModelStringFilterSource](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelStringFilterSource) value and defaults to `DisplayName`.

That enumeration has these values:

| Value | Description |
|-----|-----|
| `Category` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[Category](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Category) property should be used as the source. |
| `Description` | The [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[Description](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.Description) property should be used as the source. |
| `DisplayName` | The [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[DisplayName](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.DisplayName) property should be used as the source. |
| `Name` | The [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[Name](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.Name) property should be used as the source. |
| `ValueType` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[ValueType](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueType) property should be used as the source. |

This code shows how to only show properties that contain "is" in their display names:

```xaml
<grids:PropertyGrid ... IsFilterActive="True">
	<grids:PropertyGrid.DataFilter>
		<grids:PropertyModelStringFilter x:Name="stringFilter" Source="DisplayName" Operation="Contains" Value="is" />
	</grids:PropertyGrid.DataFilter>
</grids:PropertyGrid>
```

## Built-in PropertyModelBooleanFilter

The [PropertyModelBooleanFilter](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBooleanFilter) class is an [IDataFilter](xref:@ActiproUIRoot.Data.Filtering.IDataFilter) implementation for [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) boolean properties.  It inherits [BooleanFilterBase](xref:@ActiproUIRoot.Data.Filtering.BooleanFilterBase) and all of the information in the [Tree Controls Filtering](../tree-control-features/filtering.md) topic related to "Boolean Filters" applies.  There are numerous comparison operators available.

The [PropertyModelBooleanFilter](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBooleanFilter) class introduces a [Source](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBooleanFilter.Source) property that accepts a [PropertyModelBooleanFilterSource](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBooleanFilterSource) value and defaults to `IsReadOnly`.

That enumeration has these values:

| Value | Description |
|-----|-----|
| `CanResetValue` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[CanResetValue](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.CanResetValue) property should be used as the source. |
| `HasStandardValues` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[HasStandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.HasStandardValues) property should be used as the source. |
| `IsLimitedToStandardValues` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[IsLimitedToStandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsLimitedToStandardValues) property should be used as the source. |
| `IsMergeable` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[IsMergeable](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsMergeable) property should be used as the source. |
| `IsModified` | The [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[IsModified](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.IsModified) property should be used as the source. |
| `IsReadOnly` | The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[IsReadOnly](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsReadOnly) property should be used as the source. |

This code shows how to apply a filter that excludes read-only properties:

```xaml
<grids:PropertyGrid ... IsFilterActive="True">
	<grids:PropertyGrid.DataFilter>
		<grids:PropertyModelBooleanFilter x:Name="booleanFilter" Source="IsReadOnly" Operation="Equals" Value="False" />
	</grids:PropertyGrid.DataFilter>
</grids:PropertyGrid>
```

## Group Filters

Multiple filters like [PropertyModelStringFilter](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelStringFilter) and [PropertyModelBooleanFilter](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBooleanFilter) can be combined in a group with logical AND or OR operators to return a filter result.

See the [Tree Controls Filtering](../tree-control-features/filtering.md) topic for details on using group filters.

This code shows how to only show properties that contain "is" in their display names and aren't read-only:

```xaml
<grids:PropertyGrid ... IsFilterActive="True">
	<grids:PropertyGrid.DataFilter>
		<shared:DataFilterGroup x:Name="filterGroup">
			<grids:PropertyModelStringFilter x:Name="stringFilter" Source="DisplayName" Operation="Contains" Value="is" />
			<grids:PropertyModelBooleanFilter x:Name="booleanFilter" Source="IsReadOnly" Operation="Equals" Value="False" />
		</shared:DataFilterGroup>
	</grids:PropertyGrid.DataFilter>
</grids:PropertyGrid>
```

## Predicate or Custom Filters

Predicates or completely custom filters can be created as well.  See the [Tree Controls Filtering](../tree-control-features/filtering.md) topic for details on using predicate or custom filters.
