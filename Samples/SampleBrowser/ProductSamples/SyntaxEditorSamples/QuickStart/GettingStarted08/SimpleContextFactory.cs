using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleTokenId
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;

/// <summary>
/// Creates <see cref="SimpleContext"/> objects for a <see cref="TextSnapshotOffset"/>.
/// </summary>
public class SimpleContextFactory {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns whether the specified token ID is allowed in an invocation.
	/// </summary>
	/// <param name="tokenId">The token ID.</param>
	/// <returns>
	/// <c>true</c> if the specified token ID is allowed in an invocation; otherwise, <c>>false</c>.
	/// </returns>
	private static bool IsTokenAllowedInInvocation(int tokenId) {
		switch (tokenId) {
			case SimpleTokenId.Assignment:
			case SimpleTokenId.CloseCurlyBrace:
			case SimpleTokenId.Function:
			case SimpleTokenId.OpenCurlyBrace:
			case SimpleTokenId.Return:
			case SimpleTokenId.SemiColon:
			case SimpleTokenId.Var:
				return false;
			default:
				return true;
		}
	}

	/// <summary>
	/// Examines the AST data to determine the context type and insert the containing function declaration.
	/// </summary>
	/// <param name="context">The <see cref="SimpleContext"/> to update.</param>
	private static void UpdateFromAst(SimpleContext context) {
		// Get the snapshot offset
		var snapshotOffset = context.SnapshotOffset;

		// Get the parse data and compilation unit
		if (snapshotOffset.Snapshot.Document is ICodeDocument {
			ParseData: ILLParseData {
				Ast: CompilationUnit { HasMembers: true } compilationUnit
			} parseData
		}) {
			// Translate the snapshot offset to the AST's snapshot
			if (parseData.Snapshot is not null)
				snapshotOffset = snapshotOffset.TranslateTo(parseData.Snapshot, TextOffsetTrackingMode.Negative);

			// Loop through AST nodes
			foreach (var functionAstNode in compilationUnit.Members) {

				// If the child node is a function declaration with valid offsets...
				if (functionAstNode is { StartOffset: { } functionStartOffset, EndOffset: { } functionEndOffset }) {

					// If the function's text range contains the offset...
					var functionTextRange = new TextRange(functionStartOffset, functionEndOffset);
					if (functionTextRange.Contains(snapshotOffset.Offset)) {

						// Initially assume we are in a header
						context.Type = SimpleContextType.FunctionDeclarationHeader;
						context.ContainingFunctionDeclaration = functionAstNode;

						// If the function has a body with a range...
						if (functionAstNode.Body is { StartOffset: { } bodyStartOffset, EndOffset: { } bodyEndOffset }) {

							// If the block's text range contains the offset...
							var blockTextRange = new TextRange(bodyStartOffset + 1, bodyEndOffset - 1);
							if (blockTextRange.Contains(snapshotOffset.Offset)) {
								// Mark that we are in a block instead
								context.Type = SimpleContextType.FunctionDeclarationBlock;
							}
						}
						break;
					}
				}
			}
		}
	}

