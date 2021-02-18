---
title: "Media"
page-title: "Media - Shared Library Reference"
order: 9
---
# Media

The [ActiproSoftware.Windows.Media](xref:ActiproSoftware.Windows.Media) namespace has a [UIColor](xref:ActiproSoftware.Windows.Media.UIColor) structure that has a lot of helper methods for working with colors and a [VisualTreeHelperExtended](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended) class that adds some useful methods that aren't in the core `VisualTreeHelper` class.

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

[IVisualParent](xref:ActiproSoftware.Windows.Media.IVisualParent)

</td>
<td>

Provides the base requirements for an element that can add and remove visual child elements.

> [!NOTE]
> This interface is used in conjunction with [LogicalChildrenCollection<T>](xref:ActiproSoftware.Windows.LogicalChildrenCollection`1).

</td>
</tr>

<tr>
<td>

[LinearGradientType](xref:ActiproSoftware.Windows.Media.LinearGradientType)

</td>
<td>

Specifies the type of `LinearGradientBrush` to be created by [LinearGradientBrushExtension](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension).

</td>
</tr>

</tbody>
</table>

## BrushOpacityConverter Class

[BrushOpacityConverter](xref:ActiproSoftware.Windows.Media.BrushOpacityConverter) value converter can be used in XAML bindings to return a clone of a `Brush` with it's opacity set to a specified value.

See the [Value Converters](value-converters.md) topic for more information on this class.

## ColorInterpolationConverter Class

[ColorInterpolationConverter](xref:ActiproSoftware.Windows.Media.ColorInterpolationConverter) value converter can be used in XAML bindings to return the linear `Color` value that is the specified percentage between the value of two `Color` objects.

See the [Value Converters](value-converters.md) topic for more information on this class.

## IconFrameConverter Class

[IconFrameConverter](xref:ActiproSoftware.Windows.Media.IconFrameConverter) value converter can be used in binding statements to select an image frame based on its size from an icon (.ICO) file. Normally, the first frame from icon file is returned which may not be the desired size. Using this value converter, a different size frame can be selected regardless of whether it's the first frame.

By default, [IconFrameConverter](xref:ActiproSoftware.Windows.Media.IconFrameConverter) will return the first `32x32` image frame.  The desired size can be set using [DesiredHeight](xref:ActiproSoftware.Windows.Media.IconFrameConverter.DesiredHeight) and [DesiredWidth](xref:ActiproSoftware.Windows.Media.IconFrameConverter.DesiredWidth).  If no image of the desired size is found, then the passed value is returned.

## IconFrameSelector Class

[IconFrameSelector](xref:ActiproSoftware.Windows.Media.IconFrameSelector) markup extension can be used in XAML to select an image frame based on its size from an icon (.ICO) file. Normally, the first frame from icon file is returned which may not be the desired size. Using this markup extension, a different size frame can be selected regardless of whether it's the first frame.

By default, [IconFrameSelector](xref:ActiproSoftware.Windows.Media.IconFrameSelector) will return the first `32x32` image frame.  The desired size can be set using [DesiredHeight](xref:ActiproSoftware.Windows.Media.IconFrameSelector.DesiredHeight) and [DesiredWidth](xref:ActiproSoftware.Windows.Media.IconFrameSelector.DesiredWidth).  If no image of the desired size is found, then the default `ImageSource` is returned.

## LinearGradientBrushExtension Class

The [LinearGradientBrushExtension](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension) class implements a markup extension that returns a `LinearGradientBrush` based on the specified parameters.

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

[AdditionalStopCount](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.AdditionalStopCount) Property

</td>
<td>

Gets or sets the additional stops used when creating a `LinearGradientBrush`.

> [!NOTE]
> When using additional stops, the start and end color will be alternated at each stop. Therefore if 1 additional stop is used, then the `LinearGradientBrush` will have 3 stops with the colors: start color, end color, start color.

</td>
</tr>

<tr>
<td>

[Angle](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.Angle) Property

</td>
<td>

Gets or sets the angle used when creating a `LinearGradientBrush`. This value is only used when the [GradientType](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.GradientType) is set to [CustomAngle](xref:ActiproSoftware.Windows.Media.LinearGradientType.CustomAngle).

</td>
</tr>

<tr>
<td>

[EndColor](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.EndColor) Property

</td>
<td>Gets or sets the end color of the gradient.</td>
</tr>

<tr>
<td>

[GradientType](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.GradientType) Property

</td>
<td>Gets or sets the type of gradient.</td>
</tr>

<tr>
<td>

[StartColor](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.StartColor) Property

</td>
<td>Gets or sets the start color of the gradient.</td>
</tr>

</tbody>
</table>

These static members are found in the class:

| Member | Description |
|-----|-----|
| [CreateBrush](xref:ActiproSoftware.Windows.Media.LinearGradientBrushExtension.CreateBrush*) Method | Creates a `LinearGradientBrush` based on the specified parameters. |

## UIColor Structure

The [UIColor](xref:ActiproSoftware.Windows.Media.UIColor) structure provides an enhanced representation of a `Color` object that supports the RGB, HSB, and HLS color models, conversion between models, and numerous other color helper methods.  Static methods on the structure are used to create an instance of it.

These instance members are found in the structure:

| Member | Description |
|-----|-----|
| [A](xref:ActiproSoftware.Windows.Media.UIColor.A) Property | Gets or sets the alpha component value of this `UIColor` structure. |
| [B](xref:ActiproSoftware.Windows.Media.UIColor.B) Property | Gets or sets the RGB blue component value of this `UIColor` structure. |
| [ExceedsW3CBrightnessThreshold](xref:ActiproSoftware.Windows.Media.UIColor.ExceedsW3CBrightnessThreshold) Property | Gets whether the color exceeds the W3C threshold for brightness. |
| [G](xref:ActiproSoftware.Windows.Media.UIColor.G) Property | Gets or sets the RGB green component value of this `UIColor` structure. |
| [Grayscale](xref:ActiproSoftware.Windows.Media.UIColor.Grayscale*) Method | Converts the color to grayscale. |
| [HlsHue](xref:ActiproSoftware.Windows.Media.UIColor.HlsHue) Property | Gets or sets the HLS hue component value of this `UIColor` structure. |
| [HlsLightness](xref:ActiproSoftware.Windows.Media.UIColor.HlsLightness) Property | Gets or sets the HLS lightness component value of this `UIColor` structure. |
| [HlsSaturation](xref:ActiproSoftware.Windows.Media.UIColor.HlsSaturation) Property | Gets or sets the HLS saturation component value of this `UIColor` structure. |
| [HsbBrightness](xref:ActiproSoftware.Windows.Media.UIColor.HsbBrightness) Property | Gets or sets the HSB brightness component value of this `UIColor` structure. |
| [HsbHue](xref:ActiproSoftware.Windows.Media.UIColor.HsbHue) Property | Gets or sets the HSB hue component value of this `UIColor` structure. |
| [HsbSaturation](xref:ActiproSoftware.Windows.Media.UIColor.HsbSaturation) Property | Gets or sets the HSB saturation component value of this `UIColor` structure. |
| [IsGrayscale](xref:ActiproSoftware.Windows.Media.UIColor.IsGrayscale) Property | Gets whether the color is grayscale. |
| [Luminance](xref:ActiproSoftware.Windows.Media.UIColor.Luminance) Property | Gets the luminance of the color. |
| [R](xref:ActiproSoftware.Windows.Media.UIColor.R) Property | Gets or sets the RGB red component value of this `UIColor` structure. |
| [Tint](xref:ActiproSoftware.Windows.Media.UIColor.Tint*) Method | Tints this color towards the specified tint `Color`. |
| [ToColor](xref:ActiproSoftware.Windows.Media.UIColor.ToColor*) Method | Gets the `Color` value of this `UIColor` structure. |
| [ToWebColor](xref:ActiproSoftware.Windows.Media.UIColor.ToWebColor*) Method | Converts the color to a web color string (e.g. `#FF0000` for `Red`). |
| [W3CBrightness](xref:ActiproSoftware.Windows.Media.UIColor.W3CBrightness) Property | Gets the brightness of the color, based on the W3C formula for calculating brightness. |

These static members are found in the structure, many of which are used to create a [UIColor](xref:ActiproSoftware.Windows.Media.UIColor):

| Member | Description |
|-----|-----|
| [FromAhls](xref:ActiproSoftware.Windows.Media.UIColor.FromAhls*) Method | Creates a `UIColor` structure from an alpha value and the specified HLS color values (hue, lightness, and saturation). |
| [FromAhsb](xref:ActiproSoftware.Windows.Media.UIColor.FromAhsb*) Method | Creates a `UIColor` structure from an alpha value and the specified HSB color values (hue, saturation, and brightness). |
| [FromArgb](xref:ActiproSoftware.Windows.Media.UIColor.FromArgb*) Method | Creates a `UIColor` structure from an alpha value and the specified RGB color values (red, green, and blue). |
| [FromColor](xref:ActiproSoftware.Windows.Media.UIColor.FromColor*) Method | Creates a `UIColor` structure from the specified `Color`. |
| [FromColorComplement](xref:ActiproSoftware.Windows.Media.UIColor.FromColorComplement*) Method | Creates a `UIColor` structure from the complement of the specified `Color`. |
| [FromHls](xref:ActiproSoftware.Windows.Media.UIColor.FromHls*) Method | Creates a `UIColor` structure from the specified HLS color values (hue, lightness, and saturation). |
| [FromHsb](xref:ActiproSoftware.Windows.Media.UIColor.FromHsb*) Method | Creates a `UIColor` structure from the specified HSB color values (hue, saturation, and brightness). |
| [FromMix](xref:ActiproSoftware.Windows.Media.UIColor.FromMix*) Method | Creates a `UIColor` structure that is the specified percentage between the value of two `Color` objects. |
| [FromName](xref:ActiproSoftware.Windows.Media.UIColor.FromName*) Method | Creates a `UIColor` structure from the specified name of a pre-defined color. |
| [FromRgb](xref:ActiproSoftware.Windows.Media.UIColor.FromRgb*) Method | Creates a `UIColor` structure from the specified RGB color values (red, green, and blue). |
| [FromWebColor](xref:ActiproSoftware.Windows.Media.UIColor.FromWebColor*) Method | Creates a `UIColor` structure from the specified web color.  This method can process HTML color specifications (e.g. #FF00FF) and known color names. |
| [GetStandardCustomColors](xref:ActiproSoftware.Windows.Media.UIColor.GetStandardCustomColors*) Method | Returns a `Color` array containing all of the standard custom values. |
| [GetSystemColors](xref:ActiproSoftware.Windows.Media.UIColor.GetSystemColors*) Method | Returns a `Color` array containing all of the `SystemColors` values. |
| [GetTintedColor](xref:ActiproSoftware.Windows.Media.UIColor.GetTintedColor*) Method | Returns the custom tinted color for the specified base color. |
| [GetWebColors](xref:ActiproSoftware.Windows.Media.UIColor.GetWebColors*) Method | Returns an array containing all of the web color values. |

## VisualTreeHelperExtended Class

The [VisualTreeHelperExtended](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended) class has several static methods that are useful for performing common tasks with nodes in a visual tree.  This class provides some advanced functionality that is not found in the `VisualTreeHelper` class.

These static members are found in the class:

| Member | Description |
|-----|-----|
| [GetAllDescendants](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAllDescendants*) Method | Returns a list of `DependencyObject` values that includes all the descendant visual objects that are of the specified `Type`. |
| [GetAncestor](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAncestor*) Method | Returns a `DependencyObject` value that represents an ancestor of the visual object that is of the specified `Type`. |
| [GetAncestorPopup](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetAncestorPopup*) Method | Returns a `Popup` value that represents an ancestor of the visual object. |
| [GetChild](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetChild*) Method | Returns a `DependencyObject` value that represents the first child visual object that is of the specified `Type`. |
| [GetCurrentOrAncestor](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetCurrentOrAncestor*) Method | Returns a `DependencyObject` value that represents the visual object, or an ancestor of the visual object, that is of the specified `Type`. |
| [GetDescendant](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetDescendant*) Method | Includes several overloads that allow a descendent visual to be returned from a specified object using various criteria. |
| [GetFirstDescendant](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetFirstDescendant*) Method | Returns a `DependencyObject` value that represents the first descendant visual object that is of the specified `Type`. |
| [GetFirstFocusableDescendant](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetFirstFocusableDescendant*) Method | Returns a `UIElement` value that represents the first descendant visual object that is focusable. |
| [GetNextFocusable](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetNextFocusable*) Method | Returns a `UIElement` value that represents the next visual object that is focusable. |
| [GetPreviousFocusable](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetPreviousFocusable*) Method | Returns a `UIElement` value that represents the previous visual object that is focusable. |
| [GetRoot](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.GetRoot*) Method | Returns the root `DependencyObject` in the visual tree. |
| [IsMouseOver](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.IsMouseOver*) Method | Returns whether the bounds of the specified `DependencyObject` contains the mouse without using a call to `IsMouseOver`, which can provide inaccurate results in mouse capture scenarios. |
| [IsVisual](xref:ActiproSoftware.Windows.Media.VisualTreeHelperExtended.IsVisual*) Method | Returns whether the specified `DependencyObject` is a `Visual` or `Visual3D`. |
