using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Windows;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.ComboBoxIntro {

	/// <summary>
	/// Provides the user control for this sample that uses an MVVM-based ribbon configuration.
	/// </summary>
	public partial class SampleMvvmControl : SampleControlBase {

		private BarTextBoxViewModel textBoxViewModel;

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
		/// Initializes the view models for the MVVM-based ribbon.
		/// </summary>
		private void InitializeRibbonViewModels() {
			//
			// Configure Individual Controls
			//

			// Textbox
			textBoxViewModel = new BarTextBoxViewModel("Textbox") {
				Command = this.TextBoxCommitCommand,
				Description = "A textbox control that commits changed text on Enter or focus loss.",
				RequestedWidth = 120,
				Text = "A text value",
			};

			//
			// Configure Ribbon
			//

			Ribbon = new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				Tabs = {
					new RibbonTabViewModel("MvvmSamples", "MVVM Samples") {
						Groups = {

							new RibbonGroupViewModel("BasicSamples") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {

											// Editable
											new BarComboBoxViewModel("Editable") {
												Description = "A basic, editable combobox sample.",
												TextPath = nameof(SimpleComboBoxGalleryItem.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedTextCommand,
												MenuItems = {
													new BarGalleryViewModel("EditableGallery", this.ComboBoxGalleryCommand, this.ComboBoxPersonItems) {
														ItemTemplate = (DataTemplate)FindResource(LocalResourceKeys.PersonComboBoxGalleryItemTemplate)
													}
												}
											},

											// Read-Only
											new BarComboBoxViewModel("ReadOnly") {
												Description = "A basic, editable and read-only combobox sample.",
												IsReadOnly = true,
												TextPath = nameof(SimpleComboBoxGalleryItem.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedTextCommand,
												MenuItems = {
													new BarGalleryViewModel("EditableGallery", this.ComboBoxGalleryCommand, this.ComboBoxPersonItems) {
														ItemTemplate = (DataTemplate)FindResource(LocalResourceKeys.PersonComboBoxGalleryItemTemplate)
													}
												}
											},

											// Non-Editable
											new BarComboBoxViewModel("NonEditable", "Non-Editable") {
												Description = "A basic, non-editable combobox sample.",
												IsEditable = false,
												TextPath = nameof(SimpleComboBoxGalleryItem.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedTextCommand,
												MenuItems = {
													new BarGalleryViewModel("NonEditableGallery", this.ComboBoxGalleryCommand, this.ComboBoxPersonItems) {
														ItemTemplate = (DataTemplate)FindResource(LocalResourceKeys.PersonComboBoxGalleryItemTemplate)
													}
												}
											},

										}
									}
								}
							},

							new RibbonGroupViewModel("CategorizedSamples") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {

											// Categorized items in a single column
											new BarComboBoxViewModel("CategorizedSingleColumn", "Single-Column") {
												Description = "A combobox with items categorized and displayed in a single column.",
												TextPath = nameof(SimpleComboBoxGalleryItem.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedTextCommand,
												MenuItems = {
													new BarGalleryViewModel("CategorizedSingleColumnGallery", this.ComboBoxGalleryCommand, this.ComboBoxPersonItems) {
														CanCategorize = true,
														MaxMenuColumnCount = 1,
														ItemTemplate = (DataTemplate)FindResource(LocalResourceKeys.PersonComboBoxGalleryItemTemplate)
													}
												}
											},

											// Categorized items in multiple columns
											new BarComboBoxViewModel("CategorizedMultiColumn", "Multi-Column") {
												Description = "A combobox with items categorized and displayed using multiple columns.",
												TextPath = nameof(SimpleComboBoxGalleryItem.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedNumberTextCommand,
												MenuItems = {
													new BarGalleryViewModel("CategorizedMultiColumnGallery", this.ComboBoxGalleryCommand, this.ComboBoxNumberItems) {
														CanCategorize = true,
														MinMenuColumnCount = 5,
														MaxMenuColumnCount = 5,
														ItemTemplate = (DataTemplate)FindResource(LocalResourceKeys.NumberComboBoxGalleryItemTemplate)
													}
												}
											},

											// Categorized/Filtered items with menu item appearance consistent with large menu items
											new BarComboBoxViewModel("MenuStyle") {
												Description = "A combobox using a menu-like appearance for items, filtering, and an additional menu item below the list of combobox items.",
												TextPath = nameof(SimpleComboBoxGalleryItem.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedTextCommand,
												MenuItems = {
													new BarGalleryViewModel("MenuStyleGallery", this.ComboBoxGalleryCommand, this.ComboBoxColorItems) {
														CanCategorize = true,
														CanFilter = true,
														MaxMenuColumnCount = 1,
														ItemTemplate = (DataTemplate)FindResource(LocalResourceKeys.LargeMenuComboBoxGalleryItemTemplate)
													},
													new BarButtonViewModel("MoreColors", "More colors...") {
														Command = this.NotImplementedCommand,
													}
												}
											},

										}
									}
								}
							},

							new RibbonGroupViewModel("FontSamples") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {

											// Font family
											new BarComboBoxViewModel("FontFamily") {
												Description = "A combobox with system fonts and a category for recently-used fonts.",
												RequestedWidth = 120,
												TextPath = nameof(FontFamilyBarGalleryItemViewModel.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedTextCommand,
												MenuItems = {
													new BarGalleryViewModel("FontFamilyGallery", this.ComboBoxGalleryCommand, this.ComboBoxFontFamilyItems) {
														CanCategorize = true,
														ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
														MaxMenuColumnCount = 1,
														MenuResizeMode = Windows.Controls.ControlResizeMode.Vertical,
													}
												}
											},

											// Font size
											new BarComboBoxViewModel("FontSize") {
												Description = "A combobox with common font sizes.",
												RequestedWidth = 45,
												TextPath = nameof(FontSizeBarGalleryItemViewModel.Label),
												UnmatchedTextCommand = this.ComboBoxUnmatchedNumberTextCommand,
												MenuItems = {
													new BarGalleryViewModel("FontSizeGallery", this.ComboBoxGalleryCommand, this.ComboBoxFontSizeItems) {
														ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
														MaxMenuColumnCount = 1,
														MenuResizeMode = Windows.Controls.ControlResizeMode.Vertical,
													}
												}
											},

										}
									}
								}
							},

							new RibbonGroupViewModel("OtherSamples") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {

											// Textbox (using previously created view model)
											textBoxViewModel,

										}
									}
								}
							},
						},
					},
				},
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override string GetTextBoxCommitCommandText(object commandParameter) {
			// In the MVVM sample the text is accessed directly from the BarTextBoxViewModel
			// associated with the command since a single command would not typically be
			// associated with more than one BarTextBoxViewModel
			return textBoxViewModel?.Text;
		}


		/// <summary>
		/// Gets the view model for the Ribbon control.
		/// </summary>
		/// <value>A <see cref="RibbonViewModel"/>.</value>
		public RibbonViewModel Ribbon { get; private set; }

	}

}
