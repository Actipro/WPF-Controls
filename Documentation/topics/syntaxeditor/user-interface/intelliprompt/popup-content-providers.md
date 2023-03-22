---
title: "Content Providers"
page-title: "Content Providers - SyntaxEditor IntelliPrompt Features"
order: 7
---
# Content Providers

Content providers are classes that provide content on demand to IntelliPrompt sessions that display popups.  An example of their usage is providing content for quick info and completion item description tip popups.

## The IContentProvider Interface

All content providers implement the [IContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IContentProvider) interface.  There are several built-in provider implementations described below.

@if (winrt wpf) {

## The DirectContentProvider Class

The [DirectContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.DirectContentProvider) is the simplest way to provide content.  Its constructor takes an object instance and that is what is returned by the provider.

The downside to using this provider is that the content must be loaded prior to creation of the provider.  If a large number of providers are needed (such as for a completion list), then it is better to use another provider or build a custom one.

This code creates a [DirectContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.DirectContentProvider) using the `TextBlock` control that is already loaded in a `textBlock` variable:

```csharp
DirectContentProvider provider = new DirectContentProvider(textBlock);
```

}

## The PlainTextContentProvider Class

[PlainTextContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.PlainTextContentProvider) takes a `String` and renders it as plain text.

Behind the scenes a `TextBlock` with word wrap capabilities is created that renders the `String` value and the `TextBlock` is returned as the content.

## The HtmlContentProvider Class

The [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) is a really nice way to spruce up IntelliPrompt popups because it supports a mini-HTML markup format, allowing multiple fonts, colors, etc. to be used.

As with [PlainTextContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.PlainTextContentProvider), a word wrap capable `TextBlock` is created behind the scenes but here, the `String` passed into the provider is parsed for markup instead of being treated as plain text.

### Markup Tags

The markup text is specified using lowercase HTML-like formatting tags.  All tags must be well-formed must be in XML-compliant syntax.

These tags are supported:

<table>
<thead>

<tr>
<th>Tag</th>
<th>Description</th>
</tr>

</thead>
<tbody>

@if (wpf) {
<tr>
<td>

`a`

</td>
<td>

Creates a hyperlink over the enclosed text.  The `href` attribute indicates the target.  See the special notes for this tag below.

</td>
</tr>
}

<tr>
<td>

`b`

</td>
<td>Renders the enclosed text in a bold face.</td>
</tr>

<tr>
<td>

`br`

</td>
<td>Adds a line break.</td>
</tr>

<tr>
<td>

`em`

</td>
<td>Renders the enclosed text using emphasis.  The default style renders in italics.</td>
</tr>

<tr>
<td>

`i`

</td>
<td>Renders the enclosed text in an italic style.</td>
</tr>

<tr>
<td>

`img`

</td>
<td>

Renders an image.  The `src` specifies the image and the `align` tag controls alignment.  See the special notes for this tag below.

</td>
</tr>

<tr>
<td>

`span`

</td>
<td>

Changes the formatting of the enclosed text.  This tag should be used with the `style` attribute to specify formatting such as color, font, etc.

</td>
</tr>

<tr>
<td>

`strong`

</td>
<td>Renders the enclosed text using a strong face.  The default style renders in bold face.</td>
</tr>

<tr>
<td>

`u`

</td>
<td>Renders the enclosed text with an underline.</td>
</tr>

</tbody>
</table>

### The style Attribute

The `style` attribute can be applied to any tag listed above.  It accepts a subset of CSS (cascading style sheets) properites as input.

The supported properties are:

| Property | Description |
|-----|-----|
| `background-color` | The background color of the text.  This value can be specified using HTML format (e.g., `#RRGGBB`) or by referencing standard web colors by name (e.g., `Red`). |
| `color` | The foreground color of the text.  This value can be specified using HTML format (e.g., `#RRGGBB`) or by referencing standard web colors by name (e.g., `Red`). |
| `font-family` | The font family of the text.  This can be any valid font family name (e.g., `Tahoma`), even in quotes (e.g., `"Courier New"`).  It can also be one of the generic family names (`Monospace`, `Serif`, or `Sans-Serif`). |
| `font-size` | The font size of the text.  This is currently always specified in points (e.g., `10pt`). |
| `font-weight` | The font weight, or boldness, of the text.  These values map to a normal font: `normal`, `lighter`, `100`, `200`, `300`, `400`, `500`.  These values map to a bold font: `bold`, `bolder`, `600`, `700`, `800`, `900`. |
| `font-style` | The font style of the text.  The values `italic` and `oblique` map to an italic font.  The value `normal` map to a normal font. |
| `text-decoration` | The decoration for the text.  The value `none` uses no decoration.  The value `underline` draws a line under the text.  The value `line-through` draws a line through the middle of the text (strike-through). |

