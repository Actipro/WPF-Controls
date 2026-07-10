using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers;

/// <summary>
/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
/// </summary>
public partial class SampleMvvmControl : SampleControlBase {

	private const string MenuItemColorPickerGalleryKey = "MenuItemColorPickerGallery";

	private bool _areViewModelsInitialized;
	private BarPopupButtonViewModel? _customLayoutFontColorPickerViewModel;
	private BarPopupButtonViewModel? _customStyleFontColorPickerViewModel;
	private BarPopupButtonViewModel? _fontColorPickerWithAutomaticViewModel;
	private BarPopupButtonViewModel? _largerSwatchesFontColorPickerViewModel;
	private BarPopupButtonViewModel? _menuItemColorPickerViewModel;
	private BarPopupButtonViewModel? _textHighlightColorPickerViewModel;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleMvvmControl() {
		InitializeComponent();

		// NOTE: Several view models are configured based on the current options and initialization
		//   is delayed until the Options property is populated

		// Configure this code-behind to be the view model for this sample
		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns all the sample <see cref="BarGalleryViewModel"/> instances.
	/// </summary>
	private IEnumerable<BarGalleryViewModel> GetBarGalleryViewModels()
		=> GetFontColorPickerBarGalleryViewModels().Concat(GetTextHighlightColorPickerBarGalleryViewModels());

	/// <summary>
	/// Gets all the sample <see cref="BarGalleryViewModel"/> instances for the given popup buttons.
	/// </summary>
	/// <param name="popupButtons">The popup buttons whose gallery view models should be included.</param>
	private static IEnumerable<BarGalleryViewModel> GetBarGalleryViewModels(IEnumerable<BarPopupButtonViewModel?> popupButtons) {
		if (popupButtons is not null) {
			foreach (var popupButtonViewModel in popupButtons) {
				if (popupButtonViewModel?.MenuItems is { } menuItems) {
					foreach (var galleryViewModel in menuItems.OfType<BarGalleryViewModel>())
						yield return galleryViewModel;
				}
			}
		}
	}

	/// <summary>
	/// Returns all the sample <see cref="BarGalleryViewModel"/> instances configured as a font color picker.
	/// </summary>
	private IEnumerable<BarGalleryViewModel> GetFontColorPickerBarGalleryViewModels()
		=> GetBarGalleryViewModels(GetFontColorPickerBarPopupButtonViewModels());

	/// <summary>
	/// Returns all the sample <see cref="BarPopupButtonViewModel"/> instances configured as a font color picker.
	/// </summary>
	private IEnumerable<BarPopupButtonViewModel> GetFontColorPickerBarPopupButtonViewModels() {
		if (_customLayoutFontColorPickerViewModel is not null)
			yield return _customLayoutFontColorPickerViewModel;
		if (_customStyleFontColorPickerViewModel is not null)
			yield return _customStyleFontColorPickerViewModel;
		if (_fontColorPickerWithAutomaticViewModel is not null)
			yield return _fontColorPickerWithAutomaticViewModel;
		if (_largerSwatchesFontColorPickerViewModel is not null)
			yield return _largerSwatchesFontColorPickerViewModel;
		if (_menuItemColorPickerViewModel is not null)
			yield return _menuItemColorPickerViewModel;
	}

	/// <summary>
	/// Returns all the sample <see cref="BarGalleryViewModel"/> instances configured as a text highlight color picker.
	/// </summary>
	private IEnumerable<BarGalleryViewModel> GetTextHighlightColorPickerBarGalleryViewModels()
		=> GetBarGalleryViewModels(GetTextHighlightColorPickerBarPopupButtonViewModels());

	/// <summary>
	/// Returns all the sample <see cref="BarPopupButtonViewModel"/> instances configured as a text highlight color picker.
	/// </summary>
	private IEnumerable<BarPopupButtonViewModel> GetTextHighlightColorPickerBarPopupButtonViewModels() {
		if (_textHighlightColorPickerViewModel is not null)
			yield return _textHighlightColorPickerViewModel;
	}

	/// <summary>
	/// Initializes the view models for the MVVM-based ribbon.
	/// </summary>
	private void InitializeRibbonViewModels() {
		// Initialization should be delayed until Options are defined
		if (Options is null)
			return;

		// The BarGalleryItemTemplateSelector class in the MVVM library is a default implementation of DataTemplateSelector that
		//   assigns the most appropriate DataTemplate for each BarGalleryItem. Specifically for this sample, that includes a
		//   DataTemplate for ColorBarGalleryItemViewModel used for Font Color, and ColorBarGalleryItemViewModel
		//   used for Text Highlight. Use the BarGalleryViewModel.ItemTemplate property to explicitly set the DateTemplate to be
		//   used instead of the one defined by the DataTemplateSelector.
		var galleryItemTemplateSelector = new BarGalleryItemTemplateSelector();

		//
		// Define 'Font' and 'Text Highlight' color pickers
		//

		// Font Color with Automatic picker
		_fontColorPickerWithAutomaticViewModel = new BarPopupButtonViewModel("FontColorPickerWithAutomatic", "Font Color w/Automatic") {
			SmallImageSource = FontColorSmallImageSource,
			ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			MenuItems = {
				new BarGalleryViewModel("FontColorPickerGalleryWithAutomatic", "Font Color w/Automatic", SetFontColorCommand, FontColorItemsWithAutomatic) {
					AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed,
					CanCategorize = Options.FontColorCanCategorize,
					CanFilter = Options.FontColorCanFilter,
					ItemSpacing = Options.ItemSpacing,
					ItemTemplateSelector = galleryItemTemplateSelector,
					MaxMenuColumnCount = FontColorItemsColumnCount,
					MinMenuColumnCount = FontColorItemsColumnCount,
					UseAccentedItemBorder = Options.UseAccentedItemBorder,
					UseMenuItemIndent = Options.UseMenuItemIndent,
				},
				new BarButtonViewModel("MoreColors", "More Colors...", MoreColorsCommand) { SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png") }
			}
		};

		// Text highlight picker
		_textHighlightColorPickerViewModel = new BarPopupButtonViewModel("TextHighlightColorPicker", "Highlight Color") {
			SmallImageSource = TextHighlightColorSmallImageSource,
			ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			MenuItems = {
				new BarGalleryViewModel("TextHighlightColorPickerGallery", "Highlight Color", SetTextHighlightColorCommand, TextHighlightColorItems) {
					AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed,
					ItemSpacing = Options.ItemSpacing,
					ItemTemplateSelector = galleryItemTemplateSelector,
					MaxMenuColumnCount = Options.TextHighlightColCount,
					MinMenuColumnCount = Options.TextHighlightColCount,
					MinItemHeight = 28,
					MinItemWidth = 28,
					UseAccentedItemBorder = Options.UseAccentedItemBorder,
					UseMenuItemIndent = Options.UseMenuItemIndent,
				},
				new BarButtonViewModel("StopHighlighting", StopHighlightingCommand)
			}
		};

		//
		// Define Additional Custom Color Pickers
		//

		// Gallery of custom color gallery items that use BarGalleryItemLayoutBehavior to define custom groups
		//   and also uses the larger button template
		_customLayoutFontColorPickerViewModel = new BarPopupButtonViewModel("CustomLayoutColorPicker", "Custom Layout", "Y") {
			MenuItems = {
				new BarGalleryViewModel("CustomLayoutColorPickerGallery", "Custom Layout", SetFontColorCommand, CustomLayoutColorPickerItems) {
					AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed,
					CanCategorize = Options.FontColorCanCategorize,
					CanFilter = Options.FontColorCanFilter,
					ItemSpacing = Options.ItemSpacing,
					MaxMenuColumnCount = CustomLayoutColorPickerItemsColumnCount,
					MinMenuColumnCount = CustomLayoutColorPickerItemsColumnCount,
					UseAccentedItemBorder = Options.UseAccentedItemBorder,
					UseMenuItemIndent = Options.UseMenuItemIndent,
					ItemTemplate = FindResource("LargerButtonGalleryItemTemplate") as DataTemplate,
				},
			}
		};

		// Gallery of custom color gallery items that define a custom Style and DataTemplate to display each color
		//   as a circle instead of the default square
		_customStyleFontColorPickerViewModel = new BarPopupButtonViewModel("CustomStyleColorPicker", "Custom Style", "S") {
			MenuItems = {
				new BarGalleryViewModel("CustomStyleColorPickerGallery", "Custom Style", SetFontColorCommand, CustomStyleColorPickerItems) {
					AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed,
					CanCategorize = Options.FontColorCanCategorize,
					CanFilter = Options.FontColorCanFilter,
					ItemSpacing = Options.ItemSpacing,
					MinMenuColumnCount = 7,
					UseAccentedItemBorder = Options.UseAccentedItemBorder,
					UseMenuItemIndent = Options.UseMenuItemIndent,
					ItemContainerStyle = FindResource("BarGalleryItemCircleStyle") as Style,
					ItemTemplate = FindResource("CircleStyleGalleryItemTemplate") as DataTemplate,
				},
				new BarButtonViewModel("Customize", "Customize...", MoreColorsCommand),
			}
		};

		// Gallery of the default color gallery items with custom ItemTemplate that defines larger swatches
		_largerSwatchesFontColorPickerViewModel = new BarPopupButtonViewModel("LargerSwatchesFontColorPicker", "Larger Swatches") {
			MenuItems = {
				new BarGalleryViewModel("LargerSwatchesFontColorPickerGallery", "Larger Swatches", SetFontColorCommand, FontColorItems) {
					AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed,
					CanCategorize = Options.FontColorCanCategorize,
					CanFilter = Options.FontColorCanFilter,
					ItemSpacing = Options.ItemSpacing,
					MaxMenuColumnCount = FontColorItemsColumnCount,
					MinMenuColumnCount = FontColorItemsColumnCount,
					UseAccentedItemBorder = Options.UseAccentedItemBorder,
					UseMenuItemIndent = Options.UseMenuItemIndent,
					ItemTemplate = FindResource("LargerButtonGalleryItemTemplate") as DataTemplate,
				},
			}
		};

		// Gallery where colors are displayed similar to standard menu items
		_menuItemColorPickerViewModel = new BarPopupButtonViewModel("MenuItemColorPicker", "Menu Items") {
			MenuItems = {
				new BarGalleryViewModel(MenuItemColorPickerGalleryKey, "Menu Items", SetFontColorCommand, CustomMenuItemColorPickerItems) {
					// Configure the gallery based on current sample options
					AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed,
					CanCategorize = Options.FontColorCanCategorize,
					CanFilter = Options.FontColorCanFilter,
					ItemTemplateSelector = galleryItemTemplateSelector,

					// The following sample options must be ignored for the gallery to properly display like a menu
					ItemSpacing = 0,
					MaxMenuColumnCount = 1,
					MinMenuColumnCount = 1,
					UseAccentedItemBorder = false,
					UseMenuItemIndent = false,
				},
				new BarButtonViewModel("MoreColors", "More Colors...", MoreColorsCommand) { SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png") }
			}
		};

		//
		// Configure Ribbon
		//

		Ribbon = new RibbonViewModel() {
			IsApplicationButtonVisible = false,
			IsCollapsible = false,
			QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
			Tabs = {
				new RibbonTabViewModel("MvvmSamples", "MVVM Samples") {
					Groups = {
						new RibbonGroupViewModel("CommonColorPickers") {
							SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png"),
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
									Items = { _fontColorPickerWithAutomaticViewModel, _textHighlightColorPickerViewModel }
								}
							}
						},
						new RibbonGroupViewModel("Other") {
							SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png"),
							Items = {
								new BarPopupButtonViewModel("MoreSamples") {
									ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									LargeImageSource = ImageLoader.GetIcon("ColorPicker32.png"),
									SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png"),
									MenuItems = {
										_largerSwatchesFontColorPickerViewModel,
										_menuItemColorPickerViewModel,
										_customStyleFontColorPickerViewModel,
										_customLayoutFontColorPickerViewModel,
									}
								}
							}
						}
					},
				},
			},
		};

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void NotifyPropertyChanged(string? propertyName) {
		base.NotifyPropertyChanged(propertyName);

		// Update the view models
		if (propertyName == nameof(FontColorSmallImageSource)) {
			if (_fontColorPickerWithAutomaticViewModel is not null)
				_fontColorPickerWithAutomaticViewModel.SmallImageSource = FontColorSmallImageSource;
		}
		else if (propertyName == nameof(TextHighlightColorSmallImageSource)) {
			if (_textHighlightColorPickerViewModel is not null)
				_textHighlightColorPickerViewModel.SmallImageSource = TextHighlightColorSmallImageSource;
		}
	}

	/// <inheritdoc/>
	protected override void OnOptionsPropertyChanged(object? sender, PropertyChangedEventArgs args) {
		base.OnOptionsPropertyChanged(sender, args);

		if (Options is null)
			return;

		// Update the view models
		if (args.PropertyName == nameof(OptionsViewModel.AreSurroundingSeparatorsAllowed)) {
			foreach (var viewModel in GetBarGalleryViewModels())
				viewModel.AreSurroundingSeparatorsAllowed = Options.AreSurroundingSeparatorsAllowed;
		}
		else if (args.PropertyName == nameof(OptionsViewModel.FontColorCanCategorize)) {
			foreach (var viewModel in GetFontColorPickerBarGalleryViewModels())
				viewModel.CanCategorize = Options.FontColorCanCategorize;
		}
		else if (args.PropertyName == nameof(OptionsViewModel.FontColorCanFilter)) {
			foreach (var viewModel in GetFontColorPickerBarGalleryViewModels())
				viewModel.CanFilter = Options.FontColorCanFilter;
		}
		else if (args.PropertyName == nameof(OptionsViewModel.ItemSpacing)) {
			foreach (var viewModel in GetBarGalleryViewModels()) {
				// Ignore this sample option on the menu item sample
				if (viewModel.Key == MenuItemColorPickerGalleryKey)
					continue;

				viewModel.ItemSpacing = Options.ItemSpacing;
			}
		}
		else if (args.PropertyName == nameof(OptionsViewModel.TextHighlightColCount)) {
			foreach (var viewModel in GetTextHighlightColorPickerBarGalleryViewModels())
				viewModel.MinMenuColumnCount = viewModel.MaxMenuColumnCount = Options.TextHighlightColCount;
		}
		else if (args.PropertyName == nameof(OptionsViewModel.UseAccentedItemBorder)) {
			foreach (var viewModel in GetBarGalleryViewModels()) {
				// Ignore sample option on the menu item sample
				if (viewModel.Key == MenuItemColorPickerGalleryKey)
					continue;

				viewModel.UseAccentedItemBorder = Options.UseAccentedItemBorder;
			}
		}
		else if (args.PropertyName == nameof(OptionsViewModel.UseCustomColors)) {
			foreach (var colorGalleryViewModel in GetBarGalleryViewModels([_largerSwatchesFontColorPickerViewModel])) {
				colorGalleryViewModel.Items = FontColorItems;
				colorGalleryViewModel.MinMenuColumnCount = colorGalleryViewModel.MaxMenuColumnCount = FontColorItemsColumnCount;
			}
			foreach (var colorGalleryViewModel in GetBarGalleryViewModels([_fontColorPickerWithAutomaticViewModel])) {
				colorGalleryViewModel.Items = FontColorItemsWithAutomatic;
				colorGalleryViewModel.MinMenuColumnCount = colorGalleryViewModel.MaxMenuColumnCount = FontColorItemsColumnCount;
			}
			foreach (var highlightGalleryViewModel in GetBarGalleryViewModels([_textHighlightColorPickerViewModel])) {
				highlightGalleryViewModel.Items = TextHighlightColorItems;
			}
		}
		else if (args.PropertyName == nameof(OptionsViewModel.UseMenuItemIndent)) {
			foreach (var viewModel in GetBarGalleryViewModels()) {
				// Ignore sample option on the menu item sample
				if (viewModel.Key == MenuItemColorPickerGalleryKey)
					continue;

				viewModel.UseMenuItemIndent = Options.UseMenuItemIndent;
			}
		}
	}

	/// <inheritdoc/>
	protected override void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		base.OnOptionsPropertyValueChanged(oldValue, newValue);

		// Wait to initialize view models until after the Options are defined
		if ((newValue is not null) && (!_areViewModelsInitialized)) {
			_areViewModelsInitialized = true;
			InitializeRibbonViewModels();
		}
	}

	/// <summary>
	/// The view model for the Ribbon control.
	/// </summary>
	public RibbonViewModel? Ribbon { get; private set; }

}
