using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04a;

/// <summary>
/// Represents an object that can provide tokens to a <see cref="ILLParser"/> in a forward-only direction for the <c>Simple</c> language.
/// </summary>
/// <param name="reader">The <see cref="ITextBufferReader"/> to use for consuming text.</param>
/// <param name="rootLexer">The root <see cref="IMergeableLexer"/>.</param>
public class SimpleTokenReader(ITextBufferReader reader, IMergeableLexer rootLexer) : MergeableTokenReader(reader, rootLexer) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IToken? GetNextToken() {
		// Call the base method
		var token = base.GetNextToken();

		// Loop to skip over tokens that are insignificant to the parser
		while (!IsAtEnd) {
			switch (token?.Id) {
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
