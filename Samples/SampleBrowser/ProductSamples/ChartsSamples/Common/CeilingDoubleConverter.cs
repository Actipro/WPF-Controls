namespace ActiproSoftware.ProductSamples.Charts.Common;

/// <summary>
/// Performs <see cref="Math.Ceiling"/> on double values that it converts.
/// </summary>
public class CeilingDoubleConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is double doubleValue)
			return Math.Ceiling(doubleValue);

		return value;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
