---
title: "Assemblies and Add-on Licensing"
page-title: "Assemblies and Add-on Licensing - SyntaxEditor Reference"
order: 6
---
# Assemblies and Add-on Licensing

SyntaxEditor is a bit unique in relation to our other @@PlatformName control products in that it requires several assemblies depending on which features and add-ons you use.

## What is an Add-on?

An add-on is simply a term used to describe some sort of functionality that can optionally be added onto the main product.

Add-ons may include very advanced syntax language implementations that have features such as automated IntelliPrompt, etc.  These sorts of add-ons are generally sold separately from SyntaxEditor or its containing bundles.

@if (wpf) {

Two add-ons we include with SyntaxEditor are free assemblies for integrating SyntaxEditor with parsers created using Irony and ANTLR parsing frameworks.  These add-ons are optionally installed during setup.

}

## Add-on Packaging

Add-ons can come in one or more assemblies.  Often add-ons for a syntax language implementation involve two assemblies.  One contains the core text/parsing (non-UI) implementation for language and the other provides UI-related functionality such as working with IntelliPrompt.

The reason add-on assemblies are split up in this way is that we have designed this SyntaxEditor framework to be multi-platform capable.  This means that the core text/parsing assembly could be used in WPF, Windows Forms, ASP.NET, etc.  Then each platform would have their own UI-related assembly that interacted with the user interface.

## Assemblies and Cost

This list of assemblies indicates all of the assemblies related to SyntaxEditor, if they are required or optional, the related add-on (if any), and if they are included for free or sold separately from SyntaxEditor and its containing bundles.

<table>
<thead>

<tr>
<th>Assembly</th>
<th>Required</th>
<th>Add-on</th>
<th>Cost</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll</td>
<td>Yes</td>
<td>-</td>
<td>Included</td>
<td>Core framework for all Actipro @@PlatformName controls</td>
</tr>

<tr>
<td>ActiproSoftware.Text.@@PlatformAssemblySuffix.dll</td>
<td>Yes</td>
<td>-</td>
<td>Included</td>
<td>Core text/parsing framework for SyntaxEditor</td>
</tr>

<tr>
<td>ActiproSoftware.Text.LLParser.@@PlatformAssemblySuffix.dll</td>
<td>Yes</td>
<td>-</td>
<td>Included</td>
<td>Framework for building top-down text parsers that parse input left-to-right and construct a leftmost derivation.</td>
</tr>

<tr>
<td>ActiproSoftware.SyntaxEditor.@@PlatformAssemblySuffix.dll</td>
<td>Yes</td>
<td>-</td>
<td>Included</td>
<td>SyntaxEditor for @@PlatformName control</td>
</tr>

@if (wpf) {
<tr>
<td>ActiproSoftware.Text.Addons.Antlr.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[ANTLR](antlr-addon/index.md)

</td>
<td>Included</td>
<td>Integrates ANTLR-based parsers with syntax languages</td>
</tr>
}

@if (wpf) {
<tr>
<td>ActiproSoftware.Text.Addons.Irony.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Irony](irony-addon/index.md)

</td>
<td>Included</td>
<td>Integrates Irony-based parsers with syntax languages</td>
</tr>
}

<tr>
<td>ActiproSoftware.Text.Addons.DotNet.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[.NET Languages](dotnet-languages-addon/index.md)

</td>
<td>Sold Separately</td>
<td>Core text/parsing for .NET languages</td>
</tr>

@if (wpf winforms) {
<tr>
<td>ActiproSoftware.Text.Addons.DotNet.Roslyn.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[.NET Languages](dotnet-languages-addon/index.md)

</td>
<td>Sold Separately</td>
<td>Optional extensions to load binary assembly reflection data with Roslyn</td>
</tr>
}

<tr>
<td>ActiproSoftware.SyntaxEditor.Addons.DotNet.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[.NET Languages](dotnet-languages-addon/index.md)

</td>
<td>Sold Separately</td>
<td>SyntaxEditor for @@PlatformName advanced C# and VB syntax language implementations</td>
</tr>

<tr>
<td>ActiproSoftware.Text.Addons.JavaScript.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Web Languages](web-languages-addon/javascript/index.md)

</td>
<td>Sold Separately</td>
<td>Core text/parsing for the JavaScript and JSON languages</td>
</tr>

<tr>
<td>ActiproSoftware.SyntaxEditor.Addons.JavaScript.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Web Languages](web-languages-addon/javascript/index.md)

</td>
<td>Sold Separately</td>
<td>SyntaxEditor for @@PlatformName advanced JavaScript and JSON syntax language implementations</td>
</tr>

<tr>
<td>ActiproSoftware.Text.Addons.Python.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Python Language](python-language-addon/python/index.md)

</td>
<td>Sold Separately</td>
<td>Core text/parsing for the Python language</td>
</tr>

<tr>
<td>ActiproSoftware.SyntaxEditor.Addons.Python.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Python Language](python-language-addon/python/index.md)

</td>
<td>Sold Separately</td>
<td>SyntaxEditor for @@PlatformName advanced Python syntax language implementation</td>
</tr>

<tr>
<td>ActiproSoftware.Text.Addons.Xml.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Web Languages](web-languages-addon/xml/index.md)

</td>
<td>Sold Separately</td>
<td>Core text/parsing for the XML language</td>
</tr>

<tr>
<td>ActiproSoftware.SyntaxEditor.Addons.Xml.@@PlatformAssemblySuffix.dll</td>
<td>No</td>
<td>

[Web Languages](web-languages-addon/xml/index.md)

</td>
<td>Sold Separately</td>
<td>SyntaxEditor for @@PlatformName advanced XML syntax language implementation</td>
</tr>

</tbody>
</table>

Although the add-ons that are marked as sold separately are included with the @@PlatformName controls sample for demo purposes, licenses for them must be purchased separately if you would like to use them.  However, the pricing on the add-ons is very affordable and the licenses are all Enterprise licenses, meaning they cover your entire organization.
