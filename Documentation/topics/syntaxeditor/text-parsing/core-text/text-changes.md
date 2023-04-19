---
title: "Text Changes and Operations"
page-title: "Text Changes and Operations - Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 7
---
# Text Changes and Operations

A large portion of the text framework is focused on the ability to make changes to document text.  Multiple individual replace transactions, called text change operations, can be batched up into a single text change and performed together as a single undoable modification.

## Text Changes vs. Text Change Operations

Text changes, represented by the [ITextChange](xref:ActiproSoftware.Text.ITextChange) interface, consist of one or more text change operations.  Each text change is considered a single undoable modification.

A text change operation, represented by the [ITextChangeOperation](xref:ActiproSoftware.Text.ITextChangeOperation) interface, is a single replace transaction.  The operation can insert text, delete text, or do both.

A text change and its operations are not performed until the text change is applied.

## Text Change Types

Each text change is described by an [ITextChangeType](xref:ActiproSoftware.Text.ITextChangeType) instance that is assigned to the [ITextChange](xref:ActiproSoftware.Text.ITextChange) when it is created.  Text change types can be anything, but some common examples are `Typing`, `Paste`, `Auto-Complete`, etc.

> [!NOTE]
> The [TextChangeTypes](xref:ActiproSoftware.Text.TextChangeTypes) static class provides quick access to the most common text change types.

If you are creating a text change for which no [TextChangeTypes](xref:ActiproSoftware.Text.TextChangeTypes) member property describes, you can build your own custom text change type by making a class that implements [ITextChangeType](xref:ActiproSoftware.Text.ITextChangeType).

## Single vs. Multiple Operation Text Changes

The text framework supports both single- and multiple-operation text changes.  It is up to you to decide which to use, but the key thing to remember is that each text change is considered a single undoable modification in the [undo history](../advanced-text/undo-history.md).

If you were just going to append some text to the end of the document, you'd use a single-operation text change.  If you were writing some code to insert a comment delimiter at the start of multiple lines, you'd use a multiple-operation text change instead.

## Single-Operation Text Changes

The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument) interface provides a number of helper methods that quickly create and instantly apply single-operation text changes:

### AppendText Method

The [AppendText](xref:ActiproSoftware.Text.ITextDocument.AppendText*) method appends text to the end of the current snapshot's text.

There are two overloads of this method.  One overload accepts an [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) parameter.

This code appends text to the end of the document while also defining an option to keep the current selection:

```csharp
TextChangeOptions options = new TextChangeOptions();
options.RetainSelection = true;
document.AppendText(TextChangeTypes.Custom, "This text appended to the end of the document.", options);
```

### DeleteText Method

The [DeleteText](xref:ActiproSoftware.Text.ITextDocument.DeleteText*) method performs a delete text change in the current snapshot.

There are four overloads of this method.  Various overloads accept offset/length parameters vs. a [TextRange](xref:ActiproSoftware.Text.TextRange) parameter.  Two overloads accept an [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) parameter.

This code deletes the first 5 characters in a document:

```csharp
document.DeleteText(TextChangeTypes.Custom, 0, 5);
```

### InsertText Method

The [InsertText](xref:ActiproSoftware.Text.ITextDocument.InsertText*) method performs an insert text change in the current snapshot at the specified offset.

There are two overloads of this method.  One overload accepts an [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) parameter.

This code demonstrates commenting the first line of a document by inserting comment characters:

```csharp
document.InsertText(TextChangeTypes.CommentLines, 0, "//");
```

### ReplaceText Method

The [ReplaceText](xref:ActiproSoftware.Text.ITextDocument.ReplaceText*) method performs a replace text change in the current snapshot at the specified offset.

There are four overloads of this method.  Various overloads accept offset/length parameters vs. a [TextRange](xref:ActiproSoftware.Text.TextRange) parameter.  Two overloads accept an [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) parameter.

