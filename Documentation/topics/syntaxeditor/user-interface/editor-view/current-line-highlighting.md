---
title: "Current Line Highlighting"
page-title: "Current Line Highlighting - SyntaxEditor Editor View Features"
order: 11
---
# Current Line Highlighting

SyntaxEditor supports two methods of highlighting the current editor view line, meaning the view that currently contains the caret:
- Current line highlighting
- Current line number highlighting

## Current Line Highlighting

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsCurrentLineHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsCurrentLineHighlightingEnabled) property can be set to `true` to activate current line highlighting.  It is off by default.

When active, a border can be rendered above and below the entire line with an optional background brush.

### Changing the Highlight Brush

The brushes used to render the highlight can be adjusted by the end user since it is exposed via a special classification type's style in the highlighting style registry.

See the "Special Classification Types" section in the [Highlighting Style Registries](../styles/highlighting-style-registries.md) topic for more information on how to modify the style.

## Current Line Number Highlighting

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsCurrentLineNumberHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsCurrentLineHighlightingEnabled) property can be set to `true` to activate current line number highlighting.  It is on by default.

When active and the line number margin is displayed, the current line number will be rendered in a different style than the other line numbers.

### Changing the Current Line Number Style

The brushes used to render the line number can be adjusted by the end user since it is exposed via a special classification type's style in the highlighting style registry.

See the "Special Classification Types" section in the [Highlighting Style Registries](../styles/highlighting-style-registries.md) topic for more information on how to modify the style.