---
title: "Image Provider"
page-title: "Image Provider - Themes Reference"
order: 10
---
# Image Provider

The [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) class has logic to manipulate images for various scenarios.  Features include:

- Chromatic adaptation (color shifting) for images, which allows images designed for light themes to be automatically adjusted for use in dark themes.
- Converting a monochrome vector image to render in the current foreground color.
- Dynamic loading of pre-defined high-DPI and/or theme-specific image variations for raster images.
- Conversion of images to grayscale.
- Conversion of images to monochrome, in a specified color.

While all the image adaptation logic is contained within the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) class and can be called programmatically, the [DynamicImage](../shared/windows-controls/dynamicimage.md) control is generally used within the user interface to access the image provider's functionality.

![Screenshot](../shared/images/dynamicimage.png)

*A single BitmapImage that is altered to show normal, disabled, monochrome, and monochrome disabled states in both light and dark themes*

## Usage Scenarios for Image Provider

Common scenarios for using an image provider (and generally [DynamicImage](../shared/windows-controls/dynamicimage.md) controls) are:

- Your application designed images for a light theme and you want them adapted to look good in a dark theme.
- You have monochrome vector icons and want to render them in the current foreground color instead of their designed color.
- You wish to support alternate scaled variations of raster images that automatically apply themselves when the application's DPI changes.
- You wish to support predesigned variations of raster images designed for specific themes, that are automatically applied when those themes become active.
- You want images adapted to grayscale.
- You want images adapted to monochrome.

## DynamicImage and ImageProvider

The [DynamicImage](../shared/windows-controls/dynamicimage.md) control calls into the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) method to adapt the `ImageSource` set to its `Source` property.

It watches for theme and DPI changes and updates the image source adaptation appropriately.

Note that the native `Image` control from which [DynamicImage](../shared/windows-controls/dynamicimage.md) inherits does not include any functionality to interact with [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).

## Assigning a Non-Default Image Provider to an ImageSource

