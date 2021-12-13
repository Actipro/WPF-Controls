---
title: "Value Converters"
page-title: "Value Converters - Shared Library Reference"
order: 14
---
# Value Converters

The [ActiproSoftware.Windows.Data](xref:ActiproSoftware.Windows.Data), [ActiproSoftware.Windows.Controls](xref:ActiproSoftware.Windows.Controls), [ActiproSoftware.Windows.Media](xref:ActiproSoftware.Windows.Media) namespaces contain numerous value converters that can be used in XAML for working with data.

## BooleanAndConverter Class

The [BooleanAndConverter](xref:ActiproSoftware.Windows.Data.BooleanAndConverter) represents a multi-value converter that performs a logical AND (&&) operation on one or more `Boolean` values passed to the associated `MultiBinding` object.

## BooleanNotConverter Class

The [BooleanNotConverter](xref:ActiproSoftware.Windows.Data.BooleanNotConverter) represents a value converter that performs a NOT (!) operation on a specified `Boolean` value.

## BooleanOrConverter Class

The [BooleanOrConverter](xref:ActiproSoftware.Windows.Data.BooleanOrConverter) represents a multi-value converter that performs a logical OR (||) operation on one or more `Boolean` values passed to the associated `MultiBinding` object.

## BorderChildClipConverter Class

The [BorderChildClipConverter](xref:ActiproSoftware.Windows.Media.BorderChildClipConverter) multi-value converter can be applied to a `Border.Child` element's `Clip` property to ensure the child element is properly clipped based on the `Border.CornerRadius` value.

This is useful when the `Border.Child` element or another element within its hierarchy has a background that renders into the portion of the parent `Border` that is normally rounded off by the corner radius.

Four values must be passed into the converter using a `MultiBinding`.  The first two are the width and height of the child element.  The third is the `BorderThickness` of the parent `Border`.  The fourth is the `CornerRadius` of the parent `Border`.

This XAML shows an example of using the converter:

```xaml
<Border x:Name="outerBorder" BorderBrush="Black" BorderThickness="1" CornerRadius="8">
    <Grid>
        <Grid.Clip>
            <MultiBinding Converter="{StaticResource BorderClipConverter}">
                <Binding RelativeSource="{RelativeSource Self}" Path="ActualWidth" />
                <Binding RelativeSource="{RelativeSource Self}" Path="ActualHeight" />
                <Binding ElementName="outerBorder" Path="BorderThickness" />
                <Binding ElementName="outerBorder" Path="CornerRadius" />
            </MultiBinding>
        </Grid.Clip>
        <!-- Grid child controls here -->
    </Grid>
</Border>
```

## BrushOpacityConverter Class

The [BrushOpacityConverter](xref:ActiproSoftware.Windows.Media.BrushOpacityConverter) represents a value converter that clones a specified `Brush` and updates the `Brush.Opacity` property based on the converter's parameter.

## CharacterCasingConverter Class

The [CharacterCasingConverter](xref:ActiproSoftware.Windows.Data.CharacterCasingConverter) represents a value converter that can be used to change a string's character casing to uppercase or lowercase.  Set the [CharacterCasingConverter](xref:ActiproSoftware.Windows.Data.CharacterCasingConverter).[CharacterCasing](xref:ActiproSoftware.Windows.Data.CharacterCasingConverter.CharacterCasing) property to either `Upper` (the default) or `Lower` to specify the casing mode.

## CoalesceConverter Class

The [CoalesceConverter](xref:ActiproSoftware.Windows.Data.CoalesceConverter) represents a value converter that returns the value if it is non-null; otherwise, the value of the converter's parameter.  This converter can also be used in a `MultiBinding`, in which case the first non-null value is returned; otherwise, the converter's parameter is returned.

## ColorInterpolationConverter Class

The [ColorInterpolationConverter](xref:ActiproSoftware.Windows.Media.ColorInterpolationConverter) can be used in XAML bindings to return the linear `Color` value that is the specified percentage between the value of two `Color` objects.

The optional percentage is indicated in the converter parameter.  If not specified, the default of `0.5` (50%) will be used.

Exactly two `Color` objects must be passed into the converter using a `MultiBinding`.

This XAML shows how to use the converter to find a color that is between the system control and window colors but 90% towards the window color:

```xaml
<MultiBinding Converter="{StaticResource ColorInterpolationConverter}" ConverterParameter="0.9">
	<Binding Source="{x:Static SystemColors.ControlColor}" />
	<Binding Source="{x:Static SystemColors.WindowColor}" />
</MultiBinding>
```

## ConditionalConverter Class

