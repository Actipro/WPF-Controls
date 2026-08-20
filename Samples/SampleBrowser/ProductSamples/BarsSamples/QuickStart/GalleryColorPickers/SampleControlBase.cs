using ActiproSoftware.SampleBrowser;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using ActiproSoftware.Windows.Input;
using ActiproSoftware.Windows.Media;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GalleryColorPickers;

/// <summary>
/// Provides the base user control of shared logic for this sample that is extended for MVVM- and XAML-based samples.
/// </summary>
public abstract class SampleControlBase : UserControl, INotifyPropertyChanged {

	// NOTE: Galleries that support categorization must define a CollectionViewSource configured with grouping.
	//   Otherwise, any IEnumerable can be used as the items source.

	private ColorBarGalleryItemViewModel? _automaticColorGalleryItemViewModel;
	private ImageSource? _fontColorSmallImageSource;
	private ICommand? _moreColorsCommand;
	private ICommand? _setFontColorCommand;
	private ICommand? _setTextHighlightColorCommand;
	private ICommand? _stopHighlightingCommand;
	private ImageSource? _textHighlightColorSmallImageSource;

	private CollectionViewSource? _customLayoutColorPickerItems;
	private CollectionViewSource? _customMenuItemColorPickerItems;
	private CollectionViewSource? _customStyleColorPickerItems;

	private CollectionViewSource? _defaultFontColorItems;
	private CollectionViewSource? _defaultFontColorItemsWithAutomatic;
	private CollectionViewSource? _customFontColorItems;
	private CollectionViewSource? _customFontColorItemsWithAutomatic;

	#region Dependency Properties

