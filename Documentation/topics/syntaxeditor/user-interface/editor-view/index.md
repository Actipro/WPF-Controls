---
title: "Overview"
page-title: "SyntaxEditor Editor View Features"
order: 1
---
# Overview

Editor views are where end users primarily interact with documents and are customizable in a number of ways.

> [!TIP]
> If you want your SyntaxEditor instances to look just like Visual Studio's code editor, use the `Consolas` font with a `FontSize` of `13`.

## Caret

The SyntaxEditor caret is the bar that blinks within an editor view and marks the location where typing will insert text.  It also is linked to the end position of a [selection](selection.md).  SyntaxEditor optionally supports multiple selections and therefore multiple carets, which enables simulataneous editing in several document locations at the same time.

See the [Caret](caret.md) topic for more information.

## Selection and Block Selection

SyntaxEditor has a powerful text selection model that supports both continuous stream (snaking) as well as block selection within editor views.

See the [Selection and Block Selection](selection.md) topic for more information.

## Locations, Offsets, and Positions

There are several ways to reference the location of characters within an [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView), each with their own benefits.

See the [Locations, Offsets, and Positions](locations-offsets-positions.md) topic for more information.

## View Lines

Each view consists of one or more view lines, which are the lines of text that are visible in the view.  View lines don't always match up with snapshot lines when features like word wrap or code outlining are active.

See the [View Lines](view-lines.md) topic for more information.

## Splitting

SyntaxEditor supports split views so that you can edit multiple areas of a document at the same time.

See the [Splitting](splitting.md) topic for more information.

## Editor View Margins

While the SyntaxEditor editor views have numerous built-in margins (line number, outlining, etc.), SyntaxEditor offers an extensibility point where custom margins can be created and added to editor views in any location.

See the [Editor View Margins](editor-view-margins.md) topic for more information.

## Whitespace

SyntaxEditor can optionally display whitespace characters, change tab size, do tab to space conversion, and more.

See the [Whitespace](whitespace.md) topic for more information.

## Word Wrap

Word wrapping is a powerful feature that  allows users to view all text for a line by wrapping text that normally would have been outside the view horizontally to one or more view lines in the editor.

See the [Word Wrap](word-wrap.md) topic for more information.

## Zooming

SyntaxEditor supports animated zooming in and out of views, which is a great feature to use when giving overhead presentations.  Only the scrollable area content of each view is zoomed, not the scrollbars.

See the [Zooming](zooming.md) topic for more information.

## Current Line Highlighting

SyntaxEditor supports highlighting of the current editor view line, meaning the view line that currently contains the caret.

See the [Current Line Highlighting](current-line-highlighting.md) topic for more information.

## Delimiter Highlighting

SyntaxEditor supports highlighting of the delimiter pairs that are currently next to the caret, also known as bracket highlighting.

While delimiter pairs such as `{...}`, and `(...)` are most commonly highlighted, delimiter highlighting also supports the highlighting of more than two delimiters at a time, such as when there are `#if...#else...#endif` blocks.

See the [Delimiter Highlighting](delimiter-highlighting.md) topic for more information.

## Code Block Selection

SyntaxEditor supports code block selection expansion and contraction, meaning that the view's selection can be expanded to include containing code blocks and contracted all the way back down to the caret as appropriate.

For instance, in C# the first time you expand the selection, it may select the containing identifier.  By expanding it again, it may select the containing expression, then the containing statement, then the containing method.  And so on up the compilation unit.

By contracting the selection, it goes back and selects the previously selected block.  Contracting can occur recursively to go back to the original selection.

See the [Code Block Selection](code-block-selection.md) topic for more information.

## Indicators

Indicators are special "tagged" regions of text that display a glyph in the indicator margin also generally highlight the text range with special styles.  Quick info can be displayed when hovering over the glyph, and custom mouse handling can be implemented to support functionality like toggling indicators when clicking in the indicator margin.

SyntaxEditor includes several built-in indicators: bookmarks, breakpoints, and current statement markers.  Any sort of custom indicator can easily be created as well.

See the [Indicators](indicators.md) topic for more information.

## Indentation Guides

Indentation guides are subtle vertical lines that render at each tab stop on lines prior to the first non-whitespace character.  Whitespace-only lines render indentation guides based on the tab stop level of surrounding text.

See the [Indentation Guides](indentation-guides.md) topic for more information.

## Scrolling

Scrolling can be performed by end users via keyboard, mouse wheel, or touch.  Editor views can be scrolled programmatically as well.

Scrollbars can optionally be hidden in the views, or even automatically hidden when not needed.

Scrollbar acceleration allows scrolling speed to increase the longer a scrollbar button is held.

Each editor view in SyntaxEditor also has four scrollbar tray areas, where custom controls can be inserted via data templates.

See the [Scrolling](scrolling.md) topic for more information.

## Hit Testing

SyntaxEditor makes it easy to hit test any `Point` in the editor and return detailed information about what is at that location.

See the [Hit Testing](hit-testing.md) topic for more information.

## View Lifecycle

SyntaxEditor views can be opened and closed via various end user actions.  This topic discussed how to be notified when views are opened and closed.

See the [View Lifecycle](lifecycle.md) topic for more information.

## Delayed UI Updates

SyntaxEditor includes a special event that is raised shortly after any text, caret, or selection changes occur within the editor.  This event can be handled to update external UI controls based on updated editor context.

See the [Delayed UI Updates](delayedui-updates.md) topic for more information.

## View Properties

The [ITextView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView).[Properties](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView.Properties) property provides a dictionary-like object that can store any sort of custom data that should be associated with the view.

The property returns an [PropertyDictionary](xref:ActiproSoftware.Text.Utility.PropertyDictionary) object, which supports any object type as a key or value.

See the [View Properties](view-properties.md) topic for more information.

## Single-Line Mode

SyntaxEditor is a multi-line editor by default, but also supports a single-line edit mode where it looks like a single-line native `TextBox`.

In this mode, nearly all of SyntaxEditor's features remain available, such as syntax highlighting, syntax error squiggle lines, and automated IntelliPrompt.

See the [Single-Line Mode](single-line-mode.md) topic for more information.
