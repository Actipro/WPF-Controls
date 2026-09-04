using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionTags;

/// <summary>
/// A provider that can handle requests for display of an IntelliPrompt completion list.
/// </summary>
public class CustomCompletionProvider : CompletionProviderBase, IEditorDocumentTextChangeEventSink {

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e)
		=> OnDocumentTextChanged(editor, e);

	void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e)
		=> OnDocumentTextChanging(editor, e);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs after a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
	/// </summary>
	/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> is changed.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
		if ((editor is null) || (e.TextChange is null) || (e.TextChange.Source != editor.ActiveView))
			return;

		// The e.TypedText is not null only when a Typing change occurs that inserts text,
		//   so we can check that to display the completion list when "<" is typed
		switch (e.TypedText) {
			case "<":
				if (!editor.IntelliPrompt.Sessions.Contains(IntelliPromptSessionTypes.Completion))
					RequestSession(editor.ActiveView, canCommitWithoutPopup: false, includeStartDelimiter: false);
				break;
		}
	}

	/// <summary>
	/// Occurs before a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
	/// </summary>
	/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> that is changing.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e) { /* no-op */ }

	/// <inheritdoc/>
	public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup)
		=> RequestSession(view, canCommitWithoutPopup, includeStartDelimiter: true);

	#pragma warning disable CA1822 // Mark members as static
	/// <summary>
	/// Requests that an <see cref="ICompletionSession"/> be opened for the specified <see cref="IEditorView"/>.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> that will host the session.</param>
	/// <param name="canCommitWithoutPopup">Whether the session can immediately commit if a single match is made when the session is opened, commonly known as "complete word" functionality.</param>
	/// <param name="includeStartDelimiter">Whether to include the start delimiter.</param>
	/// <returns>
	/// <c>true</c> if a session was opened; otherwise, <c>false</c>.
	/// </returns>
	public bool RequestSession(IEditorView view, bool canCommitWithoutPopup, bool includeStartDelimiter) {
		//
		// IMPORTANT NOTE:
		//   The items for this completion list are hardcoded in this sample and are simply meant
		//   to illustrate the rich features of the SyntaxEditor completion list.  When implementing
		//   a real language, you should vary the items based on the current context of the caret.
		//

		// Create a session
		var session = new CompletionSession {
			CanCommitWithoutPopup = canCommitWithoutPopup,
			MatchOptions = CompletionMatchOptions.TargetsDisplayText
		};

		// HTML allows ! and - characters to be typed too... make sure they are inserted
		session.AllowedCharacters.Add('!');
		session.AllowedCharacters.Add('-');

		// Add some items
		var highlightingStyleRegistry = view.HighlightingStyleRegistry;
		var commentWebColor = HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor();
		session.Items.Add(new CompletionItem("!--", new CommonImageSourceProvider(CommonImageKind.XmlComment),
			new HtmlContentProvider($"<b>&lt;!-- --&gt;</b> Comment<br/><span style=\"color: {commentWebColor};\">A comment.</span>"),
			autoCompletePreText: string.Format("{0}!-- ", (includeStartDelimiter ? "<" : string.Empty)),
			autoCompletePostText: " -->"
		));
		session.Items.Add(new CompletionItem("a", new CommonImageSourceProvider(CommonImageKind.XmlTag),
			new HtmlContentProvider($"<b>a</b> Element<br/><span style=\"color: {commentWebColor};\">A hyperlink to another URL.</span>"),
			autoCompletePreText: string.Format("{0}a href=\"", (includeStartDelimiter ? "<" : string.Empty)),
			autoCompletePostText: "\""
		));
		session.Items.Add(new CompletionItem("br", new CommonImageSourceProvider(CommonImageKind.XmlTag),
			new HtmlContentProvider($"<b>br</b> Element<br/><span style=\"color: {commentWebColor};\">Creates a line break.</span>"),
			autoCompletePreText: string.Format("{0}br/>", (includeStartDelimiter ? "<" : string.Empty)),
			autoCompletePostText: null
		));

		if (session.Items.Count > 0) {
			// Ensure the caret is visible
			view.Scroller.ScrollToCaret();

			// Ensure the items are sorted and open the session
			session.SortItems();
			session.Open(view);
			return true;
		}

		return false;
	}
	#pragma warning restore CA1822

}
