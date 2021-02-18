---
title: "Indent Provider"
page-title: "Indent Provider - SyntaxEditor Python Language Add-on"
order: 4
---
# Indent Provider

An indent provider enables support for smart indent features when pressing ENTER.

The [Indent Providers](../../user-interface/input-output/indent-providers.md) topic talks about indent providers in general and how to register them as a "feature" language service.

The [PythonIndentProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonIndentProvider) class is the default implementation of an [IIndentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IIndentProvider) service for this language.

## Feature Summary

### Smart Indent

The built-in indent provider will attempt to properly indent lines inside the current block scope.

For instance, pressing ENTER after a `:` will cause the next line to be indented by one tab stop.  Pressing ENTER after a statement such as a `return` statement will outdent by one tab stop.

## Registering with a Syntax Language

Any object that implements [IIndentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IIndentProvider) can be associated with a syntax language by registering it as an [IIndentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IIndentProvider) service on the language.

The [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) class automatically registers a [PythonIndentProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonIndentProvider) with itself when it is created, so normally indent providers never need to be set on a Python language unless a custom one is made.

This code creates a custom indent provider (defined in a make-believe `CustomPythonIndentProvider` class) and registers it with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterIndentProvider(new CustomPythonIndentProvider());
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterIndentProvider](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterIndentProvider*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../../language-creation/service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.

## Disabling the Functionality

Since this feature is installed as a "feature" service on the language and is installed on [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) by default, it can be disabled by uninstalling the service from the language.
