---
title: "Single-Line Mode"
page-title: "Single-Line Mode - SyntaxEditor Editor View Features"
order: 28
---
# Single-Line Mode

SyntaxEditor is a multi-line editor by default, but also supports a single-line edit mode where it looks like a single-line native `TextBox`.

In this mode, nearly all of SyntaxEditor's features remain available, such as syntax highlighting, syntax error squiggle lines, and automated IntelliPrompt.

## Activating Single-Line Mode

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[IsMultiLine](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsMultiLine) property can be set to `false` to activate single-line mode.

When that property is set to `false`, these properties are also automatically adjusted:

- [AcceptsTab](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.AcceptsTab) is set to `false`.
- [IsOutliningMarginVisible](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsOutliningMarginVisible) is set to `false`.
- [IsSelectionMarginVisible](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsSelectionMarginVisible) is set to `false`.
- [InactiveSelectedTextBackground](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.InactiveSelectedTextBackground) is set to null.
