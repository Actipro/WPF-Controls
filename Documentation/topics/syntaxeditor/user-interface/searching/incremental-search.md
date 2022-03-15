---
title: "Incremental Search"
page-title: "Incremental Search - SyntaxEditor Searching (Find/Replace) Features"
order: 9
---
# Incremental Search

SyntaxEditor supports incremental search, which is a feature that allows for fast text searching via the keyboard, and without the use of any search dialogs or panes.

## Run-Time Usage

Incremental searches are performed from the current caret offset in the editor.  To start incremental search mode, press `Ctrl+I` and start typing some characters.  The characters will be accumulated into a find text string and used to search the document for the next instance of the find text, which will be selected.

All find text matches will be automatically highlighted as long as the [Search Result Highlighting](search-result-highlighting.md) feature is enabled.

Press `Ctrl+I` to move to the next match.  Press `Ctrl+Shift+I` to move to the previous match.

Continue typing characters to append to the find text. `Backspace` can be used to remove characters from the find text. `Esc` can be used to deactivate incremental search mode.

## UI Updates While Searching

The application UI should provide some sort of status indicator when incremental search events occur.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ViewSearch](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ViewSearch) event fires whenever any editor view search occurs, including when incremental searches take place.  In this scenario, the result set's [ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet).[OperationType](xref:ActiproSoftware.Text.Searching.ISearchResultSet.OperationType) property will be `FindNextIncremental`.  UI such as a statusbar can be updated in an event handler to indicate that incremental search is active and what the find text is.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ViewIsIncrementalSearchActiveChanged](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ViewIsIncrementalSearchActiveChanged) event fires whenever incremental search mode is activated or deactivated.  It's a great place to clear out any UI that indicates incremental search is active.  The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[IsIncrementalSearchActive](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.IsIncrementalSearchActive) property is used to know if incremental search mode is active.

This code shows how a statusbar panel could be updated in response to the events mentioned above:

```csharp
private void OnSyntaxEditorViewIsIncrementalSearchActiveChanged(object sender, TextViewEventArgs e) {
	IEditorView editorView = e.View as IEditorView;
	if ((editorView != null) && (!editorView.IsIncrementalSearchActive)) {
		// Incremental search is now deactivated
		messagePanel.Content = "Ready";
	}
}
		
private void OnSyntaxEditorViewSearch(object sender, EditorViewSearchEventArgs e) {
	// If an incremental search was performed...
	if (e.ResultSet.OperationType == SearchOperationType.FindNextIncremental) {
		// Show a statusbar message
		bool hasFindText = !String.IsNullOrEmpty(e.ResultSet.Options.FindText);
		bool notFound = (hasFindText) && (e.ResultSet.Results.Count == 0);
		string notFoundMessage = (notFound ? " (not found)" : String.Empty);
		messagePanel.Content = "Incremental Search: " + e.ResultSet.Options.FindText + notFoundMessage;
	}
}
```

@if (wpf) {

> [!NOTE]
> Incremental search also automatically sets a custom mouse cursor while it is active.

}

@if (winrt) {

> [!NOTE]
> Incremental search also automatically sets a custom mouse cursor while it is active, as long as the cursor has been registered according to the instructions in the [Custom Cursors](../editor-view/custom-cursors.md) topic.

}

## Trimming Unmatched Find Text

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CanIncrementalSearchTrimUnmatchedFindText](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanIncrementalSearchTrimUnmatchedFindText), which defaults to `true`, determines whether characters typed by the end user while incremental search is active will be appended to the find text in scenarios where the previous find text didn't match anything.

For instance, say incremental search mode is active and the end user types "int".  This finds a match.  Then the user types "e" but no match is found.  At this point, if [CanIncrementalSearchTrimUnmatchedFindText](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanIncrementalSearchTrimUnmatchedFindText) is `true` and another character like "g" is typed, it will be ignored and not appended to the find text.  If the property is `false` instead, the "g" would be appended to the find text.

## Disabling Incremental Search

Incremental search features are on by default and can be accessed by the end user via the `Ctrl+I` and `Ctrl+Shift+I` key bindings.

To prevent the end user from activating incremental search mode, remove the appropriate entries from the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).`InputBindings` collection.
