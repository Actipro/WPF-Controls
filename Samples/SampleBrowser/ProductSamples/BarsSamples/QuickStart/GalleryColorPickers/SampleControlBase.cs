using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers {

	/// <summary>
	/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
	/// </summary>
	public abstract class SampleControlBase : UserControl, INotifyPropertyChanged {

		private ColorBarGalleryItemViewModel	automaticColorGalleryItemViewModel;
		private ImageSource						fontColorSmallImageSource;
		private ICommand						moreColorsCommand;
		private ICommand						setFontColorCommand;
		private ICommand						setTextHighlightColorCommand;
		private ICommand						stopHighlightingCommand;
		private ImageSource						textHighlightColorSmallImageSource;

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
					automaticColorGalleryItemViewModel = new ColorBarGalleryItemViewModel(category: null, Colors.Black, "Automatic") {
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
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#c00000").ToColor(), "Dark Red"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ff0000").ToColor(), "Red"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ffc000").ToColor(), "Orange"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ffff00").ToColor(), "Yellow"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#92d050").ToColor(), "Light Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#00b050").ToColor(), "Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#00b0f0").ToColor(), "Light Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#0070c0").ToColor(), "Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#002060").ToColor(), "Dark Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#7030a0").ToColor(), "Purple"),
			};
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the layout that defines custom colors and groups.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateFontCustomLayoutColorItemsCollection() {
			var category = "Colors";
			return new [] {
				// Row 1 - Group Start
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#fff600").ToColor(),"Yellow") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#00fff6").ToColor(),"Teal") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ff88ee").ToColor(),"Pink") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },

				// Row 2 - Group Inner
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ffba00").ToColor(),"Gold") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#00d2ff").ToColor(),"Aqua") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#fc00ff").ToColor(),"Purple") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },

				// Row 3 - Group End
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ff5a00").ToColor(),"Orange") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#00a2ff").ToColor(),"Blue") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#c000ff").ToColor(),"Purple") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },

				// Row 4 - No Group
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#ff0000").ToColor(),"Red"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#0000ff").ToColor(),"Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#8000ff").ToColor(),"Purple"),
			};
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the base colors of a custom theme.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateFontCustomThemeColorItemsCollection() {
			var category = "Custom Theme Colors";
			return new[] {
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#dfe3e5").ToColor(), "Ice Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#335b74").ToColor(), "Dark Teal"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#1cade4").ToColor(), "Turquoise"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#2683c6").ToColor(), "Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#27ced7").ToColor(), "Turquoise"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#42ba97").ToColor(), "Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#3e8853").ToColor(), "Dark Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#62a39f").ToColor(), "Teal"),
			};
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the colors that might be used for tagging or categorization.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateCategoryColorItemsCollection() {
			var category = "Category Colors";
			return new[] {
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#f04f58").ToColor(), "Red"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#f1a247").ToColor(), "Orange"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#f3cf4a").ToColor(), "Yellow"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#5dd260").ToColor(), "Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#5c85f5").ToColor(), "Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#b163d3").ToColor(), "Purple"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#9c9ca0").ToColor(), "Gray"),
			};
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the colors that will be styled to look like traditional menu items.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateMenuItemItemsCollection() {
			var primaryColorsCategory = "Primary Colors";
			var secondaryColorsCategory = "Secondary Colors";
			return new[] {
				// Primary colors
				new ColorBarGalleryItemViewModel(primaryColorsCategory, UIColor.FromWebColor("#f04f58").ToColor(), "Red") {
					KeyTipText = "R", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(primaryColorsCategory, UIColor.FromWebColor("#f3cf4a").ToColor(), "Yellow") {
					KeyTipText = "Y", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(primaryColorsCategory, UIColor.FromWebColor("#5c85f5").ToColor(), "Blue") {
					KeyTipText = "B", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				
				// Secondary colors
				new ColorBarGalleryItemViewModel(secondaryColorsCategory, UIColor.FromWebColor("#f1a247").ToColor(), "Orange") {
					KeyTipText = "O", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(secondaryColorsCategory, UIColor.FromWebColor("#5dd260").ToColor(), "Green") {
					KeyTipText = "G", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
				new ColorBarGalleryItemViewModel(secondaryColorsCategory, UIColor.FromWebColor("#b163d3").ToColor(), "Purple") {
					KeyTipText = "P", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				},
			};
		}

		/// <summary>
		/// Creates a collection of gallery item view models for the customs colors used for text highlighting.
		/// </summary>
		/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
		private static ColorBarGalleryItemViewModel[] CreateTextHighlightCustomColorItemsCollection() {
			var category = "Text Highlight Colors";
			return new[] {
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#335b74").ToColor(), "Dark Teal"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#1cade4").ToColor(), "Turquoise"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#2683c6").ToColor(), "Blue"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#27ced7").ToColor(), "Turquoise"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#42ba97").ToColor(), "Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#3e8853").ToColor(), "Dark Green"),
				new ColorBarGalleryItemViewModel(category, UIColor.FromWebColor("#62a39f").ToColor(), "Teal"),
				new ColorBarGalleryItemViewModel(category, Colors.Yellow, "Yellow"),
				new ColorBarGalleryItemViewModel(category, Colors.Lime, "Lime"),
				new ColorBarGalleryItemViewModel(category, Colors.Cyan, "Cyan"),
				new ColorBarGalleryItemViewModel(category, Colors.Magenta, "Magenta"),
				new ColorBarGalleryItemViewModel(category, Colors.Red, "Red"),
				new ColorBarGalleryItemViewModel(category, Colors.Purple, "Purple"),
				new ColorBarGalleryItemViewModel(category, Colors.Maroon, "Maroon"),
				new ColorBarGalleryItemViewModel(category, Colors.Olive, "Olive"),
				new ColorBarGalleryItemViewModel(category, Colors.AliceBlue, "Alice Blue"),
				new ColorBarGalleryItemViewModel(category, Colors.Goldenrod, "Goldenrod"),
				new ColorBarGalleryItemViewModel(category, Colors.DarkSlateGray, "Dark Slate Gray"),
				new ColorBarGalleryItemViewModel(category, Colors.CornflowerBlue, "Cornflower Blue"),
				new ColorBarGalleryItemViewModel(category, Colors.Pink, "Pink"),
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
			this.CustomStyleColorPickerItems = CreateCategoryColorItemsCollection();

			//
			// Custom Layout Font Colors
			//

			// Create a collection of items with a custom layout that includes grouping similar to the
			// default collection of shaded alternate colors
			this.CustomLayoutColorPickerItems = CreateFontCustomLayoutColorItemsCollection();
			this.CustomLayoutColorPickerItemsColumnCount = 3;

			//
			// Custom MenuItem Style Colors
			//

			// Create a collection of items that will be rendered like traditional menu items
			this.CustomMenuItemColorPickerItems = CreateMenuItemItemsCollection();

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
		/// Gets or sets the gallery item view models for a color picker using a custom layout.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> CustomLayoutColorPickerItems { get; private set; }

		/// <summary>
		/// Gets or sets the number of columns to be used when displaying <see cref="CustomLayoutColorPickerItems"/> in a gallery.
		/// </summary>
		/// <value>An integer value.</value>
		public int CustomLayoutColorPickerItemsColumnCount { get; private set; }

		/// <summary>
		/// Gets or sets the gallery item view models for a color picker that will appear like traditional menu items.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> CustomMenuItemColorPickerItems { get; private set; }

		/// <summary>
		/// Gets or sets the gallery item view models for a color picker using a custom style.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> CustomStyleColorPickerItems { get; private set; }

		/// <summary>
		/// Gets the gallery item view models for a Font Color gallery.
		/// </summary>
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> FontColorItems {
			get => this.Options?.UseCustomColors == true ? CustomColorPickerItems : DefaultFontColorItems;
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
		/// <value>An <see cref="IEnumerable{T}"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
		public IEnumerable<ColorBarGalleryItemViewModel> FontColorItemsWithAutomatic {
			get => new ColorBarGalleryItemViewModel[] { this.AutomaticColorGalleryItemViewModel }.Concat(this.FontColorItems);
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
								SetFontColor(param.Color, param.Label);
						},
						canExecuteFunc: param => true,
						previewAction: param => {
							if (param != null)
								SetPreviewColor(param.Color);
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
								SetTextHighlightColor(param.Color, param.Label);
						},
						canExecuteFunc: param => true,
						previewAction: param => {
							if (param != null)
								SetPreviewColor(param.Color);
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
