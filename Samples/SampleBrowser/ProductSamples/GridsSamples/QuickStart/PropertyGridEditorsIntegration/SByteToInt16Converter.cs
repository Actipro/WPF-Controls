namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridEditorsIntegration;

/// <summary>
/// Represents the converter that converts non-CLS compliant <see cref="SByte"/> values to <see cref="Int16"/> values.
/// </summary>
/// <remarks>
/// This class is only needed if your app specifically wants to support non-CLS compliant <see cref="SByte"/> values in editors.
/// </remarks>
[ValueConversion(typeof(SByte), typeof(Int16))]
public class SByteToInt16Converter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> (Int16)(SByte)value!;

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> (SByte)(Int16)value!;

}
