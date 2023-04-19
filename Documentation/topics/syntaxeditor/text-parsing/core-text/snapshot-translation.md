---
title: "Snapshot Translation"
page-title: "Snapshot Translation - Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 5
---
# Snapshot Translation

Offsets and offset ranges can be translated from one snapshot to another using a feature called snapshot translation.  Snapshots are immutable views of a document and are able to determine the changes made between any two snapshots for the same document.  This allows for the locating of a certain offset or range of text in the current snapshot based on some offset/range created using an older snapshot, even with multiple document changes being made in between the two snapshots.

> [!NOTE]
> See the [Documents, Snapshots, and Versions](documents-snapshots-versions.md) topic for a basic overview on snapshots and how different snapshots relate to each other within a document.

## Scenario 1: Threaded Parsing

Say you start some time-consuming parsing on the most current snapshot, which we'll call snapshot 1.  While the parsing is executing in a separate thread on snapshot 1, more text changes in the document are made, bringing us to what we'll call snapshot 2.  Then assume snapshot 1's parsing completes and returns an AST of the document.

All the offsets for the AST are based on the text found in snapshot 1.  Since snapshot 2 is the most current snapshot, the offsets for the AST are most likely incorrect by some delta amount since they were not based on snapshot 2.

The text framework can translate offsets from one snapshot to another, taking any text changes into account.  So, if we were editing a document in an editor and wanted to jump directly to a class name whose offset we know from the AST based on snapshot 1, we can translate the offset from snapshot 1 to the current snapshot (snapshot 2), and move the editor's caret right to the translated offset.

## Scenario 2: Text Range Results

Another common scenario where snapshot translation is useful is when working with some sort of parsing result set.

Say you do a "find all" operation on a snapshot and list the replace locations in a `ListBox`.  By tracking a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) for each result, you could move directly to the found text on double-clicks by translating the clicked range to the current snapshot, even after multiple edits have been made following the find all operation.

## Storing Results in Preparation for a Translation

The [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) structures can both be used to translate offsets/ranges.

[TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) stores an offset and references a particular [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  Likewise, [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) stores a text range and references a particular [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).

Both have `TranslateTo` methods that are used to translate the offset/range to a new [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).

When persisting results related to a parse operation, such as in the two scenarios above, there are a couple recommended options.  The first options is to directly use [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) instances to store the data.  Alternatively, if you have some sort of result set, have the root result set store the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) from which the results were found and use integer offsets or [TextRange](xref:ActiproSoftware.Text.TextRange) structures to store the individual results.  With this approach, you can use the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) and offset/range to create [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) and [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) instances as needed.

## Offset Translation

The [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) structure provides functionality for translating an offset from one snapshot to another.  By calling the [TranslateTo](xref:ActiproSoftware.Text.TextSnapshotOffset.TranslateTo*) method, a new [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) can be retrieved, indicating the offset value within the target [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).

The [TranslateTo](xref:ActiproSoftware.Text.TextSnapshotOffset.TranslateTo*) method accepts two parameters: the target [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot), and a [TextOffsetTrackingMode](xref:ActiproSoftware.Text.TextOffsetTrackingMode) value.

The tracking mode indicates how the offset tends to move when text changes are made next to or surrounding it, between the original snapshot and the target snapshot.  See the [TextOffsetTrackingMode](xref:ActiproSoftware.Text.TextOffsetTrackingMode) enumeration documentation for details on available values.

This code shows how to translate an offset to a new snapshot after changes are made to the document:

```csharp
TextSnapshotOffset offset = new TextSnapshotOffset(document.CurrentSnapshot, 10);
// Document changes made here, meaning new snapshots are created
TextSnapshotOffset translatedOffset = offset.TranslateTo(document.CurrentSnapshot, TextOffsetTrackingMode.Negative);
```

A [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) can be marked as "deleted" via its [IsDeleted](xref:ActiproSoftware.Text.TextSnapshotOffset.IsDeleted) property.  The [Deleted](xref:ActiproSoftware.Text.TextSnapshotOffset.Deleted) static property provides access to an instance of the structure that has been marked "deleted".  This variation of the offset may be useful when you wish to flag that some edit operation removed the range that contained the offset.

## Text Range Translation

The [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) structure provides functionality for translating an offset range (or [TextRange](xref:ActiproSoftware.Text.TextRange)) from one snapshot to another.  By calling the [TranslateTo](xref:ActiproSoftware.Text.TextSnapshotRange.TranslateTo*) method, a new [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) can be retrieved, indicating the offset range within the target [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).

The [TranslateTo](xref:ActiproSoftware.Text.TextSnapshotRange.TranslateTo*) method accepts two parameters: the target [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot), and a [TextRangeTrackingModes](xref:ActiproSoftware.Text.TextRangeTrackingModes) value.

The tracking modes indicates how the range tends to update when text changes are made next to or surrounding it, between the original snapshot and the target snapshot.  See the [TextRangeTrackingModes](xref:ActiproSoftware.Text.TextRangeTrackingModes) enumeration documentation for details on available values.

This code shows how to translate a text range to a new snapshot after changes are made to the document:

```csharp
TextSnapshotRange range = new TextSnapshotRange(document.CurrentSnapshot, 10, 15);
// Document changes made here, meaning new snapshots are created
TextSnapshotRange translatedRange = range.TranslateTo(document.CurrentSnapshot, TextRangeTrackingModes.Default);
```

When translating a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange), it is possible that the resulting range no longer exists.  For example, this could happen if the user deleted the line that contained the range.  In these cases, the [IsDeleted](xref:ActiproSoftware.Text.TextSnapshotRange.IsDeleted) property may be set to `true`, depending on which tracking mode options were used.

> [!NOTE]
> The [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange).[IsDeleted](xref:ActiproSoftware.Text.TextSnapshotRange.IsDeleted) static property provides access to an instance of the structure that has been marked "deleted".
