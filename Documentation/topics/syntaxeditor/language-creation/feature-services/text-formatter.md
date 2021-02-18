---
title: "Text Formatter"
page-title: "Text Formatter - Feature Services - SyntaxEditor Language Creation Guide"
order: 13
---
# Text Formatter

Syntax languages support text formatters, which provide functionality for formatting the layout of whitespace and symbols such as braces to make code more readable.

## Basic Concepts

Text formatters are classes implementing [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) that can format a specified range of text. Formatting involves adjusting the layout of whitespace and symbols such as braces to make code more readable.

See the [Text Formatting](../../text-parsing/advanced-text/text-formatting.md) topic for details on how to create and work with text formatter classes.

## Registering with a Language

Any object that implements [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) can be associated with a syntax language by registering it as an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service on the language.

This code creates an XML text formatter and registers it with the syntax language that is already declared in the `language` variable:

```csharp
XmlFormatter textFormatter = new XmlFormatter();
language.RegisterTextFormatter(textFormatter);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextFormatter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextFormatter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
