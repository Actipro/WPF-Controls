namespace ActiproSoftware.ProductSamples.MicroChartsSamples.ChartTypes.Segment;

/// <summary>
/// Represents a value converter that converts between a segment value and height.
/// </summary>
public class SegmentHeightConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		// Create a green to yellow to red gradient effect
		var intValue = ((int?)value).GetValueOrDefault(0);
		return 10 + 3 * intValue;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
