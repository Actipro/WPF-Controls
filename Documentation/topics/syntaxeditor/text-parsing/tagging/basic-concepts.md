---
title: "Tags and Classification Types"
page-title: "Tags and Classification Types - Tagging - SyntaxEditor Text/Parsing Framework"
order: 2
---
# Tags and Classification Types

Tags are objects that store data and can be applied to a text range.  Classification types are used throughout the product to categorize objects.  Both are key pieces towards understanding tagging.

## What are Tags?

Tags are any object inheriting [ITag](xref:ActiproSoftware.Text.Tagging.ITag) that can contain some data and can be applied to and tracked with a range of text in a document.

There are several built-in tag types are used to drive various functionality pieces in the product.  This table lists the built-in tag interface types that are supported, along with optional implementation classes that can be used.

<table>
<thead>

<tr>
<th>Type</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag)

</td>
<td>

A tag that refers to an [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) and can be used to classify a range of text (see below), which when used in an editor can also drive syntax highlighting.

The [ClassificationTag](xref:ActiproSoftware.Text.Tagging.Implementation.ClassificationTag) class implements this tag interface.

</td>
</tr>

<tr>
<td>

[IReadOnlyRegionTag](xref:ActiproSoftware.Text.Tagging.IReadOnlyRegionTag)

</td>
<td>

A tag that can mark a range of text as read-only (uneditable) by the end user.  This is similar to setting the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[IsReadOnly](xref:ActiproSoftware.Text.ITextDocument.IsReadOnly) property to `true`, except that the read-only state only applies to the tagged text ranges.

Normally, only text changes that overlap (cross) the read-only region will be blocked.  The [IReadOnlyRegionTag](xref:ActiproSoftware.Text.Tagging.IReadOnlyRegionTag).[IncludeFirstEdge](xref:ActiproSoftware.Text.Tagging.IReadOnlyRegionTag.IncludeFirstEdge) property can be set to `true` to also block text changes that border on the leading (first) edge of the read-only region.  This is ideal for when you want to prevent text changes at the very start of a document.  Likewise the [IReadOnlyRegionTag](xref:ActiproSoftware.Text.Tagging.IReadOnlyRegionTag).[IncludeLastEdge](xref:ActiproSoftware.Text.Tagging.IReadOnlyRegionTag.IncludeLastEdge) property can be set to `true` to block text changes that border on the trailing (last) edge of the read-only region.  This is ideal for when you want to prevent text changes at the very end of a document.

The [ReadOnlyRegionTag](xref:ActiproSoftware.Text.Tagging.Implementation.ReadOnlyRegionTag) class implements this tag interface and also includes an [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) reference, which defaults to [ClassificationTypes](xref:ActiproSoftware.Text.ClassificationTypes).[ReadOnlyRegion](xref:ActiproSoftware.Text.ClassificationTypes.ReadOnlyRegion).  If that classification type is registered with a style in the [ambient highlighting style registry](../../user-interface/styles/highlighting-style-registries.md), it is possible for the read-only regions to render in an alternate appearance, such as with a gray background.

</td>
</tr>

<tr>
<td>

[ISquiggleTag](xref:ActiproSoftware.Text.Tagging.ISquiggleTag)

</td>
<td>

A tag that may be available when the text/parsing framework is used in conjunction with an editor.  In that case, the tag can mark a range of text as needing a squiggle underline.  This is generally used to flag syntax errors, etc.  The tag includes an [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) reference so that the type of squiggle can be classified.

The [SquiggleTag](xref:ActiproSoftware.Text.Tagging.Implementation.SquiggleTag) class implements this tag interface.

</td>
</tr>

<tr>
<td>

[ITokenTag](xref:ActiproSoftware.Text.Tagging.ITokenTag)

</td>
<td>

A tag that refers to an [IToken](xref:ActiproSoftware.Text.Lexing.IToken) and can be used to mark a range of text with the token.  Use of these tags allow [snapshot readers](../core-text/scanning-text.md) to scan through a document by token.

Token tags are typically handled completely internally by the text/parsing framework via the use of [token taggers](taggers.md).

</td>
</tr>

<tr>
<td>

[IUnusedRegionTag](xref:ActiproSoftware.Text.Tagging.IUnusedRegionTag)

</td>
<td>

A tag that indicates a text range is unused, such as when the code can safely be removed or will never execute.

Text ranges tagged with this tag will render with their normal syntax highlighting colors, but with reduced opacity.  This feature is described in more detail in the [Syntax Highlighting](../../user-interface/adornment/syntax-highlighting.md) topic.

</td>
</tr>

</tbody>
</table>

## Classification Types

The [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) interface provides information about classification types.  A classification type is a high-level categorization, such as `Identifier` or `Comment`, that can be applied to text ranges via tags.

Each [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) defines a string-based `Key` that identifies it, along with a more descriptive [Description](xref:ActiproSoftware.Text.IClassificationType.Description) that may be used in user interfaces.

> [!NOTE]
> Several of the most common classification types are available via static properties on the [ClassificationTypes](xref:ActiproSoftware.Text.ClassificationTypes) class.

The reuse of [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) instances is highly recommended.

## Tokens vs. Classifications

[Tokens](../lexing/tokens.md) and classification tags are often related, especially in the case of token taggers.  Let us examine an example.

In a C# document, consider a token that has been created via lexing for the keyword `foreach`.  Syntax languages (advanced ones at least) generally assign different token ID values for each individual keyword.  So the token ID for keyword `foreach` could be `50` for instance.  This value is likely defined in some enumeration or static class of integer constants for the C# language.  By doing this, when we later scan the tokens in the document, we know that this particular token is a `foreach` token without having to look at its text.

Now assume that a classification tag has been applied over the same text range via a [token tagger](taggers.md).  This classification would apply a `Keyword` classification type over that text range.

The difference is that where a token can be very specific as to what it represents, a classification tag over the same range is usually much more general.  When the text/parsing framework is used within an editor control such as SyntaxEditor, classification tags are what drive syntax highlighting.  In that scenario, each classification type is associated with a highlighting style.  Therefore when ranges of text are classified with a classification type, SyntaxEditor maps the classification type to its related highlighting style and renders the text in the range using that style.

Even though token taggers most often provide both [ITokenTag](xref:ActiproSoftware.Text.Tagging.ITokenTag) and [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) instances, additional [IClassificationTag](xref:ActiproSoftware.Text.Tagging.IClassificationTag) taggers can be layered on top of a token tagger to override and reclassify text.  When used in an editor, this allows for more customized syntax highlighting to be added that merges with the default syntax highlighting.

## Providing Tagged Ranges

Taggers are objects that can return an enumerable of tagged snapshot ranges that overlap a requested set of snapshot ranges.  They are described in detail in the [Taggers](taggers.md) topic, along with details on special token taggers.