The [ConditionalConverter](xref:ActiproSoftware.Windows.Data.ConditionalConverter) represents a value and multi-value converter that provides if-else functionality for `Binding` and `MultiBinding` objects.

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

## CornerRadiusConverter Class

The [CornerRadiusConverter](xref:ActiproSoftware.Windows.Data.CornerRadiusConverter) represents a value converter that converts between a `CornerRadius` and a number.

```xaml
... Value="{Binding Path=Number, Converter={StaticResource CornerRadiusConverter}}" ...
```

## DelegateConverter Class

The [DelegateConverter](xref:ActiproSoftware.Windows.Data.DelegateConverter) represents a value converter that uses delegates to perform the underlying conversion to/from the source.  Set the [ConvertCallback](xref:ActiproSoftware.Windows.Data.DelegateConverter.ConvertCallback) and [ConvertBackCallback](xref:ActiproSoftware.Windows.Data.DelegateConverter.ConvertBackCallback) properties to the delegates to use for conversion and back conversion respectively.

## DurationToMillisecondConverter Class

The [DurationToMillisecondConverter](xref:ActiproSoftware.Windows.Data.DurationToMillisecondConverter) represents a value converter that converts between a `Duration` and a number of milliseconds.

## EnumDescriptionConverter Class

The [EnumDescriptionConverter](xref:ActiproSoftware.Windows.Data.EnumDescriptionConverter) represents a value converter that uses the `DescriptionAttribute` for the string representation of the enumeration values, when available.

## ImageConverter Class

The [ImageConverter](xref:ActiproSoftware.Windows.Controls.ImageConverter) represents a value converter that returns a new [DynamicImage](windows-controls/dynamicimage.md) (inherits `Image`) control instance created using a specified URI or `BitmapSource`.

This converter expects the source value to be a `Uri`, a URI `String`, or a `BitmapSource` that can be used to create a new [DynamicImage](windows-controls/dynamicimage.md) instance. A prefix can be defined in [UriPrefix](xref:ActiproSoftware.Windows.Controls.ImageConverter.UriPrefix) which will be prepended to all source values of type `String` before the [DynamicImage](windows-controls/dynamicimage.md) is created.

The `Width` and `Height` properties can optionally be set to set the related properties on the [DynamicImage](windows-controls/dynamicimage.md) control that is created.

The [ImageProvider](xref:ActiproSoftware.Windows.Controls.ImageConverter.ImageProvider) property can optionally be set to a specific [ImageProvider](../themes/image-provider.md) to assign to the `ImageSource` created by the converter.  Leave the property its default value of `null` to use the static default [ImageProvider](../themes/image-provider.md) instance.

## ImageKeyToImageSourceConverter Class

The [ImageKeyToImageSourceConverter](xref:ActiproSoftware.Windows.Media.ImageKeyToImageSourceConverter) represents a value converter that uses an [ImageProvider](xref:ActiproSoftware.Windows.Media.ImageProvider) to lookup an `ImageSource` associated with a specified key.

This converter expects the source value to be a `String` that is a key recognized by [ImageProvider](xref:ActiproSoftware.Windows.Media.ImageProvider).

Optionally set the converter parameter to a specific instance of [ImageProvider](xref:ActiproSoftware.Windows.Media.ImageProvider). Otherwise, the instance defined by [ImageProvider](xref:ActiproSoftware.Windows.Media.ImageProvider).[Default](xref:ActiproSoftware.Windows.Media.ImageProvider.Default) will be used.

```xaml
<... ImageSource="{Binding Source={x:Static shared:SharedImageSourceKeys.Warning}, Converter={StaticResource ImageKeyToImageSourceConverter}}" ...
```

## InverseConverter Class

The [InverseConverter](xref:ActiproSoftware.Windows.Data.InverseConverter) represents a value converter that inverts another value converter.  Set an instance of the `IValueConverter` to invert in the [Converter](xref:ActiproSoftware.Windows.Data.InverseConverter.Converter) property.

## IsEnumFlagSetConverter Class

The [IsEnumFlagSetConverter](xref:ActiproSoftware.Windows.Data.IsEnumFlagSetConverter) represents a value converter that returns whether the specified enumeration value has the flag, specified by the converter's parameter, set.

## IsNullOrEmptyConverter Class

The [IsNullOrEmptyConverter](xref:ActiproSoftware.Windows.Data.IsNullOrEmptyConverter) represents a value converter that returns whether the specified value is `null`, and if it is a string, also if it is `null` or empty.

## IsTypeConverter Class

