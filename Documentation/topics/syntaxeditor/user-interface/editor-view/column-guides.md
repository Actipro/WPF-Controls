---
title: "Column Guides"
page-title: "Column Guides - SyntaxEditor Editor View Features"
order: 16
---
# Column Guides

Column guides are vertical lines typically aligned with a specific column but can also be displayed at an explicit location on the x-axis of the view.

Commonly, a column guide is used at a specific column (e.g., 80 or 120) to create a subtle line indicating the maximum desired length of a line of text.  Multiple column guides of varying colors, styles, and thicknesses are supported at the same time.

## Placement

A column guide supports the following types of [ColumnGuidePlacement](xref:@ActiproUIRoot.Controls.SyntaxEditor.ColumnGuidePlacement):

| Placement | Description |
|-----|-----|
| Column | The guide is placed at a specific column (e.g., 80). This guide is the most common and automatically adjusts to changes in font size but requires a consistent fixed-width font to accurately represent the column position throughout the document. |
| Absolute | The guide is always placed at a specific location on the x-axis relative to the start of the text line. This guide is always in the same relative position and is not dependent upon the font being used. |

> [!NOTE]
> All column guide placements are relative to the start of a text line and will move in response to horizontal scrolling.

## Setting Column Guides

By default, all column guides defined by the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ColumnGuides](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ColumnGuides) property are visible.  The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[AreColumnGuidesVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.AreColumnGuidesVisible) property can be set to `false` to hide column guides without removing them.

Basic column guides can easily be added using convenience methods on [IColumnGuideCollection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IColumnGuideCollection).  The following sample demonstrates how to add a default column guide at column 80:

```csharp
editor.ColumnGuides.Add(80);
```

To create more advanced column guides with non-default line styles and colors, create and add an instance of a class that implements [IColumnGuide](xref:@ActiproUIRoot.Controls.SyntaxEditor.IColumnGuide).  A default implementation of this interface is available as [ColumnGuide](xref:@ActiproUIRoot.Controls.SyntaxEditor.Implementation.ColumnGuide). The following example demonstrates adding a customized column guide at column 80 with a 2px wide [Dash](xref:@ActiproUIRoot.Controls.Rendering.LineKind.Dash) line:

```csharp
editor.ColumnGuides.Add(new ColumnGuide(ColumnGuidePlacement.Column, 80, LineKind.Dash, 2, null));
```

## Changing the Default Column Guide Brush

The brush used to render a column guide can use a color explicitly set using the [IColumnGuide](xref:@ActiproUIRoot.Controls.SyntaxEditor.IColumnGuide).[Color](xref:@ActiproUIRoot.Controls.SyntaxEditor.IColumnGuide.Color) property.  Otherwise, the brush can be adjusted by the end user since it is exposed via a special classification type's style in the highlighting style registry.

See the "Special Classification Types" section in the [Highlighting Style Registries](../styles/highlighting-style-registries.md) topic for more information on how to modify the style.
