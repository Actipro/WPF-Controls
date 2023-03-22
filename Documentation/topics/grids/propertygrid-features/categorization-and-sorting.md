---
title: "Categorization and Sorting"
page-title: "Categorization and Sorting - PropertyGrid Features"
order: 13
---
# Categorization and Sorting

Properties displayed in the property grid can be categorized into groups and sorted.

## Categorization

### Overview

The properties presented by the property grid can be grouped together into categories, which is controlled by the [IsCategorized](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.IsCategorized) property.

In reality, the [data factory](data-models.md) performs the categorization of the properties based on that option.  When enabled, the built-in data factory examines all data object properties for `CategoryAttribute` (or `DisplayAttribute` with a `GroupName`).  These values get stored in the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[Category](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Category) property for the property models that are generated.  Then the data factory groups all the property models with the same category together as children of a new [ICategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryModel).

### Expansion

By default, all categories are automatically expanded.  This default can be changed via information detailed in the [Expandability](expandability.md) topic.

### Nested Categories

Properties can also be categorized into nested categories.  For example, if a property specifies a category of "One\\Two", then by default the property will be in a top-level category titled "One\\Two".  By setting [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[AreNestedCategoriesSupported](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.AreNestedCategoriesSupported) to `true`, the `PropertyGrid` will interpret the category name as a path to the desired category.  In the previous example, a top-level category titled "One" will be created, with a nested category titled "Two" (which will contain the property).

If two properties specify the same root category name, then the nested categories & properties will be combined. Continuing the example from above, if another property specifies a category of "One" then it will be contained in the category titled "One" along with the nested category "Two".

Any number of nested categories can be specified, at any level.  A category titled "One\\Two\\Three" would create three categories, with each of the latter two being nested.

The [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[MiscCategoryName](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.MiscCategoryName) property also supports the nested category syntax. For example, if set to "One\\Two" then any miscellaneous properties will be placed in the category titled "Two", which is nested in the top-level category "One".

## Sorting

The items presented by the property grid can be sorted using any custom logic.  By default, the items will be sorted by this data in ascending order:

- [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[SortImportance](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortImportance)

- [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[SortOrder](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortOrder)

- Numeric [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[DisplayName](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.DisplayName) index (i.e., `[0]`, `[1]`, etc.) if applicable

- [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[DisplayName](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.DisplayName)

> [!NOTE]
> The [DataModelSortImportance](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataModelSortImportance) enumeration defines several levels of sorting, which allows categories to be sorted before category editors and properties, and category editors before properties.  Statically-defined properties (via [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[Properties](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.Properties)) can easily change their sort importance by setting the [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel).[SortImportance](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortImportance) property.

The default sort comparer is what implements the above logic and can be completely customized.  It is specified via the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[SortComparer](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.SortComparer) property.  This is initialized by default with an instance of [DataModelSortComparer](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataModelSortComparer), which is a class implementing `IComparer<IDataModel>`.  The class has boolean properties to optionally disable any of the sorting mechanisms described above and the logic to sort each one is implemented in a separate virtual method that can be overridden with custom logic.

Set the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[SortComparer](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.SortComparer) property to `null` to prevent default sorting.

The [data factory](data-models.md) is what locates and applies sorting based on a sort comparer that was found.  This occurs in the virtual [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[SortDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.SortDataModels*) method.

Every [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) that is generated by the data factory has a [SortComparer](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortComparer) property.  If that property is left `null`, then the property grid default sort comparer is used for child models of the data model.  If a sort comparer is specified for a particular data model (such as a category), then that model's direct children will be sorted using the sort comparer.

While any custom sort comparer must inherit [DataModelSortComparer](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataModelSortComparer), any of its virtual `Compare*` methods may be overridden with custom logic tailored to your needs.
