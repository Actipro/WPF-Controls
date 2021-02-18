---
title: "Word Wrap"
page-title: "Word Wrap - SyntaxEditor Editor View Features"
order: 9
---
# Word Wrap

Word wrapping is a powerful feature that  allows users to view all text for a line by wrapping text that normally would have been outside the view horizontally to one or more view lines in the editor.

## Word Wrap Modes

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[WordWrapMode](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.WordWrapMode) property can be set to `WordWrapMode.Word` to activate word wrap functionality.  By default it is set to `WordWrapMode.None`, meaning no word wrap.

![Screenshot](../../images/word-wrap.png)

*Word wrap active in the editor, with the glyphs on the right indicating wrapped lines*

This code activates word wrap:

```csharp
editor.WordWrapMode = WordWrapMode.Word;
```

## Word Wrap Glyph Margin

Word wrap glyphs can be displayed on the right side of view lines in a special margin that indicate soft line breaks (wrapped lines).

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[AreWordWrapGlyphsVisible](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.AreWordWrapGlyphsVisible) property can be set to `true` to activate this functionality.
