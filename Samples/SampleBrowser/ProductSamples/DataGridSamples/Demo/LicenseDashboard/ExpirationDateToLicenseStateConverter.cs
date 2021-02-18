using System;
using System.Globalization;
using System.Windows.Data;

namespace ActiproSoftware.ProductSamples.DataGridSamples.Demo.LicenseDashboard {

	/// <summary>
	/// Provides a converter that gets the <c>LicenseState</c> based on an expiration date.
	/// </summary>
	public sealed class ExpirationDateToLicenseStateConverter : IValueConverter {

		/// <summary>
		/// Converts the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="targetType">Type of the target.</param>
		/// <param name="parameter">The parameter.</param>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (null != value && (value is DateTime || value is DateTime?)) {
				DateTime dateTime = (DateTime)value;
				if ((dateTime - DateTime.Now).TotalDays <= 0)
					return LicenseState.Expired;
				else if ((dateTime - DateTime.Now).TotalDays <= 60)
					return LicenseState.ExpiringSoon;
			}
			return LicenseState.Valid;
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
