using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ExplorerBarBoundItems {

	/// <summary>
	/// Selects a <see cref="Style"/> appropriate for a specified item.
	/// </summary>
	public class ExpanderStyleSelector : StyleSelector {

		/// <summary>
		/// When overridden in a derived class, returns a <see cref="Style"/> based on custom logic.
		/// </summary>
		/// <param name="item">The content.</param>
		/// <param name="container">The element to which the style will be applied.</param>
		/// <returns>
		/// Returns an application-specific style to apply; otherwise, null.
		/// </returns>
		public override Style SelectStyle(object item, DependencyObject container) {
			Expander expander = container as Expander;
			if (null == expander)
				return null;

			string styleKey = "ExpanderGroupStyle";
			XmlElement element = item as XmlElement;
			if (null != element) {
				if (null != element.Attributes["UseAlternateStyle"]) {
					bool useAlternateStyle;
					if (bool.TryParse(element.Attributes["UseAlternateStyle"].Value, out useAlternateStyle)) {
						if (useAlternateStyle)
							styleKey = "ExpanderGroupAlternateStyle";
					}
				}
			}

			return expander.FindResource(styleKey) as Style;
		}
	}
}
