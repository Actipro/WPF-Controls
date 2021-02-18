---
title: "Undo History"
page-title: "Undo History - Advanced Text Features - SyntaxEditor Text/Parsing Framework"
order: 3
---
# Undo History

The text framework's undo history tracks undoable text changes, allows for examination and manipulation of the undo/redo stacks, and provides access to the change types (unsaved/saved) for snapshot lines.

## Undo History

The [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[UndoHistory](xref:ActiproSoftware.Text.ITextDocument.UndoHistory) property is used to access the [IUndoHistory](xref:ActiproSoftware.Text.Undo.IUndoHistory), which provides the undo/redo features for a document.

The undo history contains two stacks, the [UndoStack](xref:ActiproSoftware.Text.Undo.IUndoHistory.UndoStack) and [RedoStack](xref:ActiproSoftware.Text.Undo.IUndoHistory.RedoStack).  As [text changes](../core-text/text-changes.md) are performed on a document, undo entries are added to the undo stack.  When an undo operation is performed, the top item on the undo stack is moved to the redo stack and the document text is updated appropriately.  If a redo operation is performed, the top item on the redo stack is moved back to the undo stack and the document text is updated appropriately.

## Clearing the Undo/Redo Stacks

The undo/redo stacks can be cleared by calling the [IUndoHistory](xref:ActiproSoftware.Text.Undo.IUndoHistory).[Clear](xref:ActiproSoftware.Text.Undo.IUndoHistory.Clear*) method.  The parameterless overload of this method keeps all change tracking in place, while another overload allows for specifying whether to reset change tracking.

The undo/redo stacks are automatically cleared when the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[SetText](xref:ActiproSoftware.Text.ITextDocument.SetText*) method overload is called that only accepts a `String` parameter.  This is done since when calling that method, it is assumed that you are completely replacing and resetting the text content of the document.

## Enumerating Undo/Redo Stack Entries

The [CanUndo](xref:ActiproSoftware.Text.Undo.IUndoHistory.CanUndo) and [CanRedo](xref:ActiproSoftware.Text.Undo.IUndoHistory.CanRedo) properties indicate whether there are any entries currently on the undo/redo stacks.

For enumerating the stack entries, use the [UndoStack](xref:ActiproSoftware.Text.Undo.IUndoHistory.UndoStack) and [RedoStack](xref:ActiproSoftware.Text.Undo.IUndoHistory.RedoStack) properties to retrieve the proper stack.  These both return an [IUndoableTextChangeStack](xref:ActiproSoftware.Text.Undo.IUndoableTextChangeStack) instance.

Each stack has a [Count](xref:ActiproSoftware.Text.Undo.IUndoableTextChangeStack.Count) property and the stack is capable of enumerating [IUndoableTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChange) entries.  A specific stack entry can be retrieved using the [GetTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChangeStack.GetTextChange*) method.  The capacity for a stack can be changed via the [Capacity](xref:ActiproSoftware.Text.Undo.IUndoableTextChangeStack.Capacity) property.  The default capacity is `100` entries but can be made as large or small as you wish.

The [IUndoableTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChange) interface contains a lot of information found on the [ITextChange](xref:ActiproSoftware.Text.ITextChange) interface that created it.  A [Description](xref:ActiproSoftware.Text.Undo.IUndoableTextChange.Description) property returns a description of the entry that can be displayed in user interfaces.

## Performing an Undo or Redo

An undo operation can be performed like this:

```csharp
document.UndoHistory.Undo();
```

An redo operation can be performed like this:

```csharp
document.UndoHistory.Redo();
```

Other overloads for the [Undo](xref:ActiproSoftware.Text.Undo.IUndoHistory.Undo*) and [Redo](xref:ActiproSoftware.Text.Undo.IUndoHistory.Redo*) methods allow you to undo/redo multiple entries.

## Getting the Change Type for a Line

The undo history tracks the change types for lines in the document, meaning whether lines have been modified at all, modified since the last save, or never modified.  The change type is represented by the [SavePointChangeType](xref:ActiproSoftware.Text.Undo.SavePointChangeType) enumeration and can be either `None`, `UnsavedChanges`, or `SavedChanges`.

As changes occur in a document, the appropriate ranges are marked as unsaved changes.  When the [ITextDocument](xref:ActiproSoftware.Text.ITextDocument).[IsModified](xref:ActiproSoftware.Text.ITextDocument.IsModified) property changes to `false`, the unsaved changes are modified to be saved changes.  From that point on, any new changes that occur are marked unsaved again.  When the undo history gets cleared (see above), all change tracking is cleared, since it is assumed that the document has been reset.
