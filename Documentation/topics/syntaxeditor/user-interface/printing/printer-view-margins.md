---
title: "Printer View Margins"
page-title: "Printer View Margins - SyntaxEditor Printing Features"
order: 4
---
# Printer View Margins

While the SyntaxEditor printer views have numerous built-in margins (document title, page number, etc.), SyntaxEditor offers an extensibility point where custom margins can be created and added to printer views in any location.

Printer view margins all implement the [IPrinterViewMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMargin) interface.

## Margin Locations

Printer view margins surround the text area and can be displayed in one of four placement locations (left, top, right, bottom).

The [IPrinterViewMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMargin).[Placement](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMargin.Placement) property has an [PrinterViewMarginPlacement](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.PrinterViewMarginPlacement) enumeration value that indicates one of those four placement locations.

Since each [IPrinterViewMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMargin) has a string-based `Key`, and the interface also implements [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable), it has an [Orderings](xref:ActiproSoftware.Text.Utility.IOrderable.Orderings) property that returns an enumerable of [Ordering](xref:ActiproSoftware.Text.Utility.Ordering) objects.  Each ordering indicates the string `Key` of another margin and whether this margin comes before or after that one.  When multiple margins are combined into a placement location, they are ordered based on the [Orderings](xref:ActiproSoftware.Text.Utility.IOrderable.Orderings) property values, where "after" means farther away from the text area.

## Built-In Margin Summary

These printer view margins are built-in to the product:

- Document title margin
- Line number margin
- Word wrap glyph margin
- Page number margin

## Document Title Margin

The document title margin, implemented by the [PrinterDocumentTitleMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.PrinterDocumentTitleMargin) class, is used to display the title of the document, which is specified in the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[DocumentTitle](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.DocumentTitle) property.  It appears in the top placement.

### Visibility

This margin is visible by default (if a document title is specified), and can be made invisible when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[IsDocumentTitleMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsDocumentTitleMarginVisible) property is `false`.

## Line Number Margin

The line number margin, implemented by the [PrinterLineNumberMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.PrinterLineNumberMargin) class, is used to display snapshot line numbers.  It appears in the left placement.

Word wrapped lines do not display any line numbers.

### Visibility

This margin is not visible by default, and can be made visible when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[IsLineNumberMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsLineNumberMarginVisible) property is `true`.

## Word Wrap Glyph Margin

The word wrap glyph margin, implemented by the [PrinterWordWrapGlyphMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.PrinterWordWrapGlyphMargin) class, is used to display glyphs at the end of lines that are too long to fit in the view and must be wrapped.  It appears in the right placement.

### Visibility

This margin is visible by default, and can be made invisible when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[IsWordWrapGlyphMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsWordWrapGlyphMarginVisible) property is `false`.

## Page Number Margin

The page number margin, implemented by the [PrinterPageNumberMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.PrinterPageNumberMargin) class, is used to display the current page number on each page@if (wpf) {, along with the total page count}.  It appears in the bottom placement.

### Visibility

This margin is visible by default, and can be made invisible when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[IsPageNumberMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsPageNumberMarginVisible) property is `false`.

## Margin Factories

Margin factories are used to tell SyntaxEditor which margins to display in a printer view.  Margin factories are implementations of the [IPrinterViewMarginFactory](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMarginFactory) interface.

The [DefaultPrinterViewMarginFactory](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.Implementation.DefaultPrinterViewMarginFactory) class is an implementation of a factory that creates all of the built-in margins.  While all the built-in margins are added if this factory is used, they still change their visibility based on the related [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings) margin visibility properties such as [IsDocumentTitleMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsDocumentTitleMarginVisible).

The [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[ViewMarginFactories](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.ViewMarginFactories) property is a collection of [IPrinterViewMarginFactory](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMarginFactory) objects.  By default, it is auto-populated with the [DefaultPrinterViewMarginFactory](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.Implementation.DefaultPrinterViewMarginFactory), meaning all built-in margins are available.  Add additional margin factories to the collection if you wish to add custom margins.  A QuickStart is available in the samples that shows how to do this.

## Custom Margins

Custom margins are pretty easy to create.  These are the basic steps involved:

1. Make a margin `Control` that implements [IPrinterViewMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMargin).
1. Make a custom [IPrinterViewMarginFactory](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IPrinterViewMarginFactory) that creates an instance of your margin class.
1. Add your custom margin factory to the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[ViewMarginFactories](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.ViewMarginFactories) collection.

At this point your margin should now be getting added to the view.

For details on implementing a custom margin including several complete examples, please see the related QuickStarts in the samples.
