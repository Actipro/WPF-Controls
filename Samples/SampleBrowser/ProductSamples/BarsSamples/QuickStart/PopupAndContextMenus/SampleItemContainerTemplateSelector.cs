using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Provides an <see cref="ItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
	/// generally assigned to root bar controls, like to <see cref="ActiproSoftware.Windows.Controls.Bars.Ribbon"/>'s
	/// <see cref="ActiproSoftware.Windows.Controls.Bars.Ribbon.ItemContainerTemplateSelector"/> property.
	/// </summary>
	internal class SampleItemContainerTemplateSelector : BarControlTemplateSelector {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="MenuPopupViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate MenuPopupTemplate { get; set; }

		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl) {
			// Associate the custom view model with the appropriate template
			if (item is MenuPopupViewModel)
				return MenuPopupTemplate;

			return base.SelectTemplate(item, parentItemsControl);
		}

	}

}
