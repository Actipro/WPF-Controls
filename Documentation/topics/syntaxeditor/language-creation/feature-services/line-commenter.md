---
title: "Line Commenter"
page-title: "Line Commenter - Feature Services - SyntaxEditor Language Creation Guide"
order: 6
---
# Line Commenter

Syntax languages support line commenters, which provide built-in functionality for commenting and uncommenting text based on language-specific delimiters.

## Basic Concepts

Line commenters are classes implementing [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) that can comment or uncomment a range of text.  The text/parsing framework includes two implementations of line commenters: one that comments out a specific line (like C#'s `//`) and one that comments out a specific range of text (like XML's `<!-- -->`).

See the [Line Commenting](../../text-parsing/advanced-text/line-commenting.md) topic for details on how to create and work with line commenter classes.

When a line commenter is associated with a syntax language, SyntaxEditor can take advantage of it to implement its comment and uncomment lines feature.

## Registering with a Language

Any object that implements [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) can be associated with a syntax language by registering it as an [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) service on the language.

This code creates a line commenter for C# style comments and registers it with the syntax language that is already declared in the `language` variable:

```csharp
LineBasedLineCommenter commenter = new LineBasedLineCommenter();
commenter.StartDelimiter = "//";
language.RegisterLineCommenter(commenter);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterLineCommenter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterLineCommenter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
