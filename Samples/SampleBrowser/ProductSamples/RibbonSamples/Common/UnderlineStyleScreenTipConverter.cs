namespace ActiproSoftware.ProductSamples.RibbonSamples.Common;

/// <summary>
/// Represents a value converter that converts an <see cref="UnderlineStyle"/> to its appropriate screen tip.
/// </summary>
[ValueConversion(typeof(UnderlineStyle), typeof(string))]
public class UnderlineStyleScreenTipConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		return ((UnderlineStyle?)value) switch {
			UnderlineStyle.Underline => "Underline",
			UnderlineStyle.DoubleUnderline => "Double underline",
			UnderlineStyle.ThickUnderline => "Thick underline",
			UnderlineStyle.DottedUnderline => "Dotted underline",
			UnderlineStyle.DashedUnderline => "Dashed underline",
			UnderlineStyle.DotDashUnderline => "Dot-dash underline",
			UnderlineStyle.DotDotDashUnderline => "Dot-dot-dash underline",
			UnderlineStyle.WaveUnderline => "Wave underline",
			_ => "None"
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> DependencyProperty.UnsetValue;

}
