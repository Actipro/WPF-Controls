using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public abstract class SampleControlBase : UserControl, INotifyPropertyChanged {

		// NOTE: Galleries that support categorization must define a CollectionViewSource configured with grouping.
		//		 Otherwise, any IEnumerable can be used as the items source.

		private ColorBarGalleryItemViewModel	automaticColorGalleryItemViewModel;
		private ImageSource						fontColorSmallImageSource;
		private ICommand						moreColorsCommand;
		private ICommand						setFontColorCommand;
		private ICommand						setTextHighlightColorCommand;
		private ICommand						stopHighlightingCommand;
		private ImageSource						textHighlightColorSmallImageSource;

		private CollectionViewSource			customLayoutColorPickerItems;
		private CollectionViewSource			customMenuItemColorPickerItems;
		private CollectionViewSource			customStyleColorPickerItems;

		private CollectionViewSource			defaultFontColorItems;
		private CollectionViewSource			defaultFontColorItemsWithAutomatic;
		private CollectionViewSource			customFontColorItems;
		private CollectionViewSource			customFontColorItemsWithAutomatic;

		#region Dependency Properties

		public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register(nameof(Options), typeof(OptionsViewModel), typeof(SampleControlBase), new PropertyMetadata(null, OnOptionsPropertyValueChanged));

		#endregion Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
		public event PropertyChangedEventHandler PropertyChanged;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SampleControlBase"/> class.
		/// </summary>
		public SampleControlBase() {
			// Initialize the collection of color gallery items (used by both XAML and MVVM samples)
			InitializeColorGalleryItemViewModelCollections();

			// Initialize the Font and Text Highlight colors
			SetFontColor(Colors.Red, suppressMessage: true);
			SetTextHighlightColor(Colors.Yellow, suppressMessage: true);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
		/// </summary>
		/// <value>A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.</value>
		private ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel {
			get {
				if (automaticColorGalleryItemViewModel is null) {
					automaticColorGalleryItemViewModel = new ColorBarGalleryItemViewModel(Colors.Black, category: null, "Automatic") {
						LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
					};
				}
				return automaticColorGalleryItemViewModel;
			}
		}

		/// <summary>
		/// Creates a <see cref="BitmapImage"/> where a color bar is added to a pre-defined image.
		/// </summary>
		/// <param name="color">The color to be used when rendering the color bar.</param>
		/// <param name="fileName">The name of the file resource which defines the base image.</param>
		/// <returns>An <see cref="ImageSource"/> of the composed image.</returns>
		private static ImageSource CreateBitmapImageWithColorBar(Color color, string fileName) {
			// Load the base image
			var imageSource = ImageLoader.GetIcon(fileName);
			if (imageSource != null) {

				// Determine the bounds of the color bar
				var imageHeight = imageSource.Height;
				var imageWidth = imageSource.Width;
				if (imageHeight > 0 && imageWidth > 0) {
					var colorBarHeight = Math.Max(1, imageHeight / 4);
					var colorBarBounds = new Rect(0, imageHeight - colorBarHeight, imageWidth, colorBarHeight);
					if (!colorBarBounds.IsEmpty) {
						// Add the color bar to the image
						imageSource = ImageProvider.GetImageSourceWithColorSwatch(imageSource, colorBarBounds, color);
					}
				}

				return imageSource;
			}

			return null;
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the base colors consistent with the default standard colors.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateFontBaseStandardColorItemsCollection() {
			var category = "Standard Colors";

			return new[] {
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#c00000").ToColor(), category, "Dark Red"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ff0000").ToColor(), category, "Red"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffc000").ToColor(), category, "Orange"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffff00").ToColor(), category, "Yellow"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#92d050").ToColor(), category, "Light Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00b050").ToColor(), category, "Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00b0f0").ToColor(), category, "Light Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#0070c0").ToColor(), category, "Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#002060").ToColor(), category, "Dark Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#7030a0").ToColor(), category, "Purple"),
			};
		}

		/// <summary>
		/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the layout that defines custom colors and groups.
		/// </summary>
		/// <returns>A <see cref="CollectionViewSource"/> of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static CollectionViewSource CreateFontCustomLayoutColorItemsCollectionViewSource() {
			var category = "Colors";

			return BarGalleryViewModel.CreateCollectionViewSource(new [] {
				// Row 1 - Group Start
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#fff600").ToColor(), category, "Yellow") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00fff6").ToColor(), category, "Teal") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ff88ee").ToColor(), category, "Pink") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },

				// Row 2 - Group Inner
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffba00").ToColor(), category, "Gold") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00d2ff").ToColor(), category, "Aqua") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#fc00ff").ToColor(), category, "Purple") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },

				// Row 3 - Group End
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ff5a00").ToColor(), category, "Orange") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00a2ff").ToColor(), category, "Blue") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#c000ff").ToColor(), category, "Purple") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },

				// Row 4 - No Group
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ff0000").ToColor(), category, "Red"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#0000ff").ToColor(), category, "Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#8000ff").ToColor(), category, "Purple"),
			}, categorize: true);
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the base colors of a custom theme.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateFontCustomThemeColorItemsCollection() {
			var category = "Custom Theme Colors";

			return new[] {
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#dfe3e5").ToColor(), category, "Ice Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#335b74").ToColor(), category, "Dark Teal"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#1cade4").ToColor(), category, "Turquoise"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#2683c6").ToColor(), category, "Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#27ced7").ToColor(), category, "Turquoise"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#42ba97").ToColor(), category, "Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#3e8853").ToColor(), category, "Dark Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#62a39f").ToColor(), category, "Teal"),
			};
		}

		/// <summary>
		/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the colors that might be used for tagging or categorization.
		/// </summary>
		/// <returns>A <see cref="CollectionViewSource"/> of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static CollectionViewSource CreateCategoryColorItemsCollectionViewSource() {
			var category = "Category Colors";

			return BarGalleryViewModel.CreateCollectionViewSource(new[] {
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#f04f58").ToColor(), category, "Red"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#f1a247").ToColor(), category, "Orange"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#f3cf4a").ToColor(), category, "Yellow"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#5dd260").ToColor(), category, "Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#5c85f5").ToColor(), category, "Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#b163d3").ToColor(), category, "Purple"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#9c9ca0").ToColor(), category, "Gray"),
			}, categorize: true);
		}

		/// <summary>
		/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the colors that will be styled to look like traditional menu items.
		/// </summary>
		/// <returns>A <see cref="CollectionViewSource"/> of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static CollectionViewSource CreateMenuItemItemsCollectionViewSource() {
			var primaryColorsCategory = "Primary Colors";
			var secondaryColorsCategory = "Secondary Colors";

			return BarGalleryViewModel.CreateCollectionViewSource(new[] {
				// Primary colors
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#f04f58").ToColor(), primaryColorsCategory, "Red") {
					KeyTipText = "R", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#f3cf4a").ToColor(), primaryColorsCategory, "Yellow") {
					KeyTipText = "Y", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#5c85f5").ToColor(), primaryColorsCategory, "Blue") {
					KeyTipText = "B", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				
				// Secondary colors
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#f1a247").ToColor(), secondaryColorsCategory, "Orange") {
					KeyTipText = "O", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#5dd260").ToColor(), secondaryColorsCategory, "Green") {
					KeyTipText = "G", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#b163d3").ToColor(), secondaryColorsCategory, "Purple") {
					KeyTipText = "P", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
			}, categorize: true);
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the customs colors used for text highlighting.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateTextHighlightCustomColorItemsCollection() {
			var category = "Text Highlight Colors";

			return new[] {
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#335b74").ToColor(), category, "Dark Teal"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#1cade4").ToColor(), category, "Turquoise"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#2683c6").ToColor(), category, "Blue"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#27ced7").ToColor(), category, "Turquoise"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#42ba97").ToColor(), category, "Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#3e8853").ToColor(), category, "Dark Green"),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#62a39f").ToColor(), category, "Teal"),
				new ColorBarGalleryItemViewModel(Colors.Yellow, category, "Yellow"),
				new ColorBarGalleryItemViewModel(Colors.Lime, category, "Lime"),
				new ColorBarGalleryItemViewModel(Colors.Cyan, category, "Cyan"),
				new ColorBarGalleryItemViewModel(Colors.Magenta, category, "Magenta"),
				new ColorBarGalleryItemViewModel(Colors.Red, category, "Red"),
				new ColorBarGalleryItemViewModel(Colors.Purple, category, "Purple"),
				new ColorBarGalleryItemViewModel(Colors.Maroon, category, "Maroon"),
				new ColorBarGalleryItemViewModel(Colors.Olive, category, "Olive"),
				new ColorBarGalleryItemViewModel(Colors.AliceBlue, category, "Alice Blue"),
				new ColorBarGalleryItemViewModel(Colors.Goldenrod, category, "Goldenrod"),
				new ColorBarGalleryItemViewModel(Colors.DarkSlateGray, category, "Dark Slate Gray"),
				new ColorBarGalleryItemViewModel(Colors.CornflowerBlue, category, "Cornflower Blue"),
				new ColorBarGalleryItemViewModel(Colors.Pink, category, "Pink"),
			};
		}

		/// <summary>
		/// Gets or sets the gallery item view models for a color picker using custom colors.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		private IEnumerable<ColorBarGalleryItemViewModel> CustomColorPickerItems { get; set; }

		/// <summary>
		/// Gets or sets the number of columns to be used when displaying <see cref="CustomColorPickerItems"/> in a gallery.
		/// </summary>
		/// <value>An integer value.</value>
		private int CustomColorPickerItemsColumnCount { get; set; }

		/// <summary>
		/// Gets or sets the gallery item view models for a text highlight color picker using custom colors.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		private IEnumerable<ColorBarGalleryItemViewModel> CustomTextHighlightColorItems { get; set; }

		/// <summary>
		/// Gets or sets the gallery item view models for a color picker using the default collection.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		private IEnumerable<ColorBarGalleryItemViewModel> DefaultFontColorItems { get; set; }

		/// <summary>
		/// Gets or sets the number of columns to be used when displaying <see cref="DefaultFontColorItems"/> in a gallery.
		/// </summary>
		/// <value>An integer value.</value>
		private int DefaultFontColorItemsColumnCount { get; set; }

		/// <summary>
		/// Gets or sets the gallery item view models for a text highlight color picker using the default collection.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		private IEnumerable<ColorBarGalleryItemViewModel> DefaultTextHighlightColorItems { get; set; }

		/// <summary>
		/// Initializes the collection of gallery item view models for the galleries used by this sample.
		/// </summary>
		private void InitializeColorGalleryItemViewModelCollections() {

			//
			// Default Font Colors
			//

			// The default collection in ColorBarGalleryItemViewModel is based on 70 colors:
			// - The first 10 colors are the base theme colors.
			// - The next 10 colors are the first of five alternate shades for each base theme color (BarGalleryItemLayoutBehavior.GroupStart)
			// - The next 10 colors are the second of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
			// - The next 10 colors are the third of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
			// - The next 10 colors are the forth of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
			// - The next 10 colors are the last of five alternate shades (BarGalleryItemLayoutBehavior.GroupEnd).
			// - The last 10 colors are standard colors that will not have alternate shades.
			//
			// The LayoutBehavior of each collection of shades is configured to display the shades as a group
			this.DefaultFontColorItems = ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection();

			// The MinColumnCount and MaxColumnCount of the gallery must be set to 10 so that the gallery of
			// color items wraps to a new row after every 10 items in the collection and will align each
			// of the alternate color shades directly under the base color.
			this.DefaultFontColorItemsColumnCount = 10;

			//
			// Custom Font Colors
			//

			// Initialize one collection of "theme" colors and one collection of "standard" colors
			var customBaseThemeColors = CreateFontCustomThemeColorItemsCollection();
			var customBaseStandardColors = CreateFontBaseStandardColorItemsCollection().Take(customBaseThemeColors.Length).ToArray();
			Debug.Assert(customBaseThemeColors.Length == customBaseStandardColors.Length, "Both collections must be of the same size for colors to be properly aligned in the gallery.");

			// ColorBarGalleryItemViewModel.CreateShadedCollection can be used to create a new collection that includes
			// all of the given base colors plus 5 additional shades for each color (see comment above for Default Font Colors).
			// The LayoutBehavior of each collection of shades is configured to display the shades as a group.

			// Start with the base theme colors and shades then concatenate the standard colors and shades
			this.CustomColorPickerItems = ColorBarGalleryItemViewModel.CreateShadedCollection(customBaseThemeColors)
				.Concat(ColorBarGalleryItemViewModel.CreateShadedCollection(customBaseStandardColors));

			// The MinColumnCount and MaxColumnCount of the gallery must be set to the number of base colors so that the gallery of
			// color items wraps to a new row after every 10 items in the collection and will align each
			// of the alternate color shades directly under the base color.
			this.CustomColorPickerItemsColumnCount = customBaseThemeColors.Length;

			//
			// Custom Style Font Colors
			//

			// Use the base standard colors as the collection of color items for the custom style sample
			customStyleColorPickerItems = CreateCategoryColorItemsCollectionViewSource();

			//
			// Custom Layout Font Colors
			//

			// Create a collection of items with a custom layout that includes grouping similar to the
			// default collection of shaded alternate colors
			customLayoutColorPickerItems = CreateFontCustomLayoutColorItemsCollectionViewSource();
			this.CustomLayoutColorPickerItemsColumnCount = 3;

			//
			// Custom MenuItem Style Colors
			//

			// Create a collection of items that will be rendered like traditional menu items
			customMenuItemColorPickerItems = CreateMenuItemItemsCollectionViewSource();

			//
			// Text Highlight Colors
			//

			// Generate the default collection from the predefined view model. Unlike Font Colors, Text Highlight Colors
			// do not include any color shades or special layouts, so there are no additional requirements
			this.DefaultTextHighlightColorItems = ColorBarGalleryItemViewModel.CreateDefaultTextHighlightCollection();

			// Generate a custom collection based on a custom collection of colors
			this.CustomTextHighlightColorItems = CreateTextHighlightCustomColorItemsCollection();

		}

		/// <summary>
		/// Occurs when the <see cref="OptionsProperty"/> dependency property value has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> containing data related to this event.</param>
		private static void OnOptionsPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
			=> ((SampleControlBase)obj).OnOptionsPropertyValueChanged(e.OldValue as OptionsViewModel, e.NewValue as OptionsViewModel);

		/// <summary>
		/// Refreshes the Font image based on the current font color.
		/// </summary>
		private void RefreshFontColorSmallImageSource() {
			// Update the Font image to show the selected color
			var color = Options?.FontColor ?? Colors.Red;
			this.FontColorSmallImageSource = CreateBitmapImageWithColorBar(color, "FontColor16.png");
		}

		/// <summary>
		/// Refreshes the Text Highlight image based on the current text highlight color.
		/// </summary>
		private void RefreshTextHighlightColorSmallImageSource() {
			// Update the Text Highlight image to show the selected color
			var color = Options?.TextHighlightColor ?? Colors.Yellow;
			this.TextHighlightColorSmallImageSource = CreateBitmapImageWithColorBar(color, "TextHighlightColor16.png");
		}

		/// <summary>
		/// Performs the action necessary to set a selected font color.
		/// </summary>
		/// <param name="color">The color to set.</param>
		/// <param name="label">The optional color label.</param>
		/// <param name="suppressMessage"><c>true</c> to suppress the demo message; otherwise <c>false</c> to allow it.</param>
		private void SetFontColor(Color color, string label = null, bool suppressMessage = false) {
			if (Options != null)
				Options.FontColor = color;

			if (!suppressMessage) {
				ThemedMessageBox.Show($"This is where you would apply the following font color to the current selection:\r\n\r\n{color} {label}\r\n\r\nThis sample updates the command icon to reflect the selected color.",
					"Set Font Color", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		/// <summary>
		/// Performs the action necessary to set a preview color.
		/// </summary>
		/// <param name="color">The color to preview.</param>
		private void SetPreviewColor(Color color) {
			// When working in a real editor application this method might be used to temporarily change the
			// font or text highlight color as the user mouses over different color selections.
			// This sample updates a color property instead so the preview color can still be visualized.
			if (this.Options != null)
				this.Options.LivePreviewColor = color;
		}

		/// <summary>
		/// Performs the action necessary to set a selected text highlight color.
		/// </summary>
		/// <param name="color">The color to set.</param>
		/// <param name="label">The optional color label.</param>
		/// <param name="suppressMessage"><c>true</c> to suppress the demo message; otherwise <c>false</c> to allow it.</param>
		private void SetTextHighlightColor(Color color, string label = null, bool suppressMessage = false) {
			if (this.Options != null)
				this.Options.TextHighlightColor = color;

			if (!suppressMessage) {
				ThemedMessageBox.Show($"This is where you would apply the following text highlight color to the current selection:\r\n\r\n{color} {label}\r\n\r\nThis sample updates the command icon to reflect the selected color.",
					"Set Text Highlight Color", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the gallery item view models for a color picker using a custom layout.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable CustomLayoutColorPickerItems => customLayoutColorPickerItems.View;

		/// <summary>
		/// Gets or sets the number of columns to be used when displaying <see cref="CustomLayoutColorPickerItems"/> in a gallery.
		/// </summary>
		/// <value>An integer value.</value>
		public int CustomLayoutColorPickerItemsColumnCount { get; private set; }

		/// <summary>
		/// Gets the gallery item view models for a color picker that will appear like traditional menu items.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable CustomMenuItemColorPickerItems => customMenuItemColorPickerItems.View;

		/// <summary>
		/// Gets the gallery item view models for a color picker using a custom style.
		/// </summary>
		/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable CustomStyleColorPickerItems => customStyleColorPickerItems.View;

		/// <summary>
		/// Gets the gallery item view models for a Font Color gallery.
		/// </summary>
		/// <value>An <see cref="ICollectionView"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable FontColorItems {
			get {
				if (this.Options?.UseCustomColors == true) {
					if (customFontColorItems is null)
						customFontColorItems = BarGalleryViewModel.CreateCollectionViewSource(CustomColorPickerItems, categorize: true);
					return customFontColorItems.View;
				}
				else {
					if (defaultFontColorItems is null)
						defaultFontColorItems = BarGalleryViewModel.CreateCollectionViewSource(DefaultFontColorItems, categorize: true);
					return defaultFontColorItems.View;
				}
			}
		}
		
		/// <summary>
		/// Gets the number of columns to be used when displaying <see cref="FontColorItems"/> in a gallery.
		/// </summary>
		/// <value>An integer value.</value>
		public int FontColorItemsColumnCount {
			get => this.Options?.UseCustomColors == true ? CustomColorPickerItemsColumnCount : DefaultFontColorItemsColumnCount;
		}
		
		/// <summary>
		/// Gets the gallery item view models for a Font Color gallery.
		/// </summary>
		/// <value>An <see cref="ICollectionView"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public ICollectionView FontColorItemsWithAutomatic {
			get {
				if (this.Options?.UseCustomColors == true) {
					if (customFontColorItemsWithAutomatic is null)
						customFontColorItemsWithAutomatic = BarGalleryViewModel.CreateCollectionViewSource(new[] { this.AutomaticColorGalleryItemViewModel }.Concat(CustomColorPickerItems), categorize: true);
					return customFontColorItemsWithAutomatic.View;
				}
				else {
					if (defaultFontColorItemsWithAutomatic is null)
						defaultFontColorItemsWithAutomatic = BarGalleryViewModel.CreateCollectionViewSource(new[] { this.AutomaticColorGalleryItemViewModel }.Concat(DefaultFontColorItems), categorize: true);
					return defaultFontColorItemsWithAutomatic.View;
				}
			}
		}

		/// <summary>
		/// Gets or sets the small-sized image to be used for the Font Color commands based on the current font color.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public ImageSource FontColorSmallImageSource {
			get => fontColorSmallImageSource;
			set {
				if (fontColorSmallImageSource != value) {
					fontColorSmallImageSource = value;
					NotifyPropertyChanged(nameof(FontColorSmallImageSource));
				}
			}
		}

		/// <summary>
		/// Gets the command to be executed for selecting from 'More Colors'.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand MoreColorsCommand {
			get {
				if (moreColorsCommand is null) {
					moreColorsCommand = new DelegateCommand<object>(param => {
						ThemedMessageBox.Show("This is where you would show a prompt for the user to select a custom color.",
							"More Colors", MessageBoxButton.OK, MessageBoxImage.Information);
					});
				}
				return moreColorsCommand;
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">The name of the changed property.</param>
		protected virtual void NotifyPropertyChanged(string propertyName) {
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Handles a change in one of the individual property values on <see cref="Options"/>.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event data.</param>
		protected virtual void OnOptionsPropertyChanged(object sender, PropertyChangedEventArgs args) {
			if (args.PropertyName == nameof(OptionsViewModel.FontColor)) {
				RefreshFontColorSmallImageSource();
			}
			else if (args.PropertyName == nameof(OptionsViewModel.TextHighlightColor)) {
				RefreshTextHighlightColorSmallImageSource();
			}
			else if (args.PropertyName == nameof(OptionsViewModel.UseCustomColors)) {
				// Notify that dependent properties have also changed
				NotifyPropertyChanged(nameof(FontColorItems));
				NotifyPropertyChanged(nameof(FontColorItemsColumnCount));
				NotifyPropertyChanged(nameof(FontColorItemsWithAutomatic));
				NotifyPropertyChanged(nameof(TextHighlightColorItems));
			}
		}

		/// <summary>
		/// Handles a change in the <see cref="OptionsProperty"/> dependency property value.
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		protected virtual void OnOptionsPropertyValueChanged(OptionsViewModel oldValue, OptionsViewModel newValue) {
			// Stop listening for changes
			if (oldValue != null)
				oldValue.PropertyChanged -= OnOptionsPropertyChanged;

			// Listen for changes
			if (newValue != null)
				newValue.PropertyChanged += OnOptionsPropertyChanged;

			if (newValue != null) {
				RefreshFontColorSmallImageSource();
				RefreshTextHighlightColorSmallImageSource();
			}
		}

		/// <summary>
		/// Gets or sets the options associated with this control.
		/// </summary>
		public OptionsViewModel Options {
			get => (OptionsViewModel)GetValue(OptionsProperty);
			set => SetValue(OptionsProperty, value);
		}

		/// <summary>
		/// Gets the command to be executed when setting a font color.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetFontColorCommand {
			get {
				// Use PreviewableDelegateCommand to support being notified of when the user moves the mouse over a gallery item (or gives it
				// keyboard focus) to preview the effect; otherwise any ICommand can be used if preview is not desired
				if (setFontColorCommand is null) {
					setFontColorCommand = new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
						executeAction: param => {
							if (param != null)
								SetFontColor(param.Value, param.Label);
						},
						canExecuteFunc: param => true,
						previewAction: param => {
							if (param != null)
								SetPreviewColor(param.Value);
						},
						cancelPreviewAction: param => SetPreviewColor(Colors.Transparent)
					);
				}
				return setFontColorCommand;
			}
		}

		/// <summary>
		/// Gets the command to be executed when setting a text highlight color.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SetTextHighlightColorCommand {
			get {
				// Use PreviewableDelegateCommand to support being notified of when the user moves the mouse over a gallery item (or gives it
				// keyboard focus) to preview the effect; otherwise any ICommand can be used if preview is not desired
				if (setTextHighlightColorCommand is null) {
					setTextHighlightColorCommand = new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
						executeAction: param => {
							if (param != null)
								SetTextHighlightColor(param.Value, param.Label);
						},
						canExecuteFunc: param => true,
						previewAction: param => {
							if (param != null)
								SetPreviewColor(param.Value);
						},
						cancelPreviewAction: param => SetPreviewColor(Colors.Transparent)
					);
				}
				return setTextHighlightColorCommand;
			}
		}
		
		/// <summary>
		/// Gets the command to be executed for selecting from 'Stop Highlighting'.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand StopHighlightingCommand {
			get {
				if (stopHighlightingCommand is null) {
					stopHighlightingCommand = new DelegateCommand<object>(param => {
						ThemedMessageBox.Show("This is where you would stop highlighting.",
							"Stop Highlighting", MessageBoxButton.OK, MessageBoxImage.Information);
					});
				}
				return stopHighlightingCommand;
			}
		}

		/// <summary>
		/// Gets the gallery item view models for a Text Highlight Color gallery.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> TextHighlightColorItems {
			get => this.Options?.UseCustomColors == true ? CustomTextHighlightColorItems : DefaultTextHighlightColorItems;
		}

		/// <summary>
		/// Gets or sets the small-sized image to be used for the Text Highlight commands based on the current text highlight color.
		/// </summary>
		/// <value>An <see cref="ImageSource"/>.</value>
		public ImageSource TextHighlightColorSmallImageSource {
			get => textHighlightColorSmallImageSource;
			set {
				if (textHighlightColorSmallImageSource != value) {
					textHighlightColorSmallImageSource = value;
					NotifyPropertyChanged(nameof(TextHighlightColorSmallImageSource));
				}
			}
		}

	}

}