The [IsTypeConverter](xref:ActiproSoftware.Windows.Data.IsTypeConverter) represents a value converter that returns whether the specified object is the `Type` indicated in the converter's parameter.

## MultiplicationConverter Class

The [MultiplicationConverter](xref:ActiproSoftware.Windows.Data.MultiplicationConverter) represents a single and multi-value converter that multiplies all the source values provided, and optionally the converter's parameter.

## NoOpConverter Class

The [NoOpConverter](xref:ActiproSoftware.Windows.Data.NoOpConverter) represents a value converter that does not alter the value in any way (e.g. no operation).

This converter can be used to bypass an issue that arises from an optimization present in the WPF binding system.

## ParallaxConverter Class

The [ParallaxConverter](xref:ActiproSoftware.Windows.Data.ParallaxConverter) represents a value converter that can be used to create a parallax background scrolling effect.

Use the converter in a binding on a background element's `RenderTransform`'s `TranslateTransform.X` or `Y` property, and bind to a related `ScrollViewer.HorizontalOffset` or `VerticalOffset` property.

## PercentageConverter Class

The [PercentageConverter](xref:ActiproSoftware.Windows.Data.PercentageConverter) represents a value converter that converts between a number and a percentage.  The percentage is simply the number multiplied by `100`.

## StringFormatConverter Class

The [StringFormatConverter](xref:ActiproSoftware.Windows.Data.StringFormatConverter) represents a multi-value converter that provides `String.Format` functionality for both simple `Binding` and `MultiBinding` objects.

Pass the format string in the converter's parameter.  Since `{` characters are normally interpreted as markup extension starts, you can escape them by placing `{}` before the format string like this:

```xaml
<MultiBinding Converter="{StaticResource StringFormatConverter}" ConverterParameter="{}{0} of {1}">...
```

## ThicknessConverter Class

The [ThicknessConverter](xref:ActiproSoftware.Windows.Data.ThicknessConverter) represents a value converter that converts between a `Thickness` and a number.

By default, the `Thickness` returned will have all four sides (`Thickness.Left`, `Thickness.Top`, `Thickness.Right`, and `Thickness.Bottom`) set to the specified number. An optional parameter of type [Sides](xref:ActiproSoftware.Windows.Controls.Sides) can be specified, which can be used to customize the sides that are set.

```xaml
<!-- Set all four sides -->
... Value="{Binding Path=Number, Converter={StaticResource ThicknessConverter}}" ...
								
<!-- Only set the Left and Right sides -->
... Value="{Binding Path=Number, Converter={StaticResource ThicknessConverter}, ConverterParameter='Left,Right'}" ...
```

## TypeNameConverter Class

The [TypeNameConverter](xref:ActiproSoftware.Windows.Data.TypeNameConverter) represents a value converter that converts a value to a `String` of its short type name.

## UnitToDoubleConverter Class

The [UnitToDoubleConverter](xref:ActiproSoftware.Windows.Data.UnitToDoubleConverter) represents a value converter that converts between a [Unit](xref:ActiproSoftware.Windows.Unit) and a `Double`.

This converter needs additional information in order to convert a `Double` into a [Unit](xref:ActiproSoftware.Windows.Unit).  Specifically, it needs to the know whether the value is measured in pixels or as a percentage. To accommodate this need, the property [Type](xref:ActiproSoftware.Windows.Data.UnitToDoubleConverter.Type) is used to specify the type of measurement.

## UriConverter Class

The [UriConverter](xref:ActiproSoftware.Windows.Data.UriConverter) represents a value converter that returns a new `Uri` instance created using the specified URI string combined with an optional URI prefix.

This converter expects the source value to be a `String` which, when combined with the [UriPrefix](xref:ActiproSoftware.Windows.Data.UriConverter.UriPrefix), can be used to create a new `Uri` instance.

## UserPromptUIDialogButtonTextConverter Class

The [UserPromptUIDialogButtonTextConverter](xref:ActiproSoftware.Windows.Controls.UserPromptUIDialogButtonTextConverter) represents a value converter that translates a [UserPromptStandardResult](xref:ActiproSoftware.Windows.Controls.UserPromptStandardResult) into the equivalent text which can describe that button in a user interface dialog.

## ZoomLevelToTextFormattingModeConverter Class

The [ZoomLevelToTextFormattingModeConverter](xref:ActiproSoftware.Windows.Data.ZoomLevelToTextFormattingModeConverter) represents a value converter that alters the `TextFormattingMode` based on the specified zoom level.

`Display` mode is used when the zoom level is `1.0`. `Ideal` mode is used when the zoom level is increased.
