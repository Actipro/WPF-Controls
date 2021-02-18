---
title: "Structure Matcher"
page-title: "Structure Matcher - Feature Services - SyntaxEditor Language Creation Guide"
order: 15
---
# Structure Matcher

Syntax languages support structure matchers, which provide functionality for locating matching structural text delimiters, such as brackets.

## Basic Concepts

Structure matchers are classes implementing [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) that can scan through text and tokens to find a pair or set of related text delimiters, most often brackets.  Once the language service is implemented for structure matching, SyntaxEditor features such as move to matching bracket and select to matching bracket are automatically supported.

See the [Structure Matching](../../text-parsing/advanced-text/structure-matching.md) topic for details on how to create and work with structure matcher classes.

## Registering with a Language

Any object that implements [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) can be associated with a syntax language by registering it as an [IStructureMatcher](xref:ActiproSoftware.Text.Analysis.IStructureMatcher) service on the language.

This code creates a [StructureMatcher](xref:ActiproSoftware.Text.Analysis.Implementation.StructureMatcher) object and registers it with the syntax language that is already declared in the `language` variable:

```csharp
StructureMatcher matcher = new StructureMatcher();
language.RegisterStructureMatcher(matcher);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterStructureMatcher](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterStructureMatcher*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
