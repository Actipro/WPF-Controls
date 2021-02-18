---
title: "Parameter Info"
page-title: "Parameter Info - SyntaxEditor IntelliPrompt Features"
order: 5
---
# Parameter Info

IntelliPrompt parameter info displays helpful popup hints about an invocation that is being typed, and its parameters.  Tip content can include any WPF control and it's easy to generate richly-formatted content using an HTML-like syntax.

## Creating and Opening a Session

Creating and opening a parameter info session is very easy.  There are only a few simple steps to follow.

### Creating a Session

The first step is to create a [ParameterInfoSession](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.ParameterInfoSession):

```csharp
ParameterInfoSession session = new ParameterInfoSession();
```

### Setting the Content to Display

Next, the content that will be displayed must be specified.  Parameter info can display one or more overloads of an invocation.  When multiple overloads are specified, built-in UI is added to show up and down arrows for switching the selected overload.

Each item in the parameter info is an [ISignatureItem](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.ISignatureItem) instance, that are implemented by the [SignatureItem](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.SignatureItem) class.  This interface consists of a [Content Providers](popup-content-providers.md) and an optional data object.

This code adds a couple method overloads using a [PlainTextContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.PlainTextContentProvider) for the displayed content of each item:

```csharp
session.Items.Add(new SignatureItem(new PlainTextContentProvider("void Foo()"));
session.Items.Add(new SignatureItem(new PlainTextContentProvider("void Foo(int a)"));
```

### Open the Session

The final step is to open the session:

```csharp
session.Open(editor.ActiveView, new TextRange(editor.Caret.Offset));
```

The above code is simplistic and will display a parameter info session however there is more code required for managing a session and knowing when to switch to a new session (in cases where the caret moves into a nested invocation), supporting the bolding of the current parameter, etc.  These concepts are described further below.

## Managing Parameter Info Sessions

While it's easy to create and display a parameter info session, there is some more advanced work needed to actually manage the session so that you know when it should be altered (such as bolding a new parameter) or closed.  The management of the session is performed by its calling parameter info provider.

See the [Parameter Info Provider](../../language-creation/provider-services/parameter-info-provider.md) topic for details on how to manage a parameter info session.

## Using Rich Formatting for Content

As mentioned above, the built-in content providers are a great way to easily populate quick info content.  The [HtmlContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) in particular is nice since it uses a mini-HTML markup to style the text content.

It is highly recommended to make use of the [HtmlContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) and all of its features that are described in detail in the [Content Providers](popup-content-providers.md) topic.

## Bolding the Current Parameter

When implementing a content provider for an [ISignatureItem](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.ISignatureItem), an [IParameterizedContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterizedContentProvider) interface can be implemented on the [IContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IContentProvider) to add support for the designation of the current parameter.

The [IParameterizedContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterizedContentProvider) interface defines a [ParameterIndex](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterizedContentProvider.ParameterIndex) property that is a nullable integer, indicating the current parameter index, if known.  The content provider implementation can examine that property and provide some method of focusing the end user's attention on it, such as bolding the parameter name if a [HtmlContentProvider](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) is used behind the scenes.

The parameter index can be set by code described in the Managing Parameter Info Sessions topic above.

## Handling Link Clicks

When links are present in quick info tip content, the [IParameterInfoSession](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession).[RequestNavigate](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession.RequestNavigate) event will fire if a link is clicked, requesting that you take some action.

## Maximum Width

The [IParameterInfoSession](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession).[MaxWidth](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession.MaxWidth) property can be set to specify a maximum width for the popup.

## Arrow Key Selection

When two or more signatures are displayed in a parameter info tip, arrow buttons appear in the tip's UI that allow for toggling between which one is selected.  The [IParameterInfoSession](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession).[IsArrowKeySelectionEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession.IsArrowKeySelectionEnabled) property determines whether the up and down arrow keys also can toggle between the selected signature.

## Programmatically Requesting Parameter Info

Sometimes it is desirable to programmatically request that parameter info be displayed if the editor caret is in a location at which the language is capable of displaying parameter info.

The [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView).[IntelliPrompt](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView.IntelliPrompt).[RequestParameterInfoSession](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestParameterInfoSession*) method can be called to request quick info display, if the current language allows it.
