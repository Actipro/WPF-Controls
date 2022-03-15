---
title: "Getting Started"
page-title: "Getting Started - Views Reference"
order: 2
---
# Getting Started

Getting up and running with the panels is extremely easy.

This topic's information will assume you are using Visual Studio to write your XAML code for a WPF `Window` that will contain one of the panels.

## Add Assembly References

First, add references to the "ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll" and "ActiproSoftware.Views.@@PlatformAssemblySuffix.dll" assemblies.  They should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## Getting Started with Views

This code shows the base XAML that you can use to create a simple [AnimatedStackPanel](xref:@ActiproUIRoot.Controls.Views.AnimatedStackPanel):

```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:AnimatedStackPanel>
	<Button Content="One" \>
	<Button Content="Two" \>
</views:AnimatedStackPanel>
```

This code shows the base XAML that you can use to use a [AnimatedStackPanel](xref:@ActiproUIRoot.Controls.Views.AnimatedStackPanel) in a `ListBox`:

```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<ListBox ...>
	<ListBox.ItemsPanel>
		<ItemsPanelTemplate>
			<views:AnimatedStackPanel />
		</ItemsPanelTemplate>
	</ListBox.ItemsPanel>
</ListBox>
```

## Further Study

It's very easy to use the various panels and there are probably a lot of great features that you aren't aware of.

Run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of the controls.

If you require further assistance after looking through those, please visit our support forum for the product.
