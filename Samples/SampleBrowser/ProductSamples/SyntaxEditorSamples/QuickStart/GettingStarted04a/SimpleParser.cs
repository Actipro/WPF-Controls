using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using Step3c = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c;  // For SimpleLexer

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04a {

	/// <summary>
	/// Represents a parser for the <c>Simple</c> language.
	/// </summary>
	public class SimpleParser : LLParserBase {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleParser</c> class.
		/// </summary>
		public SimpleParser() : this(new SimpleGrammar()) {}

		/// <summary>
		/// Initializes a new instance of the <c>SimpleParser</c> class.
		/// </summary>
		/// <param name="grammar">The <see cref="Grammar"/> to use.</param>
		public SimpleParser(SimpleGrammar grammar) : base(grammar) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates an <see cref="ITokenReader"/> that is used by the parser to read through tokens.
		/// </summary>
		/// <param name="reader">The <see cref="ITextBufferReader"/> that provides access to the text buffer.</param>
		/// <returns>An <see cref="ITokenReader"/> that is used by the parser to read through tokens.</returns>
		public override ITokenReader CreateTokenReader(ITextBufferReader reader) {
			return new SimpleTokenReader(reader, new Step3c.SimpleLexer(true));
		}
		
	}

}
