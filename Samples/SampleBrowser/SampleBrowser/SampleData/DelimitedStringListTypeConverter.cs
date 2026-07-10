namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Converts a delimited string to a list of strings.
/// </summary>
public class DelimitedStringListTypeConverter : TypeConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) {
		if (value is string { Length: > 0 } delimitedString)
			return delimitedString.Split([';'], StringSplitOptions.RemoveEmptyEntries);

		return base.ConvertFrom(context, culture, value);
	}

	/// <inheritdoc/>
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) {
		if (sourceType == typeof(string))
			return true;

		return base.CanConvertFrom(context, sourceType);
	}

}
