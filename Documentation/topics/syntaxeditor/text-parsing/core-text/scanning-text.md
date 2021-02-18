---
title: "Scanning Text Using a Reader"
page-title: "Scanning Text Using a Reader - Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 6
---
# Scanning Text Using a Reader

The text framework has very robust features built-in for scanning through snapshot text via the use of readers.

## ITextBufferReader vs. ITextSnapshotReader

There are two types of readers included with the text framework: [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) and [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader).

### ITextBufferReader

[ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) provides a very fast, low-level interface for scanning through text.  It reads character-by-character, but does allow for jumping to a specific offset.

The current character's value, offset, and [TextPosition](xref:ActiproSoftware.Text.TextPosition) can be retrieved at any time.  This interface also has some help methods like [ReadThrough](xref:ActiproSoftware.Text.ITextBufferReader.ReadThrough*), that advance the reader until it encounters and passes a particular character.

Please see the [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) members list in the class library to see what scanning features are available for use.

### ITextSnapshotReader

[ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) is a higher-level interface for scanning through text, and is made specifically for reading an [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) instance.  It internally uses a [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) but wraps the core buffer reader with a ton of extended functionality.

A snapshot reader can return the current character, offset, [TextPosition](xref:ActiproSoftware.Text.TextPosition), current [IToken](xref:ActiproSoftware.Text.Lexing.IToken), current token text, current [ITextSnapshotLine](xref:ActiproSoftware.Text.ITextSnapshotLine), and more.

It has a large number of helper methods for navigating through text, such as [ReadToken](xref:ActiproSoftware.Text.ITextSnapshotReader.ReadToken*), [GoToNextTokenWithId](xref:ActiproSoftware.Text.ITextSnapshotReader.GoToNextTokenWithId*), etc.

Please see the [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) members list in the class library to see what scanning features are available for use.

## Getting an ITextSnapshotReader for a Snapshot

To get an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) for an [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot), call the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[GetReader](xref:ActiproSoftware.Text.ITextSnapshot.GetReader*) method.

There are two overloads for this method.  Each accept a parameter indicating to where in the snapshot the reader should be initialized.  The first overload accepts an offset and the second accepts a [TextPosition](xref:ActiproSoftware.Text.TextPosition).

This code retrieves an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) for the current text in a document, starting at offset `0`:

```csharp
ITextSnapshotReader reader = document.CurrentSnapshot.GetReader(0);
```

## Getting an ITextBufferReader for a Snapshot

As mentioned above, the [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) implementations use a core [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) instance.  So once you have an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) instance via the code in the previous section, simply use its [BufferReader](xref:ActiproSoftware.Text.ITextSnapshotReader.BufferReader) property to return the wrapped [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader).

This code retrieves an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) from the reader defined above:

```csharp
ITextBufferReader bufferReader = reader.BufferReader;
```

## Notes on Performance

If you only need to do core character scanning for a snapshot, use an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader).  It is the most low-level reader available and therefore has the fastest performance.

You also can do basic text/character scanning with an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader).  This will incur a negligible performance loss since all of those members essentially just call members on the core [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader).

Performance, while still fast, will be lessened a little when using the various members on [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) that deal with tokens and token scanning.  Any of these methods do additional work to ensure that tokens are in sync with the reader.  However overall they should still perform fast, as this is a common usage for readers.

The [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader).[Options](xref:ActiproSoftware.Text.ITextSnapshotReader.Options) property allows you to customize some optimizations used for token scanning to aid in performance.  Since tokens are virtualized in the text/parsing framework, tweaking these options to best fit your scenario can make a difference in scanning performance.  The following section describes the options.

## ITextSnapshotReader Options

The [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) interface has an [Options](xref:ActiproSoftware.Text.ITextSnapshotReader.Options) property that allows you to modify the options for the reader instance.  The options are of type [ITextSnapshotReaderOptions](xref:ActiproSoftware.Text.ITextSnapshotReaderOptions) and must be changed prior to using any members on the reader, if you are changing the options.

The options are mainly used to configure optimizations for token scanning, since tokens are virtualized in the text/parsing framework.  If you will not be doing token scanning with yoru reader, you do not need to change the options.  Even if you are doing token scanning, you can optionally leave the default options if you wish.

The options basically deal with how large of a block of tokens to load up, both initially and afterward, when scanning through text.  They also let you indicate whether you primarily plan on scanning forward, backward, or both.

Please see the [ITextSnapshotReaderOptions](xref:ActiproSoftware.Text.ITextSnapshotReaderOptions) members list in the class library to see what options available for use.

## Getting an ITextBufferReader for a String

The [StringTextBufferReader](xref:ActiproSoftware.Text.Implementation.StringTextBufferReader) class is an implementation of [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) that operates on a `string`.

This code creates a [StringTextBufferReader](xref:ActiproSoftware.Text.Implementation.StringTextBufferReader) for a string:

```csharp
StringTextBufferReader bufferReader = new StringTextBufferReader("Text to read");
```

## Getting a System.IO.TextReader for an ITextBufferReader

The [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader).[AsTextReader](xref:ActiproSoftware.Text.ITextBufferReader.AsTextReader*) method can be used to create a `System.IO.TextReader` instance.

`System.IO.TextReader` objects are defined in the core .NET framework and are useful for passing to various external components that are based on text readers.
