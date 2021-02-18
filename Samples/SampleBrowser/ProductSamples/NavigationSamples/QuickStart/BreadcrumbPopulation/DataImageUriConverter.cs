using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbPopulation {
	/// <summary>
	/// Converts an <see cref="XmlElement"/> to and from a path.
	/// </summary>
	[ValueConversion(typeof(XmlElement), typeof(Uri))]
	public class DataImageUriConverter : IValueConverter {
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
			if (null != element) {
				if (element.Name == "MyRootElement")
					return new Uri("/Images/Icons/Computer16.png", UriKind.RelativeOrAbsolute);
				else if (element.Name == "MyFolderElement")
					return new Uri("/Images/Icons/FolderOpen16.png", UriKind.RelativeOrAbsolute);
				else if (element.Name == "MySubfolderElement")
					return new Uri("/Images/Icons/FolderOpenGreen16.png", UriKind.RelativeOrAbsolute);
			}
			return null;
		}

		/// <summary>
		/// This method always returns  <see langword="null"/> and should not be used.
		/// </summary>
		/// <param name="value">Not used.</param>
		/// <param name="targetType">Not used.</param>
		/// <param name="parameter">Not used.</param>
		/// <param name="culture">Not used.</param>
		/// <returns> <see langword="null"/>.</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
	}

}
