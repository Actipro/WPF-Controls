---
title: "Overview"
page-title: "Feature Services - SyntaxEditor Language Creation Guide"
order: 1
---
# Overview

Syntax languages have a wide array of optional features that can be implemented to enhance their capabilities when used with SyntaxEditor.

## Lexer

A lexer is the lowest-level key piece of a syntax language.  It examines a stream of text and parses it into tokens that give text ranges meaning.  Tokens are then used to support many other language features such as classification.

See the [Lexer](lexer.md) topic for more information.

## Parser

A parser is something that performs syntax and/or semantic analysis on code and often returns parse data such as an abstract syntax tree (AST), list of syntax errors, symbol table, etc.  Syntax languages can have parsers registered with them that get called automatically via worker threads when text changes in code documents occur.

See the [Parser](parser.md) topic for more information.

## Outliner

An outliner returns a language-specific outlining source for a particular [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) when requested by an outlining manager.  Having an outliner service registered on a language is what tells SyntaxEditor that the language supports automatic outlining.

See the [Outliner](outliner.md) topic for more information.

## Line Commenter

Syntax languages support line commenters, which provide built-in functionality for commenting and uncommenting text based on language-specific delimiters.

See the [Line Commenter](line-commenter.md) topic for more information.

## Example Text

Syntax languages have the ability to expose a small code snippet showing many of the constructs in the language.

See the [Example Text](example-text.md) topic for more information.

## Text Statistics

While the text/parsing framework includes an implementation of text statistics that provides the most common statistics for text, sometimes it's useful to create custom statistics classes that are tailored to a particular language and can return additional statistics such as comment coverage.

See the [Text Statistics](text-statistics.md) topic for more information.

## Word Break Finder

While most languages use the same criteria for locating word breaks, some languages like may wish to include a `@` character as the start of a word.

See the [Word Break Finder](word-break-finder.md) topic for more information.

## Indent Provider (Auto-Indent)

Syntax languages support indent providers, which provide functionality for auto-indentation of the code when the <kbd>Enter</kbd> key is pressed.

See the [Indent Provider (Auto-Indent)](indent-provider.md) topic for more information.

## Text Formatter

Syntax languages support text formatters, which provide functionality for formatting the layout of whitespace and symbols such as braces to make code more readable.

See the [Text Formatter](text-formatter.md) topic for more information.

## IntelliPrompt Navigable Symbol Provider

Languages can choose to support an IntelliPrompt navigable symbol provider that lists the accessible symbols within an editor's document.  Once a language implements this provider service, the [Navigable Symbol Selector](../../user-interface/intelliprompt/navigable-symbol-selector.md) control can be paired with a `SyntaxEditor` control to provide Visual Studio type/member dropdown-like functionality.

See the [IntelliPrompt Navigable Symbol Provider](navigable-symbol-provider.md) topic for more information.

## Structure Matcher

Syntax languages support structure matchers, which provide functionality for locating matching structural text delimiters, such as brackets.

See the [Structure Matcher](structure-matcher.md) topic for more information.

## Code Block Finder

A code block finder finds the snapshot range of a logical code block that contains a specified snapshot range.  This service is used to drive the [Code Block Selection](../../user-interface/editor-view/code-block-selection.md) feature.

See the [Code Block Finder](code-block-finder.md) topic for more information.

## Auto-Corrector

An auto-corrector is a language service that typically occurs after text changes and can fix up things like character casing.

A pre-built class is available that can help add automatic case correction to a language.

See the [Auto-Corrector](auto-corrector.md) topic for more information.

## Delimiter Auto-Completer

A delimiter auto-completer is a language service that can insert an end delimiter when its related start delimiter is typed.

A pre-built class is available that performs nearly all the work of this feature.

See the [Delimiter Auto-Completer](delimiter-auto-completer.md) topic for more information.

## Line Number Provider

A line number provider allows the line numbers displayed in a [line number margin](../../user-interface/editor-view/editor-view-margins.md) to be customized.  While any string value can be displayed for each view line, one-based integers are most commonly used.

See the [Line Number Provider](line-number-provider.md) topic for more information.
