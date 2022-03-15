---
title: "Sessions"
page-title: "Sessions - SyntaxEditor IntelliPrompt Features"
order: 2
---
# Sessions

An IntelliPrompt session is essentially a controller for a certain type of IntelliPrompt UI.  Each type of IntelliPrompt UI (completion list, quick info, etc.) has a related session.  The IntelliPrompt UI and functionality can be made activate by "opening" the related session.

## Built-In Sessions

IntelliPrompt functionality is active only while related sessions are open.  For instance, a completion list is displayed only while its session is open.

SyntaxEditor includes several built-in IntelliPrompt sessions, however you can create your own as well.

These sessions are currently included:

- [Completion list](completion-list.md)
- [Quick info](quick-info.md)
- [Parameter info](parameter-info.md)
- [Code snippets (selection / template)](code-snippets.md)

## Provider Language Services for Built-In Sessions

There are several provider interfaces that can be [registered as language services](../../language-creation/service-locator-architecture.md).  These providers make it very easy to respond to certain editor events and respond with the appropriate IntelliPrompt sessions.

These providers are currently included:

- [Completion provider](../../language-creation/provider-services/completion-provider.md)
- [Quick info provider](../../language-creation/provider-services/quick-info-provider.md)
- [Parameter info provider](../../language-creation/provider-services/parameter-info-provider.md)
- [Code snippet provider](../../language-creation/provider-services/code-snippet-provider.md)

## Session Types

Each IntelliPrompt session must be associated with a specific session type.  A session type is represented by the [IIntelliPromptSessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSessionType) interface.

This interface requires that each type specify a unique string-based `Key` that identifies it.  It also has an [AreMultipleSessionsAllowed](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSessionType.AreMultipleSessionsAllowed) property that returns whether more than one session of that type are permitted at a time.  If that property is `false` and a second session of the same types is added to the manager (see below), the first session will be removed.

The [IntelliPromptSessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.IntelliPromptSessionType) class implements [IIntelliPromptSessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSessionType) and can be used if you need to create your own session type.

> [!NOTE]
> The [IntelliPromptSessionTypes](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IntelliPromptSessionTypes) class has a number of static properties that return the most common [IIntelliPromptSessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSessionType) instances such as session types for Quick Info, Completion List, etc.

## Opening and Closing Sessions

When a session is "open", it is active within the user interface.  The [IIntelliPromptSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession).[IsOpen](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.IsOpen) property indicates if a session is currently open.

A session can be opened by calling the [Open](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.Open*) method.  This method accepts two parameters: the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) in which the session should be opened and the [TextRange](xref:ActiproSoftware.Text.TextRange) in the view that is affected by the session.  The text range is most often the selection range or the text range of the word surrounding the caret.

When a session opens, its [Opened](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.Opened) event fires.

> [!NOTE]
> Some session implementations may add additional `Open` method overloads that should be used for that session implementation instead of the basic [Open](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.Open*) method.

A session can be closed by calling the [Close](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.Close*) method.  This method accepts a `Boolean` parameter that indicates whether the session was cancelled or not.

When a session opens, its [Closed](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.Closed) event fires.  The event arguments of this event specify whether the session was cancelled.

## Session Information

Each [IIntelliPromptSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession) contains these members that provide some useful information about the session:

| Member | Description |
|-----|-----|
| [SessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.SessionType) Property | Gets the [IIntelliPromptSessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSessionType) that identifies the type of session. |
| [SnapshotRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.SnapshotRange) Property | Gets the [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) containing the original [TextRange](xref:ActiproSoftware.Text.TextRange) in the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) that triggered the session.  This member should only be used while the session is open. |
| [View](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSession.View) Property | Gets the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) in which the session is opened.  This member should only be used while the session is open. |

## Managing Sessions at Run-Time

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) class has a [IntelliPrompt](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IntelliPrompt) property that is of type [IIntelliPromptManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptManager) and provides access to all of the open sessions within the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).

The [Sessions](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptManager.Sessions) collection lists all the open sessions and can be enumerated.

The [CloseAllSessions](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptManager.CloseAllSessions*) method closes all open sessions, while the [CloseSessions](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptManager.CloseSessions*) member only closes sessions of a particular [IIntelliPromptSessionType](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IIntelliPromptSessionType).

## Requesting Built-In IntelliPrompt Session Types

Sometimes you may wish to programmatically request that a certain type of IntelliPrompt session be opened, but you want the current set of IntelliPrompt providers (generally designated from language services) to decide if an IntelliPrompt session should open and what should be in it.  An example scenario is if your language checks to see when the `.` key is pressed via an event sink service.  When that event occurs, it raises a request to see if an IntelliPrompt completion session should open.

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[IntelliPrompt](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.IntelliPrompt) object has methods for requesting built-in session types:

| Member | Description |
|-----|-----|
| [RequestAutoComplete](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestAutoComplete*) Method | Performs an auto-complete if the language supports an IntelliPrompt completion session at the current offset. |
| [RequestCompletionSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestCompletionSession*) Method | Displays a completion list if the language supports an IntelliPrompt completion session at the current offset. |
| [RequestInsertSnippetSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestInsertSnippetSession*) Method | Displays IntelliPrompt 'expansion' code snippet selection at the current offset. |
| [RequestParameterInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestParameterInfoSession*) Method | Displays IntelliPrompt parameter info based on the current context. |
| [RequestQuickInfoSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestQuickInfoSession*) Method | Displays IntelliPrompt quick info based on the current context. |
| [RequestSurroundWithSession](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewIntelliPrompt.RequestSurroundWithSession*) Method | Displays IntelliPrompt 'surrounds with' code snippet selection at the current offset. |
