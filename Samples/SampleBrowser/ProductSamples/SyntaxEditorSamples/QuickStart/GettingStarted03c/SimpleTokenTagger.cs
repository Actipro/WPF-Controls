using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using Step3b = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c;

/// <summary>
/// Represents a token tagger for the <c>Simple</c> language.
/// </summary>
/// <remarks>
/// Languages with non-mergeable <see cref="ILexer"/> implementations require a customized token
/// tagger class that can return classification types for the tokens managed by the tagger.
/// The classifications made are then used by SyntaxEditor to drive syntax highlighting.
/// </remarks>
/// <param name="document">The specific <see cref="ICodeDocument"/> for which this token tagger will be used.</param>
/// <param name="classificationTypeProvider">A <see cref="ISimpleClassificationTypeProvider"/> that provides classification types used by this token tagger.</param>
public class SimpleTokenTagger(ICodeDocument document, ISimpleClassificationTypeProvider classificationTypeProvider) : Step3b.SimpleTokenTagger(document, classificationTypeProvider) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IClassificationType? ClassifyToken(IToken token) {
		switch (token.Id) {
			case SimpleTokenId.Identifier:
				return ClassificationTypeProvider.Identifier;
			case SimpleTokenId.Function:
			case SimpleTokenId.Return:
			case SimpleTokenId.Var:
				return ClassificationTypeProvider.Keyword;
			case SimpleTokenId.Number:
				return ClassificationTypeProvider.Number;
			case SimpleTokenId.MultiLineCommentEndDelimiter:
			case SimpleTokenId.MultiLineCommentLineTerminator:
			case SimpleTokenId.MultiLineCommentStartDelimiter:
			case SimpleTokenId.MultiLineCommentText:
			case SimpleTokenId.SingleLineCommentEndDelimiter:
			case SimpleTokenId.SingleLineCommentStartDelimiter:
			case SimpleTokenId.SingleLineCommentText:
				return ClassificationTypeProvider.Comment;
			default:
				return null;
		}
	}

}
