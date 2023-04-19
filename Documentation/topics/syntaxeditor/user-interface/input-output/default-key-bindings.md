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
| <kbd>Ctrl</kbd>+<kbd>C</kbd> | [CopyToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CopyToClipboardAction) |
| <kbd>Ctrl</kbd>+<kbd>Ins</kbd> | [CopyToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CopyToClipboardAction) |
| <kbd>Ctrl</kbd>+<kbd>X</kbd> | [CutToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CutToClipboardAction) |
| <kbd>Shift</kbd>+<kbd>Del</kbd> | [CutToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CutToClipboardAction) |
| <kbd>Ctrl</kbd>+<kbd>L</kbd> | [CutLineToClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CutLineToClipboardAction) |
| <kbd>Ctrl</kbd>+<kbd>V</kbd> | [PasteFromClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.PasteFromClipboardAction) |
| <kbd>Shift</kbd>+<kbd>Ins</kbd> | [PasteFromClipboardAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.PasteFromClipboardAction) |
| <kbd>Ctrl</kbd>+<kbd>Y</kbd> | [RedoAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RedoAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Z</kbd> | [RedoAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RedoAction) |
| <kbd>Ctrl</kbd>+<kbd>Z</kbd> | [UndoAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.UndoAction) |

## Deletion Edit Actions

| Key | Edit Action |
|-----|-----|
| <kbd>Bkspace</kbd> | [BackspaceAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.BackspaceAction) |
| <kbd>Shift</kbd>+<kbd>Bkspace</kbd> | [BackspaceAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.BackspaceAction) |
| <kbd>Ctrl</kbd>+<kbd>Bkspace</kbd> | [BackspaceToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.BackspaceToPreviousWordAction) |
| <kbd>Del</kbd> | [DeleteAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DeleteAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>L</kbd> | [DeleteLineAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DeleteLineAction) |
| <kbd>Ctrl</kbd>+<kbd>Del</kbd> | [DeleteToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DeleteToNextWordAction) |

## Insertion Edit Actions

| Key | Edit Action |
|-----|-----|
| <kbd>Enter</kbd> | [InsertLineBreakAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.InsertLineBreakAction) |
| <kbd>Shift</kbd>+<kbd>Enter</kbd> | [InsertLineBreakAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.InsertLineBreakAction) |
| <kbd>Ctrl</kbd>+<kbd>Enter</kbd> | [OpenLineAboveAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.OpenLineAboveAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Enter</kbd> | [OpenLineBelowAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.OpenLineBelowAction) |

## IntelliPrompt Edit Actions

| Key | Edit Action |
|-----|-----|
| <kbd>Ctrl</kbd>+<kbd>Space</kbd> | [RequestIntelliPromptAutoCompleteAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RequestIntelliPromptAutoCompleteAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Space</kbd> | [RequestIntelliPromptParameterInfoSessionAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RequestIntelliPromptParameterInfoSessionAction) |

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
<td><kbd>Tab</kbd></td>
<td>

[InsertTabStopOrIndentAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.InsertTabStopOrIndentAction)

</td>
</tr>

<tr>
<td><kbd>Shift</kbd>+<kbd>Tab</kbd></td>
<td>

[RemoveTabStopOrOutdentAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.RemoveTabStopOrOutdentAction)

</td>
</tr>

<tr>
<td><kbd>Ctrl</kbd>+<kbd>U</kbd></td>
<td>

[MakeLowercaseAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MakeLowercaseAction)

</td>
</tr>

<tr>
<td><kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>U</kbd></td>
<td>

[MakeUppercaseAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MakeUppercaseAction)

</td>
</tr>

<tr>
<td><kbd>Ins</kbd></td>
<td>

[ToggleOverwriteModeAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ToggleOverwriteModeAction)

</td>
</tr>

<tr>
<td><kbd>Ctrl</kbd>+<kbd>T</kbd></td>
<td>

[TransposeCharactersAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.TransposeCharactersAction)

</td>
</tr>

<tr>
<td><kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd></td>
<td>

[TransposeWordsAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.TransposeWordsAction)

</td>
</tr>

<tr>
<td><kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>T</kbd></td>
<td>

[TransposeLinesAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.TransposeLinesAction)

</td>
</tr>

<tr>
<td><kbd>Alt</kbd>+<kbd>Up Arrow</kbd></td>
<td>

[MoveSelectedLinesUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveSelectedLinesUpAction)

</td>
</tr>

<tr>
<td><kbd>Alt</kbd>+<kbd>Down Arrow</kbd></td>
<td>

[MoveSelectedLinesDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveSelectedLinesDownAction)

</td>
</tr>

@if (winrt wpf) {
<tr>
<td><kbd>Ctrl</kbd>+<kbd>Num +</kbd></td>
<td>

[ZoomInAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ZoomInAction)

</td>
</tr>
}

@if (winrt wpf) {
<tr>
<td><kbd>Ctrl</kbd>+<kbd>Num -</kbd></td>
<td>

[ZoomOutAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ZoomOutAction)

</td>
</tr>
}

@if (winrt wpf) {
<tr>
<td><kbd>Ctrl</kbd>+<kbd>0</kbd></td>
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
| <kbd>Down Arrow</kbd> | [MoveDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveDownAction) |
| <kbd>Up Arrow</kbd>  | [MoveUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveUpAction) |
| <kbd>Left Arrow</kbd> | [MoveLeftAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveLeftAction) |
| <kbd>Right Arrow</kbd> | [MoveRightAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveRightAction) |
| <kbd>Ctrl</kbd>+<kbd>Left Arrow</kbd> | [MoveToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToPreviousWordAction) |
| <kbd>Ctrl</kbd>+<kbd>Right Arrow</kbd> | [MoveToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToNextWordAction) |
| <kbd>Home</kbd> | [MoveToLineStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToLineStartAction) |
| <kbd>End</kbd> | [MoveToLineEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToLineEndAction) |
| <kbd>Ctrl</kbd>+<kbd>Home</kbd> | [MoveToDocumentStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToDocumentStartAction) |
| <kbd>Ctrl</kbd>+<kbd>End</kbd> | [MoveToDocumentEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToDocumentEndAction) |
| <kbd>PgUp</kbd> | [MovePageUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MovePageUpAction) |
| <kbd>PgDn</kbd> | [MovePageDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MovePageDownAction) |
| <kbd>Ctrl</kbd>+<kbd>PgUp</kbd> | [MoveToVisibleTopAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToVisibleTopAction) |
| <kbd>Ctrl</kbd>+<kbd>PgDn</kbd> | [MoveToVisibleBottomAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToVisibleBottomAction) |
| <kbd>Ctrl</kbd>+<kbd>]</kbd> | [MoveToMatchingBracketAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.MoveToMatchingBracketAction) |

## Scroll Edit Actions

| Key | Edit Action |
|-----|-----|
| <kbd>Ctrl</kbd>+<kbd>Down Arrow</kbd> | [ScrollDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ScrollDownAction) |
| <kbd>Ctrl</kbd>+<kbd>Up Arrow</kbd> | [ScrollUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ScrollUpAction) |

## Search Edit Actions

| Key | Edit Action |
|-----|-----|
| <kbd>Ctrl</kbd>+<kbd>F</kbd> | [FindAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindAction) |
| <kbd>F3</kbd>  | [FindNextAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindNextAction) |
| <kbd>Ctrl</kbd>+<kbd>F3</kbd> | [FindNextSelectedAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindNextSelectedAction) |
| <kbd>Shift</kbd>+<kbd>F3</kbd> | [FindPreviousAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindPreviousAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>F3</kbd> | [FindPreviousSelectedAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.FindPreviousSelectedAction) |
| <kbd>Ctrl</kbd>+<kbd>H</kbd> | [ReplaceAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ReplaceAction) |
| <kbd>Ctrl</kbd>+<kbd>I</kbd> | [IncrementalSearchAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.IncrementalSearchAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>I</kbd> | [ReverseIncrementalSearchAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.ReverseIncrementalSearchAction) |

## Selection Edit Actions

| Key | Edit Action |
|-----|-----|
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Num -</kbd> | [CodeBlockSelectionContractAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CodeBlockSelectionContractAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Num +</kbd> | [CodeBlockSelectionExpandAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CodeBlockSelectionExpandAction) |
| <kbd>Esc</kbd> | [CollapseSelectionAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.CollapseSelectionAction) |
| <kbd>Shift</kbd>+<kbd>Down Arrow</kbd> | [SelectDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectDownAction) |
| <kbd>Shift</kbd>+<kbd>Up Arrow</kbd> | [SelectUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectUpAction) |
| <kbd>Shift</kbd>+<kbd>Left Arrow</kbd> | [SelectLeftAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectLeftAction) |
| <kbd>Shift</kbd>+<kbd>Right Arrow</kbd> | [SelectRightAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectRightAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Left Arrow</kbd> | [SelectToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToPreviousWordAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Right Arrow</kbd> | [SelectBlockToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockToNextWordAction) |
| <kbd>Shift</kbd>+<kbd>Home</kbd> | [SelectToLineStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToLineStartAction) |
| <kbd>Shift</kbd>+<kbd>End</kbd> | [SelectToLineEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToLineEndAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Home</kbd> | [SelectToDocumentStartAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToDocumentStartAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>End</kbd> | [SelectToDocumentEndAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToDocumentEndAction) |
| <kbd>Shift</kbd>+<kbd>PgUp</kbd> | [SelectPageUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectPageUpAction) |
| <kbd>Shift</kbd>+<kbd>PgDn</kbd> | [SelectPageDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectPageDownAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>PgUp</kbd> | [SelectToVisibleTopAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToVisibleTopAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>PgDn</kbd> | [SelectToVisibleBottomAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToVisibleBottomAction) |
| <kbd>Ctrl</kbd>+<kbd>A</kbd> | [SelectAllAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectAllAction) |
| <kbd>Ctrl</kbd>+<kbd>D</kbd> | [AddNextOccurrenceToSelectionAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.AddNextOccurrenceToSelectionAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>W</kbd> | [SelectWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectWordAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>]</kbd> | [SelectToMatchingBracketAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectToMatchingBracketAction) |
| <kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Down Arrow</kbd> | [SelectBlockDownAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockDownAction) |
| <kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Up Arrow</kbd> | [SelectBlockUpAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockUpAction) |
| <kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Left Arrow</kbd> | [SelectBlockLeftAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockLeftAction) |
| <kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Right Arrow</kbd> | [SelectBlockRightAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockRightAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Left Arrow</kbd> | [SelectBlockToPreviousWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockToPreviousWordAction) |
| <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>Alt</kbd>+<kbd>Right Arrow</kbd> | [SelectBlockToNextWordAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.SelectBlockToNextWordAction) |
