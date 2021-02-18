---
title: "Hierarchical Data"
page-title: "Hierarchical Data - Breadcrumb Features"
order: 6
---
# Hierarchical Data

When binding a WPF control, such as the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) or `TreeView` controls, to a hierarchical data source you must specify how to locate the "child" items. This allows the WPF control to walk down the hierarchy and is typically done using the `HierarchicalDataTemplate` class. Although, this can also be accomplished using a `Style`.

## Using a HierarchicalDataTemplate

The `HierarchicalDataTemplate` class can be used to define the look of the data and how to locate any child items. The look of the data is defined in the exact same manner as with the `DataTemplate` class, by specifying zero or more visual elements and binding them appropriately. The child items can be specified by setting the `ItemsSource` property on the `HierarchicalDataTemplate` class.

This sample code shows how to define a `HierarchicalDataTemplate` for the sample XML data below:

```xaml
<HierarchicalDataTemplate ItemsSource="{Binding XPath=MyXmlElement}"
                          ...>
    <TextBlock Text="{Binding XPath=@Name}" />
</HierarchicalDataTemplate>
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<MyXmlElement Name="Root Item">
    <MyXmlElement Name="First Child Item">
        <MyXmlElement Name="First Grandchild Item" />
        <MyXmlElement Name="Second Grandchild Item" />
    </MyXmlElement>
    <MyXmlElement Name="Second Child Item" />
</MyXmlElement>
```

As you can see, the `Name` attribute is used by the `TextBlock` control to define the look. The `ItemsSource` property is bound to the child `MyXmlElement` elements, which can be used by WPF controls to find any child items.

Since hierachical data is rarely this uniform, the `ItemTemplate` or `ItemTemplateSelector` properties of the `HierarchicalDataTemplate` class can be used to change the template as needed.

This sample code shows how to define a `HierarchicalDataTemplate` for the sample XML data below, which uses distinct elements at each level:

```xaml
<!-- Because this item does not have any child items we can use
     regular DataTemplate. -->
<DataTemplate x:Key="MyGrandchildXmlElementTemplate">
    <TextBlock Text="{Binding XPath=@Name}" />
</DataTemplate>

<HierarchicalDataTemplate x:Key="MyChildXmlElementTemplate"
                          ItemsSource="{Binding XPath=MyGrandchildXmlElement}"
                          ItemTemplate="{StaticResource MyGrandchildXmlElementTemplate}">
    <TextBlock Text="{Binding XPath=@Name}" />
</HierarchicalDataTemplate>

<HierarchicalDataTemplate x:Key="MyRootXmlElementTemplate"
                          ItemsSource="{Binding XPath=MyChildXmlElement}"
                          ItemTemplate="{StaticResource MyChildXmlElementTemplate}">
    <TextBlock Text="{Binding XPath=@Name}" />
</HierarchicalDataTemplate>
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<MyRootXmlElement Name="Root Item">
    <MyChildXmlElement Name="First Child Item">
        <MyGrandchildXmlElement Name="First Grandchild Item" />
        <MyGrandchildXmlElement Name="Second Grandchild Item" />
    </MyChildXmlElement>
    <MyChildXmlElement Name="Second Child Item" />
</MyRootXmlElement>
```

As you can see, we now define a `HierarchicalDataTemplate` for each distinct XML element and assign them appropriately as we move down the hierarchy.

## Using a Style

A `Style` can also be used to to define the look of the data, using a `DataTemplate`, and how to locate any child items. This can be accomplished by setting the `ItemsSource` and `ItemTemplate` directly on the container elements, typically an `ItemsControl`-derived class, created for each data object. Examples of container elements include [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) and `TreeViewItem`.

This sample code shows how to define a `Style` for the sample XML data below:

```xaml
<!-- Used to define the look -->
<DataTemplate x:Key="MyXmlElementTemplate">
    <TextBlock Text="{Binding XPath=@Name}" />
</DataTemplate>

<Style ...>
    <Setter Property="ItemsSource"
            Value="{Binding XPath=MyXmlElement}" />
    <Setter Property="ItemTemplate"
            Value="{StaticResource MyXmlElementTemplate}" />
</Style>
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<MyXmlElement Name="Root Item">
    <MyXmlElement Name="First Child Item">
        <MyXmlElement Name="First Grandchild Item" />
        <MyXmlElement Name="Second Grandchild Item" />
    </MyXmlElement>
    <MyXmlElement Name="Second Child Item" />
</MyXmlElement>
```

As you can see, the `DataTemplate` class defines the look just like with the `HierarchicalDataTemplate` class. Although, now the `ItemsSource` property is explictly bound to the child `MyXmlElement` elements using a `Setter` in the `Style`. Once the `ItemsSource` property is set, it can be used by WPF controls to find any child items.

This sample code shows how to define a `Style` for the sample XML data below, which uses distinct elements at each level:

```xaml
<!-- This can be reused across all elements, because they
     all have the Name attribute -->
<DataTemplate x:Key="MyXmlElementTemplate">
    <TextBlock Text="{Binding XPath=@Name}" />
</DataTemplate>

<Style x:Key="MyGrandchildXmlElementStyle"
       ...>
       ...
</Style>

<Style x:Key="MyChildXmlElementStyle"
       ...>
    <Setter Property="ItemsSource"
            Value="{Binding XPath=MyGrandchildXmlElement}" />
    <Setter Property="ItemContainerStyle"
            Value="{StaticResource MyGrandchildXmlElementStyle}" />
    <Setter Property="ItemTemplate"
            Value="{StaticResource MyXmlElementTemplate}" />
</Style>

<Style x:Key="MyRootXmlElementStyle"
       ...>
    <Setter Property="ItemsSource"
            Value="{Binding XPath=MyChildXmlElement}" />
    <Setter Property="ItemContainerStyle"
            Value="{StaticResource MyChildXmlElementStyle}" />
    <Setter Property="ItemTemplate"
            Value="{StaticResource MyXmlElementTemplate}" />
</Style>
```

```xml
<?xml version="1.0" encoding="utf-8" ?>
<MyRootXmlElement Name="Root Item">
    <MyChildXmlElement Name="First Child Item">
        <MyGrandchildXmlElement Name="First Grandchild Item" />
        <MyGrandchildXmlElement Name="Second Grandchild Item" />
    </MyChildXmlElement>
    <MyChildXmlElement Name="Second Child Item" />
</MyRootXmlElement>
```

As you can see, we now define a `Style` for each distinct XML element and assign them appropriately as we move down the hierarchy.

## Breadcrumb Considerations

The [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control supports both methods of defining the data hierarchy, as does the `TreeView` control and others. The [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) control does differ slightly, in that the same data can be presented in more than one area of the control. For example, a data object can be displayed in a [BreadcrumbItem](xref:ActiproSoftware.Windows.Controls.Navigation.BreadcrumbItem) control, a `MenuItem` when in a context menu, and a `ComboBoxItem` when display in the drop-down list.

If the data source of the [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) will be shared with another hiearchical control, then using a `HierarchicalDataTemplate` class would allow the look of the data to be shared. But typically, a `Style` is still be required to set properties not supported by the `HierarchicalDataTemplate` class, such as [MenuItemExpandedTemplate](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb.MenuItemExpandedTemplate).
