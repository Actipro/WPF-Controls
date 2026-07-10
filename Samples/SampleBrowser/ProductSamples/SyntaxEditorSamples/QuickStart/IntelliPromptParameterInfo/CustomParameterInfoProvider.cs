using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptParameterInfo;

/// <summary>
/// Implements a parameter info provider that demonstrates automated parameter info session management.
/// </summary>
public class CustomParameterInfoProvider() : ParameterInfoProviderBase("Custom"), IEditorDocumentTextChangeEventSink, IEditorViewSelectionChangeEventSink {

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Contains context information.
	/// </summary>
	private class Context {

		public int ArgumentIndex;
		public int ArgumentListOffset;
		public string? InvocationName;

	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e)
		=> OnDocumentTextChanged(editor, e);

	void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e)
		=> OnDocumentTextChanging(editor, e);

	void IEditorViewSelectionChangeEventSink.NotifySelectionChanged(IEditorView view, EditorViewSelectionEventArgs e)
		=> OnViewSelectionChanged(view, e);

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns parameter info context information for the current offset in the view.
	/// </summary>
	/// <param name="view">The view to examine.</param>
	private static Context? CreateContext(IEditorView view) {
		// Get a reader
		var reader = view.GetReader();
		if (!reader.IsAtTokenStart)
			reader.GoToCurrentTokenStart();

		// Loop backwards and examine tokens
		int? argumentIndex = null;
		var parenthesisLevel = 0;
		var token = reader.ReadTokenReverse();
		var tokenCount = 0;
		while (token is not null) {
			// Quit if reading too many tokens
			if (tokenCount > 100)
				return null;

			// NOTE: Use IDs for better performance if your lexer assigns token IDs
			switch (token.Key) {
				case "CloseParenthesis":
					parenthesisLevel++;
					break;
				case "Comma":
					if (argumentIndex.HasValue)
						argumentIndex++;
					else
						argumentIndex = 1;
					break;
				case "OpenParenthesis":
					if (--parenthesisLevel < 0) {
						if (!argumentIndex.HasValue)
							argumentIndex = 0;

						// Create a context object
						var context = new Context {
							ArgumentIndex = argumentIndex.Value,
							ArgumentListOffset = token.EndOffset
						};

						// Ensure a 'Foo' identifier is before the '(' (hardcoded like this for this sample)
						token = reader.ReadTokenReverse();
						if ((token?.Key == "Identifier") && (reader.TokenText == "Foo")) {
							context.InvocationName = reader.TokenText;
							return context;
						}
						else
							return null;
					}
					break;
			}

			token = reader.ReadTokenReverse();
			tokenCount++;
		}

		return null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs after a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
	/// </summary>
	/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> is changed.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
		if ((editor is null) || (e.Handled) || (e.TextChange is null) || (e.TextChange.Source != editor.ActiveView))
			return;

		// If no parameter info sessions are open yet...
		var typedText = e.TypedText;
		if (editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.ParameterInfo] is null) {
			switch (typedText) {
				case "(":
				case ",":
					// Request a session
					RequestSession(editor.ActiveView);
					break;
			}
		}
	}

	/// <summary>
	/// Occurs before a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
	/// </summary>
	/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> that is changing.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e) { /* no-op */ }

	/// <summary>
	/// Occurs when the selection is changed in the specified <see cref="IEditorView"/>.
	/// </summary>
	/// <param name="view">The <see cref="IEditorView"/> that received the event.</param>
	/// <param name="e">The event data.</param>
	protected virtual void OnViewSelectionChanged(IEditorView view, EditorViewSelectionEventArgs e) {
		#if NET
		ArgumentNullException.ThrowIfNull(view);
		#else
		if (view is null)
			throw new ArgumentNullException(nameof(view));
		#endif

		// Look for an existing session
		var session = view.SyntaxEditor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.ParameterInfo] as IParameterInfoSession;
		if (session?.IsOpen == true) {
			// Quit if for a different view
			if (view != session.View)
				return;

			// Get the context
			var requestNewSession = false;
			if (CreateContext(view) is { } context) {
				// If the current session is still valid...
				if (context.ArgumentListOffset == session.SnapshotRange?.StartOffset) {
					// Leave it open
					return;
				}
				else {
					// Flag to request a new session
					requestNewSession = true;
				}
			}

			// Close the session
			session.Close(cancelled: true);

			// If a new session should be requested...
			if (requestNewSession)
				RequestSession(view);
		}
	}

	/// <inheritdoc/>
	public override bool RequestSession(IEditorView view) {
		if (view is null)
			return false;

		// Get a context object, which will be filled in if we are in a valid offset for parameter info
		if (CreateContext(view) is not { } context)
			return false;

		// Create a session
		var session = new ParameterInfoSession();

		// Add items (hardcoded in this sample)... a real implementation like the one in the Getting Started series would
		//   dynamically build this sort of data, and track the current parameter for bolding as well
		var highlightingStyleRegistry = view.HighlightingStyleRegistry;
		var commentWebColor = HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor();
		var keywordWebColor = HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor();
		session.Items.Add(new SignatureItem(new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">function</span> Foo()<br/><span style=\"color: {commentWebColor};\">Returns 0.</span>", view.DefaultBackgroundColor)));
		session.Items.Add(new SignatureItem(new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">function</span> Foo(x)<br/><span style=\"color: {commentWebColor};\">Returns the value passed in.</span>", view.DefaultBackgroundColor)));
		session.Items.Add(new SignatureItem(new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">function</span> Foo(x, y)<br/><span style=\"color: {commentWebColor};\">Returns the multiplicative result of the two arguments.</span>", view.DefaultBackgroundColor)));

		// Try to pick the best overload to show by default... normally this would be much more robust code where
		//   your context would include the number of arguments that have been specified and you'd base the selection on that...
		//   For this sample, we're just going to use the argument index
		switch (context.ArgumentIndex) {
			case 1:
				session.Selection = session.Items[2];
				break;
			case 0:
				session.Selection = session.Items[1];
				break;
		}

		// Open the session
		session.Open(view, new TextRange(context.ArgumentListOffset));

		return true;
	}

}
