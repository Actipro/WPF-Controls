---
title: "Media"
page-title: "Media - Shared Library Reference"
order: 9
---
# Media

The [ActiproSoftware.Windows.Media](xref:@ActiproUIRoot.Media) namespace has a [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure that has a lot of helper methods for working with colors and a [VisualTreeHelperExtended](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended) class that adds some useful methods that aren't in the core `VisualTreeHelper` class.

## General

These general types are found in the namespace:

<table>
<thead>

<tr>
<th>Type</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[IVisualParent](xref:@ActiproUIRoot.Media.IVisualParent)

</td>
<td>

Provides the base requirements for an element that can add and remove visual child elements.

> [!NOTE]
> This interface is used in conjunction with [LogicalChildrenCollection<T>](xref:@ActiproUIRoot.LogicalChildrenCollection`1).

</td>
</tr>

<tr>
<td>

[LinearGradientType](xref:@ActiproUIRoot.Media.LinearGradientType)

</td>
<td>

Specifies the type of `LinearGradientBrush` to be created by [LinearGradientBrushExtension](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension).

</td>
</tr>

</tbody>
</table>

## BrushOpacityConverter Class

[BrushOpacityConverter](xref:@ActiproUIRoot.Media.BrushOpacityConverter) value converter can be used in XAML bindings to return a clone of a `Brush` with it's opacity set to a specified value.

See the [Value Converters](value-converters.md) topic for more information on this class.

## ColorInterpolationConverter Class

[ColorInterpolationConverter](xref:@ActiproUIRoot.Media.ColorInterpolationConverter) value converter can be used in XAML bindings to return the linear `Color` value that is the specified percentage between the value of two `Color` objects.

See the [Value Converters](value-converters.md) topic for more information on this class.

## IconFrameConverter Class

[IconFrameConverter](xref:@ActiproUIRoot.Media.IconFrameConverter) value converter can be used in binding statements to select an image frame based on its size from an icon (.ICO) file. Normally, the first frame from icon file is returned which may not be the desired size. Using this value converter, a different size frame can be selected regardless of whether it's the first frame.

By default, [IconFrameConverter](xref:@ActiproUIRoot.Media.IconFrameConverter) will return the first `32x32` image frame.  The desired size can be set using [DesiredHeight](xref:@ActiproUIRoot.Media.IconFrameConverter.DesiredHeight) and [DesiredWidth](xref:@ActiproUIRoot.Media.IconFrameConverter.DesiredWidth).  If no image of the desired size is found, then the passed value is returned.

## IconFrameSelector Class

[IconFrameSelector](xref:@ActiproUIRoot.Media.IconFrameSelector) markup extension can be used in XAML to select an image frame based on its size from an icon (.ICO) file. Normally, the first frame from icon file is returned which may not be the desired size. Using this markup extension, a different size frame can be selected regardless of whether it's the first frame.

By default, [IconFrameSelector](xref:@ActiproUIRoot.Media.IconFrameSelector) will return the first `32x32` image frame.  The desired size can be set using [DesiredHeight](xref:@ActiproUIRoot.Media.IconFrameSelector.DesiredHeight) and [DesiredWidth](xref:@ActiproUIRoot.Media.IconFrameSelector.DesiredWidth).  If no image of the desired size is found, then the default `ImageSource` is returned.

## LinearGradientBrushExtension Class

The [LinearGradientBrushExtension](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension) class implements a markup extension that returns a `LinearGradientBrush` based on the specified parameters.

These instance members are found in the class:

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[AdditionalStopCount](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.AdditionalStopCount) Property

</td>
<td>

Gets or sets the additional stops used when creating a `LinearGradientBrush`.

> [!NOTE]
> When using additional stops, the start and end color will be alternated at each stop. Therefore if 1 additional stop is used, then the `LinearGradientBrush` will have 3 stops with the colors: start color, end color, start color.

</td>
</tr>

<tr>
<td>

[Angle](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.Angle) Property

</td>
<td>

Gets or sets the angle used when creating a `LinearGradientBrush`. This value is only used when the [GradientType](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.GradientType) is set to [CustomAngle](xref:@ActiproUIRoot.Media.LinearGradientType.CustomAngle).

</td>
</tr>

<tr>
<td>

[EndColor](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.EndColor) Property

</td>
<td>Gets or sets the end color of the gradient.</td>
</tr>

<tr>
<td>

[GradientType](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.GradientType) Property

</td>
<td>Gets or sets the type of gradient.</td>
</tr>

<tr>
<td>

[StartColor](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.StartColor) Property

</td>
<td>Gets or sets the start color of the gradient.</td>
</tr>

</tbody>
</table>

These static members are found in the class:

| Member | Description |
|-----|-----|
| [CreateBrush](xref:@ActiproUIRoot.Media.LinearGradientBrushExtension.CreateBrush*) Method | Creates a `LinearGradientBrush` based on the specified parameters. |

## UIColor Structure

The [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure provides an enhanced representation of a `Color` object that supports the RGB, HSB, and HLS color models, conversion between models, and numerous other color helper methods.  Static methods on the structure are used to create an instance of it.

These instance members are found in the structure:

| Member | Description |
|-----|-----|
| [A](xref:@ActiproUIRoot.Media.UIColor.A) Property | Gets or sets the alpha component value of this `UIColor` structure. |
| [B](xref:@ActiproUIRoot.Media.UIColor.B) Property | Gets or sets the RGB blue component value of this `UIColor` structure. |
| [ExceedsW3CBrightnessThreshold](xref:@ActiproUIRoot.Media.UIColor.ExceedsW3CBrightnessThreshold) Property | Gets whether the color exceeds the W3C threshold for brightness. |
| [G](xref:@ActiproUIRoot.Media.UIColor.G) Property | Gets or sets the RGB green component value of this `UIColor` structure. |
| [Grayscale](xref:@ActiproUIRoot.Media.UIColor.Grayscale*) Method | Converts the color to grayscale. |
| [HlsHue](xref:@ActiproUIRoot.Media.UIColor.HlsHue) Property | Gets or sets the HLS hue component value of this `UIColor` structure. |
| [HlsLightness](xref:@ActiproUIRoot.Media.UIColor.HlsLightness) Property | Gets or sets the HLS lightness component value of this `UIColor` structure. |
| [HlsSaturation](xref:@ActiproUIRoot.Media.UIColor.HlsSaturation) Property | Gets or sets the HLS saturation component value of this `UIColor` structure. |
| [HsbBrightness](xref:@ActiproUIRoot.Media.UIColor.HsbBrightness) Property | Gets or sets the HSB brightness component value of this `UIColor` structure. |
| [HsbHue](xref:@ActiproUIRoot.Media.UIColor.HsbHue) Property | Gets or sets the HSB hue component value of this `UIColor` structure. |
| [HsbSaturation](xref:@ActiproUIRoot.Media.UIColor.HsbSaturation) Property | Gets or sets the HSB saturation component value of this `UIColor` structure. |
| [IsGrayscale](xref:@ActiproUIRoot.Media.UIColor.IsGrayscale) Property | Gets whether the color is grayscale. |
| [Luminance](xref:@ActiproUIRoot.Media.UIColor.Luminance) Property | Gets the luminance of the color. |
| [R](xref:@ActiproUIRoot.Media.UIColor.R) Property | Gets or sets the RGB red component value of this `UIColor` structure. |
| [Tint](xref:@ActiproUIRoot.Media.UIColor.Tint*) Method | Tints this color towards the specified tint `Color`. |
| [ToColor](xref:@ActiproUIRoot.Media.UIColor.ToColor*) Method | Gets the `Color` value of this `UIColor` structure. |
| [ToWebColor](xref:@ActiproUIRoot.Media.UIColor.ToWebColor*) Method | Converts the color to a web color string (e.g., `#FF0000` for `Red`). |
| [W3CBrightness](xref:@ActiproUIRoot.Media.UIColor.W3CBrightness) Property | Gets the brightness of the color, based on the W3C formula for calculating brightness. |

These static members are found in the structure, many of which are used to create a [UIColor](xref:@ActiproUIRoot.Media.UIColor):

| Member | Description |
|-----|-----|
| [FromAhls](xref:@ActiproUIRoot.Media.UIColor.FromAhls*) Method | Creates a `UIColor` structure from an alpha value and the specified HLS color values (hue, lightness, and saturation). |
| [FromAhsb](xref:@ActiproUIRoot.Media.UIColor.FromAhsb*) Method | Creates a `UIColor` structure from an alpha value and the specified HSB color values (hue, saturation, and brightness). |
| [FromArgb](xref:@ActiproUIRoot.Media.UIColor.FromArgb*) Method | Creates a `UIColor` structure from an alpha value and the specified RGB color values (red, green, and blue). |
| [FromColor](xref:@ActiproUIRoot.Media.UIColor.FromColor*) Method | Creates a `UIColor` structure from the specified `Color`. |
| [FromColorComplement](xref:@ActiproUIRoot.Media.UIColor.FromColorComplement*) Method | Creates a `UIColor` structure from the complement of the specified `Color`. |
| [FromHls](xref:@ActiproUIRoot.Media.UIColor.FromHls*) Method | Creates a `UIColor` structure from the specified HLS color values (hue, lightness, and saturation). |
| [FromHsb](xref:@ActiproUIRoot.Media.UIColor.FromHsb*) Method | Creates a `UIColor` structure from the specified HSB color values (hue, saturation, and brightness). |
| [FromMix](xref:@ActiproUIRoot.Media.UIColor.FromMix*) Method | Creates a `UIColor` structure that is the specified percentage between the value of two `Color` objects. |
| [FromName](xref:@ActiproUIRoot.Media.UIColor.FromName*) Method | Creates a `UIColor` structure from the specified name of a pre-defined color. |
| [FromRgb](xref:@ActiproUIRoot.Media.UIColor.FromRgb*) Method | Creates a `UIColor` structure from the specified RGB color values (red, green, and blue). |
| [FromWebColor](xref:@ActiproUIRoot.Media.UIColor.FromWebColor*) Method | Creates a `UIColor` structure from the specified web color.  This method can process HTML color specifications (e.g., #FF00FF) and known color names. |
| [GetStandardCustomColors](xref:@ActiproUIRoot.Media.UIColor.GetStandardCustomColors*) Method | Returns a `Color` array containing all of the standard custom values. |
| [GetSystemColors](xref:@ActiproUIRoot.Media.UIColor.GetSystemColors*) Method | Returns a `Color` array containing all of the `SystemColors` values. |
| [GetTintedColor](xref:@ActiproUIRoot.Media.UIColor.GetTintedColor*) Method | Returns the custom tinted color for the specified base color. |
| [GetWebColors](xref:@ActiproUIRoot.Media.UIColor.GetWebColors*) Method | Returns an array containing all of the web color values. |

## VisualTreeHelperExtended Class

The [VisualTreeHelperExtended](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended) class has several static methods that are useful for performing common tasks with nodes in a visual tree.  This class provides some advanced functionality that is not found in the `VisualTreeHelper` class.

These static members are found in the class:

| Member | Description |
|-----|-----|
| [GetAllDescendants](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetAllDescendants*) Method | Returns a list of `DependencyObject` values that includes all the descendant visual objects that are of the specified `Type`. |
| [GetAncestor](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetAncestor*) Method | Returns a `DependencyObject` value that represents an ancestor of the visual object that is of the specified `Type`. |
| [GetAncestorPopup](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetAncestorPopup*) Method | Returns a `Popup` value that represents an ancestor of the visual object. |
| [GetChild](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetChild*) Method | Returns a `DependencyObject` value that represents the first child visual object that is of the specified `Type`. |
| [GetCurrentOrAncestor](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetCurrentOrAncestor*) Method | Returns a `DependencyObject` value that represents the visual object, or an ancestor of the visual object, that is of the specified `Type`. |
| [GetDescendant](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetDescendant*) Method | Includes several overloads that allow a descendent visual to be returned from a specified object using various criteria. |
| [GetFirstDescendant](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetFirstDescendant*) Method | Returns a `DependencyObject` value that represents the first descendant visual object that is of the specified `Type`. |
| [GetFirstFocusableDescendant](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetFirstFocusableDescendant*) Method | Returns a `UIElement` value that represents the first descendant visual object that is focusable. |
| [GetNextFocusable](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetNextFocusable*) Method | Returns a `UIElement` value that represents the next visual object that is focusable. |
| [GetPreviousFocusable](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetPreviousFocusable*) Method | Returns a `UIElement` value that represents the previous visual object that is focusable. |
| [GetRoot](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.GetRoot*) Method | Returns the root `DependencyObject` in the visual tree. |
| [IsMouseOver](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.IsMouseOver*) Method | Returns whether the bounds of the specified `DependencyObject` contains the mouse without using a call to `IsMouseOver`, which can provide inaccurate results in mouse capture scenarios. |
| [IsVisual](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended.IsVisual*) Method | Returns whether the specified `DependencyObject` is a `Visual` or `Visual3D`. |
