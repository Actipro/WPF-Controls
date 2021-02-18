using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId
using Step3b = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c {

	/// <summary>
	/// Represents a token tagger for the <c>Simple</c> language.
	/// </summary>
	/// <remarks>
	/// Languages with non-mergable <see cref="ILexer"/> implementations require a customized token
	/// tagger class that can return classification types for the tokens managed by the tagger.
	/// The classifications made are then used by SyntaxEditor to drive syntax highlighting.
	/// </remarks>
	public class SimpleTokenTagger : Step3b.SimpleTokenTagger {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SimpleTokenTagger</c> class.
		/// </summary>
		/// <param name="document">The specific <see cref="ICodeDocument"/> for which this token tagger will be used.</param>
		/// <param name="classificationTypeProvider">A <see cref="ISimpleClassificationTypeProvider"/> that provides classification types used by this token tagger.</param>
		public SimpleTokenTagger(ICodeDocument document, ISimpleClassificationTypeProvider classificationTypeProvider) : 
			base(document, classificationTypeProvider) {}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns an <see cref="IClassificationType"/> for the specified <see cref="IToken"/>, if one is appropriate.
		/// </summary>
		/// <param name="token">The <see cref="IToken"/> to examine.</param>
		/// <returns>An <see cref="IClassificationType"/> for the specified <see cref="IToken"/>, if one is appropriate.</returns>
		/// <remarks>
		/// The default implementation of this method automatically returns the classification type if the token
		/// is an <see cref="IMergableToken"/>.
		/// </remarks>
		public override IClassificationType ClassifyToken(IToken token) {
			switch (token.Id) {
				case SimpleTokenId.Identifier:
					return this.ClassificationTypeProvider.Identifier;
				case SimpleTokenId.Function:
				case SimpleTokenId.Return:
				case SimpleTokenId.Var:
					return this.ClassificationTypeProvider.Keyword;
				case SimpleTokenId.Number:
					return this.ClassificationTypeProvider.Number;
				case SimpleTokenId.MultiLineCommentEndDelimiter:
				case SimpleTokenId.MultiLineCommentLineTerminator:
				case SimpleTokenId.MultiLineCommentStartDelimiter:
				case SimpleTokenId.MultiLineCommentText:
				case SimpleTokenId.SingleLineCommentEndDelimiter:
				case SimpleTokenId.SingleLineCommentStartDelimiter:
				case SimpleTokenId.SingleLineCommentText:
					return this.ClassificationTypeProvider.Comment;
				default:
					return null;
			}
		}
	}
}
