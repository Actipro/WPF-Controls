---
title: "Parser Infrastructure"
page-title: "Parser Infrastructure - SyntaxEditor LL(*) Parser Framework"
order: 4
---
# Parser Infrastructure

There are several classes that will need to be created and connected in order to use the LL(*) Parser Framework.  This topic makes the process easy by guiding you through each step that is required.

By the end of this topic, you should have a system built that can compile without errors.  However, there are additional things you will need to do in the next topic and beyond before your parser will operate properly, such as adding grammar productions.

## Grammar Class

The [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar) class contains the meat of the whole setup.  To quickly sum up, a grammar is a set of parsing rules defined via EBNF for how to derive structure from text.  It is used to tell the [ILLParser](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParser) how to parse text.

The grammar class constructor will do four tasks:

1. Create terminals

1. Create non-terminals

1. Configure non-terminal productions

1. Configure non-terminal can-match callbacks

 We will explain these further in the [Symbols and EBNF Terms](symbols-and-terms.md) and [Walkthrough: Adding Symbols and Productions](walkthrough-symbols-and-terms.md) topics.

For now, all you need to do is create the class and the empty constructor.  Here is an example of the [Grammar](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.Grammar) derived class we've created for our `Simple` language:

```csharp
/// <summary>
/// Represents a <see cref="Grammar" /> for the <c>Simple</c> language.
/// </summary>
public class SimpleGrammar : Grammar {

	/// <summary>
	/// Initializes a new instance of the <c>SimpleGrammar</c> class.
	/// </summary>
	public SimpleGrammar() : base("Simple") {

		// More code will be added here in the next several topics

	}
}
```

## ITokenReader Interface

An [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader) provides tokens to the parsing framework.  A token reader is an intermediate class that is created for each parse request and sits between the [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) that will be used to tokenize text for the parser, and the parser itself.  This means that it can also be used to filter out certain tokens that the parser won't care about.

For instance, many parsers don't care about comments so those tokens can be skipped over by the token reader as it passes tokens up to the parser.  Many parsers find that whitespace is insignificant as well.  Thus, whitespace can also generally be skipped over.  There may be other token types that are desirable to be skipped by the token reader as it iterates and passes tokens to the parser.

If you are using a [mergable lexer](../text-parsing/lexing/basic-concepts.md) to supply tokens to the parser, it may be sufficient to use the built-in [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) class since it fully implements [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader).  The [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) class will by default provide every token from a mergable lexer to the parser.  If you have a mergable lexer but would like to filter out certain token types, such as whitespace in a whitespace insensitive language, you can subclass [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) as shown in sample below.

If you are using a non-mergable lexer then you should inherit [TokenReaderBase](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.TokenReaderBase) instead and implement its abstract [GetNextToken](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.TokenReaderBase.GetNextToken*) method, which simply returns the next token that will be consumed by the parser.

If you inherit from [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader), your token reader class will need two things.  First, you will need a public constructor which takes an [ITextBufferReader](xref:ActiproSoftware.Text.ITextBufferReader) and an [IMergableLexer](xref:ActiproSoftware.Text.Lexing.IMergableLexer) as arguments.  Pass these along to the base constructor.  Second, you will need to override the [GetNextToken](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader.GetNextToken*) member.

Here is an example token reader, based on [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader), which discards all whitespace tokens.  The concepts here show how to filter tokens regardless of whether you inherit [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) or [TokenReaderBase](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.TokenReaderBase).  The `SimpleParsingTokenId` class contains token ID constants and was generated by the [Language Designer](../language-designer-tool/index.md) tool using our `Simple` language project.  You should have an equivalent class that was generated for your language.  This also makes the assumption that you have a `Whitespace` token defined in the language that was used to generate the `SimpleParsingTokenId` class.

