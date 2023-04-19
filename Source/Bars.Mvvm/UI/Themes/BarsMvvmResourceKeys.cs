using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.Windows.Themes {

	/// <summary>
	/// Provides access to the resource keys that identify all reusable resources included in this assembly.
	/// </summary>
	public static class BarsMvvmResourceKeys {

		// Styles
		private static ComponentResourceKey barGalleryItemStyle;
		private static ComponentResourceKey ribbonStyle;
		private static ComponentResourceKey standaloneToolBarStyle;

		// Items container templates
		private static ComponentResourceKey barButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey barButtonMenuItemItemContainerTemplate;
		private static ComponentResourceKey barCheckBoxDefaultItemContainerTemplate;
		private static ComponentResourceKey barCheckBoxMenuItemItemContainerTemplate;
		private static ComponentResourceKey barComboBoxDefaultItemContainerTemplate;
		private static ComponentResourceKey barComboBoxMenuItemItemContainerTemplate;
		private static ComponentResourceKey barGalleryDefaultItemContainerTemplate;
		private static ComponentResourceKey barGalleryItemDefaultItemContainerTemplate;
		private static ComponentResourceKey barHeadingMenuItemItemContainerTemplate;
		private static ComponentResourceKey barGalleryMenuItemItemContainerTemplate;
		private static ComponentResourceKey barGalleryOverflowMenuItemItemContainerTemplate;
		private static ComponentResourceKey barPopupButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey barPopupButtonMenuItemItemContainerTemplate;
		private static ComponentResourceKey barSeparatorDefaultItemContainerTemplate;
		private static ComponentResourceKey barSeparatorMenuItemItemContainerTemplate;
		private static ComponentResourceKey barSizeSelectionGalleryMenuItemItemContainerTemplate;
		private static ComponentResourceKey barSplitButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey barSplitButtonMenuItemItemContainerTemplate;
		private static ComponentResourceKey barSplitToggleButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey barSplitToggleButtonMenuItemItemContainerTemplate;
		private static ComponentResourceKey barTextBoxDefaultItemContainerTemplate;
		private static ComponentResourceKey barTextBoxMenuItemContainerTemplate;
		private static ComponentResourceKey barToggleButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey barToggleButtonMenuItemItemContainerTemplate;
		private static ComponentResourceKey ribbonApplicationButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonBackstageDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonBackstageHeaderButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonBackstageHeaderSeparatorDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonBackstageTabDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonContextualTabGroupDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonControlGroupDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonFooterDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonGroupDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonGroupLauncherButtonDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonQuickAccessToolBarDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonTabDefaultItemContainerTemplate;
		private static ComponentResourceKey ribbonTabRowToolBarDefaultItemContainerTemplate;

		// Gallery item data templates
		private static ComponentResourceKey barGalleryItemColorDataTemplate;
		private static ComponentResourceKey barGalleryItemColorMenuItemDataTemplate;
		private static ComponentResourceKey barGalleryItemDefaultDataTemplate;
		private static ComponentResourceKey barGalleryItemFontFamilyDataTemplate;
		private static ComponentResourceKey barGalleryItemFontSizeDataTemplate;
		private static ComponentResourceKey barGalleryItemMenuItemDataTemplate;
		private static ComponentResourceKey barGalleryItemSizeSelectionDataTemplate;
		private static ComponentResourceKey barGalleryItemSymbolDataTemplate;
		private static ComponentResourceKey barGalleryItemTextStyleDataTemplate;

		// Ribbon footer content data templates
		private static ComponentResourceKey ribbonFooterContentSimpleDataTemplate;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <see cref="BarsMvvmResourceKeys"/> class.
		/// </summary>
		static BarsMvvmResourceKeys() {
			// Ensure the resources are registered in the app
            ThemeManager.RegisterThemeCatalog(nameof(BarsMvvmThemeCatalog), new BarsMvvmThemeCatalog());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// RESOURCE KEYS (STYLES)
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemStyle
			=> (barGalleryItemStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemStyle)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonStyle
			=> (ribbonStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonStyle)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey StandaloneToolBarStyle
			=> (standaloneToolBarStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(StandaloneToolBarStyle)));
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// RESOURCE KEYS (ITEM CONTAINER TEMPLATES)
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarButtonDefaultItemContainerTemplate
			=> (barButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarButtonMenuItemItemContainerTemplate
			=> (barButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarButtonMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarCheckBoxDefaultItemContainerTemplate
			=> (barCheckBoxDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarCheckBoxDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarCheckBoxMenuItemItemContainerTemplate
			=> (barCheckBoxMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarCheckBoxMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarComboBoxDefaultItemContainerTemplate
			=> (barComboBoxDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarComboBoxDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarComboBoxMenuItemItemContainerTemplate
			=> (barComboBoxMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarComboBoxMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryDefaultItemContainerTemplate
			=> (barGalleryDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryMenuItemItemContainerTemplate
			=> (barGalleryMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryOverflowMenuItemItemContainerTemplate
			=> (barGalleryOverflowMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryOverflowMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemDefaultItemContainerTemplate
			=> (barGalleryItemDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemDefaultItemContainerTemplate)));

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarHeadingMenuItemItemContainerTemplate
			=> (barHeadingMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarHeadingMenuItemItemContainerTemplate)));

		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarPopupButtonDefaultItemContainerTemplate
			=> (barPopupButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarPopupButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarPopupButtonMenuItemItemContainerTemplate
			=> (barPopupButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarPopupButtonMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSeparatorDefaultItemContainerTemplate
			=> (barSeparatorDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSeparatorDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSeparatorMenuItemItemContainerTemplate
			=> (barSeparatorMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSeparatorMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSizeSelectionGalleryMenuItemItemContainerTemplate
			=> (barSizeSelectionGalleryMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSizeSelectionGalleryMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSplitButtonDefaultItemContainerTemplate
			=> (barSplitButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSplitButtonMenuItemItemContainerTemplate
			=> (barSplitButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitButtonMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSplitToggleButtonDefaultItemContainerTemplate
			=> (barSplitToggleButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitToggleButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarSplitToggleButtonMenuItemItemContainerTemplate
			=> (barSplitToggleButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitToggleButtonMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarTextBoxDefaultItemContainerTemplate
			=> (barTextBoxDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarTextBoxDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarTextBoxMenuItemContainerTemplate
			=> (barTextBoxMenuItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarTextBoxMenuItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarToggleButtonDefaultItemContainerTemplate
			=> (barToggleButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarToggleButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarToggleButtonMenuItemItemContainerTemplate
			=> (barToggleButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarToggleButtonMenuItemItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonApplicationButtonDefaultItemContainerTemplate
			=> (ribbonApplicationButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonApplicationButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonBackstageDefaultItemContainerTemplate
			=> (ribbonBackstageDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonBackstageHeaderButtonDefaultItemContainerTemplate
			=> (ribbonBackstageHeaderButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageHeaderButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate
			=> (ribbonBackstageHeaderSeparatorDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonBackstageTabDefaultItemContainerTemplate
			=> (ribbonBackstageTabDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageTabDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonContextualTabGroupDefaultItemContainerTemplate
			=> (ribbonContextualTabGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonContextualTabGroupDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonControlGroupDefaultItemContainerTemplate
			=> (ribbonControlGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonControlGroupDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonFooterDefaultItemContainerTemplate
			=> (ribbonFooterDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonFooterDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonGroupDefaultItemContainerTemplate
			=> (ribbonGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonGroupDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonGroupLauncherButtonDefaultItemContainerTemplate
			=> (ribbonGroupLauncherButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonGroupLauncherButtonDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonQuickAccessToolBarDefaultItemContainerTemplate
			=> (ribbonQuickAccessToolBarDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonQuickAccessToolBarDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonTabDefaultItemContainerTemplate
			=> (ribbonTabDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonTabDefaultItemContainerTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonTabRowToolBarDefaultItemContainerTemplate
			=> (ribbonTabRowToolBarDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonTabRowToolBarDefaultItemContainerTemplate)));
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// RESOURCE KEYS (GALLERY ITEM DATA TEMPLATES)
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemColorDataTemplate
			=> (barGalleryItemColorDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemColorDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemColorMenuItemDataTemplate
			=> (barGalleryItemColorMenuItemDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemColorMenuItemDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemDefaultDataTemplate
			=> (barGalleryItemDefaultDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemDefaultDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemFontFamilyDataTemplate
			=> (barGalleryItemFontFamilyDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemFontFamilyDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemFontSizeDataTemplate
			=> (barGalleryItemFontSizeDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemFontSizeDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemMenuItemDataTemplate
			=> (barGalleryItemMenuItemDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemMenuItemDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemSizeSelectionDataTemplate
			=> (barGalleryItemSizeSelectionDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemSizeSelectionDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemSymbolDataTemplate
			=> (barGalleryItemSymbolDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemSymbolDataTemplate)));
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey BarGalleryItemTextStyleDataTemplate
			=> (barGalleryItemTextStyleDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemTextStyleDataTemplate)));
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// RESOURCE KEYS (RIBBON FOOTER CONTENT DATA TEMPLATES)
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to ribbon footer content.
		/// </summary>
		/// <value>A resource key.</value>
		public static ResourceKey RibbonFooterContentSimpleDataTemplate
			=> (ribbonFooterContentSimpleDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonFooterContentSimpleDataTemplate)));
		
	}

}
