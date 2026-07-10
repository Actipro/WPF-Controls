namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.RadialSliderIntro;

/// <summary>
/// Represents a value converter that converts between a degree and a warning level message.
/// </summary>
public class WarningLevelConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var angle = (int)((double?)value ?? 0.0);
		return angle switch {
			90 => "Warning",
			150 => "Critical",
			_ => "Good"
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
