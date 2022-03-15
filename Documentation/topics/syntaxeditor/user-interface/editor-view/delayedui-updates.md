---
title: "Delayed UI Updates"
page-title: "Delayed UI Updates - SyntaxEditor Editor View Features"
order: 26
---
# Delayed UI Updates

SyntaxEditor includes a special event that is raised shortly after any text, caret, or selection changes occur within the editor.  This event can be handled to update external UI controls based on updated editor context.

## The UserInterfaceUpdate Event

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[UserInterfaceUpdate](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.UserInterfaceUpdate) event is raised following a brief delay after certain actions occur in the editor.

The event is set to raise `1` second after any of these actions occur:

- Document text changed.
- Document [ParseData](xref:ActiproSoftware.Text.ICodeDocument.ParseData) property changed.
- Caret moves.
- Selection changes.

## Repeated Actions

The [UserInterfaceUpdate](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.UserInterfaceUpdate) event will not raise until the delay period has elapsed following any action listed above.

For instance, if the end user types a word and then stops, the timer to raise this event starts.  However then assume that the end user holds down the down arrow to scroll through the document, before the delay period has elapsed.  This action resets the event timer, and the process restarts again.  Once the delay period has been reached without any further actions taking place, the event is raised.
