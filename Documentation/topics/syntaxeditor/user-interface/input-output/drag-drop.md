---
title: "Drag and Drop"
page-title: "Drag and Drop - SyntaxEditor Input/Output Features"
order: 4
---
# Drag and Drop

SyntaxEditor supports drag and drop operations within itself, as well as with external controls.

## Allowing Drag and Drop

SyntaxEditor supports drag and drop by default.  However drags and drops can also be disabled if desired.

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[AllowDrag](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.AllowDrag) property can be set to `false` to disable dragging from the editor.

The `SyntaxEditor.AllowDrop` property can be set to `false` to disable dropping into the editor.

## Dragging with HTML and RTF Export

The text that is dragged can include HTML and RTF exported highlighting.  Then, if you drop the text in an application such as Word, the text will appear highlighted.

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[CanCutCopyDragWithHtml](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyDragWithHtml) property controls whether HTML exporting is performed whenever dragging.

The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[CanCutCopyDragWithRtf](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyDragWithRtf) property controls whether RTF exporting is performed whenever dragging.

## Customizing Text to be Cut, Copied, or Dragged

Sometimes it is useful to be able to customize the text, or objects, to be cut, copied, or dragged.  The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event that fires before text is cut or copied to the clipboard, and also before a drag occurs.

In its event arguments, it passes the [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) that is to be copied as well as the type of operation that will be performed.  When the event fires, the [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) has already been initialized with `DataFormats.UnicodeText` and `DataFormats.Text` entries based on the current selection in the editor.  The [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) can be modified to customize what is sent to the clipboard or dragged.

> [!NOTE]
> The [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) interface is used instead of `IDataObject` because `IDataObject` will throw a security exception in most XBAP applications. [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) is designed to use many of the same method signatures that `IDataObject` does.

## Customizing Text to be Pasted or Dropped

Just like the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event, an event is provided to allow for customization of text that is to be pasted or dropped onto the editor.  The [PasteDragDrop](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.PasteDragDrop) event fires in several situations:

- Paste operations
- Paste completion
- CanPaste checks
- Drag enter
- Drag over
- Drag/drop

This event passes the [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) that is to be pasted/dropped and the source of the event.  The [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) is similar to `IDataObject` (see note above) and provides access to any clipboard data (such as non-text formats) that was used to trigger the event.

The [PasteDragDropEventArgs](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property can be set to the text to be inserted.  It also can be set to `null` to insert nothing.

The `Text` property is auto-populated by examining the [IDataStore](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.IDataStore) to see if any data is present with format "Text".  If pasted or dragged data doesn't include data of that format type, then the `Text` property will be `null`, indicating no paste or drop should be allowed.  In a situation where a file is dragged over the editor, but no data with format "Text" is part of the dragged data, the `Text` property should be set to something like the full file path in the event handler to allow it to be dropped.

> [!NOTE]
> For CanPaste checks and drag enter scenarios, the event arg's `Text` property must be set to any non-null value to indicate that the object can be pasted or dropped.  The `Text` property doesn't need to be set to the actual value to be pasted/dropped in those scenarios, which is helpful when it's time consuming to retrieve the final text that will be pasted/dropped.

The drag over scenario can be ignored unless you wish to change the drag effect.

## Reselection of Text After a Drop

By default, any dropped text will be reselected following the drop.  The [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).[IsDragDropTextReselectEnabled](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor.IsDragDropTextReselectEnabled) property can be set to `false` to disable this behavior.
