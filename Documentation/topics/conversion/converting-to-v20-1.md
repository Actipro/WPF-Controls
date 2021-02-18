---
title: "Converting to v20.1"
page-title: "Converting to v20.1 - Conversion Notes"
order: 89
---
# Converting to v20.1

The 20.1 version made major updates and improvements to themes and types defined in the Shared Library.  The goal of this version was to modernize our UI control features/themes, and to make theme customization much easier.

The 'Complete Update Details' section in this topic gives information on the breaking API changes made, such as renamed/removed types/members. The sections before it walk through some selected larger breaking changes in detail.

## Theme Name Changes

The old `ThemeName` enumeration that had all our predefined themes has been marked obsolete and a new static [ThemeNames](xref:ActiproSoftware.Windows.Themes.ThemeNames) class with constant theme names has been added in its place.  The use of string constants is better than prior use of an enumeration, since a `ToString` call on the values is no longer necessary.

As part of this change, we updated a number of built-in theme names for clarity per the list below:

- `Generic` - Removed since the Microsoft operating systems that had non-Metro themes are now all end-of-life.  Going forward, the `MetroLight` theme is the default for all operating systems.
- `Classic` - Removed since Windows 10 no longer supports a Classic theme.
- `MetroLightBlue` - Renamed to `OfficeColorfulBlue`.
- `MetroLightCyan` - Renamed to `OfficeColorfulTeal`.
- `MetroLightGreen` - Renamed to `OfficeColorfulGreen`.
- `MetroLightOrange` - Renamed to `OfficeColorfulOrange`.
- `MetroLightPurple` - Renamed to `OfficeColorfulPurple`.
- `MetroLightRed` - Renamed to `OfficeColorfulRed`.
- `MetroLightRoyal` - Renamed to `OfficeColorfulIndigo`.
- `MetroWhiteBlue` - Renamed to `OfficeWhiteBlue`.
- `MetroWhiteCyan` - Renamed to `OfficeWhiteTeal`.
- `MetroWhiteGreen` - Renamed to `OfficeWhiteGreen`.
- `MetroWhiteOrange` - Renamed to `OfficeWhiteOrange`.
- `MetroWhitePurple` - Renamed to `OfficeWhitePurple`.
- `MetroWhiteRed` - Renamed to `OfficeWhiteRed`.
- `MetroWhiteRoyal` - Renamed to `OfficeWhiteIndigo`.
- `OfficeBlack` - Renamed the optional old Aero-style Office 2010 black theme to `Office2010Black` so as not to be confused with modern Office themes.  See the "Aero and Office 2010 Theme Updates" section below for notes on using this theme.
- `OfficeBlue` - Renamed the optional old Aero-style Office 2010 blue theme to `Office2010Blue` so as not to be confused with modern Office themes.  See the "Aero and Office 2010 Theme Updates" section below for notes on using this theme.
- `OfficeSilver` - Renamed the optional old Aero-style Office 2010 silver theme to `Office2010Silver` so as not to be confused with modern Office themes.  See the "Aero and Office 2010 Theme Updates" section below for notes on using this theme.

Other new predefined theme names were added to the [ThemeNames](xref:ActiproSoftware.Windows.Themes.ThemeNames) enumeration as well.

## ThemeManager Enhancements and Theme Definitions

A primary focus for this version has been making a new theme generation system that dynamically generates a complete theme based on theme definition options.  In the past, a theme was implemented with hundreds of predefined asset resources (brushes, thicknesses, etc.) in a resource dictionary.  With the new system, resource dictionaries are still used, but their contents are dynamically generated at run-time.  Theme definitions (described in the [Architecture](../themes/architecture.md) topic) have many options that determine what is generated.  For instance, colors can be adjusted to match company branding, checkboxes can be set to use accented appearances, and so on.  Theme definitions can be edited and visualized with the new Theme Designer application described below.

Note that the tinting feature for themes is no longer available in the newer system since it only worked properly on the old predefined theme resource dictionaries.  That being said, if you use the optional Aero-style themes, they still use predefined theme resource dictionaries and can be tinted.

`ThemeManager` now has a property that uses various API calls to determine which application mode (light, dark, or high-contrast) is requested by the system.  An event is raised when this system preference changes (such as the system enters a high-contrast theme).  The theme manager can also be configured to automatically switch application themes based on which application mode is selected by the end user.  See the [Getting Started](../themes/getting-started.md) topic for more information.

`ThemeManager` added a couple helper properties that can return whether animation should be supported on the current system, and if hardware-accelerated graphics are available.

## Theme Designer Application

Actipro's WPF Theme Designer application makes it easy to fully configure a theme definition.  There are settings for everything from colors and grayscale saturation amounts to corner radii and where accent colors are used.  With every theme definition setting change, the application theme is immediately visualized so that you can see the exact outcome of the theme definition configuration.  The Theme Designer even generates the code necessary to paste into your application `OnStartup` to replicate the theme definition.

See the [Theme Designer Application](../themes/theme-designer.md) topic for more information on the application.

## WindowChrome Enhancements

The `WindowChrome` class, which renders a custom chrome for a `Window`, received many enhancements, the usage of which are described in the [WindowChrome](../themes/windowchrome.md) topic.

Windows can now render different title bar text than what is displayed in the Windows taskbar.  Title bar text can be hidden, or even completely replaced with custom header UI.

Custom UI can optionally be injected on both the left and ride side of the title bar.  Left-side custom UI appears next to the window title bar icon.  Right-side custom UI appears next to the window system title bar buttons (minimize, maximize, close).  All custom UI areas support MVVM concepts if desired, since object-based content and `DataTemplate`-based properties are provided for each.

