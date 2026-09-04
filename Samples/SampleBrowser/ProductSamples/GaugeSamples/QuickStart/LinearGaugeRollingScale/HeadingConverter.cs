namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale;

/// <summary>
/// Represents a value converter that converts any heading to a <c>0..359</c> string.
/// </summary>
[ValueConversion(typeof(double), typeof(string))]
public class HeadingConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is not double)
			throw new ArgumentException("The value passed to this converter must be a Double.");

		var doubleValue = (double)value;
		if (doubleValue < 0.0)
			doubleValue += 360.0;
		if (doubleValue >= 360.0)
			doubleValue -= 360.0;

		return doubleValue.ToString("N0");
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
