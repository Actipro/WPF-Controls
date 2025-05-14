---
title: "Third-Party Software"
page-title: "Third-Party Software"
order: 55
---
# Third-Party Software

All Actipro @@PlatformName controls are built specifically for Microsoft's Windows Presentation Foundation UI framework and, as such, have a dependency on either .NET or .NET Framework:

- [.NET](https://github.com/microsoft/dotnet) - Provides the runtime framework for projects that target .NET (previously known as .NET Core).
- [.NET Framework](https://learn.microsoft.com/en-us/dotnet/framework/) - Provides the runtime framework for projects that target .NET Framework.

> [!IMPORTANT]
> Any Actipro control libraries with dependencies will only have **references** to those dependencies and do not directly **distribute** any third-party software.

Any libraries with additional dependencies are listed below.

> [!NOTE]
> Any listed dependencies primarily focus on [redistributable assemblies](deployment.md) and do not include assemblies used only for software development unless otherwise noted.

*In the event that Actipro Software LLC accidentally failed to list a required notice for third-party software, please bring it to our attention by [contacting support](support.md).*

## Design Assemblies

Individual software components are distributed to developers with specialized assemblies that enable designer software, like Visual Studio, to enhance the development experience.  These assemblies are for development only and are not distributed with final applications.  Design assemblies may have the following additional dependencies:

- [Microsoft.VisualStudio.DesignTools.Extensibility](https://www.nuget.org/packages/Microsoft.VisualStudio.DesignTools.Extensibility)

## SyntaxEditor .NET Languages Add-on

This [optional add-on](syntaxeditor/dotnet-languages-addon/index.md) for the [SyntaxEditor](syntaxeditor/index.md) product has the following additional dependencies:

- [Microsoft.CodeAnalysis.Compilers v4.8.0](https://www.nuget.org/packages/Microsoft.CodeAnalysis.Compilers/4.8.0)

## SyntaxEditor ANTLR Add-on (Deprecated)

This [optional add-on](syntaxeditor/antlr-addon/index.md) for the [SyntaxEditor](syntaxeditor/index.md) product has the following additional dependencies:

- [Antlr3.Runtime v3.5.1](https://www.nuget.org/packages/Antlr3.Runtime/3.5.1)

## SyntaxEditor Irony Add-on (Deprecated)

This [optional add-on](syntaxeditor/irony-addon/index.md) for the [SyntaxEditor](syntaxeditor/index.md) product has the following additional dependencies:

- [Irony v1.1.0](https://www.nuget.org/packages/Irony/1.1.0)

