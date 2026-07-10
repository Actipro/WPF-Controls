namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.RangeSliderIntro;

/// <summary>
/// Represents a value converter that converts a total number of minutes into a string.
/// </summary>
public class TotalMinutesToElapsedTimeConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is not double doubleValue)
			throw new ArgumentException($"Value must be of type {nameof(Double)}.", nameof(value));

		if (double.IsNaN(doubleValue) || double.IsInfinity(doubleValue))
			return null;

		var elapsedTime = TimeSpan.FromMinutes(doubleValue);
		return (elapsedTime.Hours > 0)
			? $"{elapsedTime.Hours}h {elapsedTime.Minutes}m"
			: $"{elapsedTime.Minutes}m";
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}