namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// Converts a decimal value to a string formatted as change in currency.
/// </summary>
public class ChangeValueConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is decimal decimalValue) {
			var sign = decimalValue >= 0 ? "+" : "";
			return sign + decimalValue.ToString("00.00");
		}

		return value;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
