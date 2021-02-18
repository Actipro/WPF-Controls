---
title: "Value Converters"
page-title: "Value Converters - Shared Library Reference"
order: 14
---
# Value Converters

The [ActiproSoftware.Windows.Data](xref:ActiproSoftware.Windows.Data), [ActiproSoftware.Windows.Controls](xref:ActiproSoftware.Windows.Controls), [ActiproSoftware.Windows.Media](xref:ActiproSoftware.Windows.Media) namespaces contain numerous value converters that can be used in XAML for working with data.

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

[BooleanAndConverter](xref:ActiproSoftware.Windows.Data.BooleanAndConverter)

</td>
<td>

Represents a multi-value converter that performs a logical AND (&&) operation on one or more `Boolean` values passed to the associated `MultiBinding` object.

</td>
</tr>

<tr>
<td>

[BooleanNotConverter](xref:ActiproSoftware.Windows.Data.BooleanNotConverter)

</td>
<td>

Represents a value converter that performs a NOT (!) operation on a specified `Boolean` value.

</td>
</tr>

<tr>
<td>

[BooleanOrConverter](xref:ActiproSoftware.Windows.Data.BooleanOrConverter)

</td>
<td>

Represents a multi-value converter that performs a logical OR (||) operation on one or more `Boolean` values passed to the associated `MultiBinding` object.

</td>
</tr>

<tr>
<td>

[BrushOpacityConverter](xref:ActiproSoftware.Windows.Media.BrushOpacityConverter)

</td>
<td>

Represents a value converter that clones a specified `Brush` and updates the `Brush.Opacity` property based on the converter's parameter.

</td>
</tr>

<tr>
<td>

[CharacterCasingConverter](xref:ActiproSoftware.Windows.Data.CharacterCasingConverter)

</td>
<td>

Represents a value converter that can be used to change a string's character casing to uppercase or lowercase.  Set the [CharacterCasingConverter](xref:ActiproSoftware.Windows.Data.CharacterCasingConverter).[CharacterCasing](xref:ActiproSoftware.Windows.Data.CharacterCasingConverter.CharacterCasing) property to either `Upper` (the default) or `Lower` to specify the casing mode.

</td>
</tr>

<tr>
<td>

[CoalesceConverter](xref:ActiproSoftware.Windows.Data.CoalesceConverter)

</td>
<td>

Represents a value converter that returns the value if it is non-null; otherwise, the value of the converter's parameter.  This converter can also be used in a `MultiBinding`, in which case the first non-null value is returned; otherwise, the converter's parameter is returned.

</td>
</tr>

<tr>
<td>

[ColorInterpolationConverter](xref:ActiproSoftware.Windows.Media.ColorInterpolationConverter)

</td>
<td>

This converter can be used in XAML bindings to return the linear `Color` value that is the specified percentage between the value of two `Color` objects.

The optional percentage is indicated in the converter parameter.  If not specified, the default of `0.5` (50%) will be used.

Exactly two `Color` objects must be passed into the converter using a `MultiBinding`.

This XAML shows how to use the converter to find a color that is between the system control and window colors but 90% towards the window color:

```xaml
<MultiBinding Converter="{StaticResource ColorInterpolationConverter}" ConverterParameter="0.9">
	<Binding Source="{x:Static SystemColors.ControlColor}" />
	<Binding Source="{x:Static SystemColors.WindowColor}" />
</MultiBinding>
```

</td>
</tr>

<tr>
<td>

[ConditionalConverter](xref:ActiproSoftware.Windows.Data.ConditionalConverter)

</td>
<td>

Represents a value and multi-value converter that provides if-else functionality for `Binding` and `MultiBinding` objects.

This converter expects the following source values to be specified:

1. Condition - A boolean value indicating whether the true or false value should be returned.
1. TrueValue - An object that is returned when the Condition is `true`. This can be omitted if the FalseValue parameter is also omitted, in which case the value of the `TrueValue` property will be used.
1. FalseValue - An object that is returned when the Condition is `false`. This can be omitted, in which case the value of the `FalseValue` property will be used.

This sample code shows how to use the converter in a `MultiBinding` in XAML:

```xaml
<MultiBinding Converter="{StaticResource ConditionalConverter}">
	<Binding Path="IfConditionValue" />
	<Binding Path="TrueValue" />
	<Binding Path="FalseValue" />
</MultiBinding>
```

