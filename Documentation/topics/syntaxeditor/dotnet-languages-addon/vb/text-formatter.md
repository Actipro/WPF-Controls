---
title: "Text Formatter"
page-title: "Text Formatter - Visual Basic Language - SyntaxEditor .NET Languages Add-on"
order: 5
---
# Text Formatter

The built-in text formatter can adjust whitespace between code elements to make code more readable.

The [Text Formatting](../../text-parsing/advanced-text/text-formatting.md) topic talks about text formatters in general and how to register them as a "feature" language service.

The [VBTextFormatter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBTextFormatter) class is the default implementation of an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service for this language.

## Registering with a Syntax Language

Any object that implements [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) can be associated with a syntax language by registering it as an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service on the language.

The [VBSyntaxLanguage](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage) class automatically registers a [VBTextFormatter](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBTextFormatter) with itself when it is created, so normally text formatters never need to be set on a VB language unless a custom one is made.

This code creates a custom text formatter (defined in a make-believe `CustomVBTextFormatter` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterTextFormatter(new CustomVBTextFormatter());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextFormatter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextFormatter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [VBSyntaxLanguage](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
