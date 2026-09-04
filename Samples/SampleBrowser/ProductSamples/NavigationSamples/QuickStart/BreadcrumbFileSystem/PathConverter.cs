using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbFileSystem;

/// <summary>
/// Converts an <see cref="XmlElement"/> to and from a path.
/// </summary>
[ValueConversion(typeof(XmlElement), typeof(string))]
public class PathConverter : IValueConverter {

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> ConvertItemHelper.GetPath(value);

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
		=> null;

}
