using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Common {

	/// <summary>
	/// Represents a value converter that converts an <see cref="UnderlineStyle"/> to its appropriate screen tip.
	/// </summary>
	[ValueConversion(typeof(UnderlineStyle), typeof(string))]
	public class UnderlineStyleScreenTipConverter : IValueConverter {

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding source to the binding target.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>A converted value.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			switch ((UnderlineStyle)value) {
				case UnderlineStyle.Underline:
					return "Underline";
				case UnderlineStyle.DoubleUnderline:
					return "Double underline";
				case UnderlineStyle.ThickUnderline:
					return "Thick underline";
				case UnderlineStyle.DottedUnderline:
					return "Dotted underline";
				case UnderlineStyle.DashedUnderline:
					return "Dashed underline";
				case UnderlineStyle.DotDashUnderline:
					return "Dot-dash underline";
				case UnderlineStyle.DotDotDashUnderline:
					return "Dot-dot-dash underline";
				case UnderlineStyle.WaveUnderline:
					return "Wave underline";
				default:
					return "None";
			}
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
			return DependencyProperty.UnsetValue;
		}
	}


}

