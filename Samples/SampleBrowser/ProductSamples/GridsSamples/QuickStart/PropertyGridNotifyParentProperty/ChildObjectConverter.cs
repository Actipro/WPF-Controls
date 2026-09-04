namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNotifyParentProperty;

/// <summary>
/// Represents a <c>TypeConverter</c> for the <see cref="ChildObject"/> type.
/// </summary>
public class ChildObjectConverter : ExpandableObjectConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) {
		return ((destinationType == typeof(string)) && (value is ChildObject child))
			? string.Format("Will={0}, WillNot={1}", child.WillNotify, child.WillNotNotify)
			: base.ConvertTo(context, culture, value, destinationType);
	}

}
