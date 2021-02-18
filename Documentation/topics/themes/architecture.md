---
title: "Architecture"
page-title: "Architecture - Themes Reference"
order: 3
---
# Architecture

Actipro Themes contains a complete framework for managing run-time selection of the themes of both Actipro and native controls, but can also be used for your own custom controls as well.

Themes are dynamically generated based on theme definitions, which allow for you to configure various appearance options prior to theme generation.  This topic covers the underlying architecture for the themes and describes how theme definitions are used to configure generated themes.

Actipro's [Theme Designer application](theme-designer.md) comes with the WPF Controls, making it easy to fully configure a theme definition and see how various options affect appearance.  The Theme Designer even generates the code necessary to paste into your application `OnStartup` to replicate the theme definition.

## What are Themes?

Themes are resource dictionaries that define styles and templates for a group of controls.  Each "theme" has its own visual appearance that it creates.  For instance, a Ribbon can be rendered in an Office blue theme or a modern dark theme, among many others.

Some resource dictionaries may be used to provide the styles and templates for controls, while others may simply define asset resources, such as brushes and pens.  Still others may tie those combinations together to provide a complete theme implementation.

The Actipro [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager) allows you to specify a particular theme to use for all Actipro controls, and optionally native WPF controls as well.

## Theme Generation

Prior to v2020.1, each predefined Actipro theme consisted of large resource dictionaries with fixed asset resources (brushes, etc.).  While this worked well, it was difficult to know which asset resources to customize if you wished to override some of the defaults for a theme.  For v2020.1, we updated our theming system to dynamically generate the resource dictionaries at run-time based on a configurable theme definition.  This allows you to adjust the generated theme to suit your needs, such as if you wish to change the hue of accent colors used or general border contrast.

Note that the optional Aero-style themes still use the older design of loading a large resource dictionary with predefined asset resources.  This is done so that they retain their very customized appearance.  All other more modern themes are based on the newer theme definition design.

## Theme Definition Overview

Theme definitions are represented by the [ThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition) class.  They have many options that are interpreted by the Actipro theme generator when dynamically building a resource dictionary containing assets for a theme.

Theme definitions allow for easy configuration of these kinds of options:

- Intent of a white, light, dark, or black theme.
- Base colors used to define the color palette for color families.
- Hue and saturation to tint grayscale colors.
- Color families used for various UI such as accents and windows.
- Font family and font sizes.
- Arrow and glyph kinds.
- Border contrast amounts.
- Corner radii, padding, and margins.
- Title and status bar backgrounds.

## Registering and Using a Theme Definition

This sample code shows how to create a basic [ThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition) whose name is "Custom", register it with the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager), and then tell the theme manager to use it as the current theme.  Note that we also tell the theme manager to theme native WPF controls.  The generated theme will be dark colors with green accents.

```csharp
public partial class App : Application {

	protected override void OnStartup(StartupEventArgs e) {
		// Configure the Actipro theme manager
		ThemeManager.BeginUpdate();
		try {
			// Register the theme definitions for your application
			ThemeManager.RegisterThemeDefinition(new ThemeDefinition("Custom") {
				Intent = ThemeIntent.Dark,
				PrimaryAccentColorFamilyName = ColorFamilyName.Green,
				WindowColorFamilyName = ColorFamilyName.Green,
			});

			// Use the Actipro styles for native WPF controls that look great with Actipro's control products
			ThemeManager.AreNativeThemesEnabled = true;

			// Set the current app theme via a registered theme definition name
			ThemeManager.CurrentTheme = "Custom";
		}
		finally {
			ThemeManager.EndUpdate();

        // ...

        // Call the base method
    	base.OnStartup(e);
    }
	
}
```

