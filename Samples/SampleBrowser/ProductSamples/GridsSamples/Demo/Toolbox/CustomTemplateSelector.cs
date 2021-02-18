using System;
#if WINRT
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#else
using System.Windows;
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox {

	/// <summary>
	/// Chooses a <see cref="DataTemplate"/> based on the data object and the data-bound element.
	/// </summary>
	public class CustomTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for categories.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for categories.</value>
		public DataTemplate CategoryTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for controls.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for controls.</value>
		public DataTemplate ControlTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a placeholder in an empty category.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use for a placeholder in an empty category.</value>
		public DataTemplate EmptyPlaceholderTemplate { get; set; }

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
			if (item is ControlTreeNodeModel)
				return this.ControlTemplate;
			else if (item is CategoryTreeNodeModel)
				return this.CategoryTemplate;
			else if (item is EmptyPlaceholderTreeNodeModel)
				return this.EmptyPlaceholderTemplate;
			else
				return base.SelectTemplate(item, container);
		}

	}

}
