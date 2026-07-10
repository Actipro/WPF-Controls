namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// Converts a decimal value to a string formatted as currency.
/// </summary>
public class CurrencyValueConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is decimal decimalValue)
			return decimalValue.ToString("#.00");

		return value;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
