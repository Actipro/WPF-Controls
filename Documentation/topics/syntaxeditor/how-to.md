---
title: "How To..."
page-title: "How To... - SyntaxEditor Reference"
order: 5
---
# How To...

Since the documentation for SyntaxEditor is quite large, this topic provides a quick jump off point for the most common tasks of working with the product.

## Text Features

### How to get text from a document?

Immutable snapshots are what store the text for a document.  Each [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) points to its current text content via the [CurrentSnapshot](xref:ActiproSoftware.Text.ITextDocument.CurrentSnapshot) property.

The [Documents, Snapshots, and Versions](text-parsing/core-text/documents-snapshots-versions.md) topic talks about some basic methods for getting the full text or substrings from a snapshot.

The [Scanning Text Using a Reader](text-parsing/core-text/scanning-text.md) topic talks about more advanced scanning of a snapshot's text, characters, tokens, and more.

### How to make changes to a document?

Text changes can be made using a couple different methods, both described in the [Text Changes and Operations](text-parsing/core-text/text-changes.md) topic.  Batch changes that are flagged as a single undoable unit are fully supported.

### How to load a document from or save a document to a file/Stream?

The text content of an [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) can easily be saved to a file or `Stream` via methods described in the [Loading and Saving Files](text-parsing/core-text/loading-saving-files.md) topic.

### How to convert between offsets and text positions?

The [Offsets, Ranges, and Positions](text-parsing/core-text/offsets-ranges-positions.md) topic describes all the types of character indices used by the text framework and how to convert between them.

### Why are offsets that I externally found not syncing up with document offsets?

Normal multi-line strings in Windows use both carriage return and newline characters as line terminators.  When [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) objects load text, they convert all line terminators to newline characters only.  This helps with parsing performance.

The [Line Terminators](text-parsing/core-text/line-terminators.md) topic talks about why this is done and how to get document text back out using any common line terminator.

### How to programmatically find and replace text?

The [Low-Level Search Operations](text-parsing/advanced-text/searching.md) topic describes how to programmatically find/replace text using the lowest-level search object model.

### How to perform an undo or redo?

The [Undo History](text-parsing/advanced-text/undo-history.md) topic describes how to perform an undo or redo operation.

### How to obtain statistics for snapshot text or a string?

The [Text Statistics](text-parsing/advanced-text/statistics.md) topic shows how to easy obtain a wealth of statistics about snapshot text or text within a `String`.

### How to export a snapshot's text to HTML or RTF?

The [Exporting to HTML / RTF](text-parsing/advanced-text/exporting.md) topic describes how to export a snapshot's text to HTML or RTF format.

## Parsing Features

### How to comment or uncomment text in a document?

The [Line Commenting](text-parsing/advanced-text/line-commenting.md) topic demonstrates how to comment or uncomment text.

## IntelliPrompt Features

### How to display a completion list in response to the end user typing a '.', '<', etc.?

The [Completion List](user-interface/intelliprompt/completion-list.md) topic contains several samples on how to display a completion list in response to user typing.
