namespace ActiproSoftware.ProductSamples.GaugeSamples.Demo.ScientificDashboard;

/// <summary>
/// Converts from Fahrenheit to Celsius.
/// </summary>
public sealed class CelsiusConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is null)
			return null;

		var fahrenheit = (double)value;
		return (5.0 / 9.0) * (fahrenheit - 32.0);
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotSupportedException();

}
