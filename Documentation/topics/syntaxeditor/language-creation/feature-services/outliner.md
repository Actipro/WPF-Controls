---
title: "Outliner"
page-title: "Outliner - Feature Services - SyntaxEditor Language Creation Guide"
order: 5
---
# Outliner

An outliner returns a language-specific outlining source for a particular [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) when requested by an outlining manager.  Having an outliner service registered on a language is what tells SyntaxEditor that the language supports automatic outlining.

## Basic Concepts

Outliners are classes implementing [IOutliner](xref:@ActiproUIRoot.Controls.SyntaxEditor.Outlining.IOutliner) that provide outlining manager with [IOutliningSource](xref:@ActiproUIRoot.Controls.SyntaxEditor.Outlining.IOutliningSource) objects upon request.  Each outliner designated whether the automatic outlining should be performed in the main UI thread when any text change occurs, or if it should be performed in response to a parse data update.

See the [Outlining Sources and Outliners](../../user-interface/outlining/outlining-sources.md) topic for details on how to create and work with outlining sources and outliners.

When an outliner is associated with a syntax language, SyntaxEditor knows that the language supports automatic outlining.

## Registering with a Language

Any object that implements [IOutliner](xref:@ActiproUIRoot.Controls.SyntaxEditor.Outlining.IOutliner) can be associated with a syntax language by registering it as an [IOutliner](xref:@ActiproUIRoot.Controls.SyntaxEditor.Outlining.IOutliner) service on the language.

This code creates an outliner for a [TokenOutliningSourceBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.Outlining.Implementation.TokenOutliningSourceBase)-based outlining source named `JavascriptOutliningSource`, and registers it with the syntax language that is already declared in the `language` variable:

```csharp
TokenOutliner<JavascriptOutliningSource> source = new TokenOutliner<JavascriptOutliningSource>();
language.RegisterOutliner(source);
```

> [!NOTE]
> The [SyntaxLanguageExtensions](xref:ActiproSoftware.Text.SyntaxLanguageExtensions).[RegisterOutliner](xref:ActiproSoftware.Text.SyntaxLanguageExtensions.RegisterOutliner*) method in the code snippet above is a helper extension method that gets added to [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) objects when the `ActiproSoftware.Text` namespace is imported.  See the [Service Locator Architecture](../service-locator-architecture.md) topic for details on registering and retrieving various service object instances, both via extension methods and generically, as there are some additional requirements for using the extension methods.
