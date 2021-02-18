---
title: "Code Block Finder"
page-title: "Code Block Finder - Feature Services - SyntaxEditor Language Creation Guide"
order: 16
---
# Code Block Finder

A code block finder finds the snapshot range of a logical code block that contains a specified snapshot range.  This service is used to drive the [Code Block Selection](../../user-interface/editor-view/code-block-selection.md) feature.

## Basic Concepts

Code block finders are classes implementing [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) that can scan through text, tokens, or AST data to locate the snapshot range of a code block that contains the specified snapshot range.  Once the code block finder language service is implemented, SyntaxEditor's [Code Block Selection](../../user-interface/editor-view/code-block-selection.md) feature can be used.

## Registering with a Language

Any object that implements [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) can be associated with a syntax language by registering it as an [ICodeBlockFinder](xref:ActiproSoftware.Text.Analysis.ICodeBlockFinder) service on the language.

This code creates a [LLParseDataCodeBlockFinder](xref:ActiproSoftware.Text.Analysis.Implementation.LLParseDataCodeBlockFinder) object and registers it with the syntax language that is already declared in the `language` variable:

```csharp
LLParseDataCodeBlockFinder finder = new LLParseDataCodeBlockFinder();
language.RegisterCodeBlockFinder(matcher);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterCodeBlockFinder](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterCodeBlockFinder*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
