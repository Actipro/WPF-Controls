namespace ActiproSoftware.Windows.Themes;

/// <summary>
/// Provides access to the resource keys that identify all reusable resources included in this assembly.
/// </summary>
public static class BarsMvvmResourceKeys {

	// Styles
	private static ComponentResourceKey? _barGalleryItemStyle;
	private static ComponentResourceKey? _barMainMenuStyle;
	private static ComponentResourceKey? _dockableToolBarHostStyle;
	private static ComponentResourceKey? _ribbonStyle;
	private static ComponentResourceKey? _standaloneToolBarStyle;

	// Items container templates
	private static ComponentResourceKey? _barButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barButtonMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barCheckBoxDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barCheckBoxMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barComboBoxDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barComboBoxMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barGalleryDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barGalleryItemDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barHeadingMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barGalleryMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barGalleryOverflowMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barPopupButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barPopupButtonMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barSeparatorDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barSeparatorMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barSizeSelectionGalleryMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barSplitButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barSplitButtonMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barSplitToggleButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barSplitToggleButtonMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _barTextBoxDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barTextBoxMenuItemContainerTemplate;
	private static ComponentResourceKey? _barToggleButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _barToggleButtonMenuItemItemContainerTemplate;
	private static ComponentResourceKey? _dockableToolBarDefaultItemContainerTemplate;
	private static ComponentResourceKey? _miniToolBarDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonApplicationButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonBackstageDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonBackstageHeaderButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonBackstageHeaderSeparatorDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonBackstageTabDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonContextualTabGroupDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonControlGroupDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonFooterDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonGroupDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonGroupLauncherButtonDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonMultiRowControlGroupDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonQuickAccessToolBarDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonTabDefaultItemContainerTemplate;
	private static ComponentResourceKey? _ribbonTabRowToolBarDefaultItemContainerTemplate;

	// Gallery item data templates
	private static ComponentResourceKey? _barGalleryItemColorDataTemplate;
	private static ComponentResourceKey? _barGalleryItemColorMenuItemDataTemplate;
	private static ComponentResourceKey? _barGalleryItemDefaultDataTemplate;
	private static ComponentResourceKey? _barGalleryItemFontFamilyDataTemplate;
	private static ComponentResourceKey? _barGalleryItemFontSizeDataTemplate;
	private static ComponentResourceKey? _barGalleryItemMenuItemDataTemplate;
	private static ComponentResourceKey? _barGalleryItemSizeSelectionDataTemplate;
	private static ComponentResourceKey? _barGalleryItemSymbolDataTemplate;
	private static ComponentResourceKey? _barGalleryItemTextStyleDataTemplate;

	// Ribbon footer content data templates
	private static ComponentResourceKey? _ribbonFooterContentInfoBarDataTemplate;
	private static ComponentResourceKey? _ribbonFooterContentSimpleDataTemplate;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the <see cref="BarsMvvmResourceKeys"/> class.
	/// </summary>
	static BarsMvvmResourceKeys() {
		// Ensure the resources are registered in the app
		ThemeManager.RegisterThemeCatalog(nameof(BarsMvvmThemeCatalog), new BarsMvvmThemeCatalog());
	}

