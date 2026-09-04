using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryInRibbon;

/// <summary>
/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
/// </summary>
public partial class SampleMvvmControl : SampleControlBase {

	private BarGalleryViewModel? _colorPickerGalleryViewModel;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleMvvmControl() {
		InitializeComponent();

		// Initialize the Ribbon view models (used by MVVM samples only)
		InitializeRibbonViewModels();

		// Configure this code-behind to be the view model for this sample
		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update properties on the gallery view model to match the current sample options.
	/// </summary>
	/// <param name="viewModel">The gallery to update.</param>
	private void ApplySampleOptionsToGallery(BarGalleryViewModel? viewModel) {
		if ((viewModel is not null) && (Options is { } options)) {
			// General properties
			viewModel.ItemSpacing = options.ItemSpacing;
			viewModel.ItemTemplate = options.ItemTemplate;
			viewModel.UseAccentedItemBorder = options.UseAccentedItemBorder;

			// Ribbon-specific properties
			viewModel.MinMediumRibbonColumnCount = options.MinMediumRibbonColumnCount;
			viewModel.MinLargeRibbonColumnCount = options.MinLargeRibbonColumnCount;
			viewModel.MaxRibbonColumnCount = options.MaxRibbonColumnCount;

			// Menu-specific properties
			viewModel.CanCategorize = options.CanCategorizeOnMenu;
			viewModel.CanFilter = options.CanFilterOnMenu;
			viewModel.MenuResizeMode = options.MenuResizeMode;
			viewModel.MinMenuColumnCount = options.MinMenuColumnCount;
			viewModel.MaxMenuColumnCount = options.MaxMenuColumnCount;
		}
	}

	/// <summary>
	/// Initializes the view models for the MVVM-based ribbon.
	/// </summary>
	private void InitializeRibbonViewModels() {

		//
		// Define Buttons
		//

		var configureOneRowLayoutButtonViewModel = new BarButtonViewModel("OneRowLayout", "1-Row Layout", ConfigureOneRowLayoutCommand) {
			Description = "Configures the gallery with a layout that will typically display as 1 row.",
			LargeImageSource = OneRowLayoutLargeImageSource,
			SmallImageSource = OneRowLayoutSmallImageSource
		};
		var configureTwoRowLayoutButtonViewModel = new BarButtonViewModel("TwoRowLayout", "2-Row Layout", ConfigureTwoRowLayoutCommand) {
			Description = "Configures the gallery with a layout that will typically display as 2 rows.",
			LargeImageSource = TwoRowLayoutLargeImageSource,
			SmallImageSource = TwoRowLayoutSmallImageSource
		};
		var configureThreeRowLayoutButtonViewModel = new BarButtonViewModel("ThreeRowLayout", "3-Row Layout", ConfigureThreeRowLayoutCommand) {
			Description = "Configures the gallery with a layout that will typically display as 3 rows.",
			LargeImageSource = ThreeRowLayoutLargeImageSource,
			SmallImageSource = ThreeRowLayoutSmallImageSource
		};

		//
		// Define Gallery
		//

		_colorPickerGalleryViewModel = new BarGalleryViewModel("ColorPicker", "Color Picker", SetColorCommand, ColorItems) {
			KeyTipText = "C",
			LargeImageSource = ImageLoader.GetIcon("ColorPicker32.png"),
			SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png"),
		};
		ApplySampleOptionsToGallery(_colorPickerGalleryViewModel);
		_colorPickerGalleryViewModel.BelowMenuItems.Add(configureOneRowLayoutButtonViewModel);
		_colorPickerGalleryViewModel.BelowMenuItems.Add(configureTwoRowLayoutButtonViewModel);
		_colorPickerGalleryViewModel.BelowMenuItems.Add(configureThreeRowLayoutButtonViewModel);

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
						new RibbonGroupViewModel("RibbonGallery") {
							SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png"),
							Items = {
								_colorPickerGalleryViewModel
							}
						},
						new RibbonGroupViewModel("Layout") {
							CanAutoCollapse = false,
							Items= {
								new RibbonControlGroupViewModel() {
									Items = {
										configureOneRowLayoutButtonViewModel,
										configureTwoRowLayoutButtonViewModel,
										configureThreeRowLayoutButtonViewModel
									}
								},
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
	protected override void OnOptionsPropertyChanged(object? sender, PropertyChangedEventArgs args) {
		base.OnOptionsPropertyChanged(sender, args);

		// Update the Gallery when any individual Options property is changed
		ApplySampleOptionsToGallery(_colorPickerGalleryViewModel);
	}

	/// <inheritdoc/>
	protected override void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		base.OnOptionsPropertyValueChanged(oldValue, newValue);

		// Update the Gallery when the Options property is changed
		ApplySampleOptionsToGallery(_colorPickerGalleryViewModel);
	}

	/// <summary>
	/// The view model for the Ribbon control.
	/// </summary>
	public RibbonViewModel? Ribbon { get; private set; }

}
