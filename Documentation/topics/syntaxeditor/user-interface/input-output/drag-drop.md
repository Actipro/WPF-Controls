---
title: "Drag and Drop"
page-title: "Drag and Drop - SyntaxEditor Input/Output Features"
order: 4
---
# Drag and Drop

SyntaxEditor supports drag and drop operations within itself, as well as with external controls.

> [!TIP]
> **Drag and Drop** and **Clipboard** operations share many of the same concepts. Refer to the [Clipboard Operations](clipboard-operations.md) topic for details specific to working with the clipboard.

## Allowing Drag and Drop

SyntaxEditor supports drag and drop by default.  However, drags and drops can also be disabled if desired.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[AllowDrag](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.AllowDrag) property can be set to `false` to disable dragging from the editor.

The `SyntaxEditor.AllowDrop` property can be set to `false` to disable dropping onto the editor.

## Dragging with HTML and RTF Export

The text that is dragged can include HTML and RTF exported highlighting.  Then, if you drop the text in an application such as Word, the text will appear with syntax highlighting.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CanCutCopyDragWithHtml](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyDragWithHtml) property controls whether HTML exporting is performed whenever dragging.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CanCutCopyDragWithRtf](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanCutCopyDragWithRtf) property controls whether RTF exporting is performed whenever dragging.

## Customizing Text to be Dragged

Sometimes it is useful to be able to customize the text, or objects, to be dragged.  The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event fires before text is cut/copied to the clipboard and also before a drag occurs (see the [Clipboard Operations](clipboard-operations.md) topic for additional details).

In its event arguments, it passes the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) that is to be dragged as well as the type of operation that will be performed.  When the event fires, the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) has already been initialized with @if (wpf winforms) {`DataFormats.UnicodeText` and `DataFormats.Text` entries}@if (winrt) {`StandardDataFormats.Text`} based on the current selection in the editor.  The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) can be modified to customize what is dragged.

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

## Customizing Text to be Dropped

Just like the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CutCopyDrag](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CutCopyDrag) event, an event is provided to allow for customization of text that is to be pasted or dropped onto the editor (see the [Clipboard Operations](clipboard-operations.md) topic for additional details).  The [PasteDragDrop](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.PasteDragDrop) event fires in several situations:

- Paste operations
- Paste completion
- CanPaste checks
- Drag enter
- Drag over
- Drag/drop

This event passes the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) that is to be dropped and the source of the event. @if (wpf winforms) {The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) is similar to `IDataObject` (see note above) and provides access to any shared application data (such as non-text formats) that was used to trigger the event. }@if (winrt) {The [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) is similar to `DataPackage` (see note above) and provides access to any shared application data (such as non-text formats) that was used to trigger the event. }

The [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property can be set to the text to be inserted.  It also can be set to `null` to insert nothing.

@if (wpf winforms) {

The `Text` property is auto-populated by examining the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) for the following standard text formats (in this order):

- `DataFormats.UnicodeText`
- `DataFormats.Text`
- `DataFormats.StringFormat`

}

@if (winrt) {

The `Text` property is auto-populated by examining the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore) for `StandardDataFormats.Text`. 

}

If dragged data doesn't include one of the standard text formats, then the `Text` property will be `null`.  The easiest way to support dragging onto SyntaxEditor is to ensure that one of the standard text formats is set when initiating the drag.

@if (wpf) {

The following code demonstrates initiating a drag with a `String` value that will be automatically converted to `DataFormats.StringFormat`.

```csharp
using System.Windows;
...
DragDrop.DoDragDrop(dragSource, "Insert Drag Text Here", DragDropEffects.Copy);
```

}

@if (winforms) {

The following code demonstrates initiating a drag with a `String` value that will be automatically converted to `DataFormats.StringFormat`.

```csharp
using System.Windows;
...
dragSource.DoDragDrop("Insert Drag Text Here", DragDropEffects.Copy);
```

}

@if (wpf) {

If more than one data format is needed, simply make sure text is one of the standard formats like shown in the following code:

```csharp
using System.Windows;
...
var dataObject = new DataObject();
dataObject.SetData(myCustomObject);
dataObject.SetText("Insert Drag Text Here");
DragDrop.DoDragDrop(dragSource, dataObject, DragDropEffects.Copy);

```

}

@if (winforms) {

If more than one data format is needed, simply make sure text is one of the standard formats like shown in the following code:

```csharp
using System.Windows;
...
var dataObject = new DataObject();
dataObject.SetData(myCustomObject);
dataObject.SetText("Insert Drag Text Here");
dragSource.DoDragDrop(dataObject, DragDropEffects.Copy);

```

}

See the **Customizing Drag Behavior** section below for more advanced drag scenarios like file drop.

