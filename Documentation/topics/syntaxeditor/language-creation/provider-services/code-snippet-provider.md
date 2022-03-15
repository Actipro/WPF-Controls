---
title: "IntelliPrompt Code Snippet Provider"
page-title: "IntelliPrompt Code Snippet Provider - Provider Services - SyntaxEditor Language Creation Guide"
order: 10
---
# IntelliPrompt Code Snippet Provider

Languages can choose to support [IntelliPrompt code snippets](../../user-interface/intelliprompt/code-snippets.md) that provide a way to insert pre-defined fragments of text into the editor.  Each code snippet can declare multiple fields of text, and when a code snippet template session is activated in SyntaxEditor, the text is inserted and the end user can tab between the fields to edit their values.

## Basic Concepts

Code snippet providers, represented by the [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider) interface and implemented by the [CodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetProvider) class, serve several purposes when registered as a service on a language:

- They let other code know that code snippets are supported for the language.
- They make the available code snippets accessible.
- They allow for selection sessions to be opened.
- They watch for `Tab` to be pressed after a code snippet shortcut, and open a template session in response.
- They allow for the various template session adornments to be displayed.

A [CodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetProvider) class instance can be used directly as the service.

### Loading a Root Folder

The [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider).[RootFolder](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider.RootFolder) property needs to be set to an [ICodeSnippetFolder](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetFolder) instance that has loaded code snippet metadata into itself or one of its nested folders.  This is required for certain functionality to be enabled.

The [IntelliPrompt code snippets](../../user-interface/intelliprompt/code-snippets.md) topic discusses ways to populate a folder, including the ability to recursively load from a file system folder.

### Setting Case Sensitivity

The [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider).[IsCaseSensitive](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider.IsCaseSensitive) property can be set to `false` for languages that don't require that code snippet shortcuts be matched with exact case.

## Registering with a Language

Any object that implements [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider) can be associated with a syntax language by registering it as an [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider) service on the language.

This code registers a code snippet provider in the `codeSnippetProvider` variable with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterService(codeSnippetProvider);
```

## Ordering Providers

The [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider) interface inherits the [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable) interface.  When multiple language services are registered that implement [ICodeSnippetProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider), they can be ordered.  The highest priority provider will be used first.  If it indicates that a session was opened, no other providers will be used.  If it indicates that a session was not opened, the next provider is checked, and so on.  This allows for layering of multiple providers.
