---
title: "Programmatic Lexers"
page-title: "Programmatic Lexers - Lexing - SyntaxEditor Text/Parsing Framework"
order: 5
---
# Programmatic Lexers

The fastest lexers are handwritten classes (called "programmatic") that are hard-coded to parse a specific language's code.

## Mergable vs. Non-Mergable

Programmatic lexers can be mergable or non-mergable.  Mergable lexers allow a language to be merged with other languages at run-time via language transitions.  An example of a language that uses merging is HTML since it can transition to CSS, VBScript, etc.

Mergable lexers provide this functionality by implementing the [IMergableLexer](xref:ActiproSoftware.Text.Lexing.IMergableLexer) interface.  While this merging functionality is useful for certain scenarios, it does add a bit of overhead and thus results in some performance loss.  This performance loss may not be very significant in many situations, but still if you wish to make your lexer as fast as possible and it doesn't need to merge with other language, you should make your lexer non-mergable.

A non-mergable lexer has the best performance among the various lexer types, although it requires a little more work to get started.

## Implementing a Mergable Programmatic Lexer

A mergable programmatic lexer is made by creating a class that implements the [IMergableLexer](xref:ActiproSoftware.Text.Lexing.IMergableLexer) interface, although inheriting the [MergableLexerBase](xref:ActiproSoftware.Text.Lexing.Implementation.MergableLexerBase) class is the easiest way to accomplish this.  When inheriting [MergableLexerBase](xref:ActiproSoftware.Text.Lexing.Implementation.MergableLexerBase), the only method we are required to implement is the [GetNextToken](xref:ActiproSoftware.Text.Lexing.Implementation.MergableLexerBase.GetNextToken*) method.

That method is passed an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) and an [ILexicalState](xref:ActiproSoftware.Text.Lexing.ILexicalState) that indicates the current lexical state.  The reader is already initialized to the current offset that should be scanned to look for tokens.  See the [Scanning Text Using a Reader](../core-text/scanning-text.md) topic for some more details on the low-level [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) class.

The [GetNextToken](xref:ActiproSoftware.Text.Lexing.Implementation.MergableLexerBase.GetNextToken*) method asks us to return a [MergableLexerResult](xref:ActiproSoftware.Text.Lexing.MergableLexerResult) object.  So after we scan a range of text using the reader and identify a token, we need to return an appropriate [MergableLexerResult](xref:ActiproSoftware.Text.Lexing.MergableLexerResult) instance.

This code shows how to return a result for some text that was successfully identified as a token:

```csharp
return new MergableLexerResult(MatchType.ExactMatch, new LexicalStateTokenData(lexicalState, tokenId));
```

In that example we indicate there was an exact case-sensitive match and also indicates the [ILexicalState](xref:ActiproSoftware.Text.Lexing.ILexicalState) that match was made in (in case we moved to a new lexical state), along with the ID of the token.  The token ID is user defined and varies for each language.  For instance a CSS property name token may have ID value `10`.  A token is created for you behind-the-scenes based on the result value that spans from the reader's original offset through to the reader's current offset.

If there was no successful match, we want to move the reader back to the offset it started at and return [MergableLexerResult](xref:ActiproSoftware.Text.Lexing.MergableLexerResult).[NoMatch](xref:ActiproSoftware.Text.Lexing.MergableLexerResult.NoMatch) instead.

There is an example of a mergable programmatic lexer in the sample project for the Simple language.

## Implementing a Non-Mergable Programmatic Lexer

A non-mergable programmatic lexer is made by creating a class that implements the core [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) interface.  The main member of this interface is the [Parse](xref:ActiproSoftware.Text.Lexing.ILexer.Parse*) method.

This method passes a [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) indicating the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) and the range to parse, along with an [ILexerTarget](xref:ActiproSoftware.Text.Lexing.ILexerTarget).  A [TextRange](xref:ActiproSoftware.Text.TextRange) is returned that specifies the snapshot range that was modified.

### Preparing to Lex

The first step in a [Parse](xref:ActiproSoftware.Text.Lexing.ILexer.Parse*) implementation is to adjust the range to lex.  The [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) passed in tells us the minimum range to lex but often times we'll want to actually start our lexing at the start of the line that contains the range's start offset, or even somewhere on the line before that.  Thus you can use the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) instance from the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange).[Snapshot](xref:ActiproSoftware.Text.TextSnapshotRange.Snapshot) property to find the line start and move the lexing start offset back appropriately.

Once we have determined the lexing start offset, call the [ILexerTarget](xref:ActiproSoftware.Text.Lexing.ILexerTarget).[OnPreParse](xref:ActiproSoftware.Text.Lexing.ILexerTarget.OnPreParse*) method, passing in the lexing start offset by reference.  This tells the lexer target where we would like to begin lexing at.  It will return an [ILexerContext](xref:ActiproSoftware.Text.Lexing.ILexerContext) object and will further update the lexing start offset to indicate where the offset at which that context is valid, and thus from where we should actually start our lexing.

The [ILexerContext](xref:ActiproSoftware.Text.Lexing.ILexerContext) instance includes a stack of lexical states and scopes at the specified lexing start offset, thus allowing us to resume incremental parsing.  This helps performance since we don't need to start lexing from the beginning of the document.

### Lexing

The next step is to actually do some lexing.  For this we will need to do some [core text scanning](../core-text/scanning-text.md) by using an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader).

This code retrieves an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) that is initialized at the lexing start offset:

```csharp
ITextBufferReader reader = snapshotRange.Snapshot.GetReader(lexingStartOffset).BufferReader;
```

At this point we want to start a loop while the `reader.IsAtEnd` property returns `false`, meaning keep going until the end of the snapshot.  Inside the loop we scan characters using the reader and tokenize the text.

When some sort of pattern is recognized, we are ready to tell the lexer target.  We call the [ILexerTarget](xref:ActiproSoftware.Text.Lexing.ILexerTarget).[OnTokenParsed](xref:ActiproSoftware.Text.Lexing.ILexerTarget.OnTokenParsed*) method by passing it the next [IToken](xref:ActiproSoftware.Text.Lexing.IToken) that was lexed (which can be any class that inherits [TokenBase](xref:ActiproSoftware.Text.Lexing.Implementation.TokenBase)) and an [ILexicalScopeStateNode](xref:ActiproSoftware.Text.Lexing.ILexicalScopeStateNode) that can be persisted and returned to us in future [ILexerContext](xref:ActiproSoftware.Text.Lexing.ILexerContext) instances if we wish to resume incremental lexing at this point.

The [OnTokenParsed](xref:ActiproSoftware.Text.Lexing.ILexerTarget.OnTokenParsed*) returns a boolean value indicating if lexing should continue.  If that return value is `false` and we have passed the desired range to parse, we can safely quit our loop and complete lexing.  Otherwise we continue the loop.

### Completing Lexing

If we exited the loop because the end of the snapshot was reached, we should return a token indicating a document end.  The `TokenIdProviderBase.DocumentEnd` value is the token ID to use for this token type.

Next we need to let the parse target know that parsing is complete so we call [ILexerTarget](xref:ActiproSoftware.Text.Lexing.ILexerTarget).[OnPostParse](xref:ActiproSoftware.Text.Lexing.ILexerTarget.OnPostParse*) and tell it the offset of the reader we ended on.

Finally we return a [TextRange](xref:ActiproSoftware.Text.TextRange) indicating the actual offset range that was modified.