	/// <summary>
	/// Examines the snapshot text to determine more detail about the context.
	/// </summary>
	/// <param name="context">The <see cref="SimpleContext"/> to update.</param>
	/// <param name="includeArgumentInfo">Whether to populate the argument-related context properties, for use with parameter info.</param>
	private static void UpdateFromSnapshotText(SimpleContext context, bool includeArgumentInfo) {
		// Get the snapshot offset
		var snapshotOffset = context.SnapshotOffset;

		// Create a default initialization range
		context.InitializationSnapshotRange = new TextSnapshotRange(snapshotOffset);

		// Get a snapshot reader
		var reader = snapshotOffset.Snapshot.GetReader(snapshotOffset.Offset);
		if (reader is null)
			return;

		var token = reader.ReadToken();
		if (token is not null) {
			// If the current token is a comment, flag no context
			switch (token.Id) {
				case SimpleTokenId.MultiLineCommentEndDelimiter:
				case SimpleTokenId.MultiLineCommentLineTerminator:
				case SimpleTokenId.MultiLineCommentStartDelimiter:
				case SimpleTokenId.MultiLineCommentText:
				case SimpleTokenId.SingleLineCommentEndDelimiter:
				case SimpleTokenId.SingleLineCommentStartDelimiter:
				case SimpleTokenId.SingleLineCommentText:
					context.Type = SimpleContextType.None;
					return;
			}
		}

		// If we are in a function declaration block...
		if (context.Type == SimpleContextType.FunctionDeclarationBlock) {

			// Get the AST
			if (snapshotOffset.Snapshot.Document is not ICodeDocument {
				ParseData: ILLParseData {
					Ast: CompilationUnit { HasMembers: true } compilationUnit
				}
			}) {
				// Quit
				return;
			}

			// If argument data should be scanned...
			if (includeArgumentInfo) {
				// Scan backward to look for argument data
				reader.Offset = snapshotOffset.Offset;
				reader.GoToCurrentTokenStart();
				var startOffset = reader.Offset;
				token = reader.ReadTokenReverse();
				while (token is not null) {
					// Quit if we look behind too far (for performance reasons) - 500 is arbitrary number and could be tweaked
					if ((!context.ArgumentIndex.HasValue) && (startOffset - reader.Offset > 500))
						return;

					switch (token.Id) {
						case SimpleTokenId.CloseParenthesis:
							// Skip over ( ... ) pair
							if (!reader.GoToPreviousMatchingTokenById(SimpleTokenId.CloseParenthesis, SimpleTokenId.OpenParenthesis))
								return;
							break;
						case SimpleTokenId.Comma:
							// Update the context data
							if (context.ArgumentIndex.HasValue)
								context.ArgumentIndex++;
							else {
								context.ArgumentIndex = 1;
								context.ArgumentSnapshotOffset = new TextSnapshotOffset(reader.Snapshot, token.EndOffset);
							}
							break;
						case SimpleTokenId.OpenParenthesis:
							// Update the context data
							context.ArgumentListSnapshotOffset = new TextSnapshotOffset(reader.Snapshot, token.EndOffset);
							if (!context.ArgumentIndex.HasValue) {
								context.ArgumentIndex = 0;
								context.ArgumentSnapshotOffset = context.ArgumentListSnapshotOffset;
							}

							// Go to the previous token
							reader.GoToPreviousToken();
							token = reader.Token;
							if (token?.Id == SimpleTokenId.Identifier) {
								// Loop through the AST nodes to ensure that the identifier text matches a declared function name
								var functionName = reader.TokenText;
								foreach (var functionAstNode in compilationUnit.Members) {
									// If the name matches the function's name...
									if (functionAstNode.Name == functionName) {
										// Update the target function
										context.TargetFunction = functionAstNode;
										break;
									}
								}
							}
							return;
						default:
							// Quit if any tokens are found that aren't allowed in invocations
							if (!IsTokenAllowedInInvocation(token.Id))
								return;
							else
								break;
					}

					// Move back a token
					token = reader.ReadTokenReverse();
				}
			}
			else {
				// If the current token is an identifier...
				if (token?.Id == SimpleTokenId.Identifier) {
					// Store the identifier snapshot range in case this is a function reference token
					var identifierSnapshotRange = new TextSnapshotRange(reader.Snapshot, token.TextRange);

					// Next, check to ensure the next non-whitespace token is a '('
					token = reader.ReadToken();
					while (!reader.IsAtSnapshotEnd) {
						if (token is not null) {
							switch (token.Id) {
								case SimpleTokenId.Whitespace:
									// Continue
									break;
								case SimpleTokenId.OpenParenthesis: {
									// Loop through the AST nodes to ensure that the identifier text matches a declared function name
									foreach (var functionAstNode in compilationUnit.Members) {
										// If the name matches the function's name...
										if (functionAstNode.Name == identifierSnapshotRange.Text) {
											// Update the context type
											context.Type = SimpleContextType.FunctionReference;
											context.InitializationSnapshotRange = identifierSnapshotRange;
											context.TargetFunction = functionAstNode;
											break;
										}
									}
									return;
								}
								default:
									// Quit
									return;
							}
						}

						// Advance
						token = reader.ReadToken();
					}
				}
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	#pragma warning disable CA1822 // Mark members as static

	/// <summary>
	/// Creates a <see cref="SimpleContext"/> for the specified <see cref="TextSnapshotOffset"/>.
	/// </summary>
	/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> for which to create a context.</param>
	/// <param name="includeArgumentInfo">Whether to populate the argument-related context properties, for use with parameter info.</param>
	public SimpleContext CreateContext(TextSnapshotOffset snapshotOffset, bool includeArgumentInfo) {
		// Create a context
		var context = new SimpleContext(snapshotOffset);

		// Update the context from the AST
		UpdateFromAst(context);

		// Update the context from the snapshot text
		UpdateFromSnapshotText(context, includeArgumentInfo);

		return context;
	}

	#pragma warning restore CA1822

}
