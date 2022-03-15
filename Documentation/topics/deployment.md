---
title: "Deployment"
page-title: "Deployment"
order: 23
---
# Deployment

## Deployment Steps

There are two main steps to follow when deploying an application that uses Actipro control products.

### Configure Licensing

Please read the [Licensing](licensing.md) topic for details on the two options for applying purchased licenses.  Once configured properly, your license data will either be applied via code or embedded into your application by the build process.

### Include Redistributable Assemblies

Next, simply copy the appropriate redistributable assemblies to the same folder as your application's executable on the target machine.  Nothing else needs to be deployed.

This table describes where referenced assemblies appear after project compilation:

| Reference Kind | Description |
|-----|-----|
| NuGet package reference | The referenced assemblies will be in your `bin` folder after compilation. |
| Assembly reference | The referenced assemblies will be in your `bin` folder after compilation if the "Copy Local" property on the reference (via the Visual Studio Properties window) is set to true. |

## Redistributable Files

When you own a license for WPF Studio (our large control bundle), WPF Essentials (our smaller control bundle), or one of their individual products, you are licensed to redistribute certain files with your application.

The tables below show the product assemblies that may be redistributed based on the product(s) for which you own licenses.

### Individual Products

This table shows the product assemblies that may be redistributed based on individual products for which you own licenses, and includes their source NuGet packages:

<table>
<thead>

<tr>
<th>Actipro Product</th>
<th>Redistributable Assemblies and Related NuGet Packages</th>
</tr>

</thead>
<tbody>

<tr>
<td>Bar Code</td>
<td>

- `ActiproSoftware.BarCode.Wpf.dll` (via `ActiproSoftware.Controls.WPF.BarCode` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Charts</td>
<td>

- `ActiproSoftware.Charts.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Charts` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Docking/MDI</td>
<td>

- `ActiproSoftware.Docking.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Docking` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Editors</td>
<td>

- `ActiproSoftware.Editors.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Editors` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

Optional interop with PropertyGrid if Grids is also licensed:

- `ActiproSoftware.Editors.Interop.Grids.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Editors.Interop.Grids` NuGet package)

 Optional interop with the Microsoft DataGrid:

- `ActiproSoftware.Editors.Interop.DataGrid.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Editors.Interop.DataGrid` NuGet package)

</td>
</tr>

<tr>
<td>Gauge</td>
<td>

- `ActiproSoftware.Gauge.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Gauge` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Grids</td>
<td>

- `ActiproSoftware.Grids.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Grids` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Micro Charts</td>
<td>

- `ActiproSoftware.MicroCharts.Wpf.dll` (via `ActiproSoftware.Controls.WPF.MicroCharts` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Navigation</td>
<td>

- `ActiproSoftware.Navigation.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Navigation` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Ribbon</td>
<td>

- `ActiproSoftware.Ribbon.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Ribbon` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Shell</td>
<td>

- `ActiproSoftware.Shell.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shell` NuGet package)
- `ActiproSoftware.Grids.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Grids` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>SyntaxEditor</td>
<td>

