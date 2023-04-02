---
title: "IntelliPrompt Completion Provider"
page-title: "IntelliPrompt Completion Provider - Provider Services - SyntaxEditor Language Creation Guide"
order: 7
---
# IntelliPrompt Completion Provider

Languages can choose to support [IntelliPrompt completion lists](../../user-interface/intelliprompt/completion-list.md) and respond to <kbd>Ctrl</kbd>+<kbd>Space</kbd>.

## Basic Concepts

The [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) interface defines a single method: [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider.RequestSession*).  This method has a parameter that indicates the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) in which to open the session and a boolean parameter that indicates whether to auto-complete if a single match is found.

SyntaxEditor is wired up to automatically call this method if a completion provider is registered as a service on the current language when <kbd>Ctrl</kbd>+<kbd>Space</kbd> is typed by the end user.

[RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider.RequestSession*) can also be called directly in your code if you wish to generically initiate a completion session, assuming the language supports it.

Implementors of the method should use code to examine the context of the caret in the view.  This may involve token scanning and/or AST examination if available.  Based on the context (in a method declaration, in a type declaration, in a documentation comment, etc.), a completion session should be opened if appropriate.

## The CompletionProviderBase Base Class

The easiest way to implement an [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) for a language is to inherit the [CompletionProviderBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CompletionProviderBase) class.  Then just override its abstract [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CompletionProviderBase.RequestSession*) method.

## Registering with a Language

Any object that implements [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) can be associated with a syntax language by registering it as an [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) service on the language.

This code registers a completion provider in the `completionProvider` variable with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterService(completionProvider);
```

It is a common practice for a language itself to implement the [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) interface and register itself as a completion provider service.

## Ordering Providers

The [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider) interface inherits the [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable) interface.  When multiple language services are registered that implement [ICompletionProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionProvider), they can be ordered.  The highest priority provider will be used first.  If it indicates that a session was opened, no other providers will be used.  If it indicates that a session was not opened, the next provider is checked, and so on.  This allows for layering of multiple providers.
