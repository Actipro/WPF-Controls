---
title: "Automated IntelliPrompt"
page-title: "Automated IntelliPrompt - Visual Basic Language - SyntaxEditor .NET Languages Add-on"
order: 7
---
# Automated IntelliPrompt

The Visual Basic language has built-in automated IntelliPrompt completion list, parameter info, quick info, code snippet, and navigable symbol selector features that greatly enhance the end user's editing experience.

## Completion List

[IntelliPrompt completion lists](../../user-interface/intelliprompt/completion-list.md) are used to present end users with a list of accessible members from the code editor's current caret location.  In many cases, the completion list is triggered by key events such as pressing `Ctrl+Space` or typing a `.` character after an identifier.

There are two types of completion list scenarios when typing in code blocks.  First is when listing members of something.  This can occur when typing a `.` character after an identifier.  The second type is when showing all accessible members based on the current context.  This can occur when starting to type a new sequence of identifiers.

When typing a `<` character in documentation comments, a list of appropriate documentation comment tags is displayed.

In cases where a completion list is triggered via `Ctrl+Space`, if there is a single match for the text that has been typed, it will not show the list but will auto-complete the match.  This saves the user from having to type the entire identifier/keyword that was matched.

The completion list features are implemented by the [VBCompletionProvider](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBCompletionProvider) class.

### Disabling Auto-Show On Typed Word Start

The [VBCompletionProvider](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBCompletionProvider) class has a [CanShowOnTypedWordStart](xref:ActiproSoftware.Text.Languages.DotNet.Implementation.DotNetCompletionProviderBase.CanShowOnTypedWordStart) property that defaults to `true`.  When `true`, the completion provider will attempt to automatically show a completion list as the user starts to type a new word, but only in certain contexts.  Set the property to `false` to disable this behavior.

## Parameter Info

[IntelliPrompt parameter info](../../user-interface/intelliprompt/parameter-info.md) is the ability to display helpful tooltips when typing an invocation that presents all of the signature overloads, along with information on the current parameter being typed.

Parameter info is displayed for method, indexer, and constructor invocations.

IntelliPrompt quick info features are implemented by the [VBParameterInfoProvider](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBParameterInfoProvider) language service.

## Quick Info

[IntelliPrompt quick info](../../user-interface/intelliprompt/quick-info.md) is the ability to display helpful tooltips when hovering over various identifiers and some language keywords in the code editor.  When hovering over an identifier, the resolver attempts to determine what the identifier means, and the resulting information is presented in a quick info tip.

Detailed information is shown for namespaces, types, members, parameters, and variables.  Appropriate documentation comments are displayed when available.

IntelliPrompt quick info features are implemented by the [VBQuickInfoProvider](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBQuickInfoProvider) language service.

## Code Snippets

[IntelliPrompt code snippets](../../user-interface/intelliprompt/code-snippets.md) provide the ability to insert pre-defined text fragments and for the end user to edit fields declared within the text.  When a code snippet provider with code snippets is registered as a service on the language, the automated completion list will list code snippets at appropriate times.  Typing a code snippet's shortcut, which is also inserted by selecting a code snippet from the completion list, and pressing `Tab` will activate a code snippet template session.

The [ICodeSnippetProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetProvider) service that is registered needs to have a root [ICodeSnippetFolder](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetFolder) set on it, with at least code snippet within it or its child folders.  A folder can be created programmatically. Or it can be loaded recursively via the [CodeSnippetFolder](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetFolder).[LoadFrom](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.CodeSnippetFolder.LoadFrom*) method.

This code creates a [VBSyntaxLanguage](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage) and registers a [VBCodeSnippetProvider](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBCodeSnippetProvider) service on it, which is a code snippet provider that is optimized for this language.  The provider has a root [ICodeSnippetFolder](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetFolder) assigned that contains all the snippets recursively found within the file system path specified by the `path` variable.  Note that `VB` is passed as a language name parameter when loading the folder.  This ensures that only code snippets designed for the Visual Basic language will be loaded.

```csharp
var language = new VBSyntaxLanguage();

ICodeSnippetFolder snippetFolder = CodeSnippetFolder.LoadFrom(path, "VB");
language.RegisterService(new VBCodeSnippetProvider() { RootFolder = snippetFolder });
```

## Navigable Symbol Selector

The [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) is a control that generally sits above and is bound to a [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).  It shows the types and members that currently enclose or are nearest the caret, and allows the end user to select other types/members within the same document via a dropdown, and to navigate to those members upon selection.

This control populates its symbol lists by the [VBNavigableSymbolProvider](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBNavigableSymbolProvider) language service.

## Disabling Automated IntelliPrompt

Automated IntelliPrompt features can be easily turned off for a language by unregistering the appropriate services that are installed by default.

This code disables IntelliPrompt features on the [VBSyntaxLanguage](xref:ActiproSoftware.Text.Languages.VB.Implementation.VBSyntaxLanguage) that is created:

```csharp
var language = new VBSyntaxLanguage();

// Disable completion lists
language.UnregisterService<VBCompletionProvider>();

// Disable parameter info
language.UnregisterService<VBParameterInfoProvider>();

// Disable quick info
language.UnregisterService<VBQuickInfoProvider>();

// Disable navigable symbol provider
language.UnregisterService<INavigableSymbolProvider>();
```
