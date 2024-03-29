---
title: "Overview"
page-title: "SyntaxEditor Theme and Highlighting Style Features"
order: 1
---
# Overview

Highlighting styles govern how SyntaxEditor text is rendered and can be used to create entire themes for various languages.

## Highlighting Styles

Highlighting styles store information about how text should be rendered within a SyntaxEditor, including fore/background, font styles, and more.

See the [Highlighting Styles](highlighting-styles.md) topic for more information.

## Highlighting Style Registries

Highlighting style registries provide a mapping from classification types to highlighting styles.  They tell SyntaxEditor how the various ranges of classified text should appear within the text area.

See the [Highlighting Style Registries](highlighting-style-registries.md) topic for more information.

## TextStylePreview Control

The `TextStylePreview` control is a small control that is used on options dialogs to show the end user a preview of how an [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) will render.

See the [TextStylePreview Control](text-style-preview.md) topic for more information.

## Dark Themes

[SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) supports the ability to easily switch between light and dark themes.

All of the default styles registered by [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) and [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider) as well as the built-in language implementations are designed to support either light or dark themes.  However, any custom language implementations may require additional configuration to support a dark theme since most styles intended for use on a light background are not very appealing on a dark background.

See the [Dark Themes](dark-themes.md) topic for more information.
