---
title: "Dark Themes"
page-title: "Dark Theme - SyntaxEditor Theme and Highlighting Style Features"
order: 5
---
# Dark Themes

[SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) supports the ability to easily switch between light and dark themes.

All of the default styles registered by [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) and [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider) as well as the built-in language implementations are designed to support either light or dark themes.  However, any custom language implementations may require additional configuration to support a dark theme since most styles intended for use on a light background are not very appealing on a dark background.

## Understanding Dark Themes

There are several aspects that must be considered to fully support dark themes with [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor).

@if (winforms) {
> [!TIP]
> The [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) can manage many of these settings for you, but the concepts are still important to understand.
}
@if (wpf) {
> [!TIP]
> The [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) can manage most of these settings for you, but the concepts are still important to understand.  In most cases, the only step necessary to support dark themes is to ensure highlighting styles are using appropriate colors.
}

@if (winforms) {
### UI Renderer

The [ISyntaxEditorRenderer](xref:@ActiproUIRoot.Controls.SyntaxEditor.ISyntaxEditorRenderer) affects the appearance of UI elements like scrollbars that are used by the editor.  For the best experience using the editor with a dark theme, ensure the editor is configured with a renderer that supports dark themes.

The following code shows how to use the Metro Dark renderer with an individual instance of SyntaxEditor and synchronize the `ForeColor` and `BackColor` properties with the color scheme:

```csharp
// Set the renderer
editor.Renderer = new MetroSyntaxEditorRenderer(WindowsColorSchemeType.MetroDark);

// Configure default colors to match the color scheme
editor.ForeColor = editor.Renderer.ColorScheme.GetKnownColor(KnownColor.WindowText);
editor.BackColor = editor.Renderer.ColorScheme.GetKnownColor(KnownColor.Window);
```

The following code shows how to change the global color scheme to Metro Dark and sychronize the `ForeColor` and `BackColor` properties with the color scheme:

```csharp
// Set the global color scheme
UIRendererManager.ColorScheme = WindowsColorSchemeType.MetroDark;

// Configure default colors to match the color scheme
editor.ForeColor = UIRendererManager.ColorScheme.GetKnownColor(KnownColor.WindowText);
editor.BackColor = UIRendererManager.ColorScheme.GetKnownColor(KnownColor.Window);
```


> [!IMPORTANT]
> By default, the special Plain Text classification type is registered with a style that does not explicitly define foreground and background colors for the style, so the editor's `ForeColor` and `BackColor` properties will be used for Plain Text unless the style is updated with explicit colors.

See the [Global Renderers](../../../shared/global-renderers.md) topic for more details on working with renderers.

}
@if (wpf) {
### Application Theme

The overall application theme affects default foreground and background colors and the appearance of UI elements like scrollbars that are used by the editor.  For the best experience using the editor with a dark theme, ensure the application theme is also configured with a dark theme.  [Actipro Themes](../../../themes/index.md) can be used to set a dark theme like Metro Dark.

```csharp
// Switch to the MetroDark application theme
ThemeManager.CurrentTheme = ThemeNames.MetroDark;
```

> [!IMPORTANT]
> By default, the special Plain Text classification type is registered with a style that does not explicitly define foreground and background colors for the style, so the editor's `Foreground` and `Background` properties will be used for Plain Text unless the style is updated with explicit colors.
}

### Common Image Source Provider

The static [CommonImageSourceProvider.DefaultImageSet](../intelliprompt/image-source-providers.md) property indicates which set of images will be used in [IntelliPrompt](../intelliprompt/index.md).   For dark themes, the [MetroDark](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSet.MetroDark) images are designed with colors that are appropriate for dark themes.

```csharp
// Switch to the MetroDark image set
CommonImageSourceProvider.DefaultImageSet = CommonImageSet.MetroDark;
```

### Highlighting Styles