This sample code shows how to use the converter in a `Binding` in XAML:

```xaml
<shared:ConditionalConverter x:Key="ConditionalConverter" TrueValue="The Value is true" FalseValue="The Value is false" />
...
<TextBlock Text="{Binding Path=BooleanProperty, Converter={StaticResource ConditionalConverter}}" />
```

If the condition is not a boolean, other condition evaluation logic executes as follows.  Strings evaluate true if they are not null or empty.  Non-null objects also return true.

</td>
</tr>

<tr>
<td>

[CornerRadiusConverter](xref:ActiproSoftware.Windows.Data.CornerRadiusConverter)

</td>
<td>

Represents a value converter that converts between a `CornerRadius` and a number.

```xaml
... Value="{Binding Path=Number, Converter={StaticResource CornerRadiusConverter}}" ...
```

</td>
</tr>

<tr>
<td>

[DelegateConverter](xref:ActiproSoftware.Windows.Data.DelegateConverter)

</td>
<td>

Represents a value converter that uses delegates to perform the underlying conversion to/from the source.  Set the [ConvertCallback](xref:ActiproSoftware.Windows.Data.DelegateConverter.ConvertCallback) and [ConvertBackCallback](xref:ActiproSoftware.Windows.Data.DelegateConverter.ConvertBackCallback) properties to the delegates to use for conversion and back conversion respectively.

</td>
</tr>

<tr>
<td>

[DurationToMillisecondConverter](xref:ActiproSoftware.Windows.Data.DurationToMillisecondConverter)

</td>
<td>

Represents a value converter that converts between a `Duration` and a number of milliseconds.

</td>
</tr>

<tr>
<td>

[EnumDescriptionConverter](xref:ActiproSoftware.Windows.Data.EnumDescriptionConverter)

</td>
<td>

Represents a value converter that uses the `DescriptionAttribute` for the string representation of the enumeration values, when available.

</td>
</tr>

<tr>
<td>

[ImageConverter](xref:ActiproSoftware.Windows.Controls.ImageConverter)

</td>
<td>

Represents a value converter that returns a new [DynamicImage](windows-controls/dynamicimage.md) (inherits `Image`) control instance created using a specified URI or `BitmapSource`.

This converter expects the source value to be a `Uri`, a URI `String`, or a `BitmapSource` that can be used to create a new [DynamicImage](windows-controls/dynamicimage.md) instance. A prefix can be defined in [UriPrefix](xref:ActiproSoftware.Windows.Controls.ImageConverter.UriPrefix) which will be prepended to all source values of type `String` before the [DynamicImage](windows-controls/dynamicimage.md) is created.

The `Width` and `Height` properties can optionally be set to set the related properties on the [DynamicImage](windows-controls/dynamicimage.md) control that is created.

The [ImageProvider](xref:ActiproSoftware.Windows.Controls.ImageConverter.ImageProvider) property can optionally be set to a specific [ImageProvider](../themes/image-provider.md) to assign to the `ImageSource` created by the converter.  Leave the property its default value of `null` to use the static default [ImageProvider](../themes/image-provider.md) instance.

</td>
</tr>

<tr>
<td>

[InverseConverter](xref:ActiproSoftware.Windows.Data.InverseConverter)

</td>
<td>

Represents a value converter that inverts another value converter.  Set an instance of the `IValueConverter` to invert in the [Converter](xref:ActiproSoftware.Windows.Data.InverseConverter.Converter) property.

</td>
</tr>

<tr>
<td>

[IsEnumFlagSetConverter](xref:ActiproSoftware.Windows.Data.IsEnumFlagSetConverter)

</td>
<td>Represents a value converter that returns whether the specified enumeration value has the flag, specified by the converter's parameter, set.</td>
</tr>

<tr>
<td>

[IsNullOrEmptyConverter](xref:ActiproSoftware.Windows.Data.IsNullOrEmptyConverter)

</td>
<td>

Represents a value converter that returns whether the specified value is `null`, and if it is a string, also if it is `null` or empty.

</td>
</tr>

<tr>
<td>

[IsTypeConverter](xref:ActiproSoftware.Windows.Data.IsTypeConverter)

</td>
<td>

Represents a value converter that returns whether the specified object is the `Type` indicated in the converter's parameter.

</td>
</tr>

<tr>
<td>

