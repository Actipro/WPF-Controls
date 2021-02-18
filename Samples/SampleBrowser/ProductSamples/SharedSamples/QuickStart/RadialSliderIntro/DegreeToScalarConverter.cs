using System;
using System.Globalization;
#if WINRT
using Windows.UI.Xaml.Data;
#else
using System.Windows.Data;
#endif

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.RadialSliderIntro {

	/// <summary>
	/// Represents a value converter that converts between a degree and a scalar value.
	/// </summary>
	public class DegreeToScalarConverter : IValueConverter {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding source to the binding target.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>A converted value.</returns>
		#if WINRT
		public object Convert(object value, Type targetType, object parameter, string culture) {
		#else
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		#endif
			var angle = (double)value;

			var stepValue = 1.0;
			var parameterText = parameter as string;
			if (!string.IsNullOrEmpty(parameterText))
				double.TryParse(parameterText, out stepValue);

			return Math.Round(angle / stepValue);
		}

		/// <summary>
		/// Converts a value. The data binding engine calls this method when it propagates a value from the binding target to the binding source.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>A converted value.</returns>
		#if WINRT
		public object ConvertBack(object value, Type targetType, object parameter, string culture) {
		#else
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		#endif
			throw new NotImplementedException();
		}
	}


}

