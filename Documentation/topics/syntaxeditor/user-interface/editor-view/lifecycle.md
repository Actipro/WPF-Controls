---
title: "View Lifecycle"
page-title: "View Lifecycle - SyntaxEditor Editor View Features"
order: 25
---
# View Lifecycle

SyntaxEditor views can be opened and closed via various end user actions.  This topic discussed how to be notified when views are opened and closed.

## Editor View Lifecycle

Editor views, represented by the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) interface, are the interactive views that end users type in.  There always is one active editor view in a SyntaxEditor instance, retrieved with the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ActiveView](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ActiveView) property.

Via the use of the [Splitting](splitting.md) feature, additional editor views can be opened and closed with in a SyntaxEditor.

## Printer View Lifecycle

Printer views, represented by the [IPrinterView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IPrinterView) interface, are quickly opened and immediately closed when building printable pages.

See the [Printing Features](../printing/index.md) topic for more information on printing editor contents.

## SyntaxEditor Events

Whenever a view is opened, the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ViewOpened](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ViewOpened) event is raised.

Likewise, a corresponding [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ViewClosed](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ViewClosed) is raised when a view is closed.

## Language Event Sinks

Objects implementing the [ITextViewLifecycleEventSink](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLifecycleEventSink) interface can be registered as a service on a syntax language.  This allows the objects to be notified whenever a view that uses the language is attached or detached.

See the [Event Sink Services](../../language-creation/event-sinks.md) topic for more information on how to register and implement event sinks.
