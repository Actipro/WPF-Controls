namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.BarChartSlotting;

/// <summary>
/// Converts the value into a label for a slot interval specifying number of months.
/// </summary>
public class MonthLabelConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		int intValue = System.Convert.ToInt32(value);
		return (intValue == 1)
			? string.Format("{0} month", intValue)
			: string.Format("{0} months", intValue);
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
