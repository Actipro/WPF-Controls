---
title: "Locations, Offsets, and Positions"
page-title: "Locations, Offsets, and Positions - SyntaxEditor Editor View Features"
order: 4
---
# Locations, Offsets, and Positions

There are several ways to reference the location of characters within an [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView), each with their own benefits.

> [!NOTE]
> Before reading this topic, first read the [Offsets, Ranges, and Positions](../../text-parsing/core-text/offsets-ranges-positions.md) topic since that provides a lot of information about core text-related structures that apply to the information in this topic too.

## Offsets and Positions

As mentioned in the [Offsets, Ranges, and Positions](../../text-parsing/core-text/offsets-ranges-positions.md) topic, offsets are zero-based integer numbers indicating a character index from the beginning of a string or snapshot and text positions are similar but specify a line index and a character index within that line instead.

Offsets and positions are used within [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView) objects to provide layout information to the view lines.  However it is important to note that while a [TextPosition](xref:ActiproSoftware.Text.TextPosition) (`4,0`) is the fifth line in a view's [CurrentSnapshot](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView.CurrentSnapshot), it may not be the fifth view line in the view, even when the view is scrolled all the way to the top.  Things that can cause this scenario are [word wrap](word-wrap.md), collapsed outlining nodes, etc.  So in the same scenario, assume word wrap is on and the second line wrapped once.  This would mean that [TextPosition](xref:ActiproSoftware.Text.TextPosition) (`4,0`) would appear as the sixth line.

## Scrolling a View to a Position

The [ITextView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView).[ScrollState](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView.ScrollState) property returns the current resolved scroll state for the view.

See the [Scrolling](scrolling.md) topic for details on how to programmatically scroll a view to show a position's view line.

## Locations

When related to a view, locations are the term used to describe the x,y layout position such as mouse coordinates, etc.  They are specified using the Windows `Point` structure.

## Converting Between Locations and Offsets/Positions

Locations can be translated to a [TextPosition](xref:ActiproSoftware.Text.TextPosition) by using the [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView).[LocationToPosition](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView.LocationToPosition*) method.  This method accepts a `Point` in text area-relative coordinates.  The [TransformToTextArea](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView.TransformToTextArea*) method can transform a view-relative coordinate to the text area.  A [LocationToPositionAlgorithm](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.LocationToPositionAlgorithm) enumeration value is also passed, indicating how the logic used to convert the x,y coordinate to a [TextPosition](xref:ActiproSoftware.Text.TextPosition).

The [GetCharacterBounds](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView.GetCharacterBounds*) method can return the `Rect` coordinates that contain the character at a particular offset or [TextPosition](xref:ActiproSoftware.Text.TextPosition).

## Hit Testing

SyntaxEditor features a very powerful hit testing mechanism where you indicate a `Point` relative to the SyntaxEditor control and it returns very detailed information about what is at the location.  Information includes related character offsets, positions, etc. as well.

See the [Hit Testing](hit-testing.md) topic for information on how to perform hit testing.
