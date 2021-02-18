---
title: "Current Line Highlighting"
page-title: "Current Line Highlighting - SyntaxEditor Editor View Features"
order: 11
---
# Current Line Highlighting

SyntaxEditor supports highlighting of the current editor view line, meaning the view line that currently contains the caret.

## Activating Current Line Highlighting

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[IsCurrentLineHighlightingEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsCurrentLineHighlightingEnabled) property can be set to `true` to activate current line highlighting.  It is off by default.

## Changing the Highlight Brush

The brush used to render the highlight can be adjusted by the end user since it is exposed via a special classification type's style in the highlighting style registry.

See the "Special Classification Types" section in the [Highlighting Style Registries](../styles/highlighting-style-registries.md) topic for more information on how to modify the style.