This code demonstrates replacing 4 characters at the beginning of a document with a single tab character (i.e., replacing spaces with tabs):

```csharp
document.ReplaceText(TextChangeTypes.AutoFormat, 0, 4, "\t");
```

### SetText Method

The [SetText](xref:ActiproSoftware.Text.ITextDocument.SetText*) method replaces all the text in the current snapshot.

There are three overloads of this method.  The simplest overload that only accepts a `String` parameter replaces all the text in the current snapshot and also clears the [undo history](../advanced-text/undo-history.md).  One overload accepts an [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) parameter.

This code demonstrates replacing all text in a document:

```csharp
document.SetText(TextChangeTypes.Custom, "New document text.");
```

> [!TIP]
> When using the methods above, you do not interact directly with an [ITextChange](xref:ActiproSoftware.Text.ITextChange) since it is created and applied behind the scenes for you.

## Multiple-Operation Text Changes

Use the following techniques whenever you are going to perform a number of replacements that should be considered a single undoable modification.

### Creating an ITextChange

First, create an [ITextChange](xref:ActiproSoftware.Text.ITextChange) object by calling the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[CreateTextChange](xref:ActiproSoftware.Text.ITextDocument.CreateTextChange*).

This code creates an [ITextChange](xref:ActiproSoftware.Text.ITextChange):

```csharp
TextChangeOptions options = new TextChangeOptions();
options.OffsetDelta = TextChangeOffsetDelta.SequentialOnly;
ITextChange change = document.CreateTextChange(TextChangeTypes.CommentLines, options);
```

The [ITextChangeType](xref:ActiproSoftware.Text.ITextChangeType) parameter describes the text change that will be performed.

In this sample we also used the overload of [CreateTextChange](xref:ActiproSoftware.Text.ITextDocument.CreateTextChange*) that accepts an [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) parameter.  One of the settings in the options was set to apply a "sequential-only" offset delta.  This is described in detail in the "Text Change Options" section below.

### Creating a Reader

Most often, when performing multiple operations in a text change, you need to scan an [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  To do this, create an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) as described in the [Scanning Text Using a Reader](scanning-text.md) topic.

But be sure to use the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) for which the [ITextChange](xref:ActiproSoftware.Text.ITextChange) is based.  This ensures that the offsets will be properly in sync.

This code retrieves an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) for the [ITextChange](xref:ActiproSoftware.Text.ITextChange)'s snapshot, starting at offset `0`:

```csharp
ITextSnapshotReader reader = change.Snapshot.GetReader(0);
```

### Scanning Using a Reader and Adding Operations

Often the next code that you'd implement is to scan through the snapshot using the [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) that was created.

This code uses the reader to scan through code and insert a C-like comment at the start of each line:

```csharp
while (!reader.IsAtSnapshotEnd) {
	change.InsertText(reader.Offset, "//");
	reader.ReadCharacterThrough('\n');
}
```

### Applying the Text Change

Finally, when you have completely constructed the text change, call its [Apply](xref:ActiproSoftware.Text.ITextChange.Apply*) method to perform the text change on the document.

This code applies the text change.

```csharp
change.Apply();
```

## Text Change Options

All methods that either quickly perform a single-operation text change or create an [ITextChange](xref:ActiproSoftware.Text.ITextChange) have an overload that accepts an optional [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) object.  This object allows you to specify some additional settings that are used when the text change is applied.

> [!TIP]
> The [TextChangeOptions](xref:ActiproSoftware.Text.Implementation.TextChangeOptions) class provides a default implementation of the [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) interface.

### Custom Data

The [CustomData](xref:ActiproSoftware.Text.ITextChangeOptions.CustomData) property allows you to pass some custom data object with the text change.  This custom data object will be persisted in the undo history.  Therefore when examining the undo history or undoing back to this change, you can retrieve the custom data object via the [IUndoableTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChange).[CustomData](xref:ActiproSoftware.Text.Undo.IUndoableTextChange.CustomData) property.

