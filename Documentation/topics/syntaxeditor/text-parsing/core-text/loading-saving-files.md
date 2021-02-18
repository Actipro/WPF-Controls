---
title: "Loading and Saving Files"
page-title: "Loading and Saving Files - Core Text Features - SyntaxEditor Text/Parsing Framework"
order: 8
---
# Loading and Saving Files

It's very easy to load document text from a file, stream, or string.  Likewise, document text can be saved back out to a file, stream, or string.

## Loading Text Directly from a File/Stream

Document text may be loaded from files using one of the overloads of the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) method.

Use of a [LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) method will reset the [IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property to `false` and will clear the undo history.  Use of the [LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) method overloads that accept a path parameter will auto-update the [FileName](xref:ActiproSoftware.Text.ITextDocument.FileName) property with the file path.

### Load from File System Using UTF-8 Encoding

The first overload of [LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) simply accepts a path parameter. It loads files using standard UTF-8 encoding.

This code loads text from a file using UTF-8 encoding:

```csharp
document.LoadFile(@"C:\Code.cs");
```

### Load from File System Using a Specified Encoding

The second overload of [LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) accepts a path parameter as well as an `Encoding` parameter.  It loads files using the `Encoding` that you specify.  This allows for easy loading of non-UTF-8 encoded files.

This code loads text from a file using ASCII encoding:

```csharp
document.LoadFile(@"C:\Code.cs", Encoding.ASCII);
```

### Load from Stream Using a Specified Encoding

The third overload accepts a `Stream` parameter as well as an `Encoding` parameter.  It loads from the stream using the `Encoding` that you specify.  This allows for easy loading of non-UTF-8 encoded files.

This code loads text from a `Stream` using UTF-8 encoding:

```csharp
document.LoadFile(stream, Encoding.UTF8);
```

## Loading Text from a String

Sometimes a document's text is fetched using an external process, such as from a database.  In this case, the text to place in the document is available as a string variable.

The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[SetText](xref:ActiproSoftware.Text.ITextDocument.SetText*) method can be used to completely replace the document text with a string.

There are several overloads of the [SetText](xref:ActiproSoftware.Text.ITextDocument.SetText*) available.  The overload that only accepts a `String` parameter will will reset the [IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property to `false` and will clear the undo history.  The other overloads will not do this.

This code replaces the text content of a document from a string, marks the [IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property to `false`, and clears the undo history:

```csharp
string text = "int a;";
document.SetText(text);
```

This code replaces the text content of a document from a string, without updating the [IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property or the undo history:

```csharp
string text = "int a;";
document.SetText(TextChangeTypes.Typing, text);
```

## Saving Text to a File

Document text may be saved to files using one of the overloads of the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[SaveFile](xref:ActiproSoftware.Text.ITextDocument.SaveFile*) method.

### Save to File Using UTI-8 Encoding

The first overload simply accepts a path parameter and a [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) parameter.  It saves files using standard UTF-8 encoding.

This code saves document text to a file using UTF-8 encoding:

```csharp
document.SaveFile("C:\Code.cs", LineTerminator.CarriageReturnNewline);
```

### Save to File Using a Specified Encoding

The second overload accepts a path parameter, a [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) parameter, as well as an `Encoding` parameter.  It saves files using the `Encoding` that you specify.  This allows for easy saving of non-UTF-8 encoded files.

This code saves document text to a file using ASCII encoding:

```csharp
document.SaveFile("C:\Code.cs", Encoding.ASCII, LineTerminator.CarriageReturnNewline);
```

### Save to Stream Using a Specified Encoding

The third overload accepts a `Stream` parameter, a [LineTerminator](xref:ActiproSoftware.Text.LineTerminator) parameter, as well as an `Encoding` parameter.  It saves to the stream using the `Encoding` that you specify.  This allows for easy saving of non-UTF-8 encoded files.

This code saves document text to a `Stream` using UTF-8 encoding:

```csharp
document.SaveFile(stream, Encoding.UTF8, LineTerminator.CarriageReturnNewline);
```

## Updating the IsModified Property

After saving a document, it is a good idea to mark it as no longer modified.  Save points are tracked in the undo history so that you are notified when the document reaches the save point.

This code sets a save point by telling a document that it is no longer modified:

```csharp
document.IsModified = false;
```

When the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property is set to `false` all unsaved changes in undo history's change tracking will be switched to saved changes.

Calling any of the `SaveFile` overloads with a path parameter will automatically set the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property to `false` if the specified path is the same as the document's [FileName](xref:ActiproSoftware.Text.ITextDocument.FileName) property value.

## Getting Text into a String

Sometimes a document's text needs to be saved using an external process, such as to a database.  In this case, the text to save needs to be retrieve into a string variable.

The current snapshot of a document (see the [Documents, Snapshots, and Versions](documents-snapshots-versions.md) topic for more information) is available via the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[CurrentSnapshot](xref:ActiproSoftware.Text.ITextDocument.CurrentSnapshot) property.  The [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot).[GetText](xref:ActiproSoftware.Text.ITextSnapshot.GetText*) method can be used to retrieve a snapshot's text content with a specified [LineTerminator](xref:ActiproSoftware.Text.LineTerminator).  The [Text](xref:ActiproSoftware.Text.ITextSnapshot.Text) property returns the snapshot's text using standard Windows carriage return / newline line terminators.

This code fetches the current document text into a string:

```csharp
string text = document.CurrentSnapshot.Text;
```

## Exporting to HTML / RTF

The text framework supports exporting document text to HTML and RTF formats.

See the [Exporting to HTML / RTF](../advanced-text/exporting.md) topic for more information on this feature.

## Storing the File Name of a Document

The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[FileName](xref:ActiproSoftware.Text.ITextDocument.FileName) property can be used to store the file name (full path) of the file that has been loaded into the document.  The [LoadFile](xref:ActiproSoftware.Text.ITextDocument.LoadFile*) method overloads that read a file automatically set this property.

When the [FileName](xref:ActiproSoftware.Text.ITextDocument.FileName) property value is changed, the [FileNameChanged](xref:ActiproSoftware.Text.ITextDocument.FileNameChanged) event fires.
