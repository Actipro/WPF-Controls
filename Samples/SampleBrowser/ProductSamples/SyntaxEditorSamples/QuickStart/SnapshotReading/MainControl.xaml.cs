using ActiproSoftware.Text;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.SnapshotReading;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : UserControl {

	private readonly ITextSnapshotReader _reader;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Load a language from a language definition
		editor.Document.Language = Common.SyntaxEditorHelper.LoadLanguageDefinitionFromResourceStream("CSharp.langdef");

		// Create a reader
		_reader = editor.Document.CurrentSnapshot.GetReader(0);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends a message to the results editor and updates the UI.
	/// </summary>
	/// <param name="message">The message to append.</param>
	/// <param name="isTokenSearch">Whether the search was for a token.</param>
	private void AppendMessageAndUpdateUI(string message, bool isTokenSearch) {
		var token = _reader.Token;

		// Get token message portion
		var tokenMessage = "<null>";
		if (token is not null)
			tokenMessage = string.Format("{0} (TextRange={1})", token.Key, token.TextRange);

		// Append message
		resultsEditor.Document.AppendText(TextChangeTypes.Typing,
			string.Format("{0}: Offset={1}, Position={2}, Token={3}{4}", message, _reader.Offset, _reader.Position, tokenMessage, Environment.NewLine));

		// Focus the editor
		editor.Focus();

		// Select the text that was read (select in reverse so the caret is at the actual "current" offset)
		if (isTokenSearch && (token is not null))
			editor.ActiveView.Selection.SelectRange(new TextRange(token.EndOffset, token.StartOffset));
		else if (!_reader.IsAtSnapshotEnd)
			editor.ActiveView.Selection.SelectRange(new TextRange(_reader.Offset + 1, _reader.Offset));
		else
			editor.ActiveView.Selection.StartOffset = _reader.Offset;
	}

	private void OnGoToCurrentLineEndHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToCurrentSnapshotLineEnd();
		AppendMessageAndUpdateUI("Current line end", isTokenSearch: false);
	}

	private void OnGoToCurrentLineStartHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToCurrentSnapshotLineStart();
		AppendMessageAndUpdateUI("Current line start", isTokenSearch: false);
	}

	private void OnGoToCurrentWordEndHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToCurrentWordEnd();
		AppendMessageAndUpdateUI("Current word end", isTokenSearch: false);
	}

	private void OnGoToCurrentWordStartHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToCurrentWordStart();
		AppendMessageAndUpdateUI("Current word start", isTokenSearch: false);
	}

	private void OnGoToNextCharacterHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.ReadCharacter();
		AppendMessageAndUpdateUI("Next character", isTokenSearch: false);
	}

	private void OnGoToNextDocCommentHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToNextTokenWithKey("XmlCommentStartTag");
		AppendMessageAndUpdateUI("Next documentation comment", isTokenSearch: true);
	}

	private void OnGoToNextLineStartHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToNextSnapshotLineStart();
		AppendMessageAndUpdateUI("Next line start", isTokenSearch: false);
	}

	private void OnGoToNextThirdTokenHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToNextToken(3);
		AppendMessageAndUpdateUI("Next third token", isTokenSearch: true);
	}

	private void OnGoToNextTokenHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToNextToken();
		AppendMessageAndUpdateUI("Next token", isTokenSearch: true);
	}

	private void OnGoToNextWordStartHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToNextWordStart();
		AppendMessageAndUpdateUI("Next word start", isTokenSearch: false);
	}

	private void OnGoToPreviousCharacterHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.ReadCharacterReverse();
		AppendMessageAndUpdateUI("Previous character", isTokenSearch: false);
	}

	private void OnGoToPreviousDocCommentHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToPreviousTokenWithKey("XmlCommentStartTag");
		AppendMessageAndUpdateUI("Previous documentation comment", isTokenSearch: true);
	}

	private void OnGoToPreviousLineEndHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToPreviousSnapshotLineEnd();
		AppendMessageAndUpdateUI("Previous line end", isTokenSearch: false);
	}

	private void OnGoToPreviousThirdTokenHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToPreviousToken(3);
		AppendMessageAndUpdateUI("Previous third token", isTokenSearch: true);
	}

	private void OnGoToPreviousTokenHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToPreviousToken();
		AppendMessageAndUpdateUI("Previous token", isTokenSearch: true);
	}

	private void OnGoToPreviousWordStartHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToPreviousWordStart();
		AppendMessageAndUpdateUI("Previous word start", isTokenSearch: false);
	}

	private void OnGoToSnapshotEndHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToSnapshotEnd();
		AppendMessageAndUpdateUI("Snapshot end", isTokenSearch: false);
	}

	private void OnGoToSnapshotStartHyperlinkClick(object sender, RoutedEventArgs e) {
		_reader.Offset = editor.ActiveView.Selection.EndOffset;
		_reader.GoToSnapshotStart();
		AppendMessageAndUpdateUI("Snapshot start", isTokenSearch: false);
	}

}