	// --------------------------------------------------------------------------------------------------
	// RESOURCE KEYS (STYLES)
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarGalleryItemStyle
		=> _barGalleryItemStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemStyle));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarMainMenuStyle
		=> _barMainMenuStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarMainMenuStyle));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey DockableToolBarHostStyle
		=> _dockableToolBarHostStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(DockableToolBarHostStyle));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonStyle
		=> _ribbonStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonStyle));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="Style"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey StandaloneToolBarStyle
		=> _standaloneToolBarStyle ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(StandaloneToolBarStyle));

	// --------------------------------------------------------------------------------------------------
	// RESOURCE KEYS (ITEM CONTAINER TEMPLATES)
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarButtonDefaultItemContainerTemplate
		=> _barButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarButtonMenuItemItemContainerTemplate
		=> _barButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarButtonMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarCheckBoxDefaultItemContainerTemplate
		=> _barCheckBoxDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarCheckBoxDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarCheckBoxMenuItemItemContainerTemplate
		=> _barCheckBoxMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarCheckBoxMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarComboBoxDefaultItemContainerTemplate
		=> _barComboBoxDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarComboBoxDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarComboBoxMenuItemItemContainerTemplate
		=> _barComboBoxMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarComboBoxMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarGalleryDefaultItemContainerTemplate
		=> _barGalleryDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarGalleryMenuItemItemContainerTemplate
		=> _barGalleryMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarGalleryOverflowMenuItemItemContainerTemplate
		=> _barGalleryOverflowMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryOverflowMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarGalleryItemDefaultItemContainerTemplate
		=> _barGalleryItemDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarHeadingMenuItemItemContainerTemplate
		=> _barHeadingMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarHeadingMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarPopupButtonDefaultItemContainerTemplate
		=> _barPopupButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarPopupButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarPopupButtonMenuItemItemContainerTemplate
		=> _barPopupButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarPopupButtonMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSeparatorDefaultItemContainerTemplate
		=> _barSeparatorDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSeparatorDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSeparatorMenuItemItemContainerTemplate
		=> _barSeparatorMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSeparatorMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSizeSelectionGalleryMenuItemItemContainerTemplate
		=> _barSizeSelectionGalleryMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSizeSelectionGalleryMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSplitButtonDefaultItemContainerTemplate
		=> _barSplitButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSplitButtonMenuItemItemContainerTemplate
		=> _barSplitButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitButtonMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSplitToggleButtonDefaultItemContainerTemplate
		=> _barSplitToggleButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitToggleButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarSplitToggleButtonMenuItemItemContainerTemplate
		=> _barSplitToggleButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarSplitToggleButtonMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarTextBoxDefaultItemContainerTemplate
		=> _barTextBoxDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarTextBoxDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarTextBoxMenuItemContainerTemplate
		=> _barTextBoxMenuItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarTextBoxMenuItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarToggleButtonDefaultItemContainerTemplate
		=> _barToggleButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarToggleButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey BarToggleButtonMenuItemItemContainerTemplate
		=> _barToggleButtonMenuItemItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarToggleButtonMenuItemItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey DockableToolBarDefaultItemContainerTemplate
		=> _dockableToolBarDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(DockableToolBarDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey MiniToolBarDefaultItemContainerTemplate
		=> _miniToolBarDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(MiniToolBarDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonApplicationButtonDefaultItemContainerTemplate
		=> _ribbonApplicationButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonApplicationButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonBackstageDefaultItemContainerTemplate
		=> _ribbonBackstageDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonBackstageHeaderButtonDefaultItemContainerTemplate
		=> _ribbonBackstageHeaderButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageHeaderButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate
		=> _ribbonBackstageHeaderSeparatorDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonBackstageTabDefaultItemContainerTemplate
		=> _ribbonBackstageTabDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonBackstageTabDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonContextualTabGroupDefaultItemContainerTemplate
		=> _ribbonContextualTabGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonContextualTabGroupDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonControlGroupDefaultItemContainerTemplate
		=> _ribbonControlGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonControlGroupDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonFooterDefaultItemContainerTemplate
		=> _ribbonFooterDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonFooterDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonGroupDefaultItemContainerTemplate
		=> _ribbonGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonGroupDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonGroupLauncherButtonDefaultItemContainerTemplate
		=> _ribbonGroupLauncherButtonDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonGroupLauncherButtonDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonMultiRowControlGroupDefaultItemContainerTemplate
		=> _ribbonMultiRowControlGroupDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonMultiRowControlGroupDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonQuickAccessToolBarDefaultItemContainerTemplate
		=> _ribbonQuickAccessToolBarDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonQuickAccessToolBarDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonTabDefaultItemContainerTemplate
		=> _ribbonTabDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonTabDefaultItemContainerTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="ItemContainerTemplate"/> that may be applied to a bar control.
	/// </summary>
	public static ResourceKey RibbonTabRowToolBarDefaultItemContainerTemplate
		=> _ribbonTabRowToolBarDefaultItemContainerTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonTabRowToolBarDefaultItemContainerTemplate));

	// --------------------------------------------------------------------------------------------------
	// RESOURCE KEYS (GALLERY ITEM DATA TEMPLATES)
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemColorDataTemplate
		=> _barGalleryItemColorDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemColorDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemColorMenuItemDataTemplate
		=> _barGalleryItemColorMenuItemDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemColorMenuItemDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemDefaultDataTemplate
		=> _barGalleryItemDefaultDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemDefaultDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemFontFamilyDataTemplate
		=> _barGalleryItemFontFamilyDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemFontFamilyDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemFontSizeDataTemplate
		=> _barGalleryItemFontSizeDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemFontSizeDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemMenuItemDataTemplate
		=> _barGalleryItemMenuItemDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemMenuItemDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemSizeSelectionDataTemplate
		=> _barGalleryItemSizeSelectionDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemSizeSelectionDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemSymbolDataTemplate
		=> _barGalleryItemSymbolDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemSymbolDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to a gallery item.
	/// </summary>
	public static ResourceKey BarGalleryItemTextStyleDataTemplate
		=> _barGalleryItemTextStyleDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(BarGalleryItemTextStyleDataTemplate));

	// --------------------------------------------------------------------------------------------------
	// RESOURCE KEYS (RIBBON FOOTER CONTENT DATA TEMPLATES)
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to ribbon footer content.
	/// </summary>
	public static ResourceKey RibbonFooterContentInfoBarDataTemplate
		=> _ribbonFooterContentInfoBarDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonFooterContentInfoBarDataTemplate));

	/// <summary>
	/// The <see cref="ResourceKey"/> for an <see cref="DataTemplate"/> that may be applied to ribbon footer content.
	/// </summary>
	public static ResourceKey RibbonFooterContentSimpleDataTemplate
		=> _ribbonFooterContentSimpleDataTemplate ??= new ComponentResourceKey(typeof(BarsMvvmResourceKeys), nameof(RibbonFooterContentSimpleDataTemplate));

}
