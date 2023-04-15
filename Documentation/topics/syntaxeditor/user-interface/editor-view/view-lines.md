---
title: "View Lines"
page-title: "View Lines - SyntaxEditor Editor View Features"
order: 5
---
# View Lines

Each view consists of one or more view lines, which are the lines of text that are visible in the view.  View lines don't always match up with snapshot lines when features like word wrap and code outlining are active.

## View Lines vs. Snapshot Lines

View lines are the rendered display lines within a view, and are represented by the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) interface.  Snapshot lines are the actual physical lines within a [snapshot](../../text-parsing/core-text/documents-snapshots-versions.md), and are represented by the [ITextSnapshotLine](xref:ActiproSoftware.Text.ITextSnapshotLine) interface.

The view lines most often do line up exactly with the lines in the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[CurrentSnapshot](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.CurrentSnapshot), however this may not be the case when features such as [word wrap](word-wrap.md) or code outlining are active.

To better illustrate the difference, let us examine a snippet of code that is being edited in view.  This is the full text as it appears within the document's snapshot (line numbers are included for reference):

```
0: // Sample code
1: int myIntValue = 123;
2: string myStringValue = "This is a long bit of text that will be word wrapped.";
3: // End of code
```

You can see there are four lines in the snapshot.  Now assume that the editor has word wrap enabled.  And assume that the second last line is wrapped like so:

```
0: // Sample code
1: int myIntValue = 123;
2: string myStringValue = "This is a long bit of text
3: that will be word wrapped.";
4: // End of code
```

Even though there are four snapshot lines, there are five view lines needed to display the text due to the word wrap.

View lines are completely virtualized meaning they are generated on demand and are not persisted in memory.  This is a key feature since it means much less memory usage and processing required when working with large documents.

## Getting a View Line for an Offset

There is an [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[GetViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.GetViewLine*) method overload that accepts an offset parameter.  This method allows you to retrieve the view line that contains the specified offset.

This code gets the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) that contains offset `10`:

```csharp
editor.ActiveView.GetViewLine(10);
```

## Getting a View Line for a Text Position

There is an [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[GetViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.GetViewLine*) method overload that accepts a [TextPosition](xref:ActiproSoftware.Text.TextPosition) parameter.  This method allows you to retrieve the view line that contains the specified text position.

This code gets the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) that contains the start of the fourth line:

```csharp
editor.ActiveView.GetViewLine(new TextPosition(3, 0));
```

## Getting a View Line for a Location

There is an [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[GetViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.GetViewLine*) method overload that accepts two parameters: a y-coordinate and a [LocationToPositionAlgorithm](xref:@ActiproUIRoot.Controls.SyntaxEditor.LocationToPositionAlgorithm) to use.  This method allows you to retrieve the view line under a specific mouse coordinate for example.

## Getting a Relative View Line

If you already have an [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) instance and would like to find a view line that is above or below it by a certain number of lines, there is a [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[GetViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.GetViewLine*) overload that accepts the source [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) instance and the number of lines to traverse, which can be negative.

If you are just iterating view lines, the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) has [NextLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.NextLine) and previous [PreviousLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.PreviousLine) properties that return the next and previous view lines respectively.

## Getting or Setting the First Visible View Line

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[ScrollState](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.ScrollState) property returns a [TextViewScrollState](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextViewScrollState) that indicates the current view scroll state.  Scroll states allow a vertical anchor text position to be specified.  The view line containing the vertical anchor text position can be placed at the top, center, or bottom of the view and then further displaced by a pixel amount.  The view may also be scrolled horizontally by a pixel amount.  All of this information is provided by the scroll state.

This code shows how to get the resolved first [TextPosition](xref:ActiproSoftware.Text.TextPosition) visible on the first view line in the active editor view:

```csharp
var firstVisiblePosition = editor.ActiveView.VisibleViewLines[0].VisibleStartPosition;
```

See the [Locations, Offsets, and Positions](locations-offsets-positions.md) topic for details on scrolling to a specific [TextPosition](xref:ActiproSoftware.Text.TextPosition) using a [TextViewScrollState](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextViewScrollState).

## Display Line Number

Use the [DisplayLine](xref:ActiproSoftware.Text.TextPosition.DisplayLine) property from the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine).[VisibleStartPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.VisibleStartPosition) property result to obtain the one-based line number that appears in the line number margin.

## Scrolling

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[Scroller](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.Scroller) property provides access to an [IEditorViewScroller](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller) for the view.  This interface has a lot of methods such as [ScrollByPixels](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollByPixels*), [ScrollDown](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollDown*), [ScrollToDocumentEnd](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollToDocumentEnd*), [ScrollIntoView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollIntoView*), [ScrollTo](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollTo*) (for scrolling to a specific [TextViewScrollState](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextViewScrollState)), [ScrollToCaret](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollToCaret*), etc.  that can be used to scroll the view.

See the [IEditorViewScroller](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller) class library member list documentation for the list of methods that are available.

@if (winrt wpf) {

## Auto-Sizing the View to View Line Contents

Sometimes it is desirable for the editor to resize itself based on the current text content.  This feature is useful in scenarios such as where an editor is in a `StackPanel` and its height needs to grow as text is entered by the user.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsViewLineMeasureEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsViewLineMeasureEnabled) property, which defaults to `false`, can be set to `true` to force each view line to be measured during measure phases, thus allowing the containing editor to resize based on the current text content.

> [!WARNING]
> Enabling this feature will affect performance and is only recommended for scenarios where small documents are expected to be edited.  If you expect the editor to always be docked to fill an available space, leave this feature disabled.

}