### Adding Horizontal Whitespace

You can use the `&nbsp;` character to add a non-breaking space to the text.  Use multiple `&nbsp;` characters in a row to create a horizontal spacer.

@if (wpf) {

### Working With the 'a' (Hyperlink) Tag

The `href` attribute of an `a` tag specifies the target of the hyperlink.  When the link is clicked, the IntelliPrompt session usually provides an event that is raised, such as [IQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession).[RequestNavigate](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.RequestNavigate).  A property in the event arguments indicates the HREF that was clicked so that you can take appropriate action.  One sample action would be to open a web browser to a URL passed in the HREF.

}

### Working With the 'img' (Image) Tag

Since there is no way to pass binary image data in the XML markup itself, the [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider).[GetImage](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider.GetImage*) method is called when an `img` tag is encountered.  The `src` attribute value is passed in and the method returns an `Image`.  This method must be overridden in a custom class that inherits [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider), otherwise a null reference is always returned.

The `align` attribute accepts values of `baseline` and `absbottom`.  The default is `baseline`.

### Specifying the Background Color

It is recommended that the [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider).[BackgroundColorHint](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider.BackgroundColorHint) property is set when the content will be used in [quick info](quick-info.md) or [parameter info](parameter-info.md) tips.  Both of those tips use the editor's syntax highlighting.  Since the editor's highlighting styles might not match the light/dark nature of the application's theme, supplying the background color hint from the [ITextView](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView).[DefaultBackgroundColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.DefaultBackgroundColor) value will ensure that the appropriate light or dark Metro image set is used for optimal rendering if Metro images are active via the [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider).[DefaultImageSet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider.DefaultImageSet) property.

### Syntax Example

This is an example of markup text that can be used with [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider):

```xml
<span style="font-size: 16pt; font-weight: bold;">Actipro SyntaxEditor</span>
<br/><i>The ultimate syntax-highlighting code editor control.</i>
```

### Escaping Markup Text

[HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) provides an XML escape method for ensuring that "plain text" can be used in it without having to worry about possible malformed XML errors.  Situations where this is useful are generally those where the text comes from an external data source that could have `&` or other XML control characters in the text.

The [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider).[Escape](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider.Escape*) static method will escape all XML control characters such as `<`, `>`, `&`, etc. and will also convert line feed characters to `<br />` tags.

This is an example of escaping the markup text that without an escape, would have raised a bad XML exception:

```csharp
string text = "Line 1 &" + Environment.NewLine + "Line 2";
text = HtmlContentProvider.Escape(text);
```

## Custom Content Providers

It's easy to write your own custom content provider if none of the implementations above work for your scenario.  Simply create a class that implements [IContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IContentProvider).

The [GetContent](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IContentProvider.GetContent*) method that must be implemented as part of that interface returns an object.  You could have it be pulled from a storage mechanism or could dynamically generate it.

Since the method is called on-demand, the content doesn't need to be loaded up front.  When creating a custom provider, the provider generally stores some object that contains data that should be described by the generated content.  Then in the [GetContent](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IContentProvider.GetContent*) method, the data object is examined in order to generate the appropriate content.

It is common to use the [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) to aid in dynamically generating content in custom content providers.  In this example of a [GetContent](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IContentProvider.GetContent*) implementation, there is a `Type` field in the custom content provider class called `targetType`.  A [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) is used to help provide nicely-formatted markup content describing the type.

```csharp
string htmlSnippet = String.Format("Type: <b>{0}</b>", 
	HtmlContentProvider.Escape(targetType.FullName));
return new HtmlContentProvider(htmlSnippet).GetContent();
```
