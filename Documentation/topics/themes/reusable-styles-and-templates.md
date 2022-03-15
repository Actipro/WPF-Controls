---
title: "Reusable Styles and Glyphs"
page-title: "Reusable Styles and Glyphs - Themes Reference"
order: 6
---
# Reusable Styles and Glyphs

Actipro Themes includes several reusable styles and templates for use in various parts of your application, to maintain a consistent look.

## Reusable Styles

There are several miscellaneous styles for use with Actipro and native controls.  This list shows the style resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys):

<table>
<thead>

<tr>
<th>Style Resource Key</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[CloseToolWindowTitleBarButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.CloseToolWindowTitleBarButtonBaseStyleKey)

</td>
<td>

This style is identical to [ToolWindowTitleBarButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolWindowTitleBarButtonBaseStyleKey), but uses a slightly different look to distinguish the close button.

</td>
</tr>

<tr>
<td>

[CloseWindowTitleBarButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.CloseWindowTitleBarButtonBaseStyleKey)

</td>
<td>

This style is identical to [WindowTitleBarButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonBaseStyleKey), but uses a slightly different look to distinguish the close button.

</td>
</tr>

<tr>
<td>

[EmbeddedButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

This style should be used for buttons embedded inside other controls.

</td>
</tr>

<tr>
<td>

[EmbeddedButtonBaseOverrideStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedButtonBaseOverrideStyleKey)

</td>
<td>

This style is identical to [EmbeddedButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedButtonBaseStyleKey), but also sets `OverridesDefaultStyle` to `true`.

</td>
</tr>

<tr>
<td>

[EmbeddedComboBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedComboBoxStyleKey)

</td>
<td>

Applies to `ComboBox` controls.

This style should be used for comboboxes embedded inside other controls.

</td>
</tr>

<tr>
<td>

[EmbeddedPopupButtonStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedPopupButtonStyleKey)

</td>
<td>

Applies to [PopupButton](xref:@ActiproUIRoot.Controls.PopupButton) controls.

This style should be used for popup buttons embedded inside other controls.

</td>
</tr>

<tr>
<td>

[EmbeddedPopupButtonOverrideStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedPopupButtonOverrideStyleKey)

</td>
<td>

This style is identical to [EmbeddedPopupButtonStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.EmbeddedPopupButtonStyleKey), but also sets `OverridesDefaultStyle` to `true`.

</td>
</tr>

<tr>
<td>

[MenuHeadingStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuHeadingStyleKey)

</td>
<td>

Applies to `MenuItem` controls.

This style gives the menu item a text-based heading appearance, which is sometimes seen in Office apps to help categorize groups of menu items.

</td>
</tr>

<tr>
<td>

[ToolWindowTitleBarButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolWindowTitleBarButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

This style should be used for buttons inside the title bar of a tool window.

</td>
</tr>

<tr>
<td>

[WindowTitleBarButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

This style should be used for buttons inside the title bar of a window.

</td>
</tr>

</tbody>
</table>

## Reusable Glyphs

There are several reusable glyphs, defined as `DataTemplate` resources, available for use in your application.

### General

This list shows general glyph template resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys):

| DataTemplate Resource Key | Description |
|-----|-----|
| [CloseGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.CloseGlyphTemplateKey) | Represents a glyph for use on a generic close button. |
| [DropDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.DropDownArrowGlyphTemplateKey) | Represents a glyph for use on a drop-down button. |
| [ExternalWindowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ExternalWindowGlyphTemplateKey) | Represents a glyph for use on buttons that open an external window. |
| [MaximizeGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MaximizeGlyphTemplateKey) | Represents a glyph for use on a generic maximize button. |
| [MenuCheckGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuCheckGlyphTemplateKey) | Represents a glyph for a menu check. |
| [MenuPopupGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuPopupGlyphTemplateKey) | Represents a glyph for a menu popup. |
| [MinimizeGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MinimizeGlyphTemplateKey) | Represents a glyph for use on a generic minimize button. |
| [MinusGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MinusGlyphTemplateKey) | Represents a glyph for a minus (decrease). |
| [OverflowDropDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.OverflowDropDownArrowGlyphTemplateKey) | Represents a glyph for use on a drop-down button, to indicate there are hidden items. |
| [PinGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.PinGlyphTemplateKey) | Represents a glyph for use on a pin button, such as for auto-hiding tool windows. |
| [PlusGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.PlusGlyphTemplateKey) | Represents a glyph for a plus (increase). |
| [RestoreGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.RestoreGlyphTemplateKey) | Represents a glyph for use on a generic restore button. |
| [SmallDropDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallDropDownArrowGlyphTemplateKey) | Represents a glyph for use on a small drop-down button. |
| [SmallMinusGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallMinusGlyphTemplateKey) | Represents a glyph for a small minus (decrease). |
| [SmallPlusGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallPlusGlyphTemplateKey) | Represents a glyph for a small plus (increase). |
| [ToolBarOverflowDropDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarOverflowDropDownArrowGlyphTemplateKey) | Represents a glyph for use on a toolbar overflow drop-down button. |
| [UnpinGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.UnpinGlyphTemplateKey) | Represents a glyph for use on an unpin button, such as for sauto-hiding tool windows. |

### Arrows

This list shows glyph template resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) and render arrows.

| DataTemplate Resource Key | Description |
|-----|-----|
| [LeftArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LeftArrowGlyphTemplateKey) | Represents a normal size left arrow glyph. |
| [UpArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.UpArrowGlyphTemplateKey) | Represents a normal size up arrow glyph. |
| [RightArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.RightArrowGlyphTemplateKey) | Represents a normal size right arrow glyph. |
| [DownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.DownArrowGlyphTemplateKey) | Represents a normal size down arrow glyph. |
| [SmallLeftArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallLeftArrowGlyphTemplateKey) | Represents a small size left arrow glyph. |
| [SmallUpArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallUpArrowGlyphTemplateKey) | Represents a small size up arrow glyph. |
| [SmallRightArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallRightArrowGlyphTemplateKey) | Represents a small size right arrow glyph. |
| [SmallDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SmallDownArrowGlyphTemplateKey) | Represents a small size down arrow glyph. |
| [LargeLeftArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeLeftArrowGlyphTemplateKey) | Represents a large size left arrow glyph. |
| [LargeUpArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeUpArrowGlyphTemplateKey) | Represents a large size up arrow glyph. |
| [LargeRightArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeRightArrowGlyphTemplateKey) | Represents a large size right arrow glyph. |
| [LargeDownArrowGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LargeDownArrowGlyphTemplateKey) | Represents a large size down arrow glyph. |

### Tool Windows

This list shows glyph template resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) for use in `Window` title bars, with a `WindowStyle` of `ToolWindow`:

