---
title: "Native Controls"
page-title: "Native Controls - Themes Reference"
order: 5
---
# Native Controls

Actipro Themes includes styles and templates for all native WPF controls that have been given a more modernized appearance, and mesh perfectly with Actipro's custom control products.

The native WPF control styles use the same common asset resource (brush, thickness, etc.) pool as with our other custom WPF controls.  Thus no matter which native or Actipro controls you use together, the appearance will consistently look great.

## Using Native Styles and Templates

The `Style` for each native control can be applied either explicitly or implicitly.  In most situations, applying the styles implicitly is the best approach, as this provides consistency across your application.

### Implicit Styles

The native control styles can be applied implicitly by simply setting [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[AreNativeThemesEnabled](xref:ActiproSoftware.Windows.Themes.ThemeManager.AreNativeThemesEnabled) to `true`.  The theme manager will automatically load the necessary implicit styles into the application resources, which will therefore be used by your entire application.

> [!NOTE]
> See the [Getting Started](getting-started.md) topic for a complete example of how to use this setting, as well as how to combine it with other settings.

While not recommended due to additional resources needing to be loaded at run-time, it is also possible to implicitly apply the native control styles to part of the visual tree.  This sample code shows how to enable native for `myControl` and its descendents only:

```csharp
ThemeManager.SetAreNativeThemesEnabled(myControl, true);
```

### Explicit Styles

The native control styles can also be applied explicitly on each individual control, using an associated `ComponentResourceKey` defined in the [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) class.

For example, the [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys).[ButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ButtonBaseStyleKey) can be applied a `RepeatButton` like so:

```xaml
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
...
<RepeatButton Content="Click Me" Style="{DynamicResource {x:Static themes:SharedResourceKeys.ButtonBaseStyleKey}}" />
```

For a complete list of the support native control styles, see the table in the following section.

## Style Keys for Native Controls

This list shows the style resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) for use with native controls:

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

[ButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

</td>
</tr>

<tr>
<td>

[CheckBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.CheckBoxStyleKey)

</td>
<td>

Applies to `CheckBox` controls.

</td>
</tr>

<tr>
<td>

[ComboBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ComboBoxStyleKey)

</td>
<td>

Applies to `ComboBox` controls.

</td>
</tr>

<tr>
<td>

[ComboBoxItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ComboBoxItemStyleKey)

</td>
<td>

Applies to `ComboBoxItem` controls.

</td>
</tr>

<tr>
<td>

[ContextMenuStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ContextMenuStyleKey)

</td>
<td>

Applies to `ContextMenu` controls.

</td>
</tr>

<tr>
<td>

[DocumentViewerStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.DocumentViewerStyleKey)

</td>
<td>

Applies to `DocumentViewer` controls.

</td>
</tr>

<tr>
<td>

[ExpanderStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ExpanderStyleKey)

</td>
<td>

Applies to `Expander` controls.

</td>
</tr>

<tr>
<td>

[GridSplitterStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.GridSplitterStyleKey)

</td>
<td>

Applies to `GridSplitter` controls.

</td>
</tr>

<tr>
<td>

[GridViewColumnHeaderStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.GridViewColumnHeaderStyleKey)

</td>
<td>

Applies to `GridViewColumnHeader` controls.

</td>
</tr>

<tr>
<td>

[GroupBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.GroupBoxStyleKey)

</td>
<td>

Applies to `GroupBox` controls.

</td>
</tr>

<tr>
<td>

[HyperlinkStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.HyperlinkStyleKey)

</td>
<td>

Applies to `Hyperlink` controls.

</td>
</tr>

<tr>
<td>

[LabelStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.LabelStyleKey)

</td>
<td>

Applies to `Label`.

</td>
</tr>

<tr>
<td>

[ListBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ListBoxStyleKey)

</td>
<td>

Applies to `ListBox` controls.

</td>
</tr>

<tr>
<td>

[ListBoxItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ListBoxItemStyleKey)

</td>
<td>

Applies to `ListBoxItem` controls.

</td>
</tr>

<tr>
<td>

[ListViewStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ListViewStyleKey)

</td>
<td>

Applies to `ListView` controls.

</td>
</tr>

<tr>
<td>

[ListViewItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ListViewItemStyleKey)

</td>
<td>

Applies to `ListViewItem` controls.

</td>
</tr>

<tr>
<td>

[MenuStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuStyleKey)

</td>
<td>

Applies to `Menu` controls.

</td>
</tr>

<tr>
<td>

[MenuItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuItemStyleKey)

</td>
<td>

Applies to `MenuItem` controls. In addition, there are several keys that reference control templates for the various types of menu items:

- [MenuItemTopLevelHeaderTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuItemTopLevelHeaderTemplateKey)
- [MenuItemTopLevelItemTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuItemTopLevelItemTemplateKey)
- [MenuItemSubmenuContentTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuItemSubmenuContentTemplateKey)
- [MenuItemSubmenuHeaderTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuItemSubmenuHeaderTemplateKey)
- [MenuItemSubmenuItemTemplateKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuItemSubmenuItemTemplateKey)

</td>
</tr>

<tr>
<td>

[PasswordBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.PasswordBoxStyleKey)

</td>
<td>

Applies to `PasswordBox` controls.

</td>
</tr>

<tr>
<td>

[ProgressBarStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ProgressBarStyleKey)

</td>
<td>

Applies to `ProgressBar` controls.

</td>
</tr>

<tr>
<td>

[RadioButtonStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.RadioButtonStyleKey)

</td>
<td>

Applies to `RadioButton` controls.

</td>
</tr>

<tr>
<td>

[ResizeGripStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ResizeGripStyleKey)

</td>
<td>

Applies to `ResizeGrip` controls.

</td>
</tr>

<tr>
<td>

[ScrollBarStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ScrollBarStyleKey)

</td>
<td>

Applies to `ScrollBar` controls.

</td>
</tr>

<tr>
<td>

[ScrollViewerStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ScrollViewerStyleKey)

</td>
<td>

Applies to `ScrollViewer` controls.

</td>
</tr>

<tr>
<td>

[SeparatorStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SeparatorStyleKey)

</td>
<td>

Applies to `Separator` controls.

</td>
</tr>

<tr>
<td>

[SliderStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.SliderStyleKey)

</td>
<td>

Applies to `Slider` controls.

</td>
</tr>

<tr>
<td>

[StatusBarStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarStyleKey)

</td>
<td>

Applies to `StatusBar` controls.

</td>
</tr>

<tr>
<td>

[StatusBarItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarItemStyleKey)

</td>
<td>

Applies to `StatusBarItem` controls.

</td>
</tr>

<tr>
<td>

[TabControlStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.TabControlStyleKey)

</td>
<td>

Applies to `TabControl` controls.

</td>
</tr>

<tr>
<td>

[TabItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.TabItemStyleKey)

</td>
<td>

Applies to `TabItem` controls.

</td>
</tr>

<tr>
<td>

[TextBoxBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.TextBoxBaseStyleKey)

</td>
<td>

Applies to `TextBoxBase` controls and its derivations, which includes `TextBox` and `RichTextBox`.

</td>
</tr>

<tr>
<td>

[ThumbStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ThumbStyleKey)

</td>
<td>

Applies to `Thumb` controls.

</td>
</tr>

<tr>
<td>

[ToolBarStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarStyleKey)

</td>
<td>

Applies to `ToolBar` controls.

</td>
</tr>

<tr>
<td>

[ToolBarTrayStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarTrayStyleKey)

</td>
<td>

Applies to `ToolBarTray` controls.

</td>
</tr>

<tr>
<td>

[ToolTipStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolTipStyleKey)

</td>
<td>

Applies to `ToolTip` controls.

</td>
</tr>

<tr>
<td>

[TreeViewStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.TreeViewStyleKey)

</td>
<td>

Applies to `TreeView` controls.

</td>
</tr>

<tr>
<td>

[TreeViewItemStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.TreeViewItemStyleKey)

</td>
<td>

Applies to `TreeViewItem` controls and its derivations.

</td>
</tr>

<tr>
<td>

[WindowStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.WindowStyleKey)

</td>
<td>

Applies to `Window` controls.

</td>
</tr>

</tbody>
</table>

## Embedded Style Keys for Native Controls

Several native controls, such as `ToolBar`, require different styles for controls hosted or "embedded" inside it.

### Menu

This list shows the embedded style resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) for use with `Menu` controls:

| Style Resource Key | Description |
|-----|-----|
| [MenuEmbeddedScrollViewerStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuEmbeddedScrollViewerStyleKey) | Applies to `ScrollViewer` controls. |
| [MenuEmbeddedSeparatorStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.MenuEmbeddedSeparatorStyleKey) | Applies to `Separator` controls. |

### StatusBar

This list shows the embedded style resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) for use with `StatusBar` controls:

| Style Resource Key | Description |
|-----|-----|
| [StatusBarEmbeddedResizeGripStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarEmbeddedResizeGripStyleKey) | Applies to `ResizeGrip` controls. |
| [StatusBarEmbeddedSeparatorStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarEmbeddedSeparatorStyleKey) | Applies to `Separator` controls. |
| [StatusBarEmbeddedSliderDecreaseButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarEmbeddedSliderDecreaseButtonBaseStyleKey) | Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`. |
| [StatusBarEmbeddedSliderIncreaseButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarEmbeddedSliderIncreaseButtonBaseStyleKey) | Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`. |
| [StatusBarEmbeddedSliderStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.StatusBarEmbeddedSliderStyleKey) | Applies to `Slider` controls. |

### ToolBar

This list shows the embedded style resource keys that are available in [SharedResourceKeys](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys) for use with `ToolBar` controls:

| Style Resource Key | Description |
|-----|-----|
| [ToolBarEmbeddedButtonBaseStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarEmbeddedButtonBaseStyleKey) | Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`. |
| [ToolBarEmbeddedComboBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarEmbeddedComboBoxStyleKey) | Applies to `ComboBox` controls. |
| [ToolBarEmbeddedMenuStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarEmbeddedMenuStyleKey) | Applies to `Menu` controls. |
| [ToolBarEmbeddedPopupButtonStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarEmbeddedPopupButtonStyleKey) | Applies to [PopupButton](xref:ActiproSoftware.Windows.Controls.PopupButton) controls. |
| [ToolBarEmbeddedSeparatorStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarEmbeddedSeparatorStyleKey) | Applies to `Separator` controls. |
| [ToolBarEmbeddedTextBoxStyleKey](xref:ActiproSoftware.Windows.Themes.SharedResourceKeys.ToolBarEmbeddedTextBoxStyleKey) | Applies to `TextBox` controls. |
