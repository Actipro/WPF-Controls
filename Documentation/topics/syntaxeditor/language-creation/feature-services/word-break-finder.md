---
title: "Word Break Finder"
page-title: "Word Break Finder - Feature Services - SyntaxEditor Language Creation Guide"
order: 11
---
# Word Break Finder

While most languages use the same criteria for locating word breaks, some languages like may wish to include a `@` character as the start of a word.

## Basic Concepts

Word break finders are classes that implement [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) and navigate text to locate word breaks.  The text/parsing framework includes a [DefaultWordBreakFinder](xref:ActiproSoftware.Text.Implementation.DefaultWordBreakFinder) class that implements [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) using the most common word break finding logic that suits most languages fine.

When making a custom word break finder, the members of [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) must be implemented.  The [FindCurrentWordStart](xref:ActiproSoftware.Text.IWordBreakFinder.FindCurrentWordStart*), [FindCurrentWordEnd](xref:ActiproSoftware.Text.IWordBreakFinder.FindCurrentWordEnd*), [FindPreviousWordStart](xref:ActiproSoftware.Text.IWordBreakFinder.FindPreviousWordStart*), and [FindNextWordStart](xref:ActiproSoftware.Text.IWordBreakFinder.FindNextWordStart*) methods each accept a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) that indicates the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) and offset within that snapshot at which to begin scanning.  Those methods all return an integer indicating the offset within the same snapshot at which the appropriate word break was found.

## Registering with a Language

Any object that implements [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) can be associated with a syntax language by registering it as an [IWordBreakFinder](xref:ActiproSoftware.Text.IWordBreakFinder) service on the language.

The [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) class automatically registers a [DefaultWordBreakFinder](xref:ActiproSoftware.Text.Implementation.DefaultWordBreakFinder) with itself when it is created, so normally word break finders never need to be set on a language unless a custom one is made.

This code creates a custom word break finder (defined in a make-believe `CustomWordBreakFinder` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterWordBreakFinder(new CustomWordBreakFinder());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterWordBreakFinder](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterWordBreakFinder*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