Numerous options are available for controlling the layout of all title bar elements.  The minimum title bar height can be set.  The final arranged title bar height, and widths of the left/right content areas can be retrieved, which allows other elements in the window UI to position themselves in alignment if desired.

Normally the title bar renders completely separately from the window's content area.  A property has been added that optionally supports merging the title bar and content areas in various ways.  One option extends the `Window.Background` brush to fill the title bar background.  Another option keeps the `Window.Background` over the normal content area, but moves the `Window.Content` to arrange in the title bar as well.  There is a final option that moves both the `Window.Background` and `Window.Content` to render in the title bar.

The window's system menu, the one displayed when clicking the title bar icon or right-clicking the title bar, has been updated to show a themeable WPF-based menu instead of the typical Win32-based menu.  The new menu is fully customizable via an event, allowing you to easily inject custom menu options.

`WindowChrome` now has support for animated overlays containing temporary custom content that is positioned over the entire window, such as for a home screen, an Office-like Backstage, or a processing indicator.

An alternate title bar style can be displayed for themes with a title bar background that is based on an accent color.  The alternate style will render the title bar more like a traditional title bar.  This is commonly seen when opening an Office-like Backstage overlay.

The style used for the custom window chrome has been simplified so that there is no separate template for Aero glass (due to glass support being removed) or for Classic/HighContrast themes.  Now there is a single template used for all chromed windows.  Similar changes were made to `RibbonWindow`.

See the [WindowChrome](../themes/windowchrome.md) topic and many new WindowChrome-related samples for details on all the new features.

## DynamicImage and ImageProvider

One of the best enhancements in this version is to the [DynamicImage](../shared/windows-controls/dynamicimage.md) control and the new [ImageProvider](../themes/image-provider.md) class that it uses to manipulate images for various scenarios.

Features include:

- Chromatic adaptation (color shifting) for images, which allows images designed for light themes to be automatically adjusted for use in dark themes.
- Converting a monochrome vector image to render in the current foreground color.
- Dynamic loading of pre-defined high-DPI and/or theme-specific image variations for raster images.
- Conversion of images to grayscale.
- Conversion of images to monochrome, in a specified color.

Some of these capabilities are extremely important when supporting various themes.

The [ImageConverter](../shared/value-converters.md) value converter has been improved with optional `Width`, `Height`, and `ImageProvider` properties that are passed along to the [DynamicImage](../shared/windows-controls/dynamicimage.md) instances that are created.

## Glyphs

New shared glyph resources have been added and control templates throughout the Actipro libraries have been updated to make use of the glyphs for consistency.  Modern themes now use chevron-style arrow glyphs.

## Native Control Themes

### Menus and Popups

A new [TitleBarMenu](xref:ActiproSoftware.Windows.Controls.TitleBarMenu) control was added, which is a `Menu` control that is tailored for usage in window title bars.  It updates its top-level menu items to render properly in title bars, and wraps menu items to new lines in overflow scenarios so that they all remain accessible.

Styles for native menu items have been improved by increasing their size, adjusting element alignment, and rendering input gesture text in a lower-contrast color.  Theme definition options are available for menu corner radius and padding, along with menu item column widths and padding.

A theme definition option is available for popup shadow direction.

A new [reusable style](../themes/reusable-styles-and-templates.md) was added for `MenuItem` that gives the menu item an Office-like menu heading appearance.

### ScrollBars

The ScrollBar template was updated and new theme resources added to support thumb brush, thickness, and corner radius customization.  These new theme resources allow you to create thin-looking thumbs and/or rounded ends.

A new `ScrollThemeProperties.HasButtons` attached property was added that can be set on `ScrollViewer` or `ScrollBar` instances to determine if they have buttons, or only a thumb.  A new theme resource provides the default for this setting.

### Checks and Radio Buttons

CheckBox and RadioButton bullets have been updated to scale based on the control's font size.  When using a large font size, the bullet will automatically scale up in proportion.

Theme definition options are available for bullet glyph kind (normal, inverted, accent), bullet relative size, border width, and CheckBox corner radius.  Bullet relative size determines the relative size of the bullet compared to the control's font size.  For instance, a large bullet size will render the bullet at nearly double the height of the control's font size.  A border width of `2` can look best when using a large bullet relative size.

Check glyphs in CheckBox and Menu controls have been modernized.

The CheckBox and RadioButton templates have been updated to center the bullet and content vertically by default, which is the most common usage scenario.  Set the `VerticalContentAlignment` property to `Top` to use a top-aligned bullet, and adjust the `Padding` property as needed.

### Other Controls

Various Button, ToolBar, Expander, Slider, and other related control templates were updated to have a more modern appearance.

## PopupButton (Shared Library)

The Shared Library's [PopupButton](../shared/windows-controls/popupbutton.md) was one of our oldest controls and we took time to modernize it for this version.

The control's popup used to be built completely via code and didn't have a default border/background.  In this version, we updated the control template to include the `Popup` right in the template, and with a default border/background defined.  New [PopupBackground](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupBackground), [PopupBorderBrush](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupBorderBrush), [PopupBorderThickness](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupBorderThickness), [PopupCornerRadius](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupCornerRadius), and [PopupPadding](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupPadding) properties were added to support this.

If you customized the control's template, you must now include a `Popup` with name `PART_Popup`.  Only bind the `Popup.IsOpen` property to [IsPopupOpen](xref:ActiproSoftware.Windows.Controls.PopupButton.IsPopupOpen) when the [PopupMenu](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupMenu) property is `null`.

If the [PopupMenu](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupMenu) property is specified, it will now take priority over popup content supplied via [PopupContent](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupContent), [PopupContentTemplate](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupContentTemplate), and [PopupContentTemplateSelector](xref:ActiproSoftware.Windows.Controls.PopupButton.PopupContentTemplateSelector).

