using ActiproSoftware.Windows.Controls.Bars;
using ActiproSoftware.Windows.Controls.Bars.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	partial class BarManager {

		private CollectionViewSource borderGalleryItems;
		private CollectionViewSource bulletGalleryItems;
		private CollectionViewSource fontColorPickerGalleryItems;
		private CollectionViewSource fontFamilyGalleryItems;
		private CollectionViewSource numberingGalleryItems;
		private CollectionViewSource shadingGalleryItems;
		private CollectionViewSource shapeGalleryItems;

		private ColorBarGalleryItemViewModel automaticColorGalleryItemViewModel;
		private ColorBarGalleryItemViewModel noShadingColorGalleryItemViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
		/// </summary>
		/// <value>A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.</value>
		private ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel {
			get {
				if (automaticColorGalleryItemViewModel == null) {
					automaticColorGalleryItemViewModel = new ColorBarGalleryItemViewModel(Colors.Black, category: string.Empty, "Automatic") {
						LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
					};
				}

				return automaticColorGalleryItemViewModel;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for border gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource BorderGalleryItems {
			get {
				if (borderGalleryItems is null)
					borderGalleryItems = CreateBorderBarGalleryItemViewModelsCollectionViewSource();
				return borderGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for bullet gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource BulletGalleryItems {
			get {
				if (bulletGalleryItems is null)
					bulletGalleryItems = BulletBarGalleryItemViewModel.CreateDefaultCollectionViewSource(categorize: true);
				return bulletGalleryItems;
			}
		}

		/// <summary>
		/// Creates a categorized <see cref="CollectionViewSource"/> of gallery item view models representing a number of border styles, intended for use in a gallery.
		/// </summary>
		/// <returns>The <see cref="CollectionViewSource"/> of gallery item view models that was created.</returns>
		private CollectionViewSource CreateBorderBarGalleryItemViewModelsCollectionViewSource() {
			return BarGalleryViewModel.CreateCollectionViewSource(new BorderBarGalleryItemViewModel[] {
				// Edge Borders
				new BorderBarGalleryItemViewModel(BorderKind.Bottom, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Bottom Border")
					{ KeyTipText = "B", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderBottomGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Top, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Top Border")
					{ KeyTipText = "T", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderTopGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Left, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Left Border")
					{ KeyTipText = "L", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderLeftGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Right, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Right Border")
					{ KeyTipText = "R", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderRightGalleryItem, BarImageSize.Small) },

				// Other Borders
				new BorderBarGalleryItemViewModel(BorderKind.None, BorderBarGalleryItemViewModel.OtherBordersCategory, "No Border")
					{ KeyTipText = "N", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderNoneGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.All, BorderBarGalleryItemViewModel.OtherBordersCategory, "All Borders")
					{ KeyTipText = "A", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderAllGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Outside, BorderBarGalleryItemViewModel.OtherBordersCategory, "Outside Borders")
					{ KeyTipText = "O", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderOutsideGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Inside, BorderBarGalleryItemViewModel.OtherBordersCategory, "Inside Borders")
					{ KeyTipText = "I", ImageSource = ImageProvider.GetImageSource(BarControlKeys.BorderInsideGalleryItem, BarImageSize.Small) },
			}, categorize: true);
		}

		/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
		private CollectionViewSource CreateFontColorPickerBarGalleryItemViewModelsCollectionViewSource() {
			return BarGalleryViewModel.CreateCollectionViewSource(
				new ColorBarGalleryItemViewModel[] {
					this.AutomaticColorGalleryItemViewModel
				}.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()),
			categorize: true);
		}

		/// <inheritdoc cref="FontFamilyBarGalleryItemViewModel.CreateDefaultCollectionViewSource" />
		private static CollectionViewSource CreateFontFamilyBarGalleryItemViewModelsCollectionViewSource() {
			const string RecentlyUsedCategory = "Recently-Used Fonts";

			return BarGalleryViewModel.CreateCollectionViewSource(
				new FontFamilyBarGalleryItemViewModel[] {
					new FontFamilyBarGalleryItemViewModel(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
				}.Concat(FontFamilyBarGalleryItemViewModel.CreateDefaultCollection()),
			categorize: true);
		}

		/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
		private CollectionViewSource CreateShadingColorPickerBarGalleryItemViewModelsCollectionViewSource() {
			return BarGalleryViewModel.CreateCollectionViewSource(
				ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()
					.Concat(new ColorBarGalleryItemViewModel[] {
						this.NoShadingColorGalleryItemViewModel
					}),
				categorize: true);
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of symbols, intended for use in a gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<SymbolBarGalleryItemViewModel> CreateSymbolBarGalleryItemViewModelsCollection() {
			return new SymbolBarGalleryItemViewModel[] {
				new SymbolBarGalleryItemViewModel("\u20AC") { Label = "Euro Sign" },
				new SymbolBarGalleryItemViewModel("\u00A3") { Label = "Pound Sign" },
				new SymbolBarGalleryItemViewModel("\u00A5") { Label = "Yen Sign" },
				new SymbolBarGalleryItemViewModel("\u00A9") { Label = "Copyright Sign" },
				new SymbolBarGalleryItemViewModel("\u00AE") { Label = "Registered Sign" },
				new SymbolBarGalleryItemViewModel("\u2122") { Label = "Trademark Sign" },
				new SymbolBarGalleryItemViewModel("\u00B1") { Label = "Plus-Minus Sign" },
				new SymbolBarGalleryItemViewModel("\u2248") { Label = "Almost Equal To" },
				new SymbolBarGalleryItemViewModel("\u2260") { Label = "Not Equal To" },
				new SymbolBarGalleryItemViewModel("\u2264") { Label = "Less-Than or Equal To" },
				new SymbolBarGalleryItemViewModel("\u2265") { Label = "Greater-Than or Equal To" },
				new SymbolBarGalleryItemViewModel("\u00F7") { Label = "Division Sign" },
				new SymbolBarGalleryItemViewModel("\u00D7") { Label = "Multiplication Sign" },
				new SymbolBarGalleryItemViewModel("\u221E") { Label = "Infinity" },
				new SymbolBarGalleryItemViewModel("\u00B5") { Label = "Micro Sign" },
				new SymbolBarGalleryItemViewModel("\u03B1") { Label = "Greek Small Letter Alpha" },
				new SymbolBarGalleryItemViewModel("\u03B2") { Label = "Greek Small Letter Beta" },
				new SymbolBarGalleryItemViewModel("\u03C0") { Label = "Greek Small Letter Pi" },
				new SymbolBarGalleryItemViewModel("\u2126") { Label = "Olm Sign" },
				new SymbolBarGalleryItemViewModel("\u2211") { Label = "N-Ary Summation" },
			};
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of text styles, intended for use in a gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		private static IEnumerable<TextStyleBarGalleryItemViewModel> CreateTextStyleBarGalleryItemViewModelsCollection() {
			return new TextStyleBarGalleryItemViewModel[] {
				new TextStyleBarGalleryItemViewModel() { Label = "Normal", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 1", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading1FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 2", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading2FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 3", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading3FontSize, Color.FromArgb(0xff, 0x1f, 0x37, 0x63)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 4", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Title", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.TitleFontSize, Colors.Black) },
				new TextStyleBarGalleryItemViewModel() { Label = "Subtitle", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x5a, 0x5a, 0x5a)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Subtle Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Intense Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x44, 0x72, 0xc4)) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Strong", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Bold = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Quote", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
			};
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for font color picker gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource FontColorPickerGalleryItems {
			get {
				if (fontColorPickerGalleryItems is null)
					fontColorPickerGalleryItems = CreateFontColorPickerBarGalleryItemViewModelsCollectionViewSource();
				return fontColorPickerGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for font family gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource FontFamilyGalleryItems {
			get {
				if (fontFamilyGalleryItems is null)
					fontFamilyGalleryItems = CreateFontFamilyBarGalleryItemViewModelsCollectionViewSource();
				return fontFamilyGalleryItems;
			}
		}

		/// <summary>
		/// Gets a <see cref="ColorBarGalleryItemViewModel"/> used to represent a no shading color.
		/// </summary>
		/// <value>A <see cref="ColorBarGalleryItemViewModel"/> used to represent a no shading color.</value>
		private ColorBarGalleryItemViewModel NoShadingColorGalleryItemViewModel {
			get {
				if (noShadingColorGalleryItemViewModel == null) {
					noShadingColorGalleryItemViewModel = new ColorBarGalleryItemViewModel(Colors.Transparent, category: string.Empty, "No Color") {
						LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
					};
				}

				return noShadingColorGalleryItemViewModel;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for numbering gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource NumberingGalleryItems {
			get {
				if (numberingGalleryItems is null)
					numberingGalleryItems = NumberingBarGalleryItemViewModel.CreateDefaultCollectionViewSource(categorize: true);
				return numberingGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for shading gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource ShadingGalleryItems {
			get {
				if (shadingGalleryItems is null)
					shadingGalleryItems = CreateShadingColorPickerBarGalleryItemViewModelsCollectionViewSource();
				return shadingGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="CollectionViewSource"/> for shape gallery item view models.
		/// </summary>
		/// <value>A <see cref="CollectionViewSource"/>.</value>
		private CollectionViewSource ShapeGalleryItems {
			get {
				if (shapeGalleryItems is null)
					shapeGalleryItems = ShapeBarGalleryItemViewModel.CreateDefaultCollectionViewSource(categorize: true);
				return shapeGalleryItems;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="System.Windows.Controls.DataTemplateSelector"/> that will be assigned to <see cref="BarGalleryViewModelBase.ItemTemplateSelector"/>
		/// for each registered gallery view model.
		/// </summary>
		/// <value>The <see cref="System.Windows.Controls.DataTemplateSelector"/> that picks a <see cref="System.Windows.Controls.DataTemplateSelector"/> used to display the content for each gallery item.</value>
		public BarGalleryItemTemplateSelector GalleryItemTemplateSelector { get; } = new CustomBarGalleryItemTemplateSelector();
	
	}

}
