---
title: "Print Options"
page-title: "Print Options - SyntaxEditor Printing Features"
order: 3
---
# Print Options

There are a large number of options available that affect how printouts render.

## Accessing Print Settings

The [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings) interface contains all the options that relate to printing.  The print settings are accessed via the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[PrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.PrintSettings) property.

## The Document Title

The document title can optionally be displayed at the top of each page in a printer view.  It is visible when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[DocumentTitle](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.DocumentTitle) property is non-`null` and the [IsDocumentTitleMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsDocumentTitleMarginVisible) property is `true`, which is the default.

This code sets the document title that is displayed at the top of each page:

```csharp
editor.PrintSettings.DocumentTitle = "My Document";
```

## Turning Off Syntax Highlighting

Syntax highlighting is on by default for printer views.  To turn it off and make text black and white, set the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[IsSyntaxHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsSyntaxHighlightingEnabled) property is set to `false`.

## Using a Custom Highlighting Style Registry

When syntax highlighting is on for printer views, it will use the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) that is currently active in the editor.  However, when the editor is using a dark theme, this is not a desirable registry to use for printouts.

A [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.HighlightingStyleRegistry) property can be set to a custom [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) instance that will be used for printer output only, and is ideally set up with darker foreground colors and no backgrounds.

See the [Highlighting Style Registries](../styles/highlighting-style-registries.md) topic for more information on highlighting style registries.

## Displaying Whitespace

Tabs and spaces can be visually displayed when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[IsWhitespaceVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsWhitespaceVisible) property is set to `true`.  The default is to not show whitespace.

## Allowing Collapsed Outlining Nodes

Collapsed outlining nodes are automatically expanded in printer views when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[AreCollapsedOutliningNodesAllowed](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.AreCollapsedOutliningNodesAllowed) property is set to `false`, which is the default value.  Set the property to `true` to allow currently-collapsed outlining nodes in the document to render as collapsed in the printer view.

## Displaying Column Guides

Column guides can be rendered when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[AreColumnGuidesVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.AreColumnGuidesVisible) property is set to `true`.  The default is to not show column guides.

## Displaying Indentation Guides

Indentation guides can be rendered when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[AreIndentationGuidesVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.AreIndentationGuidesVisible) property is set to `true`.  The default is to not show indentation guides.

## Displaying Squiggle Lines

Squiggle lines, such as those that mark syntax errors, can be rendered when the [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[AreSquiggleLinesVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.AreSquiggleLinesVisible) property is set to `true`.  The default is to not show squiggle lines.

@if (wpf) {

## Page Size and Margins

The [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings) interface has several properties that control the page size and margins.

[PageSize](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.PageSize) determines the `Size` of each page.  A standard 8.5" x 11" page would have values `{96 * 8.5, 96 * 11}`, where 1 inch = 96px in default resolution.

[PageMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.PageMargin) gives the `Thickness` between the page content and the page edge.  The default value is `96 * 0.75`, where 1 inch = 96px in default resolution.  This means the default margin is `0.75in`.

Various printer view margin visibility properties such as [IsDocumentTitleMarginVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.IsDocumentTitleMarginVisible) are included as well.  These properties are described in the [Printer View Margins](printer-view-margins.md) topic.

}

@if (wpf) {

## Creating a FixedDocument

The [IPrintSettings](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings).[CreateFixedDocument](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrintSettings.CreateFixedDocument*) method makes it simple to create a WPF `FixedDocument` that renders the contents of the SyntaxEditor, including applicable printer view margins.

This code creates a `FixedDocument` of the editor's content:

```csharp
FixedDocument document = editor.PrintSettings.CreateFixedDocument(editor);
```

This feature can be used to [print to XPS](xps.md).

}
