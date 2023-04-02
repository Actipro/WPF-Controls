---
title: "Getting Started"
page-title: "Getting Started - DataGrid Reference"
order: 2
---
# Getting Started

Getting up and running with the WPF DataGrid is extremely easy.

This topic's information will assume you are using Visual Studio to write your XAML code for a WPF `Window` that will contain one of the `DataGrid` controls.

## Add Assembly References

If you would like to leverage the Actipro Contrib or Actipro Editors/DataGrid Interop assemblies, then add references to *ActiproSoftware.Shared.Wpf.dll* and then *ActiproSoftware.DataGrid.Contrib.Wpf.dll* and/or *ActiproSoftware.Editors.Interop.DataGrid.Wpf.dll*, respectively.  They will typically be installed in the GAC during the control installation process.  However, they also will be located in the appropriate *Program Files* folders.  See the product's Readme for details on those locations.

## Getting Started with DataGrid

This code shows the base XAML that you can use to create a simple `ThemedDataGrid`:

```xaml
xmlns:datagrid="http://schemas.actiprosoftware.com/winfx/xaml/datagrid"
...
<datagrid:ThemedDataGrid AutoGenerateColumns="False" ItemsSource="{Binding Source=...}">
	<datagrid:ThemedDataGrid.Columns>
		<DataGridTextColumn Binding="{Binding String}" Header="String" />
		<DataGridCheckBoxColumn Binding="{Binding Boolean}" Header="Boolean" />
		<DataGridHyperlinkColumn Binding="{Binding UriSource}" Header="Boolean" />
		...
	</datagrid:ThemedDataGrid.Columns>
</datagrid:ThemedDataGrid>
```

## Getting Started with Editors/DataGrid Interop

This code shows the base XAML that you can use to create a simple `DataGrid` that leverages several Editors' controls:

```xaml
xmlns:datagrid="http://schemas.actiprosoftware.com/winfx/xaml/datagrid"
xmlns:datagrideditors="http://schemas.actiprosoftware.com/winfx/xaml/datagrideditors"
...
<datagrid:ThemedDataGrid AutoGenerateColumns="False" ItemsSource="{Binding Source=...}">
	<datagrid:ThemedDataGrid.Columns>
		<datagrideditors:DataGridInt32Column Binding="{Binding Integer}" Header="Integer" />
		<datagrideditors:DataGridDateTimeColumn Binding="{Binding DateTime}" Header="DateTime" Format="d" />
		<datagrideditors:DataGridMaskedTextColumn Binding="{Binding String}" Header="String" Mask="\d*" />
		...
	</datagrid:ThemedDataGrid.Columns>
</datagrid:ThemedDataGrid>
```

## Further Study

It's very easy to use the WPF DataGrid and [ThemedDataGrid](xref:@ActiproUIRoot.Controls.DataGrid.ThemedDataGrid) controls and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough, and the sample project demonstrates almost every feature of the controls.

If you require further assistance after looking through those, please visit our support forum for the product.
