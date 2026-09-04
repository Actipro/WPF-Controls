namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Baseball;

/// <summary>
/// Converts a given double into a string formatted like a baseball stat.
/// </summary>
public class BaseballStatValueConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is double doubleValue)
			return doubleValue.ToString("0.000");

		return value;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
