---
title: "Event Sink Services"
page-title: "Event Sink Services - SyntaxEditor Language Creation Guide"
order: 8
---
# Event Sink Services

Event sinks are interfaces that allow a language to be notified of events that occur to a [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor) or [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument).  This could be anything from input events to text change events.  The language can choose to process these events and optionally block editor views from receiving them.

Event sinks use the service locator architecture and thus are managed as services.  This design has several benefits:

- Event notifications can be prioritized (IntelliPrompt sessions receive events before languages, etc.)
- Custom event sink handlers can be externally swapped in without inheriting a language class
- There are no extra ties between a SyntaxEditor, IEditorDocument, or ISyntaxLanguage meaning less chance for memory leaks
- Languages continue to use basic .NET 2.0-based techniques for event handling, keeping their code platform independent

> [!NOTE]
> The [Service Locator Architecture](service-locator-architecture.md) topic contains a lot of information that is necessary to understand when working with event sinks.

## Built-In Event Sink Service Types

This table lists the built-in event sink service types that can be used with [ISyntaxLanguage](xref:ActiproSoftware.Text.ISyntaxLanguage).  Note that multiple objects implementing these interfaces can be registered as language services, as long as they are registered using separate `Type` keys.

| Service Type | Description |
|-----|-----|
| [IActiveEditorViewChangeEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IActiveEditorViewChangeEventSink) | An object that can be notified of [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[ActiveView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.ActiveView) change events. |
| [ICodeDocumentLifecycleEventSink](xref:ActiproSoftware.Text.ICodeDocumentLifecycleEventSink) | An object that can be notified of when an [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) becomes associated and disassociated with a language, via events such as a language change. |
| [ICodeDocumentPropertyChangeEventSink](xref:ActiproSoftware.Text.ICodeDocumentPropertyChangeEventSink) | An object that can be notified of when attached [ICodeDocument](xref:ActiproSoftware.Text.ICodeDocument) properties, such as [ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) or [FileName](xref:ActiproSoftware.Text.ITextDocument.FileName), are changed. |
| [ICodeSnippetTemplateSessionEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.ICodeSnippetTemplateSessionEventSink) | An object that can be notified of various events occur related to a code snippet template session.  See the [Code Snippets](../user-interface/intelliprompt/code-snippets.md) documentation for more information. |
| [IEditorDocumentTextChangeEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorDocumentTextChangeEventSink) | An object that can be notified of [IEditorDocument](xref:ActiproSoftware.Text.IEditorDocument) text change events, both before and after a text change occurs. |
| [IEditorViewKeyInputEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewKeyInputEventSink) | An object that can be notified of [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView) keyboard input events, both key down and up.  These input events can be intercepted before the editor view ever receives them. |
| [IEditorViewPointerInputEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewPointerInputEventSink) | An object that can be notified of [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView) pointer input events, including all standard mouse events.  These input events can be intercepted before the editor view ever receives them. |
| [IEditorViewSelectionChangeEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewSelectionChangeEventSink) | An object that can be notified of [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView) selection change events. |
| [IEditorViewTextInputEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorViewTextInputEventSink) | An object that can be notified of [IEditorView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorView) text input events.  These input events can be intercepted before the editor view ever receives them. |
| [ITextViewLifecycleEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextViewLifecycleEventSink) | An object that can be notified of when an [ITextView](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.ITextView) becomes associated and disassociated with a language, via events such as view open/close, document change, or language change. |

## Sample: Implementing an Event Sink for Listening to Text Changes

For this sample, assume that you are implementing an HTML language and wish to watch for `<` being typed so that you know when to show a [completion list](../user-interface/intelliprompt/completion-list.md) of HTML tags.  To do this, we'll work with the [IEditorDocumentTextChangeEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorDocumentTextChangeEventSink), which notifies after text changes occur.  We examine typed text to look for `<` characters.

### Registering the Event Sink

As mentioned above, event sinks use the [service locator architecture](service-locator-architecture.md).  In this sample, we'll have our language class itself implement the [IEditorDocumentTextChangeEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorDocumentTextChangeEventSink) interface and in the language's constructor, we'll register itself as a service for that service type.

This code registers the language itself as an [IEditorDocumentTextChangeEventSink](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IEditorDocumentTextChangeEventSink) service:

```csharp
public class HtmlSyntaxLanguage : SyntaxLanguage, IEditorDocumentTextChangeEventSink }
	public HtmlSyntaxLanguage() : base("HTML") {
		// Other initialization here
		this.RegisterService<IEditorDocumentTextChangeEventSink>(this);
	}
}
```

> [!NOTE]
> See the [Service Locator Architecture](service-locator-architecture.md) topic for details on registering and retrieving various service object instances.

### Implementing the IEditorDocumentTextChangeEventSink Interface

Next we implement the interface on the language.  Again, we could have implemented this interface on any other object and registered that instead but for simplicity, we have chosen to implement it on the language itself.

This code shows the two methods on the interface, added into the language class:

```csharp
void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanged(SyntaxEditor editor, EditorSnapshotChangedEventArgs e) {
	// The e.TypedText is not null only when a Typing change occurs with a single operation that inserts text,
	//   so we can check that to display the completion list when "<" is typed
	if (e.TypedText == "<") {
		// Show completion list containing tags here
	}
}

void IEditorDocumentTextChangeEventSink.NotifyDocumentTextChanging(SyntaxEditor editor, EditorSnapshotChangingEventArgs e) {
	// Nothing needs to be done here
}
```

Note that in the text changed handler, we look to see if text that was typed was the `<` character.  If that scenario occurred, we'd call code to display a completion list.

Of course we may also wish to add code to ensure we didn't type within a comment, etc.  This logic could be added by getting an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) and examining the token that was typed.
