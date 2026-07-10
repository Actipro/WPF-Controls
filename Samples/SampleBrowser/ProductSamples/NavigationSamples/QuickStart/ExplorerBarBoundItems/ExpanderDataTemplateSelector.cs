using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ExplorerBarBoundItems;

/// <summary>
/// Selects a <see cref="DataTemplate"/> appropriate for a specified item.
/// </summary>
public class ExpanderDataTemplateSelector : DataTemplateSelector {

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		var frameworkElement = container as FrameworkElement;
		if (frameworkElement is null)
			return null;

		var xmlElement = item as XmlElement;
		var templateKey = xmlElement?.Name switch {
			"ExpanderHyperlink" => "ExpanderHyperlinkItemTemplate",
			"ExpanderFileInfo" => "ExpanderFileInfoItemTemplate",
			"ExpanderFileDetail" => "ExpanderFileDetailItemTemplate",
			_ => null
		};

		return (templateKey is not null)
			? frameworkElement.FindResource(templateKey) as DataTemplate
			: null;
	}

}
