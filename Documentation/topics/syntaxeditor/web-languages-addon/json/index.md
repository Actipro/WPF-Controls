---
title: "Overview"
page-title: "JSON Language - SyntaxEditor Web Languages Add-on"
order: 1
---
# Overview

The advanced JSON syntax language implementation in the SyntaxEditor Web Languages Add-on includes everything from code outlining to syntax error reporting.

## Features

The JSON language is packed with features, including:

- Syntax highlighting.
- Abstract syntax tree (AST) generation.
- Automatic code outlining based on AST structure.
- Reporting and automatic squiggle display of errors for invalid syntax.
- Mouse hover quick info for syntax errors.
- Smart indent.
- Text formatting.
- Code block selection.
- Delimiter (bracket) highlighting and auto-completion.
- Optional JavaScript style comment support.

## Licensing

Even though the Web Languages Add-on is distributed and demoed with the WPF controls, it is optional and is sold separately from SyntaxEditor and any containing bundles.  The pricing on the add-on is very cheap and licenses are all Enterprise licenses, meaning they cover your entire organization.

See the [Assemblies and Add-on Licensing](../../assemblies.md) topic for more information.

## Getting Started

This topic covers how to get started using the JSON language from the Web Languages Add-on, implemented by the [JsonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JsonSyntaxLanguage) class, and lists its requirements for supporting advanced features like parsing.

It is very important to follow the steps in this topic to configure the language correctly so that its advanced features operate as expected.

See the [Getting Started](getting-started.md) topic for more information.

## Parsing and Parse Data

The JSON language uses an advanced parser that has been constructed using the Actipro [LL(*) Parser Framework](../../ll-parser-framework/index.md), and provides functionality for building ASTs, reporting parse errors, and more.

The [ILLParseData](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParseData) results returned by this parser are consumed by multiple other features of the language, such as code outlining, parse error display, etc.

See the [Parsing and Parse Data](parsing.md) topic for more information.

## Indent Provider

An indent provider enables support for smart indent features when pressing ENTER.

See the [Indent Provider](indent-provider.md) topic for more information.
