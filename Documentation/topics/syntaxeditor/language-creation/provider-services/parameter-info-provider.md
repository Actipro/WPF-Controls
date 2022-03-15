---
title: "IntelliPrompt Parameter Info Provider"
page-title: "IntelliPrompt Parameter Info Provider - Provider Services - SyntaxEditor Language Creation Guide"
order: 9
---
# IntelliPrompt Parameter Info Provider

Languages can choose to support [IntelliPrompt parameter info](../../user-interface/intelliprompt/parameter-info.md) popups that are automatically displayed when the end user types an invocation, and can provide additional information about the current parameter.

## Basic Concepts

The concepts described below are overviews of how to implement a parameter info provider.  Please pair this information with the QuickStarts in the Sample Browser for IntelliPrompt Parameter Info and the Getting Started series sample that implements parameter info to see fully-implemented examples.

### Requesting a Session

The [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider) interface defines a single method: [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider.RequestSession*).  This method has a parameter that indicates the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) in which to open the session.

[RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider.RequestSession*) can be called directly in your code if you wish to generically initiate a parameter info session, assuming the language supports it.  A common usage of this is to have the provider implement the [IEditorDocumentTextChangeEventSink](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorDocumentTextChangeEventSink) interface and watch for typing events where the user enters `(` or `,` characters.

Implementors of the [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider.RequestSession*) method should use code to examine the context of the caret in the view.  This may involve token scanning and/or AST examination if available, and the resulting context should indicate if the caret is in an invocation of some sort, such as a method call.  If it is, the argument index and argument list's start offset should also be specified in the context data, as this information is important for the tracking of parameter info sessions.

Assuming that the context indicates that the caret is within an invocation, the provider should gather information about the invoked member (including overloads when appropriate), create a parameter info session, and add those as signature items to the session being created, as described in the [IntelliPrompt parameter info](../../user-interface/intelliprompt/parameter-info.md) topic.  Finally, the session should be opened with the argument list start offset used as the text range.

### Watching the Caret

The next task a parameter info provider needs to do is watch the caret to determine if it moves to another argument index, nested invocation, or outside of a valid invocation range altogether.  This can be achieved by having the parameter info provider implement the [IEditorViewSelectionChangeEventSink](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelectionChangeEventSink) interface.

In the selection change notification, if there is a parameter info session already open, a new context object should be retrieved (similar to the context retrieval when requesting a session) for the caret's location.  If the argument list's start offset has changed, the open session should be closed and a request for a new session should be made in the event the caret has moved into a nested invocation.

### Bolding the Current Parameter

If the content providers used in the session's signature items implement the [IParameterizedContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterizedContentProvider) interface, the current parameter index can be specified to them.  This allows them to alter the content that they display so that the current parameter has attention drawn to it, such as by a boldface font.

In this scenario, a method should be added to the parameter info provider that can examine a context and enumerate through a session's signature items to update the [IParameterizedContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterizedContentProvider).[ParameterIndex](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterizedContentProvider.ParameterIndex) property of each item's content provider, based on which argument index the context indicates that the caret is currently in.  If the session is already open and any updates are made to the current argument index, the [IParameterInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession).[Refresh](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoSession.Refresh*) method should be called to force an update to the displayed content.

The method to update the current parameters should be called in two places.  First is immediately before the a session is first opened, in [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider.RequestSession*).  Second is in the selection change handler, if it is detected that the caret is still within the same argument list.  This can be determined by if the context's argument list start offset matches the currently-open session's snapshot range start offset.

## The ParameterInfoProviderBase Base Class

The easiest way to implement an [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider) for a language is to inherit the [ParameterInfoProviderBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.ParameterInfoProviderBase) class.  Then just override its abstract [RequestSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.ParameterInfoProviderBase.RequestSession*) method.

## Registering with a Language

Any object that implements [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider) can be associated with a syntax language by registering it as an [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider) service on the language.

This code registers a parameter info provider in the `parameterInfoProvider` variable with the syntax language that is already declared in the `language` variable:

```csharp
language.RegisterService(parameterInfoProvider);
```

## Ordering Providers

The [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider) interface inherits the [IOrderable](xref:ActiproSoftware.Text.Utility.IOrderable) interface.  When multiple language services are registered that implement [IParameterInfoProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IParameterInfoProvider), they can be ordered.  The highest priority provider will be used first.  If it indicates that a session was opened, no other providers will be used.  If it indicates that a session was not opened, the next provider is checked, and so on.  This allows for layering of multiple providers.
