---
title: "Clipboard Operations"
page-title: "Clipboard Operations - SyntaxEditor Input/Output Features"
order: 3
---
# Clipboard Operations

SyntaxEditor makes use of the Windows clipboard as a temporary repository for data for cut/copy/paste operations. @if (wpf) {SyntaxEditor places text on the clipboard using the `DataFormats.UnicodeText` and `DataFormats.Text` formats.}

> [!TIP]
> **Clipboard** and **Drag and Drop** operations share many of the same concepts. Refer to the [Drag and Drop](drag-drop.md) topic for details specific to working with drag-and-drop.

## Cutting and Copying Text

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[CutLineToClipboard](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.CutLineToClipboard*) and [CopyToClipboard](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.CopyToClipboard*) methods may be used to cut and copy text to the clipboard respectively.  The text that is placed on the clipboard is the currently selected text designated by the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[Selection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.Selection).

If the selection length is zero, or in other words, no text is selected, a feature of SyntaxEditor is to place the entire line of text on the clipboard of which the caret is currently on.  You can control whether blank lines will overwrite the clipboard by using the [CanCutCopyBlankLineWhenNoSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyBlankLineWhenNoSelection) property.

There are [CutAndAppendToClipboard](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.CutAndAppendToClipboard*) and [CopyAndAppendToClipboard](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.CopyAndAppendToClipboard*) methods on [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) as well, that append to the clipboard instead of replacing the contents of the clipboard.

This code demonstrates how to copy the currently selected text to the clipboard:

```csharp
editor.ActiveView.CopyToClipboard();
```

## Cutting and Copying with HTML and RTF Export

The text that is copied can also be copied to the clipboard with HTML and RTF exported highlighting.  Then, if you paste the text in an application such as Word, the text will appear highlighted.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CanCutCopyDragWithHtml](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyDragWithHtml) property controls whether HTML exporting is performed whenever cutting or copying to the clipboard.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CanCutCopyDragWithRtf](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyDragWithRtf) property controls whether RTF exporting is performed whenever cutting or copying to the clipboard.

## Pasting Text

The [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[PasteFromClipboard](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.PasteFromClipboard*) method allows text on the clipboard to be pasted into the document, thereby replacing any text that is currently selected within the view from which the method is called.

If text was copied from a SyntaxEditor when the selection length was zero (see above) the entire line of text that was copied will be inserted above the line in which the caret is currently located. In this situation, the caret is not moved.

This code demonstrates how to paste text from the clipboard:

```csharp
editor.ActiveView.PasteFromClipboard();
```

@if (wpf) {

## The Clipboard and XBAP Security

If an XBAP is deployed with Internet security restrictions in place, the XBAP is not permitted to access the Windows clipboard.  SyntaxEditor properly recognizes this scenario, and maintains an internal clipboard of its own so that text can be copied and pasted between SyntaxEditor instances or within the same SyntaxEditor control.

}

## Customizing Text to be Cut or Copied

Sometimes it is useful to be able to customize the text, or objects, to be cut or copied.  The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event that fires before text is cut or copied to the clipboard, and also before a drag occurs (see the [Drag and Drop](drag-drop.md) topic for additional details).

In its event arguments, it passes the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) that is to be copied as well as the type of operation that will be performed.  When the event fires, the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) has already been initialized with @if (winrt) {a `StandardDataFormats.Text` entry}@if (wpf winforms) {`DataFormats.UnicodeText` and `DataFormats.Text` entries} based on the current selection in the editor.  The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) can be modified to customize what is sent to the clipboard.

@if (wpf) {

> [!NOTE]
> The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) interface is used instead of `IDataObject` partially because `IDataObject` will throw a security exception in most XBAP applications. [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) is designed to use many of the same method signatures that `IDataObject` does and provides a consistent API across our supported platforms.

}

@if (winforms) {

> [!NOTE]
> The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) interface is used instead of `IDataObject`. [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) is designed to use many of the same method signatures that `IDataObject` does and provides a consistent API across our supported platforms.

}

@if (winrt) {

> [!NOTE]
> The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) interface is used instead of `DataPackage`. [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) is designed to use many of the same method signatures that `DataPackage` does and provides a consistent API across our supported platforms.

}

## Customizing Text to be Pasted

Just like the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event, an event is provided to allow for customization of text that is to be pasted or dropped onto the editor (see the [Drag and Drop](drag-drop.md) topic for additional details).  The [PasteDragDrop](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.PasteDragDrop) event fires in several situations:

- Paste operations
- Paste completion
- CanPaste checks
- Drag enter
- Drag over
- Drag/drop

This event passes the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) that is to be pasted and the source of the event.  The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) is similar to `IDataObject` (see note above) and provides access to any clipboard data (such as non-text formats) that was used to trigger the event.

The [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property can be set to the text to be inserted.  It also can be set to `null` to insert nothing.

> [!TIP]
> 
> For [CanPaste](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.CanPaste) actions, the [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property can be set to any non-`null` value to indicate that an object can be pasted.  The actual value only has to be assigned for the [Paste](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.Paste) actions.

## Tracking Clipboard Change Events

Since the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event fires any time text is cut or copied from the control, it can also be used to maintain an external clipboard-setting history in your application.  This is useful for maintaining a clipboard ring for your application.
