---
title: "Overview"
page-title: "SyntaxEditor Regular Expression Guide"
order: 1
---
# Overview

The text/parsing framework includes a custom traditional backtracking Nondeterministic Finite Automation (NFA) regular expression engine.  Other products that use a similar engine are the .NET framework, Perl, Python, Emacs, and Tcl.  This regular expression engine is used by features such as the [search object model](../text-parsing/advanced-text/searching.md), allowing for regular expression searching.

The regular expression engine also is the core for performing lexing and tokenization with a [dynamic lexer](../text-parsing/lexing/dynamic-lexers.md).  The engine is very robust and is capable of parsing even the most complicated of languages.

## Language Elements

The text/parsing framework contains a regular expression engine that accepts an extensive set of regular expression elements, enabling you to efficiently search for text patterns.  This topic details the set of characters, operators, and constructs that you can use to define regular expressions.

See the [Language Elements](language-elements.md) topic for more information.

## Dynamic Lexical Macros

The built-in regular expression engine, when used with [dynamic lexers](../text-parsing/lexing/dynamic-lexers.md), allows for the definition of macros that represent regular expression elements.  These macros are valid for use in any regular expression within a dynamic lexer and promote reusability of common patterns.

See the [Dynamic Lexical Macros](dynamic-lexical-macros.md) topic for more information.

## Invalid Regular Expressions

If you enter an invalid regular expression pattern that the regular expression engine cannot parse, an [InvalidRegexPatternException](xref:ActiproSoftware.Text.RegularExpressions.InvalidRegexPatternException) is raised with a message describing the problem.