The [DisplayMode](xref:ActiproSoftware.Windows.Controls.PopupButton.DisplayMode) property default was changed from `Split` to `Merged`, which is a far more common use of the control.  A new `CenterMerged` option was added to the [PopupButtonDisplayMode](xref:ActiproSoftware.Windows.Controls.PopupButtonDisplayMode) enumeration that renders similar to `Merged` but centers the content and indicator instead of the indicator being on the right side.  The `ButtonOnly` option now will show a popup/menu like other modes and renders like `Merged` other than not showing a popup indicator.

The [IsAutoFocusOnOpenEnabled](xref:ActiproSoftware.Windows.Controls.PopupButton.IsAutoFocusOnOpenEnabled) property default was changed to `true`, which is the most common usage.

The `IsPreviewMouseDownHandled` property was removed since it wasn't used.  The `IsRounded` property was removed, and can be replaced by setting the attached `themes:ThemeProperties.CornerRadius` property to `0` to mimic a `false` setting.  The static `ClosePopup` property was renamed to `ClosePopupCommand` for clarity.

## Grids

The `PropertyGrid` control gained some big updates related to the explicit specification of properties to display.  Now when a `PropertyModel` is specified in the `PropertyGrid.Properties` collection, its `CanAutoConfigure` property is set to `true`, and its `Value` property is bound to a target property, the `TypeDescriptorFactory` will automatically fill in most of the `PropertyModel` properties for you.  In summary, these updates make it very simple to mock up an explicit list of properties to display in a property grid with full editing features, even when those properties come from various locations.

The `PropertyModel` class has be updated to inherit `FrameworkElement` instead of `DepedencyObject`.  This change works around issues in core WPF where XAML data binding wouldn't work ("Cannot find governing FrameworkElement..." error) without use of proxy objects.  None of the visual/input-related properties from `FrameworkElement` are used in any way.

The `IPropertyModel.StandardValuesSelectedValuePath` property was added so that property editors that use a `ComboBox` to display a limited list of standard values can set the `ComboBox.SelectedValuePath` property.

The `PropertyGrid.ImmediateStringValueTemplate` property and the related `DefaultValueTemplateKind.ImmediateString` enum value was added to select it.  The new `DataTemplate` is the same as the `DefaultStringValueTemplate` but uses a binding that updates immediately as text is typed instead of only on focus loss.

## Ribbon

Massive updates were made to the `Ribbon` and its related controls to modernize it and match the latest Office version's appearance.  There are new glyphs, subtle animations, better balance of whitespace, better shadows, and much more.

Several Aero glass-related properties on `RibbonWindow` and `Ribbon` were removed as part of the Shared Library's `WindowChrome` changes.

### Fonts

'Segoe UI' is no longer set on the Ribbon and its controls as the default font.  A default font size, which previously tended to be smaller than the default font size, is no longer set either.  This helps remove font appearance inconsistency between the Ribbon and other portions of an application.

### RibbonWindow

`RibbonWindow` was updated to not use a custom style, and now uses the default `WindowChrome` style.  This change allows `RibbonWindow` to render the same as other windows in your application that use `WindowChrome`, and to make use of new `WindowChome` features.

`RibbonWindow` used to have a special property for display of a `StatusBar` along its bottom edge.  Since a custom `RibbonWindow` style is no longer used, this feature has been removed.  To work around this change, simply include the `StatusBar` at the bottom of your window's content.

### Application Button

The application button appearance has been updated to always render like Ribbon tabs.

The `Ribbon` previously had a `UseScenicLayout` property that when set to false, would enable an old Office 2007-style orb application button.  We no longer support Office 2007 styles and thus have removed this option in favor of the more modern application button style.

### Backstage

In modern themes, the `Backstage` has been updated to use new animated `WindowChrome` overlay features when it is used via a `Ribbon` in a `RibbonWindow`.

The `Backstage` close button's appearance has been modernized.

### Buttons

All buttons (`Button`, `PopupButton`, `SplitButton`, etc.) have had their appearances in various contexts modernized.  This includes updating buttons in menu item contexts to seamlessly match the appearance of Actipro's improved native WPF menu item appearances.  Buttons displayed as tab panel items (i.e. next to the Ribbon tabs) have a new appearance to match Office.

### Contextual Tab Group

In making changes to match the latest Office themes, contextual tab groups no longer render any UI (such as a group background and label) in the Window title bar.

### Gallery

The category heading, filter button, popup gallery separator, and popup gallery resize grip appearances have been modernized.  Color picker items have increased in size for easier touch accessibility.  In-ribbon scroll animation was made faster.

### Group

Whitespace improvements were made and collapsed group appearances were modernized.  An improved dialog launcher glyph was created.

### QuickAccessToolBar

Adjusted and modernized the `QuickAccessToolBar` appearance.

### RecentDocumentMenu

Improved the glyph used to indicate pinned items.

### Screen Tip

Adjusted whitespace for content.

### Separator

Adjusted separator appearances for various contexts, and added a new `Large` variant for the `MenuItem` context that renders a line and heading label.

### Tab

Updated the appearance to match Office, and added an animated underline for the selected tab.  Selecting a tab now animates the tab's content into view.

### ToggleMinimizationButton

Updated the glyph and added directly into the `Ribbon` to automatically show when `Ribbon.IsMinimizable` is `true`.  If an instance of this button was previously placed in the `Ribbon.TabPanelItems`, it can be removed now that it's built into Ribbon.

## Docking/MDI

