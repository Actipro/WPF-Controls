namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.BarChartSlotting;

/// <summary>
/// Converts the value into a label for a slot interval specifying number of months.
/// </summary>
public class TickIntervalConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var doubleValue = Math.Ceiling(System.Convert.ToDouble(value));
		int intValue = System.Convert.ToInt32(doubleValue);

		return intValue switch {
			0 => 1,
			1 => 2,
			2 => 4,
			3 => 6,
			4 => 8,
			_ => 10
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
