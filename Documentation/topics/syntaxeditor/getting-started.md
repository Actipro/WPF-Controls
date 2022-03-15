---
title: "Getting Started"
page-title: "Getting Started - SyntaxEditor Reference"
order: 3
---
# Getting Started

Although SyntaxEditor is a massive product, it is pretty simple to get up and running fast.  Use the tips in this topic to get started.

@if (winrt) {

## App Must Target Fall Creators Update or Later

The Actipro @@PlatformName Controls target 'Windows 10 Fall Creators Update (10.0; Build 16299)'.  Any apps using them must also set the project's target version AND minimum version to the same or newer, or errors may occur.

The controls also use Microsoft's 'Win2D.uwp' NuGet package, which must also be referenced.  Please see the Readme file for a detailed chart specifying which Windows 10 target versions are compatible with the various NuGet package requirement versions.

}

## Understanding Abstraction (Interfaces and Implementation)

The entire product has been abstracted out into an interface-based object model.  This provides for a more open object model that can easily be extended in a number of places.

Most main namespaces in the product contain interfaces, enumerations, structs, and static classes.  Classes, both constructable and abstract, that implement the interfaces can be found in `Implementation` child namespaces of the namespaces that define the related interfaces.

This information is vital to know when examining the class library or reading through these documentation topics.

## Read the Documentation Topics

This documentation file contains a lot of information about working with SyntaxEditor and the text/parsing framework.  Whenever you have specific questions about a feature, please read through its documentation topic to search for answers.

The documentation is organized such that the text/parsing framework is described first.  This framework is the core of the product and the user interface is built around it.  Thus the user interface is described after the text/parsing framework.  A [language creation guide](language-creation/index.md) is included to help [walk you through](language-creation/walkthrough.md) the process of creating your own syntax languages.

> [!NOTE]
> If you have read through the documentation topics and samples and have found that some information is not clear enough, please e-mail us.  We want to continue to improve the documentation and samples over time to thoroughly cover all functionality found in this enormous product, and we certainly appreciate your feedback.

## Examine the Sample QuickStarts

The sample project contains a lot of full source samples called QuickStarts that focus on specific feature areas.  When learning about a certain feature, it's best to examine the documentation on the feature and also look at any related QuickStarts in the sample project.

The QuickStarts contain a wealth of information on almost every product feature area, so be sure to look at them while working with the documentation.

A 'Getting Started' series of QuickStarts is included in the sample project that shows how to build a robust syntax language from start to finish.  It is an extremely valuable sample when starting to build your own syntax languages.

## Learn Terms

The [Term Definitions](term-definitions.md) topic provides an extensive list of terms used in this product.  If you ever wonder what a term means, check that list.

## Use the 'How To' Topic

The [How To...](how-to.md) topic provides a great jump off point for where to find information on performing the most common tasks with SyntaxEditor.  It's a great resource.

@if (winrt) {

## Add Extension SDK Reference

In the Visual Studio "Add References" dialog, expand out "Windows/Extensions" and add the "Actipro Universal Windows Controls" SDK to your project.  This process is described in further detail in the [References and Deployment](../deployment.md) topic.

}

@if (wpf) {

## Add Assembly References

Once you are ready to add SyntaxEditor to your own application, you'll need to add references in your project to the proper assemblies.  The assemblies should have been installed in the GAC during the control installation process.  However they also will be located in the appropriate Program Files folders.  See the product's Readme for details on those locations.

Look at the [Assemblies and Add-on Licensing](assemblies.md) topic for details on the appropriate assemblies to reference in your project.

}

## Copy Code from the Sample Project

We encourage you to copy and use any code from our sample project, including sample language definitions.  It will really help you implement features in a matter of minutes instead of hours.

> [!NOTE]
> Please note that some images in the sample project are copyrighted by Microsoft and we include them in our sample project for control demonstration purposes only!

## Premium Language Add-ons

If you are going to use one of our premium language add-ons (sold separately), it is vital to follow all the steps in their Getting Started topics to ensure they operate correctly:

- [C#](dotnet-languages-addon/csharp/getting-started.md) syntax language in the .NET Languages Add-on

- [Visual Basic](dotnet-languages-addon/vb/getting-started.md) syntax language in the .NET Languages Add-on

- [Python](python-language-addon/python/getting-started.md) syntax language in the Python Language Add-on

- [XML](web-languages-addon/xml/getting-started.md) syntax language in the Web Languages Add-on

- [JavaScript](web-languages-addon/javascript/getting-started.md) syntax language in the Web Languages Add-on

- [JSON](web-languages-addon/json/getting-started.md) (with and without comments) syntax language in the Web Languages Add-on
