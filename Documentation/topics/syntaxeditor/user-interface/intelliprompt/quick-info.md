---
title: "Quick Info"
page-title: "Quick Info - SyntaxEditor IntelliPrompt Features"
order: 3
---
# Quick Info

IntelliPrompt quick info displays helpful popup hints about what is under the mouse or next to the caret.  Tip content can include any @@PlatformName control and it's easy to generate richly-formatted content using an HTML-like syntax.

## Creating and Opening a Session

Creating and opening a quick info session is very easy.  There are only a few simple steps to follow.

### Creating a Session

The first step is to create a [QuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoSession):

```csharp
QuickInfoSession session = new QuickInfoSession();
```

### Setting the Content to Display

Next, the content that will be displayed must be specified.  Since a `ContentProvider` is used behind the scenes, this could be any object.  However it is recommended that you use a `UIElement` or the output from one of the [Content Providers](popup-content-providers.md).

This code uses the output from a [PlainTextContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.PlainTextContentProvider) as the content to display since it provides a word wrapping `TextBlock` containing a string:

```csharp
session.Content = new PlainTextContentProvider("Text here").GetContent();
```

### Open the Session

The final step is to open the session:

```csharp
session.Open(editor.ActiveView, new TextRange(editor.Caret.Offset));
```

The [IQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession) interface defines an [Open](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.Open*) method overload that can display the popup against a particular target and location.

For most cases, once the session is opened, the built-in functionality handles all the rest for you, such as when to dismiss the popup.

## Using Rich Formatting for Content

As mentioned above, the built-in content providers are a great way to easily populate quick info content.  The [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) in particular is nice since it uses a mini-HTML markup to style the text content.

It is highly recommended to make use of the [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) and all of its features that are described in detail in the [Content Providers](popup-content-providers.md) topic.

@if (wpf) {

## Handling Link Clicks

When links are present in quick info tip content, the [IQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession).[RequestNavigate](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.RequestNavigate) event will fire if a link is clicked, requesting that you take some action.

}

## Maximum Width

The [IQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession).[MaxWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.MaxWidth) property can be set to specify a maximum width for the popup.

## Control Key Down Opacity

The quick info popup animates to be semi-transparent when the `Ctrl` key is held down, thereby allowing the end user to see the text behind it.  The [IQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession).[ControlKeyDownOpacity](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoSession.ControlKeyDownOpacity) property specifies the opacity when the popup is semi-transparent.

Set this property to `1.0` to prevent the list from being semi-transparent.

## Automatic Quick Info for a Language (Initiated by Mouse Hovers, etc.)

Syntax languages are fully capable of listening for mouse hovers and opening a quick info session in response to hovers over various tokens, or even any other part of the editor.

The [IntelliPrompt Quick Info Provider](../../language-creation/provider-services/quick-info-provider.md) topic describes a special language service that can be implemented to respond to mouse hovers and show a quick info popup.

## Programmatically Requesting Quick Info

Sometimes it is desirable to programmatically request that quick info be displayed if the editor caret is in a location at which the language is capable of displaying quick info.

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[IntelliPrompt](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.IntelliPrompt).[RequestQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestQuickInfoSession*) method can be called to request quick info display, if the current language allows it.

## Built-In Quick Info Providers

There are built in Quick Info providers for collapsed node text (used with outlining) and squiggle tags.

When using the [code outlining](../../user-interface/outlining/outlining-general.md) features, sometimes the end user will collapse outlining nodes. By default, nothing happens when the mouse is hovered over a collapsed outlining node adornment (normally a box with "..." in it). The built-in [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) implementation called [CollapsedRegionQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CollapsedRegionQuickInfoProvider) watches for mouse hovers over collapsed regions and will automatically show a quick info tip with the contained content on mouse hovers.

When using the [parsing](../../text-parsing/parsing/index.md) framework and the [ParseErrorTagger](xref:ActiproSoftware.Text.Tagging.Implementation.ParseErrorTagger) (or other custom tagger that uses [SquiggleTag](xref:ActiproSoftware.Text.Tagging.Implementation.SquiggleTag) tags), there will be squiggle adornments in the document window. By default, nothing happens when the mouse is hovered over these adornments. However, there is a built-in [IQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IQuickInfoProvider) implementation called [SquiggleTagQuickInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.SquiggleTagQuickInfoProvider) that watches for mouse hovers over squiggle adorned regions and will automatically show a quick info tip with the parse error description on mouse hovers.

For additional information on these built-in Quick Info providers, visit the [IntelliPrompt Quick Info Provider](../../language-creation/provider-services/quick-info-provider.md) topic.
