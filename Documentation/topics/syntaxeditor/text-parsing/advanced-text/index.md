---
title: "Overview"
page-title: "Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 1
---
# Overview

The text framework contains numerous advanced features that build upon the [core text features](../core-text/index.md), everything from a search object model to the exporting of HTML and RTF.

## Low-Level Search Operations

The text framework contains an object model for performing low-level search operations.  This low-level object model is usually called by higher-level search object models that are more view-based.

See the [Low-Level Search Operations](searching.md) topic for more information.

## Undo History

The text framework's undo history tracks undoable text changes, allows for examination and manipulation of the undo/redo stacks, and provides access to the change types (unsaved/saved) for snapshot lines.

See the [Undo History](undo-history.md) topic for more information.

## Text Statistics

Text statistics are a powerful feature that provide statistics about text in a document.  By default, there are numerous text statistics available, even including things like readability scores.  Syntax languages can also choose to customize the statistics by supplying additional ones such as commented-line counts, etc.

See the [Text Statistics](statistics.md) topic for more information.

## Exporting to HTML / RTF

Text from a snapshot can easily be exported to a `String` or file in various HTML and RTF (rich text) formats.

See the [Exporting to HTML / RTF](exporting.md) topic for more information.

## Line Commenting

The text framework includes an extensible interface for commenting and uncommenting regions of text.  Both line-based and range comment styles are supported.

See the [Line Commenting](line-commenting.md) topic for more information.

## Text Formatting

The text framework includes an interface for formatting regions of text, where whitespace and other symbols such as braces are adjusted to make code more readable.

See the [Text Formatting](text-formatting.md) topic for more information.

## Structure Matching

The text framework includes support for locating matching structural text delimiters, such as brackets.

See the [Structure Matching](structure-matching.md) topic for more information.

## Lipsum Generator

A special utility class is included that supports the generation of "lorem ipsum" placeholder text.  Placeholder text is useful for developers and designers when prototyping out web pages and screens.  It is designed to contain unreadable gibberish sentences, which are ideal for layout.

See the [Lipsum Generator](lipsum-generator.md) topic for more information.

## Document Properties

The [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[Properties](xref:ActiproSoftware.Text.ICodeDocument.Properties) property provides a dictionary-like object that can store any sort of custom data that should be associated with the document.

See the [Document Properties](document-properties.md) topic for more information.