The new [ShadowChrome](xref:ActiproSoftware.Windows.Controls.Primitives.ShadowChrome) class was placed in certain Docking/MDI templates in place of the older `DropShadowChrome`.  Certain properties changed as a result.

- [StandardSwitcher](xref:ActiproSoftware.Windows.Controls.Docking.StandardSwitcher)'s `DropShadowZOffset` property was removed.  A new `ShadowElevation` property was added.
- [WindowControl](xref:ActiproSoftware.Windows.Controls.Docking.WindowControl)'s `DropShadowColor` and `DropShadowZOffset` properties were removed.  A new `ShadowElevation` property was added.

## Views

The new [MultiColumnPanel](xref:ActiproSoftware.Windows.Controls.Views.MultiColumnPanel) control can arrange child elements in columns based on available width.

## Font Size Theme Resources

When using Actipro themes in your app, font sizes are now configurable.  A theme definition option allows the base font size for the application to be set.  This font size is applied via Actipro theme styles to top-level containers like `Window`, `ToolTip`, etc.

The base font size becomes the "medium" font size for the various font size theme resources that are available.  Numerous font sizes smaller and larger than the "medium" font size are also available via theme resources.  By using these theme resources instead of fixed font size numbers in your application, you can ensure font size consistency and can also easily adjust all font sizes throughout your application via a simple theme definition property change.

## Aero Glass Support Removed

Windows 7 was the last Windows version to support Aero glass.  Windows 7 is now end-of-life and no longer supported by Microsoft, so we have removed all glass-related code from our themes and controls.  This allowed us to simply our code by removing a lot of unnecessary complex internal logic for Aero glass features.

## Aero and Office 2010 Theme Updates

Windows 7 and Office 2010 are both end-of-life and are no longer support by Microsoft.  Some customers may still wish to use Aero-style themes (AeroNormalColor, OfficeBlack, OfficeBlue, and OfficeSilver) in their applications so we are keeping these available as options.

We've moved the four Aero-style themes to a new `ActiproSoftware.Themes.Aero.Wpf.dll` assembly to reduce the size of the Shared assembly.  The four themes can now only be used if this optional Aero assembly is referenced by your application, and you perform this register call in your application startup logic:

```csharp
ThemesAeroThemeCatalogRegistrar.Register();
```

The Office 2010 themes used to require a similar register call via `ThemesOfficeThemeCatalogRegistrar`, but that class was renamed to `ThemesAeroThemeCatalogRegistrar`.

Again, the `AeroNormalColor` theme will no longer work unless the optional Aero assembly is referenced and the `Register` call is made in app startup.

## Tinting

The older style of predefined asset resources in a resource dictionary supported a tinting mechanism as a way of altering colors if you didn't like the defaults.  While the Aero-style themes support this mechanism since they are still implemented with the older predefined resource dictionary design, generated themes created with theme definitions do not support tinting.  The reason is that generated themes allow you to customize the grayscale palette using various theme definition options, so tinting isn't needed.

As such, various tint-related types (like `TintedThemeCatalog`, `TintGroup`, etc.) that now only work with the Aero-style themes have been moved into the `ActiproSoftware.Themes.Aero.Wpf.dll` assembly.

## Windows XP Themes Removed

Windows XP has been end-of-life and no longer supported by Microsoft for several years, so the related Windows XP themes (LunaNormalColor, LunaHomestead, LunaMetallic, and RoyaleNormalColor) we provided were removed.

The `ActiproSoftware.Themes.Luna.Wpf.dll` assembly that contained those themes is also no longer available.

## Legacy Docking/MDI Codebase Removed

The 16.1 version made some large improvements to Docking/MDI, including some API changes.  We previously temporarily included the old API as an optional install for those who couldn't immediately upgrade to the newer API.

The `ActiproSoftware.Docking.Legacy.Wpf.dll` and `ActiproSoftware.Docking.Interop.Prism.Legacy.Wpf.dll` assemblies are no longer shipped in the v20.1 distribution.  If you were previously using those assemblies, please upgrade to the newer Docking/MDI API that has been out since v16.1.

## Visual Basic Sample Projects Removed

In consideration that a vast majority of our customers work in C# instead of Visual Basic, and that it takes a significant amount of time to keep the Visual Basic sample projects in sync with their C# equivalents, we have stopped including Visual Basic sample projects.  This will allow our development to move in a more agile way.

If you work in Visual Basic and need to convert sample code from C#, please search the web for "convert c# to vb.net" and you will find many free online tools to assist.

## ANTLR and Irony Add-on Sample Projects Removed

The sample projects for the optional add-ons that integrate ANTLR v3 And Irony parsers with SyntaxEditor are no longer available in the installation due to these add-ons being deprecated.  Contact our support team if you have a need to download the sample projects.

## Complete Update Details

The list below is the entire detailed update log, including breaking changes.

### ThemeManager (Shared Library)

- `RegisteredThemeDefinitions` property, and `RegisterThemeDefinition`, `UnregisterThemeDefinition`, `CreateDefaultThemeDefinition`, and `GetThemeDefinition` methods added, all of which deal with theme definitions.

- `SystemApplicationMode` property added, which returns the system preferred theme mode (light, dark, or high-contrast).  Related `SystemApplicationModeChanged` event added.

- `RegisterAutomaticThemes` method added, which can automatically switch application themes to specified themes when SystemApplicationMode changes.

- `IsAnimationSupported` property added, which determines if UI animation should be enabled.

- `IsGraphicsHardwareAccelerationSupported` property added, which returns whether hardware-accelerated graphics are available on the system.

- `SystemTheme` property and `SystemThemeChanged` event removed, since they are no longer useful with Windows 7 being end-of-life.

