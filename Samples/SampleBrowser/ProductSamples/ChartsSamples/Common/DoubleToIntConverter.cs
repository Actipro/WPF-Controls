namespace ActiproSoftware.ProductSamples.Charts.Common;

/// <summary>
/// Takes a <see cref="System.Double"/> value, performs <see cref="Math.Ceiling"/>, and converts it into an <see cref="System.Int32"/>.
/// </summary>
public class DoubleToIntConverter : IValueConverter {

	private static readonly List<int> _twelveDivisors = [1, 2, 3, 4, 6, 12];

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is double doubleValue) {
			var intValue = (int)Math.Ceiling(doubleValue);
			return _twelveDivisors[intValue];
		}

		return value;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> throw new NotImplementedException();

}
