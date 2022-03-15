---
title: "Reusable Assets"
page-title: "Reusable Assets - Themes Reference"
order: 7
---
# Reusable Assets

Actipro Themes includes several reusable assets resources, such as brushes, for use in various parts of your application, to maintain a consistent look.

## What is an Asset?

Assets are primitive resources such as `Boolean`, `CornerRadius`, `Thickness`, as well as `SolidColorBrush`, `LinearGradientBrush`, and `RadialGradientBrush`.  Actipro Themes uses the same common asset (brush, thickness, etc.) pool for its native WPF control styles and custom control styles.  Thus no matter which native or Actipro controls you use together, the appearance will consistently look great.

The keys for the assets in this common pool are defined in the [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys) class, which includes close to 1,000 assets.

## Browsing Theme Assets

The Actipro [Theme Designer application](theme-designer.md) includes a "Resource Browser" tab that lets you view all of the asset resources defined in the theme.  The list can be filtered and resource keys can be copied to the clipboard for easy usage within your application.

## Reusing Theme Assets

Any of theme assets used in the Actipro and native control themes can be used in your application, or for any custom controls you may have.  Once you know which assets from [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys) that you'd like to use, it's just a matter of setting up an appropriate `DynamicResource`.

The first step in reusing assets in XAML is to declare the proper namespace:

```xaml
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
```

This sample code shows how to use various low contrast container brushes in a `Border` element with some text:

```xaml
<Border Background="{DynamicResource {x:Static themes:AssetResourceKeys.ContainerBackgroundLowBrushKey}}"
	BorderThickness="1" BorderBrush="{DynamicResource {x:Static themes:AssetResourceKeys.ContainerBorderLowBrushKey}}">
	<StackPanel Margin="10">
		<TextBlock Text="Reusable Theme Assets" Foreground="{DynamicResource {x:Static themes:AssetResourceKeys.ContainerForegroundLowNormalBrushKey}}" />
		<TextBlock Text="This is a description that renders more faintly." Foreground="{DynamicResource {x:Static themes:AssetResourceKeys.ContainerForegroundLowSubtleBrushKey}}" />
	</StackPanel>
</Border>
```

> [!NOTE]
> Use the Actipro [Theme Designer application](theme-designer.md)'s "Resource Browser" tab to quickly generate the `DynamicResource` references for any built-in asset.

## Customizing Theme Assets

The theme assets can easily be customized to fit your exact requirements.  Once you know which assets from [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys) that you'd like to customize, you simply need to define a resource of the associated type and set the key appropriately.

The first step in customizing assets in XAML is to declare the proper namespace:

```xaml
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
```

This sample code shows how to customize a low contrast container background brush:

```xaml
<Application.Resources>
    <SolidColorBrush x:Key="{x:Static themes:AssetResourceKeys.ContainerBackgroundLowBrushKey}" Color="#d0d0d0" />
</Application.Resources>
```

This will change the `ContainerBackgroundLowBrushKey` brush for you entire application.  Alternatively, the brush above can be defined in the resources of an WPF element to only affect that control hierarchy.

Theme catalogs know how to pull in resource dictionaries based on whichever theme is currently active.  If multiple themes can be used in your application, it is better to use theme catalogs to pull in appropriate resource dictionaries with the asset resource overrides, instead of hard-coding them in `Application.Resources`.  The [Architecture](architecture.md) topic covers theme catalogs in detail.

## Color-Related Assets

Generated themes contain `Brush` asset resources for the entire color palette.  Each of the numerous color families (blue, green, indigo, etc.) has a `Brush` asset resource for each shade in the color family, which can be visualized in the [Theme Designer application](theme-designer.md)'s Color Palette tab.

Note that the asset resource keys are named using relative contrast notation where "low" means lowest contrast with the theme intent (light/dark) and "high" means highest contrast.  As an example, in a light theme, a "high" contrast blue would be very dark.

Relative contrast names include `Lowest`, `Lower`, `Low`, `MidLow`, `Mid`, `MidHigh`, `High`, `Higher`, and `Highest`.

### Containers

Containers are meant to refer to a pane that contains other controls.  Themes generate a number of brush asset resources for each container contrast shade.

These are the generated container brush asset keys for the `Lowest` (closest to the theme intent) contrast level:

- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ContainerBackgroundLowestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBackgroundLowestBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ContainerBorderLowestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBorderLowestBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ContainerForegroundLowestNormalBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestNormalBrushKey) - Normal text.
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ContainerForegroundLowestMutedBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestMutedBrushKey) - For descriptive text.
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ContainerForegroundLowestSubtleBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestSubtleBrushKey) - For less important text.
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ContainerForegroundLowestDisabledBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestDisabledBrushKey) - Disabled text.

There are similarly-named asset resources for each relative contrast level.

### Primary Accent

These are the generated background brush asset keys for the selected primary accent color family in lowest-to-highest contrast order:

- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundLowestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundLowestBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundLowerBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundLowerBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundLowBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundMidLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundMidLowBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundMidBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundMidBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundMidHighBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundMidHighBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundHighBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundHighBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundHigherBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundHigherBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[PrimaryAccentBackgroundHighestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentBackgroundHighestBrushKey)

There are similarly-named asset resources for foregrounds.

### Color Families

Multiple color families are included in each theme.

These are the generated background brush asset keys for the green color family in lowest-to-highest contrast order:

- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundLowestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundLowestBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundLowerBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundLowerBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundLowBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundMidLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundMidLowBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundMidBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundMidBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundMidHighBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundMidHighBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundHighBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundHighBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundHigherBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundHigherBrushKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ColorPaletteGreenBackgroundHighestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ColorPaletteGreenBackgroundHighestBrushKey)

Similar asset resources are available for each color family.

## Font-Related Assets

Generated themes contain numerous `Double`-based font size assets based on the [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition).[BaseFontSize](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.BaseFontSize) property.  The font sizes are designed to properly scale up at a pleasing ratio, with assets for everything from small text to very large headings and everything in between.

It is recommended to use the font size asset resources in your application as opposed to specifying absolute font sizes.  This way if the theme's base font size is ever changed in the future, no additional work is needed.  Plus, it forces consistency in font sizes throughout your application.

These are the generated asset keys:

- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraSmallFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraSmallFontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[SmallFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SmallFontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[MediumFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.MediumFontSizeDoubleKey) - The base font size, which defaults to 12.
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[MediumLargeFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.MediumLargeFontSizeDoubleKey) - A half-step between the medium and large font sizes.
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[LargeFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.LargeFontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraLargeFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLargeFontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraLarge2FontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLarge2FontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraLarge3FontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLarge3FontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraLarge4FontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLarge4FontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraLarge5FontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLarge5FontSizeDoubleKey)
- [AssetResourceKeys](xref:@ActiproUIRoot.Themes.AssetResourceKeys).[ExtraLarge6FontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLarge6FontSizeDoubleKey)
