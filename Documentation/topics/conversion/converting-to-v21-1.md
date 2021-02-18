---
title: "Converting to v21.1"
page-title: "Converting to v21.1 - Conversion Notes"
order: 88
---
# Converting to v21.1

The 21.1 version made a number of infrastructure updates and improvements.

## Framework Minimum Requirement Changes

The WPF Controls v20.1 added NuGet packages to nuget.org that contain both .NET Framework 4.0 (or later) and .NET Core 3.0 (or later) variations of the product assemblies.

In v21.1, the framework minimum versions have been updated.

### .NET Core 3.1 Minimum

.NET Core 3.0 support ended by Microsoft in 2020, so in v21.1, the new minimum .NET Core version for assemblies within the NuGet packages is v3.1.

### .NET Framework 4.5.2 Minimum

.NET Framework 4.0 support ended by Microsoft in 2016, so in v21.1, the new minimum .NET Framework version for assemblies within the NuGet packages and those shipped by our installer is v4.5.2.

## .NET 5 Compatibility

The v21.1 NuGet packages fully-support .NET 5.

.NET 5 will use the .NET Core 3.1 assembly variations, which are upgrade-compatible and have been tested with .NET 5.

## Documentation Updates

### Modernization

Prior versions shipped product documentation in a CHM (compiled HTML Help) file.  We have modernized our documentation to be Markdown and DocFX-based, and now ship with new offline product documentation.

### Online Docs

Online documentation for the WPF Controls is now available at: [https://www.actiprosoftware.com/docs/controls/wpf/index](https://www.actiprosoftware.com/docs/controls/wpf/index)

## New WPF Controls GitHub Repository

A new Actipro WPF Controls GitHub repository is available at: [https://github.com/Actipro/WPF-Controls](https://github.com/Actipro/WPF-Controls)

This repository contains the full source of this documentation and our sample projects.  See the repository's readme for information on how to contribute!
