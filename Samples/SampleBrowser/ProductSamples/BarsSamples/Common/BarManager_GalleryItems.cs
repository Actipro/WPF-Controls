using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

partial class BarManager {

	private CollectionViewSource? _borderGalleryItems;
	private CollectionViewSource? _bulletGalleryItems;
	private CollectionViewSource? _fontColorPickerGalleryItems;
	private CollectionViewSource? _fontFamilyGalleryItems;
	private CollectionViewSource? _numberingGalleryItems;
	private CollectionViewSource? _shadingGalleryItems;
	private CollectionViewSource? _shapeGalleryItems;

	private ColorBarGalleryItemViewModel? _automaticColorGalleryItemViewModel;
	private ColorBarGalleryItemViewModel? _noShadingColorGalleryItemViewModel;

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
	/// </summary>
	private ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel {
		get => _automaticColorGalleryItemViewModel ??= new ColorBarGalleryItemViewModel(Colors.Black, category: string.Empty, "Automatic") {
			LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
		};
	}

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for border gallery item view models.
	/// </summary>
	private CollectionViewSource BorderGalleryItems
		=> _borderGalleryItems ??= CreateBorderBarGalleryItemViewModelsCollectionViewSource();

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for bullet gallery item view models.
	/// </summary>
	private CollectionViewSource BulletGalleryItems
		=> _bulletGalleryItems ??= BulletBarGalleryItemViewModel.CreateDefaultCollectionViewSource(categorize: true);

