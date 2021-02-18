using System;
using System.Collections.Generic;
using System.Globalization;
#if WINRT
using Windows.UI.Xaml.Data;
#else
using System.Windows.Data;
#endif

namespace ActiproSoftware.ProductSamples.Charts.Common {

	/// <summary>
	/// Takes a <see cref="System.Double"/> value, performs <see cref="Math.Ceiling"/>, and converts it into an <see cref="System.Int32"/>.
	/// </summary>
	public class DoubleToIntConverter : IValueConverter {

		private static readonly List<int> twelveDivisors = new List<int> {1, 2, 3, 4, 6, 12};

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Modifies the source data before passing it to the target for display in the UI.
		/// </summary>
		/// <param name="value">The source data being passed to the target.</param>
		/// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the target dependency property.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>The value to be passed to the target dependency property.</returns>
		#if WINRT
		public object Convert(object value, Type targetType, object parameter, string culture) {
		#else
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
		#endif
			if (value is double) {
				var intValue = (int) Math.Ceiling((double) value);
				return twelveDivisors[intValue];
			}

			return value;
		}

		/// <summary>
		/// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay" /> bindings.
		/// </summary>
		/// <param name="value">The target data being passed to the source.</param>
		/// <param name="targetType">The <see cref="T:System.Type" /> of data expected by the source object.</param>
		/// <param name="parameter">An optional parameter to be used in the converter logic.</param>
		/// <param name="culture">The culture of the conversion.</param>
		/// <returns>The value to be passed to the source object.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		#if WINRT
		public object ConvertBack(object value, Type targetType, object parameter, string culture) {
		#else
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
		#endif
			throw new NotImplementedException();
		}

		#endregion PUBLIC PROCEDURES
	}
}
