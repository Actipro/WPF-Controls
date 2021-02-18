---
title: "Reusable Styles and Glyphs"
page-title: "Reusable Styles and Glyphs - Themes Reference"
order: 6
---
# Reusable Styles and Glyphs

Actipro Themes includes several reusable styles and templates for use in various parts of your application, to maintain a consistent look.

## Reusable Styles

There are several miscellaneous styles for use with Actipro and native controls.  This list shows the style resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys):

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

[CloseToolWindowTitleBarButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.CloseToolWindowTitleBarButtonBaseStyleKey)

</td>
<td>

This style is identical to [ToolWindowTitleBarButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolWindowTitleBarButtonBaseStyleKey), but uses a slightly different look to distinguish the close button.

</td>
</tr>

<tr>
<td>

[CloseWindowTitleBarButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.CloseWindowTitleBarButtonBaseStyleKey)

</td>
<td>

This style is identical to [WindowTitleBarButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonBaseStyleKey), but uses a slightly different look to distinguish the close button.

</td>
</tr>

<tr>
<td>

[EmbeddedButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

This style should be used for buttons embedded inside other controls.

</td>
</tr>

<tr>
<td>

[EmbeddedButtonBaseOverrideStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedButtonBaseOverrideStyleKey)

</td>
<td>

This style is identical to [EmbeddedButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedButtonBaseStyleKey), but also sets `OverridesDefaultStyle` to `true`.

</td>
</tr>

<tr>
<td>

[EmbeddedComboBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedComboBoxStyleKey)

</td>
<td>

Applies to `ComboBox` controls.

This style should be used for comboboxes embedded inside other controls.

</td>
</tr>

<tr>
<td>

[EmbeddedPopupButtonStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedPopupButtonStyleKey)

</td>
<td>

Applies to [PopupButton](xref:ActiproSoftware.Windows.Controls.PopupButton) controls.

This style should be used for popup buttons embedded inside other controls.

</td>
</tr>

<tr>
<td>

[EmbeddedPopupButtonOverrideStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedPopupButtonOverrideStyleKey)

</td>
<td>

This style is identical to [EmbeddedPopupButtonStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.EmbeddedPopupButtonStyleKey), but also sets `OverridesDefaultStyle` to `true`.

</td>
</tr>

<tr>
<td>

[MenuHeadingStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuHeadingStyleKey)

</td>
<td>

Applies to `MenuItem` controls.

This style gives the menu item a text-based heading appearance, which is sometimes seen in Office apps to help categorize groups of menu items.

</td>
</tr>

<tr>
<td>

[ToolWindowTitleBarButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolWindowTitleBarButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

This style should be used for buttons inside the title bar of a tool window.

</td>
</tr>

<tr>
<td>

[WindowTitleBarButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonBaseStyleKey)

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

This list shows general glyph template resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys):

| DataTemplate Resource Key | Description |
|-----|-----|
| [CloseGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.CloseGlyphTemplateKey) | Represents a glyph for use on a generic close button. |
| [DropDownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.DropDownArrowGlyphTemplateKey) | Represents a glyph for use on a drop-down button. |
| [ExternalWindowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ExternalWindowGlyphTemplateKey) | Represents a glyph for use on buttons that open an external window. |
| [MaximizeGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MaximizeGlyphTemplateKey) | Represents a glyph for use on a generic maximize button. |
| [MenuCheckGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuCheckGlyphTemplateKey) | Represents a glyph for a menu check. |
| [MenuPopupGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuPopupGlyphTemplateKey) | Represents a glyph for a menu popup. |
| [MinimizeGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MinimizeGlyphTemplateKey) | Represents a glyph for use on a generic minimize button. |
| [MinusGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MinusGlyphTemplateKey) | Represents a glyph for a minus (decrease). |
| [OverflowDropDownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.OverflowDropDownArrowGlyphTemplateKey) | Represents a glyph for use on a drop-down button, to indicate there are hidden items. |
| [PinGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.PinGlyphTemplateKey) | Represents a glyph for use on a pin button, such as for auto-hiding tool windows. |
| [PlusGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.PlusGlyphTemplateKey) | Represents a glyph for a plus (increase). |
| [RestoreGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.RestoreGlyphTemplateKey) | Represents a glyph for use on a generic restore button. |
| [SmallDropDownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallDropDownArrowGlyphTemplateKey) | Represents a glyph for use on a small drop-down button. |
| [SmallMinusGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallMinusGlyphTemplateKey) | Represents a glyph for a small minus (decrease). |
| [SmallPlusGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallPlusGlyphTemplateKey) | Represents a glyph for a small plus (increase). |
| [ToolBarOverflowDropDownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarOverflowDropDownArrowGlyphTemplateKey) | Represents a glyph for use on a toolbar overflow drop-down button. |
| [UnpinGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.UnpinGlyphTemplateKey) | Represents a glyph for use on an unpin button, such as for sauto-hiding tool windows. |

### Arrows

This list shows glyph template resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) and render arrows.

