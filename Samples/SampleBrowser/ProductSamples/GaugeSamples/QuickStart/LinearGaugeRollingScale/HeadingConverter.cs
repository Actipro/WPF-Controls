using System;
using System.Globalization;
using System.Windows.Data;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale {

	/// <summary>
	/// Represents a value converter that converts any heading to a <c>0..359</c> string.
	/// </summary>
	[ValueConversion(typeof(double), typeof(string))]
	public class HeadingConverter : IValueConverter {

		/// <summary>
		/// Converts the specified value to an average based on the previous values converted.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// An average of the specified value and previous values.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (!(value is double))
				throw new ArgumentException("The value passed to this converter must be a Double.");

			var doubleValue = (double)value;
			if (doubleValue < 0.0)
				doubleValue += 360.0;
			if (doubleValue >= 360.0)
				doubleValue -= 360.0;

			return doubleValue.ToString("N0");
		}

		/// <summary>
		/// This method always throws a <see cref="NotImplementedException"/> exception and should not be used.
		/// </summary>
		/// <param name="value">Not used.</param>
		/// <param name="targetType">Not used.</param>
		/// <param name="parameter">Not used.</param>
		/// <param name="culture">Not used.</param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}

	}
}

