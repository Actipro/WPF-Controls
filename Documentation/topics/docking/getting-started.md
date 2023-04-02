---
title: "Getting Started"
page-title: "Getting Started - Docking & MDI Reference"
order: 2
---
# Getting Started

Getting up and running with Docking & MDI controls is extremely easy.

This topic's information will assume you are using Visual Studio to write your XAML code for control that will contain docking windows and/or MDI.

@if (winrt) {

## App Must Target Fall Creators Update or Later

The Actipro @@PlatformName Controls target 'Windows 10 Fall Creators Update (10.0; Build 16299)'.  Any apps using them must also set the project's target version AND minimum version to the same or newer, or errors may occur.

The controls also use Microsoft's `Win2D.uwp` NuGet package, which must also be referenced.  Please see the Readme file for a detailed chart specifying which Windows 10 target versions are compatible with the various NuGet package requirement versions.

}

@if (winrt) {

## Add Extension SDK Reference

In the Visual Studio **Add References** dialog, expand out **Windows/Extensions** and add the **Actipro Universal Windows Controls** SDK to your project.  This process is described in further detail in the [References and Deployment](../deployment.md) topic.

}

@if (wpf) {

## Add Assembly References

First, add references to the *ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll* and *ActiproSoftware.Docking.@@PlatformAssemblySuffix.dll* assemblies.  They should have been installed in the GAC during the control installation process.  However, they also will be located in the appropriate *Program Files* folders.  See the product's Readme for details on those locations.

}

## Getting Started with Docking & MDI

@if (winrt) {

This `xmlns` declaration in your root XAML control allows access to the various controls in this product:

```xaml
xmlns:docking="using:ActiproSoftware.UI.Xaml.Controls.Docking"
```

}

@if (wpf) {

This `xmlns` declaration in your root XAML control allows access to the various controls in this product:

```xaml
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
```

}

This code shows the base XAML that you can use to create a simple docking window layout with several windows:

```xaml
<docking:DockSite>
	<docking:SplitContainer>
		<docking:Workspace>
			<docking:TabbedMdiHost>
				<docking:TabbedMdiContainer>
					<docking:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
						<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
					</docking:DocumentWindow>
				</docking:TabbedMdiContainer>
			</docking:TabbedMdiHost>
		</docking:Workspace>
		<docking:ToolWindowContainer>
			<docking:ToolWindow Title="Tool Window 1" />
			<docking:ToolWindow Title="Tool Window 2" />
		</docking:ToolWindowContainer>
	</docking:SplitContainer>
</docking:DockSite>
```

This code shows the base XAML that you can use to create a simple docking window layout that leverages MVVM support:

```xaml
<docking:DockSite DocumentItemsSource="{Binding DocumentItems}" DocumentItemContainerStyle="..."
			ToolItemsSource="{Binding ToolItems}" ToolItemContainerStyle="...">
	<docking:Workspace>
		<docking:TabbedMdiHost />
	</docking:Workspace>
</docking:DockSite>
```

Several ways of opening the windows are shown in the MVVM examples found in our Sample Browser.

@if (wpf) {

## The Visual Studio Item Templates

If you have Visual Studio, several item templates named "Docking & Standard MDI Window (WPF)", "Docking & Standard MDI Page (WPF)", "Docking & Tabbed MDI Window (WPF)", "Docking & Tabbed MDI Page (WPF)", "Docking Inner Fill Window (WPF)", and "Docking Inner Fill Page (WPF)" should have been installed during the WPF Studio installation procedure.

When you wish to create a new `Window` or `Page` with a tabbed MDI, standard MDI, or inner fill in your application, simply choose **Add New Item** in Visual Studio and select the appropriate item template.  A `DockSite` with several tool and documents already defined will be added to your project and opened.

The use of item templates is the fastest way to get started with our products in your own applications.

}

## Further Study

There are fair number of controls and concepts to understand when working with this product.  We encourage you to at a minimum, read through the [Term Definitions](term-definitions.md) and [Control Hierarchy](control-hierarchy.md) topics since they cover some essential things to know.

Also, please run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of the controls.

If you require further assistance after looking through those, please visit our support forum for the product.
