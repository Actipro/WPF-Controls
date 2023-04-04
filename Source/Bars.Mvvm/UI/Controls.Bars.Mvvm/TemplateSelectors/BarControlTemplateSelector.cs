using ActiproSoftware.Windows.Themes;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides an <see cref="ItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
	/// generally assigned to root bar controls, like to <see cref="Ribbon"/>'s <see cref="Ribbon.ItemContainerTemplateSelector"/> property.
	/// </summary>
	public class BarControlTemplateSelector : ItemContainerTemplateSelector {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="BarControlTemplateSelector"/> class.
		/// </summary>
		public BarControlTemplateSelector() {
			var dictionary = BarsMvvmResourceDictionary.Instance;

			this.BarButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarCheckBoxDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarCheckBoxDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarCheckBoxMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarCheckBoxMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarComboBoxDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarComboBoxDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarComboBoxMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarComboBoxMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarGalleryDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarGalleryMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarGalleryOverflowMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryOverflowMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarGalleryItemDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarGalleryItemDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarMenuHeadingMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarHeadingMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarPopupButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarPopupButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarPopupButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarPopupButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarSizeSelectionGalleryMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSizeSelectionGalleryMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarSeparatorDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarSeparatorDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarSeparatorMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSeparatorMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarSplitButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarSplitButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarSplitButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSplitButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarSplitToggleButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarSplitToggleButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarSplitToggleButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarSplitToggleButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.BarTextBoxDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarTextBoxDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarTextBoxMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarTextBoxMenuItemContainerTemplate] as ItemContainerTemplate;
			this.BarToggleButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.BarToggleButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.BarToggleButtonMenuItemTemplate = dictionary[BarsMvvmResourceKeys.BarToggleButtonMenuItemItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonApplicationButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonApplicationButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonBackstageDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonBackstageHeaderButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageHeaderButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonBackstageHeaderSeparatorDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonBackstageTabDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonBackstageTabDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonContextualTabGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonContextualTabGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonControlGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonControlGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonFooterDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonFooterDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonGroupDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonGroupDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonGroupLauncherButtonDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonGroupLauncherButtonDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonQuickAccessToolBarDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonQuickAccessToolBarDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonTabDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonTabDefaultItemContainerTemplate] as ItemContainerTemplate;
			this.RibbonTabRowToolBarDefaultTemplate = dictionary[BarsMvvmResourceKeys.RibbonTabRowToolBarDefaultItemContainerTemplate] as ItemContainerTemplate;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarButtonViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarButtonMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarCheckBoxViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarCheckBoxDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarCheckBoxViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarCheckBoxMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarComboBoxViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarComboBoxDefaultTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarComboBoxViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarComboBoxMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarGalleryDefaultTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryItemViewModelBase"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarGalleryItemDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarGalleryMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarGalleryViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarGalleryOverflowMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarHeadingViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarMenuHeadingMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarPopupButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarPopupButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarPopupButtonViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarPopupButtonMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSizeSelectionMenuGalleryViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSizeSelectionGalleryMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSeparatorViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSeparatorDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSeparatorViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSeparatorMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSplitButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitButtonViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSplitButtonMenuItemTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitToggleButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSplitToggleButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarSplitToggleButtonViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarSplitToggleButtonMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarTextBoxViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarTextBoxDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarTextBoxViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarTextBoxMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarToggleButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarToggleButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="BarToggleButtonViewModel"/> used in a menu item context.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate BarToggleButtonMenuItemTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonApplicationButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonApplicationButtonDefaultTemplate { get; set; }
		
		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonBackstageDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageHeaderButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonBackstageHeaderButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageHeaderSeparatorViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonBackstageHeaderSeparatorDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonBackstageTabViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonBackstageTabDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonContextualTabGroupViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonContextualTabGroupDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonControlGroupViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonControlGroupDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonFooterViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonFooterDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonGroupLauncherButtonViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonGroupLauncherButtonDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonGroupViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonGroupDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonQuickAccessToolBarViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonQuickAccessToolBarDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonTabViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonTabDefaultTemplate { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="ItemContainerTemplate"/> to use for a <see cref="RibbonTabRowToolBarViewModel"/>.
		/// </summary>
		/// <value>The <see cref="ItemContainerTemplate"/> to use.</value>
		public ItemContainerTemplate RibbonTabRowToolBarDefaultTemplate { get; set; }

		/// <inheritdoc/>
		public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl) {
			var isMenuItem = BarControlService.GetIsMenuItemHost(parentItemsControl);
			
			switch (item) {

				// Derived view models must appear first in the switch

				case BarCheckBoxViewModel _:
					return isMenuItem ? this.BarCheckBoxMenuItemTemplate : this.BarCheckBoxDefaultTemplate;
				case BarSplitToggleButtonViewModel _:
					return isMenuItem ? this.BarSplitToggleButtonMenuItemTemplate : this.BarSplitToggleButtonDefaultTemplate;
				case BarSplitButtonViewModel _:
					return isMenuItem ? this.BarSplitButtonMenuItemTemplate : this.BarSplitButtonDefaultTemplate;
				case BarToggleButtonViewModel _:
					return isMenuItem ? this.BarToggleButtonMenuItemTemplate : this.BarToggleButtonDefaultTemplate;

				// Core view models

				case BarButtonViewModel _:
					return isMenuItem ? this.BarButtonMenuItemTemplate : this.BarButtonDefaultTemplate;
				case BarComboBoxViewModel _:
					return isMenuItem ? this.BarComboBoxMenuItemTemplate : this.BarComboBoxDefaultTemplate;
				case BarGalleryItemViewModelBase _:
					return this.BarGalleryItemDefaultTemplate;
				case BarGalleryViewModel _: {
					var isOverflowMenuItem = BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
					return isMenuItem ? (isOverflowMenuItem ? this.BarGalleryOverflowMenuItemTemplate : this.BarGalleryMenuItemTemplate) : this.BarGalleryDefaultTemplate;
				}
				case BarHeadingViewModel _:
					return isMenuItem ? this.BarMenuHeadingMenuItemTemplate : base.SelectTemplate(item, parentItemsControl);
				case BarSizeSelectionMenuGalleryViewModel _:
					return this.BarSizeSelectionGalleryMenuItemTemplate;
				case BarPopupButtonViewModel _:
					return isMenuItem ? this.BarPopupButtonMenuItemTemplate : this.BarPopupButtonDefaultTemplate;
				case BarSeparatorViewModel _:
					return isMenuItem ? this.BarSeparatorMenuItemTemplate : this.BarSeparatorDefaultTemplate;
				case BarTextBoxViewModel _:
					return isMenuItem ? this.BarTextBoxMenuItemTemplate : this.BarTextBoxDefaultTemplate;
				case RibbonApplicationButtonViewModel _:
					return this.RibbonApplicationButtonDefaultTemplate;
				case RibbonBackstageViewModel _:
					return this.RibbonBackstageDefaultTemplate;
				case RibbonBackstageHeaderButtonViewModel _:
					return this.RibbonBackstageHeaderButtonDefaultTemplate;
				case RibbonBackstageHeaderSeparatorViewModel _:
					return this.RibbonBackstageHeaderSeparatorDefaultTemplate;
				case RibbonBackstageTabViewModel _:
					return this.RibbonBackstageTabDefaultTemplate;
				case RibbonContextualTabGroupViewModel _:
					return this.RibbonContextualTabGroupDefaultTemplate;
				case RibbonControlGroupViewModel _:
					return this.RibbonControlGroupDefaultTemplate;
				case RibbonFooterViewModel _:
					return this.RibbonFooterDefaultTemplate;
				case RibbonGroupViewModel _:
					return this.RibbonGroupDefaultTemplate;
				case RibbonGroupLauncherButtonViewModel _:
					return this.RibbonGroupLauncherButtonDefaultTemplate;
				case RibbonQuickAccessToolBarViewModel _:
					return this.RibbonQuickAccessToolBarDefaultTemplate;
				case RibbonTabRowToolBarViewModel _:
					return this.RibbonTabRowToolBarDefaultTemplate;
				case RibbonTabViewModel _:
					return this.RibbonTabDefaultTemplate;
			}

			return base.SelectTemplate(item, parentItemsControl);
		}

	}

}
