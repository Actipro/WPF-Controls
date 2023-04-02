---
title: "Automated IntelliPrompt"
page-title: "Automated IntelliPrompt - SyntaxEditor Python Language Add-on"
order: 8
---
# Automated IntelliPrompt

The Python language has built-in automated IntelliPrompt completion list, parameter info, quick info, and navigable symbol selector features that greatly enhance the end user's editing experience.

## Completion List

[IntelliPrompt completion lists](../../user-interface/intelliprompt/completion-list.md) are used to present end users with a list of accessible members from the code editor's current caret location.  In many cases, the completion list is triggered by key events such as pressing <kbd>Ctrl</kbd>+<kbd>Space</kbd> or typing a `.` character after an identifier.

There are two types of completion list scenarios when typing in code blocks.  First is when listing members of something.  This can occur when typing a `.` character after an identifier.  The second type is when showing all accessible members based on the current context.  This can occur when starting to type a new sequence of identifiers.

In cases where a completion list is triggered via <kbd>Ctrl</kbd>+<kbd>Space</kbd>, if there is a single match for the text that has been typed, it will not show the list but will auto-complete the match.  This saves the user from having to type the entire identifier/keyword that was matched.

The completion list features are implemented by the [PythonCompletionProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonCompletionProvider) class.

## Parameter Info

[IntelliPrompt parameter info](../../user-interface/intelliprompt/parameter-info.md) is the ability to display helpful tooltips when typing an invocation that presents all of the signature overloads, along with information on the current parameter being typed.

Parameter info is displayed for function and method invocations.

IntelliPrompt quick info features are implemented by the [PythonParameterInfoProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonParameterInfoProvider) language service.

## Quick Info

[IntelliPrompt quick info](../../user-interface/intelliprompt/quick-info.md) is the ability to display helpful tooltips when hovering over various identifiers and some language keywords in the code editor.  When hovering over an identifier, the resolver attempts to determine what the identifier means, and the resulting information is presented in a quick info tip.

Detailed information is shown for packages, modules, types, functions/methods, parameters, and variables.  Appropriate docstring comments are displayed when available.

IntelliPrompt quick info features are implemented by the [PythonQuickInfoProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonQuickInfoProvider) language service.

## Navigable Symbol Selector

The [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) is a control that generally sits above and is bound to a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).  It shows the types and attributes that currently enclose or are nearest the caret, and allows the end user to select other types/attributes within the same document via a dropdown.

The [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector).[AreMemberSymbolsSupported](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector.AreMemberSymbolsSupported) property should be set to `false` when working with Python since only one drop-down is used.

This control populates its symbol lists by the [PythonNavigableSymbolProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonNavigableSymbolProvider) language service.

## Showing Full Docstrings

By default, any docstrings that show in IntelliPrompt popups will only be the summary line.  The summary line is the text up through the first whitespace line within docstring.  This behavior can be altered for various IntelliPrompt providers so that the entire docstring is displayed instead.

This code tells each IntelliPrompt provider to display the entire docstring:

```csharp
language.RegisterService(new PythonCompletionProvider() { DocstringDisplayMode = PythonDocstringDisplayMode.All });
language.RegisterService(new PythonParameterInfoProvider() { DocstringDisplayMode = PythonDocstringDisplayMode.All });
language.RegisterService(new PythonQuickInfoProvider() { DocstringDisplayMode = PythonDocstringDisplayMode.All });
```

## Disabling Automated IntelliPrompt

Automated IntelliPrompt features can be easily turned off for a language by unregistering the appropriate services that are installed by default.

This code disables IntelliPrompt features on the [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) that is created:

```csharp
var language = new PythonSyntaxLanguage();

// Disable completion lists
language.UnregisterService<PythonCompletionProvider>();

// Disable parameter info
language.UnregisterService<PythonParameterInfoProvider>();

// Disable quick info
language.UnregisterService<PythonQuickInfoProvider>();

// Disable navigable symbol provider
language.UnregisterService<INavigableSymbolProvider>();
```
