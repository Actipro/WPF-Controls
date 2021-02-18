---
title: "Overview"
page-title: "SyntaxEditor LL(*) Parser Framework"
order: 1
---
# Overview

The LL(*) Parser Framework is Actipro's own framework for constructing robust text parsers that work standalone or with code editor controls like SyntaxEditor.  The framework features grammars that are written in C#/VB using EBNF-like notation, customizable AST construction, advanced error handling/reporting, easy code injection, a complete debugger UI, and much more.

A parsing framework is the key to enhancing a [SyntaxEditor](../index.md) editing experience because it gives us meaningful information about what is contained in the document.  This not only can be used to provide contextual information to the end user (what method is the caret in), but is used to help drive features like automated [IntelliPrompt](../user-interface/intelliprompt/index.md) completion lists, parameter info, and quick info.

## Parser Type

The parser in our framework is LL(*), meaning it is a top-down parser that can run on a subset of context-free grammars.  It parses input from left to right, traces leftmost derivation, and by default uses one symbol of look-ahead.  This normally would mean LL(1), however [can-match callbacks](callbacks-and-error-handling.md) allow infinite symbol look-ahead, thus making it LL(*).

LL parsers do not support left recursion however grammars can generally be refactored to eliminate left recursion and turn it into right recursion instead.  Ambiguity can be resolved using can-match callbacks.  Examples of handling both scenarios are given in this framework's documentation.

## Feature Summary

There are many types of parsers, each with their pros and cons.  Actipro's LL(*) Parser Framework was carefully designed to be easy-to-use, provide an enormous feature set, and address the most common shortcomings encountered with other third-party parsers.

The frameworks features include:

- A top-down LL parser that supports infinite symbol look-ahead.

- Grammars built directly in C#/VB code using EBNF-like syntax, made possible via operator overloading, implicit type conversion, and delegates.

- No code generation, everything is interpreted.

- Pre-existing grammars can be programmatically altered before they are compiled and used.

- Can-match callbacks can provide code-based determination of whether a non-terminal can match with the parser's current state.

- Custom code callbacks can be injected anywhere in the grammar before/after an EBNF term is parsed, or on success/error.

- Automated parse error reporting and recovery options, with the ability to customize.

- All productions support customized AST (abstract syntax tree) construction rules using numerous built-in tree constructor options, and ability to create custom tree constructors.

- Generated [AST nodes](../text-parsing/parsing/ast-nodes.md) are automatically assigned the proper [text offsets](../text-parsing/core-text/offsets-ranges-positions.md).

- Re-use [ILexer](../text-parsing/lexing/index.md) implementations that have already been made for a language.

- Filter out tokens that are meaningless to the parser (comments, etc.).

- Designed to integrate easily with [SyntaxEditor](../index.md).

- Complete debugger UI in the [Language Designer](../language-designer-tool/index.md) tool that allows you to step through a parser to see exactly how it is matching and building results.

## Getting Started

There are five steps involved in using the [LL(*) Parser Framework](index.md) to create a parser for a language.

See the [Getting Started](getting-started.md) topic for more information.

## Lexer Preparation

The core parser in the LL(*) Parser Framework depends on an [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) to provide tokens for consumption.  This topic discusses how to properly prepare a lexer for use with a parser.

See the [Lexer Preparation](lexer-preparation.md) topic for more information.

## Parser Infrastructure

There are several classes that will need to be created and connected in order to use the LL(*) Parser Framework.  This topic makes the process easy by guiding you through each step that is required.

See the [Parser Infrastructure](parser-infrastructure.md) topic for more information.

## Symbols and EBNF Terms

A grammar is a set of symbols, both terminals or non-terminals, along with production rules that determine how to parse text for a formal language, where there is a single starting symbol from which parsing should begin.

See the [Symbols and EBNF Terms](symbols-and-terms.md) topic for more information.

## Walkthrough: Symbols and EBNF Terms

In this walkthrough, we are going to write a grammar for a language called Simple.  The [Simple language](../simple-language.md) is basically a small subset of a Javascript-like language.  When we're done, we'll load it up into a SyntaxEditor and we'll look at the AST results that are generated for us based on some input code.

See the [Walkthrough: Symbols and EBNF Terms](walkthrough-symbols-and-terms.md) topic for more information.

## Tree Constructors

Non-terminal productions will by default produce a very verbose AST that generally contains much more information than is useful for later consumption.  The LL(*) Parser Framework has an extremely powerful tree construction mechanism with numerous built-in helper methods to do common tree construction tasks.  You also have the ability to create completely custom tree construction nodes via C#/VB code if you like.

See the [Tree Constructors](tree-constructors.md) topic for more information.

## Walkthrough: Tree Constructors

In this walkthrough, we are going to revisit the Simple language and will enhance our grammar productions with tree constructors so that we make the resulting AST very concise.

See the [Walkthrough: Tree Constructors](walkthrough-tree-constructors.md) topic for more information.

## Callbacks and Error Handling

The next step in building a grammar is to make sure that it properly handles errors.  After all, since this grammar framework is intended to be used with SyntaxEditor, we have to assume that most of the time the document’s code passed to our grammar parser will be in an invalid state.  The user is continuously typing and modifying it.

In this topic, we will look at the various callbacks that are available to you, probably the most important of which are the error handling callbacks.  We'll also dig into error handling options.

See the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic for more information.

## Walkthrough: Callbacks and Error Handling

In this topic we’ll examine the importance of adding error handling.  We’ll enhance our Simple language grammar to properly recover from nearly any invalid syntax in a document so that it can continue parsing the rest of the document.

See the [Walkthrough: Callbacks and Error Handling](walkthrough-callbacks-and-error-handling.md) topic for more information.

## Custom Data

The [IParserState](xref:ActiproSoftware.Text.Parsing.LLParser.IParserState) object passed to each callback in the parser framework has a property on it where custom data can be tracked and updated throughout each parsing operation.

Anything can be placed in this custom data store.  It could be used to help perform semantic validation, construct name tables, and more.

See the [Custom Data](custom-data.md) topic for more information.
