using System;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionTags {
	
	/// <summary>
	/// A provider that can handle requests for display of an IntelliPrompt completion list.
	/// </summary>
	public class CustomCompletionProvider : CompletionProviderBase, IEditorDocumentTextChangeEventSink {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Notifies after a text change occurs to an <see cref="IEditorDocument"/>.
		/// </summary>
		/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> is changed.</param>
		/// <param name="e">The <c>EditorSnapshotChangedEventArgs</c> that contains the event data.</param>
		void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
			this.OnDocumentTextChanged(editor, e);
		}

		/// <summary>
		/// Notifies before a text change occurs to an <see cref="IEditorDocument"/>.
		/// </summary>
		/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> that is changing.</param>
		/// <param name="e">The <c>EditorSnapshotChangingEventArgs</c> that contains the event data.</param>
		void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e) {
			this.OnDocumentTextChanging(editor, e);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs after a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
		/// </summary>
		/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> is changed.</param>
		/// <param name="e">The <c>EditorSnapshotChangedEventArgs</c> that contains the event data.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected virtual void OnDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
			if ((editor == null) || (e == null) || (e.TextChange == null) || (e.TextChange.Source != editor.ActiveView))
				return;

			// The e.TypedText is not null only when a Typing change occurs with a single operation that inserts text,
			//   so we can check that to display the completion list when "<" is typed
			switch (e.TypedText) {
				case "<":
					if (!editor.IntelliPrompt.Sessions.Contains(IntelliPromptSessionTypes.Completion))
						this.RequestSession(editor.ActiveView, false, false);
					break;
			}
		}

		/// <summary>
		/// Occurs before a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
		/// </summary>
		/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> that is changing.</param>
		/// <param name="e">The <c>EditorSnapshotChangingEventArgs</c> that contains the event data.</param>
		protected virtual void OnDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e) {}
		
		/// <summary>
		/// Requests that an <see cref="ICompletionSession"/> be opened for the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that will host the session.</param>
		/// <param name="canCommitWithoutPopup">Whether the session can immediately commit if a single match is made when the session is opened, commonly known as "complete word" functionality.</param>
		/// <returns>
		/// <c>true</c> if a session was opened; otherwise, <c>false</c>.
		/// </returns>
		public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup) {
			return this.RequestSession(view, canCommitWithoutPopup, true);
		}

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
			// The items for this completion list are hard coded in this sample and
			// are simply meant to illustrate the rich features of the SyntaxEditor completion list.
			// When implementing a real language, you should vary the items based
			// on the current context of the caret.
			//

			// Create a session
			CompletionSession session = new CompletionSession();
			session.CanCommitWithoutPopup = canCommitWithoutPopup;
			session.MatchOptions = CompletionMatchOptions.TargetsDisplayText;

			// HTML allows ! and - characters to be typed too... make sure they are inserted
			session.AllowedCharacters.Add('!');
			session.AllowedCharacters.Add('-');

			// Add some items
			var highlightingStyleRegistry = view.HighlightingStyleRegistry;
			session.Items.Add(new CompletionItem("!--", new CommonImageSourceProvider(CommonImageKind.XmlComment), 
				new HtmlContentProvider("<b>&lt;!-- --&gt;</b> Comment<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">A comment.</span>"),
				String.Format("{0}!-- ", (includeStartDelimiter ? "<" : String.Empty)), " -->"));
			session.Items.Add(new CompletionItem("a", new CommonImageSourceProvider(CommonImageKind.XmlTag), 
				new HtmlContentProvider("<b>a</b> Element<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">A hyperlink to another URL.</span>"),
				String.Format("{0}a href=\"", (includeStartDelimiter ? "<" : String.Empty)), "\""));
			session.Items.Add(new CompletionItem("br", new CommonImageSourceProvider(CommonImageKind.XmlTag), 
				new HtmlContentProvider("<b>br</b> Element<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Creates a line break.</span>"),
				String.Format("{0}br/>", (includeStartDelimiter ? "<" : String.Empty)), null));

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
		
	}

}