[MultiplicationConverter](xref:ActiproSoftware.Windows.Data.MultiplicationConverter)

</td>
<td>Represents a single and multi-value converter that multiplies all the source values provided, and optionally the converter's parameter.</td>
</tr>

<tr>
<td>

[NoOpConverter](xref:ActiproSoftware.Windows.Data.NoOpConverter)

</td>
<td>

Represents a value converter that does not alter the value in any way (e.g. no operation).

This converter can be used to bypass an issue that arises from an optimization present in the WPF binding system.

</td>
</tr>

<tr>
<td>

[ParallaxConverter](xref:ActiproSoftware.Windows.Data.ParallaxConverter)

</td>
<td>

Represents a value converter that can be used to create a parallax background scrolling effect.

Use the converter in a binding on a background element's `RenderTransform`'s `TranslateTransform.X` or `Y` property, and bind to a related `ScrollViewer.HorizontalOffset` or `VerticalOffset` property.

</td>
</tr>

<tr>
<td>

[PercentageConverter](xref:ActiproSoftware.Windows.Data.PercentageConverter)

</td>
<td>

Represents a value converter that converts between a number and a percentage.  The percentage is simply the number multiplied by `100`.

</td>
</tr>

<tr>
<td>

[StringFormatConverter](xref:ActiproSoftware.Windows.Data.StringFormatConverter)

</td>
<td>

Represents a multi-value converter that provides `String.Format` functionality for both simple `Binding` and `MultiBinding` objects.

Pass the format string in the converter's parameter.  Since `{` characters are normally interpreted as markup extension starts, you can escape them by placing `{}` before the format string like this:

```xaml
<MultiBinding Converter="{StaticResource StringFormatConverter}" ConverterParameter="{}{0} of {1}">...
```

</td>
</tr>

<tr>
<td>

[ThicknessConverter](xref:ActiproSoftware.Windows.Data.ThicknessConverter)

</td>
<td>

Represents a value converter that converts between a `Thickness` and a number.

By default, the `Thickness` returned will have all four sides (`Thickness.Left`, `Thickness.Top`, `Thickness.Right`, and `Thickness.Bottom`) set to the specified number. An optional parameter of type [Sides](xref:ActiproSoftware.Windows.Controls.Sides) can be specified, which can be used to customize the sides that are set.

```xaml
<!-- Set all four sides -->
... Value="{Binding Path=Number, Converter={StaticResource ThicknessConverter}}" ...
								
<!-- Only set the Left and Right sides -->
... Value="{Binding Path=Number, Converter={StaticResource ThicknessConverter}, ConverterParameter='Left,Right'}" ...
```

</td>
</tr>

<tr>
<td>

[TypeNameConverter](xref:ActiproSoftware.Windows.Data.TypeNameConverter)

</td>
<td>

Represents a value converter that converts a value to a `String` of its short type name.

</td>
</tr>

<tr>
<td>

[UnitToDoubleConverter](xref:ActiproSoftware.Windows.Data.UnitToDoubleConverter)

</td>
<td>

Represents a value converter that converts between a [Unit](xref:ActiproSoftware.Windows.Unit) and a `Double`.

This converter needs additional information in order to convert a `Double` into a [Unit](xref:ActiproSoftware.Windows.Unit).  Specifically, it needs to the know whether the value is measured in pixels or as a percentage. To accommodate this need, the property [Type](xref:ActiproSoftware.Windows.Data.UnitToDoubleConverter.Type) is used to specify the type of measurement.

</td>
</tr>

<tr>
<td>

[UriConverter](xref:ActiproSoftware.Windows.Data.UriConverter)

</td>
<td>

Represents a value converter that returns a new `Uri` instance created using the specified URI string combined with an optional URI prefix.

This converter expects the source value to be a `String` which, when combined with the [UriPrefix](xref:ActiproSoftware.Windows.Data.UriConverter.UriPrefix), can be used to create a new `Uri` instance.

</td>
</tr>

<tr>
<td>

[ZoomLevelToTextFormattingModeConverter](xref:ActiproSoftware.Windows.Data.ZoomLevelToTextFormattingModeConverter)

</td>
<td>

Represents a value converter that alters the `TextFormattingMode` based on the specified zoom level.

`Display` mode is used when the zoom level is `1.0`. `Ideal` mode is used when the zoom level is increased.

</td>
</tr>

</tbody>
</table>
