using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : SampleControlBase {

		private const string MenuItemColorPickerGalleryKey = "MenuItemColorPickerGallery";

		private bool						areViewModelsInitialized;
		private BarPopupButtonViewModel		customLayoutFontColorPickerViewModel;
		private BarPopupButtonViewModel		customStyleFontColorPickerViewModel;
		private BarPopupButtonViewModel		fontColorPickerWithAutomaticViewModel;
		private BarPopupButtonViewModel		largerSwatchesFontColorPickerViewModel;
		private BarPopupButtonViewModel		menuItemColorPickerViewModel;
		private BarPopupButtonViewModel		textHighlightColorPickerViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleMvvmControl"/> class.
		/// </summary>
		public SampleMvvmControl() {
			InitializeComponent();

			// NOTE: Several view models are configured based on the current options and initialization
			// is delayed until the Options property is populated

			// Configure this code-behind to be the view model for this sample
			this.DataContext = this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets all the sample <see cref="BarGalleryViewModel"/> instances.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="BarGalleryViewModel"/>.</returns>
		private IEnumerable<BarGalleryViewModel> GetBarGalleryViewModels() => GetFontColorPickerBarGalleryViewModels().Concat(GetTextHighlightColorPickerBarGalleryViewModels());

		/// <summary>
		/// Gets all the sample <see cref="BarGalleryViewModel"/> instances for the given popup buttons.
		/// </summary>
		/// <param name="popupButtons">The popup buttons whose gallery view models should be included.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="BarGalleryViewModel"/>.</returns>
		private static IEnumerable<BarGalleryViewModel> GetBarGalleryViewModels(IEnumerable<BarPopupButtonViewModel> popupButtons) {
			if (popupButtons != null) {
				foreach (var popupButtonViewModel in popupButtons) {
					if (popupButtonViewModel.MenuItems != null) {
						foreach (var galleryViewModel in popupButtonViewModel.MenuItems.OfType<BarGalleryViewModel>())
							yield return galleryViewModel;
					}
				}
			}
		}

		/// <summary>
		/// Gets all the sample <see cref="BarGalleryViewModel"/> instances configured as a font color picker.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="BarGalleryViewModel"/>.</returns>
		private IEnumerable<BarGalleryViewModel> GetFontColorPickerBarGalleryViewModels() => GetBarGalleryViewModels(GetFontColorPickerBarPopupButtonViewModels());

		/// <summary>
		/// Gets all the sample <see cref="BarPopupButtonViewModel"/> instances configured as a font color picker.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="BarPopupButtonViewModel"/>.</returns>
		private IEnumerable<BarPopupButtonViewModel> GetFontColorPickerBarPopupButtonViewModels() {
			if (customLayoutFontColorPickerViewModel != null)
				yield return customLayoutFontColorPickerViewModel;
			if (customStyleFontColorPickerViewModel != null)
				yield return customStyleFontColorPickerViewModel;
			if (fontColorPickerWithAutomaticViewModel != null)
				yield return fontColorPickerWithAutomaticViewModel;
			if (largerSwatchesFontColorPickerViewModel != null)
				yield return largerSwatchesFontColorPickerViewModel;
			if (menuItemColorPickerViewModel != null)
				yield return menuItemColorPickerViewModel;
		}

		/// <summary>
		/// Gets all the sample <see cref="BarGalleryViewModel"/> instances configured as a text highlight color picker.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="BarGalleryViewModel"/>.</returns>
		private IEnumerable<BarGalleryViewModel> GetTextHighlightColorPickerBarGalleryViewModels() => GetBarGalleryViewModels(GetTextHighlightColorPickerBarPopupButtonViewModels());

		/// <summary>
		/// Gets all the sample <see cref="BarPopupButtonViewModel"/> instances configured as a text highlight color picker.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> of type <see cref="BarPopupButtonViewModel"/>.</returns>
		private IEnumerable<BarPopupButtonViewModel> GetTextHighlightColorPickerBarPopupButtonViewModels() {
			if (textHighlightColorPickerViewModel != null)
				yield return textHighlightColorPickerViewModel;
		}

		/// <summary>
		/// Initializes the view models for the MVVM-based ribbon.
		/// </summary>
		private void InitializeRibbonViewModels() {
			// The BarGalleryItemTemplateSelector class in the MVVM library is a default implementation of DataTemplateSelector that
			// assigns the most appropriate DataTemplate for each BarGalleryItem. Specifically for this sample, that includes a
			// DataTemplate for ColorBarGalleryItemViewModel used for Font Color, and ColorBarGalleryItemViewModel
			// used for Text Highlight. Use the BarGalleryViewModel.ItemTemplate property to explicitly set the DateTemplate to be
			// used instead of the one defined by the DataTemplateSelector.
			var galleryItemTemplateSelector = new BarGalleryItemTemplateSelector();

			//
			// Define 'Font' and 'Text Highlight' color pickers
			//

			// Font Color with Automatic picker 
			fontColorPickerWithAutomaticViewModel = new BarPopupButtonViewModel("FontColorPickerWithAutomatic", "Font Color w/Automatic") {
				SmallImageSource = FontColorSmallImageSource,
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
				MenuItems = {
					new BarGalleryViewModel("FontColorPickerGalleryWithAutomatic", "Font Color w/Automatic", this.SetFontColorCommand, FontColorItemsWithAutomatic) {
						AreSurroundingSeparatorsAllowed = this.Options.AreSurroundingSeparatorsAllowed,
						CanCategorize = this.Options.FontColorCanCategorize,
						CanFilter = this.Options.FontColorCanFilter,
						ItemSpacing = this.Options.ItemSpacing,
						ItemTemplateSelector = galleryItemTemplateSelector,
						MaxMenuColumnCount = FontColorItemsColumnCount,
						MinMenuColumnCount = FontColorItemsColumnCount,
						UseAccentedItemBorder = this.Options.UseAccentedItemBorder,
						UseMenuItemIndent = this.Options.UseMenuItemIndent,
					},
					new BarButtonViewModel("MoreColors", "More Colors...", MoreColorsCommand) { SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png") }
				}
			};

			// Text highlight picker
			textHighlightColorPickerViewModel = new BarPopupButtonViewModel("TextHighlightColorPicker", "Highlight Color") {
				SmallImageSource = TextHighlightColorSmallImageSource,
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
				MenuItems = {
					new BarGalleryViewModel("TextHighlightColorPickerGallery", "Highlight Color", this.SetTextHighlightColorCommand, TextHighlightColorItems) {
						AreSurroundingSeparatorsAllowed = this.Options.AreSurroundingSeparatorsAllowed,
						ItemSpacing = this.Options.ItemSpacing,
						ItemTemplateSelector = galleryItemTemplateSelector,
						MaxMenuColumnCount = this.Options.TextHighlightColCount,
						MinMenuColumnCount = this.Options.TextHighlightColCount,
						MinItemHeight = 28,
						MinItemWidth = 28,
						UseAccentedItemBorder = this.Options.UseAccentedItemBorder,
						UseMenuItemIndent = this.Options.UseMenuItemIndent,
					},
					new BarButtonViewModel("StopHighlighting", StopHighlightingCommand)
				}
			};


			//
			// Define Additional Custom Color Pickers
			//

			// Gallery of custom color gallery items that use BarGalleryItemLayoutBehavior to define custom groups
			// and also uses the larger button template
			customLayoutFontColorPickerViewModel = new BarPopupButtonViewModel("CustomLayoutColorPicker", "Custom Layout", "Y") {
				MenuItems = {
					new BarGalleryViewModel("CustomLayoutColorPickerGallery", "Custom Layout", this.SetFontColorCommand, CustomLayoutColorPickerItems) {
						AreSurroundingSeparatorsAllowed = this.Options.AreSurroundingSeparatorsAllowed,
						CanCategorize = this.Options.FontColorCanCategorize,
						CanFilter = this.Options.FontColorCanFilter,
						ItemSpacing = this.Options.ItemSpacing,
						MaxMenuColumnCount = CustomLayoutColorPickerItemsColumnCount,
						MinMenuColumnCount = CustomLayoutColorPickerItemsColumnCount,
						UseAccentedItemBorder = this.Options.UseAccentedItemBorder,
						UseMenuItemIndent = this.Options.UseMenuItemIndent,
						ItemTemplate = this.FindResource("LargerButtonGalleryItemTemplate") as DataTemplate,
					},
				}
			};

			// Gallery of custom color gallery items that define a custom Style and DataTemplate to display each color
			// as a circle instead of the default square
			customStyleFontColorPickerViewModel = new BarPopupButtonViewModel("CustomStyleColorPicker", "Custom Style", "S") {
				MenuItems = {
					new BarGalleryViewModel("CustomStyleColorPickerGallery", "Custom Style", this.SetFontColorCommand, CustomStyleColorPickerItems) {
						AreSurroundingSeparatorsAllowed = this.Options.AreSurroundingSeparatorsAllowed,
						CanCategorize = this.Options.FontColorCanCategorize,
						CanFilter = this.Options.FontColorCanFilter,
						ItemSpacing = this.Options.ItemSpacing,
						MinMenuColumnCount = 7,
						UseAccentedItemBorder = this.Options.UseAccentedItemBorder,
						UseMenuItemIndent = this.Options.UseMenuItemIndent,
						ItemContainerStyle = this.FindResource("BarGalleryItemCircleStyle") as Style,
						ItemTemplate = this.FindResource("CircleStyleGalleryItemItemplate") as DataTemplate,
					},
					new BarButtonViewModel("Customize", "Customize...", MoreColorsCommand),
				}
			};

			// Gallery of the default color gallery items with custom ItemTemplate that defines larger swatches
			largerSwatchesFontColorPickerViewModel = new BarPopupButtonViewModel("LargerSwatchesFontColorPicker", "Larger Swatches") {
				MenuItems = {
					new BarGalleryViewModel("LargerSwatchesFontColorPickerGallery", "Larger Swatches", this.SetFontColorCommand, FontColorItems) {
						AreSurroundingSeparatorsAllowed = this.Options.AreSurroundingSeparatorsAllowed,
						CanCategorize = this.Options.FontColorCanCategorize,
						CanFilter = this.Options.FontColorCanFilter,
						ItemSpacing = this.Options.ItemSpacing,
						MaxMenuColumnCount = FontColorItemsColumnCount,
						MinMenuColumnCount = FontColorItemsColumnCount,
						UseAccentedItemBorder = this.Options.UseAccentedItemBorder,
						UseMenuItemIndent = this.Options.UseMenuItemIndent,
						ItemTemplate = this.FindResource("LargerButtonGalleryItemTemplate") as DataTemplate,
					},
				}
			};

			// Gallery where colors are displayed similar to standard menu items
			menuItemColorPickerViewModel = new BarPopupButtonViewModel("MenuItemColorPicker", "Menu Items") {
				MenuItems = {
					new BarGalleryViewModel(MenuItemColorPickerGalleryKey, "Menu Items", this.SetFontColorCommand, CustomMenuItemColorPickerItems) {
						// Configure the gallery based on current sample options
						AreSurroundingSeparatorsAllowed = this.Options.AreSurroundingSeparatorsAllowed,
						CanCategorize = this.Options.FontColorCanCategorize,
						CanFilter = this.Options.FontColorCanFilter,
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
										Items = { fontColorPickerWithAutomaticViewModel, textHighlightColorPickerViewModel }
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
											largerSwatchesFontColorPickerViewModel,
											menuItemColorPickerViewModel,
											customStyleFontColorPickerViewModel,
											customLayoutFontColorPickerViewModel,
										}
									}
								}
							}
						},
					},
				},
			};

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void NotifyPropertyChanged(string propertyName) {
			base.NotifyPropertyChanged(propertyName);

			// Update the view models
			if (propertyName == nameof(FontColorSmallImageSource)) {
				if (fontColorPickerWithAutomaticViewModel != null)
					fontColorPickerWithAutomaticViewModel.SmallImageSource = this.FontColorSmallImageSource;
			}
			else if (propertyName == nameof(TextHighlightColorSmallImageSource)) {
				if (textHighlightColorPickerViewModel != null)
					textHighlightColorPickerViewModel.SmallImageSource = this.TextHighlightColorSmallImageSource;
			}
		}

		/// <inheritdoc/>
		protected override void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs args) {
			base.OnOptionsPropertyChanged(sender, args);

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
				foreach (var colorGalleryViewModel in GetBarGalleryViewModels(new BarPopupButtonViewModel[] { largerSwatchesFontColorPickerViewModel })) {
					colorGalleryViewModel.Items = this.FontColorItems;
					colorGalleryViewModel.MinMenuColumnCount = colorGalleryViewModel.MaxMenuColumnCount = this.FontColorItemsColumnCount;
				}
				foreach (var colorGalleryViewModel in GetBarGalleryViewModels(new BarPopupButtonViewModel[] { fontColorPickerWithAutomaticViewModel })) {
					colorGalleryViewModel.Items = this.FontColorItemsWithAutomatic;
					colorGalleryViewModel.MinMenuColumnCount = colorGalleryViewModel.MaxMenuColumnCount = this.FontColorItemsColumnCount;
				}
				foreach (var highlightGalleryViewModel in GetBarGalleryViewModels(new BarPopupButtonViewModel[] { textHighlightColorPickerViewModel })) {
					highlightGalleryViewModel.Items = this.TextHighlightColorItems;
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
		protected override void OnOptionsPropertyValueChanged(OptionsViewModel oldValue, OptionsViewModel newValue) {
			base.OnOptionsPropertyValueChanged(oldValue, newValue);

			// Wait to initialize view models until after the Options are defined
			if ((newValue != null) && (!areViewModelsInitialized)) {
				areViewModelsInitialized = true;
				InitializeRibbonViewModels();
			}
		}

		/// <summary>
		/// Gets the view model for the Ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; private set; }

	}

}
