---
title: "Data Models and Factories"
page-title: "Data Models and Factories - PropertyGrid Features"
order: 5
---
# Data Models and Factories

There are several ways to populate the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) control with items (categories, properties, etc.).  This topic covers the various alternatives, along with descriptions of the data models used behind the scenes and how data factories supply the data models.

The resulting data models are passed to [property editors](property-editors.md) and other UI-related layers to create the user interface.

## Setting the DataObject or DataObjects Properties

The property grid can easily present the properties of any .NET object or objects by setting the [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) or [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) properties, respectively.  When one of these two properties is set, the current data factory examines the data object(s) and generates a property data model for each of the accessible properties.  Data factories are described in detail in a later section of this topic and can be highly customized, which is one of the best parts of the property grid's design.

@if (winrt) {

On the @@PlatformName platform, reflection is used by the default data factory to enumerate and examine properties.

}

@if (wpf) {

On the @@PlatformName platform, type descriptors are used by the default data factory to enumerate and examine properties.

}

If [categorization](categorization-and-sorting.md) is enabled, category data models are also created by the data factory.  When [category editors](category-editors.md) are supported, category editor data models can also be generated.

The entire hierarchy of data models (properties, categories, category editors, etc.) is then passed back to the property grid control and is bound to the control as the data source for display.  The [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) control inherits [TreeListView](xref:@ActiproUIRoot.Controls.Grids.TreeListView), which inherits [TreeListBox](xref:@ActiproUIRoot.Controls.Grids.TreeListBox).  Thus, the property grid harnesses and expands upon the fast data population and display framework provided by those base classes.  And as such, most of the topics in the [Tree Control Features](../tree-control-features/index.md) area of the documentation can also apply to property grid.

### Mutually-Exclusive

The [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) and [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) properties are mutually exclusive and are automatically synchronized by the property grid.  Therefore, if `DataObject` is set then `DataObjects` is automatically set to an object array containing only one item, the data object.  Conversely, if `DataObjects` is set then `DataObject` is automatically set to the first item in the object array, if any.  This functionality mirrors the Windows Forms `PropertyGrid` control.

### Setting DataObject Example

This code shows the base XAML that creates a simple [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) used to modify the properties of a single .NET object, which is the most common usage scenario:

```xaml
<grids:PropertyGrid DataObject="{Binding YourVMProperty}" />
```

### Setting DataObjects Example

This code shows the base XAML that creates a simple [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) used to modify the properties of multiple .NET objects:

```xaml
<grids:PropertyGrid DataObjects="{Binding YourVMProperty}" />
```

When the [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) property is used and includes more than one .NET object, the data factory only generates property data models for "common properties".  Common properties are properties that have the same name and `Type` and are present on all the data objects.  These common properties are merged into a single property data model and presented as a single item in the property grid control.  This allows any number of .NET objects to be presented or modified all at once.

See the [Multiple Objects](multiple-objects.md) topic for more information.

## Clearing the DataObject Property When Done with PropertyGrid

When the property grid is attached to data objects, it monitors their properties with property value change events so that it can update itself when any changes are detected.  The property grid internals use weak event handlers for these notifications to minimize any memory leaks.  That being said, memory can start to build up in certain scenarios if multiple instances of property grid controls are used without properly clearing out the data objects when the property grid control instances are no longer needed.

> [!NOTE]
> It's important to clear the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) property by setting it to `null` when the property grid is no longer needed.  Setting this property also clears the [DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) property and ensures that all property value change events are properly detached, thereby ensuring all memory is fully released.

There is no need to set the [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) property to `null` first when changing from examining one data object to another data object on the same property grid control.  As a new [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) value is set, all attachments to property value change events on the previous data object are removed.

### Using the CanClearDataObjectOnUnload Property

