# Actipro WPF Controls

![Integration Build](https://github.com/Actipro/WPF-Controls/workflows/Integration%20Build/badge.svg)
[![Contributor Covenant](https://img.shields.io/badge/Contributor%20Covenant-v2.0-ff69b4.svg)](https://github.com/Actipro/.github/blob/main/Code-of-Conduct.md)
[![Follow @Actipro](https://img.shields.io/twitter/follow/Actipro?style=social)](https://twitter.com/intent/follow?screen_name=Actipro)

Samples, documentation, and other related open-source projects for [Actipro WPF Controls](https://www.actiprosoftware.com/products/controls/wpf), a vast set of modern-themed UI controls for building beautiful WPF desktop applications that include:

- [SyntaxEditor](https://www.actiprosoftware.com/products/controls/wpf/syntaxeditor) - A syntax-highlighting code editor control and parsing suite.
- [Docking/MDI](https://www.actiprosoftware.com/products/controls/wpf/docking) - A complete docking tool window and multiple document interface solution.
- [Editors](https://www.actiprosoftware.com/products/controls/wpf/editors) - Part-based and masked edit controls, with advanced date/time picker.
- [Grids](https://www.actiprosoftware.com/products/controls/wpf/grids) - Advanced PropertyGrid and custom tree (TreeListBox/TreeListView) controls.
- [Shell](https://www.actiprosoftware.com/products/controls/wpf/shell) - Windows shell folder and file browsing controls.
- [Gauge](https://www.actiprosoftware.com/products/controls/wpf/gauge) - A complete set of circular, linear, and digital gauge controls.
- [Charts](https://www.actiprosoftware.com/products/controls/wpf/charts) - Visualize complex data sets with stunning charts.
- [Micro Charts](https://www.actiprosoftware.com/products/controls/wpf/microcharts) - Small charts, also called sparklines, designed to visualize complex data.
- [Ribbon](https://www.actiprosoftware.com/products/controls/wpf/ribbon) - Easily add an Office-like user interface that meets all Microsoft specifications.
- [Navigation](https://www.actiprosoftware.com/products/controls/wpf/navigation) - Navigation bar, explorer bar, breadcrumb, and zoom controls.
- [Wizard](https://www.actiprosoftware.com/products/controls/wpf/wizard) - Everything you need to quickly create wizard dialogs.
- [Views](https://www.actiprosoftware.com/products/controls/wpf/views) - Unique controls and panels that support fluid animated item layout.
- [Bar Code](https://www.actiprosoftware.com/products/controls/wpf/barcode) - Vector-based 2D and linear bar code generation.
- [Themes](https://www.actiprosoftware.com/products/controls/wpf/themes) - Professionally-designed themes for your whole application.
- [Shared Library](https://www.actiprosoftware.com/products/controls/wpf/shared) - A set of common controls and useful components for WPF applications.

*The control products themselves are closed-source commercial products, whose source is not included in this repo.*

## Table of Contents

- [Usage](#usage)
- [NuGet Packages](#nuget-packages)
- [Contributing](#contributing)
- [Support](#support)
- [Licensing](#licensing)

## Usage

### Running the Sample Browser Application

The Sample Browser application allows you to examine all of the Actipro WPF controls and their feature sets via hundreds of included demos and QuickStarts.  Follow these steps to run the application:

- Check out the repo.
- Open the `Samples\SampleBrowser\SampleBrowser.sln` solution in Visual Studio.
- Build and run the solution's application project, which currently targets .NET Framework 4.6.2.

### Browsing Documentation

[Product documentation](https://www.actiprosoftware.com/docs/controls/wpf/index) is available on our web site.

The Markdown source code for the product documentation is contained within this repo's `Documentation\topics` folder.  The documentation is built with [DocFx](https://github.com/dotnet/docfx). 

### Viewing Related Open-Source Projects

This repo also contains the following open-source projects, which can be opened in the `Source\WPF-Libraries.sln` solution:

- `DataGrid.Contrib` - Several enhancements for the native Microsoft WPF DataGrid control.
- `Editors.Interop.DataGrid` - Integration of Actipro Editors with the Microsoft WPF DataGrid control.

## NuGet Packages

[Packages for the Actipro WPF Controls](https://www.nuget.org/packages?q=ActiproSoftware.Controls.WPF) are published on nuget.org, all beginning with the `ActiproSoftware.Controls.WPF` name prefix.

While there are packages available for individual products, the [ActiproSoftware.Controls.WPF](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF) metapackage contains all the control products, and is easiest to reference when getting started.

[![Latest Version](https://img.shields.io/nuget/v/ActiproSoftware.Controls.WPF?label=ActiproSoftware.Controls.WPF&logo=nuget)](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF) 
[![Downloads](https://img.shields.io/nuget/dt/ActiproSoftware.Controls.WPF)](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF) 

Use the appropriate NuGet packages when adding Actipro WPF controls to your own projects.  Note that the premium SyntaxEditor language add-ons are not included in the metapackage and ship in their own separate packages.

## Contributing

We welcome contributions to our open-source repository.  If you want to submit a pull request, please first open a [GitHub issue](https://github.com/Actipro/WPF-Controls/issues) or [contact us](https://www.actiprosoftware.com/company/contact) to discuss.

Read through our [How to Contribute](https://github.com/Actipro/.github/blob/main/Contributing.md) document, as it covers everything you need to know about contributing.

## Support

Our [Support](https://github.com/Actipro/.github/blob/main/Support.md) document provides details about how to properly obtain support for both our closed-source UI control products and for code in this open-source repo (documentation, samples, and related libraries).  This chart shows a quick summary:

| | UI Control Products | Open-Source Repos |
| --- | :-: | :-: |
| [Contact us via support options](https://www.actiprosoftware.com/company/contact) | ✔ | ✔ |
| [Create a GitHub issue](https://github.com/Actipro/WPF-Controls/issues) | ❌ | ✔ |

## Licensing

While the source code in this repo falls under the terms of the [included license document](https://github.com/Actipro/WPF-Controls/blob/develop/License.md), the Actipro End-User License Agreement (EULA) applies to usage of our commercial WPF control products.  

Visit the [Actipro purchasing page](https://www.actiprosoftware.com/purchase) to learn more about product licensing or to order developer licenses.  [Contact our sales team](https://www.actiprosoftware.com/company/contact) if you have any questions.
