---
title: "Programmatically Searching"
page-title: "Programmatically Searching - SyntaxEditor Searching (Find/Replace) Features"
order: 2
---
# Programmatically Searching

SyntaxEditor includes a powerful view search model that is layered on top of the low-level search model found in the text framework.

## Understanding the View Search Model vs. the Low-Level Search Model

Before diving into this topic any further, it is essential that you understand the information presented in the [Low-Level Searching Operations](../../text-parsing/advanced-text/searching.md) topic.  That topic describes the core search object model and its capabilities.  Most of the same types described in that topic are used in the "view" search model.

The "view" search model layers itself on top of the core model and provides some extended functionality.  Most of the extended functionality deals with manipulating the search operations based on the view's selection and updated the selection when appropriate.

In general, the rule of thumb is: if you are going to search a document that is open within a SyntaxEditor control, use the "view" search model.  If you are going to search a document that is not open in a SyntaxEditor control, use the low-level search model.

## Accessing the View Search Model

The "view" search model is accessed via the [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView).[Searcher](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView.Searcher) property.  This property returns an [IEditorViewSearcher](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewSearcher) instance, which has find/replace functionality built-in.

Since the view search model is accessed for a particular view, it interacts with the owner view's selection.

## Editor Search Options

The [IEditorSearchOptions](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorSearchOptions) interface specifies the options type that must be passed into the view search model methods.  It inherits the low-level [ISearchOptions](xref:ActiproSoftware.Text.Searching.ISearchOptions) interface, which is described in the [Low-Level Searching Operations](../../text-parsing/advanced-text/searching.md) topic.

This view search model options interface adds a [Scope](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorSearchOptions.Scope) property, which is an enumeration value of type [EditorSearchScope](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditorSearchScope).  It specifies whether the search is performed within the entire document or in the current selection only.

The [EditorSearchOptions](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation.EditorSearchOptions) is an implementation of the [IEditorSearchOptions](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorSearchOptions) interface and can be used in your code.

## Search Result Sets

Each search method returns an [ISearchResultSet](xref:ActiproSoftware.Text.Searching.ISearchResultSet) object, which is described in detail in the [Low-Level Searching Operations](../../text-parsing/advanced-text/searching.md) topic.

## Performing a Find Next Operation

A find next operation can be performed like this:

```csharp
EditorSearchOptions options = new EditorSearchOptions();
options.FindText = "findtext";
options.PatternProvider = SearchPatternProviders.Normal;
ISearchResultSet resultSet = editor.ActiveView.Searcher.FindNext(options);
```

## Performing a Replace Next Operation

A replace next operation can be performed like this:

```csharp
EditorSearchOptions options = new EditorSearchOptions();
options.FindText = "findtext";
options.ReplaceText = "replacetext";
options.PatternProvider = SearchPatternProviders.Normal;
ISearchResultSet resultSet = editor.ActiveView.Searcher.ReplaceNext(options);
```

## Performing a Replace All Operation

A replace all operation can be performed like this:

```csharp
EditorSearchOptions options = new EditorSearchOptions();
options.FindText = "findtext";
options.ReplaceText = "replacetext";
options.PatternProvider = SearchPatternProviders.Normal;
ISearchResultSet resultSet = editor.ActiveView.Searcher.ReplaceAll(options);
```
