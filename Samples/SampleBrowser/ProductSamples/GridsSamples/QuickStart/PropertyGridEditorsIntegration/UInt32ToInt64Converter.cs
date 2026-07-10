namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridEditorsIntegration;

/// <summary>
/// Represents the converter that converts non-CLS compliant <see cref="UInt32"/> values to <see cref="Int64"/> values.
/// </summary>
/// <remarks>
/// This class is only needed if your app specifically wants to support non-CLS compliant <see cref="UInt32"/> values in editors.
/// </remarks>
[ValueConversion(typeof(UInt32), typeof(Int64))]
public class UInt32ToInt64Converter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> (Int64)(UInt32)value!;

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> (UInt32)(Int64)value!;

}
