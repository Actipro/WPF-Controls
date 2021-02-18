---
title: "Default Key Bindings"
page-title: "Default Key Bindings - SyntaxEditor Input/Output Features"
order: 6
---
# Default Key Bindings

Many keyboard shortcuts are automatically bound to allow keyboard access to most of the built-in [edit actions](edit-actions.md).

The following tables describe the default key bindings that are in place when the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor) control is created.  These can be modified by using the [SyntaxEditor](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.SyntaxEditor).`InputBindings` collection.

## Clipboard/Undo Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + C | [CopyToClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CopyToClipboardAction) |
| Ctrl + Ins | [CopyToClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CopyToClipboardAction) |
| Ctrl + X | [CutToClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CutToClipboardAction) |
| Shift + Del | [CutToClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CutToClipboardAction) |
| Ctrl + L | [CutLineToClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CutLineToClipboardAction) |
| Ctrl + V | [PasteFromClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.PasteFromClipboardAction) |
| Shift + Ins | [PasteFromClipboardAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.PasteFromClipboardAction) |
| Ctrl + Y | [RedoAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.RedoAction) |
| Ctrl + Shift + Z | [RedoAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.RedoAction) |
| Ctrl + Z | [UndoAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.UndoAction) |

## Deletion Edit Actions

| Key | Edit Action |
|-----|-----|
| Backspace | [BackspaceAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.BackspaceAction) |
| Shift + Backspace | [BackspaceAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.BackspaceAction) |
| Ctrl + Backspace | [BackspaceToPreviousWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.BackspaceToPreviousWordAction) |
| Del | [DeleteAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.DeleteAction) |
| Ctrl + Shift + L | [DeleteLineAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.DeleteLineAction) |
| Ctrl + Del | [DeleteToNextWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.DeleteToNextWordAction) |

## Insertion Edit Actions

| Key | Edit Action |
|-----|-----|
| Enter | [InsertLineBreakAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.InsertLineBreakAction) |
| Shift + Enter | [InsertLineBreakAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.InsertLineBreakAction) |
| Ctrl + Enter | [OpenLineAboveAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.OpenLineAboveAction) |
| Ctrl + Shift + Enter | [OpenLineBelowAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.OpenLineBelowAction) |

## IntelliPrompt Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + Space | [RequestIntelliPromptAutoCompleteAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.RequestIntelliPromptAutoCompleteAction) |
| Ctrl + Shift + Space | [RequestIntelliPromptParameterInfoSessionAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.RequestIntelliPromptParameterInfoSessionAction) |

## Miscellaneous Edit Actions

| Key | Edit Action |
|-----|-----|
| Tab | [InsertTabStopOrIndentAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.InsertTabStopOrIndentAction) |
| Shift + Tab | [RemoveTabStopOrOutdentAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.RemoveTabStopOrOutdentAction) |
| Ctrl + U | [MakeLowercaseAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MakeLowercaseAction) |
| Ctrl + Shift + U | [MakeUppercaseAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MakeUppercaseAction) |
| Insert | [ToggleOverwriteModeAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ToggleOverwriteModeAction) |
| Ctrl + T | [TransposeCharactersAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.TransposeCharactersAction) |
| Ctrl + Shift + T | [TransposeWordsAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.TransposeWordsAction) |
| Shift + Alt + T | [TransposeLinesAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.TransposeLinesAction) |
| Alt + Up | [MoveSelectedLinesUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveSelectedLinesUpAction) |
| Alt + Down | [MoveSelectedLinesDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveSelectedLinesDownAction) |
| Ctrl + Num+ | [ZoomInAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ZoomInAction) |
| Ctrl + Num- | [ZoomOutAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ZoomOutAction) |
| Ctrl + 0 | [ResetZoomLevelAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ResetZoomLevelAction) |

## Movement Edit Actions

