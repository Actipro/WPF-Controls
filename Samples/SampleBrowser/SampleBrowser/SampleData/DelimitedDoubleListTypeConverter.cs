namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Converts a delimited string to a list of double.
/// </summary>
public class DelimitedDoubleListTypeConverter : TypeConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) {
		if (value is string { Length: > 0 } delimitedString) {
			var list = new List<double>();
			var numberStrings = delimitedString.Split([';'], StringSplitOptions.RemoveEmptyEntries);
			foreach (var numberString in numberStrings) {
				if (double.TryParse(numberString, out var numberValue))
					list.Add(numberValue);
			}

			return list;
		}

		return base.ConvertFrom(context, culture, value);
	}

	/// <inheritdoc/>
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) {
		if (sourceType == typeof(string))
			return true;

		return base.CanConvertFrom(context, sourceType);
	}

}
