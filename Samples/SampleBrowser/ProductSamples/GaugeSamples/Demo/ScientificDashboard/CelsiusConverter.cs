using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.GaugeSamples.Demo.ScientificDashboard {

	/// <summary>
	/// Converts from fahrenheit to celsius.
	/// </summary>
	public sealed class CelsiusConverter : IValueConverter {

		/// <summary>
		/// Converts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			double fahrenheit = (double)value;
			return (5.0 / 9.0) * (fahrenheit - 32.0);
		}

		/// <summary>
		/// Converts the back.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotSupportedException();
		}

	}
}
