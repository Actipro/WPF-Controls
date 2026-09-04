namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Provides a <see cref="DataTemplateSelector"/> that selects item templates for various gallery item view models, 
/// generally used with a <see cref="BarGalleryBase"/>-based control and assigned to its <see cref="ItemsControl.ItemTemplateSelector"/> property.
/// </summary>
public class BarGalleryItemTemplateSelector : DataTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BarGalleryItemTemplateSelector() {
		var dictionary = BarsMvvmResourceDictionary.Instance;

		ColorMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemColorMenuItemDataTemplate] as DataTemplate;
		ColorTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemColorDataTemplate] as DataTemplate;
		DefaultTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemDefaultDataTemplate] as DataTemplate;
		FontFamilyTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemFontFamilyDataTemplate] as DataTemplate;
		FontSizeTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemFontSizeDataTemplate] as DataTemplate;
		MenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemMenuItemDataTemplate] as DataTemplate;
		SizeSelectionTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemSizeSelectionDataTemplate] as DataTemplate;
		SymbolDataTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemSymbolDataTemplate] as DataTemplate;
		TextStyleTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemTextStyleDataTemplate] as DataTemplate;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the <see cref="BarGalleryBase"/> that contains the gallery item.
	/// </summary>
	/// <param name="container">The container control.</param>
	private static BarGalleryBase? GetGallery(DependencyObject container) {
		return (container is ContentPresenter presenter)
			? ItemsControl.ItemsControlFromItemContainer(presenter.TemplatedParent) as BarGalleryBase
			: null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns whether the item should prefer menu item appearance, which is only when within a <see cref="BarMenuGallery"/>, 
	/// and either <see cref="BarMenuGallery.UseMenuItemAppearance"/> is set or the item requests it via <see cref="BarGalleryItem.LayoutBehavior"/>.
	/// </summary>
	/// <param name="item">The item to examine.</param>
	/// <param name="container">The container control.</param>
	/// <returns>
	/// <c>true</c> if the item should prefer menu item appearance; otherwise, <c>false</c>.
	/// </returns>
	protected virtual bool PrefersMenuItemAppearance(object item, DependencyObject container) {
		var containingGallery = GetGallery(container);
		var prefersMenuItemAppearance = (containingGallery is BarMenuGallery menuGallery)
			&& (
				menuGallery.UseMenuItemAppearance
				|| (item is IBarGalleryItemViewModel { LayoutBehavior: BarGalleryItemLayoutBehavior.MenuItem })
			);

		return prefersMenuItemAppearance;
	}

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, DependencyObject container) {
		return item switch {
			ColorBarGalleryItemViewModel => PrefersMenuItemAppearance(item, container) ? ColorMenuItemTemplate : ColorTemplate,
			FontFamilyBarGalleryItemViewModel => FontFamilyTemplate,
			FontSizeBarGalleryItemViewModel => FontSizeTemplate,
			Size => SizeSelectionTemplate,  // Assuming is for a BarSizeSelectionMenuGallery
			SymbolBarGalleryItemViewModel => SymbolDataTemplate,
			TextStyleBarGalleryItemViewModel => TextStyleTemplate,
			IBarGalleryItemViewModel => PrefersMenuItemAppearance(item, container) ? MenuItemTemplate : DefaultTemplate,
			_ => base.SelectTemplate(item, container)
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC DATATEMPLATE PROPERTIES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/> using a menu item appearance.
	/// </summary>
	public DataTemplate? ColorMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? ColorTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for an <see cref="IBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? DefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="FontFamilyBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? FontFamilyTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="FontSizeBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? FontSizeTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for an <see cref="IBarGalleryItemViewModel"/> using a menu item appearance.
	/// </summary>
	public DataTemplate? MenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="Size"/>.
	/// </summary>
	public DataTemplate? SizeSelectionTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="SymbolBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? SymbolDataTemplate { get; set; }

	/// <summary>
	/// The <see cref="DataTemplate"/> to use for a <see cref="TextStyleBarGalleryItemViewModel"/>.
	/// </summary>
	public DataTemplate? TextStyleTemplate { get; set; }

}