The [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[CanClearDataObjectOnUnload](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.CanClearDataObjectOnUnload) property can be set to `true` to automatically clear the [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) property when the property grid control is unloaded.

The [CanClearDataObjectOnUnload](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.CanClearDataObjectOnUnload) property should not be set to `true` if a property grid or its ancestor hierarchy can get temporarily unloaded.  For instance, when a property grid is in a tab control, the property grid is unloaded when the parent tab is deselected.  Likewise, when a property grid is a docking window, the property grid is temporarily unloaded during dock operations as layout changes occur.  In these sorts of scenarios, it is better to manually clear the [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) property when the primary UI (such as a main window) is closed.

### Manually Clearing the DataObject Property

For scenarios described above where it doesn't make sense to use the [CanClearDataObjectOnUnload](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.CanClearDataObjectOnUnload) property, the [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject) property should be manually set to `null` when the primary UI (such as a main window) is closed and the property grid will no longer be needed.

## Explicitly Defining Properties

[PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) has a [Properties](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.Properties) collection property that accepts one or more [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) items.  The [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) class is generally used in this collection since it lets you set all the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) properties yourself.

Any property models in this collection will be appended to any property models that the data factory generates for [DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject)/[DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects).  Then all of the properties will be categorized (if appropriate) together and the property grid control will render the user interface for the data models.  Explicitly-defined property can be used without bound data objects too.

This code shows the base XAML that you can use to create a simple property grid with two statically-defined properties via the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[Properties](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.Properties) collection:

```xaml
<grids:PropertyGrid>
	<grids:PropertyModel x:Name="property1" ValueType="system:String" DisplayName="Text" Value="A string value" Description="Description for the property." />
	<grids:PropertyModel x:Name="property2" ValueType="system:Boolean" DisplayName="Boolean" Value="True" Description="Description for the property." />
</grids:PropertyGrid>
```

The property models can be named per the XAML above and their values checked in code.  Alternatively, bindings to `Value` and some other properties are supported since [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel)'s properties are all dependency properties.

Further, the [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) class inherits `FrameworkElement` to allow easy XAML-based data binding.  Note that no visual/input features of the `FrameworkElement` base class are used in any way, but inheriting that minimum object is necessary for XAML-based data binding without complex workarounds.

@if (wpf) {

This code shows the implementation of a `Binding`:

```xaml
<grids:PropertyGrid>
	<grids:PropertyModel ValueType="system:String" DisplayName="Text" Value="{Binding ElementName=boundTextBox, Path=Text, Mode=TwoWay}" />
</grids:PropertyGrid>
```

}

@if (wpf) {

### Automated PropertyModel Initialization

While the techniques described above for specifying a [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) and its properties will work, it can get to be tedious because there are often a number of [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) properties that must be specified to support desired editing functionality.

With this in mind, there is special functionality in the [TypeDescriptorFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory) class that looks for [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) instances with their [CanAutoConfigure](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel.CanAutoConfigure) properties set to `true`.  As long as there is a `Binding` to a target property on their [Value](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel.Value) properties, the data factory will find related property information and configure many of the other [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) properties for you.

The following code shows an example of letting many of the property model's properties be auto-configured by the data factory.

```xaml
<grids:PropertyGrid IsCategorized="False" SortComparer="{x:Null}">
	<grids:PropertyModel CanAutoConfigure="True" Target="{Binding Mode=TwoWay, ElementName=boundTextBox, Path=Text}" />
	<grids:PropertyModel CanAutoConfigure="True" Target="{Binding Mode=TwoWay, ElementName=boundTextBox, Path=IsReadOnly}" />
	<grids:PropertyModel CanAutoConfigure="True" Target="{Binding Mode=TwoWay, ElementName=boundTextBox, Path=TabIndex}">
		<gridseditors:Int32PropertyEditor Minimum="0" />
	</grids:PropertyModel>
</grids:PropertyGrid>

```

Here we've also turned off categorization and alphabetic name sorting.  The default content for a [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) object is its [ValuePropertyEditor](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel.ValuePropertyEditor) property.  In the example above, we've said the `TabIndex` property should use the [Int32EditBox](xref:@ActiproUIRoot.Controls.Editors.Int32EditBox) from our Editors product and should have a `Minimum` of `0`.

}

## Data Models

