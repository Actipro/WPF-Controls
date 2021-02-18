---
title: "Overview"
page-title: "SyntaxEditor IntelliPrompt Features"
order: 1
---
# Overview

SyntaxEditor includes some extremely powerful IntelliPrompt features that help enhance end user productivity.  These features range from popup lists with text completion to popup tips that provide more information about what the end user it editing.

## Sessions

An IntelliPrompt session is essentially a controller for a certain type of IntelliPrompt UI.  Each type of IntelliPrompt UI (completion list, quick info, etc.) has a related session.  The IntelliPrompt UI and functionality can be made activate by "opening" the related session.

See the [Sessions](sessions.md) topic for more information.

## Quick Info

IntelliPrompt quick info displays helpful popup hints about what is under the mouse or next to the caret.  Tip content can include any WPF control and it's easy to generate richly-formatted content using an HTML-like syntax.

See the [Quick Info](quick-info.md) topic for more information.

## Completion List

The IntelliPrompt completion list allows you create popups for displaying a list of options used to complete what the end user is typing.  This is most used when editing programming languages.  Features include ctrl+space support, description tips, multiple matching algorithms, matched text highlights, filters, and more.

See the [Completion List](completion-list.md) topic for more information.

## Parameter Info

IntelliPrompt parameter info displays helpful popup hints about an invocation that is being typed, and its parameters.  Tip content can include any WPF control and it's easy to generate richly-formatted content using an HTML-like syntax.

See the [Parameter Info](parameter-info.md) topic for more information.

## Code Snippets

IntelliPrompt code snippets provide a way to insert pre-defined fragments of text into the editor.  Each code snippet can declare multiple fields of text, and when a code snippet template session is activated in SyntaxEditor, the text is inserted and the end user can tab between the fields to edit their values.

See the [Code Snippets](code-snippets.md) topic for more information.

## Content Providers

Content providers are classes that provide content on demand to IntelliPrompt sessions that display popups.  An example of their usage is providing content for quick info and completion item description tip popups.

See the [Content Providers](popup-content-providers.md) topic for more information.

## Image Source Providers

Image source providers are classes that provide `ImageSource` objects on demand to IntelliPrompt sessions.  An example of their usage is providing image sources for completion list items.

See the [Image Source Providers](image-source-providers.md) topic for more information.

## Navigable Symbol Selector

The [NavigableSymbolSelector](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.NavigableSymbolSelector) control displays two side-by-side drop-downs similar to the type/member drop-downs above the code editor in Visual Studio.  One drop-down shows all available root symbols (generally types), and the other shows all available member symbols within the currently selected root symbol.

As the caret in a bound [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor) instance is moved, the selections in the [NavigableSymbolSelector](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.NavigableSymbolSelector) update to indicate the enclosing symbols (types/members).  The end user can also select a different symbol from the drop-downs to navigate directly to the related symbol declaration.

See the [Navigable Symbol Selector](navigable-symbol-selector.md) topic for more information.
