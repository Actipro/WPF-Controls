using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Selects a <see cref="BulletContentControl"/> template selector.
	/// </summary>
	public class BulletContentControlTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for strings.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for strings.</value>
		public DataTemplate StringTemplate { get; set; }
		
		/// <summary>
		/// Selects a data template.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container to examine.</param>
		/// <returns>The <see cref="DataTemplate"/> that was selected.</returns>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			if (item is string)
				return this.StringTemplate;
			else
				return null;
		}

	}

}
