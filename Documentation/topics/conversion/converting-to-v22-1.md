---
title: "Converting to v22.1"
page-title: "Converting to v22.1 - Conversion Notes"
order: 87
---
# Converting to v22.1

The 22.1 version made several minor API changes in the SyntaxEditor Python Language Add-on. A new [ToggleSwitch](../shared/windows-controls/toggle-switch.md) control (Shared Library) also resulted in renaming the existing ToggleSwitch (Guage) as [FlipSwitch](xref:@ActiproUIRoot.Controls.Gauge.FlipSwitch) to avoid ambiguity.

## SyntaxEditor Drag and Drop Updates

The [Drag and Drop](../syntaxeditor/user-interface/input-output/drag-drop.md) functionality has been enhanced to provide more control over the experience.

Previously, any non-`null` value assigned to the [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.Text) property within a handler for the [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).[PasteDragDrop](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor.PasteDragDrop) event would indicate that drag-and-drop was allowed, but this limited advanced scenarios such as drag-and-drop with custom objects or dropping onto read-only documents.  Going forward, the accepted operation assigned through [PasteDragDropEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs).[DragEventArgs](xref:@ActiproUIRoot.Controls.SyntaxEditor.PasteDragDropEventArgs.DragEventArgs) will be used to indicate `Copy`, `Move`, or `None` operations. Refer to the [Drag and Drop](../syntaxeditor/user-interface/input-output/drag-drop.md) topic for details on customizing drag operations.

## SyntaxEditor Tagging Updates

Previously, any [ITagger<T>](xref:ActiproSoftware.Text.Tagging.ITagger`1) which implemented the `IDisposable` interface would be disposed when detached from an instance of the [TagAggregatorBase<T>](xref:ActiproSoftware.Text.Tagging.TagAggregatorBase`1) class.  Since taggers are frequently reused, this could lead to issues if the tagger was accessed after it was disposed.

