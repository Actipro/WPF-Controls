---
title: "Tokens"
page-title: "Tokens - Lexing - SyntaxEditor Text/Parsing Framework"
order: 3
---
# Tokens

A token is a span of text within a snapshot that has some sort of lexical parse data associated with it.  Tokens are used throughout the product, in various types of parsing, classification, and text scanning.

## Token Basic Properties

All tokens are represented by classes implementing the [IToken](xref:ActiproSoftware.Text.Lexing.IToken) interface.  This interface defines properties that fall into two categories: identification and location.

Identification properties provide the most basic data for identifying what a token represents.  The [Id](xref:ActiproSoftware.Text.Lexing.IToken.Id) and [Key](xref:ActiproSoftware.Text.Lexing.IToken.Key) properties are used for this purpose.  The [Id](xref:ActiproSoftware.Text.Lexing.IToken.Id) property is an integer number and the [Key](xref:ActiproSoftware.Text.Lexing.IToken.Key) is a `String` value.  Either or both can be used to provide identification of the token, although the ID is preferred since integer comparisons are faster than `String` comparisons.

Location properties give information about the size and location of the token within its text source.  The [StartOffset](xref:ActiproSoftware.Text.Lexing.IToken.StartOffset) and [EndOffset](xref:ActiproSoftware.Text.Lexing.IToken.EndOffset) properties specify the start/end offsets of the token.  The end offset is the offset after the last character contained by the token.  The [Length](xref:ActiproSoftware.Text.Lexing.IToken.Length) property gives the number of characters in the token.  The [StartPosition](xref:ActiproSoftware.Text.Lexing.IToken.StartPosition) and [EndPosition](xref:ActiproSoftware.Text.Lexing.IToken.EndPosition) properties specify the start/end [TextPosition](xref:ActiproSoftware.Text.TextPosition) objects that relate to the offsets.  The [TextRange](xref:ActiproSoftware.Text.Lexing.IToken.TextRange) and [PositionRange](xref:ActiproSoftware.Text.Lexing.IToken.PositionRange) properties provide the related ranges.

> [!NOTE]
> See the [Offsets, Ranges, and Positions](../core-text/offsets-ranges-positions.md) topic for more information on offsets and text positions.

## Mergable Tokens

Mergable tokens are represented by classes implementing the [IMergableToken](xref:ActiproSoftware.Text.Lexing.IMergableToken) interface, which inherits the [IToken](xref:ActiproSoftware.Text.Lexing.IToken) interface.  Mergable tokens are the type of token that is created when using a [mergable lexer](basic-concepts.md), meaning a lexer from one language that can merge its results with another language's mergable lexer.  This is commonly seen in languages like HTML where multiple child languages like CSS, JavaScript, etc. can be used.

Mergable tokens provide more information about how the token was created, such as its owner [ILexicalState](xref:ActiproSoftware.Text.Lexing.ILexicalState), owner [ILexicalScope](xref:ActiproSoftware.Text.Lexing.ILexicalScope) (if appropriate), and the root [IMergableLexer](xref:ActiproSoftware.Text.Lexing.IMergableLexer)

Another key feature provided by the [IMergableToken](xref:ActiproSoftware.Text.Lexing.IMergableToken) interface is the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) associated with the token, available via the [IMergableToken](xref:ActiproSoftware.Text.Lexing.IMergableToken).[ClassificationType](xref:ActiproSoftware.Text.Lexing.IMergableToken.ClassificationType) property.  A classification type is a logical category such as `Identifier`, `Comment`, or `Whitespace`.

## Token Classes

If you wish to create a bare bones class that implements [IToken](xref:ActiproSoftware.Text.Lexing.IToken) for your language, and your language's lexer is not [mergable](basic-concepts.md), you can inherit the [TokenBase](xref:ActiproSoftware.Text.Lexing.Implementation.TokenBase) class.  It is abstract and only requires that you implement its [Id](xref:ActiproSoftware.Text.Lexing.Implementation.TokenBase.Id) property and optionally the [Key](xref:ActiproSoftware.Text.Lexing.Implementation.TokenBase.Key) property.

For languages that use mergable lexers, you can use the non-abstract [MergableToken](xref:ActiproSoftware.Text.Lexing.Implementation.MergableToken) class for your tokens.  Typically you don't even need to worry about this though since the [MergableLexerBase](xref:ActiproSoftware.Text.Lexing.Implementation.MergableLexerBase) class, which most [IMergableLexer](xref:ActiproSoftware.Text.Lexing.IMergableLexer) implementations inherit, automatically creates [MergableToken](xref:ActiproSoftware.Text.Lexing.Implementation.MergableToken) instances for you.
