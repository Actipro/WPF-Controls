using System;
using System.Globalization;
using System.Windows.Data;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridEditorsIntegration {

	/// <summary>
	/// Represents the converter that converts non-CLS compliant <see cref="UInt16"/> values to <see cref="Int32"/> values.
	/// </summary>
	/// <remarks>
	/// This class is only needed if your app specifically wants to support non-CLS compliant <see cref="UInt16"/> values in editors.
	/// </remarks>
	[ValueConversion(typeof(UInt16), typeof(Int32))]
	public class UInt16ToInt32Converter : IValueConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding source to the binding target.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return (Int32)(UInt16)value;
		}

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding target to the binding source.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return (UInt16)(Int32)value;
		}

	}

}