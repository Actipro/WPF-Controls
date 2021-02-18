---
title: "Overview"
page-title: "SyntaxEditor Text/Parsing Framework"
order: 1
---
# Overview

The text and parsing framework included with this product provide for complete separation from the user interface and can be reused across mutiple platforms.

## Core Text Features

The core text features presented in this portion of the documentation are essential knowledge for working with nearly every other portion of the product.

See the [Core Text Features](core-text/index.md) topic for more information.

## Syntax Languages

Syntax languages are objects that provide access to services and functionality related to the parsing of a code language, such as C#, VB, HTML, etc.

See the [Syntax Languages](syntax-languages.md) topic for more information.

## Lexing

Lexing is the process of scanning text and breaking it up into meaningful tokens that can be examined by a higher-level parser.

See the [Lexing](lexing/index.md) topic for more information.

## Tagging

Tagging is a generic mechanism that allows any sort of data to be associated with a text range.  This tagged data can then be retrieved on-demand by tag aggregators.  Tagging is the core mechanism behind features such as tokenization, classification (which in turn drives syntax highlighting when used in an editor), and more.

See the [Tagging](tagging/index.md) topic for more information.

## Parsing

Parsing is the process of performing syntax and/or semantic analysis on a text, and outputting some sort of parse data, generally an AST.  The parsing framework supports automated calling of parsers via worker threads following text changes.  Any custom or third-party parser (such as ANTLR) can be called.

See the [Parsing](parsing/index.md) topic for more information.

## Advanced Text Features

The text framework contains numerous advanced features that build upon the [core text features](core-text/index.md), everything from a search object model to the exporting of HTML and RTF.

See the [Advanced Text Features](advanced-text/index.md) topic for more information.