The [ActiproSoftware.Windows.Controls.Grids.PropertyData](xref:@ActiproUIRoot.Controls.Grids.PropertyData) namespace defines a number of interfaces and classes related to data models generated by the data factory of property grid.  There are data models for properties, categories, and category editors.  All data models implement the core [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) interface and are hierarchy objects, meaning each one can have one or more child data models.  This allows for nested categories and properties.

The abstract [DataModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataModelBase) class implements [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) and `INotifyPropertyChanged`.  It is the base class for the more concrete data model implementations.

The core [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) interface defines properties related to hierarchy ([Parent](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.Parent), [Children](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.Children), and [IsRoot](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.IsRoot)), properties related to appearance ([DisplayName](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.DisplayName), [Description](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.Description), [IsExpanded](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.IsExpanded), [IsSelected](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.IsSelected), and [IsModified](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.IsModified)), and properties related to sorting ([SortComparer](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortComparer), [SortImportance](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortImportance), and [SortOrder](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.SortOrder)).  There also is a [Tag](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel.Tag) property that can hold custom data related to the data model.

The display name is what shows in the property grid's name column.  The description is what shows in the property grid's [summary area](summary-area.md) (if displayed) when the related property is selected.

## Property Data Models

The [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) interface inherits [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) and is designed to represent a property in a property grid.  This interface has a large number of properties related to property display and editing, all of which can be bound to within a [property editor](property-editors.md).

The abstract [PropertyModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBase) class implements many of the core features of [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).  Then another abstract [CachedPropertyModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CachedPropertyModelBase) class inherits [PropertyModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBase) and provides caching of results for various properties for optimal performance. [PropertyDescriptorPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyDescriptorPropertyModel) inherits that base class and uses property descriptors to retrieve values for many of the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) properties.

Another [PropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel) class inherits [PropertyModelBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBase) and can be used in scenarios where you want to explicitly configure all of the various properties on the property model instead of using results from something like a property descriptor.

The following sections talk about the various properties declared in [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).

### Target

The [Target](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Target) property returns the object that owns the property.  For instance, if a `Person` class instance was set to [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataObject](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObject), and a property model was generated for a `Name` property on the `Person` class, that property model's `Target` would return the `Person` instance.

The [TargetType](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.TargetType) property returns the `Type` of the [Target](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Target) instance.  In the example above, it would return a `Type` representing the `Person` class.

### Value and Type

The [Value](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Value) property gets or sets the actual property value.

Some UI controls like `TextBox` require that a string representation be used.  A [ValueAsString](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueAsString) property, also a get/set, converts the [Value](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Value) to/from a string that can be bound to.  The conversions are done in protected virtual [ConvertToString](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBase.ConvertToString*) and [ConvertFromString](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModelBase.ConvertFromString*) methods.  These methods can be overridden and customized if additional logic is required for the conversion.

The [ValueType](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueType) property returns the `Type` of the [Value](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Value) property.

The [Values](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Values) property returns a list of property values in scenarios where there will be more than one if the property model represents a merged property.

### Read-Only

