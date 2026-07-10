namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Implements a <see cref="TypeConverter"/> that allows any <see cref="IConvertable"/> type to be converted to a <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The target type.</typeparam>
public class ConvertibleTypeConverter<T> : TypeConverter where T : IConvertible {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
		=> sourceType.GetInterface(nameof(IConvertible), ignoreCase: false) is not null;

	/// <inheritdoc/>
	public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
		=> destinationType?.GetInterface(nameof(IConvertible), ignoreCase: false) is not null;

	/// <inheritdoc/>
	public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
		=> ((IConvertible)value).ToType(typeof(T), culture);

	/// <inheritdoc/>
	public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
		=> ((IConvertible?)value)?.ToType(destinationType, culture);

}
