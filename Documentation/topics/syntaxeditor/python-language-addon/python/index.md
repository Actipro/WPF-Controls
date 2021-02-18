---
title: "Overview"
page-title: "SyntaxEditor Python Language Add-on"
order: 1
---
# Overview

The advanced Python syntax language implementation in the SyntaxEditor Python Language Add-on includes everything from code outlining to syntax error reporting.

> [!NOTE]
> The syntax language and its parser support both Python v3.4.1 and Python v2.7.6 syntax, toggleable via a flag when creating the syntax language.

## Features

The Python language is packed with features, including:

- Automated IntelliPrompt completion list, parameter info, and quick info.
- Syntax highlighting.
- Abstract syntax tree (AST) generation.
- Automatic code outlining based on AST structure.
- Reporting and automatic squiggle display of errors for invalid syntax.
- Mouse hover quick info for syntax errors.
- Line commenting.
- Smart indent.
- Code block selection.
- Delimiter (bracket) highlighting and auto-completion.
- Project that manage a project's code files and references to external libraries, such as the Python standard library.
- Package repository that stores and caches reflection information for Python packages.
- Support for navigable symbol selector (contextual drop-down).

## Licensing

Even though the Python Languages Add-on is distributed and demoed with the WPF controls, it is optional and is sold separately from SyntaxEditor and any containing bundles.  The pricing on the add-on is very cheap and licenses are all Enterprise licenses, meaning they cover your entire organization.

See the [Assemblies and Add-on Licensing](../../assemblies.md) topic for more information.

## Getting Started

This topic covers how to get started using the Python language from the Python Language Add-on, implemented by the [PythonSyntaxLanguage](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonSyntaxLanguage) class, and lists its requirements for supporting advanced features like parsing and automated IntelliPrompt.

It is very important to follow the steps in this topic to configure the language correctly so that its advanced features operate as expected.

See the [Getting Started](getting-started.md) topic for more information.

## Parsing and Parse Data

The Python language uses an advanced parser that has been constructed using the Actipro [LL(*) Parser Framework](../../ll-parser-framework/index.md), and provides functionality for building ASTs, reporting parse errors, and more.

The [ILLParseData](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParseData) results returned by this parser are consumed by multiple other features of the language, such as code outlining, parse error display, etc.

See the [Parsing and Parse Data](parsing.md) topic for more information.

## Indent Provider

An indent provider enables support for smart indent features when pressing ENTER.

See the [Indent Provider](indent-provider.md) topic for more information.

## Context

The Python language can return detailed context information about any offset in a document.  The context includes data such as containing AST node, target expression, and more.  This sort of information is essential in passing to the resolver to determine what to display in automated IntelliPrompt.

See the [Context](context.md) topic for more information.

## Resolver

Each Python [IProject](xref:ActiproSoftware.Text.Languages.Python.Reflection.IProject) has an [IResolver](xref:ActiproSoftware.Text.Languages.Python.Resolution.IResolver) that can examine a Python context and AST expression and return the results associated with that expression.  The resolver is used by the automated IntelliPrompt features to determine what to show in the UI for various scenarios.

See the [Resolver](resolver.md) topic for more information.

## Automated IntelliPrompt

The Python language has built-in automated IntelliPrompt completion list, parameter info, and quick info features that greatly enhance the end user's editing experience.

See the [Automated IntelliPrompt](intelliprompt.md) topic for more information.
