---
title: "Syntax Highlighting"
page-title: "Syntax Highlighting - SyntaxEditor Adornment Features"
order: 3
---
# Syntax Highlighting

The document text rendered within a view is drawn using two adornment layers, one for the text itself and normal decorations (underlines, etc.), and one for any text backgrounds.  Classification taggers can be applied to a document (via language services) to override the default syntax highlighting provided by a language's lexer.  This adds support for features like changing the foreground color of certain words, marking search result ranges in a different background color, etc.

## Text Rendering Layers

Syntax highlighting is divided into two adornment layers, a foreground and a background.  A reference to the text foreground can be obtained via the [AdornmentLayerDefinitions](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.AdornmentLayerDefinitions).[TextForeground](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.AdornmentLayerDefinitions.TextForeground) property and a reference to the text background can be obtained via the [AdornmentLayerDefinitions](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.AdornmentLayerDefinitions).[TextBackground](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.Adornments.AdornmentLayerDefinitions.TextBackground) property.

The background is separated out so that the selection layer can overlay the text background when the selection extends to text ranges that have a background set.

> [!NOTE]
> The two text rendering layers cannot be returned via the [ITextView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView).[GetAdornmentLayer](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView.GetAdornmentLayer*) method.  If you wish to add custom adornments around text, you should make your own [adornment layer](adornment-layers.md) and order it relative to the built-in adornment layers.

## Lexer-Based Syntax Highlighting

Syntax languages almost always have a [lexer](../../text-parsing/lexing/index.md) in place that is able to tokenize text.  A [token tagger](../../text-parsing/tagging/taggers.md) can then be used to provide classification tag ranges for the lexer's tokens, where the classification tags indicate a logical [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) for the contained text.  The text rendering layers watch the classification tags that are provided by the token tagger.

Each [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) can be paired with a [highlighting style](../styles/highlighting-styles.md), via a [highlighting style registry](../styles/highlighting-style-registries.md).  Once there is a mapping between classification types and highlighting styles, the text rendering layers know how each classified range should be rendered.

Thus with the pieces above in place, lexer-based syntax highlighting is achieved.

## Overriding Syntax Highlighting

Sometimes it is desirable to override default lexer-based highlighting.  Instances of this could be where certain text ranges should be rendered in a specific foreground, such as when highlighting type names in C# code.  Or perhaps changing the background color of certain ranges to indicate breakpoints or find results.

As mentioned above, the text rendering layers already use a tag aggregator to watch for updates to [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) ranges.  When update occur, the text is repainted. [Classification taggers](../../text-parsing/tagging/taggers.md) (implementations of `ITagger<IClassificationTag>`) can be ordered.  It is very important that classification taggers are set to be ordered before the [TaggerKeys](xref:ActiproSoftware.Text.Tagging.TaggerKeys).[Token](xref:ActiproSoftware.Text.Tagging.TaggerKeys.Token) tagger, which is the key for the lexer-based syntax highlighting.  Otherwise the syntax highlighting derived from the classification tag ranges will appear under the lexer-based syntax highlighting, where it likely would not be seen.

Several QuickStarts show how to implement taggers that alter syntax highlighting.
