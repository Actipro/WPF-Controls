---
title: "Converting to v19.1"
page-title: "Converting to v19.1 - Conversion Notes"
order: 90
---
# Converting to v19.1

The 19.1 version made some large updates and improvements to the SyntaxEditor product for better cross-platform support.  The infrastructure changes required some breaking changes and some API cleanup was performed at the same time.  This enables the products to stay in sync more easily moving forward.

A number of new feature areas were added, described in the Sample Browser's Release History.

This documentation topic lists specifics on the breaking changes that were made and how to handle them.

## New Feature Details

Many new features were added in the 19.1 version.  Please see the "Release History" in the Sample Browser for a list of all new features.

## Highlighting Style Changes

To simplify the highlighting style API and make it more cross-platform compatible, brushes have been replaced by color values.  Some light refactoring of naming was made for better compatibility with XAML terminology.

### Brushes Replaced by Colors

The `IHighlightingStyle.BorderBrush`, `StrikethroughBrush`, and `UnderlineBrush` properties were renamed to [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle).[BorderColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderColor), [StrikethroughColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughColor), and [UnderlineColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineColor) respectively.  The `Brush` property values were replaced by `Color`.

### Highlighting Style Viewer QuickStart Updated

The "Highlighting Style Viewer" QuickStart has been updated to use controls for editing `Color` values instead of the previously-used `Brush` values in highlighting styles.  Please reference the updated QuickStart when migrating any similar dialogs in your own apps.

### Enumeration Changes

The new cross-platform rendering framework used by SyntaxEditor has [LineKind](xref:@ActiproUIRoot.Controls.Rendering.LineKind) and [TextLineWeight](xref:@ActiproUIRoot.Controls.Rendering.TextLineWeight) enumerations, both defined in the Shared Library.  The `HighlightingStyleLineStyle` enumeration was replaced with [LineKind](xref:@ActiproUIRoot.Controls.Rendering.LineKind).  Please use the `LineKind.None` value in scenarios where `HighlightingStyleLineStyle.Default` was previously used.  Likewise, the `HighlightingStyleLineWeight` enumeration was replaced with [TextLineWeight](xref:@ActiproUIRoot.Controls.Rendering.TextLineWeight).

The `IHighlightingStyle.BorderStyle`, `StrikethroughStyle`, and `UnderlineStyle` properties were renamed to [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle).[BorderKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderKind), [StrikethroughKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughKind), and [UnderlineKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineKind) respectively and updated to use the [LineKind](xref:@ActiproUIRoot.Controls.Rendering.LineKind) property.

The `HighlightingStyleBorderCornerStyle` enumeration was renamed to [HighlightingStyleBorderCornerKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.HighlightingStyleBorderCornerKind), and the `SinglePixelRounded` value was renamed to `Rounded`.  The `IHighlightingStyle.BorderCornerStyle` property was renamed to [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle).[BorderCornerKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderCornerKind).

## Selection and Caret Changes

The caret is always at the "end" of an editor view selection.  Prior SyntaxEditor versions had a single selection, but the 19.1 version allows multiple selections.

### Caret Object Removed

The `SyntaxEditor.Caret` property has been removed and caret location-related information is now available via the selection API.