- The AeroNormalColor (Windows 7 style) theme was moved to a new optional `ActiproSoftware.Themes.Aero.Wpf.dll` assembly, and now requires a `ThemesAeroThemeCatalogRegistrar.Register()` call at app startup to use.

- The Aero-style Office 2010 themes (OfficeBlack, OfficeBlue, and OfficeSilver) were moved from the old `ActiproSoftware.Themes.Office.Wpf.dll` to the new `ActiproSoftware.Themes.Aero.Wpf.dll` assembly.  Use `ThemesAeroThemeCatalogRegistrar` instead of `ThemesOfficeThemeCatalogRegistrar` to register these optional themes.

- The LunaNormalColor, LunaHomestead, LunaMetallic, and RoyaleNormalColor themes defined in the optional `ActiproSoftware.Themes.Luna.Wpf.dll` are no longer available.

### ThemeName (Shared Library)

- Use the new `ThemeNames` static class with string constant theme names in place of `ThemeName` moving forward.
- The `LunaNormalColor`, `LunaHomestead`, `LunaMetallic`, and `RoyaleNormalColor` members were removed.

### WindowChrome (Shared Library)

- Refined and improved our custom window chrome template, adding a lot of new customizable features along the way.
- `HasTitleBar` property added, which hides the title bar when set to `false`.
- Attached `TitleBarHeader` property added, which can be set on a `Window` to use different title text in the title bar.  Set it to null to hide the window's title bar text.
- `TitleBarHeaderTemplate` property added, which can be set to replace title bar text with custom header UI.
- `TitleBarLeftContentTemplate` property and attached `TitleBarLeftContent` property added, both of which can be used to inject custom UI in the left side of the window's title bar, next to the icon.
- `TitleBarRightContentTemplate` property and attached `TitleBarRightContent` property added, both of which can be used to inject custom UI in the right side of the window's title bar, next to the system title bar buttons.
- `TitleBarContentTemplate` property renamed to `TitleBarRightContentTemplate`.
- `TitleBarMinHeight` attached property added, which sets the minimum height of the title bar.
- `TitleBarHeaderMargin` property added, which is the margin around the header.
- `TitleBarHeaderMinWidth` property added, which sets the header minimum width when not all title bar elements fit in the available space.
- `TitleBarLeftContentMaxWidthOverflowPercentage` and `TitleBarRightContentMaxWidthOverflowPercentage` properties added, set the maximum percentage of total title bar space to use for each area when not all title bar elements fit in the available space.
- `TitleBarMergeKind` property added, which can be set to `BackgroundOnly` to have the `Window.Background` brush fill the title bar area, or set to `Full` to have the `Window.Content` extend to fill the title bar area.
- `TitleBarTextAlignment` property renamed to `TitleBarHeaderAlignment` and changed to return `HorizontalAlignment`.
- `GlassThickness`, `IsGlassActive`, and `IsGlassEnabled` properties removed since Aero glass is no longer supported.

### DynamicImage and ImageProvider (Shared Library)

- `ImageProvider` class added that supports adapting source images for various scenarios (themes, scales, grayscale, monochrome, etc.).
- `DynamicImage` control updated to call into `ImageProvider` to support rich functionality.
- `DynamicImage` control updated to watch for theme, DPI, and foreground changes and adapt its image when appropriate.
- `UseMonochrome` property added to the `DynamicImage ` control to support monochrome images.
- `ImageSourceContentConverter` and `ImageSourceContentConversionMode` types moved to the `Legacy` assembly where they will be deprecated.  Use the improved `DynamicImage` control instead.
- `ImageSourceContentConverter.CanConvertToMonochrome` attached property replaced with the new `ImageProvider.CanAdapt` attached property.
- `ImageSourceContentConverter.BrightnessThreshold` static property replaced with the new `ImageProvider.MonochromeBrightnessThreshold` property.

### SharedResourceKeys (Shared Library)

These style updates were made:

- `MenuHeadingStyleKey` added to render special `MenuItems` as text-based headings.
- `ToolWindowTitleBarButtonBaseAlternateStyleKey` renamed to `CloseToolWindowTitleBarButtonBaseStyleKey` to clarify its purpose.
- `WindowTitleBarButtonBaseAlternateStyleKey` renamed to `CloseWindowTitleBarButtonBaseStyleKey` to clarify its purpose.

These glyph template updates were made:

- `LargeLeftArrowGlyphTemplateKey` (formerly `ScrollLeftGlyphTemplateKey`), `LargeUpArrowGlyphTemplateKey`, `LargeRightArrowGlyphTemplateKey` (formerly `ScrollRightGlyphTemplateKey`), `LargeDownArrowGlyphTemplateKey` added for large sized arrows.

- `LeftArrowGlyphTemplateKey`, `UpArrowGlyphTemplateKey`, `RightArrowGlyphTemplateKey`, `DownArrowGlyphTemplateKey` added for normal sized arrows.

- `SmallLeftArrowGlyphTemplateKey`, `SmallUpArrowGlyphTemplateKey`, `SmallRightArrowGlyphTemplateKey`, `SmallDownArrowGlyphTemplateKey` added for small sized arrows.

- `ExternalWindowGlyphTemplateKey` added for use in buttons that open an external window.

- `MenuCheckGlyphTemplateKey`, `MenuIndeterminateCheckGlyphTemplateKey`, and `MenuPopupGlyphTemplateKey` added for use in menu items.

- `MinusGlyphTemplateKey` and `PlusGlyphTemplateKey` added for use in decrease/increase buttons.

- `SmallDropDownArrowGlyphTemplateKey` added for use in small drop-down buttons.

- `SmallMinusGlyphTemplateKey` and `SmallPlusGlyphTemplateKey` added for use in decrease/increase buttons.

