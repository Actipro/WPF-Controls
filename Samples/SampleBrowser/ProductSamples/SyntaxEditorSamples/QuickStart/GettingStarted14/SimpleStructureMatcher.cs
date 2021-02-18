using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Analysis;
using ActiproSoftware.Text.Analysis.Implementation;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted14 {
    
    /// <summary>
	/// Provides a <c>Simple</c> language structure matcher.
    /// </summary>
    public class SimpleStructureMatcher : StructureMatcher {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleStructureMatcher</c> class.
		/// </summary>
		public SimpleStructureMatcher() {
			// Initialize options for base class' built-in functionality
			this.CanMatchSquareBraces = false;
			this.CloseCurlyBraceTokenId = SimpleTokenId.CloseCurlyBrace;
			this.CloseParenthesisTokenId = SimpleTokenId.CloseParenthesis;
			this.OpenCurlyBraceTokenId = SimpleTokenId.OpenCurlyBrace;
			this.OpenParenthesisTokenId = SimpleTokenId.OpenParenthesis;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Attempts to locate structural text, such as bracket pairs, at the specified offset.
		/// </summary>
		/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> to examine.</param>
		/// <param name="options">The <see cref="IStructureMatchOptions"/> to use.</param>
		/// <returns>An <see cref="IStructureMatchResultSet"/> that contains the results that were found, if any.</returns>
		/// <remarks>
		/// When the result set is empty, no structural delimiter was next to the specified offset.
		/// When the result set contains a single result, a structural delimiter such as a bracket was found next to the specified offset, 
		/// but no matching structural delimiter was found.
		/// The result set may contain multiple entries in cases such as when a language wishes to flag <c>#if...#else...#endif</c> blocks as a structure.
		/// </remarks>
		public override IStructureMatchResultSet Match(TextSnapshotOffset snapshotOffset, IStructureMatchOptions options) {
			if (snapshotOffset.IsDeleted)
				throw new ArgumentNullException("snapshotOffset");

			// Get a snapshot reader and configure it for quick initial lookup
			ITextSnapshotReader reader = snapshotOffset.Snapshot.GetReader(snapshotOffset.Offset);
			reader.Options.DefaultTokenLoadBufferLength = 250;
			reader.Options.InitialTokenLoadBufferLength = 4;
			
			IToken token = reader.Token;
			if (token != null) {
				// If the token is not a multi-line comment but is at the start of a token, check the previous token
				if ((token.Id != SimpleTokenId.MultiLineCommentText) && (reader.IsAtTokenStart))
					token = reader.ReadTokenReverse();

				// If the token is a multi-line comment...
				if (token.Id == SimpleTokenId.MultiLineCommentText) {
					// The Simple language programmatic lexer variant only has a single token for the entire comment so
					//   ensure the target offset is at a delimiter (and not within the body of the comment)...
					//   For most other languages, you'd want to scan tokens to find a matching delimiter token instead
					bool isAtStart = (snapshotOffset.Offset <= token.StartOffset + 2);
					bool isAtEnd = (snapshotOffset.Offset >= token.EndOffset - 2);
					if (isAtStart || isAtEnd) {
						// Get the token's text and ensure it ends with a proper delimiter
						string tokenText = reader.TokenText;
						if ((token.Length >= 4) && (tokenText.EndsWith("*/", StringComparison.Ordinal))) {
							// Found a valid match
							StructureMatchResultCollection results = new StructureMatchResultCollection();
							results.Add(new StructureMatchResult(new TextSnapshotRange(reader.Snapshot, token.StartOffset, token.StartOffset + 2)) {
								IsSource = isAtStart,
								NavigationSnapshotOffset = new TextSnapshotOffset(reader.Snapshot, token.StartOffset)
							});
							results.Add(new StructureMatchResult(new TextSnapshotRange(reader.Snapshot, token.EndOffset - 2, token.EndOffset)) {
								IsSource = !isAtStart,
								NavigationSnapshotOffset = new TextSnapshotOffset(reader.Snapshot, token.EndOffset)
							});
							return new StructureMatchResultSet(results);
						}
					}
				}
			}

			// Call the base method
			return base.Match(snapshotOffset, options);
		}
		
    }
	
}
