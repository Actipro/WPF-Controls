using System;
using System.Linq;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Utility;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted13 {

	/// <summary>
	/// Provides a <c>Simple</c> language text formatter service.
	/// </summary>
	public class SimpleTextFormatter : ITextFormatter {

		private bool openingBraceOnNewLine = false;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Formats the specified snapshot range.
		/// </summary>
		/// <param name="change">The <see cref="ITextChange"/> to use.</param>
		/// <param name="snapshotRange">The snapshot range.</param>
		private void FormatCore(ITextChange change, TextSnapshotRange snapshotRange) {
			// Get the snapshot
			ITextSnapshot snapshot = snapshotRange.Snapshot;
			if (snapshot == null)
				return;

			// Get the snapshot reader
			ITextSnapshotReader reader = snapshot.GetReader(snapshotRange.StartOffset);

			// Optimize reader options
			reader.Options.PrimaryScanDirection = TextScanDirection.Forward;
			reader.Options.InitialTokenLoadBufferLength = Math.Min(100000, snapshotRange.Length);
			reader.Options.DefaultTokenLoadBufferLength = reader.Options.InitialTokenLoadBufferLength;

			// Get the code document
			ICodeDocument document = snapshot.Document as ICodeDocument;
			if (document == null)
				return;

			// Get the tab size
			int tabSize = document.TabSize;

			// Keep track of the last non whitespace token Id
			int lastNonWhitespaceTokenId = -1;

			// Keep track of the indent level
			int indentLevel = 0;

			// Loop through the document
			while ((reader.Token != null) && (reader.Offset < snapshotRange.EndOffset)) {
				// If the token is whitespace, delete the text
				if (reader.Token.Id == SimpleTokenId.Whitespace)
					change.DeleteText(reader.Token.TextRange);
				// The token is not whitespace
				else {
					// Create a variable that will contain the text to be inserted
					string insertText = "";

					// Determine the insertText value based on the previous non-whitespace token and the current token
					switch (lastNonWhitespaceTokenId) {
						case SimpleTokenId.CloseCurlyBrace:
							// If the token is a close curly brace, decrement the indent level
							if (reader.Token.Id == SimpleTokenId.CloseCurlyBrace) {
								indentLevel = Math.Max(0, indentLevel - 1);
								insertText = "\n" + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
							}
							else {
								// If the indent level is zero, a function declaration just finished, which means we want an extra newline
								if (indentLevel == 0)
									insertText = "\n\n";
								else
									insertText = "\n" + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
							}
							break;
						case SimpleTokenId.CloseParenthesis:
							// If the current token is an OpenCurlyBrace, determine whether the brace goes on a new line or not
							if (reader.Token.Id == SimpleTokenId.OpenCurlyBrace) {
								if (openingBraceOnNewLine)
									insertText = "\n" + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
								else
									insertText = " ";
							}
							break;
						case SimpleTokenId.Identifier:
						case SimpleTokenId.Number:
							// Sometimes a space should be added after an identifier or number, sometimes not
							if (reader.Token.Id != SimpleTokenId.SemiColon
								&& reader.Token.Id != SimpleTokenId.CloseParenthesis
								&& reader.Token.Id != SimpleTokenId.OpenParenthesis
								&& reader.Token.Id != SimpleTokenId.Comma)
								insertText = " ";
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
							// Multiline comments get an extra newline
							insertText = "\n\n" + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
							break;
						case SimpleTokenId.OpenCurlyBrace:
							// If the token is not a close curly brace, increment the indent level
							if (reader.Token.Id != SimpleTokenId.CloseCurlyBrace)
								indentLevel++;
							insertText = "\n" + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
							break;
						case SimpleTokenId.SemiColon:
						case SimpleTokenId.SingleLineCommentText:
							// If the token is a close curly brace, decrement the indent level
							if (reader.Token.Id == SimpleTokenId.CloseCurlyBrace)
								indentLevel = Math.Max(0, indentLevel - 1);
							insertText = "\n" + StringHelper.GetIndentText(document.AutoConvertTabsToSpaces, tabSize, indentLevel * tabSize);
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

			// If the entire document was formatted, add a newline to the end
			if ((snapshot.SnapshotRange.StartOffset == snapshotRange.StartOffset)
					&& (snapshot.SnapshotRange.EndOffset == snapshotRange.EndOffset))
				change.InsertText(snapshotRange.EndOffset, "\n");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Applies text formatting to part or all of a snapshot.
		/// </summary>
		/// <param name="snapshot">The target <see cref="ITextSnapshot"/>.</param>
		/// <param name="selectionPositionRanges">
		/// The <see cref="ITextPositionRangeCollection"/> indicating the selection position ranges.
		/// These are used to translate the current selection to the new snapshot that is created, even when formatting the entire snapshot.
		/// </param>
		/// <param name="mode">A <see cref="TextFormatMode"/> indicating whether to format part or all of the snapshot.  The default value is <c>Ranges</c>.</param>
		/// <remarks>
		/// The containing <see cref="ITextDocument"/> is accessible via the <see cref="ITextSnapshot"/>.
		/// </remarks>
		public void Format(ITextSnapshot snapshot, ITextPositionRangeCollection selectionPositionRanges, TextFormatMode mode = TextFormatMode.Ranges) {
			if (snapshot == null)
				throw new ArgumentNullException("snapshot");
			if ((selectionPositionRanges == null) || (selectionPositionRanges.Count == 0))
				throw new ArgumentNullException("selectionPositionRanges");
			
			// Changes must occur sequentially so that we can use unmodified offsets while looping over the document
			var options = new TextChangeOptions();
			options.OffsetDelta = TextChangeOffsetDelta.SequentialOnly;
			options.RetainSelection = true;
			var change = snapshot.Document.CreateTextChange(TextChangeTypes.AutoFormat, options);

			// Get the snapshot ranges to format
			var snapshotRanges = (mode == TextFormatMode.All ?
				new TextSnapshotRange[] { snapshot.SnapshotRange } :
				selectionPositionRanges.Select(pr => new TextSnapshotRange(snapshot, snapshot.PositionRangeToTextRange(pr))).ToArray()
				);

			// Loop through the snapshot ranges
			foreach (var snapshotRange in snapshotRanges)
				this.FormatCore(change, snapshotRange);

			// Apply the changes
			if (change.Operations.Count > 0)
				change.Apply();
		}

		/// <summary>
		/// Gets or sets a value indicating whether opening braces are on new lines.
		/// </summary>
		/// <value><c>true</c> if the opening braces are on new lines; otherwise, <c>false</c>.</value>
		public bool OpeningBraceOnNewLine {
			get {
				return openingBraceOnNewLine;
			}
			set {
				openingBraceOnNewLine = value;
			}
		}

	}
}
