---
title: "Caret"
page-title: "Caret - SyntaxEditor Editor View Features"
order: 2
---
# Caret

The SyntaxEditor caret is the bar that blinks within an editor view and marks the location where typing will insert text.  It also is linked to the end position of a [selection](selection.md).  SyntaxEditor optionally supports multiple selections and therefore multiple carets, which enables simulataneous editing in several document locations at the same time.

## Accessing the Caret

The primary selection's caret and its functionality described below is available via the [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection) interface, which can be retrieved from the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ActiveView](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ActiveView).[Selection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.Selection) property.

## Determining and Moving the Caret Location

The primary selection has several properties for determining its caret location.  A couple of the properties can be set to move the caret.

| Member | Description |
|-----|-----|
| [CaretCharacterColumn](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretCharacterColumn) Property | Gets the character column of the primary caret within the active view. |
| [CaretDisplayCharacterColumn](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretDisplayCharacterColumn) Property | Gets the character column of the primary caret within the active view.  and adds one to the value for display purposes within a status bar. |
| [CaretOffset](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretOffset) Property | Gets or sets the offset of the primary caret within the active view. |
| [CaretPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretPosition) Property | Gets or sets the [TextPosition](xref:ActiproSoftware.Text.TextPosition) of the primary caret within the active view. |

This code moves the caret to the start of the third line (the value is zero-based):

```csharp
editor.ActiveView.Selection.CaretPosition = new TextPosition(2, 0);
```

Text positions are zero-based by default.  Use the one-based [TextPosition](xref:ActiproSoftware.Text.TextPosition).[DisplayLine](xref:ActiproSoftware.Text.TextPosition.DisplayLine) and [DisplayCharacter](xref:ActiproSoftware.Text.TextPosition.DisplayCharacter) properties when displaying the position in a status bar or other user interface.

## Multiple Selections and Carets

A caret appears at the end of each editor view selection.  Multiple selections are supported when the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[AreMultipleSelectionRangesEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.AreMultipleSelectionRangesEnabled) property is `true`.

See the [Selection](selection.md) topic for more information on working with multiple selections and how end users can add selections at run-time.

## Changing the Blink Interval

The caret blink interval is set by default to `500ms`.

Use the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CaretBlinkInterval](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretBlinkInterval) property to change the blink interval to be faster or slower.  The value indicates the millisecond delay between blinks.

This code changes the blink interval to one second:

```csharp
editor.CaretBlinkInterval = 1000;
```

## Changing the Insert and Overwrite Mode Width/Style

The caret can be in one of two modes: insert and overwrite.  Insert mode is the traditional mode where typed text is inserted directly at the caret's offset.  Overwrite mode will insert text at the caret's offset but will first remove any existing character on the line that is already at that offset.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CaretInsertWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretInsertWidth) and [CaretOverwriteWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretOverwriteWidth) properties can be used to tweak the width of the caret in each respective mode.

The [CaretInsertKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretInsertKind) and [CaretOverwriteKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretOverwriteKind) properties accept a [CaretKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.CaretKind) value.  This value determines the shape/alignment of the caret and can be `VerticalLine`, `HorizontalUnderline`, or `Block`.

@if (winforms) {

## High-Contrast Inversion

The caret normally renders in a single color that contrasts with the editor's background.  The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[UseInvertedCaret](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.UseInvertedCaret) property can be set to `true`, which causes the caret to invert everything behind it.  This inversion is high-contrast and its appearance was commonly used in classic code editors.

}

## Temporarily Suspending Blinking

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[SuspendCaretBlinking](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.SuspendCaretBlinking*) method can be used to temporarily suspend blinking of the caret.  It accepts a boolean parameter indicating whether to display the caret while suspended or not.  Generally, `false` is passed to this method.

When the caret should resume blinking, call the [ResumeCaretBlinking](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ResumeCaretBlinking*) method.

## Caret Move Event

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ViewSelectionChanged](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ViewSelectionChanged) event is raised whenever the caret moves, since the caret is always in sync with the end of the selection.

See the [Selection](selection.md) topic for more information on this event.

## Scroll to Caret

The editor view will auto-scroll by default to the primary caret when the selection is changed.  A batch can be created on the selection using a `NoScrollToCaret` option to prevent auto-scrolling to the caret following text changes:

```csharp
using (var batch = editor.ActiveView.Selection.CreateBatch(EditorViewSelectionBatchOptions.NoScrollToCaret)) {
	// Perform text changes here
}
```

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[ScrollToCaretOnSelectAll](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ScrollToCaretOnSelectAll) property that determines whether to scroll to the caret when a select all operation is performed.  It defaults to `true`.

The [IEditorViewScroller](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller) interface defines a [ScrollToCaret](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollToCaret*) method that can be called manually to scroll to the caret.  This code scrolls the active view to the caret:

```csharp
editor.ActiveView.Scroller.ScrollToCaret();
```
