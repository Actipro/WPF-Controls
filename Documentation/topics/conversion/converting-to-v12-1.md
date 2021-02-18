---
title: "Converting to v12.1"
page-title: "Converting to v12.1 - Conversion Notes"
order: 94
---
# Converting to v12.1

All of the breaking changes are detailed or linked below.

## .NET 4.0 Client Profile Is Minimum Framework

We've had many requests to migrate to .NET 4.0 and that change has occurred in v12.1.  The minimum required .NET framework to use our products is now .NET 4.0.

Please note that both the full and the client profile variations of .NET 4.0 are fully-supported, as are future versions of .NET, such as .NET 4.5.

> [!NOTE]
> Ensure that projects using our products target .NET 4.0, .NET 4.0 Client Profile, or a later .NET version.

## VS 2008 No Longer Supported

Visual Studio 2008 doesn't support .NET 4.0 and only supports prior .NET versions.  Thus, it is no longer supported and no sample projects are included for it.

> [!NOTE]
> Convert old VS 2008 projects to VS 2010 or later.

## Docking/MDI and Prism Interop Upgraded to v4.1

The interop assembly for using our Docking/MDI product with Prism has been updated to use Prism's latest v4.1.

> [!NOTE]
> Convert to using Prism v4.1.

## DataGrid Add-on Updated to Use .NET 4.0

The DataGrid add-on has been updated to reference the native .NET 4.0 version instead of the one from the WPF Toolkit.

> [!NOTE]
> Convert to using the native .NET 4.0 DataGrid instead of the WPF Toolkit one.

## Removal of Several Shared Proxy Classes

The Shared Library previously included a couple classes (`ScrollViewerProxy` and `RenderOptionsProxy`) that were previously used internally to access .NET 4.0 features from the .NET 3.5 framework.  Since our codebase has moved to .NET 4.0, these are no longer necessary and all code that used them has been updated to use the appropriate .NET 4.0 API.

## Removal of AllowPartiallyTrustedCallers Attribute

The upgrade to .NET 4.0 allowed our assemblies to move to the newer security model in .NET 4.0.  To accommodate this move, we removed the use of the AllowPartiallyTrustedCallers assembly attribute.
