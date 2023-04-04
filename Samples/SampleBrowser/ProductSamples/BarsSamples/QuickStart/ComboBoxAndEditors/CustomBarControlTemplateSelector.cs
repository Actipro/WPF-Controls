using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxAndEditors {
	
	/// <summary>
	/// Provides an <see cref="ItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
	/// generally assigned to root bar controls, like to <see cref="Ribbon"/>'s <see cref="Ribbon.ItemContainerTemplateSelector"/> property.
	/// </summary>
	public class CustomBarControlTemplateSelector : BarControlTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="ColorEditBoxViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate ColorEditBoxDefaultTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="DateEditBoxViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate DateEditBoxDefaultTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="Int32EditBoxViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate Int32EditBoxDefaultTemplate { get; set; }
		
		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl) {
			switch (item) {
				case ColorEditBoxViewModel _:
					return this.ColorEditBoxDefaultTemplate;
				case DateEditBoxViewModel _:
					return this.DateEditBoxDefaultTemplate;
				case Int32EditBoxViewModel _:
					return this.Int32EditBoxDefaultTemplate;
			}

			return base.SelectTemplate(item, parentItemsControl);
		}

	}

}
