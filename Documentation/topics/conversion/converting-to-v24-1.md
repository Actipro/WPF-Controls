---
title: "Converting to v24.1"
page-title: "Converting to v24.1 - Conversion Notes"
order: 85
---
# Converting to v24.1

The 24.1 version made a number of infrastructure updates and improvements.

## Bars Exits Beta and Older Ribbon is Deprecated

The [Bars](../bars/index.md) product has exited the beta phase and is now ready for production scenarios.  The older [Ribbon](../ribbon/index.md) product has been deprecated in favor of the newer product and will be removed in a future release.

Projects should begin transitioning from Ribbon to Bars.  Please see the [Converting to v23.1](converting-to-v23-1.md) topic for additional details on converting to the newer product.

## Bars Button and Menu Item Command Updates

The [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton), [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton), [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem), and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) controls all have `Command` and `PopupOpeningCommand` properties.  While v23.1's logic did execute the `Command` when a split button or split menu item's button portion was clicked, and opening a popup on any of those controls did execute the `PopupOpeningCommand`, the can-execute checks of each command were not handled in a similar fashion.

In v23.1, only the `Command` was ever examined to determine if the entire control should be disabled.  This also caused issues in split buttons and split menu items where if the button portion's `Command` was disabled, there was then no way to open the popup since it was also disabled.  In v24.1, we refactored how commands work with these controls so that the can-execute checks are in line with the execute behavior.

The updated v24.1 behavior is that:
- [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) (with a sub-menu) use the `PopupOpeningCommand` can-execute result to determine if the control is enabled, falling back to a check on `Command` for backward compatibility if no `PopupOpeningCommand` is specified.
- [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) use the `Command` can-execute result to determine if the button portion is enabled and use the `PopupOpeningCommand` can-execute result to determine if the popup portion is enabled.  This allows the button and popup portions of the controls to be enabled/disabled independently of each other.

## Bars Changes to Multi-Row Layouts in Ribbon Groups

Ribbon groups in `Classic` layout mode already supported two and three-row layouts (like the **Font** group in Word) that adjusted based on available width.  The design for this feature originally required that all controls within a ribbon group participate in that same multi-row layout.

During the beta period, we had customers send feedback that they required the ability to support multi-row layout alongside other controls in the same group that did not participate in the multi-row layout.  This unfortunately required several small breaking changes to achieve, with update directions described below.

### RibbonGroup.CanUseMultiRowLayout and ThreeRowItemSortOrder Properties Removed

- Use a [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup) control instance in the [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup).`Items` property and wrap all prior items with the [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup) control instead.  This tells the items to use to a multi-row layout.
- Move the ribbon group's `ThreeRowItemSortOrder` property setting to the [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup).[ThreeRowItemSortOrder](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup.ThreeRowItemSortOrder) property instead to maintain the desired three-row sort order.

### RibbonGroupViewModel.CanUseMultiRowLayout and ThreeRowItemSortOrder Properties Removed

Similar to the controls mentioned above, when using MVVM, the [view models](../bars/mvvm-support.md) should be handled in the same way:

- Use a [RibbonMultiRowControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonMultiRowControlGroupViewModel) instance in the [RibbonGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonGroupViewModel).`Items` property and wrap all prior items with the [RibbonMultiRowControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonMultiRowControlGroupViewModel) control instead.  This tells the items to use to a multi-row layout.
- Move the ribbon group's `ThreeRowItemSortOrder` property setting to the [RibbonMultiRowControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonMultiRowControlGroupViewModel).[ThreeRowItemSortOrder](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonMultiRowControlGroupViewModel.ThreeRowItemSortOrder) property instead to maintain the desired three-row sort order.

### XAML Example of Changes

