using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbComboBox;

/// <summary>
/// Represents a custom DataTemplateSelector, which simply selects the template resource with the key "ComboBoxItemTemplate"
/// when the item is an XmlElement. All other items will use their default template.
/// </summary>
public class CustomComboBoxItemTemplateSelector : DataTemplateSelector {

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return ((item is XmlElement) && (container is FrameworkElement element))
			? element.FindResource("ComboBoxItemTemplate") as DataTemplate
			: base.SelectTemplate(item, container);
	}

}
