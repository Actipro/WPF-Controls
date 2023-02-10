using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml;

using ActiproSoftware.Windows.Controls;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ExplorerBarBoundItems {

	/// <summary>
	/// Converts an <see cref="XmlElement"/> to an <see cref="ImageTextInfo"/>.
	/// </summary>
	[ValueConversion(typeof(XmlElement), typeof(ImageTextInfo))]
	public class ExpanderHeaderConverter : IValueConverter {
		
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
			XmlElement element = value as XmlElement;
			if (null == element)
				return null;

			string image = null;
			if (null != element.Attributes["Image"])
				image = element.Attributes["Image"].Value;

			if (string.IsNullOrEmpty(image))
				return element.Attributes["Text"].Value;
			else
				return new ImageTextInfo() { Text = element.Attributes["Text"].Value,
											 ImageSourceLarge = new BitmapImage(new Uri("/Images/Icons/" + image, UriKind.RelativeOrAbsolute)) };
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
