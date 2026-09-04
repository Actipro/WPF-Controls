namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridEditorsIntegration;

/// <summary>
/// Represents the converter that converts non-CLS compliant <see cref="UInt16"/> values to <see cref="Int32"/> values.
/// </summary>
/// <remarks>
/// This class is only needed if your app specifically wants to support non-CLS compliant <see cref="UInt16"/> values in editors.
/// </remarks>
[ValueConversion(typeof(UInt16), typeof(Int32))]
public class UInt16ToInt32Converter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> (Int32)(UInt16)value!;

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> (UInt16)(Int32)value!;

}
