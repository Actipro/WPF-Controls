---
title: "AdvancedTextBlock"
page-title: "AdvancedTextBlock - Shared Library Controls"
order: 5
---
# AdvancedTextBlock

The [AdvancedTextBlock](xref:@ActiproUIRoot.Controls.AdvancedTextBlock) control can show a tooltip when overflowed, and can highlight spans of text based on captured text ranges (i.e., filter match results).

## Setting the Text

The [Text](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.Text) property specifies the text to render in the text block.  This property fully supports bindings.

## Overflow and ToolTips

The text block determines when it is overflowed, meaning not all of the text is visible within the control.  When this situation occurs, the [IsOverflowed](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.IsOverflowed) property returns `true`.

The [TextTrimming](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.TextTrimming) property, which defaults to `CharacterEllipsis`, indicates how the text block will visually trim overflow text.

A nice feature of the text block is that it will automatically set a tooltip on itself with its full text when overflowed.  This feature is on by default but can be disabled by setting the [HasToolTipOnOverflow](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.HasToolTipOnOverflow) property to `false`.

## Highlighting Captured (Matched) Text Ranges

A really powerful feature of the control is the ability to specify an ordered list of captured text ranges that should be highlighted.  These captures often come from filter match results.  The [Captures](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.Captures) property accepts an enumerable containing [StringFilterCapture](xref:@ActiproUIRoot.Data.Filtering.StringFilterCapture) results.  Each [StringFilterCapture](xref:@ActiproUIRoot.Data.Filtering.StringFilterCapture) is a pair of character index and length text ranges, and must be provided in non-overlapping ascending order.

When one or more captured text ranges are specified, those ranges will be highlighted within the text block.  The highlight will be performed in bold font weight by default.  The @if (wpf) {[HighlightBackground](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.HighlightBackground), }[HighlightFontWeight](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.HighlightFontWeight), and [HighlightForeground](xref:@ActiproUIRoot.Controls.AdvancedTextBlock.HighlightForeground) properties determine how the highlights render.