	/// <summary>
	/// Creates a categorized <see cref="CollectionViewSource"/> of gallery item view models representing a number of border styles, intended for use in a gallery.
	/// </summary>
	private CollectionViewSource CreateBorderBarGalleryItemViewModelsCollectionViewSource() {
		return BarGalleryViewModel.CreateCollectionViewSource(
			new BorderBarGalleryItemViewModel[] {
				// Edge Borders
				new(BorderKind.Bottom, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Bottom Border")
					{ KeyTipText = "B", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderBottomGalleryItem, BarImageSize.Small) },
				new(BorderKind.Top, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Top Border")
					{ KeyTipText = "T", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderTopGalleryItem, BarImageSize.Small) },
				new(BorderKind.Left, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Left Border")
					{ KeyTipText = "L", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderLeftGalleryItem, BarImageSize.Small) },
				new(BorderKind.Right, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Right Border")
					{ KeyTipText = "R", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderRightGalleryItem, BarImageSize.Small) },

				// Other Borders
				new(BorderKind.None, BorderBarGalleryItemViewModel.OtherBordersCategory, "No Border")
					{ KeyTipText = "N", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderNoneGalleryItem, BarImageSize.Small) },
				new(BorderKind.All, BorderBarGalleryItemViewModel.OtherBordersCategory, "All Borders")
					{ KeyTipText = "A", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderAllGalleryItem, BarImageSize.Small) },
				new(BorderKind.Outside, BorderBarGalleryItemViewModel.OtherBordersCategory, "Outside Borders")
					{ KeyTipText = "O", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderOutsideGalleryItem, BarImageSize.Small) },
				new(BorderKind.Inside, BorderBarGalleryItemViewModel.OtherBordersCategory, "Inside Borders")
					{ KeyTipText = "I", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderInsideGalleryItem, BarImageSize.Small) },
			},
			categorize: true
		);
	}

	/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
	private CollectionViewSource CreateFontColorPickerBarGalleryItemViewModelsCollectionViewSource() {
		return BarGalleryViewModel.CreateCollectionViewSource(
			new ColorBarGalleryItemViewModel[] {
				AutomaticColorGalleryItemViewModel
			}.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()),
			categorize: true
		);
	}

	/// <inheritdoc cref="FontFamilyBarGalleryItemViewModel.CreateDefaultCollectionViewSource" />
	private static CollectionViewSource CreateFontFamilyBarGalleryItemViewModelsCollectionViewSource() {
		const string RecentlyUsedCategory = "Recently-Used Fonts";

		return BarGalleryViewModel.CreateCollectionViewSource(
			new FontFamilyBarGalleryItemViewModel[] {
				new(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
			}.Concat(FontFamilyBarGalleryItemViewModel.CreateDefaultCollection()),
			categorize: true
		);
	}

	/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
	private CollectionViewSource CreateShadingColorPickerBarGalleryItemViewModelsCollectionViewSource() {
		return BarGalleryViewModel.CreateCollectionViewSource(
			ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()
				.Concat([
					NoShadingColorGalleryItemViewModel
				]),
			categorize: true
		);
	}

	/// <summary>
	/// Creates a default collection of gallery item view models representing a number of symbols, intended for use in a gallery.
	/// </summary>
	public static IEnumerable<SymbolBarGalleryItemViewModel> CreateSymbolBarGalleryItemViewModelsCollection() {
		return [
			new("\u20AC") { Label = "Euro Sign" },
			new("\u00A3") { Label = "Pound Sign" },
			new("\u00A5") { Label = "Yen Sign" },
			new("\u00A9") { Label = "Copyright Sign" },
			new("\u00AE") { Label = "Registered Sign" },
			new("\u2122") { Label = "Trademark Sign" },
			new("\u00B1") { Label = "Plus-Minus Sign" },
			new("\u2248") { Label = "Almost Equal To" },
			new("\u2260") { Label = "Not Equal To" },
			new("\u2264") { Label = "Less-Than or Equal To" },
			new("\u2265") { Label = "Greater-Than or Equal To" },
			new("\u00F7") { Label = "Division Sign" },
			new("\u00D7") { Label = "Multiplication Sign" },
			new("\u221E") { Label = "Infinity" },
			new("\u00B5") { Label = "Micro Sign" },
			new("\u03B1") { Label = "Greek Small Letter Alpha" },
			new("\u03B2") { Label = "Greek Small Letter Beta" },
			new("\u03C0") { Label = "Greek Small Letter Pi" },
			new("\u2126") { Label = "Olm Sign" },
			new("\u2211") { Label = "N-Ary Summation" },
		];
	}

	/// <summary>
	/// Creates a default collection of gallery item view models representing a number of text styles, intended for use in a gallery.
	/// </summary>
	private static IEnumerable<TextStyleBarGalleryItemViewModel> CreateTextStyleBarGalleryItemViewModelsCollection() {
		return [
			new() { Label = "Normal", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) },
			new() { Label = "Heading 1", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading1FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
			new() { Label = "Heading 2", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading2FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
			new() { Label = "Heading 3", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading3FontSize, Color.FromArgb(0xff, 0x1f, 0x37, 0x63)) },
			new() { Label = "Heading 4", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) { Italic = true } },
			new() { Label = "Title", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.TitleFontSize, Colors.Black) },
			new() { Label = "Subtitle", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x5a, 0x5a, 0x5a)) },
			new() { Label = "Subtle Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
			new() { Label = "Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Italic = true } },
			new() { Label = "Intense Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x44, 0x72, 0xc4)) { Italic = true } },
			new() { Label = "Strong", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Bold = true } },
			new() { Label = "Quote", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
		];
	}

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for font color picker gallery item view models.
	/// </summary>
	private CollectionViewSource FontColorPickerGalleryItems
		=> _fontColorPickerGalleryItems ??= CreateFontColorPickerBarGalleryItemViewModelsCollectionViewSource();

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for font family gallery item view models.
	/// </summary>
	private CollectionViewSource FontFamilyGalleryItems
		=> _fontFamilyGalleryItems ??= CreateFontFamilyBarGalleryItemViewModelsCollectionViewSource();

	/// <summary>
	/// A <see cref="ColorBarGalleryItemViewModel"/> used to represent a no shading color.
	/// </summary>
	private ColorBarGalleryItemViewModel NoShadingColorGalleryItemViewModel {
		get => _noShadingColorGalleryItemViewModel ??= new ColorBarGalleryItemViewModel(Colors.Transparent, category: string.Empty, "No Color") {
			LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
		};
	}

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for numbering gallery item view models.
	/// </summary>
	private CollectionViewSource NumberingGalleryItems
		=> _numberingGalleryItems ??= NumberingBarGalleryItemViewModel.CreateDefaultCollectionViewSource(categorize: true);

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for shading gallery item view models.
	/// </summary>
	private CollectionViewSource ShadingGalleryItems
		=> _shadingGalleryItems ??= CreateShadingColorPickerBarGalleryItemViewModelsCollectionViewSource();

	/// <summary>
	/// The categorized <see cref="CollectionViewSource"/> for shape gallery item view models.
	/// </summary>
	private CollectionViewSource ShapeGalleryItems
		=> _shapeGalleryItems ??= ShapeBarGalleryItemViewModel.CreateDefaultCollectionViewSource(categorize: true);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="DataTemplateSelector"/> that will be assigned to <see cref="BarGalleryViewModelBase.ItemTemplateSelector"/>
	/// for each registered gallery view model.
	/// </summary>
	public BarGalleryItemTemplateSelector GalleryItemTemplateSelector { get; } = new CustomBarGalleryItemTemplateSelector();

}
