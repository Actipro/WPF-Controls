namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.RadialSliderIntro;

/// <summary>
/// Represents a value converter that converts between a degree and a scalar value.
/// </summary>
public class DegreeToScalarConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var angle = (double?)value ?? 0.0;

		var stepValue = 1.0;
		var parameterText = parameter as string;
		if (!string.IsNullOrEmpty(parameterText)) {
			if (!double.TryParse(parameterText, out stepValue))
				stepValue = 1.0;
		}

		return Math.Round(angle / stepValue);
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