The [IsHostReadOnly](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsHostReadOnly) property returns whether the entire host property grid is flagged as read-only via [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[IsReadOnly](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.IsReadOnly).  The [IsValueReadOnly](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsValueReadOnly) property returns whether the property itself is flagged as read-only, such as by not having a setter, or by having a `ReadOnlyAttribute` applied.

If either of those two values is `true`, then the [IsReadOnly](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsReadOnly) property will return `true`.  This is the property that UI controls in value editing templates will bind to for a read-only state.

That property can also return `true` if the property model is for a nested property and the parent property model is flagged with [IsImmutable](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsImmutable).

### Standard Values

Standard values can be specified for a property, which are effectively a list of known values from which the end user can choose for the property.  The [HasStandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.HasStandardValues) property returns if the property has any standard values.

If the [IsLimitedToStandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel.IsLimitedToStandardValues) property returns `true`, the end user can only choose from standard values when entering a value.  Otherwise, freeform entry is allowed, and the standard values are presented solely as options.

The [StandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.StandardValues) property returns the actual collection of standard values.  As an example, this collection could be bound to a `CombBox.ItemsSource`.  For cases where these must be converted to strings, use the [StandardValuesAsStrings](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.StandardValuesAsStrings) property instead.

For scenarios where [StandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.StandardValues) is being used and the [IsLimitedToStandardValues](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel.IsLimitedToStandardValues) property is `true`, it is assumed that a complex object type is being used for standard value items.  The [StandardValuesDisplayMemberPath](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyModel.StandardValuesDisplayMemberPath) property can be set to the name of the property to display in the data template for each standard value item.

The [CycleToNextStandardValue](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.CycleToNextStandardValue*) method can be manually called to change the [Value](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Value) to the next standard value.

### Name Property Editors and Templates

The actual `DataTemplate` to use for showing the property display name in a property grid is determined by the [NameTemplate](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.NameTemplate), [NameTemplateSelector](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.NameTemplateSelector), [NameTemplateKey](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.NameTemplateKey) properties, and finally related properties on a property editor, in that order.

If a [property editor](property-editors.md) is used to select the `DataTemplate`, it will be available in the [NamePropertyEditor](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.NamePropertyEditor) property.

The `DataTemplate`-selecting process is described in detail in the [Property Editors](property-editors.md) topic.

### Value Property Editors and Templates

The actual `DataTemplate` to use editing the property value in a property grid is determined by the [ValueTemplate](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueTemplate), [ValueTemplateSelector](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueTemplateSelector), [ValueTemplateKey](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueTemplateKey), [ValueTemplateKind](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValueTemplateKind) properties, and finally related properties on a property editor, in that order.

If a [property editor](property-editors.md) is used to select the `DataTemplate`, it will be available in the [ValuePropertyEditor](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ValuePropertyEditor) property.

The `DataTemplate`-selecting process is described in detail in the [Property Editors](property-editors.md) topic.

### Resetting

The [CanResetValue](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.CanResetValue) property determines if the value can be reset to a default value.

If so, a virtual [ResetValue](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ResetValue*) method is called.  A related [ResetValueCommand](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.ResetValueCommand) property can be used with UI controls like menu items to invoke that method.

### Merging

When the [IsMergeable](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.IsMergeable) property returns `true`, the property can be merged with other similarly named/typed properties.  This situation can happen if multiple objects are set to the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects) property.

### Collections

For property models that represent collections, the [CanAddChild](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.CanAddChild) property returns whether the collection allows child items to be added.  This process occurs when the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[AddChild](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.AddChild*) method is called, or the [AddChildCommand](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.AddChildCommand) is used with a UI control to invoke the method.

For property models that represent an item within a collection, the [CanRemove](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.CanRemove) property returns whether the collection allows the child item to be removed.  This process occurs when the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[Remove](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Remove*) method is called, or the [RemoveCommand](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.RemoveCommand) is used with a UI control to invoke the method.

## Category Data Models

The [ICategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryModel) interface inherits [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) and is designed to represent a category in a property grid.  The interface is implemented by the [CategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CategoryModel) class.

Category data models are created by the data factory when the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[IsCategorized](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.IsCategorized) property is set to `true`.  In this scenario, the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).[Category](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel.Category) property is examined for each property model and properties with the same category are grouped together as child models of a category model.

Categories and how they work are described in detail in the [Categorization and Sorting](categorization-and-sorting.md) topic.

## Category Editor Data Models

The [ICategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryEditorModel) interface inherits [IDataModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataModel) and is designed to represent a category editor in a property grid.  The interface is implemented by the [CategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CategoryEditorModel) class.

Category editors and how they work are described in detail in the [Category Editors](category-editors.md) topic.

## Data Factories