#### v23.1 XAML

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonTabItem Key="Home" ... >

	<bars:RibbonGroup Key="FontGroup" Label="Font" CanUseMultiRowLayout="True" ThreeRowItemSortOrder="0 2 1">

		<!-- Don't render separators next to this set of comboboxes -->
		<bars:RibbonControlGroup SeparatorMode="Never">
			<bars:BarComboBox x:Name="fontFamilyComboBox" RequestedWidth="115" KeyTipText="FF" IsStarSizingAllowed="True"
							  MenuResizeMode="Vertical" TextPath="Label" UseMenuItemAppearance="True" SelectedValuePath="Value" />
			<bars:BarComboBox x:Name="fontSizeComboBox" RequestedWidth="40" KeyTipText="FS"
							  MenuResizeMode="Vertical" TextPath="Label" UseMenuItemAppearance="True" SelectedValuePath="Value" />
		</bars:RibbonControlGroup>

		<!-- Larger and smaller font size buttons -->
		<bars:RibbonControlGroup>
			<bars:BarButton Key="Larger" SmallImageSource="/Images/GrowFont16.png" KeyTipText="FG" />
			<bars:BarButton Key="Smaller" SmallImageSource="/Images/ShrinkFont16.png" KeyTipText="FK" />
		</bars:RibbonControlGroup>

		<!-- Bold, italic, and underline buttons -->
		<bars:RibbonControlGroup>
			<bars:BarButton Key="Bold" SmallImageSource="/Images/Bold16.png" KeyTipText="B" />
			<bars:BarButton Key="Italic" SmallImageSource="/Images/Italic16.png" KeyTipText="I" />
			<bars:BarButton Key="Underline" SmallImageSource="/Images/Underline16.png" KeyTipText="U" />
		</bars:RibbonControlGroup>

	</bars:RibbonGroup>
	...

</bars:RibbonTabItem>
...
```

#### v24.1 XAML

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:RibbonTabItem Key="Home" ... >

	<bars:RibbonGroup Key="FontGroup" Label="Font">

		<bars:RibbonMultiRowControlGroup ThreeRowItemSortOrder="0 2 1">

			<!-- Don't render separators next to this set of comboboxes -->
			<bars:RibbonControlGroup SeparatorMode="Never">
				<bars:BarComboBox x:Name="fontFamilyComboBox" RequestedWidth="115" KeyTipText="FF" IsStarSizingAllowed="True"
								  MenuResizeMode="Vertical" TextPath="Label" UseMenuItemAppearance="True" SelectedValuePath="Value" />
				<bars:BarComboBox x:Name="fontSizeComboBox" RequestedWidth="40" KeyTipText="FS"
								  MenuResizeMode="Vertical" TextPath="Label" UseMenuItemAppearance="True" SelectedValuePath="Value" />
			</bars:RibbonControlGroup>

			<!-- Larger and smaller font size buttons -->
			<bars:RibbonControlGroup>
				<bars:BarButton Key="Larger" SmallImageSource="/Images/GrowFont16.png" KeyTipText="FG" />
				<bars:BarButton Key="Smaller" SmallImageSource="/Images/ShrinkFont16.png" KeyTipText="FK" />
			</bars:RibbonControlGroup>

			<!-- Bold, italic, and underline buttons -->
			<bars:RibbonControlGroup>
				<bars:BarButton Key="Bold" SmallImageSource="/Images/Bold16.png" KeyTipText="B" />
				<bars:BarButton Key="Italic" SmallImageSource="/Images/Italic16.png" KeyTipText="I" />
				<bars:BarButton Key="Underline" SmallImageSource="/Images/Underline16.png" KeyTipText="U" />
			</bars:RibbonControlGroup>

		</bars:RibbonMultiRowControlGroup>

	</bars:RibbonGroup>
	...

</bars:RibbonTabItem>
...
```

## Editors DateEditBox, DateTimeEditBox, and TimeEditBox Updates

When typing in new values within a [DateEditBox](xref:@ActiproUIRoot.Controls.Editors.DateEditBox), [DateTimeEditBox](xref:@ActiproUIRoot.Controls.Editors.DateTimeEditBox), or [TimeEditBox](xref:@ActiproUIRoot.Controls.Editors.TimeEditBox), `DateTime` parse methods would be used to convert the edited text to a `DateTime` value.  The downside of this was that no consideration was given to which edit box parts were actually displayed at the time.

An example of this is if you had a [TimeEditBox](xref:@ActiproUIRoot.Controls.Editors.TimeEditBox) with only hour and minute parts, and the original edit box value had seconds specified.  Changing the edit box's text would zero out the seconds component in the parsed `DateTime` value instead of retaining it.

