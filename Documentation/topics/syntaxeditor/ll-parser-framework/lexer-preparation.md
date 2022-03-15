---
title: "Lexer Preparation"
page-title: "Lexer Preparation - SyntaxEditor LL(*) Parser Framework"
order: 3
---
# Lexer Preparation

The core parser in the LL(*) Parser Framework depends on an [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) to provide tokens for consumption.  This topic discusses how to properly prepare a lexer for use with a parser.

The easiest and recommended way to fulfill this need is to use the [Language Designer](../language-designer-tool/index.md) to create a new language.  Once you have a language designed, you can generate source files which you can then include in your project.  One of the generated source files will contain a class that implements the [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) interface.

As mentioned above, any [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer)-based object will work with the LL(*) parser.  Thus you don't have to use the Language Designer to create your lexer.  All the techniques described in the [Lexing](../text-parsing/lexing/index.md) portion of the documentation apply.

## Designing A Parser-Friendly Dynamic Language

A lexer that is to be used with the parser framework has to take into account some special considerations.  This section assumes you are already familiar with the [Language Designer](../language-designer-tool/index.md) tool and understand the basic terminology.

Keywords for your language should each be defined with a separate token ID and key.  This is because your parser will need to know which keyword was used in a particular location, not just that it is a keyword that was used.

It is best to avoid token rules that capture compound constructs.  If you leave that sort of thing to the parser to handle, you will get more useful error messages and handling.  Make tokens for the lowest possible level of constructs, such as individual symbols and punctuation, identifiers, numbers, etc.

It can be good practice to make a comment state and a string state for languages that have comments and strings.  This allows for better handling of unterminated strings and comments and makes it easy to generate tokens for text that is within a string or comment.

## Generate Source Files

You may be used to loading a language definition file (.langdef) into your application to provide a [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage) for use with your [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor), but the parsing framework's needs are more granular.  For this reason, you need to generate source files for the language instead of the .langdef file.  This will ensure that [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer), [ITokenIdProvider](xref:ActiproSoftware.Text.Lexing.ITokenIdProvider), and other classes are built that can be used by the parser.

See the [Parser Infrastucture](parser-infrastructure.md) topic for more information on how to set up your [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage).

## Advanced Languages

In some cases, a lexer used to drive syntax highlighting may have different tokenization requirements than the lexer used to feed tokens to the parser framework.  In this scenario, a second lexer with different logic can be created for use with the parser framework.

The lexer used to drive syntax highlighting should always be registered on your syntax language as a [lexer service](../language-creation/feature-services/lexer.md), while the lexer used to provide tokens to the parser framework can optionally be different.

See the [Parser Infrastructure](parser-infrastructure.md) topic for more information on how to set a different lexer up for the parser.