[Highlighting Styles](highlighting-styles.md) are used to define colors that are associated with certain classifications of text (e.g., comment or keyword) or UI elements like [squiggle lines](../adornment/squiggle-lines.md) and [word wrap glyphs](../editor-view/word-wrap.md).  By default, these styles assume the colors are intended for a light background and, without additional configuration, may not be very appealing on a dark background.

The [Highlighting Style Registry](highlighting-style-registries.md) associated with the editor can be configured with either a light (default) or dark color palette, and switching the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[CurrentColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.CurrentColorPalette) will update all of the styles to use colors from the selected color palette.

```csharp
// Switch to the dark color palette
AmbientHighlightingStyleRegistry.Instance.CurrentColorPalette = AmbientHighlightingStyleRegistry.Instance.DarkColorPalette;
```

All of the default styles registered by [DisplayItemClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider) and [BuiltInClassificationTypeProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.BuiltInClassificationTypeProvider) are designed with colors for either light or dark themes.  Additionally, the built-in language implementations and all of the [free languages](../../free-languages.md) are similarly designed to support both themes.

Any custom styles to be used in a dark theme may need to define additional colors if the default colors are not appealing on a dark background.

## SyntaxEditor Theme Manager

The [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) class makes it easy to automatically manage the different aspects discussed in the "Understanding Dark Themes" topic above.

@if (winforms) {
By default, [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) watches the [UIRendererManager](xref:@ActiproUIRoot.Controls.UIRendererManager).[ColorSchemeChanged](xref:@ActiproUIRoot.Controls.UIRendererManager.ColorSchemeChanged) event to detect if the global renderer color scheme is changed between a light and dark color scheme.  When a change is detected, the following changes are made:}
@if (wpf) {
By default, [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) watches the [ThemeManager](xref:@ActiproUIRoot.Themes.ThemeManager).[CurrentThemeChanged](xref:@ActiproUIRoot.Themes.ThemeManager.CurrentThemeChanged) event to detect if the application theme is changed between a light and dark theme.  When a change is detected, the following changes are made:
}
1. The [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider).[DefaultImageSet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider.DefaultImageSet) is set to either [MetroLight](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSet.MetroLight) or [MetroDark](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSet.MetroDark).
1. The [CurrentColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.CurrentColorPalette) of any managed [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) is changed to either it's [LightColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.LightColorPalette) or [DarkColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.DarkColorPalette). This, in turn, will update the style colors.  See the "Color Palettes" topic below for more detail.

 By default, the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) is automatically managed by [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager).  Use the [Manage](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager.Manage*) method manage additional repositories.  If automatic management of the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry) is not desired, use the [Unmanage](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager.Unmanage*) method to remove it.

The following sample code shows how to stop managing the [AmbientHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.AmbientHighlightingStyleRegistry):

```csharp
// Disable automatically switching between light/dark color palettes
SyntaxEditorThemeManager.Unmanage(AmbientHighlightingStyleRegistry.Instance);

// Ensure the light color palette is the default (in case a dark theme was active before being unmanaged)
AmbientHighlightingStyleRegistry.Instance.CurrentColorPalette = AmbientHighlightingStyleRegistry.Instance.LightColorPalette;
```

If automatic changes to [CommonImageSourceProvider](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider).[DefaultImageSet](xref:@ActiproUIRoot.Controls.SyntaxEditor.IntelliPrompt.Implementation.CommonImageSourceProvider.DefaultImageSet) are not desired (like in the case of a custom image provider), set the [IsCommonImageSetSynchronizationEnabled](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager.IsCommonImageSetSynchronizationEnabled) property to `false`, as shown in the following sample:

```csharp
// Disable changing image sets
SyntaxEditorThemeManager.IsCommonImageSetSynchronizationEnabled = false;
```

@if (winforms) {
> [!WARNING]
> [SyntaxEditorThemeManager](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditorThemeManager) cannot set the `ForeColor` and `BackColor` properties of each [SyntaxEditor](xref:@ActiproUIRoot.Controls.SyntaxEditor.SyntaxEditor) control instance in response to theme changes.  See the "Setting Explicit Plain Text Colors" section below for details on how to set light and dark colors for Plain Text instead of relying on the `ForeColor` and `BackColor` properties.
}

