using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ExplorerBarBoundItems;

/// <summary>
/// Selects a <see cref="Style"/> appropriate for a specified item.
/// </summary>
public class ExpanderStyleSelector : StyleSelector {

	/// <inheritdoc/>
	public override Style? SelectStyle(object item, DependencyObject container) {
		if (container is not Expander expander)
			return null;

		var styleKey = "ExpanderGroupStyle";
		if (
			item is XmlElement element
			&& bool.TryParse(element.Attributes["UseAlternateStyle"]?.Value, out var useAlternateStyle)
			&& useAlternateStyle
		) {
			styleKey = "ExpanderGroupAlternateStyle";
		}

		return expander.FindResource(styleKey) as Style;
	}

}