| Key | Edit Action |
|-----|-----|
| Down | [MoveDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveDownAction) |
| Up  | [MoveUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveUpAction) |
| Left | [MoveLeftAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveLeftAction) |
| Right | [MoveRightAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveRightAction) |
| Ctrl + Left | [MoveToPreviousWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToPreviousWordAction) |
| Ctrl + Right | [MoveToNextWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToNextWordAction) |
| Home | [MoveToLineStartAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToLineStartAction) |
| End | [MoveToLineEndAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToLineEndAction) |
| Ctrl + Home | [MoveToDocumentStartAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToDocumentStartAction) |
| Ctrl + End | [MoveToDocumentEndAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToDocumentEndAction) |
| Page Up | [MovePageUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MovePageUpAction) |
| Page Down | [MovePageDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MovePageDownAction) |
| Ctrl + Page Up | [MoveToVisibleTopAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToVisibleTopAction) |
| Ctrl + Page Down | [MoveToVisibleBottomAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToVisibleBottomAction) |
| Ctrl + ] | [MoveToMatchingBracketAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.MoveToMatchingBracketAction) |

## Scroll Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + Down | [ScrollDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ScrollDownAction) |
| Ctrl + Up | [ScrollUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ScrollUpAction) |

## Search Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + F | [FindAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.FindAction) |
| F3  | [FindNextAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.FindNextAction) |
| Ctrl + F3 | [FindNextSelectedAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.FindNextSelectedAction) |
| Shift + F3 | [FindPreviousAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.FindPreviousAction) |
| Ctrl + Shift + F3 | [FindPreviousSelectedAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.FindPreviousSelectedAction) |
| Ctrl + H | [ReplaceAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ReplaceAction) |
| Ctrl + I | [IncrementalSearchAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.IncrementalSearchAction) |
| Ctrl + Shift + I | [ReverseIncrementalSearchAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.ReverseIncrementalSearchAction) |

## Selection Edit Actions

| Key | Edit Action |
|-----|-----|
| Ctrl + Shift + Num- | [CodeBlockSelectionContractAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CodeBlockSelectionContractAction) |
| Ctrl + Shift + Num+ | [CodeBlockSelectionExpandAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CodeBlockSelectionExpandAction) |
| Escape | [CollapseSelectionAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.CollapseSelectionAction) |
| Shift + Down | [SelectDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectDownAction) |
| Shift + Up | [SelectUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectUpAction) |
| Shift + Left | [SelectLeftAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectLeftAction) |
| Shift + Right | [SelectRightAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectRightAction) |
| Ctrl + Shift + Left | [SelectToPreviousWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToPreviousWordAction) |
| Ctrl + Shift + Right | [SelectBlockToNextWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockToNextWordAction) |
| Shift + Home | [SelectToLineStartAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToLineStartAction) |
| Shift + End | [SelectToLineEndAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToLineEndAction) |
| Ctrl + Shift + Home | [SelectToDocumentStartAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToDocumentStartAction) |
| Ctrl + Shift + End | [SelectToDocumentEndAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToDocumentEndAction) |
| Shift + Page Up | [SelectPageUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectPageUpAction) |
| Shift + Page Down | [SelectPageDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectPageDownAction) |
| Ctrl + Shift + Page Up | [SelectToVisibleTopAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToVisibleTopAction) |
| Ctrl + Shift + Page Down | [SelectToVisibleBottomAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToVisibleBottomAction) |
| Ctrl + A | [SelectAllAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectAllAction) |
| Ctrl + Shift + W | [SelectWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectWordAction) |
| Ctrl + Shift + ] | [SelectToMatchingBracketAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectToMatchingBracketAction) |
| Shift + Alt + Down | [SelectBlockDownAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockDownAction) |
| Shift + Alt + Up | [SelectBlockUpAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockUpAction) |
| Shift + Alt + Left | [SelectBlockLeftAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockLeftAction) |
| Shift + Alt + Right | [SelectBlockRightAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockRightAction) |
| Ctrl + Shift + Alt + Left | [SelectBlockToPreviousWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockToPreviousWordAction) |
| Ctrl + Shift + Alt + Right | [SelectBlockToNextWordAction](xref:ActiproSoftware.Windows.Controls.SyntaxEditor.EditActions.SelectBlockToNextWordAction) |