Each property grid has a data factory assigned to the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataFactory) property.  A data factory is a class that implements [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory).  It is responsible for handling requests for generating data models via its [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory.GetDataModels*) method.  Property grid may request data models whenever a top-level control property changes (e.g., [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[IsCategorized](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.IsCategorized), etc.), when expanding nested items, or when a change is detected to certain property models.

[DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase) is the abstract base class that implements [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory) and provides all the base logic needed to build a data factory.  It handles most of the mundane details of how a data factory should work so that inherited classes can focus more on actual property model creation instead of merging, categorization, sorting, etc.

@if (winrt) {

On the @@PlatformName platform, an instance of the [TypeReflectionFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory) class is the default data factory applied to [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataFactory).  This class inherits [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase) and uses reflection to return data models.

}

@if (wpf) {

On the @@PlatformName platform, an instance of the [TypeDescriptorFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory) class is the default data factory applied to [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataFactory).  This class inherits [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase) and uses type descriptors to return data models.

}

### How DataFactoryBase Works

Whenever a request for data models (generally for [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataObjects](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataObjects)) is made to the data factory, the [GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory.GetDataModels*) method is called.  This method is passed an [IDataFactoryRequest](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactoryRequest) object that contains various parameters describing what is requested.  The request object also contains many options that directly come from [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid) control options.

The [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetDataModels*) method first calls into the abstract [GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetPropertyModels*) method to get the [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) objects found for each property of each of the requested data objects to examine.  These property models are stored in a data model collection.  If multiple data objects are being examined and a merged property needs to be created, the [CreateMergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CreateMergedPropertyModel*) method will be called.

@if (winrt) {

The [TypeReflectionFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory.GetPropertyModels) method implementation uses reflection to locate properties on the data objects.  For collection properties, the [CreateCollectionPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory.CreateCollectionPropertyModel) method is called to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).  Otherwise, the [CreatePropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory.CreatePropertyModel) method is called to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).

}

@if (wpf) {

The [TypeDescriptorFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory.GetPropertyModels*) method implementation uses type descriptors to find property descriptors on the data objects.  If a property descriptor is for a collection property, the [CreateCollectionPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory.CreateCollectionPropertyModel*) method is called to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).  Otherwise, the [CreatePropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory.CreatePropertyModel*) method is called to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel).

}

Next, if the request is made for a root data object, any additional explicitly-defined properties (via [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[Properties](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.Properties)) will be appended to the data model collection.

If [categorization](categorization-and-sorting.md) is enabled (via [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[IsCategorized](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.IsCategorized)), then the [CategorizeDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CategorizeDataModels*) method is called.  In that method, each of the properties in the data model collection is examined to see which category the property falls into.  If no category is specified, a default `"Misc"` category is used.  The final display name of this `"Misc"` category can be set with the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[MiscCategoryName](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.MiscCategoryName) property. [ICategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryModel) objects are created as appropriate to contain each of the properties in the related category.  The [CreateCategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CreateCategoryModel*) method is called to create the [ICategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryModel) object.  In scenarios where [category editors](category-editors.md) should be applied, the [CreateCategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CreateCategoryEditorModel*) method is called to create the [ICategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryEditorModel) object.  These category and category editor models are placed in a new top-level data model collection and they are what is returned by the [GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory.GetDataModels*) method.  If categorization is not enabled, the original uncategorized property models are what would be returned by the [GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory.GetDataModels*) method instead.

Finally, before the results are returned, the data model collection is [sorted](categorization-and-sorting.md) via a call to the [SortDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.SortDataModels*) method.  By default, the data models are sorted in order of sort importance (a [DataModelSortImportance](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataModelSortImportance) value), sort order (an integer), numeric display name index (if applicable and using format like `"[0]"`)), and finally display name.

> [!NOTE]
> All of the [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase) methods described above are virtual and can be overridden in derived classes to customize or replace default functionality.

## Custom Data Factories

Custom data factories can be written to tailor what is displayed by the property grid.  Custom factories can populate the property grid using entries in a collection, XML document, or application settings.  It is not strictly limited to properties.

A custom factory needs to implement [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory) and must be assigned to the [PropertyGrid](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid).[DataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyGrid.DataFactory) property for it to be used.

@if (winrt) {

While it's generally easiest to inherit [TypeReflectionFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory) for your custom factory and to simply override its methods as appropriate, you could inherit the lower-level [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase), or even implement [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory) completely yourself.

}

@if (wpf) {

While it's generally easiest to inherit [TypeDescriptorFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory) for your custom factory and to simply override its methods as appropriate, you could inherit the lower-level [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase), or even implement [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory) completely yourself.

}

The following sections summarize all of the virtual methods you can override in various data factory implementations that originally described above in the "How DataFactoryBase Works" portion of the documentation.

### GetDataModels Method

The [IDataFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactory.GetDataModels*) method (implemented by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetDataModels*)) is the main entry and exit point of a data factory.  Thus, you can override it to alter request data, reimplement all core logic, or alter the default final data model results for the request.

