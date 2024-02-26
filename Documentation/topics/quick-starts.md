---
title: "Sample Code and QuickStarts"
page-title: "Sample Code and QuickStarts"
order: 26
---
# Sample Code and QuickStarts

The **Sample Browser** is the main sample project for evaluating Actipro WPF Studio, and the [full source is available on GitHub](open-source.md). It is designed to provide information, comprehensive demos, and detailed samples for all the Actipro @@PlatformName products.

We pride ourselves in providing more full-source samples than our competition, allowing you to learn our products easier and avoid having to rewrite common logic and design patterns.

This source code is invaluable for learning how to use the various control products.

> [!TIP]
> The offline product installer can also be used to install a pre-compiled **Sample Browser** application and the full source code.

## Downloading the Sample Browser

Visit [https://github.com/Actipro/WPF-Controls](https://github.com/Actipro/WPF-Controls) and download our repository's source code.  The repository contains the full source for the **Sample Browser** project.

GitHub offers several download options including:
- [Cloning the repo with Git](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) using this URL:
  - `git clone https://github.com/Actipro/WPF-Controls.git`
- Downloading a ZIP of the source code's current state.

> [!TIP]
> It is recommended to clone the repository with the URL so that you can easily pull in future updates.

## Running the Sample Browser

Once the repository's source code has been downloaded to a folder, open the `Samples/SampleBrowser/SampleBrowser.sln` file in Visual Studio.

The projects in that sample are preconfigured to reference our product [NuGet packages](nuget.md).  Run the solution to open the **Sample Browser** application.

> [!IMPORTANT]
> Our **Sample Browser** project requires the .NET SDK (even for .NET Framework targets), and this component may not be installed by default.
>
>If the .NET SDK is not installed, a warning similar to "One or more projects in the solution were not loaded correctly." may be displayed with an error message similar to "The project file cannot be opened. Unable to locate the .NET SDK." displayed in the Visual Studio **Output Window**.
>
> Use the **Visual Studio Installer** to add the .NET SDK component and reload the solution.

### Changing the Actipro Product Version

The `Samples/SampleBrowser/ActiproSoftware.References.props` file contains an `ActiproVersion` property that defines the version of the Actipro product NuGet packages to reference.  Update that value if you wish to use an alternate version.

> [!NOTE]
> The samples always are intended to work with the current Actipro product version, and may not compile if used with other versions.  However, adjustments to the Actipro product version can be useful when testing prerelease builds.

## Types of Samples

Most samples involve one or more simple examples that are focused on a particular feature area or show how to perform a certain task. These kinds of samples, called QuickStarts, are the place to quickly learn how to get a specific feature working without having to navigate around the unrelated code needed to support other features.

On the other hand, a demo is a large sample that shows the combination of many product features together to create a finished product. Demos are intended to show some ideas of what you can do with our products when you use them in your own applications.

> [!TIP]
> Select **View** > **View Source Code for This Sample** from the title bar to open the **Source Code Viewer** with the currently selected sample's code pre-selected.

## Reusing Sample Source Code

The full source for all samples is located in the sample project and you are free to reuse the sample source code as you wish.

We encourage you to copy what you need to help you get started with using the product in your own applications.

## Visual Studio Item Templates

Many of our WPF Studio products also come packed with item templates for Visual Studio.  When getting started using our products in your own applications, use the item templates to get up and running quickly.

Each product's `Getting Started` topic discusses any Visual Studio item templates that are available.

> [!NOTE]
> Item Templates are installed using an option on the WPF Studio offline installer.