- `ActiproSoftware.SyntaxEditor.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor` NuGet package)
- `ActiproSoftware.Text.LLParser.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor` NuGet package)
- `ActiproSoftware.Text.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>

SyntaxEditor .NET Languages Add-on \*

</td>
<td>

- `ActiproSoftware.SyntaxEditor.Addons.DotNet.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.DotNet` NuGet package)
- `ActiproSoftware.Text.Addons.DotNet.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.DotNet` NuGet package)
- `ActiproSoftware.Text.Addons.DotNet.Roslyn.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.DotNet` NuGet package)

</td>
</tr>

<tr>
<td>

SyntaxEditor Python Language Add-on \*

</td>
<td>

- `ActiproSoftware.SyntaxEditor.Addons.Python.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.Python` NuGet package)
- `ActiproSoftware.Text.Addons.Python.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.Python` NuGet package)

</td>
</tr>

<tr>
<td>

SyntaxEditor Web Languages Add-on \*

</td>
<td>

- `ActiproSoftware.SyntaxEditor.Addons.JavaScript.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.JavaScript` NuGet package)
- `ActiproSoftware.Text.Addons.JavaScript.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.JavaScript` NuGet package)
- `ActiproSoftware.SyntaxEditor.Addons.Xml.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.XML` NuGet package)
- `ActiproSoftware.Text.Addons.Xml.Wpf.dll` (via `ActiproSoftware.Controls.WPF.SyntaxEditor.Addons.XML` NuGet package)

</td>
</tr>

<tr>
<td>Views</td>
<td>

- `ActiproSoftware.Views.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Views` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

<tr>
<td>Wizard</td>
<td>

- `ActiproSoftware.Wizard.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Wizard` NuGet package)
- `ActiproSoftware.Shared.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Shared` NuGet package)

</td>
</tr>

</tbody>
</table>

*\* Add-ons marked with an asterisk are sold separately from SyntaxEditor and its containing bundles.  See [this documentation](syntaxeditor/assemblies.md) for more details on licensing these add-ons.*

### Bundles

This table shows the two bundles that allow groups of Actipro control products to be licensed at a large discount:

<table>
<thead>

<tr>
<th>Actipro Bundle</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>WPF Studio</td>
<td>

Available via the `ActiproSoftware.Controls.WPF` NuGet metapackage, and includes these products:

- Bar Code
- Charts
- Docking/MDI
- Editors
- Gauge
- Grids
- Micro Charts
- Navigation
- Ribbon
- Shell
- SyntaxEditor
- Views
- Wizard
- Shared Library

</td>
</tr>

<tr>
<td>WPF Essentials</td>
<td>

Includes these products:

- Docking/MDI
- Editors
- Navigation
- Ribbon
- Wizard
- Shared Library

</td>
</tr>

</tbody>
</table>

### Others

There are several other optional assemblies that can be redistributed, some of which don't have NuGet packages available.

Legacy assemblies contain old deprecated code that is intended for backwards compatibility only.  It is encouraged to not use these assemblies and switch to the current assemblies listed in the table above instead.

<table>
<thead>

<tr>
<th>Name</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>Aero Themes</td>
<td>

Includes optional Windows 7 and Office 2010-like Aero themes.  Not needed unless those older themes are required for your application.  Licensed for all licensed customers.

- `ActiproSoftware.Themes.Aero.Wpf.dll` (via `ActiproSoftware.Controls.WPF.Themes.Aero` NuGet package)

</td>
</tr>

<tr>
<td>DataGrid Contrib</td>
<td>

Optional additional features and themes for the Microsoft WPF DataGrid.  Open source and licensed for all licensed customers.

- `ActiproSoftware.DataGrid.Contrib.Wpf.dll`

</td>
</tr>

<tr>
<td>SyntaxEditor ANTLR Integration</td>
<td>

Optional integration of SyntaxEditor with ANTLR v3 parsers.  Licensed for customers who have licensed SyntaxEditor individually or via a bundle.

- `ActiproSoftware.Text.Addons.Antlr.Wpf.dll`

</td>
</tr>

<tr>
<td>SyntaxEditor Irony Integration</td>
<td>

Optional integration of SyntaxEditor with Irony parsers.  Licensed for customers who have licensed SyntaxEditor individually or via a bundle.

- `ActiproSoftware.Text.Addons.Irony.Wpf.dll`

</td>
</tr>

<tr>
<td>Legacy</td>
<td>

Several deprecated common controls, licensed for all licensed customers.

- `ActiproSoftware.Legacy.Wpf.dll`

</td>
</tr>

<tr>
<td>Editors (Legacy)</td>
<td>

Licensed for customers who have licensed Editors individually or via a bundle, as these contain the old Editors codebase prior to the 2017.1 version.

- `ActiproSoftware.Editors.Legacy.Wpf.dll`
- `ActiproSoftware.Editors.Interop.DataGrid.Legacy.Wpf.dll`
- `ActiproSoftware.Editors.Interop.PropertyGrid.Legacy.Wpf.dll`
- `ActiproSoftware.Editors.Interop.Ribbon.Legacy.Wpf.dll`

</td>
</tr>

<tr>
<td>PropertyGrid (Legacy)</td>
<td>

Licensed for customers who have licensed PropertyGrid (predecessor to Grids) individually or via a bundle, as these contain the old PropertyGrid codebase prior to the 2017.1 version.

- `ActiproSoftware.PropertyGrid.Legacy.Wpf.dll`
- `ActiproSoftware.PropertyGrid.Interop.WinForms.Legacy.Wpf.dll`

</td>
</tr>

</tbody>
</table>

## Default Assembly Install Folders

When the @@PlatformName Controls installer is run on a machine, .NET Framework redistributable assemblies are all installed to this folder by default:

`[Drive]:\Program Files (x86)\Actipro Software\WPF-Controls\[Version]\Assemblies\`

If you require code-signed versions of the assemblies, they are located in a "CodeSigned" child folder, if optionally installed.

Note that .NET Core assemblies are not included in the @@PlatformName Controls installer.  The .NET Core assemblies are available in the [NuGet packages](nuget.md).  The .NET Framework assemblies are contained in the [NuGet packages](nuget.md) as well.

## Other Deployment Notes

### No Run-time Royalties

There are no run-time royalties or other distribution charges for our WPF control products.  We only require that you have the proper of licenses for each developer on your project.

See the [Licensing](licensing.md) topic for more information on licensing requirements.

### Do Not Use Our Product Installers on End User Machines

You do **NOT** need to use our product installers to place our controls on end user machines, nor are you permitted to apply your license key on end user machines.

If you are getting license popups on end user machines, your licensing configuration is incorrect on your build machine.  Read the [Licensing](licensing.md) topic to troubleshoot the issue.  Once licensing is configured properly, you should only need to xcopy our product assemblies into the same folder as your application on end machines.

### Do Not Install Our Controls to End User GACs (Global Assembly Cache)

We ask that you do not place our .NET Framework-based control assemblies in the GACs of end user machines.  Our controls should only be in the GACs of developer machines, which is done by our product installer.
