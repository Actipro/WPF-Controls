using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.PopupAndContextMenus {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : SampleControlBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleMvvmControl"/> class.
		/// </summary>
		public SampleMvvmControl() {
			InitializeComponent();

			// Initialize the Ribbon view models (used by MVVM samples only)
			InitializeRibbonViewModels();

			// Configure this code-behind to be the view model for this sample
			this.DataContext = this;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the <see cref="BarGalleryViewModel"/> for the "Advanced Paste Options" showcase sample.
		/// </summary>
		/// <returns>A new <see cref="BarGalleryViewModel"/>.</returns>
		private BarGalleryViewModel CreateAdvancedPastOptionsGalleryViewModel() {
			var pasteOptionsGallery = new BarGalleryViewModel("PasteOptions", this.PasteSpecialCommand, this.PasteOptions) {
				AreSurroundingSeparatorsAllowed = false,
				MaxMenuColumnCount = 6,
				IsSelectionSupported = false,
				UseMenuItemIndent = true,

				// Lookup the DataTemplates defined in SampleCommonDictionary.xaml
				CategoryHeaderTemplate = this.FindResource("PasteOptionGalleryCategoryTemplate") as DataTemplate,
				ItemTemplate = this.FindResource("PasteOptionGalleryItemTemplate") as DataTemplate,
			};

			return pasteOptionsGallery;
		}

		/// <summary>
		/// Creates the <see cref="BarPopupButtonViewModel"/> which defines the "Advanced Paste Options" showcase sample.
		/// </summary>
		/// <returns>A new <see cref="BarPopupButtonViewModel"/>.</returns>
		private BarPopupButtonViewModel CreateShowcaseSampleAdvancedPasteOptionsViewModel() {
			return new BarPopupButtonViewModel("AdvancedPasteOptions") {
				Description = "A sample clipboard menu using a gallery to provide multiple paste options.",
				KeyTipText = "P",
				LargeImageSource = ImageLoader.GetIcon("Paste32.png"),
				SmallImageSource = ImageLoader.GetIcon("Paste16.png"),
				UseLargeMenuItem = true,
				MenuItems = {
					new BarButtonViewModel(ApplicationCommands.Cut) { SmallImageSource = ImageLoader.GetIcon("Cut16.png") },
					new BarButtonViewModel(ApplicationCommands.Copy) { SmallImageSource = ImageLoader.GetIcon("Copy16.png") },
					CreateAdvancedPastOptionsGalleryViewModel(),
					new BarButtonViewModel("PasteSpecial", "Paste Special...", "S", this.PasteSpecialCommand),
				},
			};
		}

		/// <summary>
		/// Creates the <see cref="BarPopupButtonViewModel"/> which defines the "View Options with Color Tagging" showcase sample.
		/// </summary>
		/// <returns>A new <see cref="BarPopupButtonViewModel"/>.</returns>
		private BarPopupButtonViewModel CreateShowcaseSampleViewOptionsWithColorTaggingViewModel() {
			var tagColorsGallery = new BarGalleryViewModel("TagColors", this.TagColors) {
				AreSurroundingSeparatorsAllowed = false,
				ItemSpacing = 6,
				MinMenuColumnCount = 7,
				UseAccentedItemBorder = true,
				UseMenuItemIndent = true,

				// Lookup the Style defined in SampleCommonDictionary.xaml
				ItemContainerStyle = this.FindResource("CircularColorSwatchGalleryItemStyle") as Style,
			};

			return new BarPopupButtonViewModel("ViewOptionsWithColorTagging", "View Options with Color Tagging") {
				Description = "A sample 'View' menu that includes a gallery to provide color-based tagging.",
				LargeImageSource = ImageLoader.GetIcon("ColorPicker32.png"),
				SmallImageSource = ImageLoader.GetIcon("ColorPicker16.png"),
				UseLargeMenuItem = true,
				MenuItems = {
					new BarButtonViewModel("UseStacks"),
					new BarPopupButtonViewModel("SortBy") {
						KeyTipText = "B",
						MenuItems = {
							new BarToggleButtonViewModel("Name") { IsChecked = true },
							new BarToggleButtonViewModel("DateModified"),
						}
					},
					new BarButtonViewModel("CleanUpSelections"),
					new BarButtonViewModel("ShowViewOptions") { KeyTipText = "O" },
					new BarSeparatorViewModel(),
					tagColorsGallery,
					new BarButtonViewModel("Tags", "Tags..."),
					new BarSeparatorViewModel(),
					new BarPopupButtonViewModel("Services") {
						KeyTipText = "V",
						MenuItems = {
							new BarButtonViewModel("SampleService"),
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the <see cref="BarPopupButtonViewModel"/> which defines the "Common Menu Controls" technical demo.
		/// </summary>
		/// <returns>A new <see cref="BarPopupButtonViewModel"/>.</returns>
		private BarPopupButtonViewModel CreateTechnicalDemoCommonMenuControlsViewModel() {
			return new BarPopupButtonViewModel("CommonMenuControls") {
				Description = "Common menu controls shown in various states and configurations.",
				LargeImageSource = ImageLoader.GetIcon("Menu32.png"),
				SmallImageSource = ImageLoader.GetIcon("Menu16.png"),
				UseLargeMenuItem = true,
				MenuItems = {

					//
					// Default
					//

					new BarHeadingViewModel("Default"),
					new BarButtonViewModel("DefaultButton") {
						KeyTipText = "D",
						Label = nameof(BarButtonViewModel)
					},
					new BarToggleButtonViewModel("DefaultToggleButton") {
						IsChecked = true,
						KeyTipText = "D",
						Label = nameof(BarToggleButtonViewModel),
					},
					new BarPopupButtonViewModel("DefaultPopupButton") {
						KeyTipText = "D",
						Label = nameof(BarPopupButtonViewModel),
						MenuItems = {
							new BarButtonViewModel("DefaultPopupButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DefaultPopupButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitButtonViewModel("DefaultSplitButton") {
						KeyTipText = "D",
						Label = nameof(BarSplitButtonViewModel),
						MenuItems = {
							new BarButtonViewModel("DefaultSplitButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DefaultSplitButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitToggleButtonViewModel("DefaultSplitToggleButton") {
						KeyTipText = "D",
						Label = nameof(BarSplitToggleButtonViewModel),
						IsChecked = true,
						MenuItems = {
							new BarButtonViewModel("DefaultSplitToggleButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DefaultSplitToggleButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},

					//
					// With Icons
					//

					new BarSeparatorViewModel(),
					new BarHeadingViewModel("With Icons"),
					new BarButtonViewModel("WithIconsButton") {
						KeyTipText = "W",
						Label = nameof(BarButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("New16.png"),
					},
					new BarToggleButtonViewModel("WithIconsToggleButton") {
						IsChecked = true,
						KeyTipText = "W",
						Label = nameof(BarToggleButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("QuickStart16.png")
					},
					new BarPopupButtonViewModel("WithIconsPopupButton") {
						KeyTipText = "W",
						Label = nameof(BarPopupButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("Open16.png"),
						MenuItems = {
							new BarButtonViewModel("WithIconsPopupButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("WithIconsPopupButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitButtonViewModel("WithIconsSplitButton") {
						KeyTipText = "W",
						Label = nameof(BarSplitButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("Save16.png"),
						MenuItems = {
							new BarButtonViewModel("WithIconsSplitButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("WithIconsSplitButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitToggleButtonViewModel("WithIconsSplitToggleButton") {
						KeyTipText = "W",
						Label = nameof(BarSplitToggleButtonViewModel),
						IsChecked = true,
						SmallImageSource = ImageLoader.GetIcon("Print16.png"),
						MenuItems = {
							new BarButtonViewModel("WithIconsSplitToggleButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("WithIconsSplitToggleButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},

					//
					// StaysOpenOnClick
					//

					new BarSeparatorViewModel(),
					new BarHeadingViewModel("StaysOpenOnClick"),
					new BarButtonViewModel("StaysOpenOnClickButton") {
						KeyTipText = "S",
						Label = nameof(BarButtonViewModel),
						StaysOpenOnClick = true
					},
					new BarToggleButtonViewModel("StaysOpenOnClickToggleButton") {
						IsChecked = true,
						KeyTipText = "S",
						Label = nameof(BarToggleButtonViewModel),
						StaysOpenOnClick = true
					},
					new BarSplitButtonViewModel("StaysOpenOnClickSplitButton") {
						KeyTipText = "S",
						Label = nameof(BarSplitButtonViewModel),
						StaysOpenOnClick = true,
						MenuItems = {
							new BarButtonViewModel("StaysOpenOnClickSplitButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("StaysOpenOnClickSplitButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitToggleButtonViewModel("StaysOpenOnClickSplitToggleButton") {
						KeyTipText = "S",
						IsChecked = true,
						Label = nameof(BarSplitToggleButtonViewModel),
						StaysOpenOnClick = true,
						MenuItems = {
							new BarButtonViewModel("StaysOpenOnClickSplitToggleButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("StaysOpenOnClickSplitToggleButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},

					//
					// Disabled
					//
					// NOTE: Command-based view models are disabled when their associated command is disabled,
					//		 so using ApplicationCommands.NotACommand will disable the control since that
					//		 command cannot be executed

					new BarSeparatorViewModel(),
					new BarHeadingViewModel("Disabled"),
					new BarButtonViewModel("DisabledButton", ApplicationCommands.NotACommand) {
						KeyTipText = "A",
						Label = nameof(BarButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("New16.png")
					},
					new BarToggleButtonViewModel("DisabledToggleButton", ApplicationCommands.NotACommand) {
						IsChecked = true,
						KeyTipText = "A",
						Label = nameof(BarToggleButtonViewModel),
					},
					new BarToggleButtonViewModel("DisabledWithIconToggleButton", ApplicationCommands.NotACommand) {
						IsChecked = true,
						KeyTipText = "A",
						Label = nameof(BarToggleButtonViewModel) + " (With Icon)",
						SmallImageSource = ImageLoader.GetIcon("QuickStart16.png")
					},
					new BarPopupButtonViewModel("DisabledPopupButton") {
						Command = ApplicationCommands.NotACommand,
						KeyTipText = "A",
						Label = nameof(BarPopupButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("Open16.png"),
						MenuItems = {
							new BarButtonViewModel("DisabledPopupButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DisabledPopupButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitButtonViewModel("DisabledSplitButton", ApplicationCommands.NotACommand) {
						KeyTipText = "A",
						Label = nameof(BarSplitButtonViewModel),
						SmallImageSource = ImageLoader.GetIcon("Save16.png"),
						MenuItems = {
							new BarButtonViewModel("DisabledSplitButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DisabledSplitButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitToggleButtonViewModel("DisabledSplitToggleButton", ApplicationCommands.NotACommand) {
						IsChecked = true,
						KeyTipText = "A",
						Label = nameof(BarSplitToggleButtonViewModel),
						MenuItems = {
							new BarButtonViewModel("DisabledSplitToggleButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DisabledSplitToggleButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitToggleButtonViewModel("DisabledWithIconSplitToggleButton", ApplicationCommands.NotACommand) {
						IsChecked = true,
						KeyTipText = "A",
						Label = nameof(BarSplitToggleButtonViewModel) + " (With Icon)",
						SmallImageSource = ImageLoader.GetIcon("Print16.png"),
						MenuItems = {
							new BarButtonViewModel("DisabledWithIconSplitToggleButtonChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("DisabledWithIconSplitToggleButtonChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the <see cref="BarPopupButtonViewModel"/> which defines the "Input Gesture Text" technical demo.
		/// </summary>
		/// <returns>A new <see cref="BarPopupButtonViewModel"/>.</returns>
		private BarPopupButtonViewModel CreateTechnicalDemoInputGestureTextViewModel() {
			return new BarPopupButtonViewModel("InputGestureText") {
				Description = "Input gestures are automatically populated, when available, or can be set to display any text.",
				KeyTipText = "G",
				LargeImageSource = ImageLoader.GetIcon("KeyboardShortcut32.png"),
				SmallImageSource = ImageLoader.GetIcon("KeyboardShortcut16.png"),
				UseLargeMenuItem = true,
				MenuItems = {

					//
					// Automatic from KeyGesture
					//

					new BarHeadingViewModel("Automatic from KeyGesture"),
					new BarButtonViewModel(ApplicationCommands.Cut) { SmallImageSource = ImageLoader.GetIcon("Cut16.png") },
					new BarButtonViewModel(ApplicationCommands.Copy) { SmallImageSource = ImageLoader.GetIcon("Copy16.png") },
					new BarButtonViewModel(ApplicationCommands.Paste) { SmallImageSource = ImageLoader.GetIcon("Paste16.png") },
					
					//
					// Explicitly Defined
					//

					new BarSeparatorViewModel(),
					new BarHeadingViewModel("Explicitly Defined"),
					new BarButtonViewModel("Custom") {
						InputGestureText = "Alt+F, N",
						KeyTipText = "U",
						SmallImageSource = ImageLoader.GetIcon("QuickStart16.png")
					},
				}
			};
		}

		/// <summary>
		/// Creates the <see cref="BarPopupButtonViewModel"/> which defines the "Large Size" technical demo.
		/// </summary>
		/// <returns>A new <see cref="BarPopupButtonViewModel"/>.</returns>
		private BarPopupButtonViewModel CreateTechnicalDemoLargeSizeViewModel() {
			return new BarPopupButtonViewModel("LargeSize") {
				Description = "Large-sized menu items (like this one) can be used for emphasis or to add descriptions.",
				LargeImageSource = ImageLoader.GetIcon("Height32.png"),
				SmallImageSource = ImageLoader.GetIcon("Height16.png"),
				UseLargeMenuItem = true,
				MenuItems = {
					//
					// Large Items
					//
					new BarHeadingViewModel("Large Items"),
					new BarPopupButtonViewModel("New") {
						Description = "Create a new document",
						LargeImageSource = ImageLoader.GetIcon("New32.png"),
						SmallImageSource = ImageLoader.GetIcon("New16.png"),
						UseLargeMenuItem = true,
						MenuItems = {
							new BarButtonViewModel("NewChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("NewChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitButtonViewModel("Open") {
						Description = "Open an existing document",
						LargeImageSource = ImageLoader.GetIcon("Open32.png"),
						SmallImageSource = ImageLoader.GetIcon("Open16.png"),
						UseLargeMenuItem = true,
						MenuItems = {
							new BarButtonViewModel("OpenChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("OpenChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarButtonViewModel("Save") {
						Description = "Save the current document",
						LargeImageSource = ImageLoader.GetIcon("Save32.png"),
						SmallImageSource = ImageLoader.GetIcon("Save16.png"),
						UseLargeMenuItem = true,
					},

					//
					// Large Items (No Description)
					//
					new BarSeparatorViewModel(),
					new BarHeadingViewModel("Large Items (No Description)"),
					new BarPopupButtonViewModel("NewNoDesc", "New") {
						LargeImageSource = ImageLoader.GetIcon("New32.png"),
						SmallImageSource = ImageLoader.GetIcon("New16.png"),
						UseLargeMenuItem = true,
						MenuItems = {
							new BarButtonViewModel("NewNoDescChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("NewNoDescChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarSplitButtonViewModel("OpenNoDesc", "Open") {
						LargeImageSource = ImageLoader.GetIcon("Open32.png"),
						SmallImageSource = ImageLoader.GetIcon("Open16.png"),
						UseLargeMenuItem = true,
						MenuItems = {
							new BarButtonViewModel("OpenNoDescChild1") { Label = nameof(BarButtonViewModel) + " (Child 1)" },
							new BarButtonViewModel("OpenNoDescChild2") { Label = nameof(BarButtonViewModel) + " (Child 2)" },
						}
					},
					new BarButtonViewModel("SaveNoDesc", "Save") {
						LargeImageSource = ImageLoader.GetIcon("Save32.png"),
						SmallImageSource = ImageLoader.GetIcon("Save16.png"),
						UseLargeMenuItem = true,
					},

					//
					// Checkable
					//
					new BarSeparatorViewModel(),
					new BarHeadingViewModel("Checkable"),
					new BarToggleButtonViewModel("DefaultCheck") {
						Description = "Checkmark automatically used as image",
						IsChecked = true,
						UseLargeMenuItem = true,
					},
					new BarToggleButtonViewModel("ExplicitImage") {
						Description = "Explicit images also supported",
						IsChecked = true,
						LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
						SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
						UseLargeMenuItem = true,
					},

					//
					// Small Items in Same Menu
					//
					new BarSeparatorViewModel(),
					new BarHeadingViewModel("Small Items in Same Menu"),
					new BarButtonViewModel(ApplicationCommands.Undo) { SmallImageSource = ImageLoader.GetIcon("Undo16.png") },
					new BarButtonViewModel(ApplicationCommands.Redo) { SmallImageSource = ImageLoader.GetIcon("Redo16.png") },
				}
			};
		}

		/// <summary>
		/// Creates the <see cref="BarPopupButtonViewModel"/> which defines the "Vertical Scrolling" technical demo.
		/// </summary>
		/// <returns>A new <see cref="BarPopupButtonViewModel"/>.</returns>
		private BarPopupButtonViewModel CreateTechnicalDemoVerticalScrollingViewModel() {
			var viewModel = new BarPopupButtonViewModel("VerticalScrolling") {
				Description = "Vertical scrolling is fully supported when a menu is too tall.",
				LargeImageSource = ImageLoader.GetIcon("VerticalScroll32.png"),
				SmallImageSource = ImageLoader.GetIcon("VerticalScroll16.png"),
				UseLargeMenuItem = true,
			};

			// Generate a lot of menu items to require scrolling
			bool isFirstGroup = true;
			foreach (var group in new string[] { "A", "B", "C", "D", "E", "F" }) {
				if (isFirstGroup)
					isFirstGroup = false;
				else
					viewModel.MenuItems.Add(new BarSeparatorViewModel());
				viewModel.MenuItems.Add(new BarHeadingViewModel($"Group {group}"));
				for (var i = 0; i <= 9; i++)
					viewModel.MenuItems.Add(
						new BarButtonViewModel($"Button{group}{i}") {
							KeyTipText = $"{group}{i}",
							Label = $"{nameof(BarButtonViewModel)} {i}"
						});
			}

			return viewModel;
		}

		/// <summary>
		/// Initializes the view models for the MVVM-based ribbon.
		/// </summary>
		private void InitializeRibbonViewModels() {

			//
			// Configure Ribbon
			//

			Ribbon = new RibbonViewModel(RibbonLayoutMode.Simplified) {
				IsApplicationButtonVisible = false,
				IsCollapsible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				ItemContainerTemplateSelector = (BarControlTemplateSelector)FindResource("SampleItemContainerTemplateSelector"),
				Tabs = {
					new RibbonTabViewModel("MvvmSamples", "MVVM Samples") {
						Groups = {
							new RibbonGroupViewModel("PopupMenuSamples") {
								CanAutoCollapse = false,
								Items = {
									new BarPopupButtonViewModel("TechnicalDemos") {
										Description = "A collection of technical demonstrations to illustrate the features and capabilities of menus.",
										LargeImageSource = ImageLoader.GetIcon("QuickStart32.png"),
										SmallImageSource = ImageLoader.GetIcon("QuickStart16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											CreateTechnicalDemoCommonMenuControlsViewModel(),
											new BarSeparatorViewModel(),
											CreateTechnicalDemoLargeSizeViewModel(),
											new BarSeparatorViewModel(),
											CreateTechnicalDemoInputGestureTextViewModel(),
											new BarSeparatorViewModel(),
											CreateTechnicalDemoVerticalScrollingViewModel(),
										}
									},
									new BarPopupButtonViewModel("SampleShowcase") {
										Description = "A collection of sample menus to showcase how menus might be used in real-world scenarios.",
										LargeImageSource = ImageLoader.GetIcon("QuickStartGreen32.png"),
										SmallImageSource = ImageLoader.GetIcon("QuickStartGreen16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											CreateShowcaseSampleAdvancedPasteOptionsViewModel(),
											new BarSeparatorViewModel(),
											CreateShowcaseSampleViewOptionsWithColorTaggingViewModel(),
										}
									},
									new BarPopupButtonViewModel("DialogPopup") {
										Description = "A dialog-style popup is displayed as a menu control.",
										LargeImageSource = ImageLoader.GetIcon("DialogWindow32.png"),
										SmallImageSource = ImageLoader.GetIcon("DialogWindow16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											// Custom view model associated with the popup view
											new MenuPopupViewModel(),
										}
									},
								}
							},
						},
					},
				},
			};

			//
			// Configure TextBox Context Menu
			//

			ContextMenuItems = new ObservableCollection<object>() {
				new BarButtonViewModel(ApplicationCommands.Undo) { SmallImageSource = ImageLoader.GetIcon("Undo16.png") },
				new BarButtonViewModel(ApplicationCommands.Redo) { SmallImageSource = ImageLoader.GetIcon("Redo16.png") },
				new BarSeparatorViewModel(),
				new BarButtonViewModel(ApplicationCommands.Cut) { SmallImageSource = ImageLoader.GetIcon("Cut16.png") },
				new BarButtonViewModel(ApplicationCommands.Copy) { SmallImageSource = ImageLoader.GetIcon("Copy16.png") },
				CreateAdvancedPastOptionsGalleryViewModel(),
				new BarSeparatorViewModel(),
				new BarButtonViewModel(ApplicationCommands.SelectAll),
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the collection of menu item view models to be displayed in the context menu.
		/// </summary>
		/// <value>An <see cref="ObservableCollection{T}"/> of type <see cref="object"/>.</value>
		public ObservableCollection<object> ContextMenuItems { get; private set; }

		/// <summary>
		/// Gets the view model for the Ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; private set; }

	}

}
