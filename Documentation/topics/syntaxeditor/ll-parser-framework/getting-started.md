---
title: "Getting Started"
page-title: "Getting Started - SyntaxEditor LL(*) Parser Framework"
order: 2
---
# Getting Started

There are five steps involved in using the [LL(*) Parser Framework](index.md) to create a parser for a language.

## Assembly Requirements

The following list indicates the assemblies that are used with the LL(*) Parser Framework.

| Assembly | Required | Author | Licensed With | Description |
|-----|-----|-----|-----|-----|
| ActiproSoftware.Text.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | Core text/parsing framework for SyntaxEditor |
| ActiproSoftware.Text.LLParser.@@PlatformAssemblySuffix.dll | Yes | Actipro | SyntaxEditor | LL parser framework implementation |
| ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll | No \* | Actipro | SyntaxEditor | Core framework for all Actipro @@PlatformName controls |
| ActiproSoftware.SyntaxEditor.@@PlatformAssemblySuffix.dll | No \* | Actipro | SyntaxEditor | SyntaxEditor for @@PlatformName control |

*\* Not required however is used to integrate parsers made with the framework with a SyntaxEditor control.*

## Step 1: Constructing a Lexer

The core parser in the LL(*) Parser Framework depends on an [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) to provide tokens for consumption.

The easiest and recommended way to fulfill this need is to use the [Language Designer](../language-designer-tool/index.md) to create a new language.  Once you have a language designed, you can generate source files which you can then include in your project.  One of the generated source files will contain a class that implements the [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) interface.

The new language's lexer can more often than not be reused for the syntax highlighting engine.  Or if you have already created a [lexer](../language-creation/feature-services/lexer.md) for your language, you can probably reuse it here.

In some cases, a lexer used to drive syntax highlighting may have different tokenization requirements than the lexer used to feed tokens to the parser framework.  In this scenario, a second lexer with different logic can be created for use with the parser framework.

For more information on constructing a lexer for the parsing framework, view the [Lexer Preparation](lexer-preparation.md) topic.

## Step 2: Creating the Core Classes

There are a few classes which you will need to create in order to use the LL(*) Parser Framework.

The most important one, the [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar) class, contains the core of the whole setup.  Its constructor will set up the productions that tell the LL(*) Parser Framework how to interpret the syntax of your language.

An [ILLParser](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParser)-based class (you should generally inherit from [LLParserBase](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase)) is registered as a service to the [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) that is associated with the [SyntaxEditor](../index.md).

An [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader)-based class is necessary to provide tokens to the parsing framework.  For some languages with [mergable lexers](../text-parsing/lexing/basic-concepts.md), it is sufficient to use the existing [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) class.  However, in non-mergable lexer scenarios or scenarios where you wish to filter the tokens being supplied to the parser, you may need to subclass [TokenReaderBase](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.TokenReaderBase) or [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) for more control.

For more information on creating the core classes, view the [Parser Infrastructure](parser-infrastructure.md) topic.

## Step 3: Set Up Symbols and Non-Terminal Productions

Your grammar class will need to define the rules for how the core parser will interpret input.  This is done by defining all the symbols that are in your language and defining a number of productions which reference the symbols.

There are two types of symbols: terminals and non-terminals.  Terminals are the lowest-level fixed-input lexical element used in a grammar production rule.  In the case of Actipro's text framework, terminals equate to tokens that are read in from a lexer.  Non-terminals each have a production rule that is comprised of some combination of terminal and non-terminal symbol references.

For more information on setting up the symbols and non-terminal productions, view the [Symbols and EBNF Terms](symbols-and-terms.md) topic.

## Step 4: Add Customized Tree Constructors

The default tree constructors will output nearly everything that is parsed.  You will probably want to customize the tree construction to generate a cleaner tree.

There are a number of built-in tree construction nodes that should handle nearly all your tree construction needs.  These nodes are created by method calls on the [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar) class.  If you require special tree construction logic, there are provisions available.

For more information on adding customized tree constructors, view the [Tree Constructors](tree-constructors.md) topic.

## Step 5: Add Error Handling and Advanced Error Reporting

When building a grammar, it is important to cleanly handle errors and report helpful messages to the user.  This framework is intended to be used with [SyntaxEditor](../index.md), so we have to assume that the code passed to our parser will often be in an invalid state.  This is because the user is continuously typing and modifying it.

Error handling can be performed automatically, or manually via the use of error callbacks.  There are a number of built-in error callbacks that can be utilized, and you can also create custom error callbacks.

It is possible to assign an [ErrorAlias](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Symbol.ErrorAlias) to any [Terminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Terminal) and [NonTerminal](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.NonTerminal) instance, which gives you greater control over what automated messages are reported and when.

For more information on adding error handling and advanced error reporting, view the [Callbacks and Error Handling](callbacks-and-error-handling.md) topic.
