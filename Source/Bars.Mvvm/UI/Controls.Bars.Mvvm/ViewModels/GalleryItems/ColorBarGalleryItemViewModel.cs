namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents a gallery item view model for a color.
/// </summary>
public class ColorBarGalleryItemViewModel : BarGalleryItemViewModel<Color> {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ColorBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
		: this(default) { }

	/// <summary>
	/// Initializes an instance of the class with the specified color.
	/// </summary>
	/// <param name="value">The color represented by the gallery item.</param>
	public ColorBarGalleryItemViewModel(Color value)
		: this(value, category: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified color and category.
	/// </summary>
	/// <param name="value">The color represented by the gallery item.</param>
	/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
	public ColorBarGalleryItemViewModel(Color value, string? category)
		: this(value, category, label: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified color, category, and label.
	/// </summary>
	/// <param name="value">The color represented by the gallery item.</param>
	/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
	/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
	public ColorBarGalleryItemViewModel(Color value, string? category, string? label)
		: base(value, category, label) { }

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Appends color shade view models.
	/// </summary>
	/// <param name="viewModels">The collection to update.</param>
	/// <param name="baseViewModels">The base color gallery item view models to examine.</param>
	private static void AppendColorShadeViewModels(List<ColorBarGalleryItemViewModel> viewModels, IList<ColorBarGalleryItemViewModel> baseViewModels) {
		if (baseViewModels is { Count: > 0 }) {
			// Add base colors
			var shadeDictionary = new Dictionary<ColorBarGalleryItemViewModel, IList<NamedColorShade>>();
			foreach (var sourceViewModel in baseViewModels!.Where(x => x is not null)) {
				viewModels.Add(sourceViewModel);

				shadeDictionary[sourceViewModel] = ColorShadeGenerator.Generate(sourceViewModel.Value, sourceViewModel.Label ?? sourceViewModel.Value.ToString());
			}

			// Add shade colors
			var shadeCount = shadeDictionary.First().Value.Count;
			for (var shadeIndex = 0; shadeIndex < shadeCount; shadeIndex++) {
				foreach (var sourceViewModel in baseViewModels) {
					var shade = shadeDictionary[sourceViewModel][shadeIndex];

					var shadeViewModel = new ColorBarGalleryItemViewModel(shade.Color, sourceViewModel.Category, shade.Name) {
						LayoutBehavior = (shadeIndex == 0)
							? BarGalleryItemLayoutBehavior.GroupStart
							: (shadeIndex == shadeCount - 1 ? BarGalleryItemLayoutBehavior.GroupEnd : BarGalleryItemLayoutBehavior.GroupInner)
					};

					viewModels.Add(shadeViewModel);
				}
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a default collection of gallery item view models representing a number of standard colors and their various shades, intended for use in a color picker gallery.
	/// </summary>
	/// <returns>The collection of gallery item view models that was created.</returns>
	public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultColorPickerCollection() {
		var viewModels = new List<ColorBarGalleryItemViewModel>();

		var standardColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryStandardColorsText);
		var themeColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryThemeColorsText);

		AppendColorShadeViewModels(viewModels,
			[
				new(UIColor.FromWebColor("#ffffff").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorWhiteText)),
				new(UIColor.FromWebColor("#000000").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlackText)),
				new(UIColor.FromWebColor("#e7e6e6").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorLightGrayText)),
				new(UIColor.FromWebColor("#44546a").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueGrayText)),
				new(UIColor.FromWebColor("#4472c4").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new(UIColor.FromWebColor("#ed7d31").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorOrangeText)),
				new(UIColor.FromWebColor("#a5a5a5").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorGrayText)),
				new(UIColor.FromWebColor("#ffc000").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorGoldText)),
				new(UIColor.FromWebColor("#5b9bd5").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new(UIColor.FromWebColor("#70ad47").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorGreenText)),
			]
		);

		viewModels.AddRange(
			[
				new(UIColor.FromWebColor("#c00000").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorDarkRedText)),
				new(UIColor.FromWebColor("#ff0000").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorRedText)),
				new(UIColor.FromWebColor("#ffc000").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorOrangeText)),
				new(UIColor.FromWebColor("#ffff00").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorYellowText)),
				new(UIColor.FromWebColor("#92d050").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorLightGreenText)),
				new(UIColor.FromWebColor("#00b050").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorGreenText)),
				new(UIColor.FromWebColor("#00b0f0").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorLightBlueText)),
				new(UIColor.FromWebColor("#0070c0").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new(UIColor.FromWebColor("#002060").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorDarkBlueText)),
				new(UIColor.FromWebColor("#7030a0").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorPurpleText)),
			]
		);

		return viewModels;
	}

	/// <summary>
	/// Creates a default collection of gallery item view models representing a number of background highlight colors, intended for use in a text highlight gallery.
	/// </summary>
	/// <returns>The collection of gallery item view models that was created.</returns>
	public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultTextHighlightCollection() {
		var category = SR.GetString(SRName.UIGalleryItemCategoryTextHighlightColorsText);

		return [
			new(Colors.Yellow, category, SR.GetString(SRName.UINamedColorYellowText)) { KeyTipText = "Y" },
			new(Colors.Lime, category, SR.GetString(SRName.UINamedColorBrightGreenText)) { KeyTipText = "L" },
			new(Colors.Cyan, category, SR.GetString(SRName.UINamedColorTurquoiseText)) { KeyTipText = "C" },
			new(Colors.Magenta, category, SR.GetString(SRName.UINamedColorPinkText)) { KeyTipText = "M" },
			new(Colors.Blue, category, SR.GetString(SRName.UINamedColorBlueText)) { KeyTipText = "B" },
			new(Colors.Red, category, SR.GetString(SRName.UINamedColorRedText)) { KeyTipText = "R" },
			new(Colors.Navy, category, SR.GetString(SRName.UINamedColorDarkBlueText)) { KeyTipText = "V" },
			new(Colors.Teal, category, SR.GetString(SRName.UINamedColorTealText)) { KeyTipText = "T" },
			new(Colors.Green, category, SR.GetString(SRName.UINamedColorGreenText)) { KeyTipText = "E" },
			new(Colors.Purple, category, SR.GetString(SRName.UINamedColorVioletText)) { KeyTipText = "P" },
			new(Colors.Maroon, category, SR.GetString(SRName.UINamedColorDarkRedText)) { KeyTipText = "N" },
			new(Colors.Olive, category, SR.GetString(SRName.UINamedColorDarkYellowText)) { KeyTipText = "O" },
			new(Colors.Gray, category, SR.GetString(SRName.UINamedColorGray50Text)) { KeyTipText = "G" },
			new(Colors.Silver, category, SR.GetString(SRName.UINamedColorGray25Text)) { KeyTipText = "I" },
			new(Colors.Black, category, SR.GetString(SRName.UINamedColorBlackText)) { KeyTipText = "K" },
		];
	}

	/// <summary>
	/// Create a collection of gallery item view models representing the shades of colors from the specified base color gallery item view models.
	/// </summary>
	/// <param name="baseViewModels">The base color gallery item view models to examine.</param>
	/// <returns>The collection of gallery item view models that was created.</returns>
	public static IEnumerable<ColorBarGalleryItemViewModel> CreateShadedCollection(params ColorBarGalleryItemViewModel[] baseViewModels) {
		var viewModels = new List<ColorBarGalleryItemViewModel>();

		AppendColorShadeViewModels(viewModels, baseViewModels);

		return viewModels;
	}

}
