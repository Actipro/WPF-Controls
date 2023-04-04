---
title: "Native Controls"
page-title: "Native Controls - Themes Reference"
order: 5
---
# Native Controls

Actipro Themes includes styles and templates for all native WPF controls that have been given a more modernized appearance, and mesh perfectly with Actipro's custom control products.

The native WPF control styles use the same common asset resource (brush, thickness, etc.) pool as with our other custom WPF controls.  Thus, no matter which native or Actipro controls you use together, the appearance will consistently look great.

## Using Native Styles and Templates

The `Style` for each native control can be applied either explicitly or implicitly.  In most situations, applying the styles implicitly is the best approach, as this provides consistency across your application.

### Implicit Styles

The native control styles can be applied implicitly by simply setting [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[AreNativeThemesEnabled](xref:@ActiproUIRoot.Themes.ThemeManager.AreNativeThemesEnabled) to `true`.  The theme manager will automatically load the necessary implicit styles into the application resources, which will therefore be used by your entire application.

```csharp
ThemeManager.AreNativeThemesEnabled = true;
```

> [!NOTE]
> See the [Getting Started](getting-started.md) topic for a complete example of how to use this setting, as well as how to combine it with other settings.

While not recommended due to additional resources needing to be loaded at run-time, it is also possible to implicitly apply the native control styles to part of the visual tree.  This sample code shows how to enable native for `myControl` and its descendents only:

```csharp
ThemeManager.SetAreNativeThemesEnabled(myControl, true);
```

### Explicit Styles

The native control styles can also be applied explicitly on each individual control, using an associated `ComponentResourceKey` defined in the [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) class.

For example, the [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys).[ButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ButtonBaseStyleKey) can be applied a `RepeatButton` like so:

```xaml
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
...
<RepeatButton Content="Click Me" Style="{DynamicResource {x:Static themes:SharedResourceKeys.ButtonBaseStyleKey}}" />
```

For a complete list of the support native control styles, see the table in the following section.

## Style Keys for Native Controls

This list shows the style resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) for use with native controls:

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

[ButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ButtonBaseStyleKey)

