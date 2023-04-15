---
title: "Search Result Highlighting"
page-title: "Search Result Highlighting - SyntaxEditor Searching (Find/Replace) Features"
order: 10
---
# Search Result Highlighting

When performing searches, many modern editors actively display matching results as the user types the find text.  SyntaxEditor supports the live highlighting of search results.

There are two common usage scenarios for search result highlighting:  incremental searches and search panes.

## Incremental Search Highlights

The [Incremental Search](incremental-search.md) features built into SyntaxEditor allow for an end user to quickly search for a string that they type.  When the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsSearchResultHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsSearchResultHighlightingEnabled) property is set to `true` (the default), highlights will appear throughout the document showing all matches for the find text.

This allows the end user to immediately visualize where all matches occur instead of having to navigate through the incremental search matches one-by-one.

## Search Pane Highlights

If your app has a search pane, it may wish to highlight search results live as the user types in the search pane's **Find** textbox, and prior to the user pressing the **Find** button.

If you are using the built-in [search overlay pane](search-overlay-pane.md), this will happen automatically.

@if (winrt wpf) {

If you are using an external search pane (like an [EditorSearchView](editor-search-view.md)), then the following manual process is required to update the highlights.

}

This can be achieved by setting an [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) instance to the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[HighlightedResultSearchOptions](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.HighlightedResultSearchOptions) property.  Every time that property is set or an option is updated, a worker task kicks off that does a find all operation in the document and then when complete, updates the search result highlights.

Set the property to a `null` value when the search pane closes, so that all highlights will be removed.

This code shows how highlights could be displayed over any text in the document with the words "highlight this text":

```csharp
EditorSearchOptions options = new EditorSearchOptions;
options.FindText = "highlight this text";
editor.ActiveView.HighlightedResultSearchOptions = options;
```

> [!IMPORTANT]
> Make sure that the [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) object passed in implements an `Equals` method override that examines each property in the options, since this is used for internal comparisons.  The predefined [EditorSearchOptions](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.EditorSearchOptions) class does so.

## Requirements

This code is required to be called once per application instance since it registers the classification types and related styles for the highlights.

```csharp
new DisplayItemClassificationTypeProvider().RegisterAll();
```

> [!IMPORTANT]
> If the classification types are not registered, then the highlights might not display.
