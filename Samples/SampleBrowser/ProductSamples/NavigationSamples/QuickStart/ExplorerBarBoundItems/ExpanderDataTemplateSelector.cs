using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.ExplorerBarBoundItems {

	/// <summary>
	/// Selects a <see cref="DataTemplate"/> appropriate for a specified item.
	/// </summary>
	public class ExpanderDataTemplateSelector : DataTemplateSelector {

		/// <summary>
		/// When overridden in a derived class, returns a <see cref="DataTemplate"/> based on custom logic.
		/// </summary>
		/// <param name="item">The data object for which to select the template.</param>
		/// <param name="container">The data-bound object.</param>
		/// <returns>
		/// Returns a <see cref="DataTemplate"/> or null. The default value is null.
		/// </returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			FrameworkElement frameworkElement = container as FrameworkElement;
			if (null == frameworkElement)
				return null;

			XmlElement xmlElement = item as XmlElement;
			if (null == xmlElement)
				return null;

			string templateKey = null;
			if (xmlElement.Name == "ExpanderHyperlink")
				templateKey = "ExpanderHyperlinkItemTemplate";
			else if (xmlElement.Name == "ExpanderFileInfo")
				templateKey = "ExpanderFileInfoItemTemplate";
			else if (xmlElement.Name == "ExpanderFileDetail")
				templateKey = "ExpanderFileDetailItemTemplate";
			else
				return null;

			return frameworkElement.FindResource(templateKey) as DataTemplate;
		}
	}
}
