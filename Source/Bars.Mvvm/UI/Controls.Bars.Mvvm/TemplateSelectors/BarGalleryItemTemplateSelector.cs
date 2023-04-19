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
			this.DefaultTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemDefaultDataTemplate] as DataTemplate;
			this.FontFamilyTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemFontFamilyDataTemplate] as DataTemplate;
			this.FontSizeTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemFontSizeDataTemplate] as DataTemplate;
			this.MenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemMenuItemDataTemplate] as DataTemplate;
			this.SizeSelectionTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemSizeSelectionDataTemplate] as DataTemplate;
			this.SymbolDataTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemSymbolDataTemplate] as DataTemplate;
			this.TextStyleTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemTextStyleDataTemplate] as DataTemplate;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the <see cref="BarGalleryBase"/> that contains the gallery item.
		/// </summary>
		/// <param name="container">The container control.</param>
		/// <returns>The <see cref="BarGalleryBase"/> that contains the gallery item.</returns>
		private static BarGalleryBase GetGallery(DependencyObject container) {
			if (container is ContentPresenter presenter)
				return ItemsControl.ItemsControlFromItemContainer(presenter.TemplatedParent) as BarGalleryBase;
			else
				return null;
		}

		/// <summary>
		/// Returns whether the item should prefer menu item appearance, which is only when within a <see cref="BarMenuGallery"/>, 
		/// and either <see cref="BarMenuGallery.UseMenuItemAppearance"/> is set or the item requests it via <see cref="BarGalleryItem.LayoutBehavior"/>.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container control.</param>
		/// <returns>
		/// <c>true</c> if the item should prefer menu item appearance; otherwise, <c>false</c>.
		/// </returns>
		private static bool PrefersMenuItemAppearance(object item, DependencyObject container) {
			var containingGallery = GetGallery(container);
			var prefersMenuItemAppearance = (containingGallery is BarMenuGallery menuGallery)
				&& (
					menuGallery.UseMenuItemAppearance 
					|| ((item is IBarGalleryItemViewModel vm) && (vm.LayoutBehavior == BarGalleryItemLayoutBehavior.MenuItem))
				);

			return prefersMenuItemAppearance;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			return item switch {
				ColorBarGalleryItemViewModel => PrefersMenuItemAppearance(item, container) ? this.ColorMenuItemTemplate : this.ColorTemplate,
				FontFamilyBarGalleryItemViewModel => this.FontFamilyTemplate,
				FontSizeBarGalleryItemViewModel => this.FontSizeTemplate,
				Size => this.SizeSelectionTemplate,  // Assuming is for a BarSizeSelectionMenuGallery
				SymbolBarGalleryItemViewModel => this.SymbolDataTemplate,
				TextStyleBarGalleryItemViewModel => this.TextStyleTemplate,
				IBarGalleryItemViewModel => PrefersMenuItemAppearance(item, container) ? this.MenuItemTemplate : this.DefaultTemplate,
				_ => base.SelectTemplate(item, container)
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/> using a menu item appearance.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate ColorMenuItemTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate ColorTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="DataTemplate"/> to use for an <see cref="IBarGalleryItemViewModel"/>.
		/// </summary>
		/// <value>The <see cref="DataTemplate"/> to use.</value>
		public DataTemplate DefaultTemplate { get; set; }

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
		/// Gets or sets the <see cref="DataTemplate"/> to use for an <see cref="IBarGalleryItemViewModel"/> using a menu item appearance.
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
