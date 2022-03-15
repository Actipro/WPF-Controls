---
title: "Overview"
page-title: "SyntaxEditor Searching (Find/Replace) Features"
order: 1
---
# Overview

SyntaxEditor's view search model interacts with the view's selection and provides extensive find/replace functionality.  A built-in search overlay pane makes this functionality accessible to end users.

@if (winrt wpf) {

A standalone [EditorSearchView](editor-search-view.md) control can be used to instantly recreate a classic Visual Studio-like search experience for end users. 

}

## Programmatically Searching

SyntaxEditor includes a powerful view search model that is layered on top of the low-level search model found in the text framework.

See the [Programmatically Searching](programmatic-searching.md) topic for more information.

## Search Overlay Pane

The search overlay pane is a control built into SyntaxEditor that allows find and replace operations to be performed.  When active, the pane appears in-line at the upper right corner of the text area.

See the [Search Overlay Pane](search-overlay-pane.md) topic for more information.

## Incremental Search

SyntaxEditor supports incremental search, which is a feature that allows for fast text searching via the keyboard, and without the use of any search dialogs or panes.

See the [Incremental Search](incremental-search.md) topic for more information.

## Search Result Highlighting

When performing searches, many modern editors actively display matching results as the user types the find text.  SyntaxEditor supports the live highlighting of search results.

See the [Search Result Highlighting](search-result-highlighting.md) topic for more information.

@if (winrt wpf) {

## EditorSearchView Control

The [EditorSearchView](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.EditorSearchView) control is a standalone control that can be used within an application tool window to provide an user interface with Visual Studio-like search capabilities for a [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).

See the [EditorSearchView Control](editor-search-view.md) topic for more information.

}
