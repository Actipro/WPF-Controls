---
title: "Default Key Bindings"
page-title: "Default Key Bindings - SyntaxEditor Input/Output Features"
order: 6
---
# Default Key Bindings

Many keyboard shortcuts are automatically bound to allow keyboard access to most of the built-in [edit actions](edit-actions.md).

The following tables describe the default key bindings that are in place when the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control is created.  These can be modified by using the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).`InputBindings` collection.

## Clipboard/Undo Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + C | [CopyToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CopyToClipboardAction) |
| Ctrl + Ins | [CopyToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CopyToClipboardAction) |
| Ctrl + X | [CutToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CutToClipboardAction) |
| Shift + Del | [CutToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CutToClipboardAction) |
| Ctrl + L | [CutLineToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CutLineToClipboardAction) |
| Ctrl + V | [PasteFromClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.PasteFromClipboardAction) |
| Shift + Ins | [PasteFromClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.PasteFromClipboardAction) |
| Ctrl + Y | [RedoAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RedoAction) |
| Ctrl + Shift + Z | [RedoAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RedoAction) |
| Ctrl + Z | [UndoAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.UndoAction) |

## Deletion Edit Actions

| Key | Edit Action |
|-----|-----|
| Backspace | [BackspaceAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.BackspaceAction) |
| Shift + Backspace | [BackspaceAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.BackspaceAction) |
| Ctrl + Backspace | [BackspaceToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.BackspaceToPreviousWordAction) |
| Del | [DeleteAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DeleteAction) |
| Ctrl + Shift + L | [DeleteLineAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DeleteLineAction) |
| Ctrl + Del | [DeleteToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DeleteToNextWordAction) |

## Insertion Edit Actions

| Key | Edit Action |
|-----|-----|
| Enter | [InsertLineBreakAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.InsertLineBreakAction) |
| Shift + Enter | [InsertLineBreakAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.InsertLineBreakAction) |
| Ctrl + Enter | [OpenLineAboveAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.OpenLineAboveAction) |
| Ctrl + Shift + Enter | [OpenLineBelowAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.OpenLineBelowAction) |

## IntelliPrompt Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + Space | [RequestIntelliPromptAutoCompleteAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RequestIntelliPromptAutoCompleteAction) |
| Ctrl + Shift + Space | [RequestIntelliPromptParameterInfoSessionAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RequestIntelliPromptParameterInfoSessionAction) |

## Miscellaneous Edit Actions

<table>
<thead>

<tr>
<th>Key</th>
<th>Edit Action</th>
</tr>

</thead>
<tbody>

<tr>
<td>Tab</td>
<td>

[InsertTabStopOrIndentAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.InsertTabStopOrIndentAction)

</td>
</tr>

<tr>
<td>Shift + Tab</td>
<td>

[RemoveTabStopOrOutdentAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RemoveTabStopOrOutdentAction)

</td>
</tr>

<tr>
<td>Ctrl + U</td>
<td>

[MakeLowercaseAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MakeLowercaseAction)

</td>
</tr>

<tr>
<td>Ctrl + Shift + U</td>
<td>

[MakeUppercaseAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MakeUppercaseAction)

</td>
</tr>

<tr>
<td>Insert</td>
<td>

[ToggleOverwriteModeAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ToggleOverwriteModeAction)

</td>
</tr>

<tr>
<td>Ctrl + T</td>
<td>

[TransposeCharactersAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.TransposeCharactersAction)

</td>
</tr>

<tr>
<td>Ctrl + Shift + T</td>
<td>

[TransposeWordsAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.TransposeWordsAction)

</td>
</tr>

<tr>
<td>Shift + Alt + T</td>
<td>

[TransposeLinesAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.TransposeLinesAction)

</td>
</tr>

<tr>
<td>Alt + Up</td>
<td>

[MoveSelectedLinesUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveSelectedLinesUpAction)

</td>
</tr>

<tr>
<td>Alt + Down</td>
<td>

[MoveSelectedLinesDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveSelectedLinesDownAction)

</td>
</tr>

@if (winrt wpf) {
<tr>
<td>Ctrl + Num+</td>
<td>

[ZoomInAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ZoomInAction)

</td>
</tr>
}

@if (winrt wpf) {
<tr>
<td>Ctrl + Num-</td>
<td>

[ZoomOutAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ZoomOutAction)

</td>
</tr>
}