New [ITaggerBase](xref:ActiproSoftware.Text.Tagging.ITaggerBase).[NotifyTagAggregatorAttached](xref:ActiproSoftware.Text.Tagging.ITaggerBase.NotifyTagAggregatorAttached*) and [ITaggerBase](xref:ActiproSoftware.Text.Tagging.ITaggerBase).[NotifyTagAggregatorDetached](xref:ActiproSoftware.Text.Tagging.ITaggerBase.NotifyTagAggregatorDetached*) methods have been added for more granular control over the lifecycle of a tagger. Taggers which derive from [TaggerBase<T>](xref:ActiproSoftware.Text.Tagging.Implementation.TaggerBase`1) can override the [OnTagAggregatorAttached](xref:ActiproSoftware.Text.Tagging.Implementation.TaggerBase`1.OnTagAggregatorAttached*) and [OnTagAggregatorDetached](xref:ActiproSoftware.Text.Tagging.Implementation.TaggerBase`1.OnTagAggregatorDetached*) methods to respond to these notifications and more accurately determine if/when a tagger should be disposed.

The following changes were made to tagging-related interfaces:

- New [ITaggerBase](xref:ActiproSoftware.Text.Tagging.ITaggerBase) interface added which is implemented by [ITagger<T>](xref:ActiproSoftware.Text.Tagging.ITagger`1).
- The `Close` method, `Closed` event, and `TagsChanged` event were moved from [ITagger<T>](xref:ActiproSoftware.Text.Tagging.ITagger`1) to [ITaggerBase](xref:ActiproSoftware.Text.Tagging.ITaggerBase).
- New [ITagAggregatorBase](xref:ActiproSoftware.Text.Tagging.ITagAggregatorBase) interface added which is implemented by [ITagAggregator<T>](xref:ActiproSoftware.Text.Tagging.ITagAggregator`1).
- The `TagsChanged` event was moved from [ITagAggregator<T>](xref:ActiproSoftware.Text.Tagging.ITagAggregator`1) to [ITagAggregatorBase](xref:ActiproSoftware.Text.Tagging.ITagAggregatorBase).

## SyntaxEditor Python Language Add-on Updates

The [Python](../syntaxeditor/python-language-addon/python/index.md) parser grammar has been updated to support v3.9.5 syntax.

Python v2.x has been officially end of life for some time now, so we removed support for it.  The `PythonVersion` enum was removed, and all APIs that had a `PythonVersion` parameter have had that parameter removed.

## SyntaxEditor .NET Languages Add-on Updates

The [C#](../syntaxeditor/dotnet-languages-addon/csharp/index.md) parser grammar has been updated to support v8.0 syntax.

## SyntaxEditor RegexCompletionItemMatcherBase Updates

The [RegexCompletionItemMatcherBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.RegexCompletionItemMatcherBase).[GetRegex](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.RegexCompletionItemMatcherBase.GetRegex*) method added an additional argument in v22.1.1 to indicate if the regular expression should include capture groups. Previously, all regular expressions included the capture groups, and this could impact matching performance when capture groups were unnecessary.

Any custom classes which derive from [RegexCompletionItemMatcherBase](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.RegexCompletionItemMatcherBase) will need to add the additional argument and update the logic to only use capture groups (i.e., parenthesis) in the pattern when requested.

## ToggleSwitch Control (Guage Library) Renamed to FlipSwitch

To avoid ambiguity with a new [ToggleSwitch](../shared/windows-controls/toggle-switch.md) control (Shared Library), the existing Actipro Gauge `ToggleSwitch` control has been renamed to [FlipSwitch](xref:@ActiproUIRoot.Controls.Gauge.FlipSwitch).  The following related types and members have also been renamed:

- `ToggleSwitch.RenderToggleSwitch` method renamed to [FlipSwitch](xref:@ActiproUIRoot.Controls.Gauge.FlipSwitch).[RenderFlipSwitch](xref:@ActiproUIRoot.Controls.Gauge.FlipSwitch.RenderFlipSwitch*).
- `ToggleSwitchType` enumeration renamed to [FlipSwitchType](xref:@ActiproUIRoot.Controls.Gauge.FlipSwitchType).
- `ToggleSwitchAutomationPeer` class renamed as [FlipSwitchAutomationPeer](xref:@ActiproUIRoot.Controls.Gauge.Automation.Peers.FlipSwitchAutomationPeer).

## PartEditBoxCommitTriggers Changes (Editors Library)

The [PartEditBoxCommitTriggers](xref:@ActiproUIRoot.Controls.Editors.PartEditBoxCommitTriggers) enumeration determines the rules by which typed/changed values are committed to an edit box's [Value](xref:@ActiproUIRoot.Controls.Editors.Primitives.PartEditBoxBase`1.Value) property.  By default, it commits on focus loss, spinner change, and `Enter` key press.

Commit on focus loss didn't used to be an option and always occurred.  For this version, we've added an explicit `LostFocus` option to the enumeration for customers who might not want a commit to occur unless the end user pressed `Enter`.  The `Default` and `All` values include the `LostFocus` option so that they behave the same as before.  However, if any other custom values were previously used, you must now include the `LostFocus` value to retain the same behavior.

As part of these changes, the former `None` option has been renamed to `Manual`.  Usage of the old `None` option should be replaced with the new `LostFocus` option to retain the same behavior as before.

## Windows 11 WindowChrome Corner Kind

The [WindowChrome](../themes/windowchrome.md) class has a new [WindowChrome](xref:@ActiproUIRoot.Themes.WindowChrome).[CornerKind](xref:@ActiproUIRoot.Themes.WindowChrome.CornerKind) property that can be set to a [WindowChromeCornerKind](xref:@ActiproUIRoot.Themes.WindowChromeCornerKind) value.

This default `Rounded` value will use the system to render rounded borders on Windows 11 windows, matching how non-chromed windows appear.  Windows in Windows 10 or earlier will continue to render with square borders as before.

## ThemeManager.CurrentTheme Validation

The [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentTheme](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentTheme) property setter will now throw an exception if the specified theme name is not for a predefined theme or a registered custom theme, thereby warning you that you have not registered a custom theme properly before attempting to use it.
