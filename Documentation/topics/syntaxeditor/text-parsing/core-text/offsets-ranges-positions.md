---
title: "Offsets, Ranges, and Positions"
page-title: "Offsets, Ranges, and Positions - Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 3
---
# Offsets, Ranges, and Positions

There are several ways to reference the location of characters within a string or snapshot, each with their own benefits.

## Offsets

Offsets are zero-based integer numbers indicating a character index from the beginning of a string or snapshot.

The first character in a document is at index `0`.  The offset past the last character in a snapshot can be retrieved via the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[Length](xref:ActiproSoftware.Text.ITextSnapshot.Length) property.

## Text Ranges

It is often necessary to work with an ordered pair of offsets.  For instance, when getting a substring of text in a snapshot, you need to pass the offset range to obtain.

The [TextRange](xref:ActiproSoftware.Text.TextRange) structure represents an ordered pair of two offsets.  It has many methods for comparing the range with other offsets and ranges.

To create a text range that spans `2` characters starting at offset `3`, you'd create a text range with start offset `3` and end offset `5`.  Note that the end offset is the offset after the last character encapsulated by the range.

```csharp
TextRange range = new TextRange(3, 5);
```

This code creates a [TextRange](xref:ActiproSoftware.Text.TextRange) with the same offset values but creates it using start offset and length combinations instead.

```csharp
TextRange range = TextRange.FromSpan(3, 2);
```

## Text Positions

[TextPosition](xref:ActiproSoftware.Text.TextPosition) structures are similar to offsets in that they indicate a certain character within a string or snapshot.  However, they work in terms of a line index and a character index within that line.

The [Line](xref:ActiproSoftware.Text.TextPosition.Line) and [Character](xref:ActiproSoftware.Text.TextPosition.Character) values of text positions are zero-based so a text position representing line index `1` and character `7` references the eighth character on the second line in the text.

This code creates a [TextPosition](xref:ActiproSoftware.Text.TextPosition) with line index `1` and character `7`:

```csharp
TextPosition position = new TextPosition(1, 7);
```

## Text Position Ranges

Similar to how [TextRange](xref:ActiproSoftware.Text.TextRange) provides a range of offsets, the [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) structure provides a range of [TextPosition](xref:ActiproSoftware.Text.TextPosition) objects.

This code creates a [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) from position `(1,7)` to `(2,3)`:

```csharp
TextPositionRange positionRange = new TextPositionRange(1, 7, 2, 3);
```

## Converting Between Offsets and Text Positions

Sometimes it's useful to convert between offsets and text positions.  Likewise, conversion between text ranges and text positions is possible.

These members in the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) interface allow for conversion:

| Member | Description |
|-----|-----|
| [OffsetToPosition](xref:ActiproSoftware.Text.ITextSnapshot.OffsetToPosition*) Method | Returns the [TextPosition](xref:ActiproSoftware.Text.TextPosition) that represents the specified offset within the text lines. |
| [PositionRangeToTextRange](xref:ActiproSoftware.Text.ITextSnapshot.PositionRangeToTextRange*) Method | Returns the [TextRange](xref:ActiproSoftware.Text.TextRange) that represents the specified [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) within the snapshot. |
| [PositionToOffset](xref:ActiproSoftware.Text.ITextSnapshot.PositionToOffset*) Method | Returns the offset for the specified [TextPosition](xref:ActiproSoftware.Text.TextPosition) within the text lines. |
| [TextRangeToPositionRange](xref:ActiproSoftware.Text.ITextSnapshot.TextRangeToPositionRange*) Method | Returns the [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange) that represents the specified [TextRange](xref:ActiproSoftware.Text.TextRange) within the snapshot. |

## Snapshot Offsets

The [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) structure is a bit more advanced than a simple offset or [TextPosition](xref:ActiproSoftware.Text.TextPosition).  It stores a reference to a particular location within an [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) and provides access to the offset or [TextPosition](xref:ActiproSoftware.Text.TextPosition) of that location.

