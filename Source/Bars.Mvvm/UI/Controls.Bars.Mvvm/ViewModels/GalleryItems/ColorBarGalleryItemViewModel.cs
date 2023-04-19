using ActiproSoftware.Products.Bars.Mvvm;
using ActiproSoftware.Windows.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a color.
	/// </summary>
	public class ColorBarGalleryItemViewModel : BarGalleryItemViewModel<Color> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ColorBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(default) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified color.
		/// </summary>
		/// <param name="value">The color represented by the gallery item.</param>
		public ColorBarGalleryItemViewModel(Color value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified color and category.
		/// </summary>
		/// <param name="value">The color represented by the gallery item.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public ColorBarGalleryItemViewModel(Color value, string category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified color, category, and label.
		/// </summary>
		/// <param name="value">The color represented by the gallery item.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public ColorBarGalleryItemViewModel(Color value, string category, string label)
			: base(value, category, label) { }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Appends color shade view models.
		/// </summary>
		/// <param name="viewModels">The collection to update.</param>
		/// <param name="baseViewModels">The base color gallery item view models to examine.</param>
		private static void AppendColorShadeViewModels(IList<ColorBarGalleryItemViewModel> viewModels, IList<ColorBarGalleryItemViewModel> baseViewModels) {
			var colorCount = baseViewModels?.Count ?? 0;
			if (colorCount > 0) {
				// Add base colors
				var shadeDictionary = new Dictionary<ColorBarGalleryItemViewModel, IList<NamedColorShade>>();
				foreach (var sourceViewModel in baseViewModels) {
					viewModels.Add(sourceViewModel);

					shadeDictionary[sourceViewModel] = ColorShadeGenerator.Generate(sourceViewModel.Value, sourceViewModel.Label);
				}

				// Add shade colors
				var shadeCount = shadeDictionary.First().Value.Count;
				for (var shadeIndex = 0; shadeIndex < shadeCount; shadeIndex++) {
					foreach (var sourceViewModel in baseViewModels) {
						var shade = shadeDictionary[sourceViewModel][shadeIndex];

						var shadeViewModel = new ColorBarGalleryItemViewModel(shade.Color, sourceViewModel.Category, shade.Name);

						shadeViewModel.LayoutBehavior = shadeIndex == 0 
							? BarGalleryItemLayoutBehavior.GroupStart : (
								shadeIndex == shadeCount - 1 
								? BarGalleryItemLayoutBehavior.GroupEnd 
								: BarGalleryItemLayoutBehavior.GroupInner
							);

						viewModels.Add(shadeViewModel);
					}
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of standard colors and their various shades, intended for use in a color picker gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultColorPickerCollection() {
			var viewModels = new List<ColorBarGalleryItemViewModel>();

			var standardColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryStandardColorsText);
			var themeColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryThemeColorsText);

			AppendColorShadeViewModels(viewModels, new ColorBarGalleryItemViewModel[] {
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffffff").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorWhiteText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#000000").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlackText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#e7e6e6").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorLightGrayText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#44546a").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueGrayText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#4472c4").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ed7d31").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorOrangeText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#a5a5a5").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorGrayText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffc000").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorGoldText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#5b9bd5").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#70ad47").ToColor(), themeColorsCategory, SR.GetString(SRName.UINamedColorGreenText)),
			});

			viewModels.AddRange(new ColorBarGalleryItemViewModel[] {
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#c00000").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorDarkRedText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ff0000").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorRedText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffc000").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorOrangeText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#ffff00").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorYellowText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#92d050").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorLightGreenText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00b050").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorGreenText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#00b0f0").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorLightBlueText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#0070c0").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#002060").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorDarkBlueText)),
				new ColorBarGalleryItemViewModel(UIColor.FromWebColor("#7030a0").ToColor(), standardColorsCategory, SR.GetString(SRName.UINamedColorPurpleText)),
			});

			return viewModels;
		}
		
		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of background highlight colors, intended for use in a text highlight gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultTextHighlightCollection() {
			var category = SR.GetString(SRName.UIGalleryItemCategoryTextHighlightColorsText);

			return new ColorBarGalleryItemViewModel[] {
				new ColorBarGalleryItemViewModel(Colors.Yellow, category, SR.GetString(SRName.UINamedColorYellowText)),
				new ColorBarGalleryItemViewModel(Colors.Lime, category, SR.GetString(SRName.UINamedColorBrightGreenText)),
				new ColorBarGalleryItemViewModel(Colors.Cyan, category, SR.GetString(SRName.UINamedColorTurquoiseText)),
				new ColorBarGalleryItemViewModel(Colors.Magenta, category, SR.GetString(SRName.UINamedColorPinkText)),
				new ColorBarGalleryItemViewModel(Colors.Blue, category, SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(Colors.Red, category, SR.GetString(SRName.UINamedColorRedText)),
				new ColorBarGalleryItemViewModel(Colors.Navy, category, SR.GetString(SRName.UINamedColorDarkBlueText)),
				new ColorBarGalleryItemViewModel(Colors.Teal, category, SR.GetString(SRName.UINamedColorTealText)),
				new ColorBarGalleryItemViewModel(Colors.Green, category, SR.GetString(SRName.UINamedColorGreenText)),
				new ColorBarGalleryItemViewModel(Colors.Purple, category, SR.GetString(SRName.UINamedColorVioletText)),
				new ColorBarGalleryItemViewModel(Colors.Maroon, category, SR.GetString(SRName.UINamedColorDarkRedText)),
				new ColorBarGalleryItemViewModel(Colors.Olive, category, SR.GetString(SRName.UINamedColorDarkYellowText)),
				new ColorBarGalleryItemViewModel(Colors.Gray, category, SR.GetString(SRName.UINamedColorGray50Text)),
				new ColorBarGalleryItemViewModel(Colors.Silver, category, SR.GetString(SRName.UINamedColorGray25Text)),
				new ColorBarGalleryItemViewModel(Colors.Black, category, SR.GetString(SRName.UINamedColorBlackText)),
			};
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

}
