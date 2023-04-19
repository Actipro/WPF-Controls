using System;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem {
	/// <summary>
	/// Converts an <see cref="XmlElement"/> to and from a path.
	/// </summary>
	[ValueConversion(typeof(XmlElement), typeof(string))]
	public class PathConverter : IValueConverter {
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns null, the valid null value is used.
		/// </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return ConvertItemHelper.GetPath(value);
		}

		/// <summary>
		/// This method always returns  <c>null</c> and should not be used.
		/// </summary>
		/// <param name="value">Not used.</param>
		/// <param name="targetType">Not used.</param>
		/// <param name="parameter">Not used.</param>
		/// <param name="culture">Not used.</param>
		/// <returns> <c>null</c>.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

}