```csharp
/// <summary>
/// Represents an object that can provide tokens to a <see cref="ILLParser"/> in a forward-only direction for the <c>Simple</c> language.
/// </summary>
public class SimpleTokenReader : MergableTokenReader {

	/// <summary>
	/// Initializes a new instance of the <c>SimpleTokenReader</c> class.
	/// </summary>
	/// <param name="reader">The <see cref="ITextBufferReader"/> to use for consuming text.</param>
	/// <param name="rootLexer">The root <see cref="IMergableLexer"/>.</param>
	public SimpleTokenReader(ITextBufferReader reader, IMergableLexer rootLexer) : base(reader, rootLexer) {}

	/// <summary>
	/// Returns the next <see cref="IToken"/> that will be consumed by the token reader.
	/// </summary>
	/// <returns>The next <see cref="IToken"/> that will be consumed by the token reader.</returns>
	protected override IToken GetNextToken() {
		// Call the base method
		IToken token = base.GetNextToken();

		// Loop to skip over tokens that are insignificant to the parser
		while (!this.IsAtEnd) {
			switch (token.Id) {
				case SimpleParsingTokenId.Whitespace:
					// Skip
					token = base.GetNextToken();
					break;
				default:
					return token;
			}
		}

		return token;
	}
}
```

## ILLParser Interface

You must create a parser class to register as a service with your language.  This class will implement the [ILLParser](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParser) interface.  Generally, you will want to inherit from [LLParserBase](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase) class, which takes care of implementing most of the interface.

Your parser class must have a minimum of two things:

1. It should declare a public constructor which passes a new instance of your grammar class to the base constructor.

1. You must override the [CreateTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase.CreateTokenReader*) method, because it is declared abstract.

The [CreateTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase.CreateTokenReader*) override returns a new [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader) instance to provide tokens to the parsing framework.  This method is called each time parsing is started, so it is important to make a new instance of the token reader to be used with each call.

In this example code, we will create and return a [MergableTokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.MergableTokenReader) instance, but you could just as easily return a new instance of your own [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader) implementation.  The `SimpleLexer` and `SimpleClassificationTypeProvider` classes were generated by the [Language Designer](../language-designer-tool/index.md) tool using our `Simple` language project.  You should have equivalent classes that were generated for your language.

```csharp
/// <summary>
/// Represents a parser for the <c>Simple</c> language.
/// </summary>
public class SimpleParser : LLParserBase {

    /// <summary>
    /// Initializes a new instance of the <c>SimpleParser</c> class.
    /// </summary>
    public SimpleParser() : base(new SimpleGrammar()) {}

    /// <summary>
    /// Creates an <see cref="ITokenReader"/> that is used by the parser to read through tokens.
    /// </summary>
    /// <param name="reader">The <see cref="ITextBufferReader"/> that provides access to the text buffer.</param>
    /// <returns>An <see cref="ITokenReader"/> that is used by the parser to read through tokens.</returns>
    public override ITokenReader CreateTokenReader(ITextBufferReader reader) {
        return new MergableTokenReader(reader, new SimpleLexer(new SimpleClassificationTypeProvider()));
    }
}
```

## ILLParseData Interface

By default, a parser will generate an [LLParseData](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParseData) object.  This object implements the [ILLParseData](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParseData) interface, which returns data containing the [IAstNode](xref:ActiproSoftware.Text.Parsing.IAstNode) that was parsed, [IParseError](xref:ActiproSoftware.Text.Parsing.IParseError) objects that were reported, and the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) that was used, if known. [ILLParseData](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParseData) implements [IParseErrorProvider](xref:ActiproSoftware.Text.Parsing.IParseErrorProvider), meaning that it can easily be wired up to show squiggle lines where parse errors occur.

It is possible that you will want to retain additional information from the parsing process than what is provisioned with this type.  For instance, an XML language may wish to store a `boolean` flag indicating whether the document is well-formed.  If this is the case, you can create a new parse data class that inherits from [LLParseData](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParseData).  You can then add any additional properties you wish to be available.

