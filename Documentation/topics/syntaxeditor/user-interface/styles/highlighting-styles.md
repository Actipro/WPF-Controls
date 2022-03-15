---
title: "Highlighting Styles"
page-title: "Highlighting Styles - SyntaxEditor Theme and Highlighting Style Features"
order: 2
---
# Highlighting Styles

Highlighting styles store information about how text should be rendered within a SyntaxEditor, including fore/background, font styles, and more.

Styles have the capability of altering these formatting options:

- Foreground
- Background
- Border
- Bold
- Italic
- Font family
- Font size
- Strike-through
- Underline

## Basic Concepts

### Interface and Implementation

Highlighting styles are instances of the [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) interface.  The [HighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyle) class implements this interface and provides for the easiest way to create highlighting styles.

This code creates a simple highlighting style with a foreground color of maroon:

```csharp
IHighlightingStyle style = new HighlightingStyle(Colors.Maroon);
```

### Default Values and Inheritance

All the properties in [HighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyle) are initially set to "default" values.  These default values are set up to not change formatting in any way.

In the code example above however, we have assigned a `Maroon` foreground `Color` to the style.  This means that the style defined above will only change the foreground of text that it is applied against.  It will not alter the bold weight, font size, etc.

There are scenarios in SyntaxEditor where two or more styles may be merged together to create resolved formatting options.  For instance, if the special [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider).[PlainText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.PlainText) classification type is used (see the [Highlighting Style Registries](highlighting-style-registries.md) topic for more information), then the style associated with that classification type is the default style for the text area.  Classifications made by an [ITagger<IClassificationTag>](../../text-parsing/tagging/taggers.md) may apply their own styles to specific ranges of text.  SyntaxEditor resolves the two styles together to get resolved formatting options for the text in the editor.

During the resolution process, any non-default property settings on the higher-priority style will be applied and will override any property settings on the lower-priority style.  If the higher-priority style doesn't set a property value but the lower-priority style does, the lower-priority style's value will be used, thereby establishing inheritance.

## Relationship Between Classifications and Styles

[Taggers](../../text-parsing/tagging/taggers.md) are objects that provide data (in the form of tags) over ranges of text.  One such tag is the [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag), which is a tag that indicates an [IClassificationType](xref:ActiproSoftware.Text.IClassificationType).  Thus these tags can provide logical classifications of text ranges.  All this functionality is built into the [text/parsing framework](../../text-parsing/index.md).

Highlighting styles on the other hand purely deal with the user interface and how text is formatted.

There is a strong relationship that ties together classifications and highlighting styles, and that tie is made via [highlighting style registries](highlighting-style-registries.md).  These registries provide a mapping from classification types to highlighting styles.

As a SyntaxEditor prepares to draw text, it examines the logical classifications that have been on the text.  Then it uses [highlighting style registries](highlighting-style-registries.md) to determine which highlighting styles apply.  After that, it resolves styles (see above) and returns a list of resolved styles to the code that renders the text.

## Formatting Options

Highlighting styles have a lot of options for changing the formatting of text.

### Foreground and Background

The [Foreground](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Foreground) and [Background](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Background) properties are used to designate nullable `Color` objects that should be used to render the foreground and background of text.

It is recommended to generally use `Color` instances that have no transparency.

A null reference value is considered the "default" value and will not alter formatting.

### Background Spans Virtual Whitespace

The [BackgroundSpansVirtualSpace](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BackgroundSpansVirtualSpace) property specifies whether the background should span virtual space when the styled range includes a line terminator.  When set to `true`, the background will be rendered to the right edge of the view, thereby covering virtual space.

It is a `Boolean` type, where the default is `false`.  Since backgrounds are rendered in a layered fashion, a null reference value is not needed for this property.

### Bold and Italic

The [Bold](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Bold) and [Italic](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Italic) properties are used to designate whether the text should be bold and/or italic.

It is a nullable `Boolean` type, where a null reference value is considered the "default" value and will not alter formatting.

### Border

Optional borders can be applied to text via highlighting styles.  The [BorderKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderKind) property can be used to indicate the style (solid, dotted, etc.) to use for the border.

The [BorderColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderColor) property specifies the color to use for rendering the border.  If it is not specified, then the foreground is used.

Borders render with square corners by default but the [BorderCornerKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderCornerKind) property can be set to change to rounded corners instead.

### Font Family

The [FontFamilyName](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.FontFamilyName) property can be used to specify the font family name in which to render text.

A null reference value is considered the "default" value and will not alter formatting.

### Font Size

The [FontSize](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.FontSize) property can be used to specify the font size (in points) in which to render text.

A value of `0.0` is considered the "default" value and will not alter formatting.

### Strike-through

Properties like [StrikethroughKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughKind)@if (wpf winforms) {, [StrikethroughColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughColor), and [StrikethroughWeight](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughWeight)} can be used to designate how a text strike-through should render.

The default value is [LineKind](xref:@ActiproUIRoot.Controls.Rendering.LineKind).`None`, which will not render a strike-through line.

### Underline

Properties like [UnderlineKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineKind)@if (wpf winforms) {, [UnderlineColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineColor), and [UnderlineWeight](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineWeight)} can be used to designate how a text underline should render.

The default value is [LineKind](xref:@ActiproUIRoot.Controls.Rendering.LineKind).`None`, which will not render an underline.

## Editable Properties

SyntaxEditor highlighting styles are designed to be editable by the end user via an options dialog.  Therefore, styles include several options for designating which of the most common properties may be edited by the end user.

The [IsForegroundEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsForegroundEditable), [IsBackgroundEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsBackgroundEditable), [IsBoldEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsBoldEditable), [IsBorderEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsBorderEditable), and [IsItalicEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsItalicEditable) properties all indicate which of the related properties are editable.  By default, all properties except for the border one are editable.

There are a number of cases however where certain properties don't make sense to be editable.  As an example, the style for the special [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider).[CollapsibleRegion](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CollapsibleRegion) shouldn't allow bold or italic to be editable.  That style only needs its foreground and background to be editable.
