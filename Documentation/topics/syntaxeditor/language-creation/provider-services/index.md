---
title: "Overview"
page-title: "Provider Services - SyntaxEditor Language Creation Guide"
order: 1
---
# Overview

Syntax languages can register multiple instances of service objects implementing provider interfaces.  These provider interfaces can help drive features like tagging, adornments, and IntelliPrompt.

## Tagger Provider

Tagger provider services allow [taggers](../../text-parsing/tagging/taggers.md) to be created for a document or view.  Each language should provide a token tagger for documents using the language.  Token taggers read tokens from the language's lexer and marks them with a logical categorization such as `Identifier`, `Keyword`, `Comment`, or anything else.  These classifications can then be translated to highlighting styles, thereby achieving syntax highlighting in SyntaxEditor.

See the [Tagger Provider](tagger-provider.md) topic for more information.

## Adornment Manager Provider

Adornment manager provider services allow [adornment layers](../../user-interface/adornment/adornment-layers.md) to be created for a view.

See the [Adornment Manager Provider](adornment-manager-provider.md) topic for more information.

## IntelliPrompt Completion Provider

Languages can choose to support [IntelliPrompt completion lists](../../user-interface/intelliprompt/completion-list.md) and respond to <kbd>Ctrl</kbd>+<kbd>Space</kbd>.

See the [IntelliPrompt Completion Provider](completion-provider.md) topic for more information.

## IntelliPrompt Quick Info Provider

Languages can choose to support [IntelliPrompt quick info](../../user-interface/intelliprompt/quick-info.md) popups that are automatically displayed when the pointer moves over text and/or other parts of the editor, such as margins.

See the [IntelliPrompt Quick Info Provider](quick-info-provider.md) topic for more information.

## IntelliPrompt Parameter Info Provider

Languages can choose to support [IntelliPrompt parameter info](../../user-interface/intelliprompt/parameter-info.md) popups that are automatically displayed when the end user types an invocation, and can provide additional information about the current parameter.

See the [IntelliPrompt Parameter Info Provider](parameter-info-provider.md) topic for more information.

## IntelliPrompt Code Snippet Provider

Languages can choose to support [IntelliPrompt code snippets](../../user-interface/intelliprompt/code-snippets.md) that provide a way to insert pre-defined fragments of text into the editor.  Each code snippet can declare multiple fields of text, and when a code snippet template session is activated in SyntaxEditor, the text is inserted and the end user can tab between the fields to edit their values.

See the [IntelliPrompt Code Snippet Provider](code-snippet-provider.md) topic for more information.
