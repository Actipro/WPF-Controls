---
title: "Text Statistics"
page-title: "Text Statistics - Feature Services - SyntaxEditor Language Creation Guide"
order: 10
---
# Text Statistics

While the text/parsing framework includes an implementation of text statistics that provides the most common statistics for text, sometimes it's useful to create custom statistics classes that are tailored to a particular language and can return additional statistics such as comment coverage.

## Basic Concepts

Text statistics are classes that implement [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) and return numerous statistics about text such as character/line counts, whitespace percentage, etc.  The [TextStatistics](xref:ActiproSoftware.Text.Implementation.TextStatistics) class provides the default implementation of that interface.

See the text/parsing framework's [Text Statistics](../../text-parsing/advanced-text/statistics.md) topic for details on how to create and work with text statistics classes.

## Registering with a Language

Any object that implements [ITextStatisticsFactory](xref:ActiproSoftware.Text.ITextStatisticsFactory) can be associated with a syntax language by registering it as an [ITextStatisticsFactory](xref:ActiproSoftware.Text.ITextStatisticsFactory) service on the language.

Normally a custom factory that creates custom statistics does not need to be implemented.  The exception would be if you created a custom [ITextStatistics](xref:ActiproSoftware.Text.ITextStatistics) class for your language and would like to use that instead of the default statistics.

This code registers a make-believe custom text statistics factory named `CustomTextStatisticsFactory` (that creates a make-believe `CustomTextStatistics` class) with a syntax language that is already declared in the `language` variable:

```csharp
language.RegisterTextStatisticsFactory(new CustomTextStatisticsFactory());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextStatisticsFactory](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextStatisticsFactory*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
