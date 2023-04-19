---
title: "Indent Provider (Auto-Indent)"
page-title: "Indent Provider (Auto-Indent) - Feature Services - SyntaxEditor Language Creation Guide"
order: 12
---
# Indent Provider (Auto-Indent)

Syntax languages support indent providers, which provide functionality for auto-indentation of the code when the <kbd>Enter</kbd> key is pressed.

## Basic Concepts

Indent providers are classes that implement [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) that can automatically indent text when the <kbd>Enter</kbd> key is pressed.

See the [Indent Providers](../../user-interface/input-output/indent-providers.md) topic for details on how to create and work with indent provider classes.

When an indent provider is associated with a syntax language, SyntaxEditor can take advantage of it to implement its auto-indentation feature.

## Registering with a Language

Any object that implements [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) can be associated with a syntax language by registering it as an [IIndentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IIndentProvider) service on the language.

This code creates an XML indent provider and registers it with the syntax language that is already declared in the `language` variable:

```csharp
XmlIndentProvider indentProvider = new XmlIndentProvider();
language.RegisterIndentProvider(indentProvider);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterIndentProvider](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterIndentProvider*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