</td>
<td>

Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`.

</td>
</tr>

<tr>
<td>

[CheckBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.CheckBoxStyleKey)

</td>
<td>

Applies to `CheckBox` controls.

</td>
</tr>

<tr>
<td>

[ComboBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ComboBoxStyleKey)

</td>
<td>

Applies to `ComboBox` controls.

</td>
</tr>

<tr>
<td>

[ComboBoxItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ComboBoxItemStyleKey)

</td>
<td>

Applies to `ComboBoxItem` controls.

</td>
</tr>

<tr>
<td>

[ContextMenuStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ContextMenuStyleKey)

</td>
<td>

Applies to `ContextMenu` controls.

</td>
</tr>

<tr>
<td>

[DocumentViewerStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.DocumentViewerStyleKey)

</td>
<td>

Applies to `DocumentViewer` controls.

</td>
</tr>

<tr>
<td>

[ExpanderStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ExpanderStyleKey)

</td>
<td>

Applies to `Expander` controls.  Note that this style uses expand/collapse animation by default, which can lead to performance issues while expanding/collapsing when there are an enormous number of elements within the expanded content.  Set the attached `ThemeProperties.IsAnimationEnabled` dependency property to `false` on the `Expander` to prevent animation.

</td>
</tr>

<tr>
<td>

[GridSplitterStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.GridSplitterStyleKey)

</td>
<td>

Applies to `GridSplitter` controls.

</td>
</tr>

<tr>
<td>

[GridViewColumnHeaderStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.GridViewColumnHeaderStyleKey)

</td>
<td>

Applies to `GridViewColumnHeader` controls.

</td>
</tr>

<tr>
<td>

[GroupBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.GroupBoxStyleKey)

</td>
<td>

Applies to `GroupBox` controls.

</td>
</tr>

<tr>
<td>

[HyperlinkStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.HyperlinkStyleKey)

</td>
<td>

Applies to `Hyperlink` controls.

</td>
</tr>

<tr>
<td>

[LabelStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.LabelStyleKey)

</td>
<td>

Applies to `Label`.

</td>
</tr>

<tr>
<td>

[ListBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ListBoxStyleKey)

</td>
<td>

Applies to `ListBox` controls.

</td>
</tr>

<tr>
<td>

[ListBoxItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ListBoxItemStyleKey)

</td>
<td>

Applies to `ListBoxItem` controls.

</td>
</tr>

<tr>
<td>

[ListViewStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ListViewStyleKey)

</td>
<td>

Applies to `ListView` controls.

</td>
</tr>

<tr>
<td>

[ListViewItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ListViewItemStyleKey)

</td>
<td>

Applies to `ListViewItem` controls.

</td>
</tr>

<tr>
<td>

[MenuStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuStyleKey)

</td>
<td>

Applies to `Menu` controls.

</td>
</tr>

<tr>
<td>

[MenuItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuItemStyleKey)

</td>
<td>

Applies to `MenuItem` controls. In addition, there are several keys that reference control templates for the various types of menu items:

- [MenuItemTopLevelHeaderTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuItemTopLevelHeaderTemplateKey)
- [MenuItemTopLevelItemTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuItemTopLevelItemTemplateKey)
- [MenuItemSubmenuContentTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuItemSubmenuContentTemplateKey)
- [MenuItemSubmenuHeaderTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuItemSubmenuHeaderTemplateKey)
- [MenuItemSubmenuItemTemplateKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuItemSubmenuItemTemplateKey)

</td>
</tr>

<tr>
<td>

[PasswordBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.PasswordBoxStyleKey)

</td>
<td>

Applies to `PasswordBox` controls.

</td>
</tr>

<tr>
<td>

[ProgressBarStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ProgressBarStyleKey)

</td>
<td>

Applies to `ProgressBar` controls.

</td>
</tr>

<tr>
<td>

[RadioButtonStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.RadioButtonStyleKey)

</td>
<td>

Applies to `RadioButton` controls.

</td>
</tr>

<tr>
<td>

[ResizeGripStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ResizeGripStyleKey)

</td>
<td>

Applies to `ResizeGrip` controls.

</td>
</tr>

<tr>
<td>

[ScrollBarStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ScrollBarStyleKey)

</td>
<td>

Applies to `ScrollBar` controls.

</td>
</tr>

<tr>
<td>

[ScrollViewerStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ScrollViewerStyleKey)

</td>
<td>

Applies to `ScrollViewer` controls.

</td>
</tr>

<tr>
<td>

[SeparatorStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SeparatorStyleKey)

</td>
<td>

Applies to `Separator` controls.

</td>
</tr>

<tr>
<td>

[SliderStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.SliderStyleKey)

</td>
<td>

Applies to `Slider` controls.

</td>
</tr>

<tr>
<td>

[StatusBarStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarStyleKey)

</td>
<td>

Applies to `StatusBar` controls.

</td>
</tr>

<tr>
<td>

[StatusBarItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarItemStyleKey)

</td>
<td>

Applies to `StatusBarItem` controls.

</td>
</tr>

<tr>
<td>

[TabControlStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.TabControlStyleKey)

</td>
<td>

Applies to `TabControl` controls.

</td>
</tr>

<tr>
<td>

[TabItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.TabItemStyleKey)

</td>
<td>

Applies to `TabItem` controls.

</td>
</tr>

<tr>
<td>

[TextBoxBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.TextBoxBaseStyleKey)

</td>
<td>

Applies to `TextBoxBase` controls and its derivations, which includes `TextBox` and `RichTextBox`.

</td>
</tr>

<tr>
<td>

[ThumbStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ThumbStyleKey)

</td>
<td>

Applies to `Thumb` controls.

</td>
</tr>

<tr>
<td>

[ToolBarStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarStyleKey)

</td>
<td>

Applies to `ToolBar` controls.

</td>
</tr>

<tr>
<td>

[ToolBarTrayStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarTrayStyleKey)

</td>
<td>

Applies to `ToolBarTray` controls.

</td>
</tr>

<tr>
<td>

[ToolTipStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolTipStyleKey)

</td>
<td>

Applies to `ToolTip` controls.

</td>
</tr>

<tr>
<td>

[TreeViewStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.TreeViewStyleKey)

</td>
<td>

Applies to `TreeView` controls.

</td>
</tr>

<tr>
<td>

[TreeViewItemStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.TreeViewItemStyleKey)

</td>
<td>

Applies to `TreeViewItem` controls and its derivations.

</td>
</tr>

<tr>
<td>

[WindowStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.WindowStyleKey)

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

This list shows the embedded style resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) for use with `Menu` controls:

| Style Resource Key | Description |
|-----|-----|
| [MenuEmbeddedScrollViewerStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuEmbeddedScrollViewerStyleKey) | Applies to `ScrollViewer` controls. |
| [MenuEmbeddedSeparatorStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.MenuEmbeddedSeparatorStyleKey) | Applies to `Separator` controls. |

### StatusBar

This list shows the embedded style resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) for use with `StatusBar` controls:

| Style Resource Key | Description |
|-----|-----|
| [StatusBarEmbeddedResizeGripStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarEmbeddedResizeGripStyleKey) | Applies to `ResizeGrip` controls. |
| [StatusBarEmbeddedSeparatorStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarEmbeddedSeparatorStyleKey) | Applies to `Separator` controls. |
| [StatusBarEmbeddedSliderDecreaseButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarEmbeddedSliderDecreaseButtonBaseStyleKey) | Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`. |
| [StatusBarEmbeddedSliderIncreaseButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarEmbeddedSliderIncreaseButtonBaseStyleKey) | Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`. |
| [StatusBarEmbeddedSliderStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.StatusBarEmbeddedSliderStyleKey) | Applies to `Slider` controls. |

### ToolBar

This list shows the embedded style resource keys that are available in [SharedResourceKeys](xref:@ActiproUIRoot.Themes.SharedResourceKeys) for use with `ToolBar` controls:

| Style Resource Key | Description |
|-----|-----|
| [ToolBarEmbeddedButtonBaseStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarEmbeddedButtonBaseStyleKey) | Applies to `ButtonBase` controls and its derivations, which includes `Button`, `RepeatButton`, and `ToggleButton`. |
| [ToolBarEmbeddedComboBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarEmbeddedComboBoxStyleKey) | Applies to `ComboBox` controls. |
| [ToolBarEmbeddedMenuStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarEmbeddedMenuStyleKey) | Applies to `Menu` controls. |
| [ToolBarEmbeddedPopupButtonStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarEmbeddedPopupButtonStyleKey) | Applies to [PopupButton](xref:@ActiproUIRoot.Controls.PopupButton) controls. |
| [ToolBarEmbeddedSeparatorStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarEmbeddedSeparatorStyleKey) | Applies to `Separator` controls. |
| [ToolBarEmbeddedTextBoxStyleKey](xref:@ActiproUIRoot.Themes.SharedResourceKeys.ToolBarEmbeddedTextBoxStyleKey) | Applies to `TextBox` controls. |
