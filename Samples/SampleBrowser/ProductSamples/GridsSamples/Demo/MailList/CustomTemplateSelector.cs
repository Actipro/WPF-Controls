#if WINRT
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.MailList {

	/// <summary>
	/// Chooses a <see cref="DataTemplate"/> based on the data object and the data-bound element.
	/// </summary>
	public class CustomTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for groups.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for groups.</value>
		public DataTemplate GroupTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for mail.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for mail.</value>
		public DataTemplate MailTemplate { get; set; }
		
		/// <summary>
		/// Returns a <see cref="DataTemplate"/> based on custom logic.
		/// </summary>
		/// <param name="item">The data object.</param>
		/// <param name="container">The data-bound element.</param>
		/// <returns>The <see cref="DataTemplate"/> to use.</returns>
		#if WINRT
		protected override DataTemplate SelectTemplateCore(object item, DependencyObject container) {
		#else
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
		#endif
			if (item is MailTreeNodeModel)
				return this.MailTemplate;
			else
				return this.GroupTemplate;
		}

	}

}
