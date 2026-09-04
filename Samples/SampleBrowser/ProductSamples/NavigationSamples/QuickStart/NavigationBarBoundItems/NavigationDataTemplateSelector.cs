using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarBoundItems;

/// <summary>
/// Selects a <see cref="DataTemplate"/> appropriate for a specified item.
/// </summary>
public class NavigationDataTemplateSelector : DataTemplateSelector {

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		if (container is not FrameworkElement frameworkElement)
			return null;

		var xmlElement = item as XmlElement;
		var templateKey = xmlElement?.Name switch {
			"NavigationCalendar" => "NavigationCalendarItemTemplate",
			"NavigationMail" => "NavigationMailItemTemplate",
			"NavigationText" => "NavigationTextItemTemplate",
			_ => null
		};

		return (templateKey is not null)
			? frameworkElement.FindResource(templateKey) as DataTemplate
			: null;
	}

}