An [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance is created and assigned to the static [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) property by default.  This instance does not have any out-of-the-box configuration for supporting chromatic adaptation, scales, or themes.  However its properties may be adjusted to support those features.

When the [DynamicImage](../shared/windows-controls/dynamicimage.md) control needs to adapt an image, it examines the `ImageSource` set to its `Source` property to see if a value is returned for the `ImageProvider.Provider` attached property.  If no specific [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance was set via that attached property on the `ImageSource`, then the static instance returned via [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) will be used instead for adaptation.

The following example shows how a non-default [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance available via a static `ImageProviders.ChromaticAdaptation` class in your application could be applied to a `DrawingImage`.  The `DrawingImage` will then use that non-default image provider for its adaptation logic instead of the one available via [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default).

```xaml
<shared:DynamicImage Width="32" Height="32">
	<shared:DynamicImage.Source>
		<DrawingImage Drawing="{StaticResource CheckVectorDrawing}" shared:ImageProvider.Provider="{x:Static local:ImageProviders.ChromaticAdaptation}" />
	</shared:DynamicImage.Source>
</shared:DynamicImage>
```

To sum up, if you want all the [DynamicImage](../shared/windows-controls/dynamicimage.md) control instances in your application to use the same image provider configuration, simply set that configuration on the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) instance.  If instead you want some images to have an alternate configuration, such as when you only made high-DPI variations for certain raster images, create another [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance and set that on each applicable `ImageSource` via the `ImageProvider.Provider` attached property.

## Chromatic Adaptation for Dark Themes

Chromatic adaptation is the process of converting colors in an image so that they render well on a specific background color.  This is especially useful when your application has images designed for a light theme, and you wish for your application to support a dark theme.  Image provider can automatically convert them for you so that you don't have to design dark theme variations.

The following example shows the configuration for an [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support adaptation of all images when in a dark theme (i.e. `Dark`, `Black`, etc.).  The source images should be designed for a light theme and use a minimal number of colors (i.e. avoid gradients).

```csharp
ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes;
```

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" />
```

## Chromatic Adaptation for All Themes

Chromatic adaptation can also be applied in all themes.

The following example shows the configuration for an [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support adaptation of all images, regardless of theme.  The source images should be designed for a light theme and use a minimal number of colors (i.e. avoid gradients).

```csharp
ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.Always;
```

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" />
```

While the `ImageChromaticAdaptationMode.Always` mode is supported, it's generally enough to use the `DarkThemes` mode instead. `DarkThemes` mode doesn't perform any adaptation processing when in light themes.

## Predefined Monochrome Vector Image Adaptation

Vector images designed in a single color (monochrome) can be adapted to render using the [DynamicImage](../shared/windows-controls/dynamicimage.md) control's current foreground color.  This feature is extremely useful when vector images need to be used on a variety of controls, each with different foregrounds and backgrounds.

The following example shows the configuration for an [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support adaptation of vector images designed with a single color (in this case black) so that they dynamically update to match the current foreground color instead of always being black.

```csharp
ImageProvider.Default.DesignForegroundColor = Colors.Black;
```

```xaml
<shared:DynamicImage Width="32" Height="32" Source="{StaticResource MonochromeCheckVectorDrawing}" />
```

## Alternate Raster Image Scale Variations

Raster images render blurry when used in high-DPI. [DynamicImage](../shared/windows-controls/dynamicimage.md) can automatically watch for high-DPI scenarios and swap in an image variation designed for high-DPI.

The image provider knows to support specific scales when scale values higher than `1.0` are added to its [Scales](xref:@ActiproUIRoot.Media.ImageProvider.Scales) collection.  Note that if you do add a scale to the collection, you must ensure that each image design has a `"[filename].Scale-[scalefactor].[fileextension]"` image available in the same folder as the source image.

The following example shows the configuration for an [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support 200% scale images.  If the source image is normally 32x32 size in 100% DPI, then the scaled up image variation needs to be 64x64.

```csharp
ImageProvider.Default.Scales.Add(2.0);
```

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" />
```

The above configuration will have [DynamicImage](../shared/windows-controls/dynamicimage.md) watch for high-DPI scenarios and will have it adapt to swap in an image located at `"/Images/Save32.Scale-200.png"` when entering DPI greater than 100%.

No `".Scale-100"` should be in the filenames of images designed for 100% DPI.

> [!NOTE]
> When using scale variations, it's important to set the `Width` and `Height` properties on the [DynamicImage](../shared/windows-controls/dynamicimage.md) control to ensure that a swapped-in high-DPI image will not resize the control itself from its intended device independent unit size.  For instance, an image designed for 32x32 size should have the `Width` and `Height` both set to `32`, even if a 64x64 200% DPI image variation is swapped in.

## Alternate Raster Image Theme Variations

Raster images might have other specific designs when in certain themes. [DynamicImage](../shared/windows-controls/dynamicimage.md) can automatically watch for theme changes and swap in an image variation designed for that theme.  This feature should not be used if you are making use of chromatic adaptation, since these theme variations are already designed for a specific theme and no adaptation is necessary.

The image provider knows to support specific themes when theme names are added to its [ThemeNames](xref:@ActiproUIRoot.Media.ImageProvider.ThemeNames) collection.  Note that if you do add a theme name to the collection, you must ensure that each image design has a `"[filename].Theme-[themename].[fileextension]"` image available in the same folder as the source image.

The following example shows the configuration for an [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support "Dark" theme images.

```csharp
ImageProvider.Default.ThemeNames.Add("Dark");
```

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" />
```

The above configuration will have [DynamicImage](../shared/windows-controls/dynamicimage.md) watch for theme changes and will have it adapt to swap in an image located at `"/Images/Save32.Theme-Dark.png"` when theme named "Dark" is activated.

Theme name and scale variations can be combined as well.  In cases where both variations are supported by an image provider and need to load, the image file `"[filename].Theme-[themename].Scale-[scalefactor].[fileextension]"` will be loaded.

No `".Theme-[defaultthemename]"` should be in the filenames of images designed for your application's default theme.

## Grayscale Adaptation

Images are often converted to grayscale when their containing controls are disabled.

The following example shows a [DynamicImage](../shared/windows-controls/dynamicimage.md) containing an image that will render in semi-transparent grayscale when disabled.

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" IsEnabled="False" DisabledOpacity="0.4" />
```

## Monochrome Adaptation

Images are often converted to monochrome when displayed on intense backgrounds, or when going for a flat appearance.

The following example shows a [DynamicImage](../shared/windows-controls/dynamicimage.md) containing a raster image that will render in monochrome.  The monochrome color will match the color derived from the inherited foreground property.

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" UseMonochrome="True" />
```

The following example shows a [DynamicImage](../shared/windows-controls/dynamicimage.md) containing a raster image that will render in monochrome.  The monochrome color will be red.

```xaml
<shared:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" UseMonochrome="True" Foreground="Red" />
```

### High-Contrast Themes

Images can be automatically converted to monochrome when in high-contrast themes if the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[UseMonochromeInHighContrast](xref:@ActiproUIRoot.Media.ImageProvider.UseMonochromeInHighContrast) property is set to `true`.

## Preventing Portions of an Image from Being Adapted

Sometimes a `DrawingImage` may include portions within it that should not be adapted.

A common scenario of this case is a fill color image, where the image shows a paint bucket and a swatch rectangle underneath it that contains the current foreground color.  For this scenario, we only want the root portion of the image (the paint bucket) to be adapted and we do not want the swatch rectangle to be touched by adaptation logic in any way.

To prevent adaptation on an `ImageSource`, or a `Drawing` within a `DrawingImage`, set the attached `ImageProvider.CanAdapt` property to `false` on that object.  It will be skipped during the image adaptation process.

The following example shows how the attached `ImageProvider.CanAdapt` property can be used to prevent an entire image from being adapted.

```xaml
<shared:DynamicImage Width="32" Height="32">
	<shared:DynamicImage.Source>
		<BitmapImage UriSource="/Images/Save32.png" shared:ImageProvider.CanAdapt="False" />
	</shared:DynamicImage.Source>
</shared:DynamicImage>

```

## Adjusting the Scheme for Locating Scale and Theme Raster Image Variations

The [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[TransformBaseImageUriSource](xref:@ActiproUIRoot.Media.ImageProvider.TransformBaseImageUriSource*) method is where the original raster image source's `Uri` is passed in and possibly adjusted based on the [ImageProviderRequest](xref:@ActiproUIRoot.Media.ImageProviderRequest) settings to return an alternate variation of the image based on the requested scale or theme.  This method can be overridden if custom logic is needed to transform the image source's `Uri`.

The default implementation of that method calls into the [GetScalePathPath](xref:@ActiproUIRoot.Media.ImageProvider.GetScalePathPath*) and [GetThemeNamePathPart](xref:@ActiproUIRoot.Media.ImageProvider.GetThemeNamePathPart*) to see if either returns a non-empty string value.  The [GetScalePathPath](xref:@ActiproUIRoot.Media.ImageProvider.GetScalePathPath*) method will return `"Scale-200"` if the image provider supports 200% scale (via [Scales](xref:@ActiproUIRoot.Media.ImageProvider.Scales)) and the [ImageProviderRequest](xref:@ActiproUIRoot.Media.ImageProviderRequest).[Scale](xref:@ActiproUIRoot.Media.ImageProviderRequest.Scale) is `2.0`.  The [GetThemeNamePathPart](xref:@ActiproUIRoot.Media.ImageProvider.GetThemeNamePathPart*) method will return `"Theme-Dark"` if the image provider supports the "Dark" theme (via [ThemeNames](xref:@ActiproUIRoot.Media.ImageProvider.ThemeNames)) and the [ImageProviderRequest](xref:@ActiproUIRoot.Media.ImageProviderRequest).[ThemeName](xref:@ActiproUIRoot.Media.ImageProviderRequest.ThemeName) is `"Dark"`.  Either of those methods may be overridden if another naming scheme is desired.

As an example, if the image provider indicates that it supports a specific raster image for 200% scale and "Dark" theme, passing in `"Save32.png"` to [TransformBaseImageUriSource](xref:@ActiproUIRoot.Media.ImageProvider.TransformBaseImageUriSource*) will result in a `"Save32.Theme-Dark.Scale-200.png"` result.  This alternate `Uri` will be loaded and used as the base for any other adaptations (chromatic, grayscale, monochrome) that need to take place.
