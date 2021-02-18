---
title: "Example Text"
page-title: "Example Text - Feature Services - SyntaxEditor Language Creation Guide"
order: 7
---
# Example Text

Syntax languages have to ability to expose a small code snippet showing many of the constructs in the language.

This can be used in options dialogs to show the current classifications / syntax highlighting settings affect a particular language.  In this case, it is best to use code the shows all the various classifications and related highlighting styles.

## Basic Concepts

The [IExampleTextProvider](xref:ActiproSoftware.Text.IExampleTextProvider) interface is a simple interface that describes an [ExampleText](xref:ActiproSoftware.Text.IExampleTextProvider.ExampleText) property.  The property simply returns a code snippet that can be used by a language for example text purposes.

The [ExampleTextProvider](xref:ActiproSoftware.Text.Implementation.ExampleTextProvider) class implements the [IExampleTextProvider](xref:ActiproSoftware.Text.IExampleTextProvider) interface and provides an easy way to associate example text with your languages.

## Registering with a Language

Any object that implements [IExampleTextProvider](xref:ActiproSoftware.Text.IExampleTextProvider) can be associated with a syntax language by registering it as an [IExampleTextProvider](xref:ActiproSoftware.Text.IExampleTextProvider) service on the language.

This code creates XML example text and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterExampleTextProvider(new ExampleTextProvider("<xml></xml>"));
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterExampleTextProvider](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterExampleTextProvider*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
