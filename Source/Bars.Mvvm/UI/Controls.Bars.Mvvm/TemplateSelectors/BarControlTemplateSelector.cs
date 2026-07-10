namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Provides an <see cref="ItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
/// generally assigned to root bar controls, like to <see cref="Ribbon"/>'s <see cref="Ribbon.ItemContainerTemplateSelector"/> property.
/// </summary>
public class BarControlTemplateSelector : ItemContainerTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BarControlTemplateSelector() {
		var dictionary = BarsMvvmResourceDictionary.Instance;

		BarButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarCheckBoxDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarCheckBoxDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarCheckBoxMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarCheckBoxMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarComboBoxDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarComboBoxDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarGalleryDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarGalleryMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarGalleryOverflowMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryOverflowMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarGalleryItemDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarMenuHeadingMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarHeadingMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarPopupButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarPopupButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarPopupButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarPopupButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarSizeSelectionGalleryMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSizeSelectionGalleryMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarSeparatorDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarSeparatorDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarSeparatorMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSeparatorMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarSplitButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarSplitButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarSplitButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSplitButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarSplitToggleButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarSplitToggleButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarSplitToggleButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSplitToggleButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
		BarTextBoxDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarTextBoxDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarTextBoxMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarTextBoxMenuItemContainerTemplate] as ItemContainerTemplate;
		BarToggleButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarToggleButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		BarToggleButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarToggleButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
		DockableToolBarDefaultTemplate = dictionary[BarsMvvmResourceKeys.DockableToolBarDefaultItemContainerTemplate] as ItemContainerTemplate;
		MiniToolBarDefaultTemplate = dictionary[BarsMvvmResourceKeys.MiniToolBarDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonApplicationButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonApplicationButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonBackstageDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonBackstageHeaderButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageHeaderButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonBackstageHeaderSeparatorDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonBackstageTabDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageTabDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonContextualTabGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonContextualTabGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonControlGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonControlGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonFooterDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonFooterDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonGroupLauncherButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonGroupLauncherButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonMultiRowControlGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonMultiRowControlGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonQuickAccessToolBarDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonQuickAccessToolBarDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonTabDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonTabDefaultItemContainerTemplate] as ItemContainerTemplate;
		RibbonTabRowToolBarDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonTabRowToolBarDefaultItemContainerTemplate] as ItemContainerTemplate;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarButtonViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarCheckBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarCheckBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarCheckBoxViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarCheckBoxMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarComboBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarComboBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarGalleryDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="IBarGalleryItemViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarGalleryItemDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarGalleryMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarGalleryOverflowMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarHeadingViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarMenuHeadingMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarPopupButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarPopupButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarPopupButtonViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarPopupButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSizeSelectionMenuGalleryViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarSizeSelectionGalleryMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSeparatorViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarSeparatorDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSeparatorViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarSeparatorMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarSplitButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitButtonViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarSplitButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitToggleButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarSplitToggleButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitToggleButtonViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarSplitToggleButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarTextBoxViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarTextBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarTextBoxViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarTextBoxMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarToggleButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? BarToggleButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="BarToggleButtonViewModel"/> used in a menu item context.
	/// </summary>
	public ItemContainerTemplate? BarToggleButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="DockableToolBarViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? DockableToolBarDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="MiniToolBarViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? MiniToolBarDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonApplicationButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonApplicationButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonBackstageDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageHeaderButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonBackstageHeaderButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageHeaderSeparatorViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonBackstageHeaderSeparatorDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageTabViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonBackstageTabDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonContextualTabGroupViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonContextualTabGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonControlGroupViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonControlGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonFooterViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonFooterDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonGroupLauncherButtonViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonGroupLauncherButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonGroupViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonMultiRowControlGroupViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonMultiRowControlGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonQuickAccessToolBarViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonQuickAccessToolBarDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonTabViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonTabDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonTabRowToolBarViewModel"/>.
	/// </summary>
	public ItemContainerTemplate? RibbonTabRowToolBarDefaultTemplate { get; set; }

	/// <inheritdoc/>
	public override DataTemplate? SelectTemplate(object item, ItemsControl parentItemsControl) {
		var isMenuItem = BarControlService.GetIsMenuItemHost(parentItemsControl);

		switch (item) {

			// Derived view models must appear first in the switch

			case BarCheckBoxViewModel _:
				return isMenuItem ? BarCheckBoxMenuItemTemplate : BarCheckBoxDefaultTemplate;
			case BarComboBoxViewModel _: {
				var isOverflowMenuItem = BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
				return isMenuItem ? (isOverflowMenuItem ? BarGalleryOverflowMenuItemTemplate : BarGalleryMenuItemTemplate) : BarComboBoxDefaultTemplate;
			}
			case BarSplitToggleButtonViewModel _:
				return isMenuItem ? BarSplitToggleButtonMenuItemTemplate : BarSplitToggleButtonDefaultTemplate;
			case BarSplitButtonViewModel _:
				return isMenuItem ? BarSplitButtonMenuItemTemplate : BarSplitButtonDefaultTemplate;
			case BarToggleButtonViewModel _:
				return isMenuItem ? BarToggleButtonMenuItemTemplate : BarToggleButtonDefaultTemplate;

			// Core view models

			case BarButtonViewModel _:
				return isMenuItem ? BarButtonMenuItemTemplate : BarButtonDefaultTemplate;
			case IBarGalleryItemViewModel _:
				return BarGalleryItemDefaultTemplate;
			case BarGalleryViewModel _: {
				var isOverflowMenuItem = BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
				return isMenuItem ? (isOverflowMenuItem ? BarGalleryOverflowMenuItemTemplate : BarGalleryMenuItemTemplate) : BarGalleryDefaultTemplate;
			}
			case BarHeadingViewModel _:
				return isMenuItem ? BarMenuHeadingMenuItemTemplate : base.SelectTemplate(item, parentItemsControl);
			case BarSizeSelectionMenuGalleryViewModel _:
				return BarSizeSelectionGalleryMenuItemTemplate;
			case BarPopupButtonViewModel _:
				return isMenuItem ? BarPopupButtonMenuItemTemplate : BarPopupButtonDefaultTemplate;
			case BarSeparatorViewModel _:
				return isMenuItem ? BarSeparatorMenuItemTemplate : BarSeparatorDefaultTemplate;
			case BarTextBoxViewModel _:
				return isMenuItem ? BarTextBoxMenuItemTemplate : BarTextBoxDefaultTemplate;
			case DockableToolBarViewModel _:
				return DockableToolBarDefaultTemplate;
			case MiniToolBarViewModel _:
				return MiniToolBarDefaultTemplate;
			case RibbonApplicationButtonViewModel _:
				return RibbonApplicationButtonDefaultTemplate;
			case RibbonBackstageViewModel _:
				return RibbonBackstageDefaultTemplate;
			case RibbonBackstageHeaderButtonViewModel _:
				return RibbonBackstageHeaderButtonDefaultTemplate;
			case RibbonBackstageHeaderSeparatorViewModel _:
				return RibbonBackstageHeaderSeparatorDefaultTemplate;
			case RibbonBackstageTabViewModel _:
				return RibbonBackstageTabDefaultTemplate;
			case RibbonContextualTabGroupViewModel _:
				return RibbonContextualTabGroupDefaultTemplate;
			case RibbonControlGroupViewModel _:
				return RibbonControlGroupDefaultTemplate;
			case RibbonFooterViewModel _:
				return RibbonFooterDefaultTemplate;
			case RibbonGroupViewModel _:
				return RibbonGroupDefaultTemplate;
			case RibbonGroupLauncherButtonViewModel _:
				return RibbonGroupLauncherButtonDefaultTemplate;
			case RibbonMultiRowControlGroupViewModel _:
				return RibbonMultiRowControlGroupDefaultTemplate;
			case RibbonQuickAccessToolBarViewModel _:
				return RibbonQuickAccessToolBarDefaultTemplate;
			case RibbonTabRowToolBarViewModel _:
				return RibbonTabRowToolBarDefaultTemplate;
			case RibbonTabViewModel _:
				return RibbonTabDefaultTemplate;
		}

		return base.SelectTemplate(item, parentItemsControl);
	}

}