In v24.1, text edits in these three edit boxes will retain all `DateTime` components from the original value for which edit box parts aren't displayed.

Note that [DateEditBox](xref:@ActiproUIRoot.Controls.Editors.DateEditBox) will still zero out its time components as before, unless its [CanRetainTime](xref:@ActiproUIRoot.Controls.Editors.DateEditBox.CanRetainTime) property is set to `true`.

## SyntaxEditor Native Dark Theme Support

Support for dark themes is now integrated directly into [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).  Refer to the [Dark Themes](../syntaxeditor/user-interface/styles/dark-themes.md) topic for details on concepts like [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) or [IHighlightingStyleColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette).

### Switching Themes

Previously, switching between light and dark themes would require multiple steps like:
1. Unregistering all classification types in a registry.
1. Re-registering all the classification types from multiple providers.
1. Re-loading language definition *.langdef* files.
1. For dark theme, importing a *.vssettings* file with colors configured for a dark theme.
1. Changing the [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider).[DefaultImageSet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider.DefaultImageSet) between light and dark color sets.

A new [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) class now takes care of all of those settings by default, and most developers will not have to do anything to support changing themes.  Check for any custom code that watches the [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentThemeChanged](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentThemeChanged) event and remove any SyntaxEditor logic related to the previous technique of switching themes.

If custom registries are used instead of [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry), those registries can be configured to automatically switch themes by calling [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager).[Manage](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager.Manage*) and passing the custom registry.

If any developer does not want the default functionality provided by [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager), the following code will disable the default features:

```csharp
// Disable automatically switching between light/dark color palettes
SyntaxEditorThemeManager.Unmanage(AmbientHighlightingStyleRegistry.Instance);

// Ensure the light color palette is the default (in case a dark theme was active before being unmanaged)
AmbientHighlightingStyleRegistry.Instance.CurrentColorPalette = AmbientHighlightingStyleRegistry.Instance.LightColorPalette;

// Disable changing image sets
SyntaxEditorThemeManager.IsCommonImageSetSynchronizationEnabled = false;
```

### Dark Colors

If a custom *.vssettings* file was used with preferred dark colors, those colors may not match the new defaults.  A special dark color palette can be initialized with preferred colors to use in dark themes on a per-registry basis.  The following example code demonstrates how to configure the default dark colors for the ambient registry:

```csharp
IHighlightingStyleRegistry registry = AmbientHighlightingStyleRegistry.Instance;
registry.DarkColorPalette.SetForeground("String", Color.FromArgb(255, 214, 157, 133));
registry.DarkColorPalette.SetForeground("PlainText", Color.FromArgb(255, 220, 220, 220));
registry.DarkColorPalette.SetBackground("PlainText", Color.FromArgb(255, 30, 30, 30));
```

> [!NOTE]
> Configuring the dark color palette is only necessary if languages are not updated with explicit dark styles.

### Language Updates

The [Language Designer Tool](../syntaxeditor/language-designer-tool/index.md) has been updated to support the configuration and preview of dark styles for classification types.  Developers are encouraged to use the tool to configure appropriate dark colors instead of explicitly populating the dark color palette, and many dark colors will be automatically configured based on built-in classification types and a pre-defined mapping of common light colors to dark colors.

All Actipro-provided languages have been updated with the new dark style configuration.

### New IHighlightingStyleRegistry Interface Members

The [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) interface has new members to support color palettes. Any custom classes that implement this interface without deriving from [HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleRegistry) will need to implement the following new members:
- [CurrentColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.CurrentColorPalette) property.
- [DarkColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.DarkColorPalette) property.
- [LightColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.LightColorPalette) property.
- [Unregister](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.Unregister*) method overload with the `clearColorPalettes` parameter.

## SyntaxEditor Enhancements