## Color Palettes

A [IHighlightingStyleColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette) is a repository of colors intended for use with the color properties of a [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) (e.g., [Foreground](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Foreground) and [Background](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Background)).  Each [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) has a [LightColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.LightColorPalette) and [DarkColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.DarkColorPalette).  The [CurrentColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.CurrentColorPalette) property indicates which color palette is active.

The colors in each color palette are associated with a unique key, which is typically the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType).`Key` associated with a style.  The following colors are available for a given key and are accessed through corresponding methods to get or set the color (e.g., [GetForeground](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette.GetForeground*) and [SetForeground](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette.SetForeground*)):
- Foreground
- Background
- Border
- Strikethrough
- Underline

When the current color palette of a registry is changed, the colors of all styles in the registry are updated to the colors defined by the new color palette using the rules outlined in the "Applying a Color Palette to a Highlighting Style" section below.

### Applying a Color Palette to a Highlighting Style

In a [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry), each registered [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) is associated with a [IClassificationType](xref:ActiproSoftware.Text.IClassificationType).  The [IClassificationType](xref:ActiproSoftware.Text.IClassificationType).`Key` is used to lookup colors in the color palette.

- The [Foreground](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Foreground) is only updated if [IsForegroundEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsForegroundEditable) is `true`.
- The [Background](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.Background) is only updated if [IsBackgroundEditable](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.IsBackgroundEditable) is `true`.
- The [BorderColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderKind) is only updated if [BorderKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.BorderKind) is not [None](xref:@ActiproUIRoot.Controls.Rendering.LineKind.None).
- The [StrikethroughColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughColor) is only updated if [StrikethroughKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.StrikethroughKind) is not [None](xref:@ActiproUIRoot.Controls.Rendering.LineKind.None).
- The [UnderlineColor](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineColor) is only updated if [UnderlineKind](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle.UnderlineKind) is not [None](xref:@ActiproUIRoot.Controls.Rendering.LineKind.None).

> [!TIP]
> The [ApplyColorPaletteToStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.HighlightingStyleRegistryExtensions.ApplyColorPaletteToStyle*) extension method on [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry) can be used to apply the current color palette to a specific style.

### Registering Styles Auto-Populate the Color Palettes

When a [IHighlightingStyle](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyle) is registered in a [HighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleRegistry) instance, the color properties of that style will be used to initialize the light and dark color palettes using similar logic as outlined in the "Apply a Color Palette to a Highlighting Style" topic above.  For example, if a style has an editable foreground, then the current foreground color will be used to initialize the color palettes; otherwise, the color is ignored.

