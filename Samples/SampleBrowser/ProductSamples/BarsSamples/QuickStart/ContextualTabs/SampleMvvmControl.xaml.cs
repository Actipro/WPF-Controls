using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ContextualTabs;

/// <summary>
/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
/// </summary>
public partial class SampleMvvmControl : SampleControlBase {

	private RibbonContextualTabGroupViewModel? _pictureToolsContextualTabGroup;
	private RibbonContextualTabGroupViewModel? _tableToolsContextualTabGroup;

	private BarToggleButtonViewModel? _togglePictureToolsButton;
	private BarToggleButtonViewModel? _toggleTableToolsButton;

	private ICommand? _togglePictureToolsCommand;
	private ICommand? _toggleTableToolsCommand;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleMvvmControl() {
		InitializeComponent();

		// Initialize the Ribbon view models
		InitializeRibbonViewModels();

		// Configure this code-behind to be the view model for this sample
		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Updates view models with the current sample options.
	/// </summary>
	private void ApplySampleOptionsToViewModels() {
		if (Options is null)
			return;

		// Update active contextual tab groups
		if (_pictureToolsContextualTabGroup is not null)
			_pictureToolsContextualTabGroup.IsActive = Options.IsPictureToolsContextualTabGroupVisible;
		if (_tableToolsContextualTabGroup is not null)
			_tableToolsContextualTabGroup.IsActive = Options.IsTableToolsContextualTabGroupVisible;

		// Update toggle state of buttons
		if (_togglePictureToolsButton is not null)
			_togglePictureToolsButton.IsChecked = Options.IsPictureToolsContextualTabGroupVisible;
		if (_toggleTableToolsButton is not null)
			_toggleTableToolsButton.IsChecked = Options.IsTableToolsContextualTabGroupVisible;
	}

	/// <summary>
	/// Initializes the view models for the MVVM-based ribbon.
	/// </summary>
	private void InitializeRibbonViewModels() {

		//
		// Define Contextual Tab Groups
		//

		// The IsActive property of a RibbonContextualTabGroupViewModel will determine the visibility of all
		//   RibbonTabViewModel instances whose ContextualTabGroupKey matches RibbonContextualTabGroupViewModel.Key
		_pictureToolsContextualTabGroup = new RibbonContextualTabGroupViewModel(ContextualTabGroupKeys.PictureTools) {
			IsActive = Options?.IsPictureToolsContextualTabGroupVisible == true
		};
		_tableToolsContextualTabGroup = new RibbonContextualTabGroupViewModel(ContextualTabGroupKeys.TableTools) {
			IsActive = Options?.IsTableToolsContextualTabGroupVisible == true
		};

		//
		// Define Toggle Buttons
		//

		_togglePictureToolsButton = new BarToggleButtonViewModel("TogglePictureTools", "Picture Tools") {
			Command = TogglePictureToolsCommand,
			Description = "Click to toggle the visibility of the Picture Tools contextual tab group.",
			ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			LargeImageSource = ImageLoader.GetIcon("Picture32.png"),
			SmallImageSource = ImageLoader.GetIcon("Picture16.png"),
		};
		_toggleTableToolsButton = new BarToggleButtonViewModel("ToggleTableTools", "Table Tools") {
			Command = ToggleTableToolsCommand,
			Description = "Click to toggle the visibility of the Table Tools contextual tab group.",
			ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			LargeImageSource = ImageLoader.GetIcon("Table32.png"),
			SmallImageSource = ImageLoader.GetIcon("Table16.png"),
		};

		//
		// Configure Ribbon
		//

		Ribbon = new RibbonViewModel() {
			IsApplicationButtonVisible = false,
			IsCollapsible = false,
			QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
			ContextualTabGroups = {
				_pictureToolsContextualTabGroup,
				_tableToolsContextualTabGroup,
			},
			Tabs = {

				//
				// Standard Tabs (Always Visible)
				//

				new RibbonTabViewModel("MvvmSamples", "MVVM Samples") {
					Groups = {
						new RibbonGroupViewModel("ContextualTabs") {
							Items = {
								_togglePictureToolsButton,
								_toggleTableToolsButton,
							}
						},
					},
				},
				new RibbonTabViewModel("Home") {
					Groups = {
						new RibbonGroupViewModel("Placeholder") {
							Items = {
								new BarButtonViewModel("Placeholder", "Non-Contextual Tab for Demo") {
									LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
									SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
								}
							}
						}
					}
				},

				//
				// Picture Tools Contextual Tabs
				//

				new RibbonTabViewModel("PictureFormat") {
					ContextualTabGroupKey = ContextualTabGroupKeys.PictureTools,
					Groups = {
						new RibbonGroupViewModel("ImageSize") {
							LargeImageSource = ImageLoader.GetIcon("Height32.png"),
							SmallImageSource = ImageLoader.GetIcon("Height16.png"),
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
									Items = {
										new BarTextBoxViewModel("PictureHeight", "Height") { RequestedWidth = 50d, Text = "3.5\"" },
										new BarTextBoxViewModel("PictureWidth", "Width") { RequestedWidth = 50d, Text = "5.0\"" },
									}
								}
							}
						}
					}
				},

				//
				// Table Tools Contextual Tabs
				//

				new RibbonTabViewModel("TableDesign") {
					ContextualTabGroupKey = ContextualTabGroupKeys.TableTools,
					Groups = {
						new RibbonGroupViewModel("TableStyleOptions") {
							LargeImageSource = ImageLoader.GetIcon("Table32.png"),
							SmallImageSource = ImageLoader.GetIcon("Table16.png"),
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
									Items = {
										new BarCheckBoxViewModel("HeaderRow"),
										new BarCheckBoxViewModel("TotalRow"),
										new BarCheckBoxViewModel("BandedRows"),
										new BarCheckBoxViewModel("FirstColumn"),
										new BarCheckBoxViewModel("LastColumn"),
										new BarCheckBoxViewModel("BandedColumns"),
									}
								}
							}
						}
					}
				},
				new RibbonTabViewModel("Layout") {
					ContextualTabGroupKey = ContextualTabGroupKeys.TableTools,
					Groups = {
						new RibbonGroupViewModel("CellSize") {
							LargeImageSource = ImageLoader.GetIcon("Height32.png"),
							SmallImageSource = ImageLoader.GetIcon("Height16.png"),
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
									Items = {
										new BarTextBoxViewModel("TableCellHeight", "Height") { RequestedWidth = 50d, Text = "0.20\"" },
										new BarTextBoxViewModel("TableCellWidth", "Width") { RequestedWidth = 50d, Text = "2.15\"" },
									}
								},
							}
						},
						new RibbonGroupViewModel("Alignment") {
							LargeImageSource = ImageLoader.GetIcon("AlignTextCenter32.png"),
							SmallImageSource = ImageLoader.GetIcon("AlignTextCenter16.png"),
							Items = {
								new RibbonControlGroupViewModel() {
									ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
									Items = {
										new BarToggleButtonViewModel("Left") { SmallImageSource = ImageLoader.GetIcon("AlignTextLeft16.png") },
										new BarToggleButtonViewModel("Center") { SmallImageSource = ImageLoader.GetIcon("AlignTextCenter16.png") },
										new BarToggleButtonViewModel("Right") { SmallImageSource = ImageLoader.GetIcon("AlignTextRight16.png") },
									}
								}
							}
						}
					}
				}

			},
		};

	}

	/// <summary>
	/// The command to toggle the Picture Tools contextual tab group.
	/// </summary>
	private ICommand TogglePictureToolsCommand {
		get => _togglePictureToolsCommand ??= new DelegateCommand<object>(_ => {
			if (Options is { } options)
				options.IsPictureToolsContextualTabGroupVisible = !options.IsPictureToolsContextualTabGroupVisible;
		});
	}

	/// <summary>
	/// The command to toggle the Table Tools contextual tab group.
	/// </summary>
	private ICommand ToggleTableToolsCommand {
		get => _toggleTableToolsCommand = new DelegateCommand<object>(_ => {
			if (Options is { } options)
				options.IsTableToolsContextualTabGroupVisible = !options.IsTableToolsContextualTabGroupVisible;
		});
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnOptionsPropertyChanged(object? sender, PropertyChangedEventArgs args) {
		base.OnOptionsPropertyChanged(sender, args);

		// Update the view models when any individual Options property is changed
		ApplySampleOptionsToViewModels();
	}

	/// <inheritdoc/>
	protected override void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		base.OnOptionsPropertyValueChanged(oldValue, newValue);

		// Update the view models when the Options property is changed
		ApplySampleOptionsToViewModels();
	}

	/// <summary>
	/// The view model for the Ribbon control.
	/// </summary>
	public RibbonViewModel? Ribbon { get; private set; }

}
