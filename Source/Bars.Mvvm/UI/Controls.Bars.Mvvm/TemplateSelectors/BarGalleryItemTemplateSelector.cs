using ActiproSoftware.Windows.Controls.Bars.Primitives;
using ActiproSoftware.Windows.Themes;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides a <see cref="DataTemplateSelector"/> that selects item templates for various gallery item view models, 
	/// generally used with a <see cref="BarGalleryBase"/>-based control and assigned to its <see cref="ItemsControl.ItemTemplateSelector"/> property.
	/// </summary>
	public class BarGalleryItemTemplateSelector : DataTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="BarGalleryItemTemplateSelector"/> class.
		/// </summary>
		public BarGalleryItemTemplateSelector() {
			var dictionary = BarsMvvmResourceDictionary.Instance;

			this.ColorMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemColorMenuItemDataTemplate] as DataTemplate;
			this.ColorTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemColorDataTemplate] as DataTemplate;
			this.FontFamilyTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemFontFamilyDataTemplate] as DataTemplate;
			this.FontSizeTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemFontSizeDataTemplate] as DataTemplate;
			this.MenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemMenuItemDataTemplate] as DataTemplate;
			this.SizeSelectionTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemSizeSelectionDataTemplate] as DataTemplate;
			this.SymbolDataTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemSymbolDataTemplate] as DataTemplate;
			this.TextStyleTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemTextStyleDataTemplate] as DataTemplate;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
			=> item switch {
				ColorBarGalleryItemViewModel vm => vm.LayoutBehavior == BarGalleryItemLayoutBehavior.MenuItem ? this.ColorMenuItemTemplate : this.ColorTemplate,
				FontFamilyBarGalleryItemViewModel _ => this.FontFamilyTemplate,
				FontSizeBarGalleryItemViewModel _ => this.FontSizeTemplate,
				MenuItemBarGalleryItemViewModel _ => this.MenuItemTemplate,
				Size _ => this.SizeSelectionTemplate,  // Assuming is for a BarSizeSelectionMenuGallery
				SymbolBarGalleryItemViewModel _ => this.SymbolDataTemplate,
				TextStyleBarGalleryItemViewModel _ => this.TextStyleTemplate,
				_ => base.SelectTemplate(item, container)
			};

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate ColorMenuItemTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate ColorTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="FontFamilyBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate FontFamilyTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="FontSizeBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate FontSizeTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="MenuItemBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate MenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="Size"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate SizeSelectionTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="SymbolBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate SymbolDataTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="TextStyleBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate TextStyleTemplate { get; set; }

	}

}
