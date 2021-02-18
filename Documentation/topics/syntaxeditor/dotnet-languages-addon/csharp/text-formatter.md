---
title: "Text Formatter"
page-title: "Text Formatter - C# Language - SyntaxEditor .NET Languages Add-on"
order: 5
---
# Text Formatter

The built-in text formatter can adjust whitespace between code elements to make code more readable.

The [Text Formatting](../../text-parsing/advanced-text/text-formatting.md) topic talks about text formatters in general and how to register them as a "feature" language service.

The [CSharpTextFormatter](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpTextFormatter) class is the default implementation of an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service for this language.

## Registering with a Syntax Language

Any object that implements [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) can be associated with a syntax language by registering it as an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service on the language.

The [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage) class automatically registers a [CSharpTextFormatter](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpTextFormatter) with itself when it is created, so normally text formatters never need to be set on a Visual Basic language unless a custom one is made.

This code creates a custom text formatter (defined in a make-believe `CustomCSharpTextFormatter` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterTextFormatter(new CustomCSharpTextFormatter());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextFormatter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextFormatter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [CSharpSyntaxLanguage](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.

## Opening Curly Braces on New Line

The [CSharpTextFormatter](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpTextFormatter).[IsOpeningBraceOnNewLine](xref:ActiproSoftware.Text.Languages.CSharp.Implementation.CSharpTextFormatter.IsOpeningBraceOnNewLine) property can be used to determine whether opening curly braces appear on a new line.

It defaults to `false`, but can be changed to `true` to cause opening curly braces to move to the next line.
