namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems;

/// <summary>
/// Represents a type converter for <see cref="ChildObject"/> that derives from <c>TypeConverter</c>.
/// </summary>
public class ChildObjectConverter : ExpandableObjectConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) {
		if (sourceType == typeof(string))
			return true;
		return base.CanConvertFrom(context, sourceType);
	}

	/// <inheritdoc/>
	public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) {
		if (value is string stringValue)
			return new ChildObject() { Name = stringValue };

		return base.ConvertFrom(context, culture, value);
	}

}
