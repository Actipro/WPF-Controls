---
title: "Automated IntelliPrompt"
page-title: "Automated IntelliPrompt - XML Language - SyntaxEditor Web Languages Add-on"
order: 10
---
# Automated IntelliPrompt

The XML language has built-in automated IntelliPrompt completion list and quick info features that greatly enhance the end user's editing experience.

## Completion List

[IntelliPrompt completion lists](../../user-interface/intelliprompt/completion-list.md) are used to present end users with a list of accessible elements, attributes, and attribute values from the code editor's current caret location.  In many cases, the completion list is triggered by key events such as pressing <kbd>Ctrl</kbd>+<kbd>Space</kbd> or typing a `<` character, etc.

In cases where a completion list is triggered via <kbd>Ctrl</kbd>+<kbd>Space</kbd>, if there is a single match for the text that has been typed, it will not show the list but will auto-complete the match.  This saves the user from having to type the entire name/value that was matched.

The completion list features are implemented by the [XmlCompletionProvider](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlCompletionProvider) class.

## Quick Info

[IntelliPrompt quick info](../../user-interface/intelliprompt/quick-info.md) is the ability to display helpful tooltips when hovering over various identifiers in the code editor.  When hovering over an identifier, the resolver attempts to determine what the identifier means, and the resulting information is presented in a quick info tip.

Detailed information is shown for elements, attributes, and attribute values.

IntelliPrompt quick info features are implemented by the [XmlQuickInfoProvider](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlQuickInfoProvider) language service.

## Navigable Symbol Selector

The [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) is a control that generally sits above and is bound to a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).  It shows the elements and attributes that currently enclose or are nearest the caret and allows the end user to select other elements/attributes within the same document via a dropdown, and to navigate to those members upon selection.

This control populates its symbol lists by the [XmlNavigableSymbolProvider](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlNavigableSymbolProvider) language service.

## Disabling Automated IntelliPrompt

Automated IntelliPrompt features can be easily turned off for a language by unregistering the appropriate services that are installed by default.

This code disables IntelliPrompt features on the [XmlSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Xml.Implementation.XmlSyntaxLanguage) that is created:

```csharp
var language = new XmlSyntaxLanguage();

// Disable completion lists
language.UnregisterService<XmlCompletionProvider>()

// Disable quick info
language.UnregisterService<XmlQuickInfoProvider>();

// Disable navigable symbol provider
language.UnregisterService<INavigableSymbolProvider>();
```
