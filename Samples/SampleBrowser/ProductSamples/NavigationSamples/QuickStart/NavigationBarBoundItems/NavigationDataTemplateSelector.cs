using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace ActiproSoftware.ProductSamples.NavigationSamples.QuickStart.NavigationBarBoundItems {

	/// <summary>
	/// Selects a <see cref="DataTemplate"/> appropriate for a specified item.
	/// </summary>
	public class NavigationDataTemplateSelector : DataTemplateSelector {

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
			if (xmlElement.Name == "NavigationCalendar")
				templateKey = "NavigationCalendarItemTemplate";
			else if (xmlElement.Name == "NavigationMail")
				templateKey = "NavigationMailItemTemplate";
			else if (xmlElement.Name == "NavigationText")
				templateKey = "NavigationTextItemTemplate";
			else
				return null;

			return frameworkElement.FindResource(templateKey) as DataTemplate;
		}
	}
}