- `ToolBarOverflowDropDownArrowGlyphTemplateKey` added for toolbar overflow buttons.

- `WindowTitleBarButtonBackGlyphTemplateKey` added for a window title bar Back button.

- `WindowTitleBarButtonMenuBarsGlyphTemplateKey` added for a window title bar Menu button.

### AssetResourceKeys (Shared Library)

This list contains changes and removals only:

- `Accent*BrushKey` properties removed and replaced with more comprehensive PrimaryAccent* properties.
- `ApplicationButton*BrushKey` properties removed since the Ribbon application button is now themed the same as Ribbon tabs.
- `ButtonHighlight*BrushKey` properties removed since Luna themes are no longer supported.
- `*BulletChromeBorderStyleKey` and `*ElementChromeBorderStyleKey` properties removed since Classic themes are no longer supported.
- `ContainerLight*BrushKey`, `ContainerMedium*BrushKey`, and `ContainerDark*BrushKey` properties migrated to new `ContainerBackground*BrushKey` properties.
- `ContainerBorderNormalBrushKey` property to new `ContainerBorder*BrushKey` properties.
- `ContextualTabGroupAreEffectsEnabledBooleanKey`, `ContextualTabGroupUseColorsBooleanKey`, `ContextualTabUseColorsBooleanKey` properties removed since they are no longer used.
- `ControlBackgroundNormalBrushKey` property renamed to `ContainerBackgroundLowBrushKey`.
- `ControlForegroundNormalBrushKey` property renamed to `ContainerForegroundLowNormalBrushKey`.
- `ControlForegroundDisabledBrushKey` property renamed to `ContainerForegroundLowDisabledBrushKey`.
- `DockingWindowContainer*BrushKey` properties renamed to `ToolWindowContainer*BrushKey` for clarity.  Use the new `TabbedMdiContainerBackgroundNormalBrushKey` property in place of `DockingWindowContainerBackgroundNormalBrushKey` for tabbed MDI containers only.
- `ComboBoxUseAlternateStyleBooleanKey`, `DropDownGlyphAlternateBackgroundDisabledBrushKey`, `DropDownGlyphAlternateBackgroundNormalBrushKey`, `DropDownGlyphUseAlternateGeometryBooleanKey` properties removed since Classic themes are no longer supported.
- `DocumentTextAccent1ForegroundNormalBrushKey` and `DocumentTextAccent2ForegroundNormalBrushKey` properties renamed to `DocumentHeading1ForegroundNormalBrushKey` and `DocumentHeading2ForegroundNormalBrushKey` respectively.
- `EditRulerMarginBorderNormalBrushKey`, `EditSelectionBackgroundFocusedBrushKey`, `EditSelectionForegroundFocusedBrushKey`, `EditSelectionSemiTransparentBackgroundFocusedBrushKey`, `EditSelectionSemiTransparentBackgroundNormalBrushKey`, `EditSelectionSemiTransparentBorderFocusedBrushKey`, `EditSelectionSemiTransparentBorderNormalBrushKey` properties removed since they are no longer used.
- `ExpanderGlyph*BrushKey` properties removed since they are no longer used.
- `ListColumnHeader*BrushKey` and `ListRowHeader*BrushKey` properties consolidated into `ListHeader*BrushKey` properties.
- `ListToggleButtonExpansion*BrushKey` properties renamed to `ListToggleButtonExpanderGlyph*BrushKey` properties.
- `ListToggleButtonExpansionBorderNormalCornerRadiusKey` and `ListToggleButtonExpansionBorderNormalThicknessKey`, `ListToggleButtonExpansionForegroundCheckedBrushKey`, `ListToggleButtonExpansionForegroundHoverBrushKey`, `ListToggleButtonExpansionForegroundNormalBrushKey` properties removed since they are no longer used.
- `MenuSeparatorHighlightNormalBrushKey` property removed due to template simplification.
- `NavigationBarPopupButtonBackground*BrushKey` properties removed since they are no longer used.
- `RaftingWindowBorderActiveBrushKey`, `RaftingWindowBorderInactiveBrushKey`, and `RaftingWindowBorderNormalCornerRadiusKey` properties removed since they are no longer used.
- `RibbonSeparatorBorderNormalBrushKey`, `RibbonSeparatorBorderNormalThicknessKey`, and `RibbonSeparatorOpacityMaskNormalBrushKey ` properties removed since they are no longer used.
- `RibbonTabItemBackgroundFocusedBrushKey`, `RibbonTabItemBackgroundSelectedBrushKey`, `RibbonTabItemBorderFocusedBrushKey`, `RibbonTabItemBorderHoverBrushKey`, `RibbonTabItemForegroundSelectedBrushKey`, `RibbonTabItemInnerBorderFocusedBrushKey`, RibbonTabItemInnerBorderHoverBrushKey properties removed since they are no longer used.
- `RibbonToolBarTrayInnerBorderNormalBrushKey` property removed since it is no longer used.
- `ScrollBarButtonGlyphUseAlternateGeometryBooleanKey`, `ScrollBarButtonHighlightDisabledBrushKey`, `ScrollBarButtonHighlightHoverBrushKey`, `ScrollBarButtonHighlightNormalBrushKey`, `ScrollBarButtonHighlightPressedBrushKey`, `WorkspaceScrollBarButtonHighlightDisabledBrushKey`, `WorkspaceScrollBarButtonHighlightHoverBrushKey`, `WorkspaceScrollBarButtonHighlightNormalBrushKey`, `WorkspaceScrollBarButtonHighlightPressedBrushKey` properties removed since Luna themes are no longer supported.
- `SeparatorHighlightNormalBrushKey` property removed due to template simplification.
- `SliderBackgroundNormalBrushKey`, `SliderThumbHorizontalHighlightDisabledBrushKey`, `SliderThumbHorizontalHighlightFocusedBrushKey`, `SliderThumbHorizontalHighlightHoverBrushKey`, `SliderThumbHorizontalHighlightNormalBrushKey`, `SliderThumbHorizontalHighlightPressedBrushKey`, `SliderThumbVerticalHighlightDisabledBrushKey`, `SliderThumbVerticalHighlightFocusedBrushKey`, `SliderThumbVerticalHighlightHoverBrushKey`, `SliderThumbVerticalHighlightNormalBrushKey`, `SliderThumbVerticalHighlightPressedBrushKey` properties removed due to template simplification.
- `StatusBarSeparatorOpacityMaskNormalBrushKey` property removed since it is no longer used.
- `TabControlHighlightNormalBrushKey`, `TabItemHighlightDisabledBrushKey`, `TabItemHighlightHoverBrushKey`, `TabItemHighlightNormalBrushKey`, `TabItemHighlightSelectedHoverBrushKey`, `TabItemHighlightSelectedNormalBrushKey` properties removed since they are no longer used.
- `TabbedMdiContainerTabControlBackgroundActiveNormalBrushKey`, `TabbedMdiContainerTabItemBackgroundActiveSelectedBrushKey`, `TabbedMdiContainerTabItemBorderActiveSelectedBrushKey`, `TabbedMdiContainerTabItemForegroundActiveSelectedBrushKey`, `TabbedMdiContainerTabItemGlyphBackgroundActiveFocusedBrushKey`, `TabbedMdiContainerTabItemGlyphBackgroundActiveSelectedBrushKey`, `TabbedMdiContainerTabItemGlyphBackgroundHoverBrushKey`,`TabbedMdiContainerTabItemGlyphBackgroundInactiveSelectedBrushKey`, `ToolWindowContainerTabControlForegroundNormalBrushKey` properties removed due to template simplification.
- `TabItemBackgroundSelectedHoverBrushKey` and `TabItemBorderSelectedHoverBrushKey` properties removed since they are no longer used.
- `ThumbBorder*BrushKey` properties consolidated into a single `ThumbBorderNormalBrushKey` property.
- `ToolBarGripperHighlightNormalBrushKey` property removed since it is no longer used.
- `WindowGlassExtensionBackgroundActiveBrushKey` and `WindowIsGlassEnabledBooleanKey` properties removed since Aero glass is no longer supported.
- `WindowNonGlassExtensionBackgroundActiveBrushKey` and `WindowNonGlassExtensionBackgroundInactiveBrushKey` properties renamed to `WindowTitleBarExtensionBackgroundActiveBrushKey` and `WindowTitleBarExtensionBackgroundInactiveBrushKey` respectively.
- `WindowTitleBarInnerBorderActiveBrushKey` and `WindowTitleBarInnerBorderInactiveBrushKey` properties removed since they are no longer used.
- `WindowTitleBarButtonHighlight*BrushKey` and `WindowTitleBarButtonInnerBorder*BrushKey` properties removed since they are no longer used.
- `WindowTitleBarButtonAlternate*BrushKey` properties renamed to `CloseWindowTitleBarButton*BrushKey` to better clarify their purpose.
- `WindowTitleBarButtonAlternate*InactiveDisabledBrushKey` and `WindowTitleBarButtonAlternate*InactiveNormalBrushKey` properties removed since they were always set to the same values as the non-"Alternate" equivalents.
- `WindowTitleBarButtonAlternateMarginNormalThicknessKey` property renamed to `WindowTitleBarButtonMarginNormalThicknessKey`.
- `WindowTitleBarForegroundShadowActiveBrushKey` property removed since Luna themes are no longer supported.
- `WindowTitleBarQuickAccessToolBarAlignTopBooleanKey` and `WindowTitleBarUseSystemFontBooleanKey` properties removed since they are no longer used.
- `WindowTitleBarToolBarTray*BrushKey` properties removed since they are no longer used.
- `WorkspaceDark*BrushKey`, `WorkspaceMedium*BrushKey`, and `WorkspaceLight*BrushKey` properties replaced with more comprehensive `ContainerBackground*BrushKey` properties.
- `WorkspaceVeryDarkForegroundNormalBrushKey` property removed and `WorkspaceVeryDarkBackgroundNormalBrushKey` property renamed to `DockHostBackgroundNormalBrushKey`.

