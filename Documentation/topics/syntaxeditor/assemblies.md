---
title: "Assemblies and Add-on Licensing"
page-title: "Assemblies and Add-on Licensing - SyntaxEditor Reference"
order: 6
---
# Assemblies and Add-on Licensing

SyntaxEditor is a bit unique in relation to our other WPF control products in that it requires several assemblies depending on which features and add-ons you use.

## What is an Add-on?

An add-on is simply a term used to describe some sort of functionality that can optionally be added onto the main product.

Add-ons may include very advanced syntax language implementations that have features such as automated IntelliPrompt, etc.  These sorts of add-ons are generally sold separately from SyntaxEditor or its containing bundles.

Two add-ons we include with SyntaxEditor are free assemblies for integrating SyntaxEditor with parsers created using Irony and ANTLR parsing frameworks.  These add-ons are optionally installed during setup.

## Add-on Packaging

Add-ons can come in one or more assemblies.  Often add-ons for a syntax language implementation involve two assemblies.  One contains the core text/parsing (non-UI) implementation for language and the other provides UI-related functionality such as working with IntelliPrompt.

The reason add-on assemblies are split up in this way is that we have designed this SyntaxEditor framework to be multi-platform capable.  This means that the core text/parsing assembly could be used in WPF, Windows Forms, ASP.NET, etc.  Then each platform would have their own UI-related assembly that interacted with the user interface.

## Assemblies and Cost

This list of assemblies indicates all of the assemblies related to SyntaxEditor, if they are required or optional, the related add-on (if any), and if they are included for free or sold separately from SyntaxEditor and its containing bundles.

| Assembly | Required | Add-on | Cost | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Shared.Wpf.dll | Yes | -   | Included | Core framework for all Actipro WPF controls |
| ActiproSoftware.Text.Wpf.dll | Yes | -   | Included | Core text/parsing framework for SyntaxEditor |
| ActiproSoftware.Text.LLParser.Wpf.dll | Yes | -   | Included | Framework for building top-down text parsers that parse input left-to-right and construct a leftmost derivation. |
| ActiproSoftware.SyntaxEditor.Wpf.dll | Yes | -   | Included | SyntaxEditor for WPF control |
| ActiproSoftware.Text.Addons.Antlr.Wpf.dll | No  | [ANTLR](antlr-addon/index.md) | Included | Integrates ANTLR-based parsers with syntax languages |
| ActiproSoftware.Text.Addons.Irony.Wpf.dll | No  | [Irony](irony-addon/index.md) | Included | Integrates Irony-based parsers with syntax languages |
| ActiproSoftware.Text.Addons.DotNet.Wpf.dll | No  | [.NET Languages](dotnet-languages-addon/index.md) | Sold Separately | Core text/parsing for .NET languages |
| ActiproSoftware.Text.Addons.DotNet.Roslyn.Wpf.dll | No  | [.NET Languages](dotnet-languages-addon/index.md) | Sold Separately | Optional extensions to load binary assembly reflection data with Roslyn |
| ActiproSoftware.SyntaxEditor.Addons.DotNet.Wpf.dll | No  | [.NET Languages](dotnet-languages-addon/index.md) | Sold Separately | SyntaxEditor for WPF advanced C# and VB syntax language implementations |
| ActiproSoftware.Text.Addons.JavaScript.Wpf.dll | No  | [Web Languages](web-languages-addon/javascript/index.md) | Sold Separately | Core text/parsing for the JavaScript and JSON languages |
| ActiproSoftware.SyntaxEditor.Addons.JavaScript.Wpf.dll | No  | [Web Languages](web-languages-addon/javascript/index.md) | Sold Separately | SyntaxEditor for WPF advanced JavaScript and JSON syntax language implementations |
| ActiproSoftware.Text.Addons.Python.Wpf.dll | No  | [Python Language](python-language-addon/python/index.md) | Sold Separately | Core text/parsing for the Python language |
| ActiproSoftware.SyntaxEditor.Addons.Python.Wpf.dll | No  | [Python Language](python-language-addon/python/index.md) | Sold Separately | SyntaxEditor for WPF advanced Python syntax language implementation |
| ActiproSoftware.Text.Addons.Xml.Wpf.dll | No  | [Web Languages](web-languages-addon/xml/index.md) | Sold Separately | Core text/parsing for the XML language |
| ActiproSoftware.SyntaxEditor.Addons.Xml.Wpf.dll | No  | [Web Languages](web-languages-addon/xml/index.md) | Sold Separately | SyntaxEditor for WPF advanced XML syntax language implementation |

Although the add-ons that are marked as sold separately are included with the WPF controls sample for demo purposes, licenses for them must be purchased separately if you would like to use them.  However, the pricing on the add-ons is very affordable and licenses are all Enterprise licenses, meaning they cover your entire organization.