The `SyntaxEditor.Caret.CharacterColumn`, `DisplayCharacterColumn`, `Offset`, and `Position` properties have moved to the [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[CaretCharacterColumn](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretCharacterColumn), the [CaretDisplayCharacterColumn](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretDisplayCharacterColumn), the [CaretOffset](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretOffset), and the [CaretPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CaretPosition) properties respectively, accessible via `editor.ActiveView.Selection`.

The properties above return the primary selection/caret's information.  Multiple selections can be enumerated via the view's [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[Ranges](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.Ranges) collection property.  There is one entry for each selection within that collection, and the entries give the position ranges of the selections.  The "end" position is where the caret is for each selection.

The `SyntaxEditor.Caret.BlinkInterval`, `InsertStyle`, `InsertWidth`, `OverwriteStyle`, and `OverwriteWidth` properties have moved to the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CaretBlinkInterval](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretBlinkInterval), the [CaretInsertKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretInsertKind), the [CaretInsertWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretInsertWidth), the [CaretOverwriteKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretOverwriteKind), and the [CaretOverwriteWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CaretOverwriteWidth) properties respectively,

The `CaretStyle` enumeration was renamed to [CaretKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.CaretKind) for better compatibility with XAML terminology.

### Selection Changes for Multiple Selections

The `IEditorViewSelection.PreferredHorizontalLocation` property was changed to a [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[GetPreferredCaretHorizontalLocations](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.GetPreferredCaretHorizontalLocations*) method.  The [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[SelectRange](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.SelectRange*) method overloads that take an "updatePreferredHorizontalLocation" parameter have been removed since this information is pulled on-demand now based on [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[CreateBatch](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CreateBatch*) method options.

The `IEditorViewSelection.GetCharacterIndexRanges` method was removed.  Use the [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[GetCharacterIndexRanges](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.GetCharacterIndexRanges*) method instead, which has had its "allowWholeLines" parameter removed.

### Selection Changes for Batches

A new [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[CreateBatch](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CreateBatch*) method has been added that allows for passing options and grouping multiple selection changes into a single transaction.

The `IEditorView.ScrollToCaretOnSelectionChange` property was removed.  A batch option for preventing scroll-to-caret can be specified when creating a batch.

The new [IEditorViewSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection).[CreateBatch](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewSelection.CreateBatch*) method can wrap multiple selection changes to ensure that only one selection change event occurs, as the outermost batch is completed.  Related to this, the old `IEditorView.AreSelectionEventsSuspended`, `ResumeSelectionEvents`, and `SuspendSelectionEvents` members have been removed.

### Text Change Selections

The `ITextChange.SelectionPositionRange` property was replaced with [ITextChange](xref:ActiproSoftware.Text.ITextChange).[PreSelectionPositionRanges](xref:ActiproSoftware.Text.ITextChange.PreSelectionPositionRanges) and [PostSelectionPositionRanges](xref:ActiproSoftware.Text.ITextChange.PostSelectionPositionRanges) properties that store the selections for the active view before/after the text change is applied.

The `IUndoableTextChange.SelectionPositionRange` and `PostSelectionPositionRange` properties were replaced with [IUndoableTextChange](xref:ActiproSoftware.Text.Undo.IUndoableTextChange).[PreSelectionPositionRanges](xref:ActiproSoftware.Text.Undo.IUndoableTextChange.PreSelectionPositionRanges) and [PostSelectionPositionRanges](xref:ActiproSoftware.Text.Undo.IUndoableTextChange.PostSelectionPositionRanges) properties that store the selections for the active view before/after the text change is applied.

The `UndoHistory.ApplySelectionPositionRange` method was removed and can be replaced by setting the [ITextChange](xref:ActiproSoftware.Text.ITextChange).[PostSelectionPositionRanges](xref:ActiproSoftware.Text.ITextChange.PostSelectionPositionRanges) property directly.

### Event Arguments

The `EditorViewSelectionEventArgs.LastCaretPosition` property was renamed to [EditorViewSelectionEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorViewSelectionEventArgs).[PreviousCaretPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorViewSelectionEventArgs.PreviousCaretPosition).

## IntelliPrompt Changes

For improving the clarity of the property result, the `CommonImage` enumeration was renamed to [CommonImageKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageKind), and the `CommonImageSourceProvider.ImageType` property was renamed to [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider).[ImageKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider.ImageKind).

The `QuickInfoProviderBase.CanTrackMouseInput` property was renamed to [QuickInfoProviderBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase).[CanTrackPointerInput](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.QuickInfoProviderBase.CanTrackPointerInput).

The [ICompletionItemCollection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionItemCollection) interface was updated to implement `INotifyCollectionChanged`.

The [ICompletionFilter](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionFilter).[Filter](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.ICompletionFilter.Filter*) method and the [CompletionFilterPredicate](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CompletionFilterPredicate) delegate were updated to use a new [CompletionFilterResult](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.CompletionFilterResult) enumeration so that the intended result is clearer.

### Quick Info and Parameter Info Theme Support

Quick info and parameter info tips now render their backgrounds based on the [current theme](../themes/index.md).  In particular, when the application is in a Metro Dark theme, the info tips will now have a dark background.  This allows images on the info tips to render better.  New AssetResourceKeys.IntelliPromptToolTip* brush resources are used for the theme-oriented info tips.

To support this change, it is recommended that foreground colors used in [Content Providers](../syntaxeditor/user-interface/intelliprompt/popup-content-providers.md) are based on the current theme in the SyntaxEditor control (assuming SyntaxEditor is rendering in a theme similar to the application theme), or at least are set to render well in the current application theme.

The [HtmlContentProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.HtmlContentProvider) class has several static properties for common foreground colors such as neutral, comment, keyword, and type names.  These properties access the related classification type entries in the ambient highlighting style registry and return the foreground color.

## Adornments Changes

### Change Reasons

A new [AdornmentChangeReason](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.AdornmentChangeReason) enumeration has been added, allowing add/remove adornment calls to indicate the reason behind the change.

The [IAdornmentLayer](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer).[AddAdornment](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer.AddAdornment*), [RemoveAdornment](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer.RemoveAdornment*), [RemoveAdornments](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer.RemoveAdornments*), and [RemoveAllAdornments](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer.RemoveAllAdornments*) methods have all been updated with a reason parameter.

The [IntraTextAdornmentManagerBase<T, U>](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.Implementation.IntraTextAdornmentManagerBase`2) and [DecorationAdornmentManagerBase<T, U>](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.Implementation.DecorationAdornmentManagerBase`2)'s `AddAdornment` methods were updated with a reason parameter.

### Adornment View Line Associations

The [IAdornmentLayer](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer).[AddAdornment](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer.AddAdornment*) overload that accepts a snapshot range has been updated with a new [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) parameter that can be specified if the adornment is associated with a certain view line.  The value flows into the new [IAdornment](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornment).[ViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornment.ViewLine) property.

### Element-Based and Draw-Based Adornments

When updating how SyntaxEditor renders itself to support the same codebase across multiple platforms (WPF, UWP, and WinForms), we had to alter how adornments render.  Most of our pre-defined adornments now use a new draw-based mechanism where they render themselves in the rendering pipeline instead of using UI elements to render themselves.  New [IAdornmentLayer](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer).[AddAdornment](xref:@ActiproUIRoot.Controls.SyntaxEditor.Adornments.IAdornmentLayer.AddAdornment*) overloads support the creation of draw-based adornments.

While adornment layers still stack in the same order as before, element-based adornments will always render "on top" of draw-based adornments since the elements are stacked on top of the drawing canvas element.  Adornments like alternating row highlights should be draw-based so that they stack appropriately with other pre-defined adornment layers.  Adornments like intra-line controls can remain element-based since they generally appear on top of other view content anyhow.

@if (wpf) {

## Vertical View Split Option Removed

With the advent of advanced tabbed docking windows, we simplified our internal structures by removing support for vertical splits.  Horizontal splits are still supported.

The [EditorViewPlacement](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorViewPlacement) enumeration now only has `Lower` and `Upper` values.  The `SyntaxEditor.CanSplitVertically`, `HasVerticalSplit`, and `VerticalSplitPercentage` properties were removed.

}

## IEditorView Scroll State Changes

The `IEditorView.FirstVisibleX` and `FirstVisiblePosition` properties were removed and replaced with a new read-only [ITextView](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView).[ScrollState](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.ScrollState) property that returns a [TextViewScrollState](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextViewScrollState) object.  To set the scroll state, use the new [IEditorViewScroller](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller).[ScrollTo](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewScroller.ScrollTo*) method, accessible via `editor.ActiveView.Scroller.ScrollTo`.

## Indicator Changes

The [IIndicatorTag](xref:ActiproSoftware.Text.Tagging.IIndicatorTag) interface has been updated to use the fast new rendering mechanism for drawing glyphs instead of making UI elements.  Accordingly the `CreateGlyph` method has been removed and replaced with a [DrawGlyph](xref:ActiproSoftware.Text.Tagging.IIndicatorTag.DrawGlyph*) method.

See the "CustomIndicatorTag" class in the "Indicators - Custom" QuickStart for an example implementation of the [DrawGlyph](xref:ActiproSoftware.Text.Tagging.IIndicatorTag.DrawGlyph*) method.

## View Line Changes

### IEditorViewLine and IPrinterViewLine Interfaces Merged Into ITextViewLine

The former `IEditorViewLine` and `IPrinterViewLine` interfaces have been merged into their base [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine) interface.

The same is true of the `IEditorViewLineCollection` and `IPrinterViewLineCollection` interfaces being merged into their base [ITextViewLineCollection](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLineCollection) interface.

### Bounds and Size Properties Changed

For better clarity, the [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine).[Bounds](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.Bounds) and [Size](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.Size) properties have been updated to include the height of the top/bottom intra-line adornment margins.

New [TextBounds](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.TextBounds) and [TextSize](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.TextSize) properties were added, with the former return values of the `Bounds` and `Size` properties that exclude adornment margin space.

### VisibleStartPosition Property Added

A new [ITextViewLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine).[VisibleStartPosition](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextViewLine.VisibleStartPosition) property was added that returns the first visible position on the view line.  Replace any prior `ITextViewLine.DisplayLineNumber` property call with the new `ITextViewLine.VisibleStartPosition.DisplayLine` property.

## Selection Brush Property Changes

The `SyntaxEditor.SelectionBackgroundActive` and `SelectionBackgroundInactive` properties were renamed to [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[SelectedTextBackground](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.SelectedTextBackground) and [InactiveSelectedTextBackground](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.InactiveSelectedTextBackground) respectively.

The `SyntaxEditor.SelectionBorderActiveBrush` and `SelectionBorderInactiveBrush` properties were removed since selection borders are no longer supported.

## Searching Changes

The `SyntaxEditor.IsIncrementalSearchResultHighlightingEnabled` property was renamed to [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsSearchResultHighlightingEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsSearchResultHighlightingEnabled).

The `EditorSearchView.IsModeToolBarVisible` property was renamed to [CanToggleReplace](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.SearchViewBase.CanToggleReplace), and the `Mode` property was renamed to [IsReplaceVisible](xref:@ActiproUIRoot.Controls.SyntaxEditor.Primitives.SearchViewBase.IsReplaceVisible).  The boolean property is easier to bind with.  The `EditorSearchMode` enumeration was removed.

## Input Changes

The `IEditorViewMouseInputEventSink` interface was renamed to [IEditorViewPointerInputEventSink](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewPointerInputEventSink), and the interface members were updated to use a more generic naming convention along with cross-platform compatible event arguments.

The `IEditorView.StartMouseSelection` method was renamed to [IEditorView](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView).[StartPointerSelection](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorView.StartPointerSelection*) and its parameter type was changed.

The `SyntaxEditor.CanMoveCaretOnMouseRightClick` property was renamed to [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[CanMoveCaretForSecondaryPointerButton](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.CanMoveCaretForSecondaryPointerButton).

Code block selection features (available in some languages) changed keyboard shortcuts from <kbd>Ctrl</kbd>+<kbd>+</kbd> to <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>+</kbd> for expansion, and <kbd>Ctrl</kbd>+<kbd>-</kbd> to <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>-</kbd> for contraction.

## Line Commenter Changes

The `ILineCommenter.Comment` and `Uncomment` methods were updated to have snapshot and [ITextPositionRangeCollection](xref:ActiproSoftware.Text.ITextPositionRangeCollection) parameters instead of a single [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange).  These changes allow line commenters to facilitate multiple selections.  The built-in line commenter implementations were updated to support multiple selections.

> [!NOTE]
> An [ITextPositionRangeCollection](xref:ActiproSoftware.Text.ITextPositionRangeCollection) can be created via the static [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange).[CreateCollection](xref:ActiproSoftware.Text.TextPositionRange.CreateCollection*) method overloads.

## Text Formatter Changes

The [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter).[Format](xref:ActiproSoftware.Text.ITextFormatter.Format*) method was updated to have snapshot, [ITextPositionRangeCollection](xref:ActiproSoftware.Text.ITextPositionRangeCollection), and [TextFormatMode](xref:ActiproSoftware.Text.TextFormatMode) (All or Ranges options) parameters instead of a single [TextSnapshotRange](xref:ActiproSoftware.Text.TextSnapshotRange) to facilitate multiple selections.  The built-in text formatter implementations were updated to support multiple selections.

See the "Getting Started #13" QuickStart for an example of a text formatter using the newer API.

> [!NOTE]
> An [ITextPositionRangeCollection](xref:ActiproSoftware.Text.ITextPositionRangeCollection) can be created via the static [TextPositionRange](xref:ActiproSoftware.Text.TextPositionRange).[CreateCollection](xref:ActiproSoftware.Text.TextPositionRange.CreateCollection*) method overloads.

This code shows general logic that can be used for an [ITextFormatter](xref:ActiproSoftware.Text.ITextFormatter).[Format](xref:ActiproSoftware.Text.ITextFormatter.Format*) method.  It calls into a `FormatCore` method that is where you'd implement the actual text modification logic for the specified snapshot range.

```csharp
public void Format(ITextSnapshot snapshot, ITextPositionRangeCollection selectionPositionRanges, TextFormatMode mode) {
	if (snapshot == null)
		throw new ArgumentNullException("snapshot");
	if ((selectionPositionRanges == null) || (selectionPositionRanges.Count == 0))
		throw new ArgumentNullException("selectionPositionRanges");

	// Changes must occur sequentially so that we can use unmodified offsets while looping over the document
	var options = new TextChangeOptions();
	options.OffsetDelta = TextChangeOffsetDelta.SequentialOnly;
	options.RetainSelection = true;
	var change = snapshot.Document.CreateTextChange(TextChangeTypes.AutoFormat, options);

	// Get the snapshot ranges to format
	var snapshotRanges = (mode == TextFormatMode.All ?
		new TextSnapshotRange[] { snapshot.SnapshotRange } :
		selectionPositionRanges.Select(pr => new TextSnapshotRange(snapshot, snapshot.PositionRangeToTextRange(pr))).ToArray()
		);

	// Loop through the snapshot ranges
	foreach (var snapshotRange in snapshotRanges)
		this.FormatCore(change, snapshotRange);

	// Apply the changes
	if (change.Operations.Count > 0)
		change.Apply();
}
```

New methods make it easy to call the current syntax language's text formatter, if one is available.

```csharp
editor.ActiveView.TextChangeActions.FormatDocument();
editor.ActiveView.TextChangeActions.FormatSelection();
```

## DuplicateLine Edit Action Changed to Duplicate

The old `DuplicateLine` edit action has been updated to work with selected text and has been renamed to `Duplicate`.  If there is no selection, the entire document line will be duplicated.  If there is a selection, it will be inserted and selected immediately after the previous selection.

The `TextChangeTypes.DuplicateLine` property was renamed to [TextChangeTypes](xref:ActiproSoftware.Text.TextChangeTypes).[Duplicate](xref:ActiproSoftware.Text.TextChangeTypes.Duplicate).

The `EditorCommands.DuplicateLine` property was renamed to [EditorCommands](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands).[Duplicate](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditorCommands.Duplicate).

The `DuplicateLineAction` class was renamed to [DuplicateAction](xref:@ActiproUIRoot.Controls.SyntaxEditor.EditActions.DuplicateAction) and the corresponding `IEditorViewTextChangeActions.DuplicateLine` method was renamed to [IEditorViewTextChangeActions](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewTextChangeActions).[Duplicate](xref:@ActiproUIRoot.Controls.SyntaxEditor.IEditorViewTextChangeActions.Duplicate*).

## ANTLR and Irony Add-ons Not Installed By Default

The optional add-ons that integrate ANTLR v3 And Irony parsers with SyntaxEditor are no longer installed by default.  The [ANTLR Add-on](../syntaxeditor/antlr-addon/index.md) and [Irony Add-on](../syntaxeditor/irony-addon/index.md) topics give more detail on the options to pick during WPF Controls setup to install those.

## TextStylePreview and NavigableSymbolSelector Namespace Changed

The [TextStylePreview](xref:@ActiproUIRoot.Controls.SyntaxEditor.TextStylePreview) and [NavigableSymbolSelector](xref:@ActiproUIRoot.Controls.SyntaxEditor.NavigableSymbolSelector) controls have been moved from the `SyntaxEditor.Primitives` namespace up to primary `SyntaxEditor` namespace where the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control is located.

## Other Miscellaneous Changes

The `SyntaxEditor.CharacterWidth`, `LineHeight`, and `SpaceWidth` properties were replaced with [ITextView](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView).[DefaultCharacterWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.DefaultCharacterWidth), [DefaultLineHeight](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.DefaultLineHeight), and [DefaultSpaceWidth](xref:@ActiproUIRoot.Controls.SyntaxEditor.ITextView.DefaultSpaceWidth) properties respectively.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[IsOverwriteModeActiveChanged](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.IsOverwriteModeActiveChanged) event arguments were updated.

The `SyntaxEditor.CanScrollPastDocumentEnd` property was flipped to a default of `true` since view lines can support variable heights via font options and intra-line adornments.  Scrolling past document end can be turned back off if those features are not being used.

The `SyntaxEditor.IsMouseWheelZoomEnabled` property was removed in favor of the new [ZoomModesAllowed](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.ZoomModesAllowed) property.

The [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[LineNumberMarginFontSize](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.LineNumberMarginFontSize) property's default was changed to `0`.  When `0`, it will use the same font size as `SyntaxEditor.FontSize`.

The `SyntaxEditor.ZoomLevelIndicatorTemplate` property and default indicator were removed since zoom level is usually indicated in external UI.
