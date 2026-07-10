namespace ActiproSoftware.ProductSamples.MicroChartsSamples.ChartTypes.Segment;

/// <summary>
/// Represents a value converter that converts between a segment value and a brush.
/// </summary>
public class SegmentColorConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		// Create a green to yellow to red gradient effect
		return (int?)value switch {
			1 => new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x80, 0x00)),
			2 => new SolidColorBrush(Color.FromArgb(0xFF, 0x20, 0x90, 0x00)),
			3 => new SolidColorBrush(Color.FromArgb(0xFF, 0x4E, 0xA7, 0x00)),
			4 => new SolidColorBrush(Color.FromArgb(0xFF, 0x7F, 0xBF, 0x00)),
			5 => new SolidColorBrush(Color.FromArgb(0xFF, 0xB1, 0xD8, 0x00)),
			6 => new SolidColorBrush(Color.FromArgb(0xFF, 0xEA, 0xF4, 0x00)),
			7 => new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xDC, 0x00)),
			8 => new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xAE, 0x00)),
			9 => new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x50, 0x00)),
			_ => new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0x30, 0x00))
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
