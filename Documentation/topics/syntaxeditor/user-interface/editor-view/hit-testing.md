---
title: "Hit Testing"
page-title: "Hit Testing - SyntaxEditor Editor View Features"
order: 24
---
# Hit Testing

SyntaxEditor makes it easy to hit test any `Point` in the editor and return detailed information about what is at that location.

## Performing a Hit Test

A hit test can be performed by first obtaining a `Point` relative to the SyntaxEditor's coordinates and then calling the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[HitTest](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.HitTest*) method.

This code gets the current location of the mouse and performs a hit test against it:

```csharp
IHitTestResult result = editor.HitTest(Mouse.GetPosition(editor));
```

## Hit Test Results

The [HitTest](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.HitTest*) method returns an object of type [IHitTestResult](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult).  This object provides information about exactly what is at the location being hit tested.

### SyntaxEditor and Location

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.SyntaxEditor) and [Location](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Location) properties return the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) instance and location that were hit-tested.

### Result Type

The [Type](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Type) property returns a [HitTestResultType](xref:@ActiproUIRoot.Controls.SyntaxEditor.HitTestResultType) enumeration value.  This value gives a high-level categorization of the result.

### View and Margin

The [View](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.View) property returns an [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView) if the location was over a view.

If the location was over a margin in the view, the [ViewMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.ViewMargin) property returns the [IEditorViewMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.Margins.IEditorViewMargin).

### Offset and Position

The [Offset](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Offset) and [Position](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Position) properties return the offset and [TextPosition](xref:ActiproSoftware.Text.TextPosition) of the character at the location.  If the location is not over a character, the nearest character is returned.  If the location is not over a view, then the offset is `-1`.

See information below for how to determine if the offset/position are for an actual character or for a close character.

### View Line and Text Area Location

The [ViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.ViewLine) property returns the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) that contains the [Position](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Position).

The [TextAreaLocation](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.TextAreaLocation) returns the location, but in text area-relative coordinates.

### Snapshot and Reader

The [Snapshot](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Snapshot) property returns the [ITextSnapshot](xref:ActiproSoftware.Text.ITextSnapshot) that was current when the hit test took place.  The [GetReader](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.GetReader*) method can be used to obtain an [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) that is initialized to the hit test offset.

### Intra-Text Spacer

The [IntraTextSpacerTag](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.IntraTextSpacerTag) property returns the [TagSnapshotRange<T>](xref:ActiproSoftware.Text.Tagging.TagSnapshotRange`1) of the [IIntraTextSpacerTag](xref:ActiproSoftware.Text.Tagging.IIntraTextSpacerTag) that contains the hit test offset, if any.  This property is only filled in when the hit test type is `ViewTextAreaOverIntraTextSpacer`.

@if (winrt wpf) {

### VisualElement

The [VisualElement](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.VisualElement) property returns the `FrameworkElement`, if any, that is related to the hit test result.

}

@if (winforms) {

### VisualElement

The [VisualElement](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.VisualElement) property returns the `IUIElement`, if any, that is related to the hit test result.

}

## Determining if the Hit Test is Over a Character

The [Type](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Type) property is useful for determining if the location is over a character in the text area.  When its value is `ViewTextAreaOverCharacter`, the location is directly over a character within an editor view.  When its value is `ViewTextAreaOverLine`, the location is not over a character within an editor view, but it is in the whitespace past the end of a view line.  When its value is `ViewTextArea`, the location is not over any editor view lines, but it is in the text area.

Thus the [Offset](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Offset) and [Position](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Position) properties return the exact character the location is over when [Type](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.Type) is `ViewTextAreaOverCharacter`.  Otherwise, the offset/position of the closest character is returned.

## Scanning Text Around the Hit Test Result

The [ITextSnapshotReader](xref:ActiproSoftware.Text.ITextSnapshotReader) returned by the [GetReader](xref:@ActiproUIRoot.Controls.SyntaxEditor.IHitTestResult.GetReader*) method can be used to scan text and tokens near the hit test result.

See the [Scanning Text Using a Reader](../../text-parsing/core-text/scanning-text.md) topic for more information on scanning text.