| DataTemplate Resource Key | Description |
|-----|-----|
| [LeftArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.LeftArrowGlyphTemplateKey) | Represents a normal size left arrow glyph. |
| [UpArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.UpArrowGlyphTemplateKey) | Represents a normal size up arrow glyph. |
| [RightArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.RightArrowGlyphTemplateKey) | Represents a normal size right arrow glyph. |
| [DownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.DownArrowGlyphTemplateKey) | Represents a normal size down arrow glyph. |
| [SmallLeftArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallLeftArrowGlyphTemplateKey) | Represents a small size left arrow glyph. |
| [SmallUpArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallUpArrowGlyphTemplateKey) | Represents a small size up arrow glyph. |
| [SmallRightArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallRightArrowGlyphTemplateKey) | Represents a small size right arrow glyph. |
| [SmallDownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SmallDownArrowGlyphTemplateKey) | Represents a small size down arrow glyph. |
| [LargeLeftArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.LargeLeftArrowGlyphTemplateKey) | Represents a large size left arrow glyph. |
| [LargeUpArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.LargeUpArrowGlyphTemplateKey) | Represents a large size up arrow glyph. |
| [LargeRightArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.LargeRightArrowGlyphTemplateKey) | Represents a large size right arrow glyph. |
| [LargeDownArrowGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.LargeDownArrowGlyphTemplateKey) | Represents a large size down arrow glyph. |

### Tool Windows

This list shows glyph template resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) for use in `Window` title bars, with a `WindowStyle` of `ToolWindow`:

| DataTemplate Resource Key | Description |
|-----|-----|
| [ToolWindowTitleBarButtonCloseGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolWindowTitleBarButtonCloseGlyphTemplateKey) | Represents a glyph for use on a tool window's close button. |
| [ToolWindowTitleBarButtonMaximizeGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolWindowTitleBarButtonMaximizeGlyphTemplateKey) | Represents a glyph for use on a tool window's maximize button. |
| [ToolWindowTitleBarButtonMinimizeGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolWindowTitleBarButtonMinimizeGlyphTemplateKey) | Represents a glyph for use on a tool window's minimize button. |
| [ToolWindowTitleBarButtonRestoreGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolWindowTitleBarButtonRestoreGlyphTemplateKey) | Represents a glyph for use on a tool window's restore button. |

### Windows

This list shows glyph template resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) for use in `Window` title bars:

| DataTemplate Resource Key | Description |
|-----|-----|
| [WindowTitleBarButtonBackGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonBackGlyphTemplateKey) | Represents a glyph for use on a window's back button. |
| [WindowTitleBarButtonCloseGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonCloseGlyphTemplateKey) | Represents a glyph for use on a window's close button. |
| [WindowTitleBarButtonMaximizeGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonMaximizeGlyphTemplateKey) | Represents a glyph for use on a window's maximize button. |
| [WindowTitleBarButtonMenuBarsGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonMenuBarsGlyphTemplateKey) | Represents a glyph for use on a window's menu bars button. |
| [WindowTitleBarButtonMinimizeGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonMinimizeGlyphTemplateKey) | Represents a glyph for use on a window's minimize button. |
| [WindowTitleBarButtonRestoreGlyphTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowTitleBarButtonRestoreGlyphTemplateKey) | Represents a glyph for use on a window's restore button. |
