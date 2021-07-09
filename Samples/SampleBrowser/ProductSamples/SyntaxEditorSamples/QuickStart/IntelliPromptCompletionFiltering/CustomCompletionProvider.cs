using System;
using System.Windows.Input;
using System.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using VKey = System.Windows.Input.Key;
using VModifierKeys = System.Windows.Input.ModifierKeys;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionFiltering {

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
		/// Gets or sets a value indicating whether filter tabs are visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if filter tabs are visible; otherwise, <c>false</c>.
		/// </value>
		public bool FilterTabsVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to filter unmatched items.
		/// </summary>
		/// <value>
		/// <c>true</c> if unmatched items are to be filtered; otherwise, <c>false</c>.
		/// </value>
		public bool FilterUnmatchedItems { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the inherited filter button is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if the inherited filter button is visible; otherwise, <c>false</c>.
		/// </value>
		public bool InheritedFilterButtonVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether member type filter buttons are visible.
		/// </summary>
		/// <value>
		/// <c>true</c> if member type filter buttons are visible; otherwise, <c>false</c>.
		/// </value>
		public bool MemberTypeFilterButtonsVisible { get; set; }
		
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
			// The items and filters for this completion list are hard coded in this sample and
			// are simply meant to illustrate the rich features of the SyntaxEditor completion list.
			// When implementing a real language, you should vary the items and filters based
			// on the current context of the caret.
			//

			// Create a session
			CompletionSession session = new CompletionSession();
			session.CanCommitWithoutPopup = canCommitWithoutPopup;
			session.CanFilterUnmatchedItems = this.FilterUnmatchedItems;
			session.MatchOptions = CompletionMatchOptions.UseAcronyms | CompletionMatchOptions.UseShorthand;

			// Add some items
			var highlightingStyleRegistry = view.HighlightingStyleRegistry;
			session.Items.Add(new CompletionItem("_intValue", new CommonImageSourceProvider(CommonImageKind.FieldPrivate), 
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">int</span> <b>Foo._intValue</b><br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">An integer value.</span>")));
			session.Items.Add(new CompletionItem("Equals", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">bool</span> <b>object.Equals</b>(<span style=\"color: " + 
					HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">object</span> obj)<br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Determines whether the specified <b>System.Object</b> is equal to the current <b>System.Object</b>.</span>")));
			session.Items.Add(new CompletionItem("GetHashCode", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">int</span> <b>object.GetHashCode</b>()<br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Gets a hash code for this <b>System.Object</b>.</span>")));
			session.Items.Add(new CompletionItem("GetType", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetTypeNameForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Type</span> <b>object.GetType</b>()<br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Gets the <b>System.Type</b> of the current instance.</span>")));
			session.Items.Add(new CompletionItem("IntValue", new CommonImageSourceProvider(CommonImageKind.PropertyPublic), 
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">int</span> <b>Foo.IntValue</b><br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">An integer value.</span>")));
			session.Items.Add(new CompletionItem("IntValueChanged", new CommonImageSourceProvider(CommonImageKind.EventPublic),
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetTypeNameForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">EventHandler</span> <b>Foo.IntValueChanged</b>")));
			session.Items.Add(new CompletionItem("OnIntValueChanged", new CommonImageSourceProvider(CommonImageKind.MethodProtected),
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">void</span> <b>Foo.OnIntValueChanged</b>(<span style=\"color: " + 
					HtmlContentProvider.GetTypeNameForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">EventArgs</span> e)")));
			session.Items.Add(new CompletionItem("ToString", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
				new HtmlContentProvider("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">string</span> <b>object.ToString</b>()<br/><span style=\"color: " + 
					HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Returns the string representation of the object.</span>")));

			//
			// NOTE: In the filters below, the filtering conditions are just looking at the item text and image used.
			//   Normally you would store some contextual object in the Tag property and examine that instead.
			//

			if (this.MemberTypeFilterButtonsVisible == true) {
				// Add member type filters
				session.Filters.Add(new CompletionFilter("Events",
					(mysession, item) => (((item.Text == null) || (((CommonImageSourceProvider)item.ImageSourceProvider).ImageKind.ToString().StartsWith("Event"))) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded)
					) {
						DisplayMode = CompletionFilterDisplayMode.ToggleButton,
						GroupName = "MemberType",
						ToolTip = "Events",
						Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.EventPublic).GetImageSource() },
						KeyGesture = new KeyGesture(VKey.E, VModifierKeys.Alt)
					});
				session.Filters.Add(new CompletionFilter("Fields",
					(mysession, item) => (((item.Text == null) || (((CommonImageSourceProvider)item.ImageSourceProvider).ImageKind.ToString().StartsWith("Field"))) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded)
					) {
						DisplayMode = CompletionFilterDisplayMode.ToggleButton,
						GroupName = "MemberType",
						ToolTip = "Fields",
						Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.FieldPublic).GetImageSource() },
						KeyGesture = new KeyGesture(VKey.F, VModifierKeys.Alt)
					});
				session.Filters.Add(new CompletionFilter("Methods",
					(mysession, item) => (((item.Text == null) || (((CommonImageSourceProvider)item.ImageSourceProvider).ImageKind.ToString().StartsWith("Method"))) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded)
					) {
						DisplayMode = CompletionFilterDisplayMode.ToggleButton,
						GroupName = "MemberType",
						ToolTip = "Methods",
						Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.MethodPublic).GetImageSource() },
						KeyGesture = new KeyGesture(VKey.M, VModifierKeys.Alt)
					});
				session.Filters.Add(new CompletionFilter("Properties",
					(mysession, item) => (((item.Text == null) || (((CommonImageSourceProvider)item.ImageSourceProvider).ImageKind.ToString().StartsWith("Property"))) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded)
					) {
						DisplayMode = CompletionFilterDisplayMode.ToggleButton,
						GroupName = "MemberType",
						ToolTip = "Properties",
						Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.PropertyPublic).GetImageSource() },
						KeyGesture = new KeyGesture(VKey.P, VModifierKeys.Alt)
					});
			}

			if (this.InheritedFilterButtonVisible == true) {
				// Add inherited filter
				session.Filters.Add(new CompletionFilter("Inherited",
					(CompletionFilterPredicate)delegate(ICompletionSession mysession, ICompletionItem item) { return ("Equals,GetHashCode,GetType,ToString".IndexOf(item.Text) != -1) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded; }) {
						DisplayMode = CompletionFilterDisplayMode.ToggleButton,
						GroupName = "Inherited",
						ToolTip = "Inherited members",
						Content = "Inherited",
						KeyGesture = new KeyGesture(VKey.I, VModifierKeys.Alt)
					});
			}

			if (this.FilterTabsVisible == true) {
				// Add access filters
				session.Filters.Add(new CompletionFilter("All",
					(mysession, item) => (((item.Text == null) || (((CommonImageSourceProvider)item.ImageSourceProvider).ImageKind.ToString().EndsWith("Public"))) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded)
					) {
						DisplayMode = CompletionFilterDisplayMode.AllTab,
						ToolTip = "All members",
						Content = "All members",
						KeyGesture = new KeyGesture(VKey.A, VModifierKeys.Alt)
					});
				session.Filters.Add(new CompletionFilter("Public",
					(mysession, item) => (((item.Text == null) || (((CommonImageSourceProvider)item.ImageSourceProvider).ImageKind.ToString().EndsWith("Public"))) ? CompletionFilterResult.Included : CompletionFilterResult.Excluded)
					) {
						DisplayMode = CompletionFilterDisplayMode.Tab,
						ToolTip = "Public members",
						Content = "Public members",
						KeyGesture = new KeyGesture(VKey.B, VModifierKeys.Alt)
					});
			}

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
