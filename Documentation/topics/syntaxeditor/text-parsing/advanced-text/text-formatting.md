---
title: "Text Formatting"
page-title: "Text Formatting - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 7
---
# Text Formatting

The text framework includes an interface for formatting regions of text. Text that has been formatted can be more readable because whitespace and symbols such as braces are uniformly placed.

## Formatting Logic

Text formatters implement the [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) interface.  This interface defines one method, the [Format](xref:ActiproSoftware.Text.ITextFormatter.Format*) method, which has one parameter that indicates the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) to examine and update.

The [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset).[Snapshot](xref:ActiproSoftware.Text.TextSnapshotOffset.Snapshot) property gives access to its [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  Likewise, the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[Document](xref:ActiproSoftware.Text.ITextSnapshot.Document) provides access to its owner [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).  The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[TabSize](xref:ActiproSoftware.Text.ITextDocument.TabSize) property tells how many spaces are in a tab, in case that information is useful while formatting.

If the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) is an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument), the [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument).[ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property may also contain AST or other language-specific information that can be useful when formatting.

When formatting, it is likely that multiple changes will be made.  These updates can be batched up into a single undoable text change.  For information on text changes, see the [Text Changes and Operations](../core-text/text-changes.md) topic.

Formatters are free to implement any custom logic to perform the formatting. However, there are most likely going to be similarities in the technique that most formatters use. Here are some common steps that a formatter might follow in the process of formatting a document:

1. Find the next token that allows whitespace between itself and the following non-whitespace token.

1. Replace all whitespace between these two tokens with the new whitespace, which can either be no whitespace at all, or a combination of any number of linebreaks, tabs and spaces.

1. Repeat from step 1 until the entire range is formatted.

## Registering a Text Formatter with a Syntax Language

An [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter) can be associated with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) by registering it with the language:

```csharp
language.RegisterService<ITextFormatter>(myTextFormatter);
```

Registering a text formatter with a language is optional, but recommended.

## Formatting a Range of Text

This code uses an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument)'s current language's text formatter to format a range of text from offset `0`-`10`:

```csharp
ITextFormatter textFormatter = document.Language.GetService<ITextFormatter>();
if (textFormatter != null) {
	TextSnapshotRange range = new TextSnapshotRange(document.CurrentSnapshot, 0, 10);
	textFormatter.Format(range.Snapshot, TextPositionRange.CreateCollection(range.PositionRange, false), TextFormatMode.Ranges);
}
```

This code uses an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument)'s current language's text formatter to format the entire document:

```csharp
ITextFormatter textFormatter = document.Language.GetService<ITextFormatter>();
if (textFormatter != null)
	textFormatter.Format(document.CurrentSnapshot, TextPositionRange.CreateCollection(document.CurrentSnapshot.SnapshotRange.PositionRange, false), TextFormatMode.All);

```
