using ActiproSoftware.Extensions;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Utility;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted13;

/// <summary>
/// Provides a <c>Simple</c> language text formatter service.
/// </summary>
public class SimpleTextFormatter : ITextFormatter {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Formats the specified snapshot range.
	/// </summary>
	/// <param name="change">The <see cref="ITextChange"/> to use.</param>
	/// <param name="snapshotRange">The snapshot range.</param>
	private void FormatCore(ITextChange change, TextSnapshotRange snapshotRange) {
		// Get the snapshot and code document
		if (snapshotRange.Snapshot is not { Document: ICodeDocument document } snapshot)
			return;

		// Get the snapshot reader
		var reader = snapshot.GetReader(snapshotRange.StartOffset);

		// Optimize reader options
		reader.Options.PrimaryScanDirection = TextScanDirection.Forward;
		reader.Options.InitialTokenLoadBufferLength = Math.Min(100000, snapshotRange.Length);
		reader.Options.DefaultTokenLoadBufferLength = reader.Options.InitialTokenLoadBufferLength;

		// Get the tab size
		var tabSize = document.TabSize;

		// Keep track of the last non whitespace token Id
		var lastNonWhitespaceTokenId = -1;

		// Keep track of the indent level
		var indentLevel = 0;

		// Get the line terminator text
		var lineTerminatorText = snapshot.InferredLineTerminator.GetText();

		// Loop through the document
		while ((reader.Token is not null) && (reader.Offset < snapshotRange.EndOffset)) {
			// If the token is whitespace, delete the text
			if (reader.Token.Id == SimpleTokenId.Whitespace)
				change.DeleteText(reader.Token.TextRange);
			// The token is not whitespace
			else {
				// Create a variable that will contain the text to be inserted
				var insertText = string.Empty;

				// Determine the insertText value based on the previous non-whitespace token and the current token
				switch (lastNonWhitespaceTokenId) {
					case SimpleTokenId.CloseCurlyBrace:
						// If the token is a close curly brace, decrement the indent level
						if (reader.Token.Id == SimpleTokenId.CloseCurlyBrace) {
							indentLevel = (indentLevel - 1).ClampToNonnegative();
							insertText = lineTerminatorText + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
						}
						else {
							// If the indent level is zero, a function declaration just finished, which means we want an extra line terminator
							insertText = (indentLevel == 0)
								? lineTerminatorText + lineTerminatorText
								: lineTerminatorText + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
						}
						break;
					case SimpleTokenId.CloseParenthesis:
						// If the current token is an OpenCurlyBrace, determine whether the brace goes on a new line or not
						if (reader.Token.Id == SimpleTokenId.OpenCurlyBrace) {
							insertText = OpeningBraceOnNewLine
								? lineTerminatorText + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize)
								: " ";
						}
						break;
					case SimpleTokenId.Identifier:
					case SimpleTokenId.Number:
						// Sometimes a space should be added after an identifier or number, sometimes not
						if (reader.Token.Id != SimpleTokenId.SemiColon
							&& reader.Token.Id != SimpleTokenId.CloseParenthesis
							&& reader.Token.Id != SimpleTokenId.OpenParenthesis
							&& reader.Token.Id != SimpleTokenId.Comma
						) {
							insertText = " ";
						}
						break;
					case SimpleTokenId.Comma:
					case SimpleTokenId.Function:
					case SimpleTokenId.Return:
					case SimpleTokenId.Var:
					case SimpleTokenId.Multiplication:
					case SimpleTokenId.Equality:
					case SimpleTokenId.Inequality:
					case SimpleTokenId.Assignment:
					case SimpleTokenId.Subtraction:
					case SimpleTokenId.Addition:
					case SimpleTokenId.Division:
						// Keywords and operators get a space
						insertText = " ";
						break;
					case SimpleTokenId.MultiLineCommentText:
						// Multiline comments get an extra line terminator
						insertText = lineTerminatorText + lineTerminatorText + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
						break;
					case SimpleTokenId.OpenCurlyBrace:
						// If the token is not a close curly brace, increment the indent level
						if (reader.Token.Id != SimpleTokenId.CloseCurlyBrace)
							indentLevel++;
						insertText = lineTerminatorText + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
						break;
					case SimpleTokenId.SemiColon:
					case SimpleTokenId.SingleLineCommentText:
						// If the token is a close curly brace, decrement the indent level
						if (reader.Token.Id == SimpleTokenId.CloseCurlyBrace)
							indentLevel = (indentLevel - 1).ClampToNonnegative();
						insertText = lineTerminatorText + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
						break;
				}
				// Insert the replacement text
				change.InsertText(reader.Token.StartOffset, insertText);

				// Update the last non-whitespace token Id
				lastNonWhitespaceTokenId = reader.Token.Id;
			}
			// Go to the next token
			reader.GoToNextToken();
		}

		// If the entire document was formatted, add a line terminator to the end
		if (
			snapshot.SnapshotRange.StartOffset == snapshotRange.StartOffset
			&& snapshot.SnapshotRange.EndOffset == snapshotRange.EndOffset
		) {
			change.InsertText(snapshotRange.EndOffset, lineTerminatorText);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="ITextFormatter.Format"/>
	public void Format(ITextSnapshot snapshot, ITextPositionRangeCollection selectionPositionRanges, TextFormatMode mode = TextFormatMode.Ranges) {
		#if NET
		ArgumentNullException.ThrowIfNull(snapshot);
		#else
		if (snapshot is null)
			throw new ArgumentNullException(nameof(snapshot));
		#endif
		if (selectionPositionRanges is not { Count: > 0 })
			throw new ArgumentNullException(nameof(selectionPositionRanges));

		// Changes must occur sequentially so that we can use unmodified offsets while looping over the document
		var options = new TextChangeOptions {
			OffsetDelta = TextChangeOffsetDelta.SequentialOnly,
			RetainSelection = true
		};
		var change = snapshot.Document.CreateTextChange(TextChangeTypes.AutoFormat, options);

		// Get the snapshot ranges to format
		var snapshotRanges = (mode == TextFormatMode.All)
			? [snapshot.SnapshotRange]
			: selectionPositionRanges.Select(pr => new TextSnapshotRange(snapshot, snapshot.PositionRangeToTextRange(pr))).ToArray();

		// Loop through the snapshot ranges
		foreach (var snapshotRange in snapshotRanges)
			FormatCore(change, snapshotRange);

		// Apply the changes
		if (change.Operations.Count > 0)
			change.Apply();
	}

	/// <summary>
	/// Indicates whether opening braces are on new lines.
	/// </summary>
	public bool OpeningBraceOnNewLine { get; set; }

}
