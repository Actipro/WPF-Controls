using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId
using ActiproSoftware.Text;
using ActiproSoftware.Text.Analysis;
using ActiproSoftware.Text.Analysis.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted14;

/// <summary>
/// Provides a <c>Simple</c> language structure matcher.
/// </summary>
public class SimpleStructureMatcher : StructureMatcher {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleStructureMatcher() {
		// Initialize options for base class' built-in functionality
		CanMatchSquareBraces = false;
		CloseCurlyBraceTokenId = SimpleTokenId.CloseCurlyBrace;
		CloseParenthesisTokenId = SimpleTokenId.CloseParenthesis;
		OpenCurlyBraceTokenId = SimpleTokenId.OpenCurlyBrace;
		OpenParenthesisTokenId = SimpleTokenId.OpenParenthesis;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IStructureMatchResultSet? Match(TextSnapshotOffset snapshotOffset, IStructureMatchOptions? options) {
		// Get a snapshot reader and configure it for quick initial lookup
		var reader = snapshotOffset.Snapshot.GetReader(snapshotOffset.Offset);
		reader.Options.DefaultTokenLoadBufferLength = 250;
		reader.Options.InitialTokenLoadBufferLength = 4;

		var token = reader.Token;
		if (token is not null) {
			// If the token is not a multi-line comment but is at the start of a token, check the previous token
			if ((token.Id != SimpleTokenId.MultiLineCommentText) && (reader.IsAtTokenStart))
				token = reader.ReadTokenReverse();

			// If the token is a multi-line comment...
			if (token?.Id == SimpleTokenId.MultiLineCommentText) {
				// The Simple language programmatic lexer variant only has a single token for the entire comment so
				//   ensure the target offset is at a delimiter (and not within the body of the comment)...
				//   For most other languages, you'd want to scan tokens to find a matching delimiter token instead
				var isAtStart = (snapshotOffset.Offset <= token.StartOffset + 2);
				var  isAtEnd = (snapshotOffset.Offset >= token.EndOffset - 2);
				if (isAtStart || isAtEnd) {
					// Get the token's text and ensure it ends with a proper delimiter
					var tokenText = reader.TokenText;
					if ((token.Length >= 4) && (tokenText?.EndsWith("*/", StringComparison.Ordinal) == true)) {
						// Found a valid match
						var results = new StructureMatchResultCollection {
							new StructureMatchResult(new TextSnapshotRange(reader.Snapshot, token.StartOffset, token.StartOffset + 2)) {
								IsSource = isAtStart,
								NavigationSnapshotOffset = new TextSnapshotOffset(reader.Snapshot, token.StartOffset)
							},
							new StructureMatchResult(new TextSnapshotRange(reader.Snapshot, token.EndOffset - 2, token.EndOffset)) {
								IsSource = !isAtStart,
								NavigationSnapshotOffset = new TextSnapshotOffset(reader.Snapshot, token.EndOffset)
							}
						};
						return new StructureMatchResultSet(results);
					}
				}
			}
		}

		return base.Match(snapshotOffset, options);
	}

}
