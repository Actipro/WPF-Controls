using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d {

	/// <summary>
	/// Represents an object that can provide tokens to a <see cref="ILLParser"/> in a forward-only direction for the <c>Simple</c> language.
	/// </summary>
	public class SimpleTokenReader : MergableTokenReader {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleTokenReader</c> class.
		/// </summary>
		/// <param name="reader">The <see cref="ITextBufferReader"/> to use for consuming text.</param>
		/// <param name="rootLexer">The root <see cref="IMergableLexer"/>.</param>
		public SimpleTokenReader(ITextBufferReader reader, IMergableLexer rootLexer) : base(reader, rootLexer) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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
					case SimpleTokenId.MultiLineCommentEndDelimiter:
					case SimpleTokenId.MultiLineCommentLineTerminator:
					case SimpleTokenId.MultiLineCommentStartDelimiter:
					case SimpleTokenId.MultiLineCommentText:
					case SimpleTokenId.SingleLineCommentEndDelimiter:
					case SimpleTokenId.SingleLineCommentStartDelimiter:
					case SimpleTokenId.SingleLineCommentText:
					case SimpleTokenId.Whitespace:
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

}