### Tint-Related Types (Shared Library)

- `TintedResourceDictionary`, `TintedResourceDictionaryReference`, `TintedThemeCatalog`, `TintGroup`, `TintGroupCollection`, `TintGroupNames`, and `TintGroupSets` types moved to the `ActiproSoftware.Themes.Aero.Wpf.dll` assembly since they now only work with the older Aero-style themes.

### AnimatedExpanderDecorator (Shared Library)

- `ExpandDuration` and `CollapseDuration` property defaults reduced from `250ms` to `150ms` to speed up animations by default, and easings added to animations.

### GlassWindow (Shared Library)

- `GlassWindow` class removed since Aero glass is no longer supported.

### PopupButton (Shared Library)

- A `Popup` is now built into the `PopupButton` template.
- `PopupBackground`, `PopupBorderBrush`, `PopupBorderThickness`, `PopupCornerRadius`, and `PopupPadding` properties added to support customization of the popup chrome.
- `CenterMerged` option added to `PopupButtonDisplayMode` enumeration.
- `ButtonOnly` display mode option now will show a popup/menu if one is defined.  This allows the `PopupButton` to show popups even when there is no popup indicator.
- `DisplayMode` property default changed to `Merged`, which is the most common usage.
- `IsAutoFocusOnOpenEnabled` property default changed to `true`, which is the most common usage.
- `IsPreviewMouseDownHandled` property removed since it wasn't used.
- `IsRounded` property removed.  Set the attached `themes:ThemeProperties.CornerRadius` property to `0` to mimic setting `IsRounded` to `false`.
- If the `PopupMenu` property is specified, it will now take priority over popup content supplied via `PopupContent`, `PopupContentTemplate`, and `PopupContentTemplateSelector`.
- Static `ClosePopup` property renamed to `ClosePopupCommand` for clarity.

