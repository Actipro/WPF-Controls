using ActiproSoftware.Products.Bars.Mvvm;
using ActiproSoftware.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a color.
	/// </summary>
	public class ColorBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		private Color color;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ColorBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(category: null, Colors.Red, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified category, color, and label.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="color">The color represented by the gallery item.</param>
		/// <param name="label">The text label to display.</param>
		public ColorBarGalleryItemViewModel(string category, Color color, string label) : base(category) {
			this.color = color;
			this.Label = label;
		}
		
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

					shadeDictionary[sourceViewModel] = ColorShadeGenerator.Generate(sourceViewModel.Color, sourceViewModel.Label);
				}

				// Add shade colors
				var shadeCount = shadeDictionary.First().Value.Count;
				for (var shadeIndex = 0; shadeIndex < shadeCount; shadeIndex++) {
					foreach (var sourceViewModel in baseViewModels) {
						var shade = shadeDictionary[sourceViewModel][shadeIndex];

						var shadeViewModel = new ColorBarGalleryItemViewModel(sourceViewModel.Category, shade.Color, shade.Name);

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
		/// Gets or sets the color represented by the gallery item.
		/// </summary>
		/// <value>The color represented by the gallery item.</value>
		public Color Color {
			get {
				return color;
			}
			set {
				if (color != value) {
					color = value;
					this.NotifyPropertyChanged(nameof(Color));
				}
			}
		}
		
		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of standard colors and their various shades, intended for use in a color picker gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultColorPickerCollection() {
			var viewModels = new List<ColorBarGalleryItemViewModel>();

			var standardColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryStandardColorsText);
			var themeColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryThemeColorsText);

			AppendColorShadeViewModels(viewModels, new ColorBarGalleryItemViewModel[] {
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#ffffff").ToColor(), SR.GetString(SRName.UINamedColorWhiteText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#000000").ToColor(), SR.GetString(SRName.UINamedColorBlackText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#e7e6e6").ToColor(), SR.GetString(SRName.UINamedColorLightGrayText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#44546a").ToColor(), SR.GetString(SRName.UINamedColorBlueGrayText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#4472c4").ToColor(), SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#ed7d31").ToColor(), SR.GetString(SRName.UINamedColorOrangeText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#a5a5a5").ToColor(), SR.GetString(SRName.UINamedColorGrayText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#ffc000").ToColor(), SR.GetString(SRName.UINamedColorGoldText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#5b9bd5").ToColor(), SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(themeColorsCategory, UIColor.FromWebColor("#70ad47").ToColor(), SR.GetString(SRName.UINamedColorGreenText)),
			});

			viewModels.AddRange(new ColorBarGalleryItemViewModel[] {
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#c00000").ToColor(), SR.GetString(SRName.UINamedColorDarkRedText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#ff0000").ToColor(), SR.GetString(SRName.UINamedColorRedText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#ffc000").ToColor(), SR.GetString(SRName.UINamedColorOrangeText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#ffff00").ToColor(), SR.GetString(SRName.UINamedColorYellowText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#92d050").ToColor(), SR.GetString(SRName.UINamedColorLightGreenText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#00b050").ToColor(), SR.GetString(SRName.UINamedColorGreenText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#00b0f0").ToColor(), SR.GetString(SRName.UINamedColorLightBlueText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#0070c0").ToColor(), SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#002060").ToColor(), SR.GetString(SRName.UINamedColorDarkBlueText)),
				new ColorBarGalleryItemViewModel(standardColorsCategory, UIColor.FromWebColor("#7030a0").ToColor(), SR.GetString(SRName.UINamedColorPurpleText)),
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
				new ColorBarGalleryItemViewModel(category, Colors.Yellow, SR.GetString(SRName.UINamedColorYellowText)),
				new ColorBarGalleryItemViewModel(category, Colors.Lime, SR.GetString(SRName.UINamedColorBrightGreenText)),
				new ColorBarGalleryItemViewModel(category, Colors.Cyan, SR.GetString(SRName.UINamedColorTurquoiseText)),
				new ColorBarGalleryItemViewModel(category, Colors.Magenta, SR.GetString(SRName.UINamedColorPinkText)),
				new ColorBarGalleryItemViewModel(category, Colors.Blue, SR.GetString(SRName.UINamedColorBlueText)),
				new ColorBarGalleryItemViewModel(category, Colors.Red, SR.GetString(SRName.UINamedColorRedText)),
				new ColorBarGalleryItemViewModel(category, Colors.Navy, SR.GetString(SRName.UINamedColorDarkBlueText)),
				new ColorBarGalleryItemViewModel(category, Colors.Teal, SR.GetString(SRName.UINamedColorTealText)),
				new ColorBarGalleryItemViewModel(category, Colors.Green, SR.GetString(SRName.UINamedColorGreenText)),
				new ColorBarGalleryItemViewModel(category, Colors.Purple, SR.GetString(SRName.UINamedColorVioletText)),
				new ColorBarGalleryItemViewModel(category, Colors.Maroon, SR.GetString(SRName.UINamedColorDarkRedText)),
				new ColorBarGalleryItemViewModel(category, Colors.Olive, SR.GetString(SRName.UINamedColorDarkYellowText)),
				new ColorBarGalleryItemViewModel(category, Colors.Gray, SR.GetString(SRName.UINamedColorGray50Text)),
				new ColorBarGalleryItemViewModel(category, Colors.Silver, SR.GetString(SRName.UINamedColorGray25Text)),
				new ColorBarGalleryItemViewModel(category, Colors.Black, SR.GetString(SRName.UINamedColorBlackText)),
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
		
		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Color='{this.Color}', Label='{this.Label}']";
		}

	}

}
