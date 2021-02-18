---
title: "Overview"
page-title: "SyntaxEditor Irony Add-on"
order: 1
---
# Overview

[Irony](https://github.com/IronyProject/Irony) is a framework for constructing recognizers, interpreters, compilers, and translators from grammatical descriptions containing actions. Irony provides excellent support for tree construction, tree walking, translation, error recovery, and error reporting.

While Irony can do advanced parsing and abstract syntax tree construction for the text in the editor (via calls to our multi-threaded [parsing framework](../text-parsing/parsing/index.md)), it unfortunately doesn't support incremental lexing and therefore cannot be used to drive syntax highlighting in the editor.

The Actipro Irony Add-on provides a bridge between an Irony parser and the SyntaxEditor control.  Namely, if you have created a grammar with Irony, you can use it to perform syntax and semantic parsing within SyntaxEditor.

The best part is that all this can be done in only **a few lines of code**!

> [!NOTE]
> SyntaxEditor's integration with Irony parsers via this add-on is being deprecated.  We offer and recommend our own advanced [LL(*) Parser Framework](../ll-parser-framework/index.md) instead that features grammars that are written in C#/VB using EBNF-like notation, customizable AST construction, advanced error handling/reporting, easy code injection, a complete debugger UI, and much more.

## Getting Started

The Irony Add-on makes it possible in just a few lines of code to create a syntax language that can automatically call an Irony parser whenever document text is modified and asynchronously return its parse tree or AST result to the document.  This topic walks through the basic concepts involved, the requirements, and includes a complete sample of how to pull everything together.

> [!NOTE]
> Due to this add-on being deprecated, the sample project that shows off this add-on is no longer installed.  Please contact our support team if you have a need to download it.

See the [Getting Started](getting-started.md) topic for more information.