@if (winrt wpf) {
<tr>
<td>Ctrl + 0</td>
<td>

[ResetZoomLevelAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ResetZoomLevelAction)

</td>
</tr>
}

</tbody>
</table>

## Movement Edit Actions

| Key | Edit Action |
|-----|-----|
| Down | [MoveDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveDownAction) |
| Up  | [MoveUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveUpAction) |
| Left | [MoveLeftAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveLeftAction) |
| Right | [MoveRightAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveRightAction) |
| Ctrl + Left | [MoveToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToPreviousWordAction) |
| Ctrl + Right | [MoveToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToNextWordAction) |
| Home | [MoveToLineStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToLineStartAction) |
| End | [MoveToLineEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToLineEndAction) |
| Ctrl + Home | [MoveToDocumentStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToDocumentStartAction) |
| Ctrl + End | [MoveToDocumentEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToDocumentEndAction) |
| Page Up | [MovePageUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MovePageUpAction) |
| Page Down | [MovePageDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MovePageDownAction) |
| Ctrl + Page Up | [MoveToVisibleTopAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToVisibleTopAction) |
| Ctrl + Page Down | [MoveToVisibleBottomAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToVisibleBottomAction) |
| Ctrl + ] | [MoveToMatchingBracketAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToMatchingBracketAction) |

## Scroll Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + Down | [ScrollDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ScrollDownAction) |
| Ctrl + Up | [ScrollUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ScrollUpAction) |

## Search Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + F | [FindAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindAction) |
| F3  | [FindNextAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindNextAction) |
| Ctrl + F3 | [FindNextSelectedAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindNextSelectedAction) |
| Shift + F3 | [FindPreviousAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindPreviousAction) |
| Ctrl + Shift + F3 | [FindPreviousSelectedAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindPreviousSelectedAction) |
| Ctrl + H | [ReplaceAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ReplaceAction) |
| Ctrl + I | [IncrementalSearchAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.IncrementalSearchAction) |
| Ctrl + Shift + I | [ReverseIncrementalSearchAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ReverseIncrementalSearchAction) |

## Selection Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + Shift + Num- | [CodeBlockSelectionContractAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CodeBlockSelectionContractAction) |
| Ctrl + Shift + Num+ | [CodeBlockSelectionExpandAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CodeBlockSelectionExpandAction) |
| Escape | [CollapseSelectionAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CollapseSelectionAction) |
| Shift + Down | [SelectDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectDownAction) |
| Shift + Up | [SelectUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectUpAction) |
| Shift + Left | [SelectLeftAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectLeftAction) |
| Shift + Right | [SelectRightAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectRightAction) |
| Ctrl + Shift + Left | [SelectToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToPreviousWordAction) |
| Ctrl + Shift + Right | [SelectBlockToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockToNextWordAction) |
| Shift + Home | [SelectToLineStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToLineStartAction) |
| Shift + End | [SelectToLineEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToLineEndAction) |
| Ctrl + Shift + Home | [SelectToDocumentStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToDocumentStartAction) |
| Ctrl + Shift + End | [SelectToDocumentEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToDocumentEndAction) |
| Shift + Page Up | [SelectPageUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectPageUpAction) |
| Shift + Page Down | [SelectPageDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectPageDownAction) |
| Ctrl + Shift + Page Up | [SelectToVisibleTopAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToVisibleTopAction) |
| Ctrl + Shift + Page Down | [SelectToVisibleBottomAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToVisibleBottomAction) |
| Ctrl + A | [SelectAllAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectAllAction) |
| Ctrl + Shift + W | [SelectWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectWordAction) |
| Ctrl + Shift + ] | [SelectToMatchingBracketAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToMatchingBracketAction) |
| Shift + Alt + Down | [SelectBlockDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockDownAction) |
| Shift + Alt + Up | [SelectBlockUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockUpAction) |
| Shift + Alt + Left | [SelectBlockLeftAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockLeftAction) |
| Shift + Alt + Right | [SelectBlockRightAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockRightAction) |
| Ctrl + Shift + Alt + Left | [SelectBlockToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockToPreviousWordAction) |
| Ctrl + Shift + Alt + Right | [SelectBlockToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockToNextWordAction) |
