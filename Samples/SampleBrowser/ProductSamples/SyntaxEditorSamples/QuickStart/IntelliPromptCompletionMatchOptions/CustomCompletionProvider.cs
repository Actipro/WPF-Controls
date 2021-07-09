using System;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionMatchOptions {
	
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
		
		/// Gets or sets whether list items show possible matched characters in a highlight color.
		/// </summary>
		/// <value>
		/// <c>true</c> if list items show possible matched characters in a highlight color; otherwise, <c>false</c>.
		/// </value>
		public bool CanHighlightMatchedText { get; set; }

		/// <summary>
		/// Gets or sets a value indicating case sensitivity.
		/// </summary>
		/// <value>
		/// <c>true</c> if case sensitive; otherwise, <c>false</c>.
		/// </value>
		public bool IsCaseSensitive { get; set; }
		
		/// <summary>
		/// Occurs after a text change occurs to an <see cref="IEditorDocument"/> that uses this language.
		/// </summary>
		/// <param name="editor">The <see cref="SyntaxEditor"/> whose <see cref="IEditorDocument"/> is changed.</param>
		/// <param name="e">The <c>EditorSnapshotChangedEventArgs</c> that contains the event data.</param>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		protected virtual void OnDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
			if ((editor == null) || (e == null) || (e.TextChange == null) || (e.TextChange.Source != editor.ActiveView))
				return;

			// The e.TypedText is not null only when a Typing change occurs that inserts text,
			//   so we can check that to display the completion list when "." is typed
			switch (e.TypedText) {
				case ".": {
					// Use a snapshot reader to iterate backwards through the active view's current text
					var reader = editor.ActiveView.GetReader();
					reader.ReadCharacterReverseThrough('.');
					var token = reader.ReadTokenReverse();
				
					// In production code, a token ID comparison would be better than this string comparison
					if ((token != null) && (reader.TokenText == "this")) {
						// A dot was typed after a "this" keyword so show the completion list
						this.RequestSession(editor.ActiveView, false);
					}
					break;
				}
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

			// Set options
			session.CanHighlightMatchedText = this.CanHighlightMatchedText;
			session.MatchOptions = CompletionMatchOptions.None;
			if (IsCaseSensitive == false)
				session.MatchOptions |= CompletionMatchOptions.IsCaseInsensitive;
			if (RequiresExact == true)
				session.MatchOptions |= CompletionMatchOptions.RequiresExact;
			if (UseAcronyms == true)
				session.MatchOptions |= CompletionMatchOptions.UseAcronyms;
			if (UseShorthand == true)
				session.MatchOptions |= CompletionMatchOptions.UseShorthand;

			// Add some items
			var highlightingStyleRegistry = view.HighlightingStyleRegistry;
			session.Items.Add(new CompletionItem("aField", new CommonImageSourceProvider(CommonImageKind.FieldPrivate), 
				new HtmlContentProvider("<img src=\"resource:FieldPrivate\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">int</span> <b>Foo.aField</b>")));
			session.Items.Add(new CompletionItem("AMethod", new CommonImageSourceProvider(CommonImageKind.MethodPublic), 
				new HtmlContentProvider("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">void</span> <b>Foo.AMethod</b>()")));
			session.Items.Add(new CompletionItem("AnIntValue", new CommonImageSourceProvider(CommonImageKind.PropertyPublic), 
				new HtmlContentProvider("<img src=\"resource:PropertyPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">int</span> <b>Foo.AnIntValue</b>")));
			session.Items.Add(new CompletionItem("AStringValue", new CommonImageSourceProvider(CommonImageKind.PropertyPublic), 
				new HtmlContentProvider("<img src=\"resource:PropertyPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">string</span> <b>Foo.AStringValue</b>")));
			session.Items.Add(new CompletionItem("Equals", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + 
					";\">bool</span> <b>object.Equals</b>(<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">object</span> obj)<br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Determines whether the specified <b>System.Object</b> is equal to the current <b>System.Object</b>.</span>")));
			session.Items.Add(new CompletionItem("GetHashCode", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + 
				";\">int</span> <b>object.GetHashCode</b>()<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Gets a hash code for this <b>System.Object</b>.</span>")));
			session.Items.Add(new CompletionItem("GetType", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetTypeNameForegroundColor(highlightingStyleRegistry).ToWebColor() + 
				";\">Type</span> <b>object.GetType</b>()<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Gets the <b>System.Type</b> of the current instance.</span>")));
			session.Items.Add(new CompletionItem("ToString", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> <span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + 
				";\">string</span> <b>object.ToString</b>()<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Returns the string representation of the object.</span>")));

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
		/// Gets or sets a value requiring exact matches.
		/// </summary>
		/// <value><c>true</c> if requiring exact matches; otherwise, <c>false</c>.</value>
		public bool RequiresExact { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use acronyms.
		/// </summary>
		/// <value><c>true</c> if acronyms are to be used; otherwise, <c>false</c>.</value>
		public bool UseAcronyms { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use shorthand.
		/// </summary>
		/// <value><c>true</c> if shorthand is to be used; otherwise, <c>false</c>.</value>
		public bool UseShorthand { get; set; }

	}
}
