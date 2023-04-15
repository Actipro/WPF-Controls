---
title: "Line Commenting"
page-title: "Line Commenting - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 6
---
# Line Commenting

The text framework includes an extensible interface for commenting and uncommenting regions of text.  Both line-based and range comment styles are supported.

Commenting is performed by inserting language-specific comment delimiters.  Uncommenting is performed by removing the language-specific comment delimiters.

## Registering a Line Commenter with a Syntax Language

Line commenters implement the [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) interface.  This interface defines two methods.

- The [Comment](xref:ActiproSoftware.Text.ILineCommenter.Comment*) method has parameters that indicate the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) to examine and the [ITextChangeOptions](xref:ActiproSoftware.Text.ITextChangeOptions) to use when inserting comment delimiters.
- The [Uncomment](xref:ActiproSoftware.Text.ILineCommenter.Uncomment*) method has the same parameters, although it removes comment delimiters instead.

An [ILineCommenter](xref:ActiproSoftware.Text.ILineCommenter) can be associated with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) by registering it with the language:

```csharp
language.RegisterService<ILineCommenter>(myLineCommenter);
```

Registering line commenters with a language is optional but recommended.

## Commenting a Range of Text

This code uses an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument)'s current language's line commenter to comment out a range of text from offset `0`-`10`:

```csharp
if (document.Language.LineCommenter != null) {
	TextSnapshotRange range = new TextSnapshotRange(document.CurrentSnapshot, 0, 10);
	ITextChangeOptions options = new TextChangeOptions();
	document.Language.LineCommenter.Comment(range, options);
}
```

## Uncommenting a Range of Text

This code uses an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument)'s current language's line commenter to uncomment a range of text from offsets `0` to `10`:

```csharp
if (document.Language.LineCommenter != null) {
	TextSnapshotRange range = new TextSnapshotRange(document.CurrentSnapshot, 0, 10);
	ITextChangeOptions options = new TextChangeOptions();
	document.Language.LineCommenter.Uncomment(range, options);
}
```

## Creating a Line-Based Line Commenter

Line-based line commenters are used in languages like C# where `//` should be used to comment out lines of text.  To accomplish this, create a [LineBasedLineCommenter](xref:ActiproSoftware.Text.Implementation.LineBasedLineCommenter) object.

[LineBasedLineCommenter](xref:ActiproSoftware.Text.Implementation.LineBasedLineCommenter) already has the comment and uncomment code implemented.  However, it defines a [StartDelimiter](xref:ActiproSoftware.Text.Implementation.LineBasedLineCommenter.StartDelimiter) property that must be set to the start comment delimiter for your language.

[LineBasedLineCommenter](xref:ActiproSoftware.Text.Implementation.LineBasedLineCommenter) has a [CanCommentEmptyLines](xref:ActiproSoftware.Text.Implementation.LineBasedLineCommenter.CanCommentEmptyLines) property that determines whether to comment a line that is only whitespace. The default is `true`.

For C#, this would be set to the string `//`.  For VB, this would be set to the string `'`.

## Creating a Range Line Commenter

Range line commenters are used in languages like XML where `<!-- -->` should be used to comment out lines of text.  To accomplish this, create a [RangeLineCommenter](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter) object.

[RangeLineCommenter](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter) already has the comment and uncomment code implemented.  However, it defines a [StartDelimiter](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter.StartDelimiter) and [EndDelimiter](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter.EndDelimiter) properties that must be set to the start and end comment delimiters for your language.

For XML, the start delimiter would be set to the string `<!--` and the end delimiter would be set to the string `-->`.

### Comment Token ID

If your range comment consists of a single token and you wish to enhance the comment locating logic when performing an uncomment, set the [RangeLineCommenter](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter).[CommentTokenId](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter.CommentTokenId) property to the comment token's ID.  As long as the selection is fully within a token with that ID, it will be found.

### Advanced Logic

If your range comment consists of multiple tokens or requires more advanced logic to properly locate it, you can override the virtual [RangeLineCommenter](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter).[FindCommentTextRange](xref:ActiproSoftware.Text.Implementation.RangeLineCommenter.FindCommentTextRange*) method.  It passes in the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) to examine.  You can create a [snapshot reader](../core-text/scanning-text.md) to examine text and tokens over that range.  Then return a [TextRange](xref:ActiproSoftware.Text.TextRange) indicating the range of the comment.  If no comment was found, return `null` instead.
