using ActiproSoftware.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ExplorerBarBoundItems;

/// <summary>
/// Converts an <see cref="XmlElement"/> to an <see cref="ImageTextInfo"/>.
/// </summary>
[ValueConversion(typeof(XmlElement), typeof(ImageTextInfo))]
public class ExpanderHeaderConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is not XmlElement element)
			return null;

		var image = element.Attributes["Image"]?.Value;
		if (string.IsNullOrEmpty(image))
			return element.Attributes["Text"]?.Value;
		else {
			return new ImageTextInfo() {
				Text = element.Attributes["Text"]?.Value,
				ImageSourceLarge = new BitmapImage(new Uri("/Images/Icons/" + image, UriKind.RelativeOrAbsolute))
			};
		}
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> null;

}