### TitleBarMenu (Shared Library)

- `TitleBarMenu` control added, for use in `WindowChrome` custom title bars.

### TransitionPresenter (Shared Library)

- `DefaultDuration` property default reduced from `500ms` to `200ms` to speed up animations by default.

### Value Converters (Shared Library)

- `CharacterCasingConverter` added that can be used to uppercase or lowercase a string.
- `Width`, `Height`, and `ImageProvider` properties added to `ImageConverter` and passed onto the created `DynamicImage` instance.

### WindowControl (Docking/MDI product)

- `IsTitleBarTextShadowEnabled` property removed since XP themes are no longer supported.

### TypeDescriptorFactory (Grids product)

- A new feature makes the explicit specification of properties via `PropertyGrid.Properties` very simple.  Simply set a `PropertyModel`'s new `CanAutoConfigure` property to `true` and place a `Binding` on the `Value` property to the target property, and the data factory will do the rest.
- `AutoConfigurePropertyModel` method added, which is called for explicitly-defined `PropertyModel` instances whose `CanAutoConfigure` properties are set to `true`.
- `GetDisplayNameFromName` method added that converts a property's `Name` to a `DisplayName` when executing the `AutoConfigurePropertyModel` method.

### IPropertyModel (Grids product)

- `StandardValuesSelectedValuePath` property added to allow setting of `ComboBox.SelectedValuePath` in property editors for limited standard values.

### PropertyModel (Grids product)

- `PropertyModel` now inherits `FrameworkElement` instead of `DependencyObject` to work around core WPF issues with data binding.
- `CanAutoConfigure` property added that can auto-configure most `PropertyModel` properties based on a `Value` binding.

### PropertyGrid (Grids product)

- `ImmediateStringValueTemplate` property added, which is the same as `DefaultStringValueTemplate`, but uses a binding that updates immediately as text is typed instead of only on focus loss.

### DefaultValueTemplateKind (Grids product)

- `ImmediateString` value added that can select the new `PropertyGrid.ImmediateStringValueTemplate``DataTemplate`.

### ContextualTabGroup (Ribbon product)

- `AreEffectsEnabled`, `MaxTitleAreaWidth`, `OuterGlowBrush`, `UseColors` properties removed since contextual tab groups no longer render UI in the title bar.

### Group (Ribbon product)

- `RibbonGroupBackgroundPressedBrushKey`, `RibbonGroupBorderHoverBrushKey`, `RibbonGroupSeparatorOpacityMaskNormalBrushKey` properties removed since they are no longer needed.

### QuickAccessToolBar (Ribbon product)

- `IsApplicationButtonVisible`, `IsItemOuterGlowEnabled` properties removed since they are no longer needed.
- `IsBelowRibbon` property default changed to `false`.

### Ribbon (Ribbon product)

- `ContextualTabGroupContainerHeight`, `ContextualTabGroupContainerOffset`, and `SelectedContextualTabGroupColor` properties removed since contextual tab groups no longer render UI in the title bar.
- `IsGlassActive` and `IsGlassExtensionEnabled` properties removed since Aero glass is no longer supported.
- `IsRibbonWindowIconVisible` property removed since it is no longer needed.
- `IsTitleBarAreaVisible` property removed since it is no longer needed.  Set `QuickAccessToolBarLocation` to `None` to mimic `false` now.
- Static `SystemHasSegoeUIFont` property removed since it is no longer needed.
- `UseScenicLayout` property removed since the Office 2007 orb style of application button is no longer supported.
- `ApplicationButtonDefault16.png` image file that used to be included in the Ribbon assembly and can be set to `Ribbon.ApplicationButtonImageSource` is now included in the Sample Browser project instead.

### RibbonWindow (Ribbon product)

- `GlassExtensionHeight`, `IsGlassActive`, `IsGlassEnabled` properties removed since Aero glass is no longer supported.
- `StatusBar ` property removed since `RibbonWindow` no longer uses a custom template.  Put your `StatusBar` at the bottom of your window's content instead.
- `TitleBarScaleTransform ` property removed since it is no longer needed.

### RibbonWindowTitleBarText (Ribbon product)

- `ApplicationNameBrush`, `DocumentNameBrush`, and `UsesSystemFont` properties removed since they are no longer needed.

### Tab (Ribbon product)

- `IsTinted`, `TintColor`, `TintedSelectedInnerBorderBrush`, `TintedSelectedOuterBorderBrush`, `UseColors` properties removed since they are no longer needed.

### MultiColumnPanel (Views product)

- `MultiColumnPanel` control added that can arrange child elements in columns based on available width.

### AeroWizardTitleBar (Wizard product)

- `AeroWizardTitleBar` class removed since it's no longer used.  See notes below on `AeroWizardWindow` for details.

### AeroWizardWindow (Wizard product)

- `AeroWizardWindow` class removed since it's no longer used.  It was previously based on the `GlassWindow` class, which is no longer applicable with Windows 7 being end-of-life.  See the [Aero Wizards](../wizard/appearance-features/aero-wizard.md) topic for new Window XAML that can be used instead, rendering a Back button in the window's title bar via new `WindowChrome` features.
