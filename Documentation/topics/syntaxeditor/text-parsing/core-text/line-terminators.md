---
title: "Line Terminators"
page-title: "Line Terminators - Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 4
---
# Line Terminators

The text framework can load text using any standard line terminator character sequence.  Document snapshots can return the line terminator kind that is used within the document and report when mixed line terminators are used.  Text can easily be normalized to any line terminator.

## Standard Line Terminators

The [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) enumeration has a value for each of the standard line terminators:

- [CRLF](xref:ActiproSoftware.Text.LineTerminator.CRLF) (`"\r\n"`) - Carriage return and line feed sequence.  This format is typically used on Windows machines.
- [LF](xref:ActiproSoftware.Text.LineTerminator.LF) (`"\n"`) - Line feed.  This format is typically used on UNIX and macOS machines.
- [CR](xref:ActiproSoftware.Text.LineTerminator.CR) (`"\r"`) - Carriage return.  Not commonly used.

<!--
- [LS](xref:ActiproSoftware.Text.LineTerminator.LS) (`"\u2028"`) - Line separator.  Not commonly used.
- [PS](xref:ActiproSoftware.Text.LineTerminator.PS) (`"\u2029"`) - Paragraph separator.  Not commonly used.
-->

## Document Snapshots Track Line Terminators

Document snapshots track the line terminator for each line in the document text.  This means that when reading characters in a snapshot, you could encounter any of the line terminator characters described above at line end, or even a CRLF sequence.

The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[HasUniformLineTerminators](xref:ActiproSoftware.Text.ITextSnapshot.HasUniformLineTerminators) property returns whether all of the line terminators in the document snapshot are the same.  When this property returns `false`, the line terminators are considered mixed.

The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[InferredLineTerminator](xref:ActiproSoftware.Text.ITextSnapshot.InferredLineTerminator) property returns a [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) value indicating which line terminator should be used in the document.  The value returned is based on what is identified in the document text.

In the case of mixed line terminators, priority is given to line terminators in the order of the list above.  A document with one CRLF line terminator and two LF line terminators in its text will return CRLF as the inferred line terminator, since the presence of any CRLF has a higher priority than LF.

When a document is empty, there are no line terminators from which to infer a result.  The system's line terminator via `Environment.NewLine` will be used to infer a result in that case.

## Getting Text and Substrings With Actual Line Terminators

These [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) members return text with the actual line terminators in the document text:

- [Text](xref:ActiproSoftware.Text.ITextSnapshot.Text) property - Returns the full snapshot text with actual line terminators.
- [GetSubstring](xref:ActiproSoftware.Text.ITextSnapshot.GetSubstring*) method - Returns a substring with actual line terminators when the optional [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument is not specified.
- [GetText](xref:ActiproSoftware.Text.ITextSnapshot.GetText*) method - Returns the full snapshot text with actual line terminators when the optional [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument is not specified.  The [Text](xref:ActiproSoftware.Text.ITextSnapshot.Text) property effectively calls this method with no [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) specified.

## Getting Text and Substrings With a Specific Line Terminator

These [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) members return text that contains line terminators normalized to a specified kind:

- [GetSubstring](xref:ActiproSoftware.Text.ITextSnapshot.GetSubstring*) method - Returns a substring with normalized line terminators when the [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument is specified.  The [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument indicates the kind of line terminator to use.
- [GetText](xref:ActiproSoftware.Text.ITextSnapshot.GetText*) method - Returns the full snapshot text with normalized line terminators when the [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument is specified.  The [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument indicates the kind of line terminator to use.

## Notes on Snapshot Readers

Any code that reads snapshot characters, such as while lexing or parsing, should support any CRLF sequence, LF, or CR as line terminators at a minimum.

<!--
Less commonly used line terminators such as LS or PS may also be optionally supported.
-->

## Normalizing Line Terminators

In scenarios where the line terminators in a document snapshot are mixed or simply not the desired line terminator kind, the document's line terminators can be normalized with a call to the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[NormalizeLineTerminators](xref:ActiproSoftware.Text.ITextDocument.NormalizeLineTerminators*) method.  The [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) argument indicates the line terminator kind to which all line terminators should be normalized.  No text change occurs if nothing needs to be updated.

This code shows how to normalize a document's line terminators to CRLF:

```csharp
document.NormalizeLineTerminators(LineTerminator.CRLF);
```
