---
title: "Getting Started"
page-title: "Getting Started - Ribbon Reference"
order: 2
---
# Getting Started

> [!IMPORTANT]
> This older Ribbon product will be deprecated in the future in favor of the new ribbon implementation in the [Bars product](../bars/index.md), which has a much-improved design and appearance, and many of the latest features currently found in Office.  It is recommended to implement new ribbons using the Bars product instead, and to [migrate away from this older Ribbon product](../conversion/converting-to-v23-1.md) to the newer Bars ribbon when possible.

Getting up and running with Ribbon is very easy, especially with the samples we include.  Ribbon provides all the Office-like ribbon user interface features right out of the box, saving you hundreds of hours of work over implementing it yourself.

## Read the Documentation Topics

This documentation file contains a lot of information about using Ribbon and its related controls to its fullest.  Whenever you have specific questions about a feature, please read through its documentation topic to search for answers.

## Start with the Sample Project's "Getting Started" QuickStart Series

The sample project includes a series of "Getting Started" QuickStart windows that walk you through the creation of a ribbon for an application window, step by step.  The first step starts with simple creation of a [RibbonWindow](xref:@ActiproUIRoot.Controls.Ribbon.RibbonWindow) without a [Ribbon](xref:@ActiproUIRoot.Controls.Ribbon.Ribbon) control on it.  The steps proceed to add a [Ribbon](xref:@ActiproUIRoot.Controls.Ribbon.Ribbon), configuring a new feature in each step until at the last step, there is a fully-functional ribbon.  Since the XAML code for a ribbon implementation can get quite large, we chose to put all our sample code in the Getting Started series of quick starts instead of in this documentation file.

The Getting Started series is probably the best place to go for quickly getting up and running.  All of the "Getting Started" windows can be run from the Sample Browser and their source code (the most important thing) is located in the sample browser's project.

Although each "Getting Started" QuickStart focuses on adding a new piece of functionality to the ribbon and the changes for each step are described within the QuickStarts, it still may be useful to use a "text diff" application to see exactly what code differences are between each "Getting Started" step.

## Examine the Other QuickStarts

We've spent a lot of time adding numerous QuickStarts for Ribbon that are located in the sample project.  Each QuickStart focuses on a specific feature area and provides some great code that you can look at to use in your own applications.

## Add Assembly References

Once you are ready to add Ribbon to your own application, first add references to the *ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll* and *ActiproSoftware.Ribbon.@@PlatformAssemblySuffix.dll* assemblies.  They should have been installed in the GAC during the control installation process.  However, they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

## The Visual Studio Item Templates (Ribbon Window, Ribbon Page)

If you have Visual Studio, an item template named `Ribbon Window (WPF)` should have been installed during the WPF Studio installation procedure.

When you wish to create a new [RibbonWindow](controls/ribbonwindow.md) in your application, simply choose `Add New Item` in Visual Studio and select the `Ribbon Window (WPF)` item template.  A `RibbonWindow` with a `Ribbon` already on it will be added to your project and opened.

The use of item templates is the fastest way to get started with our products in your own applications.

## Copy Code from the Sample Project

We encourage you to copy any code from our sample project.  It will really help you build a ribbon framework in a matter of minutes instead of hours.  Again, the best place to copy from is the "Getting Started" series of QuickStarts.

> [!NOTE]
> Please note that some images in the sample project are copyrighted by Microsoft and we include them in our sample project for control demonstration purposes only!
