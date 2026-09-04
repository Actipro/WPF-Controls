namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters;

/// <summary>
/// Represents a <see cref="DoubleConverter"/> that presents a value in Fahrenheit, but can accept values in
/// Fahrenheit or Celsius.
/// </summary>
public class TemperatureTypeConverter : DoubleConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) {
		if (value is string stringValue) {
			stringValue = stringValue.Trim().ToUpperInvariant();

			// Determine if Fahrenheit or Celsius
			bool fahrenheit = true;
			if (stringValue.Length > 0) {
				var scale = stringValue.Substring(stringValue.Length - 1);
				var removeLastChar = false;
				if ("C" == scale) {
					fahrenheit = false;
					removeLastChar = true;
				}
				else if ("F" == scale) {
					removeLastChar = true;
				}

				if (removeLastChar) {
					stringValue = (stringValue.Length > 1)
						? stringValue.Substring(0, stringValue.Length - 1).Trim()
						: string.Empty;
				}
			}

			// Remove degree symbol, if it's there
			if (stringValue.Length > 0) {
				var scale = stringValue.Substring(stringValue.Length - 1);
				if ("°" == scale) {
					stringValue = (stringValue.Length > 1)
						? stringValue.Substring(0, stringValue.Length - 1).Trim()
						: string.Empty;
				}
			}

			// Convert degrees portion using base class
			var degrees = 0.0;
			if (stringValue.Length != 0)
				degrees = (double)base.ConvertFrom(context, culture, stringValue)!;

			// Converter if needed
			if (!fahrenheit)
				degrees = degrees * 9 / 5 + 32.0;

			return degrees;
		}

		return base.ConvertFrom(context, culture, value);
	}

	/// <inheritdoc/>
	public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) {
		if (destinationType is null)
			throw new ArgumentNullException(nameof(destinationType));

		if ((value is not null) && (destinationType == typeof(string)))
			return string.Format("{0:#######0.0}° F", value);

		return base.ConvertTo(context, culture, value, destinationType);
	}

}
