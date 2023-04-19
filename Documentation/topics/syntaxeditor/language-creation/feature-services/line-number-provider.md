---
title: "Line Number Provider"
page-title: "Line Number Provider - Feature Services - SyntaxEditor Language Creation Guide"
order: 21
---
# Line Number Provider

A line number provider allows the line numbers displayed in a [line number margin](../../user-interface/editor-view/editor-view-margins.md) to be customized.  While any string value can be displayed for each view line, one-based integers are most commonly used.

## Basic Concepts

Line number providers are classes implementing [ITextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineNumberProvider) that can return the display text for each view line that should appear in the line number margin.  The [GetLineNumberText](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineNumberProvider.GetLineNumberText*) method is called for each view line and a string is returned containing the line number display text.  This typically is a one-based integer number converted to a string.

The language service also defines a [GetLargestLineNumberText](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineNumberProvider.GetLargestLineNumberText*) that returns the known longest line number text, used for measuring purposes.

The [DefaultTextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DefaultTextViewLineNumberProvider) class implements the [ITextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineNumberProvider) interface and provides a default implementation for one-based integer line numbers.  It has several virtual methods that can be overridden to customize results.  An instance of [DefaultTextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DefaultTextViewLineNumberProvider) is used if no custom line number provider language service is set.

## Setting the Line Number Origin

The [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument).[LineNumberOrigin](xref:ActiproSoftware.Text.IEditorDocument.LineNumberOrigin) property is the origin line number to use as the base for the first view line.  It defaults to `1` but can be set to any other number as needed.  The [DefaultTextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DefaultTextViewLineNumberProvider) implementation uses this origin value for the first view line.

For instance, if the first view line in a document is meant to represent the 400th line in some code snippet, set the origin to `400`.

## Registering with a Language

Any object that implements [ITextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineNumberProvider) can be associated with a syntax language by registering it as an [ITextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineNumberProvider) service on the language.

This code creates a [DefaultTextViewLineNumberProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.DefaultTextViewLineNumberProvider) object and registers it with the syntax language that is already declared in the `language` variable:

```csharp
DefaultTextViewLineNumberProvider lineNumberProvider = new DefaultTextViewLineNumberProvider();
language.RegisterTextViewLineNumberProvider(lineNumberProvider);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterTextViewLineNumberProvider](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterTextViewLineNumberProvider*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
