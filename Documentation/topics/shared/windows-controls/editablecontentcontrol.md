---
title: "EditableContentControl"
page-title: "EditableContentControl - Shared Library Controls"
order: 16
---
# EditableContentControl

The [EditableContentControl](xref:@ActiproUIRoot.Controls.EditableContentControl) control is a standard `ContentControl`, which also features an alternate editing mode that can be toggled to display a `TextBox` for editing a string.

When entering edit mode, the `Content` property value is copied to the [EditableContent](xref:@ActiproUIRoot.Controls.EditableContentControl.EditableContent) property, and the UI switches to a `TextBox` for editing the editable content.  Note that an alternate template could be provided for the control to support an editor other than a `TextBox`.

After the `TextBox` is displayed, the [SelectText](xref:@ActiproUIRoot.Controls.EditableContentControl.SelectText*) method is called, which selects all text by default.  This method is passed the `TextBox` and can be overridden to select nothing or a certain range of text.  An example usage scenario is when editing a filename and you only wish to update the name portion without the extension being selected.

While in editing mode, pressing <kbd>Enter</kbd> or moving focus out of the control will commit the value.  This has the effect of copying the [EditableContent](xref:@ActiproUIRoot.Controls.EditableContentControl.EditableContent) property value back over to `Content`, which is done in the [SetContentAfterEditing](xref:@ActiproUIRoot.Controls.EditableContentControl.SetContentAfterEditing*) method.  That method passes in the edited content and by default sets that value to the `Content`.  The method can be overridden to coerce the edited content, or can choose to not set `Content`, which has the effect of canceling the edit.  The [IsEditing](xref:@ActiproUIRoot.Controls.EditableContentControl.IsEditing) can even be set back to `true` if you wish for focus to remain in the `TextBox`, such as when there's an invalid value.

If <kbd>Esc</kbd> is pressed while editing, the value is not committed.

The [EditableContentControl](xref:@ActiproUIRoot.Controls.EditableContentControl) class has these important members:

| Member | Description |
|-----|-----|
| [EditableContent](xref:@ActiproUIRoot.Controls.EditableContentControl.EditableContent) Property | Gets or sets the content being edited.  This property is for internal use only. |
| [IsEditing](xref:@ActiproUIRoot.Controls.EditableContentControl.IsEditing) Property | Gets or sets whether the content is currently being edited.  This property toggles the UI of the control between display and edit modes. |