	public static readonly DependencyProperty OptionsProperty
		= DependencyProperty.Register(nameof(Options), typeof(OptionsViewModel), typeof(SampleControlBase), new PropertyMetadata(defaultValue: null, OnOptionsPropertyValueChanged));

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// EVENTS
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
	public event PropertyChangedEventHandler? PropertyChanged;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SampleControlBase() {
		// Initialize the collection of color gallery items (used by both XAML and MVVM samples)
		InitializeColorGalleryItemViewModelCollections();

		// Initialize the Font and Text Highlight colors
		SetFontColor(Colors.Red, suppressMessage: true);
		SetTextHighlightColor(Colors.Yellow, suppressMessage: true);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
	/// </summary>
	private ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel
		=> _automaticColorGalleryItemViewModel ??= new ColorBarGalleryItemViewModel(Colors.Black, category: null, "Automatic") { LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem };

	/// <summary>
	/// Creates a <see cref="BitmapImage"/> where a color bar is added to a pre-defined image.
	/// </summary>
	/// <param name="color">The color to be used when rendering the color bar.</param>
	/// <param name="fileName">The name of the file resource which defines the base image.</param>
	private static ImageSource? CreateBitmapImageWithColorBar(Color color, string fileName) {
		// Load the base image
		if (ImageLoader.GetIcon(fileName) is { } imageSource) {
			// Determine the bounds of the color bar
			var imageHeight = imageSource.Height;
			var imageWidth = imageSource.Width;
			if ((imageHeight > 0) && (imageWidth > 0)) {
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
	private static ColorBarGalleryItemViewModel[] CreateFontBaseStandardColorItemsCollection() {
		var category = "Standard Colors";
		return [
			new(UIColor.FromWebColor("#c00000").ToColor(), category, "Dark Red"),
			new(UIColor.FromWebColor("#ff0000").ToColor(), category, "Red"),
			new(UIColor.FromWebColor("#ffc000").ToColor(), category, "Orange"),
			new(UIColor.FromWebColor("#ffff00").ToColor(), category, "Yellow"),
			new(UIColor.FromWebColor("#92d050").ToColor(), category, "Light Green"),
			new(UIColor.FromWebColor("#00b050").ToColor(), category, "Green"),
			new(UIColor.FromWebColor("#00b0f0").ToColor(), category, "Light Blue"),
			new(UIColor.FromWebColor("#0070c0").ToColor(), category, "Blue"),
			new(UIColor.FromWebColor("#002060").ToColor(), category, "Dark Blue"),
			new(UIColor.FromWebColor("#7030a0").ToColor(), category, "Purple"),
		];
	}

	/// <summary>
	/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the layout that defines custom colors and groups.
	/// </summary>
	private static CollectionViewSource CreateFontCustomLayoutColorItemsCollectionViewSource() {
		var category = "Colors";
		return BarGalleryViewModel.CreateCollectionViewSource(
			new ColorBarGalleryItemViewModel[] {
				// Row 1 - Group Start
				new(UIColor.FromWebColor("#fff600").ToColor(), category, "Yellow") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
				new(UIColor.FromWebColor("#00fff6").ToColor(), category, "Teal") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
				new(UIColor.FromWebColor("#ff88ee").ToColor(), category, "Pink") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },

				// Row 2 - Group Inner
				new(UIColor.FromWebColor("#ffba00").ToColor(), category, "Gold") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
				new(UIColor.FromWebColor("#00d2ff").ToColor(), category, "Aqua") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
				new(UIColor.FromWebColor("#fc00ff").ToColor(), category, "Purple") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },

				// Row 3 - Group End
				new(UIColor.FromWebColor("#ff5a00").ToColor(), category, "Orange") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
				new(UIColor.FromWebColor("#00a2ff").ToColor(), category, "Blue") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
				new(UIColor.FromWebColor("#c000ff").ToColor(), category, "Purple") { LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },

				// Row 4 - No Group
				new(UIColor.FromWebColor("#ff0000").ToColor(), category, "Red"),
				new(UIColor.FromWebColor("#0000ff").ToColor(), category, "Blue"),
				new(UIColor.FromWebColor("#8000ff").ToColor(), category, "Purple"),
			},
			categorize: true
		);
	}

	/// <summary>
	/// Creates a collection of gallery item view models for the base colors of a custom theme.
	/// </summary>
	/// <returns>An array of <see cref="ColorBarGalleryItemViewModel"/>.</returns>
	private static ColorBarGalleryItemViewModel[] CreateFontCustomThemeColorItemsCollection() {
		var category = "Custom Theme Colors";
		return [
			new(UIColor.FromWebColor("#dfe3e5").ToColor(), category, "Ice Blue"),
			new(UIColor.FromWebColor("#335b74").ToColor(), category, "Dark Teal"),
			new(UIColor.FromWebColor("#1cade4").ToColor(), category, "Turquoise"),
			new(UIColor.FromWebColor("#2683c6").ToColor(), category, "Blue"),
			new(UIColor.FromWebColor("#27ced7").ToColor(), category, "Turquoise"),
			new(UIColor.FromWebColor("#42ba97").ToColor(), category, "Green"),
			new(UIColor.FromWebColor("#3e8853").ToColor(), category, "Dark Green"),
			new(UIColor.FromWebColor("#62a39f").ToColor(), category, "Teal"),
		];
	}

	/// <summary>
	/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the colors that might be used for tagging or categorization.
	/// </summary>
	private static CollectionViewSource CreateCategoryColorItemsCollectionViewSource() {
		var category = "Category Colors";
		return BarGalleryViewModel.CreateCollectionViewSource(
			new ColorBarGalleryItemViewModel[] {
				new(UIColor.FromWebColor("#f04f58").ToColor(), category, "Red"),
				new(UIColor.FromWebColor("#f1a247").ToColor(), category, "Orange"),
				new(UIColor.FromWebColor("#f3cf4a").ToColor(), category, "Yellow"),
				new(UIColor.FromWebColor("#5dd260").ToColor(), category, "Green"),
				new(UIColor.FromWebColor("#5c85f5").ToColor(), category, "Blue"),
				new(UIColor.FromWebColor("#b163d3").ToColor(), category, "Purple"),
				new(UIColor.FromWebColor("#9c9ca0").ToColor(), category, "Gray"),
			},
			categorize: true
		);
	}

	/// <summary>
	/// Creates a <see cref="CollectionViewSource"/> of gallery item view models for the colors that will be styled to look like traditional menu items.
	/// </summary>
	private static CollectionViewSource CreateMenuItemItemsCollectionViewSource() {
		var primaryColorsCategory = "Primary Colors";
		var secondaryColorsCategory = "Secondary Colors";

		return BarGalleryViewModel.CreateCollectionViewSource(
			new ColorBarGalleryItemViewModel[] {
				// Primary colors
				new(UIColor.FromWebColor("#f04f58").ToColor(), primaryColorsCategory, "Red") { KeyTipText = "R", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
				new(UIColor.FromWebColor("#f3cf4a").ToColor(), primaryColorsCategory, "Yellow") { KeyTipText = "Y", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
				new(UIColor.FromWebColor("#5c85f5").ToColor(), primaryColorsCategory, "Blue") { KeyTipText = "B", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },

				// Secondary colors
				new(UIColor.FromWebColor("#f1a247").ToColor(), secondaryColorsCategory, "Orange") { KeyTipText = "O", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
				new(UIColor.FromWebColor("#5dd260").ToColor(), secondaryColorsCategory, "Green") { KeyTipText = "G", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
				new(UIColor.FromWebColor("#b163d3").ToColor(), secondaryColorsCategory, "Purple") { KeyTipText = "P", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
			}, categorize: true
		);
	}

	/// <summary>
	/// Creates a collection of gallery item view models for the customs colors used for text highlighting.
	/// </summary>
	private static ColorBarGalleryItemViewModel[] CreateTextHighlightCustomColorItemsCollection() {
		var category = "Text Highlight Colors";
		return [
			new(UIColor.FromWebColor("#335b74").ToColor(), category, "Dark Teal"),
			new(UIColor.FromWebColor("#1cade4").ToColor(), category, "Turquoise"),
			new(UIColor.FromWebColor("#2683c6").ToColor(), category, "Blue"),
			new(UIColor.FromWebColor("#27ced7").ToColor(), category, "Turquoise"),
			new(UIColor.FromWebColor("#42ba97").ToColor(), category, "Green"),
			new(UIColor.FromWebColor("#3e8853").ToColor(), category, "Dark Green"),
			new(UIColor.FromWebColor("#62a39f").ToColor(), category, "Teal"),
			new(Colors.Yellow, category, "Yellow"),
			new(Colors.Lime, category, "Lime"),
			new(Colors.Cyan, category, "Cyan"),
			new(Colors.Magenta, category, "Magenta"),
			new(Colors.Red, category, "Red"),
			new(Colors.Purple, category, "Purple"),
			new(Colors.Maroon, category, "Maroon"),
			new(Colors.Olive, category, "Olive"),
			new(Colors.AliceBlue, category, "Alice Blue"),
			new(Colors.Goldenrod, category, "Goldenrod"),
			new(Colors.DarkSlateGray, category, "Dark Slate Gray"),
			new(Colors.CornflowerBlue, category, "Cornflower Blue"),
			new(Colors.Pink, category, "Pink"),
		];
	}

	/// <summary>
	/// The gallery item view models for a color picker using custom colors.
	/// </summary>
	private IEnumerable<ColorBarGalleryItemViewModel>? CustomColorPickerItems { get; set; }

	/// <summary>
	/// The number of columns to be used when displaying <see cref="CustomColorPickerItems"/> in a gallery.
	/// </summary>
	private int CustomColorPickerItemsColumnCount { get; set; }

	/// <summary>
	/// The gallery item view models for a text highlight color picker using custom colors.
	/// </summary>
	private IEnumerable<ColorBarGalleryItemViewModel>? CustomTextHighlightColorItems { get; set; }

	/// <summary>
	/// The gallery item view models for a color picker using the default collection.
	/// </summary>
	private IEnumerable<ColorBarGalleryItemViewModel>? DefaultFontColorItems { get; set; }

	/// <summary>
	/// The number of columns to be used when displaying <see cref="DefaultFontColorItems"/> in a gallery.
	/// </summary>
	private int DefaultFontColorItemsColumnCount { get; set; }

	/// <summary>
	/// The gallery item view models for a text highlight color picker using the default collection.
	/// </summary>
	private IEnumerable<ColorBarGalleryItemViewModel>? DefaultTextHighlightColorItems { get; set; }

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
		DefaultFontColorItems = ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection();

		// The MinColumnCount and MaxColumnCount of the gallery must be set to 10 so that the gallery of
		//   color items wraps to a new row after every 10 items in the collection and will align each
		//   of the alternate color shades directly under the base color.
		DefaultFontColorItemsColumnCount = 10;

		//
		// Custom Font Colors
		//

		// Initialize one collection of "theme" colors and one collection of "standard" colors
		var customBaseThemeColors = CreateFontCustomThemeColorItemsCollection();
		var customBaseStandardColors = CreateFontBaseStandardColorItemsCollection().Take(customBaseThemeColors.Length).ToArray();
		Debug.Assert(customBaseThemeColors.Length == customBaseStandardColors.Length, "Both collections must be of the same size for colors to be properly aligned in the gallery.");

		// ColorBarGalleryItemViewModel.CreateShadedCollection can be used to create a new collection that includes
		//   all of the given base colors plus 5 additional shades for each color (see comment above for Default Font Colors).
		//   The LayoutBehavior of each collection of shades is configured to display the shades as a group.

		// Start with the base theme colors and shades then concatenate the standard colors and shades
		CustomColorPickerItems = ColorBarGalleryItemViewModel.CreateShadedCollection(customBaseThemeColors)
			.Concat(ColorBarGalleryItemViewModel.CreateShadedCollection(customBaseStandardColors));

		// The MinColumnCount and MaxColumnCount of the gallery must be set to the number of base colors so that the gallery of
		//   color items wraps to a new row after every 10 items in the collection and will align each
		//   of the alternate color shades directly under the base color.
		CustomColorPickerItemsColumnCount = customBaseThemeColors.Length;

		//
		// Custom Style Font Colors
		//

		// Use the base standard colors as the collection of color items for the custom style sample
		_customStyleColorPickerItems = CreateCategoryColorItemsCollectionViewSource();

		//
		// Custom Layout Font Colors
		//

		// Create a collection of items with a custom layout that includes grouping similar to the
		//   default collection of shaded alternate colors
		_customLayoutColorPickerItems = CreateFontCustomLayoutColorItemsCollectionViewSource();
		CustomLayoutColorPickerItemsColumnCount = 3;

		//
		// Custom MenuItem Style Colors
		//

		// Create a collection of items that will be rendered like traditional menu items
		_customMenuItemColorPickerItems = CreateMenuItemItemsCollectionViewSource();

		//
		// Text Highlight Colors
		//

		// Generate the default collection from the predefined view model. Unlike Font Colors, Text Highlight Colors
		//   do not include any color shades or special layouts, so there are no additional requirements
		DefaultTextHighlightColorItems = ColorBarGalleryItemViewModel.CreateDefaultTextHighlightCollection();

		// Generate a custom collection based on a custom collection of colors
		CustomTextHighlightColorItems = CreateTextHighlightCustomColorItemsCollection();

	}

	/// <summary>
	/// Occurs when the <see cref="OptionsProperty"/> dependency property value has changed.
	/// </summary>
	private static void OnOptionsPropertyValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		=> ((SampleControlBase)obj).OnOptionsPropertyValueChanged(e.OldValue as OptionsViewModel, e.NewValue as OptionsViewModel);

	/// <summary>
	/// Refreshes the Font image based on the current font color.
	/// </summary>
	private void RefreshFontColorSmallImageSource() {
		// Update the Font image to show the selected color
		var color = Options?.FontColor ?? Colors.Red;
		FontColorSmallImageSource = CreateBitmapImageWithColorBar(color, "FontColor16.png");
	}

	/// <summary>
	/// Refreshes the Text Highlight image based on the current text highlight color.
	/// </summary>
	private void RefreshTextHighlightColorSmallImageSource() {
		// Update the Text Highlight image to show the selected color
		var color = Options?.TextHighlightColor ?? Colors.Yellow;
		TextHighlightColorSmallImageSource = CreateBitmapImageWithColorBar(color, "TextHighlightColor16.png");
	}

	/// <summary>
	/// Performs the action necessary to set a selected font color.
	/// </summary>
	/// <param name="color">The color to set.</param>
	/// <param name="label">The optional color label.</param>
	/// <param name="suppressMessage"><c>true</c> to suppress the demo message; otherwise <c>false</c> to allow it.</param>
	private void SetFontColor(Color color, string? label = null, bool suppressMessage = false) {
		if (Options is not null)
			Options.FontColor = color;

		if (!suppressMessage) {
			MessageBox.Show($"This is where you would apply the following font color to the current selection:\r\n\r\n{color} {label}\r\n\r\nThis sample updates the command icon to reflect the selected color.",
				"Set Font Color", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

	/// <summary>
	/// Performs the action necessary to set a preview color.
	/// </summary>
	/// <param name="color">The color to preview.</param>
	private void SetPreviewColor(Color color) {
		// When working in a real editor application this method might be used to temporarily change the
		//   font or text highlight color as the user mouses over different color selections.
		//   This sample updates a color property instead so the preview color can still be visualized.
		if (Options is not null)
			Options.LivePreviewColor = color;
	}

	/// <summary>
	/// Performs the action necessary to set a selected text highlight color.
	/// </summary>
	/// <param name="color">The color to set.</param>
	/// <param name="label">The optional color label.</param>
	/// <param name="suppressMessage"><c>true</c> to suppress the demo message; otherwise <c>false</c> to allow it.</param>
	private void SetTextHighlightColor(Color color, string? label = null, bool suppressMessage = false) {
		if (Options is not null)
			Options.TextHighlightColor = color;

		if (!suppressMessage) {
			MessageBox.Show($"This is where you would apply the following text highlight color to the current selection:\r\n\r\n{color} {label}\r\n\r\nThis sample updates the command icon to reflect the selected color.",
				"Set Text Highlight Color", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The gallery item view models for a color picker using a custom layout.
	/// </summary>
	/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
	public IEnumerable? CustomLayoutColorPickerItems
		=> _customLayoutColorPickerItems?.View;

	/// <summary>
	/// The number of columns to be used when displaying <see cref="CustomLayoutColorPickerItems"/> in a gallery.
	/// </summary>
	public int CustomLayoutColorPickerItemsColumnCount { get; private set; }

	/// <summary>
	/// The gallery item view models for a color picker that will appear like traditional menu items.
	/// </summary>
	/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
	public IEnumerable? CustomMenuItemColorPickerItems
		=> _customMenuItemColorPickerItems?.View;

	/// <summary>
	/// The gallery item view models for a color picker using a custom style.
	/// </summary>
	/// <value>An <see cref="IEnumerable"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
	public IEnumerable? CustomStyleColorPickerItems
		=> _customStyleColorPickerItems?.View;

	/// <summary>
	/// The gallery item view models for a Font Color gallery.
	/// </summary>
	/// <value>An <see cref="ICollectionView"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
	public IEnumerable? FontColorItems {
		get {
			if (Options?.UseCustomColors == true) {
				if ((_customFontColorItems is null) && (CustomColorPickerItems is { } items))
					_customFontColorItems = BarGalleryViewModel.CreateCollectionViewSource(items, categorize: true);
				return _customFontColorItems?.View;
			}
			else {
				if ((_defaultFontColorItems is null) && (DefaultFontColorItems is { } items))
					_defaultFontColorItems = BarGalleryViewModel.CreateCollectionViewSource(items, categorize: true);
				return _defaultFontColorItems?.View;
			}
		}
	}

	/// <summary>
	/// The number of columns to be used when displaying <see cref="FontColorItems"/> in a gallery.
	/// </summary>
	public int FontColorItemsColumnCount
		=> Options?.UseCustomColors == true ? CustomColorPickerItemsColumnCount : DefaultFontColorItemsColumnCount;

	/// <summary>
	/// The gallery item view models for a Font Color gallery.
	/// </summary>
	/// <value>An <see cref="ICollectionView"/> of <see cref="ColorBarGalleryItemViewModel"/>.</value>
	public ICollectionView? FontColorItemsWithAutomatic {
		get {
			if (Options?.UseCustomColors == true) {
				if ((_customFontColorItemsWithAutomatic is null) && (CustomColorPickerItems is { } items))
					_customFontColorItemsWithAutomatic = BarGalleryViewModel.CreateCollectionViewSource(new[] { AutomaticColorGalleryItemViewModel }.Concat(items), categorize: true);
				return _customFontColorItemsWithAutomatic?.View;
			}
			else {
				if ((_defaultFontColorItemsWithAutomatic is null) && (DefaultFontColorItems is { } items))
					_defaultFontColorItemsWithAutomatic = BarGalleryViewModel.CreateCollectionViewSource(new[] { AutomaticColorGalleryItemViewModel }.Concat(items), categorize: true);
				return _defaultFontColorItemsWithAutomatic?.View;
			}
		}
	}

	/// <summary>
	/// The small-sized image to be used for the Font Color commands based on the current font color.
	/// </summary>
	public ImageSource? FontColorSmallImageSource {
		get => _fontColorSmallImageSource;
		set => SetProperty(ref _fontColorSmallImageSource, value);
	}

	/// <summary>
	/// The command to be executed for selecting from 'More Colors'.
	/// </summary>
	public ICommand MoreColorsCommand {
		get => _moreColorsCommand ??= new DelegateCommand<object>(_ => {
			MessageBox.Show("This is where you would show a prompt for the user to select a custom color.",
				"More Colors", MessageBoxButton.OK, MessageBoxImage.Information);
		});
	}

	/// <summary>
	/// Handles a change in one of the individual property values on <see cref="Options"/>.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="args">The event data.</param>
	protected virtual void OnOptionsPropertyChanged(object? sender, PropertyChangedEventArgs args) {
		if (args.PropertyName == nameof(OptionsViewModel.FontColor)) {
			RefreshFontColorSmallImageSource();
		}
		else if (args.PropertyName == nameof(OptionsViewModel.TextHighlightColor)) {
			RefreshTextHighlightColorSmallImageSource();
		}
		else if (args.PropertyName == nameof(OptionsViewModel.UseCustomColors)) {
			// Notify that dependent properties have also changed
			OnPropertyChanged(nameof(FontColorItems));
			OnPropertyChanged(nameof(FontColorItemsColumnCount));
			OnPropertyChanged(nameof(FontColorItemsWithAutomatic));
			OnPropertyChanged(nameof(TextHighlightColorItems));
		}
	}

	/// <summary>
	/// Handles a change in the <see cref="OptionsProperty"/> dependency property value.
	/// </summary>
	/// <param name="oldValue">The old value.</param>
	/// <param name="newValue">The new value.</param>
	protected virtual void OnOptionsPropertyValueChanged(OptionsViewModel? oldValue, OptionsViewModel? newValue) {
		// Stop listening for changes
		if (oldValue is not null)
			oldValue.PropertyChanged -= OnOptionsPropertyChanged;

		if (newValue is not null) {
			// Listen for changes
			newValue.PropertyChanged += OnOptionsPropertyChanged;

			RefreshFontColorSmallImageSource();
			RefreshTextHighlightColorSmallImageSource();
		}
	}

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="propertyName">(Optional) The name of the property that changed.</param>
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="e">The event data.</param>
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		=> PropertyChanged?.Invoke(this, e);

	/// <summary>
	/// The options associated with this control.
	/// </summary>
	public OptionsViewModel? Options {
		get => (OptionsViewModel)GetValue(OptionsProperty);
		set => SetValue(OptionsProperty, value);
	}

	/// <summary>
	/// The command to be executed when setting a font color.
	/// </summary>
	public ICommand SetFontColorCommand {
		// Use PreviewableDelegateCommand to support being notified of when the user moves the mouse over a gallery item (or gives it
		//   keyboard focus) to preview the effect; otherwise any ICommand can be used if preview is not desired
		get => _setFontColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
			executeAction: p => {
				if (p is not null)
					SetFontColor(p.Value, p.Label);
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				if (p is not null)
					SetPreviewColor(p.Value);
			},
			cancelPreviewAction: _ => SetPreviewColor(Colors.Transparent)
		);
	}

	/// <summary>
	/// Called from a property setter to change the backing field's value and raise
	/// <see cref="PropertyChanged"/> notification events if the new value is not equal to the current value.
	/// </summary>
	/// <typeparam name="T">The type of the property that changed.</typeparam>
	/// <param name="field">The backing field that holds the property's value, which may be updated.</param>
	/// <param name="newValue">The new property value.</param>
	/// <param name="propertyName">(Optional) The name of the property that changed.</param>
	/// <returns>
	/// <c>true</c> if the property was changed; otherwise, <c>false</c>.
	/// </returns>
	protected bool SetProperty<T>(
		#if NET
		[NotNullIfNotNull(nameof(newValue))]
		#endif
		ref T field, T newValue, [CallerMemberName] string? propertyName = null) {
		// IMPORTANT NOTE: This method is replicated over multiple classes and edits must be kept in sync
		if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
			field = newValue;
			OnPropertyChanged(propertyName);
			return true;
		}

		return false;
	}

	/// <summary>
	/// The command to be executed when setting a text highlight color.
	/// </summary>
	public ICommand SetTextHighlightColorCommand {
		// Use PreviewableDelegateCommand to support being notified of when the user moves the mouse over a gallery item (or gives it
		//   keyboard focus) to preview the effect; otherwise any ICommand can be used if preview is not desired
		get => _setTextHighlightColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
			executeAction: p => {
				if (p is not null)
					SetTextHighlightColor(p.Value, p.Label);
			},
			canExecuteFunc: _ => true,
			previewAction: p => {
				if (p is not null)
					SetPreviewColor(p.Value);
			},
			cancelPreviewAction: _ => SetPreviewColor(Colors.Transparent)
		);
	}

	/// <summary>
	/// The command to be executed for selecting from 'Stop Highlighting'.
	/// </summary>
	public ICommand StopHighlightingCommand {
		get => _stopHighlightingCommand ??= new DelegateCommand<object>(_ => {
			MessageBox.Show("This is where you would stop highlighting.",
				"Stop Highlighting", MessageBoxButton.OK, MessageBoxImage.Information);
		});
	}

	/// <summary>
	/// The gallery item view models for a Text Highlight Color gallery.
	/// </summary>
	public IEnumerable<ColorBarGalleryItemViewModel>? TextHighlightColorItems
		=> Options?.UseCustomColors == true ? CustomTextHighlightColorItems : DefaultTextHighlightColorItems;

	/// <summary>
	/// The small-sized image to be used for the Text Highlight commands based on the current text highlight color.
	/// </summary>
	public ImageSource? TextHighlightColorSmallImageSource {
		get => _textHighlightColorSmallImageSource;
		set => SetProperty(ref _textHighlightColorSmallImageSource, value);
	}

}
