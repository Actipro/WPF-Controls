using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using VKey = System.Windows.Input.Key;
using VModifierKeys = System.Windows.Input.ModifierKeys;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionFiltering;

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
	/// Indicates whether filter tabs are visible.
	/// </summary>
	public bool FilterTabsVisible { get; set; }

	/// <summary>
	/// Indicates whether to filter unmatched items.
	/// </summary>
	public bool FilterUnmatchedItems { get; set; }

	/// <summary>
	/// Indicates whether the inherited filter button is visible.
	/// </summary>
	public bool InheritedFilterButtonVisible { get; set; }

	/// <summary>
	/// Indicates whether member type filter buttons are visible.
	/// </summary>
	public bool MemberTypeFilterButtonsVisible { get; set; }

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
			CanFilterUnmatchedItems = FilterUnmatchedItems,
			MatchOptions = CompletionMatchOptions.UseAcronyms | CompletionMatchOptions.UseShorthand
		};

		// Add some items
		var highlightingStyleRegistry = view.HighlightingStyleRegistry;
		var commentWebColor = HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor();
		var keywordWebColor = HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor();
		var typeNameWebColor = HtmlContentProvider.GetTypeNameForegroundColor(highlightingStyleRegistry).ToWebColor();
		session.Items.Add(new CompletionItem("_intValue", new CommonImageSourceProvider(CommonImageKind.FieldPrivate),
			new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">int</span> <b>Foo._intValue</b><br/><span style=\"color: {commentWebColor};\">An integer value.</span>")));
		session.Items.Add(new CompletionItem("Equals", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">bool</span> <b>object.Equals</b>(<span style=\"color: {keywordWebColor};\">object</span> obj)<br/>"
				+ $"<span style=\"color: {commentWebColor};\">Determines whether the specified <b>System.Object</b> is equal to the current <b>System.Object</b>.</span>")));
		session.Items.Add(new CompletionItem("GetHashCode", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">int</span> <b>object.GetHashCode</b>()<br/><span style=\"color: {commentWebColor};\">Gets a hash code for this <b>System.Object</b>.</span>")));
		session.Items.Add(new CompletionItem("GetType", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<span style=\"color: {typeNameWebColor};\">Type</span> <b>object.GetType</b>()<br/><span style=\"color: {commentWebColor};\">Gets the <b>System.Type</b> of the current instance.</span>")));
		session.Items.Add(new CompletionItem("IntValue", new CommonImageSourceProvider(CommonImageKind.PropertyPublic),
			new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">int</span> <b>Foo.IntValue</b><br/><span style=\"color: {commentWebColor};\">An integer value.</span>")));
		session.Items.Add(new CompletionItem("IntValueChanged", new CommonImageSourceProvider(CommonImageKind.EventPublic),
			new HtmlContentProvider($"<span style=\"color: {typeNameWebColor};\">EventHandler</span> <b>Foo.IntValueChanged</b>")));
		session.Items.Add(new CompletionItem("OnIntValueChanged", new CommonImageSourceProvider(CommonImageKind.MethodProtected),
			new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">void</span> <b>Foo.OnIntValueChanged</b>(<span style=\"color: {typeNameWebColor};\">EventArgs</span> e)")));
		session.Items.Add(new CompletionItem("ToString", new CommonImageSourceProvider(CommonImageKind.MethodPublic),
			new HtmlContentProvider($"<span style=\"color: {keywordWebColor};\">string</span> <b>object.ToString</b>()<br/><span style=\"color: {commentWebColor};\">Returns the string representation of the object.</span>")));

		//
		// NOTE: In the filters below, the filtering conditions are just looking at the item text and image used.
		//   Normally you would store some contextual object in the Tag property and examine that instead.
		//

		// Define a simple image-based filter that will include items based on the nameo of the image used
		static CompletionFilterResult FilterByImageKindName(ICompletionItem item, string startsOrEndsWithText) {
			if (
				(item.Text is null)
				|| (((CommonImageSourceProvider?)item.ImageSourceProvider)?.ImageKind.ToString().StartsWith(startsOrEndsWithText) == true)
				|| (((CommonImageSourceProvider?)item.ImageSourceProvider)?.ImageKind.ToString().EndsWith(startsOrEndsWithText) == true)
			) {
				return CompletionFilterResult.Included;
			}
			else
				return CompletionFilterResult.Excluded;
		}

		static CompletionFilterResult FilterByText(ICompletionItem item, params string[] matches) {
			return (item.Text is { } text) && (matches.Contains(text))
				? CompletionFilterResult.Included
				: CompletionFilterResult.Excluded;
		}

		if (MemberTypeFilterButtonsVisible == true) {
			// Add member type filters
			session.Filters.Add(new CompletionFilter("Events", (_, item) => FilterByImageKindName(item, "Event")) {
				DisplayMode = CompletionFilterDisplayMode.ToggleButton,
				GroupName = "MemberType",
				ToolTip = "Events",
				Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.EventPublic).GetImageSource() },
				KeyGesture = new KeyGesture(VKey.E, VModifierKeys.Alt)
			});
			session.Filters.Add(new CompletionFilter("Fields", (_, item) => FilterByImageKindName(item, "Field")) {
				DisplayMode = CompletionFilterDisplayMode.ToggleButton,
				GroupName = "MemberType",
				ToolTip = "Fields",
				Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.FieldPublic).GetImageSource() },
				KeyGesture = new KeyGesture(VKey.F, VModifierKeys.Alt)
			});
			session.Filters.Add(new CompletionFilter("Methods", (_, item) => FilterByImageKindName(item, "Method")) {
				DisplayMode = CompletionFilterDisplayMode.ToggleButton,
				GroupName = "MemberType",
				ToolTip = "Methods",
				Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.MethodPublic).GetImageSource() },
				KeyGesture = new KeyGesture(VKey.M, VModifierKeys.Alt)
			});
			session.Filters.Add(new CompletionFilter("Properties", (_, item) => FilterByImageKindName(item, "Property")) {
				DisplayMode = CompletionFilterDisplayMode.ToggleButton,
				GroupName = "MemberType",
				ToolTip = "Properties",
				Content = new Image() { Width = 16, Height = 16, Source = new CommonImageSourceProvider(CommonImageKind.PropertyPublic).GetImageSource() },
				KeyGesture = new KeyGesture(VKey.P, VModifierKeys.Alt)
			});
		}

		if (InheritedFilterButtonVisible == true) {
			// Add inherited filter
			session.Filters.Add(new CompletionFilter("Inherited",
				(_, item) => FilterByText(item, "Equals", "GetHashCode", "GetType", "ToString")) {
				DisplayMode = CompletionFilterDisplayMode.ToggleButton,
				GroupName = "Inherited",
				ToolTip = "Inherited members",
				Content = "Inherited",
				KeyGesture = new KeyGesture(VKey.I, VModifierKeys.Alt)
			});
		}

		if (FilterTabsVisible == true) {
			// Add access filters
			session.Filters.Add(new CompletionFilter("All", (_, _) => CompletionFilterResult.Included) {
				DisplayMode = CompletionFilterDisplayMode.AllTab,
				ToolTip = "All members",
				Content = "All members",
				KeyGesture = new KeyGesture(VKey.A, VModifierKeys.Alt)
			});
			session.Filters.Add(new CompletionFilter("Public", (_, item) => FilterByImageKindName(item, "Public")) {
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