> [!IMPORTANT]
> 
> For drag-related actions, the [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[DragEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.DragEventArgs) operation must be set to `Copy` or `Move` to indicate that the object can be dropped.
> 
> The [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property's actual value only has to be assigned for the [DragDrop](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragDrop) action.

> [!TIP]
> The [PasteDragDropAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction).[DragOver](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragOver) action can typically be ignored unless you wish to change the drag effect based on the position of the pointer or need to support custom drag objects.

## Customizing Drag Operations

The [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[DragEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.DragEventArgs) property is used to access the arguments that caused a drag-related event. SyntaxEditor will use the @if (wpf) {`DragEventArgs.Effects`}@if (winforms) {`DragEventArgs.Effect`}@if (winrt) {`DragEventArgs.AcceptedOperation`} property to determine the type of drag operation, if any, that should be accepted. Under default scenarios, this property is initialized based on the availability of text on the [IDataStore](xref:@ActiproUIRoot.Controls.SyntaxEditor.IDataStore), the read-only state of the drag and/or drop locations, and the position of the pointer within the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).  To change the default behavior, the value of the @if (wpf) {`DragEventArgs.Effects`}@if (winforms) {`DragEventArgs.Effect`}@if (winrt) {`DragEventArgs.AcceptedOperation`} property must be changed to the desired operation.

@if (wpf winforms) {

| Operation | Description |
|-----|-----|
| `DragDropEffects.Copy` | Indicates drag should be allowed that is a copy of the source. |
| `DragDropEffects.Move` | Indicates drag should be allowed that will move the text from the source to the drop location. |
| `DragDropEffects.None` | Indicates drag should not be allowed. |

}

@if (winrt) {

| Operation | Description |
|-----|-----|
| `DataPackageOperation.Copy` | Indicates drag should be allowed that is a copy of the source. |
| `DataPackageOperation.Move` | Indicates drag should be allowed that will move the text from the source to the drop location. |
| `DataPackageOperation.None` | Indicates drag should not be allowed. |

}

Multiple drag-related actions are handled by the [PasteDragDrop](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.PasteDragDrop) event and are raised in the following order:

- DragEnter
- DragOver
- DragDrop

The current action can be determined by the [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Action](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Action) property.

### DragEnter Action

The [PasteDragDropAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction).[DragEnter](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragEnter) action occurs when a drag operation first moves over the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView), and the result of this action will determine if subsequent actions are raised.

The value of @if (wpf) {`DragEventArgs.Effects`}@if (winforms) {`DragEventArgs.Effect`}@if (winrt) {`DragEventArgs.AcceptedOperation`} must be set to `Copy` or `Move` to allow a drop; otherwise, `None` if drop is not allowed.

> [!TIP]
> Setting a non-default operation for this event is only necessary if the data being dragged is not automatically recognized as text or to support dropping custom data onto the view.

> [!IMPORTANT]
> The `DragOver` and `DragDrop` actions *will not* be raised if the `DragEnter` action is completed with the operation set to `None`.

### DragOver Action

The [PasteDragDropAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction).[DragOver](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragOver) action occurs immediately after a drag operation moves over the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) and will continue to be raised every time the pointer moves while it is still over the `IEditorView`.

The value of @if (wpf) {`DragEventArgs.Effects`}@if (winforms) {`DragEventArgs.Effect`}@if (winrt) {`DragEventArgs.AcceptedOperation`} must be set to `Copy` or `Move` to allow a drop at the current pointer location; otherwise, `None` if drop is not allowed.

> [!TIP]
> The [DragOver](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragOver) action will typically assign the same operation as the [DragEnter](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragEnter) action unless special handling is necessary based on the position of the pointer. Refer to the [Hit Testing](../editor-view/hit-testing.md) topic for additional details about determine the relevant position of the pointer.

> [!IMPORTANT]
> If custom logic was used to override the default operation of the `DragEnter` action, the same logic will typically need to be repeated for the `DragOver` action.

### DragDrop Action

The [PasteDragDropAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction).[DragDrop](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropAction.DragDrop) action occurs when an allowed drag operation is completed.

The value of @if (wpf) {`DragEventArgs.Effects`}@if (winforms) {`DragEventArgs.Effect`}@if (winrt) {`DragEventArgs.AcceptedOperation`} can be set to `Copy` or `Move` to allow for a default text-based drag to be completed. When set to `Copy` or `Move`, the value of the [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property will determine the text that is dropped onto the view. If the `Text` property is `null`, a `Copy` or `Move` operation will have no effect.  Set the operation to `None` to prevent any default handling of text-based drag operations.

> [!IMPORTANT]
> If custom logic is used to handle the drop action, the operation must be set to `None` to prevent any default handling of text-based data that may have also been available with the drag event. Even when custom data is expected, a fallback text-based format of the data may have also been present which would be dropped onto the view if not canceled.

@if (wpf winforms) {

### File Drop Example

Dragging a file from the file system and onto the editor (i.e., `DataFormats.FileDrop`) is a good example of a scenario which requires customizing the drag operations.

@if (wpf) {

The following code illustrates checking for file drop and configuring the editor to allow the drag:

```csharp
using System.Windows;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
...
private void OnEditorPasteDragDrop(object sender, PasteDragDropEventArgs e) {
	if (e.DataStore.GetDataPresent(DataFormats.FileDrop)) {
		switch (e.Action) {
			case PasteDragDropAction.DragEnter:
			case PasteDragDropAction.DragOver:
				// Allow drag
				e.DragEventArgs.Effects = DragDropEffects.Copy;
			case PasteDragDropAction.DragDrop:
				// Cancel default handling
				e.DragEventArgs.Effects = DragDropEffects.None;
				// Process the dropped file here
		}		
	}
}
```

}

@if (winforms) {

The following code illustrates checking for file drop and configuring the editor to allow the drag:

```csharp
using System.Windows;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
...
private void OnEditorPasteDragDrop(object sender, PasteDragDropEventArgs e) {
	if (e.DataStore.GetDataPresent(DataFormats.FileDrop)) {
		switch (e.Action) {
			case PasteDragDropAction.DragEnter:
			case PasteDragDropAction.DragOver:
				// Allow drag
				e.DragEventArgs.Effect = DragDropEffects.Copy;
			case PasteDragDropAction.DragDrop:
				// Cancel default handling
				e.DragEventArgs.Effect = DragDropEffects.None;
				// Process the dropped file here
		}		
	}
}
```

}

}

## Reselection of Text After a Drop

By default, any dropped text will be reselected following the drop.  The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsDragDropTextReselectEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsDragDropTextReselectEnabled) property can be set to `false` to disable this behavior.