### Check Read-Only

By default, programmatically-created text changes will not check for read-only regions when applying the text change.  You can change this behavior to force the text change to cancel if it attempts to alter a read-only region by setting the [CheckReadOnly](xref:ActiproSoftware.Text.ITextChangeOptions.CheckReadOnly) property to `true`.

### Offset Delta

The [OffsetDelta](xref:ActiproSoftware.Text.ITextChangeOptions.OffsetDelta) property can be used to auto-apply an offset delta when performing multiple operations in a text change.  This feature is described in the following section.

### Retain Selection

By default, when a text change is applied, any attached editor will move its caret to the end of the last operation in the text change.  This behavior can be disabled by setting then [RetainSelection](xref:ActiproSoftware.Text.ITextChangeOptions.RetainSelection) property to `true`.

### Source

Sometimes it's useful to know the source object that created the text change.  This most often is some sort of editor view and can be passed in the [Source](xref:ActiproSoftware.Text.ITextChangeOptions.Source) property.

### Can Merge into Previous Change

When the [CanMergeIntoPreviousChange](xref:ActiproSoftware.Text.ITextChangeOptions.CanMergeIntoPreviousChange) property is `true`, the text change will try and merge into a previous text change on the undo stack.  This is useful when you want to apply a programmatically-created text change that shouldn't have its own undo history entry.  Enabling this feature means that if an undo occurs after this text change is applied, the undo will include all of the operations in the merged text change along with those of the text change they were merged into, all acting as a single atomic text change.

The [ITextChange](xref:ActiproSoftware.Text.ITextChange).[IsMerged](xref:ActiproSoftware.Text.ITextChange.IsMerged) property returns if a text change is the result of two or more text changes being merged.

## Offset Deltas

Text changes have a great optional feature called offset deltas that make things much easier when performing multiple operations in a single text change.  To explain the benefit, let's examine a simple scenario.

Assume that you scan a snapshot and determine that you would like to insert the text `//` at offsets `0` and `10`.  Therefore you create an [ITextChange](xref:ActiproSoftware.Text.ITextChange).  The first insert operation would be made at offset `0` and would insert `2` characters.  Now we have a problem since if we make the second insert operation at offset `10`, it would be off by `2` characters from what we originally wanted.  By default, operations added to a text change are made at absolute offsets, so that means at the offsets that will exist after the previous operations that have occurred for the text change.

There are two ways around this.  The first and harder way is to keep some sort of delta variable yourself to determine that any operations you perform need to be offset by the delta.  Then after each operation, update the delta to account for the deletion and insertion lengths of the operation.  For the scenario example above, you'd have to tell the second insert operation to occur at offset `12`, not `10`, to achieve the correct results.

Obviously, that is tedious, so the text framework provides a really handy feature to do the delta calculations for you.  The only caveat is that you must be performing the operations in forward sequence, meaning no operation can start at an offset that is earlier than the start offsets of any previously-added operation.

In the multi-operation example above, we activate this feature by passing options to the text change that set [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions).[OffsetDelta](xref:ActiproSoftware.Text.ITextChangeOptions.OffsetDelta) to `TextChangeOffsetDelta.SequentialOnly`.  When this option is set, all offsets you pass for operations can be based on the original offsets in the snapshot you scan.  This means that in our scenario example above, you can tell the second insert operation to occur at offset `10`.

## Auto Character Casing

The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[AutoCharacterCasing](xref:ActiproSoftware.Text.ITextDocument.AutoCharacterCasing) property enables control over the casing of text that is entered into the document via text changes.  It supports these [CharacterCasing](xref:ActiproSoftware.Text.CharacterCasing) values:

| Value | Description |
|-----|-----|
| `Normal` | The case of characters is left unchanged, which is the default. |
| `Upper` | Converts all characters to uppercase. |
| `Lower` | Converts all characters to lowercase. |
