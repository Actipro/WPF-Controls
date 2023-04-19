---
title: "NuGet Packages and Feeds"
page-title: "NuGet Packages and Feeds"
order: 24
---
# NuGet Packages and Feeds

Actipro provides NuGet packages for its @@PlatformName Control assemblies so that they can easily be consumed by .NET projects and later updated.

The NuGet packages include multiple .NET variations of the assemblies.  When a package is referenced, NuGet will use the best variation for your target framework.  See the [Supported Technologies](supported-technologies.md) topic for a list of supported frameworks.

## Actipro NuGet Packages

The following are Actipro's NuGet packages for @@PlatformName and each one's package dependencies.  See the [Deployment](deployment.md) topic for a mapping of each Actipro product to redistributable assemblies, and their containing NuGet package where appropriate.

### ActiproSoftware.Controls.WPF Package

A quick way to reference all of Actipro's @@PlatformName Control product packages ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.BarCode`
- `ActiproSoftware.Controls.WPF.Bars`
- `ActiproSoftware.Controls.WPF.Charts`
- `ActiproSoftware.Controls.WPF.Docking`
- `ActiproSoftware.Controls.WPF.Editors.Interop.DataGrid`
- `ActiproSoftware.Controls.WPF.Editors.Interop.Grids`
- `ActiproSoftware.Controls.WPF.Editors`
- `ActiproSoftware.Controls.WPF.Gauge`
- `ActiproSoftware.Controls.WPF.Grids`
- `ActiproSoftware.Controls.WPF.MicroCharts`
- `ActiproSoftware.Controls.WPF.Navigation`
- `ActiproSoftware.Controls.WPF.Ribbon`
- `ActiproSoftware.Controls.WPF.Shared`
- `ActiproSoftware.Controls.WPF.Shell`
- `ActiproSoftware.Controls.WPF.SyntaxEditor`
- `ActiproSoftware.Controls.WPF.Views`
- `ActiproSoftware.Controls.WPF.Wizard`

### ActiproSoftware.Controls.WPF.Shared Package

The Shared Library, included with all other @@PlatformName control products ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Shared)).

### ActiproSoftware.Controls.WPF.BarCode Package

The Bar Code product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.BarCode)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Bars Package

The Bars product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Bars)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Bars.Mvvm Package

An optional companion to the Bars product that supports easy MVVM integration ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Bars.Mvvm)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Bars`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Charts Package

The Charts product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Charts)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Docking Package

The Docking/MDI product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Docking)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Editors Package

The Editors product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Editors)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Editors.Interop.DataGrid Package

Editors interop functionality with the Microsoft DataGrid control ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Editors.Interop.DataGrid)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Editors`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Editors.Interop.Grids Package

Editors interop functionality with the PropertyGrid control in Grids ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Editors.Interop.Grids)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Editors`
- `ActiproSoftware.Controls.WPF.Grids`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Gauge Package

The Gauge product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Gauge)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Grids Package

The Grids product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Grids)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.MicroCharts Package

The Micro Charts product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.MicroCharts)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Navigation Package

The Navigation product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Navigation)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Ribbon Package

The Ribbon product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Ribbon)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Shell Package

The Shell product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Shell)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Grids` (Shell controls are based on tree controls in Grids)
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.SyntaxEditor Package

The SyntaxEditor product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.SyntaxEditor)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.DotNet Package

The SyntaxEditor .NET Languages add-on product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.DotNet)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.SyntaxEditor`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.JavaScript Package

The SyntaxEditor Web Languages add-on product, containing the JavaScript/JSON syntax languages ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.JavaScript)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.SyntaxEditor`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.Python Package

The SyntaxEditor Python Language add-on product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.Python)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.SyntaxEditor`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.XML Package

The SyntaxEditor Web Languages add-on product, containing the XML syntax language ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.XML)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.SyntaxEditor`
- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Themes.Aero Package

Optional older Aero-style Windows 7 and Office 2010 themes ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Themes.Aero)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Views Package

The Views product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Views)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

### ActiproSoftware.Controls.WPF.Wizard Package

The Wizard product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.WPF.Wizard)).  Dependencies include:

- `ActiproSoftware.Controls.WPF.Shared`

## Using the nuget.org Package Source

Actipro publishes its @@PlatformName Control NuGet packages to nuget.org, which is the most popular package source, run by Microsoft and natively supported by Visual Studio.

Actipro's packages can be found at ([https://www.nuget.org/packages?q=ActiproSoftware.Controls.WPF](https://www.nuget.org/packages?q=ActiproSoftware.Controls.WPF)) since all of Actipro's @@PlatformName Control NuGet package names begin with "ActiproSoftware.Controls.WPF".

Visual Studio should have the nuget.org package source defined by default, which will allow the Actipro packages to be found.  This can be verified by selecting Visual Studio's **Tools > NuGet Package Manager > Package Manager Settings** menu item and on the dialog that appears, select **Package Sources** in the tree on the left, and the available package sources will be listed on the right.  The **nuget.org** package source entry should point to `https://api.nuget.org/v3/index.json`.  Please make sure this entry exists.

## Using a Local Package Source

Some customers may wish to set up a local package source, such as on a local or network drive.  This is common in scenarios where the company policy is to maintain its own NuGet repository (instead of using nuget.org), you have an offline machine (can't access nuget.org), Actipro provided prerelease versions for testing, etc.

Visual Studio makes it easy to add a package source from a folder.  To add a local package source, select Visual Studio's **Tools > NuGet Package Manager > Package Manager Settings** menu item and on the dialog that appears, select **Package Sources** in the tree on the left, and the available package sources will be listed on the right.  Click the **+** (plus) button above the list and add a new entry.  Provide a **Name** and use the path to your local/network folder that contains the NuGet packages as the **Source**.

The Actipro NuGet packages can be downloaded directly from the package pages linked to at ([https://www.nuget.org/profiles/ActiproSoftware](https://www.nuget.org/profiles/ActiproSoftware)).

## Managing NuGet Packages

To find and install an Actipro package, right click on your solution in Visual Studio's **Solution Explorer** tool window, and select the **Manage NuGet Packages for Solution...** menu item.  In the document that appears, ensure the package source on the upper right points to the package source that contains Actipro NuGet packages (e.g., nuget.org).  Then enter *ActiproSoftware.Controls.WPF* in the **Browse** tab's **search** box to see available @@PlatformName Control packages.

Select the package in the package list that you wish to add to a project.  On the right side, check the project(s) that should have the target project added.  Choose the version to install and click the **Install** button.

After following the normal NuGet package install flow, the package will be listed as a package reference in the **Solution Explorer**.  This process can be repeated to install other Actipro NuGet packages.

If you wish to remove a package from a project, select the package in the list on the left and the project that references it on the right.  Then click the **Uninstall** button to remove the package reference.

More detailed instructions on using Visual Studio's NuGet Package Manager are available on Microsoft's documentation site ([https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio)).

## Updating NuGet Packages

When new Actipro NuGet package versions are later released, they will appear on the **Updates** tab of the document that appears when you right click on your solution in Visual Studio's **Solution Explorer** tool window and select the **Manage NuGet Packages for Solution...** menu item.

Select the package to update in the list on the left, and the pick the desired version on the right.  Then click the **Update** button to update to the selected version.

> [!IMPORTANT]
> Never mix versions of Actipro @@PlatformName Control NuGet packages.  If you reference multiple packages and change to a different version of one, make sure you update all the other Actipro packages to match the same version.

More detailed instructions on using Visual Studio's **NuGet Package Manager** are available on Microsoft's documentation site ([https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio#update-a-package](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).
