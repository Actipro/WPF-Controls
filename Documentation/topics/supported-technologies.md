---
title: "Supported Technologies"
page-title: "Supported Technologies"
order: 50
---
# Supported Technologies

The @@PlatformName Controls are compatible with a number of different technologies, all described below.

## Frameworks

The products have assemblies available for multiple frameworks, including:

- .NET 6 or later
- .NET Framework 4.6.2 or later

> [!IMPORTANT]
> The @@PlatformName Controls installer only installs the product assemblies specifically for .NET Framework.  Please use our [NuGet packages](nuget.md) to make use of assemblies that can target any of the .NET variations listed above, including the .NET Framework ones.

## Architectures

The products have been tested and are supported under the following architectures:

- Any CPU
- ARM64
- x64
- x86

## IDEs

The products work best at design-time with the following IDEs:

- Visual Studio 2022

> [!NOTE]
> Older versions of Visual Studio should also work when building applications that use Actipro @@PlatformName controls but may not fully support all designer features and are not actively tested with new releases.  For example, Visual Studio 2019 does not support .NET 6+, so it should only be used for applications that target .NET Framework.
>
> Please understand that Actipro Support may not be able assist with issues arising from the use of unsupported IDEs.

> [!IMPORTANT]
> Our [Sample Browser](quick-starts.md) application project configuration requires the .NET SDK even if targeting .NET Framework.