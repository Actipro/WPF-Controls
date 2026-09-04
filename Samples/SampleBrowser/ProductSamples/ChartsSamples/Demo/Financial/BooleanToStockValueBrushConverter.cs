namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// Converts a boolean value to <see cref="Colors.Green"/> or <see cref="Colors.Red"/>.
/// </summary>
public class BooleanToStockValueBrushConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is bool boolValue) {
			return (boolValue)
				? new SolidColorBrush(Colors.Green)
				: new SolidColorBrush(Colors.Red);
		}

		return value;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
