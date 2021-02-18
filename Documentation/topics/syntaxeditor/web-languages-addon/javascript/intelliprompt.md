---
title: "Automated IntelliPrompt"
page-title: "Automated IntelliPrompt - JavaScript Language - SyntaxEditor Web Languages Add-on"
order: 7
---
# Automated IntelliPrompt

The JavaScript language has built-in automated IntelliPrompt completion list features that greatly enhance the end user's editing experience.

## Completion List

[IntelliPrompt completion lists](../../user-interface/intelliprompt/completion-list.md) are used to present end users with a list of accessible members from the code editor's current caret location.  The completion list is triggered by key events such as pressing `Ctrl+Space`.

The JavaScript completion list is relatively simplistic in its implementation.  It will present a list of JavaScript keywords, along with identifiers that have been defined in the document.  Note that the identifiers listed do not account for their scope within the document.  As the end user types, the list is filtered down to only show matched items.  Both acronym and shorthand item matchers are enabled.

The completion list features are implemented by the [JavaScriptCompletionProvider](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JavaScriptCompletionProvider) class.

## Disabling Automated IntelliPrompt

Automated IntelliPrompt features can be easily turned off for a language by unregistering the appropriate services that are installed by default.

This code disables IntelliPrompt features on the [JavaScriptSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JavaScriptSyntaxLanguage) that is created:

```csharp
var language = new JavaScriptSyntaxLanguage();

// Disable completion lists
language.UnregisterService<JavaScriptCompletionProvider>();
```
