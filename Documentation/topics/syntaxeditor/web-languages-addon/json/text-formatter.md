---
title: "Text Formatter"
page-title: "Text Formatter - JSON Language - SyntaxEditor Web Languages Add-on"
order: 5
---
# Text Formatter

The built-in text formatter can adjust whitespace between code elements to make code more readable.

The [Text Formatting](../../text-parsing/advanced-text/text-formatting.md) topic talks about text formatters in general and how to register them as a "feature" language service.

The [JsonTextFormatter](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonTextFormatter) class is the default implementation of an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service for this language.

## Registering with a Syntax Language

Any object that implements [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) can be associated with a syntax language by registering it as an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) service on the language.

The [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) class automatically registers a [JsonTextFormatter](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonTextFormatter) with itself when it is created, so normally text formatters never need to be set on a JSON language unless a custom one is made.

This code creates a custom text formatter (defined in a make-believe `CustomJsonTextFormatter` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterTextFormatter(new CustomJsonTextFormatter());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextFormatter](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextFormatter*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.

## Opening Braces on New Line

The [JsonTextFormatter](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonTextFormatter).[IsOpeningBraceOnNewLine](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonTextFormatter.IsOpeningBraceOnNewLine) property can be used to determine whether opening braces appear on a new line after a colon.

It defaults to `false`, but can be changed to `true` to cause opening braces to move to the next line after a colon.
