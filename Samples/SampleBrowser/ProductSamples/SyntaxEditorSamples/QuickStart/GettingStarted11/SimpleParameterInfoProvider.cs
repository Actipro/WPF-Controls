using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03b;  // For SimpleLexicalStateId
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;  // For context-related types
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09;  // For FunctionContentProvider
using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted11;

/// <summary>
/// Provides IntelliPrompt parameter info data for the <c>Simple</c> language.
/// </summary>
public class SimpleParameterInfoProvider() : ParameterInfoProviderBase("Simple"), IEditorDocumentTextChangeEventSink, IEditorViewSelectionChangeEventSink {

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
		if ((typedText == ",") || (editor.IntelliPrompt.Sessions[IntelliPromptSessionTypes.ParameterInfo] is null)) {
			switch (typedText) {
				case "(":
				case ",":
					// If in the default state...
					var reader = editor.ActiveView.GetReader();
					var token = reader.Token;
					if (token?.LexicalStateId == SimpleLexicalStateId.Default) {
						// Request a session
						RequestSession(editor.ActiveView);
					}
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
			var context = new SimpleContextFactory().CreateContext(view.Selection.EndSnapshotOffset, includeArgumentInfo: true);
			if (context.ArgumentListSnapshotOffset.HasValue) {
				// If the current session is still valid...
				if (context.ArgumentListSnapshotOffset.Value.Offset == session.SnapshotRange?.StartOffset) {
					// If the current parameter was updated...
					if (UpdateCurrentParameter(session, context)) {
						// Refresh content
						session.Refresh();
					}
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

		// Get the context
		var context = new SimpleContextFactory().CreateContext(view.Selection.EndSnapshotOffset, includeArgumentInfo: true);
		if ((context.TargetFunction is null) || (!context.ArgumentListSnapshotOffset.HasValue))
			return false;

		// Create a session
		var session = new ParameterInfoSession();

		// NOTE: Normally you'd add any possible overloads here as well (the parameter info UI will handle the display of
		//   multiple overloads), but for this language we'll assume that overloads are not allowed

		// Add item for the target function
		session.Items.Add(new SignatureItem(new FunctionContentProvider(view.HighlightingStyleRegistry, context.TargetFunction, includeImage: true, view.DefaultBackgroundColor), context.TargetFunction));

		// Update the current parameter
		UpdateCurrentParameter(session, context);

		// Ensure the caret is visible
		view.Scroller.ScrollToCaret();

		// Open the session
		session.Open(view, new TextRange(context.ArgumentListSnapshotOffset.Value));
		return true;
	}

	/// <summary>
	/// Updates the current parameter in the session's signatures, based on information in the specified <see cref="SimpleContext"/>.
	/// </summary>
	/// <param name="session">The <see cref="IParameterInfoSession"/> to update.</param>
	/// <param name="context">The <see cref="SimpleContext"/> to examine.</param>
	/// <returns>
	/// <c>true</c> if an update was made; otherwise, <c>false</c>.
	/// </returns>
	protected virtual bool UpdateCurrentParameter(IParameterInfoSession session, SimpleContext context) {
		#if NET
		ArgumentNullException.ThrowIfNull(session);
		ArgumentNullException.ThrowIfNull(context);
		#else
		if (session is null)
			throw new ArgumentNullException(nameof(session));
		if (context is null)
			throw new ArgumentNullException(nameof(context));
		#endif

		var updated = false;
		// Loop through signatures in the session
		foreach (var signatureItem in session.Items) {
			// Check for parameterized content to a function with parameters
			if (signatureItem is {
				ContentProvider: IParameterizedContentProvider contentProvider,
				Tag: FunctionDeclaration { Parameters.Count: > 0 } functionDecl
			}) {
				if (context.ArgumentIndex.HasValue) {
					// Get the parameter index and set if valid
					int parameterIndex = context.ArgumentIndex.Value;

					// If the parameter index is valid...
					if ((0 <= parameterIndex) && (parameterIndex < functionDecl.Parameters.Count)) {
						if (contentProvider.ParameterIndex != parameterIndex) {
							updated = true;
							contentProvider.ParameterIndex = parameterIndex;
						}
						continue;
					}
				}

				// Clear the current parameter
				if (contentProvider.ParameterIndex.HasValue) {
					updated = true;
					contentProvider.ParameterIndex = null;
				}
			}
		}
		return updated;
	}

}