| DataTemplate Resource Key | Description |
|-----|-----|
| [ToolWindowTitleBarButtonCloseGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolWindowTitleBarButtonCloseGlyphTemplateKey) | Represents a glyph for use on a tool window's close button. |
| [ToolWindowTitleBarButtonMaximizeGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolWindowTitleBarButtonMaximizeGlyphTemplateKey) | Represents a glyph for use on a tool window's maximize button. |
| [ToolWindowTitleBarButtonMinimizeGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolWindowTitleBarButtonMinimizeGlyphTemplateKey) | Represents a glyph for use on a tool window's minimize button. |
| [ToolWindowTitleBarButtonRestoreGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolWindowTitleBarButtonRestoreGlyphTemplateKey) | Represents a glyph for use on a tool window's restore button. |

### Windows

This list shows glyph template resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) for use in `Window` title bars:

| DataTemplate Resource Key | Description |
|-----|-----|
| [WindowTitleBarButtonBackGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonBackGlyphTemplateKey) | Represents a glyph for use on a window's back button. |
| [WindowTitleBarButtonCloseGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonCloseGlyphTemplateKey) | Represents a glyph for use on a window's close button. |
| [WindowTitleBarButtonMaximizeGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonMaximizeGlyphTemplateKey) | Represents a glyph for use on a window's maximize button. |
| [WindowTitleBarButtonMenuBarsGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonMenuBarsGlyphTemplateKey) | Represents a glyph for use on a window's menu bars button. |
| [WindowTitleBarButtonMinimizeGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonMinimizeGlyphTemplateKey) | Represents a glyph for use on a window's minimize button. |
| [WindowTitleBarButtonRestoreGlyphTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowTitleBarButtonRestoreGlyphTemplateKey) | Represents a glyph for use on a window's restore button. |