Call the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[RegisterThemeDefinition](xref:ActiproSoftware.Windows.Themes.ThemeManager.RegisterThemeDefinition*) method for each theme definition to register.  Multiple theme definitions can be registered with the theme manager, but make sure each theme definition has a unique name.  The [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[CurrentTheme](xref:ActiproSoftware.Windows.Themes.ThemeManager.CurrentTheme) property is later set to the name of the theme to load for the application.  The names of predefined themes is available as constants in the [ThemeNames](xref:ActiproSoftware.Windows.Themes.ThemeNames) class.

## Automatic Light and Dark Theme Switching

Windows has a system setting where the user can indicate if they prefer light or dark themed applications.  Say that you've created and registered two theme definitions (light and dark) for your application and you wish for those themes to follow the Windows system setting.  This is easily done with the Actipro theme manager.

This sample code shows how two theme definitions could be registered (replace the ellipses with appropriate theme definition property settings) and the theme manager told to select a theme based on the Windows system preference setting for light or dark apps.  Note that no call to the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[CurrentTheme](xref:ActiproSoftware.Windows.Themes.ThemeManager.CurrentTheme) property is needed in this scenario.  The [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[RegisterAutomaticThemes](xref:ActiproSoftware.Windows.Themes.ThemeManager.RegisterAutomaticThemes*) call will switch the theme if necessary.

```csharp
ThemeManager.RegisterThemeDefinition(new ThemeDefinition("MyLightTheme") { ... });
ThemeManager.RegisterThemeDefinition(new ThemeDefinition("MyDarkTheme") { ... });
ThemeManager.RegisterAutomaticThemes("MyLightTheme", "MyDarkTheme", ThemeNames.HighContrast);
```

Behind the scenes, [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager) attaches a Win32 message hook on the `Application.MainWindow` to listen for messages from the system that indicate theme changes.  When one is detected the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[SystemApplicationMode](xref:ActiproSoftware.Windows.Themes.ThemeManager.SystemApplicationMode) property is updated and if automatic theme switching is enabled, the theme is changed.

Note that if you use a splash screen for your application, the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager) may initially attach its hook to that splash screen `Window` since it would be the value in `Application.MainWindow`.  WPF's `Application` class doesn't notify when `Application.MainWindow` changes.  If you later set a new main `Window` to `Application.MainWindow`, the hook can be updated to that `Window` by a call to the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[RegisterAutomaticThemes](xref:ActiproSoftware.Windows.Themes.ThemeManager.RegisterAutomaticThemes*) method, which ensures the hook is attached to the current `Application.MainWindow`.

## High-Contrast Support

The Actipro theme generator has complete support for high-contrast themes.  When the [ThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition).[Intent](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.Intent) property is set to `HighContrast`, only system colors will be used for generated brush resource assets.  This allows your application to be rendered properly for end users who require high-contrast.

> [!NOTE]
> A `HighContrast` theme intent should only ever be used when Windows itself is in high-contrast mode.

The [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[RegisterAutomaticThemes](xref:ActiproSoftware.Windows.Themes.ThemeManager.RegisterAutomaticThemes*) method described in the previous section has a third parameter that allows for a high-contrast theme name to be specified, which generally is `ThemeNames.HighContrast`.  When a theme name is passed in, the theme manager will watch for the system to change into high-contrast mode and will automatically activate/deactivate that theme as appropriate.

## Theme Definition Base Classes

There are several theme definition classes that provide their own set of default values.  It's best to use the base class that is closest to the look you wish to achieve with your theme, so fewer options need to be set.

- [ThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition) - Default themes with a modern appearance and some rounded corners.
- [MetroThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.MetroThemeDefinition) - Flat Metro themes that have an accented status bar and resemble apps like Visual Studio.
- [OfficeColorfulThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.OfficeColorfulThemeDefinition) - Office Colorful themes with an accented title bar.
- [OfficeWhiteThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.OfficeWhiteThemeDefinition) - Office White themes with an accented status bar.
- [HighContrastThemeDefinition](xref:ActiproSoftware.Windows.Themes.Generation.HighContrastThemeDefinition) - High-contrast themes that use system colors; only for use when Windows itself is in high-contrast mode.

## Theme Definition Important Options

The most important options of a theme definition are its [Name](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.Name) and [Intent](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.Intent).

The [Name](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.Name) property must be a string that uniquely identifies the theme definition.  Once the theme definition is registered with the theme manager, it can be used to generate a theme when its name is set to the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[CurrentTheme](xref:ActiproSoftware.Windows.Themes.ThemeManager.CurrentTheme) property.

The [Intent](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.Intent) property specifies a [ThemeIntent](xref:ActiproSoftware.Windows.Themes.Generation.ThemeIntent) enumeration value that indicates if the theme definition is intended to be a black, dark, light, or white theme.  This setting heavily affects the grayscale and color shades that are used in the theme.

## Theme Definition Color Palette Options

Each theme definition generates multiple shades of colors in various color families.  The [ColorPaletteKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ColorPaletteKind) property specifies a [ColorPaletteKind](xref:ActiproSoftware.Windows.Themes.Generation.ColorPaletteKind) enumeration value that indicates the default color palette to use for the generated color families.

- Vibrant - Bright colors.
- Office - Colors are similar to slightly more muted Office colors.

The properties for the base colors used to generate each color family can be altered from their color palette defaults.  This is useful when you wish to tweak the color family shades to match your brand colors.

The base colors for each color family is listed below:

- [BaseColorBlue](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorBlue) - Base color for the blue color family.
- [BaseColorGreen](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorGreen) - Base color for the green color family.
- [BaseColorIndigo](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorIndigo) - Base color for the indigo color family.
- [BaseColorOrange](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorOrange) - Base color for the orange color family.
- [BaseColorPink](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorPink) - Base color for the pink color family.
- [BaseColorPurple](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorPurple) - Base color for the purple color family.
- [BaseColorRed](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorRed) - Base color for the red color family.
- [BaseColorTeal](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorTeal) - Base color for the teal color family.
- [BaseColorYellow](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseColorYellow) - Base color for the yellow color family.

## Theme Definition Grayscale Palette Options

All grayscale (silver and gray shades) are tinted twoards a particular color hue.  The [BaseGrayscaleHue](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseGrayscaleHue) property determines this hue value in degrees from `0` to `359`.

The [BaseGrayscaleSaturation](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseGrayscaleSaturation) property determines the level at which the color hue is saturated into the grayscale shades.  A higher number means a higher saturation level.

The [GrayMin](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.GrayMin) and [SilverMax](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.SilverMax) properties indicate the minimum and maximum component values to use for the generated grayscale shades.  Raise the gray minimum to not have as dark of a theme.  The silver maximum generally remains `255` to ensure white can be used.

The [GraySilverRatio](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.GraySilverRatio) is a ratio percentage indicating how the gray shades and silver shades share the entire grayscale spectrum.  The default value is `0.5`, meaning they share the spectrum equally.

## Theme Definition Color Family Usage Options

Several options let you select the color family to use for various UI areas.  For instance, if you want the primary accent color for hover states, etc. in your application to be green, you'd alter the [PrimaryAccentColorFamilyName](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.PrimaryAccentColorFamilyName) property.

- [DockGuideColorFamilyName](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.DockGuideColorFamilyName) - Docking window dock guides.
- [PreviewTabColorFamilyName](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.PreviewTabColorFamilyName) - Tabbed MDI preview tabs.
- [PrimaryAccentColorFamilyName](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.PrimaryAccentColorFamilyName) - Primary accent.
- [ProgressColorFamilyName](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ProgressColorFamilyName) - Progress bar fill.
- [WindowColorFamilyName](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.WindowColorFamilyName) - Window title background and border.

## Theme Definition Font Options

The [DefaultFontFamily](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.DefaultFontFamily) property specifies the default font family to apply to all top-level elements like `Window`, `ContextMenu`, and `ToolTip` controls.  When left its default value of a null value, the system-defined result of the `SystemFonts.MessageFontFamily` property is used.  On English systems, this default value is generally "Segoe UI".  It is recommended to not specify the default font family unless you have a specific stylistic need for another font family.

The [BaseFontSize](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BaseFontSize) property indicates the base font size to use for most UI.  Window titles (when using [WindowChrome](windowchrome.md)) will use the relative font size specified in the [WindowTitleFontSizeKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.WindowTitleFontSizeKind) property.  Likewise, tool window titles will use the relative font specified in the [ToolWindowContainerTitleFontSizeKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ToolWindowContainerTitleFontSizeKind) property.

## Theme Definition Other Options

There are many other appearance options that can be configured as well.

### Arrow Kinds

The [ArrowKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ArrowKind) property specifies an [ArrowKind](xref:ActiproSoftware.Windows.Themes.Generation.ArrowKind) enumeration value that indicates whether chevron or filled triangle arrows should be used throughout the UI.

### Borders

The [RequireDarkerBorders](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.RequireDarkerBorders) property specifies whether dark-oriented themes should use darker-than-background borders instead of lighter-than-background borders.  When this property is `true` in a dark theme, it's best to use a higher [GrayMin](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.GrayMin), which will allow the darker borders to be more distinctive.  The [GraySilverRatio](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.GraySilverRatio) can optionally be slightly increased to reduce overall border contrast.

### Bullets

Several options like [BulletBorderWidth](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BulletBorderWidth), [BulletGlyphKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BulletGlyphKind), [BulletRelativeSize](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BulletRelativeSize), and [CheckBoxCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.CheckBoxCornerRadius) determine how `CheckBox` and `RadioButton` bullets render.

### Buttons and Bar Items

Several options like [BarItemBackgroundGradientKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BarItemBackgroundGradientKind), [BarItemBackgroundStateKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BarItemBackgroundStateKind), [BarItemBorderContrastKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.BarItemBorderContrastKind), [ButtonBackgroundGradientKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ButtonBackgroundGradientKind), [ButtonBorderContrastKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ButtonBorderContrastKind), [ButtonCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ButtonCornerRadius), [ButtonPadding](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ButtonPadding), and [ToolBarButtonCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ToolBarButtonCornerRadius) determine how buttons render.

### Containers

The [ContainerBorderContrastKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ContainerBorderContrastKind) property alters how container borders render.

### List Items

Several options like [ListItemBackgroundGradientKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ListItemBackgroundGradientKind), [ListItemBackgroundStateKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ListItemBackgroundStateKind), and [ListItemBorderContrastKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ListItemBorderContrastKind) determine how list items render.

### Menus

Several options like [MenuItemLargeIconColumnWidth](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.MenuItemLargeIconColumnWidth), [MenuItemIconColumnWidth](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.MenuItemIconColumnWidth), [MenuItemPopupColumnWidth](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.MenuItemPopupColumnWidth), [MenuItemPadding](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.MenuItemPadding), and [MenuPopupCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.MenuPopupCornerRadius) determine how menus render.

### Popups

Several options like [PopupBorderContrastKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.PopupBorderContrastKind), [PopupCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.PopupCornerRadius), and [PopupShadowDirection](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.PopupShadowDirection) determine how popups render.

### Scroll Bars

Several options like [ScrollBarHasButtons](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ScrollBarHasButtons), [ScrollBarThumbCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ScrollBarThumbCornerRadius), and [ScrollBarThumbMargin](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.ScrollBarThumbMargin) determine how scroll bars render.

### Status Bars

The [StatusBarBackgroundKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.StatusBarBackgroundKind) property alters how status bars render.

### Tabs

The [TabCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.TabCornerRadius) property alters how tabs render.

### Windows and Title Bars

Several options like [WindowTitleBarBackgroundKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.WindowTitleBarBackgroundKind), [TitleBarCornerRadius](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.TitleBarCornerRadius), and [WindowBorderKind](xref:ActiproSoftware.Windows.Themes.Generation.ThemeDefinition.WindowBorderKind) determine how windows and title bars render.

## Traditional WPF Themes (System-Based Switching)

There are many resources on the web describing how the non-Actipro WPF's themes system traditionally works but here is a brief overview.  You can optionally define a number of XAML resource dictionary files in a `Themes` folder off of your project root.  Special filenames include:

- Generic.xaml - The default resources that are always loaded but can be overridden by other ones below
- Classic.xaml - "Classic" Windows 9x/2000 look
- Luna.NormalColor.xaml - Windows XP default blue theme
- Luna.Homestead.xaml - Windows XP olive green theme
- Luna.Metallic.xaml - Windows XP silver theme
- Royale.NormalColor.xaml - Windows XP Media Center Edition theme
- Aero.NormalColor.xaml - Windows Vista/7 default theme
- Aero2.NormalColor.xaml - Windows 8.x/10 default theme
- AeroLite.NormalColor.xaml - Windows Server 2012 default theme

Basically if the user is running Windows XP with the olive green theme active, the `Luna.Homestead.xaml` file, if found, will be loaded so that anything in the application can use its resources (styles, brushes, etc.).  These resources are loaded like this on demand, when WPF encounters the first control or `ComponentResourceKey` lookup corresponding to the given assembly.  Also note that the `System.Windows.ThemeInfoAttribute` must be assigned to your assembly context indicating that themed-resources are in the assembly.  See the MSDN for documentation on this attribute.

> [!NOTE]
> With Windows 10-based systems being the primary operating system used at this point, much of the system-based theming described above is no obsolete.  Actipro controls now all simply implement a single `Generic.xaml` file for their styles/templates and theming is based on the Actipro theme definition used.

## Theme Catalogs

Theme catalogs can be used to indicate which resource dictionaries should be loaded for any given theme.  The theme catalogs contains zero or more resource dictionary references, which map a resource dictionary to one or more themes by name.  When the current theme is changed, the theme manager will then query each registered theme catalog to find the resource dictionaries that must be loaded for the given theme.

Each of these resource dictionaries can contain many assets, such as brushes, specific to a theme.  For instance, you may wish to override some brushes used in one of the predefined themes.  To do this, you would make a resource dictionary with the brush overrides, then register that resource dictionary with a theme catalog to load for the target theme name.

Resource dictionary references are represented by instances of [ThemedResourceDictionaryReference](xref:ActiproSoftware.Windows.Themes.ThemedResourceDictionaryReference), which include a `Uri` to the XAML file for the `ResourceDictionary` and a list of associated theme names.  Therefore, a single reference can indicate a single XAML file should be loaded for multiple themes.

### Registration

Theme catalogs can be manually registered using the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[RegisterThemeCatalog](xref:ActiproSoftware.Windows.Themes.ThemeManager.RegisterThemeCatalog*) method.  They can also be unregistered using the [ThemeManager](xref:ActiproSoftware.Windows.Themes.ThemeManager).[UnregisterThemeCatalog](xref:ActiproSoftware.Windows.Themes.ThemeManager.UnregisterThemeCatalog*) method.  A key can be used to uniquely identify a theme catalog during registration, so it can be more easily removed later.

Custom registrars, which derive from [ThemeCatalogRegistrarBase<T>](xref:ActiproSoftware.Windows.Themes.ThemeCatalogRegistrarBase`1), can be used to automatically register a system theme catalog with the theme manager when WPF loads the system themes for an assembly.

To accomplish this, a custom [ThemeCatalogRegistrarBase<T>](xref:ActiproSoftware.Windows.Themes.ThemeCatalogRegistrarBase`1) is created that passes the desired theme catalog type as the type parameter.  This registrar can then be included in the XAML files loaded by WPF and it will automatically register the theme catalog when loaded.

> [!NOTE]
> It is a best practice to register custom theme catalogs upfront in the application startup code.  The tradeoff is that registering a theme catalog at app startup will force that assembly to load immediately, and will incur the time cost of loading the assembly, but will prevent templates from being reapplied later during app usage when a themed control from a control library is first used.
