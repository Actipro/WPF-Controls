using ActiproSoftware.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbPopulation;

/// <summary>
/// Converts an <see cref="XmlElement"/> to and from a path.
/// </summary>
[ValueConversion(typeof(XmlElement), typeof(Image))]
public class DataImageConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		if (value is XmlElement element) {
			var uri = new DataImageUriConverter().Convert(value, targetType, parameter, culture) as Uri;
			if (uri is not null) {
				return new DynamicImage {
					Source = new BitmapImage(uri)
				};
			}
		}
		return null;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> null;

}