The following changes were made to enhance the capabilities of SyntaxEditor and might impact some applications.
- The default colors for several [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) instances on [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) have been changed to be more consistent with modern code editor expectations.  Some classification types were updated to remove opacity from the default color and, instead, always apply an opacity when the color is rendered.
    - [SelectedText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.SelectedText) background from #99CCFF to #0078D7.
    - [InactiveSelectedText](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.InactiveSelectedText) background from #CCDDEE to #BFCDDB.
    - [LineNumbers](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.LineNumbers) foreground from #2B91AF to #7A7A7A.
    - [BreakpointEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.BreakpointEnabled) background from #AB616B to #963A46.
    - [BreakpointDisabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.BreakpointDisabled) border from #AB616B to #000000.
    - [IndicatorMargin](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.IndicatorMargin) background from #F0F0F0 to #E6E7E8.
    - [CurrentLine](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CurrentLine) border from #30A0A0A0 to #EAEAF2; 30% opacity automatically applied when rendered.
    - [DelimiterMatching](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeKeys.DelimiterMatching) background from #30A0A0A0 to #DBE0CC with 75% opacity automatically applied when rendered.
    - [FindMatchHighlight](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.FindMatchHighlight) background from #C8F4A721 to #F4A721 with 75% opacity automatically applied when rendered.
    - [CollapsibleRegion](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.CollapsibleRegion) foreground from #80D7DDE8 to #D7DDE8 with 50% opacity automatically applied when rendered.  Background from #80EDEFF5 to #F6F7FA.
    - [ColumnGuides](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.ColumnGuides) background from #40C0C0C0 to #D0D0D0.
    - [IndentationGuides](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.IndentationGuides) background from #40C0C0C0 to #D0D0D0.
- The [Punctuation](xref:ActiproSoftware.Text.ClassificationTypes.Punctuation) classification type was added to all appropriate Actipro languages.
  - Implement the [IDotNetClassificationTypeProvider](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetClassificationTypeProvider).[Punctuation](xref:ActiproSoftware.Text.Languages.DotNet.IDotNetClassificationTypeProvider.Punctuation) property for classes that implement the interface without deriving from the [DotNetClassificationTypeProvider](xref:ActiproSoftware.Text.Languages.DotNet.Implementation.DotNetClassificationTypeProvider) class.
  - Implement the [IJavaScriptClassificationTypeProvider](xref:ActiproSoftware.Text.Languages.JavaScript.IJavaScriptClassificationTypeProvider).[Punctuation](xref:ActiproSoftware.Text.Languages.JavaScript.IJavaScriptClassificationTypeProvider.Punctuation) property for classes that implement the interface without deriving from the [JavaScriptClassificationTypeProvider](xref:ActiproSoftware.Text.Languages.JavaScript.Implementation.JavaScriptClassificationTypeProvider) class.
  - Implement the [IPythonClassificationTypeProvider](xref:ActiproSoftware.Text.Languages.Python.IPythonClassificationTypeProvider).[Punctuation](xref:ActiproSoftware.Text.Languages.Python.IPythonClassificationTypeProvider.Punctuation) property for classes that implement the interface without deriving from the [PythonClassificationTypeProvider](xref:ActiproSoftware.Text.Languages.Python.Implementation.PythonClassificationTypeProvider) class.
- The `EditLineNumberMarginForegroundNormalBrushKey` theme resource brushes were changed from #2B91AF to #7A7A7A on light theme and #8A8A8A on dark theme as these colors work better with the default current line number highlight brush.

## User Prompt Enhancements

Working with user prompts is now easier than ever thanks to the new [UserPromptBuilder](../shared/windows-controls/user-prompt/builder-pattern.md) that uses a fluent API to seamlessly stream together a series of property configurations and event callbacks for creating and showing prompts.  [ThemedMessageBox](../shared/windows-controls/user-prompt/message-box.md) has been updated to use the builder, so developers now have access to customizations not previously possible.

While still supported in v24.1, the following have been deprecated and will be removed in a future release:

### UserPromptWindow.DefaultTitle

[UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[DefaultTitle](xref:@ActiproUIRoot.Controls.UserPromptWindow.DefaultTitle) was used to set a default title for [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) if a title was not specified. This default title was used before attempting to infer a title from a status icon.  Going forward, the inferred status titles are getting prioritized over a default.  If an inferred title is not available, then [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[FallbackTitle](xref:@ActiproUIRoot.Controls.UserPromptBuilder.FallbackTitle) will be used.

Stop using [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[DefaultTitle](xref:@ActiproUIRoot.Controls.UserPromptWindow.DefaultTitle) and implement one of the following alternatives:
- Use [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[FallbackTitle](xref:@ActiproUIRoot.Controls.UserPromptBuilder.FallbackTitle) knowing that inferred titles will get higher priority than the fallback title.
- Use [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[RegisterGlobalConfigureCallback](xref:@ActiproUIRoot.Controls.UserPromptBuilder.RegisterGlobalConfigureCallback*) to pre-configure all new [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) instances with a default title. This will take priority over inferred titles and still allow explicit title configurations to override the default.

### ThemedMessageBox.Show with Help Action

Some of the [ThemedMessageBox](../shared/windows-controls/user-prompt/message-box.md) overloads allow an `Action` to be passed that will result in a **Help** button being added to the message box that, when clicked, will invoke the action.  Since [UserPromptBuilder](../shared/windows-controls/user-prompt/builder-pattern.md) makes it easy to configure a **Help** button and so much more, that overload is preferred.

```csharp
// Replace calls using Help action
result = ThemedMessageBox.Show(text, caption, button, image, defaultResult, helpAction: OpenHelp);

// With UserPromptBuilder configuration that adds Help command
result = ThemedMessageBox.Show(text, caption, button, image, defaultResult, builder => builder.WithHelpCommand(OpenHelp));
```

The overload with the Help action will be removed in a future release to reduce the number of overloads.

## UI Accessibilty Peer Updates

.NET 8 altered the default UI peer structure for basic `ItemsControl` controls and their children.  In v24.1, updates were made to UIA peers for various Actipro controls to improve UIA peer structure or mimic how it was prior to .NET 8.

The affected controls grouped by product are:

- Editors - `MonthCalendar` and related controls.
- Micro Charts - `MicroSegmentChart`.
- Navigation - `Breadcrumb`, `ZoomContentControl`, and related controls.
- Ribbon (legacy) - `Ribbon`, `Tab`, `Group`, `QuickAccessToolBar`, `TabPanelItemsControl`, and related controls.
- Shared Library - `UserPromptControl`.
- Views - `Book`, `TaskBoard`, and related controls.

## Context Menu Access Key Updates

Built-in context menus for the Docking and Grids products now support access keys.  An access key is designated by placing an underscore (`_`) character before the desired letter of a menu header.  Previously, all underscore characters in context menus were escaped.  Since the text of a context menu is a [customizable string resource](../customizing-string-resources.md) and underscores are no longer escaped, any custom strings that wish to display an underscore character without it being used as an access key must escape the character by using two underscores (e.g., `"Escaped __Underscore"` will display as `"Escaped _Underscore"`).

## Removed XBAP Support

XBAP (WPF Browser Application) support has been removed since this feature is no longer supported by .NET or modern browsers.

## Previously Deprecated Items Removed

The following members were deprecated in previous releases have been removed in this release.

- (Editors) [Country](xref:@ActiproUIRoot.Controls.Editors.Country).`Code` property was removed. Use the [Alpha2Code](xref:@ActiproUIRoot.Controls.Editors.Country.Alpha2Code)  property instead.
- (SyntaxEditor) [IntelliPromptSessionTypes](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IntelliPromptSessionTypes).`CodeSnippet` was removed. Use the [CodeSnippetTemplate](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.IntelliPromptSessionTypes.CodeSnippetTemplate) property instead.
- (Themes) [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) removed the following properties:
  - `ScrollDownGlyphTemplateKey`.  Use the [LargeDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeDownArrowGlyphTemplateKey) property instead.
  - `ScrollLeftGlyphTemplateKey`.  Use the [LargeLeftArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeLeftArrowGlyphTemplateKey) property instead.
  - `ScrollRightGlyphTemplateKey`.  Use the [LargeRightArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeRightArrowGlyphTemplateKey) property instead.
  - `ScrollUpGlyphTemplateKey`.  Use the [LargeUpArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeUpArrowGlyphTemplateKey) property instead.
- (Themes) The `ActiproSoftware.Windows.Themes.ThemeName` enum was removed. Use the static [ThemeNames](xref:@ActiproUIRoot.Themes.ThemeNames) class instead.  See the [Converting to v20.1](converting-to-v20-1.md) topic for details.
