namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale;

/// <summary>
/// Represents a value converter that adds two numbers.
/// </summary>
[ValueConversion(typeof(double), typeof(double))]
public class AdditionConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is not double)
			throw new ArgumentException("The value passed to this converter must be a Double.");

		double parameterValue = 0;
		if (parameter is string parameterStringValue) {
			if (!double.TryParse(parameterStringValue, out parameterValue))
				parameterValue = 0;
		}

		return (double)value + parameterValue;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
