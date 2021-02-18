using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.BreadcrumbComboBox {
	/// <summary>
	/// Represents a custom DataTemplateSelector, which simply selects the template resource with the key "ComboBoxItemTemplate"
	/// when the item is an XmlElement. All other items will use their default template.
	/// </summary>
	public class CustomComboBoxItemTemplateSelector : DataTemplateSelector {
		/// <summary>
		/// When overridden in a derived class, returns a <see cref="System.Windows.DataTemplate"/> based on custom logic.
		/// </summary>
		/// <param name="item">The data object for which to select the template.</param>
		/// <param name="container">The data-bound object.</param>
		/// <returns>
		/// Returns a <see cref="System.Windows.DataTemplate"/> or null. The default value is null.
		/// </returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is XmlElement) {
				FrameworkElement element = container as FrameworkElement;
				if (null != element)
					return (DataTemplate)element.FindResource("ComboBoxItemTemplate");
			}
			return base.SelectTemplate(item, container);
		}
	}
}
