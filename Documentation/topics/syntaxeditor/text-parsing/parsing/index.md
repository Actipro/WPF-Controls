---
title: "Overview"
page-title: "Parsing - SyntaxEditor Text/Parsing Framework"
order: 1
---
# Overview

Parsing is the process of performing syntax and/or semantic analysis on a text, and outputting some sort of parse data, generally an AST.  The parsing framework supports automated calling of parsers via worker threads following text changes.  Any custom or third-party parser @if (wpf) {(such as ANTLR)} can be called.

## Parse Requests and Dispatchers

Parse requests are created to request that syntax/semantic analysis be performed on some text. [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) is capable of automatically making parse requests when its text changes, so long as its [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) has an [IParser](xref:ActiproSoftware.Text.Parsing.IParser) service registered.

A parse request dispatcher is notified of incoming parse requests and queues them.  It uses one or more worker threads to execute the requested parsing operations in prioritized order, thereby preventing any blocking of the main UI thread.

See the [Parse Requests and Dispatchers](parse-requests-and-dispatchers.md) topic for more information.

## Parsers

Parsers are objects that take in a parser request and output some sort of parse data.  They are generally called via worker threads to prevent the blocking of the main UI thread.

The parse data returned by a parser is a result of performing syntax and/or semantic analysis on some text indicated by a parse request.  Parsers can optionally be implemented to call into other parsing frameworks @if (wpf) {(Irony, ANTLR, etc.)} and return their results.

See the [Parsers](parsers.md) topic for more information.

## AST Nodes

An abstract syntax tree (AST) is a structural tree representation of source code, without the specific details of the language, such as punctuation.  An AST is comprised of AST Nodes, each which represent a particular construct in the concrete syntax.

See the [AST Nodes](ast-nodes.md) topic for more information.

## Code Fragments

Documents support code fragment editing, whereby the editable document text is a code fragment that is to be surrounded by header and footer text for the purposes of parsing.  This is useful in situations where a language with parsing capabilties is used by a document but the end user should only be able to edit the contents of a specific method, expression, etc.

See the [Code Fragments](code-fragments.md) topic for more information.

## The Actipro LL(*) Parser Framework

Actipro offers a very robust parser framework that makes it simple to build parsers using EBNF-like notation in C# and VB.  It is free to use, has AST construction, error handling, callbacks, and a lot of other features that make it integrate nicely with our core text/parsing framework.

See the [LL(*) Parser Framework](../../ll-parser-framework/index.md) section for more information.

@if (wpf) {

## Free Integration with Popular Third-Party Parsers

There are two free add-ons that come with SyntaxEditor that make it easy to integrate with popular third-party parsers:

- [ANTLR Add-on](../../antlr-addon/index.md)
- [Irony Add-on](../../irony-addon/index.md)

}
