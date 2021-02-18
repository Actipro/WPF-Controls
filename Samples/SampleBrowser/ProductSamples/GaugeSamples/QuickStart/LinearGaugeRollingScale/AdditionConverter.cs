using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

using ActiproSoftware.Windows.Data;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale {

	/// <summary>
	/// Represents a value converter that adds two numbers.
	/// </summary>
	[ValueConversion(typeof(double), typeof(double))]
	public class AdditionConverter : IValueConverter {

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

			double parameterValue = 0;
			string parameterStringValue = parameter as string;
			if (parameterStringValue != null)
				double.TryParse(parameterStringValue, out parameterValue);

			return (double)value + parameterValue;
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