The root [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) and the [ITextSnapshotLine](xref:ActiproSoftware.Text.ITextSnapshotLine) that contains the snapshot offset are accessible from the snapshot offset.

This code creates a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) at offset `5` within the document's current [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot):

```csharp
TextSnapshotOffset snapshotOffset = new TextSnapshotOffset(document.CurrentSnapshot, 5);
```

Snapshot offsets can be translated to another snapshot.  Details on this are available in the [Snapshot Translation](snapshot-translation.md) topic.

## Snapshot Ranges

The [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) structure is similar to a [TextSnapshotOffset](xref:ActiproSoftware.Text.TextSnapshotOffset) in that it references a specific [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).  The difference is that it tracks two normalized locations within the snapshot.

Accordingly, it can do additional things like return the text contained within the range, give the offset/text positions of the start/end, etc.

To create a snapshot range that spans `2` characters starting at offset `3`, you'd create a snapshot range with start offset `3` and end offset `5`.  Note that the end offset is the offset after the last character encapsulated by the range.

```csharp
TextSnapshotRange snapshotRange = new TextSnapshotRange(document.CurrentSnapshot, 3, 5);
```

This code creates a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) with the same offset values but creates it using start offset and length combinations instead.

```csharp
TextSnapshotRange snapshotRange = TextSnapshotRange.FromSpan(document.CurrentSnapshot, 3, 2);
```

Snapshot ranges can be translated to another snapshot.  Details on this are available in the [Snapshot Translation](snapshot-translation.md) topic.

## Normalized Snapshot Range Collection

The [NormalizedTextSnapshotRangeCollection](xref:ActiproSoftware.Text.NormalizedTextSnapshotRangeCollection) class provides a collection implementation that normalizes all of its contained [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) objects.

This means that snapshot ranges in the collection are guaranteed to:

- All be based on the same [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).
- Be in sequential order by start offset.
- Not overlap (overlapping snapshot ranges are merged when the collection is built).

## Version Ranges

As described in the [Documents, Snapshots, and Versions](documents-snapshots-versions.md) topic, text versions are placeholders that provide the core functionality needed to link snapshots together and support features like [Snapshot Translation](snapshot-translation.md).

The [ITextVersionRange](xref:ActiproSoftware.Text.ITextVersionRange) interface designates an object that can refer to a text range within an [ITextVersion](xref:ActiproSoftware.Text.ITextVersion).  By itself, the [ITextVersionRange](xref:ActiproSoftware.Text.ITextVersionRange) object is not very useful since it doesn't provide direct access to the text range data.  It also holds no references to any text snapshots, which makes it very lightweight.

The nice thing about version ranges though is that they are a lightweight reference to a text range that can be easily translated to any snapshot range or other text range in a specific text version.  This makes them ideal for use in scenarios where you wish to store a version-oriented range for a long period of time, without needing to retain the related snapshot in memory.

Version ranges can be created with the various [ITextVersion](xref:ActiproSoftware.Text.ITextVersion).[CreateRange](xref:ActiproSoftware.Text.ITextVersion.CreateRange*) methods.

```csharp
ITextVersionRange versionRange = document.CurrentSnapshot.Version.CreateRange(1, 3);
```

Version ranges can be translated to any snapshot for the same document:

```csharp
TextSnapshotRange snapshotRange = versionRange.Translate(document.CurrentSnapshot);
```

Version ranges also can be translated to a text range for a particular lightweight [ITextVersion](xref:ActiproSoftware.Text.ITextVersion) that is also from the same document.

```csharp
TextRange range = versionRange.Translate(document.CurrentSnapshot.Version);
```

Another way a version range can be created is from a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange):

```csharp
ITextVersionRange versionRange = snapshotRange.ToVersionRange(TextRangeTrackingModes.Default);
```
