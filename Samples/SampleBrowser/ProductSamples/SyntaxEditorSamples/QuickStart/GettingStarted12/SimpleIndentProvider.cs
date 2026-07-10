using ActiproSoftware.Extensions;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted12;

/// <summary>
/// Provides a <c>Simple</c> language indent provider service.
/// </summary>
public class SimpleIndentProvider : DelimiterIndentProvider {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SimpleIndentProvider() {
		// Initialize
		CloseCurlyBraceTokenId = SimpleTokenId.CloseCurlyBrace;
		OpenCurlyBraceTokenId = SimpleTokenId.OpenCurlyBrace;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override int GetIndentAmount(TextSnapshotOffset snapshotOffset, int defaultAmount) {
		// Get the ICodeDocument from the snapshot
		if (snapshotOffset.Snapshot.Document is not ICodeDocument document)
			return defaultAmount;

		// Get a reader
		var reader = snapshotOffset.Snapshot.GetReader(snapshotOffset.Offset);
		if (reader is null)
			return defaultAmount;

		// Get the indentation base line index
		var indentationBaseLineIndex = (snapshotOffset.Line.Index - 1).ClampToNonnegative();

		// Ensure we are at the start of the current token
		if (!reader.IsAtTokenStart)
			reader.GoToCurrentTokenStart();

		// If finding indentation for an open curly brace, move back a token
		var isForOpenCurlyBrace = (reader.Token?.Id == SimpleTokenId.OpenCurlyBrace);
		if (isForOpenCurlyBrace)
			reader.GoToPreviousToken();

		// Get the tab size
		var tabSize = document.TabSize;

		// Loop backwards
		var keywordFoundAfterStatement = false;
		var statementFound = false;
		while (true) {
			switch (reader.Token?.Id) {
				case SimpleTokenId.OpenCurlyBrace:
					// Indent from this open curly brace
					return reader.SnapshotLine.IndentAmount + tabSize;
				case SimpleTokenId.CloseCurlyBrace:
					// Return the indent level of the matching {
					reader.GoToPreviousMatchingTokenById(SimpleTokenId.CloseCurlyBrace, SimpleTokenId.OpenCurlyBrace);
					return reader.SnapshotLine.IndentAmount;
				case SimpleTokenId.CloseParenthesis:
				case SimpleTokenId.SemiColon:
					if (!statementFound) {
						// Flag that a statement was found
						statementFound = true;

						if (!keywordFoundAfterStatement) {
							// Use this line as indentation base
							indentationBaseLineIndex = reader.SnapshotLine.Index;
						}
					}
					break;
				default:
					if (
						!keywordFoundAfterStatement
						&& !statementFound
						&& reader.Offset < snapshotOffset.Offset
						&& reader.Token is not null
						&& ((reader.Token.Id >= SimpleTokenId.Function) && (reader.Token.Id <= SimpleTokenId.Var))
					) {
						// Flag that a keyword was found
						keywordFoundAfterStatement = true;

						// Use this line as indentation base
						indentationBaseLineIndex = reader.SnapshotLine.Index;
					}
					break;
			}

			// Go to the previous token
			if (!reader.GoToPreviousToken())
				break;
		}

		// Indent a level if on the statement after the keyword
		return reader.Snapshot.Lines[indentationBaseLineIndex].IndentAmount + (keywordFoundAfterStatement && isForOpenCurlyBrace ? tabSize : 0);
	}

	/// <inheritdoc/>
	public override IndentMode Mode
		=> IndentMode.Smart;

	/// <inheritdoc/>
	protected override void OnDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
		// If the user is typing a '}' character...
		if ((e.TextChange?.Operations.Count == 1) && (e.TypedText == "}")) {
			// Ensure the '}' is the first non-whitespace character on the line
			var startLine = e.ChangedSnapshotRange.StartLine;
			if (startLine.FirstNonWhitespaceCharacterOffset != e.ChangedSnapshotRange.StartOffset)
				return;

			// Get the indent amount of the previous line
			var previousLineIndex = (startLine.Index - 1).ClampToNonnegative();
			var previousLineIndentAmount = startLine.Snapshot.Lines[previousLineIndex].IndentAmount;

			// The new indent should be a tab stop out
			var indentAmount = Math.Max(0, GetIndentAmount(new TextSnapshotOffset(e.ChangedSnapshotRange.Snapshot, e.ChangedSnapshotRange.StartOffset), previousLineIndentAmount));
			startLine.IndentAmount = indentAmount;
		}

		base.OnDocumentTextChanged(editor, e);
	}

	/// <inheritdoc/>
	protected override void OnDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e) {
		// While not used in this sample, this override can be used to preview changes before they are applied
		base.OnDocumentTextChanging(editor, e);
	}

}