### GetPropertyModels Method

The abstract [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetPropertyModels*) method is implemented by inheriting classes.  It is passed a single data object and the [IDataFactoryRequest](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IDataFactoryRequest) object and expects that a collection of [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) objects is returned, where each property model represents an accessible property on the data object.  Override this method to customize which properties are accessible for the specified data object.

> [!NOTE]
> This is the most commonly overridden method for a custom data factory.

@if (winrt) {

### CreatePropertyModel Method

The [TypeReflectionFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory).[CreatePropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory.CreatePropertyModel) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetPropertyModels*) to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) for a normal property.  The default return type is [PropertyDescriptorPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyDescriptorPropertyModel).

}

@if (wpf) {

### CreatePropertyModel Method

The [TypeDescriptorFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory).[CreatePropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory.CreatePropertyModel*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetPropertyModels*) to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) for a normal property.  The default return type is [PropertyDescriptorPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.PropertyDescriptorPropertyModel).

}

@if (winrt) {

### CreateCollectionPropertyModel Method

The [TypeReflectionFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory).[CreateCollectionPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeReflectionFactory.CreateCollectionPropertyModel) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetPropertyModels*) to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) for a collection property.  The default return type is [CollectionPropertyDescriptorPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CollectionPropertyDescriptorPropertyModel).

}

@if (wpf) {

### CreateCollectionPropertyModel Method

The [TypeDescriptorFactory](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory).[CreateCollectionPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.TypeDescriptorFactory.CreateCollectionPropertyModel*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetPropertyModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetPropertyModels*) to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) for a collection property.  The default return type is [CollectionPropertyDescriptorPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CollectionPropertyDescriptorPropertyModel).

}

### CreateMergedPropertyModel Method

The [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[CreateMergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CreateMergedPropertyModel*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetDataModels*) to create an [IPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.IPropertyModel) representing merged properties.  The default return type is [MergedPropertyModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.MergedPropertyModel).

### CategorizeDataModels Method

The [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[CategorizeDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CategorizeDataModels*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetDataModels*) to create top-level [ICategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryModel) objects when categorization is enabled.

### CreateCategoryModel Method

The [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[CreateCategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CreateCategoryModel*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[CategorizeDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CategorizeDataModels*) to create an [ICategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryModel) object for a certain category.  The default return type is [CategoryModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CategoryModel).

### CreateCategoryEditorModel Method

The [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[CreateCategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CreateCategoryEditorModel*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[CategorizeDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.CategorizeDataModels*) to create an [ICategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.ICategoryEditorModel) object for a certain [category editor](category-editors.md).  The default return type is [CategoryEditorModel](xref:@ActiproUIRoot.Controls.Grids.PropertyData.CategoryEditorModel).

### SortDataModels Method

The [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[SortDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.SortDataModels*) method is called by [DataFactoryBase](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase).[GetDataModels](xref:@ActiproUIRoot.Controls.Grids.PropertyData.DataFactoryBase.GetDataModels*) to sort the data models before they are returned.  While other features described in the [Categorization and Sorting](categorization-and-sorting.md) topic can be used to sort data models, override this method to implement a lower-level sort mechanism.
