---
title: "Getting Started"
page-title: "Getting Started - Navigation Reference"
order: 2
---
# Getting Started

Getting up and running with Navigation controls is extremely easy.

This topic's information will assume you are using Visual Studio to write your XAML code for a WPF `Window` that will contain one of the Navigation controls.

## Add Assembly References

First, add references to the `ActiproSoftware.Shared.Wpf.dll` and `ActiproSoftware.Navigation.Wpf.dll` assemblies.  They should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## The Visual Studio Item Templates for Navigation Controls

If you have Visual Studio, item templates named `Navigation Bar Window (WPF)` and `Explorer Bar Window (WPF)` should have been installed during the WPF Studio installation procedure.

When you wish to create a new `Window` containing one of the Navigation controls in your application, simply choose `Add New Item` in Visual Studio and select the appropriate the item template.  A `Window` with the related Navigation control on it will be added to your project and opened.

The use of item templates is the fastest way to get started with our products in your own applications.

## Getting Started with Breadcrumb

This code shows the base XAML that you can use to create a simple [Breadcrumb](xref:ActiproSoftware.Windows.Controls.Navigation.Breadcrumb) with a few items:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"     
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"     
...

<navigation:Breadcrumb Width="200">
	<navigation:BreadcrumbItem Header="RootItem" PathEntry="RootItem">
		<navigation:BreadcrumbItem Header="Folder 1" PathEntry="Folder 1">
			<navigation:BreadcrumbItem Header="Subfolder 1" PathEntry="Subfolder 1" />
			<navigation:BreadcrumbItem Header="Subfolder 2" PathEntry="Subfolder 2" />
		</navigation:BreadcrumbItem>
		<navigation:BreadcrumbItem Header="Folder 2" PathEntry="Folder 2">
			<navigation:BreadcrumbItem Header="Subfolder 3" PathEntry="Subfolder 3" />
			<navigation:BreadcrumbItem Header="Subfolder 4" PathEntry="Subfolder 4" />
		</navigation:BreadcrumbItem>
	</navigation:BreadcrumbItem>
</navigation:Breadcrumb>
```

## Getting Started with ExplorerBar

This code shows the base XAML that you can use to create a simple [ExplorerBar](xref:ActiproSoftware.Windows.Controls.Navigation.ExplorerBar) with two panes:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"     
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"     
...
<navigation:ExplorerBar Width="200">
	<shared:AnimatedExpander Header="Pane 1" IsExpanded="True">
		<TextBlock Text="Content here" />
	</shared:AnimatedExpander>
	<shared:AnimatedExpander Header="Pane 2" IsExpanded="True">
		<TextBlock Text="Content here" />
	</shared:AnimatedExpander>
</navigation:ExplorerBar>
```

## Getting Started with NavigationBar

This code shows the base XAML that you can use to create a simple [NavigationBar](xref:ActiproSoftware.Windows.Controls.Navigation.NavigationBar) with two panes:

```xaml
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"     
...
<navigation:NavigationBar ContentWidth="160" MaxWidth="250">
	<navigation:NavigationPane x:Name="firstPane" Title="Pane 1">
		<TextBlock Margin="10" Text="Content here" TextWrapping="Wrap" />
	</navigation:NavigationPane>
	<navigation:NavigationPane x:Name="secondPane" Title="Pane 2">
		<TextBlock Margin="10" Text="Content here" TextWrapping="Wrap" />
	</navigation:NavigationPane>
</navigation:NavigationBar>
```

## Getting Started with ZoomContentControl

This code shows the base XAML that you can use to create a simple [ZoomContentControl](xref:ActiproSoftware.Windows.Controls.Navigation.ZoomContentControl) with a few items:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"     
xmlns:navigation="http://schemas.actiprosoftware.com/winfx/xaml/navigation"     
...

<navigation:ZoomContentControl>
	<shared:ActiproLogo />
</navigation:ZoomContentControl>
```

## Further Study

It's very easy to use the various Navigation controls and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of the controls.

If you require further assistance after looking through those, please visit our support forum for the product.
