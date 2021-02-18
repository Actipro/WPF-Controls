---
title: "Delimiter Auto-Completer"
page-title: "Delimiter Auto-Completer - Feature Services - SyntaxEditor Language Creation Guide"
order: 20
---
# Delimiter Auto-Completer

A delimiter auto-completer can insert an end delimiter when its related start delimiter is typed.  This service is used to drive the [Delimiter Auto-Completion](../../user-interface/input-output/delimiter-auto-completion.md) feature.

## Basic Concepts

Delimiter auto-completers are classes implementing [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) that can auto-complete an end delimiter when a start delimiter is typed by the end user.  See the [Delimiter Auto-Completion](../../user-interface/input-output/delimiter-auto-completion.md) topic for details on this feature and how the built-in [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter) works.

## Registering with a Language

Any object that implements [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) can be associated with a syntax language by registering it as an [IDelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.IDelimiterAutoCompleter) service on the language.

This code creates a [DelimiterAutoCompleter](xref:ActiproSoftware.Text.Analysis.Implementation.DelimiterAutoCompleter) object and registers it with the syntax language that is already declared in the `language` variable:

```csharp
DelimiterAutoCompleter completer = new DelimiterAutoCompleter();
language.RegisterDelimiterAutoCompleter(completer);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterDelimiterAutoCompleter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterDelimiterAutoCompleter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