> [!IMPORTANT]
> If the same [IClassificationType](xref:ActiproSoftware.Text.IClassificationType).`Key` has already been registered to a style and the `overwriteExisting` flag was *not* set to `true` then no changes will be made to the color palettes.  Additionally, if any color value (e.g. foreground or background) is already assigned for the same `Key`, none of the colors in that color palette will be updated.
>
> To force the light color palette to be updated, set the `overwriteExisting` flag to `true` when registering the style.  To force the dark color palette to also be updated, call the [IHighlightingStyleColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette).[ClearKey](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette.ClearKey*) method on the dark color palette before registering the style with the `overwriteExisting` flag set to `true`.
>
> Alternatively, calling [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[Unregister](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.Unregister*) for the [IClassificationType](xref:ActiproSoftware.Text.IClassificationType) (with the `keepColorPalettes` flag to set `false`) will fully reset the configuration and allow the style to be re-registered with new colors.

Any colors currently assigned to the style are always assumed to be appropriate for the light color palette even if the dark color palette is currently active, so those color values will be set on the light color palette.

For the dark color palette, the style's color value will be adapted for use in a dark theme.  See the "Adapting Light Color to Dark Color Palette" topic below for details.

After the style is registered, the [IHighlightingStyleRegistry](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry).[CurrentColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleRegistry.CurrentColorPalette) is applied to the style.  This accomplishes two goals:
1. If a dark theme is currently active, then the colors from the dark color palette will be applied to the style even though it was registered with light colors assigned to the style.
2. If a color was already defined on a color palette before the style was registered, then the style will be updated to reflect the color defined by the color palette.

> [!TIP]
> To ensure a specific dark color is used for a style, make sure the color is set in the dark color palette before the style is registered since registering a style will not replace any colors already defined in the color palettes.

The following sample code demonstrates registering a highlighting style and allowing the default mapping of colors to the light and dark color palette:

```csharp
// Create a classification type for a custom error
var classificationType = new ClassificationType("CustomError", "Custom Error");

// Create a style designed for light colors
var style = new HighlightingStyle() {
	Foreground = Color.FromArgb(255, 255, 0, 0) // Red
};

// Register the style, allowing the Red color to be used on the light color palette
// and an adapted color to be used on the dark color palette
AmbientHighlightingStyleRegistry.Instance.Register(classificationType, style);
```

The following sample code demonstrates explicitly configuring the dark color palette before registration so that a specific color is used in dark themes instead of the default mapped color.  This approach would also be used if the light color did not support an automatic mapping to a dark color.

```csharp
// Create a classification type for a custom error
var classificationType = new ClassificationType("CustomError", "Custom Error");

var lightForeground = Color.FromArgb(255, 255, 0, 0); // Red
var darkForeground = Color.FromArgb(255, 197, 83, 143); // Pink

// Create a style designed for light colors
var style = new HighlightingStyle() {
	Foreground = lightForeground
};

// Preconfigure the dark color palette with the desired code for dark themes
AmbientHighlightingStyleRegistry.Instance.DarkColorPalette.SetForeground(classificationType.Key, darkForeground);

// Register the style, allowing the Red color to be used on the light color palette
// and the pre-defined Pink color to be used on the dark color palette
AmbientHighlightingStyleRegistry.Instance.Register(classificationType, style);
```

### Setting Explicit Plain Text Colors

@if (winforms) {
By default, the special [Plain Text](xref:@ActiproUIRoot.Controls.SyntaxEditor.DisplayItemClassificationTypeProvider.PlainText) classification type is registered with a style that does not explicitly define foreground and background colors for the style, so the editor's `ForeColor` and `BackColor` properties will be used for Plain Text instead.  It can be problematic to synchronize these colors when switching between light and dark themes, so explicitly setting the foreground and background colors for Plain Text will result in the `ForeColor` and `BackColor` properties being ignored.
}
@if (wpf) {
By default, the special Plain Text classification type is registered with a style that does not explicitly define foreground and background colors for the style, so the editor's `Foreground` and `Background` properties will be used for Plain Text instead.  If preferred, Plain Text can be configured to always use explicit colors for light and dark themes, thus ignoring the `Foreground` and `Backgroup` properties.
}

The following code demonstrates setting explicit light and dark colors for Plain Text on the ambient registry:

```csharp
IHighlightingStyleRegistry registry = AmbientHighlightingStyleRegistry.Instance;
string colorKey = DisplayItemClassificationTypeKeys.PlainText;

// Light colors
registry.LightColorPalette.SetForeground(colorKey, Color.FromArgb(255, 255, 255, 255)); // Black
registry.LightColorPalette.SetBackground(colorKey, Color.FromArgb(255, 0, 0, 0)); // White

// Dark colors
registry.DarkColorPalette.SetForeground(colorKey, Color.FromArgb(255, 220, 220, 220)); // Light Gray
registry.DarkColorPalette.SetBackground(colorKey, Color.FromArgb(255, 30, 30, 30)); // Dark Gray
```

### Modifying a Style Color

During a style's registration, the colors of the current color palette are applied to the style's color property.  After registration, any changes to the style's color properties will also update the corresponding color in the current color palette.  This is common if an application allows the end-user to customize styles and will ensure the user can switch between the light and dark color palettes without losing their changes.

> [!WARNING]
> Changing the color in a color palette will not have any effect on a style until the next time that color palette is applied.

### Save and Restore Colors

If an end-user is allowed to customize the colors of a highlighting style, they will typically expect those customizations to persist between application sessions.  This can be accomplished by serializing the color data during application shutdown and restoring the color data during application startup.

The [HighlightingStyleColorPaletteSerializer](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleColorPaletteSerializer) class is used to serialize and deserialize color data.

Use the [Serialize](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleColorPaletteSerializer.Serialize*) method to generate an XML-formatted string of all the color data for a given color palette.  During application shutdown, this string can be generated and stored with other application settings to be restored later.

Use the [Deserialize](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.Implementation.HighlightingStyleColorPaletteSerializer.Deserialize*) method to populate a color palette with the colors defined by a previously serialized XML-formatted string of color data.  Existing colors are not cleared before deserialization, but any deserialized colors will replace any colors that were already defined.  This ensures colors that were not part of the serialized data are persisted.  Call the [IHighlightingStyleColorPalette](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette).[ClearAll](xref:@ActiproUIRoot.Controls.SyntaxEditor.Highlighting.IHighlightingStyleColorPalette.ClearAll*) method before deserialization if only the deserialized colors should be present after deserialization.

> [!IMPORTANT]
> If your application supports both light and dark themes, you must serialize both the light and dark color palettes.

The following code demonstrates saving color data during application shutdown:

```csharp
// Save ambient registry colors during application shutdown and
// store this data with other persisted application settings
string lightColorData = HighlightingStyleColorPaletteSerializer.Serialize(AmbientHighlightingStyleRegistry.Instance.LightColorPalette);
string darkColorData = HighlightingStyleColorPaletteSerializer.Serialize(AmbientHighlightingStyleRegistry.Instance.DarkColorPalette);
```

The following code demonstrates restoring color data during application startup:

```csharp
// Read previously serialized color data
string lightColorData = GetApplicationSetting("LightColors");
string darkColorData = GetApplicationSetting("DarkColors");

// Restore the ambient registry colors during application startup
// before any styles are registered
if (lightColorData != null)
	HighlightingStyleColorPaletteSerializer.Deserialize(AmbientHighlightingStyleRegistry.Instance.LightColorPalette, lightColorData);
if (darkColorData != null)
	HighlightingStyleColorPaletteSerializer.Deserialize(AmbientHighlightingStyleRegistry.Instance.DarkColorPalette, darkColorData);

// Register common classification types
new DisplayItemClassificationTypeProvider().RegisterAll();
new BuiltInClassificationTypeProvider().RegisterAll();
```

> [!WARNING]
> Color palette colors should be deserialized before styles are registered to ensure the colors are available to be applied to styles when they are registered. Otherwise, the colors may not be reflected until the next time the color palette is changed.

## Language Definition Files (.langdef)

When loading a language definition file, it is recommended to enable the use of built-in classification types if any *.langdef* file created prior to version 24.1 will be used.  Older files will not have the `DarkStyle` color data, so using built-in classification types will improve the default dark theme experience with those files.

See the [Loading a Language Definition File](../../language-creation/loading-lang-def.md) topic for more details and sample code.

## Adapting Light Color for Dark Color Palette

Most colors designed for use on a light background are not very appealing when used on a dark background.

When a highlighting style is registered, any colors defined on the style are assumed to be appropriate for a light theme and will be adapted for use on a dark theme if the dark color palette does not already define a corresponding color.  This helps improve the default experience on a dark theme for many commonly used colors.

### Light to Dark Color Map

The following table shows supported light colors and the corresponding dark color to which they will be mapped:

<table>
  <tr>
    <th>Name</th>
    <th>Light Color</th>
    <th>Dark Color</th>
  </tr>
  <tr>
    <td>White</td>
    <td aria-theme="light" style="color:#000000;background-color:#ffffff">#FFFFFF</td>
    <td aria-theme="dark" style="color:#ffffff;background-color:#1e1e1e">#000000</td>
  </tr>
  <tr>
    <td>Black</td>
    <td aria-theme="light" style="color:#000000;background-color:#ffffff">#000000</td>
    <td aria-theme="dark" style="color:#ffffff;background-color:#1e1e1e">#FFFFFF</td>
  </tr>
  <tr>
    <td>Red</td>
    <td aria-theme="light" style="color:#ff0000;background-color:#ffffff">#FF0000</td>
    <td aria-theme="dark" style="color:#fc3e36;background-color:#1e1e1e">#FC3E36</td>
  </tr>
  <tr>
    <td>Orange</td>
    <td aria-theme="light" style="color:#ff8500;background-color:#ffffff">#FF8500</td>
    <td aria-theme="dark" style="color:#773800;background-color:#1e1e1e">#773800</td>
  </tr>
  <tr>
    <td>Orange (Alt)</td>
    <td aria-theme="light" style="color:#f4a721;background-color:#ffffff">#F4A721</td>
    <td aria-theme="dark" style="color:#773800;background-color:#1e1e1e">#773800</td>
  </tr>
  <tr>
    <td>Maroon</td>
    <td aria-theme="light" style="color:#800000;background-color:#ffffff">#800000</td>
    <td aria-theme="dark" style="color:#d69d85;background-color:#1e1e1e">#D69D85</td>
  </tr>
  <tr>
    <td>Maroon (Alt)</td>
    <td aria-theme="light" style="color:#a31515;background-color:#ffffff">#A31515</td>
    <td aria-theme="dark" style="color:#d69d85;background-color:#1e1e1e">#D69D85</td>
  </tr>
  <tr>
    <td>Tan</td>
    <td aria-theme="light" style="color:#d2b48c;background-color:#ffffff">#D2B48C</td>
    <td aria-theme="dark" style="color:#dcdcaa;background-color:#1e1e1e">#DCDCAA</td>
  </tr>
  <tr>
    <td>Tan (Alt)</td>
    <td aria-theme="light" style="color:#74531f;background-color:#ffffff">#74531F</td>
    <td aria-theme="dark" style="color:#dcdcaa;background-color:#1e1e1e">#DCDCAA</td>
  </tr>
  <tr>
    <td>Yellow</td>
    <td aria-theme="light" style="color:#ffff00;background-color:#ffffff">#FFFF00</td>
    <td aria-theme="dark" style="color:#eff284;background-color:#1e1e1e">#EFF284</td>
  </tr>
  <tr>
    <td>Yellow (Alt)</td>
    <td aria-theme="light" style="color:#ffee62;background-color:#ffffff">#FFEE62</td>
    <td aria-theme="dark" style="color:#eff284;background-color:#1e1e1e">#EFF284</td>
  </tr>
  <tr>
    <td>Green</td>
    <td aria-theme="light" style="color:#008000;background-color:#ffffff">#008000</td>
    <td aria-theme="dark" style="color:#57a64a;background-color:#1e1e1e">#57A64A</td>
  </tr>
  <tr>
    <td>Dark Green</td>
    <td aria-theme="light" style="color:#006400;background-color:#ffffff">#006400</td>
    <td aria-theme="dark" style="color:#608b4e;background-color:#1e1e1e">#608B4E</td>
  </tr>
  <tr>
    <td>Teal</td>
    <td aria-theme="light" style="color:#008080;background-color:#ffffff">#008080</td>
    <td aria-theme="dark" style="color:#4ec9b0;background-color:#1e1e1e">#4EC9B0</td>
  </tr>
  <tr>
    <td>Teal (Alt)</td>
    <td aria-theme="light" style="color:#2b91af;background-color:#ffffff">#2B91AF</td>
    <td aria-theme="dark" style="color:#4ec9b0;background-color:#1e1e1e">#4EC9B0</td>
  </tr>
  <tr>
    <td>Blue</td>
    <td aria-theme="light" style="color:#0000ff;background-color:#ffffff">#0000FF</td>
    <td aria-theme="dark" style="color:#569cd6;background-color:#1e1e1e">#569CD6</td>
  </tr>
  <tr>
    <td>Navy</td>
    <td aria-theme="light" style="color:#000080;background-color:#ffffff">#000080</td>
    <td aria-theme="dark" style="color:#55aaff;background-color:#1e1e1e">#55AAFF</td>
  </tr>
  <tr>
    <td>Magenta</td>
    <td aria-theme="light" style="color:#ff00ff;background-color:#ffffff">#FF00FF</td>
    <td aria-theme="dark" style="color:#ffaaf9;background-color:#1e1e1e">#FFAAF9</td>
  </tr>
  <tr>
    <td>Purple</td>
    <td aria-theme="light" style="color:#800080;background-color:#ffffff">#800080</td>
    <td aria-theme="dark" style="color:#d8a0df;background-color:#1e1e1e">#D8A0DF</td>
  </tr>
  <tr>
    <td>Purple (Alt 1)</td>
    <td aria-theme="light" style="color:#8f08c4;background-color:#ffffff">#8F08C4</td>
    <td aria-theme="dark" style="color:#ca79ec;background-color:#1e1e1e">#CA79EC</td>
  </tr>
  <tr>
    <td>Purple (Alt 2)</td>
    <td aria-theme="light" style="color:#951795;background-color:#ffffff">#951795</td>
    <td aria-theme="dark" style="color:#ca79ec;background-color:#1e1e1e">#CA79EC</td>
  </tr>
  <tr>
    <td>Gray</td>
    <td aria-theme="light" style="color:#808080;background-color:#ffffff">#808080</td>
    <td aria-theme="dark" style="color:#9b9b9b;background-color:#1e1e1e">#9B9B9B</td>
  </tr>
  <tr>
    <td>Gray (Alt 1)</td>
    <td aria-theme="light" style="color:#c0c0c0;background-color:#ffffff">#C0C0C0</td>
    <td aria-theme="dark" style="color:#717171;background-color:#1e1e1e">#717171</td>
  </tr>
  <tr>
    <td>Gray (Alt 2)</td>
    <td aria-theme="light" style="color:#7a7a7a;background-color:#ffffff">#7A7A7A</td>
    <td aria-theme="dark" style="color:#8a8a8a;background-color:#1e1e1e">#8A8A8A</td>
  </tr>
  <tr>
    <td>Gray (Alt 3)</td>
    <td aria-theme="light" style="color:#555555;background-color:#ffffff">#555555</td>
    <td aria-theme="dark" style="color:#c8c8c8;background-color:#1e1e1e">#C8C8C8</td>
  </tr>
</table>

> [!NOTE]
> Only the red, green, and blue color components are considered when mapping a color from light to dark, and the mapped dark color will maintain the same transparency (alpha component) as the original color.  For instance, the semi-transparent white color `#80FFFFFF` will be mapped as the semi-transparent black color `#80000000`.

## Importing Visual Studio Settings

Prior to version 24.1, the most common method of supporting dark themes was to import a Visual Studio Settings file (*.vssettings*) pre-configured with colors suitable for use with a dark theme.  While importing a VS Settings file is still supported, it is no longer recommended since dark themes are natively supported.

> [!WARNING]
> When a *.vssettings* file is imported, the color properties of the registered highlighting styles are replaced with the color values defined in the file, and any changes to a highlighting style's color will update the current color map.  When importing settings intended for use with a dark theme, it is recommended to have a dark theme active before importing so the correct color map is updated.

See the "Importing Visual Studio Settings" section of the [Highlighting Style Registries](highlighting-style-registries.md) topic for more information on importing a Visual Studio settings file.