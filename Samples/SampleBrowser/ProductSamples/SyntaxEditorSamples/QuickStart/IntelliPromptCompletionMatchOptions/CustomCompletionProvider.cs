using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionMatchOptions;

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
	/// Indicates whether list items show possible matched characters in a highlight color.
	/// </summary>
	public bool CanHighlightMatchedText { get; set; }

	/// <summary>
	/// Indicates case sensitivity.
	/// </summary>
	public bool IsCaseSensitive { get; set; }

	/// <summary>
	/// Occurs after a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
	/// </summary>
	/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> is changed.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
		if ((editor is null) || (e.TextChange is null) || (e.TextChange.Source != editor.ActiveView))
			return;

		// The e.TypedText is not null only when a Typing change occurs that inserts text,
		//   so we can check that to display the completion list when "." is typed
		switch (e.TypedText) {
			case ".":
				// Use a snapshot reader to iterate backwards through the active view's current text
				var reader = editor.ActiveView.GetReader();
				reader.ReadCharacterReverseThrough('.');
				var token = reader.ReadTokenReverse();

				// NOTE: In production code, a token ID comparison would be better than this string comparison
				if ((token is not null) && (reader.TokenText == "this")) {
					// A dot was typed after a "this" keyword so show the completion list
					RequestSession(editor.ActiveView, canCommitWithoutPopup: false);
				}
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
	public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup) {
		//
		// IMPORTANT NOTE:
		//   The items for this completion list are hardcoded in this sample and are simply meant
		//   to illustrate the rich features of the SyntaxEditor completion list.  When implementing
		//   a real language, you should vary the items based on the current context of the caret.
		//

		// Create a session
		var session = new CompletionSession {
			CanCommitWithoutPopup = canCommitWithoutPopup,

			// Set options
			CanHighlightMatchedText = CanHighlightMatchedText,
			MatchOptions = CompletionMatchOptions.None
		};
		if (!IsCaseSensitive)
			session.MatchOptions |= CompletionMatchOptions.IsCaseInsensitive;
		if (RequiresExact)
			session.MatchOptions |= CompletionMatchOptions.RequiresExact;
		if (UseAcronyms)
			session.MatchOptions |= CompletionMatchOptions.UseAcronyms;
		if (UseShorthand)
			session.MatchOptions |= CompletionMatchOptions.UseShorthand;

		// Add some items
		var highlightingStyleRegistry = view.HighlightingStyleRegistry;
		var commentWebColor = HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor();
		var keywordWebColor = HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor();
		var typeNameWebColor = HtmlContentProvider.GetTypeNameForegroundColor(highlightingStyleRegistry).ToWebColor();
		session.Items.Add(new CompletionItem("aField", new CommonImageSourceProvider(CommonImageKind.FieldPrivate),
			new HtmlContentProvider($"<img src=\"resource:FieldPrivate\" align=\"absbottom\" /> <span style=\"color: {keywordWebColor};\">int</span> <b>Foo.aField</b>")));
		session.Items.Add(new CompletionItem("AMethod", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: {keywordWebColor};\">void</span> <b>Foo.AMethod</b>()")));
		session.Items.Add(new CompletionItem("AnIntValue", new CommonImageSourceProvider(CommonImageKind.PropertyPublic),
			new HtmlContentProvider($"<img src=\"resource:PropertyPublic\" align=\"absbottom\" /> <span style=\"color: {keywordWebColor};\">int</span> <b>Foo.AnIntValue</b>")));
		session.Items.Add(new CompletionItem("AStringValue", new CommonImageSourceProvider(CommonImageKind.PropertyPublic),
			new HtmlContentProvider($"<img src=\"resource:PropertyPublic\" align=\"absbottom\" /> <span style=\"color: {keywordWebColor};\">string</span> <b>Foo.AStringValue</b>")));
		session.Items.Add(new CompletionItem("Equals", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<img src=\"resource:MethodPublic\" align=\"absbottom\" /> "
				+ $"<span style=\"color: {keywordWebColor};\">bool</span> <b>object.Equals</b>(<span style=\"color: {keywordWebColor};\">object</span> obj)<br/>"
				+ $"<span style=\"color: {commentWebColor};\">Determines whether the specified <b>System.Object</b> is equal to the current <b>System.Object</b>.</span>")));
		session.Items.Add(new CompletionItem("GetHashCode", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: {keywordWebColor};\">int</span> <b>object.GetHashCode</b>()<br/>"
				+ $"<span style=\"color: {commentWebColor};\">Gets a hash code for this <b>System.Object</b>.</span>")));
		session.Items.Add(new CompletionItem("GetType", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: {typeNameWebColor};\">Type</span> <b>object.GetType</b>()<br/>"
				+ $"<span style=\"color: {commentWebColor};\">Gets the <b>System.Type</b> of the current instance.</span>")));
		session.Items.Add(new CompletionItem("ToString", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: {keywordWebColor};\">string</span> <b>object.ToString</b>()<br/>"
				+ $"<span style=\"color: {commentWebColor};\">Returns the string representation of the object.</span>")));

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

	/// <summary>
	/// Indicates whether exact matches are required.
	/// </summary>
	public bool RequiresExact { get; set; }

	/// <summary>
	/// Indicates whether to use acronyms.
	/// </summary>
	public bool UseAcronyms { get; set; }

	/// <summary>
	/// Indicates whether to use shorthand.
	/// </summary>
	public bool UseShorthand { get; set; }

}
