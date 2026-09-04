using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbPopulation;

/// <summary>
/// Converts an <see cref="XmlElement"/> to and from a path.
/// </summary>
[ValueConversion(typeof(XmlElement), typeof(Uri))]
public class DataImageUriConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture) {
		var element = value as XmlElement;
		return element?.Name switch {
			"MyRootElement" => new Uri("/Images/Icons/Computer16.png", UriKind.RelativeOrAbsolute),
			"MyFolderElement" => new Uri("/Images/Icons/FolderOpen16.png", UriKind.RelativeOrAbsolute),
			"MySubfolderElement" => new Uri("/Images/Icons/FolderOpenGreen16.png", UriKind.RelativeOrAbsolute),
			_ => null
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> null;
}