If you do create a new parse data type, you will need to override the [CreateParseData](xref:ActiproSoftware.Text.Parsing.LLParser.Implementation.LLParserBase.CreateParseData*) member.  This override should look something like the following code.  The `SimpleParseData` type represents the custom parse data type that you would create for your language.

```csharp
/// <summary>
/// Creates an <see cref="IParseData"/> for the specified <see cref="IParserState"/>.
/// </summary>
/// <param name="request">The <see cref="IParseRequest"/> that contains data about the requested parsing operation.</param>
/// <param name="state">The <see cref="IParserState"/> to examine.</param>
/// <returns>The <see cref="IParseData"/> that was created.</returns>
protected virtual IParseData CreateParseData(IParseRequest request, IParserState state) {
	SimpleParseData parseData = new SimpleParseData();
	this.InitializeParseData(parseData, state);

    // Add custom parse data to the parseData object

	return parseData;
}
```

## ISyntaxLanguage Interface

Finally, you will need to register your parser class as a service with an [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) for the parsing system to operate.  You may also want to associate the [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage) with a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) instance.

If you used the [Language Designer](../language-designer-tool/index.md) tool to generate source files for your language, you should have a source file which contains a syntax language class that inherits from [SyntaxLanguage](xref:ActiproSoftware.Text.Implementation.SyntaxLanguage).  You will have to register additional services than what is included in the generated class constructor.  While you could simply add the additional registrations to the generated constructor, odds are you will need to regenerate those files at a later time.  Rather than risk losing your changes to this class constructor if you overwrite it with the [Language Designer's](../language-designer-tool/index.md) source code generator, we recommend creating a new class which inherits from the generated class.  You can perform your additional registrations in the constructor of your derived class, and if you have to regenerate the base class, no changes are lost.

In addition to registering your parser class as a service, you might also want to register a [ParseErrorTagger](xref:ActiproSoftware.Text.Tagging.Implementation.ParseErrorTagger).  This will cause parsing errors to be automatically marked with red squiggly underlines once everything is set up correctly.

Your new syntax language class should look something like the following code.  The `SimpleSyntaxLanguage` class was generated by the [Language Designer](../language-designer-tool/index.md) tool using our `Simple` language project.  You should have an equivalent class that was generated for your language.  The `SimpleParser` type is equivalent to the custom parser class that you would create for your language.

```csharp
/// <summary>
/// Represents a <c>Simple</c> syntax language definition.
/// </summary>
public class ExtendedSimpleSyntaxLanguage : SimpleSyntaxLanguage {

	/// <summary>
	/// Initializes a new instance of the <see cref="ExtendedSimpleSyntaxLanguage"/> class.
	/// </summary>
	public ExtendedSimpleSyntaxLanguage() {
		// Register a parser with the language
		this.RegisterService<IParser>(new SimpleParser());

		// Register a ParseErrorTagger so errors get squiggles
		this.RegisterService(new CodeDocumentTaggerProvider<ParseErrorTagger>(typeof(ParseErrorTagger)));
	}
}
```

As mentioned in the [Lexer Preparation](lexer-preparation.md) topic, there are some rare cases where you might want a separate lexer defined for your syntax highlighting and for your parsing.  This scenario could occur if the tokenization required to support your parser is different than the tokenization of text needed to support syntax highlighting.

In this scenario, set up your syntax language like normal.  However, when creating your [ITokenReader](xref:ActiproSoftware.Text.Parsing.LLParser.ITokenReader) in your [ILLParser](xref:ActiproSoftware.Text.Parsing.LLParser.ILLParser) class, make sure it is using a new instance of your alternate lexer that should be generating tokens for the parser.  The alternate [ILexer](xref:ActiproSoftware.Text.Lexing.ILexer) you use for parsing can be created using a second language project in the [Language Designer](../language-designer-tool/index.md) or you can create it using other techniques described in the [lexing](../text-parsing/lexing/index.md) portion of the Text/Parsing Framework documentation.
