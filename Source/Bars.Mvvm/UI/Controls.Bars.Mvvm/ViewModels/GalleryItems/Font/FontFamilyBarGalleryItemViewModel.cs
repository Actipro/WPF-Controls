using ActiproSoftware.Products.Bars.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a font family.
	/// </summary>
	public class FontFamilyBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		private const string CalibriFontFamilyName = "Calibri";
		internal const string DefaultFontFamilyName = CalibriFontFamilyName;

		private string name;

		private static ReadOnlyCollection<FontFamilyBarGalleryItemViewModel> cachedDefaultCollection;
		private static int[] knownFontWeights;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public FontFamilyBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(DefaultFontFamilyName) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name.
		/// </summary>
		/// <param name="name">The font family name.</param>
		public FontFamilyBarGalleryItemViewModel(string name)
			: this(SR.GetString(SRName.UIGalleryItemCategoryAllFontsText), name) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified category and name.
		/// </summary>
		/// <param name="category">The gallery item category.</param>
		/// <param name="name">The font family name.</param>
		public FontFamilyBarGalleryItemViewModel(string category, string name)
			: base(category) {

			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException(nameof(name));

			this.name = name;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the given <see cref="FontWeight"/> is one of the known <see cref="FontWeights"/> values.
		/// </summary>
		/// <param name="fontWeight">The value to examine.</param>
		/// <returns>
		/// <c>true</c> if the <see cref="FontWeight"/> is one of the known <see cref="FontWeights"/> values; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsKnownFontWeight(FontWeight fontWeight) {
			knownFontWeights ??= new int[] {
				100, // Thin
				200, // ExtraLight or UltraLight
				300, // Light
				400, // Normal or Regular
				500, // Medium
				600, // DemiBold or SemiBold
				700, // Bold
				800, // ExtraBold or UltraBold
				900, // Black or Heavy
				950, // ExtraBlock or UltraBlack
			};

			return knownFontWeights.Contains(fontWeight.ToOpenTypeWeight());
		}

		/// <summary>
		/// Tests if the given <see cref="FamilyTypeface"/> is recognized as the "Regular" type face for a font family.
		/// </summary>
		/// <param name="typeFace">The value to examine.</param>
		/// <returns><c>true</c> if the <see cref="FamilyTypeface"/> is recognized as the "Regular" type face for a font family; otherwise, <c>false</c>.</returns>
		private static bool IsRegularFontFamilyTypeface(FamilyTypeface typeFace) {
			return typeFace.Stretch == FontStretches.Normal
				&& typeFace.Style == FontStyles.Normal
				&& typeFace.Weight == FontWeights.Normal;
		}

		/// <summary>
		/// Tests if the given <see cref="FamilyTypeface"/> should be included when generating the default collection.
		/// </summary>
		/// <param name="typeFace">The value to examine.</param>
		/// <returns><c>true</c> if the <see cref="FamilyTypeface"/> should be included; otherwise <c>false</c>.</returns>
		private static bool ShouldIncludeFontFamilyTypeface(FamilyTypeface typeFace) {
			// Exclude slanted styles (e.g., Italic)
			if (typeFace.Style != FontStyles.Normal)
				return false;

			// Exclude standard Bold weight
			if (typeFace.Weight == FontWeights.Bold)
				return false;

			// Exclude any font weight value that are not known by WPF (e.g., ignore "350" for SemiLight since WPF does not define it)
			if (!IsKnownFontWeight(typeFace.Weight))
				return false;

			// Include all other typefaces
			return true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing the installed font families.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<FontFamilyBarGalleryItemViewModel> CreateDefaultCollection() {
			// Reading font data is a performance-sensitive operation, so the initial results will be
			// cached and re-used on subsequent calls to this method.
			if (cachedDefaultCollection is null) {

				ICollection<FontFamily> fontFamilies = null;
				try {
					// Workaround for problem triggered by KB4055532
					fontFamilies = Fonts.SystemFontFamilies;
				}
				catch { }

				var list = new List<FontFamilyBarGalleryItemViewModel>();
				if (fontFamilies is not null) {
					var allFontsCategory = SR.GetString(SRName.UIGalleryItemCategoryAllFontsText);

					var fontFamilyTypeFaceNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
					foreach (var fontFamily in fontFamilies) {

						fontFamilyTypeFaceNames.Clear();
						bool hasRegularTypeFace = false;
						foreach (var typeFace in fontFamily.FamilyTypefaces.Where(ShouldIncludeFontFamilyTypeface)) {

							var fontName = fontFamily.Source;
							if (IsRegularFontFamilyTypeface(typeFace)) {
								hasRegularTypeFace = true;
							}
							else {
								// Append the variant name for non-regular typeface
								var adjustedName = typeFace.AdjustedFaceNames?.FirstOrDefault().Value;
								if (!string.IsNullOrEmpty(adjustedName))
									fontName += " " + adjustedName;
							}

							// Cache the name if not a duplicate
							if (!fontFamilyTypeFaceNames.Contains(fontName))
								fontFamilyTypeFaceNames.Add(fontName);
						}

						if (hasRegularTypeFace) {
							// Include each font variant
							list.AddRange(fontFamilyTypeFaceNames.Select(fontName => new FontFamilyBarGalleryItemViewModel(allFontsCategory, fontName)));
						}
						else {
							// Ignore additional typeface variants if one of those was not "regular"
							list.Add(new FontFamilyBarGalleryItemViewModel(allFontsCategory, fontFamily.Source));
						}
					}
				}

				// Final, combined list of view models should be pre-sorted by label and wrapped as read-only
				cachedDefaultCollection = list.OrderBy(x => x.Label).ToList().AsReadOnly();
			}

			return cachedDefaultCollection
				?? Enumerable.Empty<FontFamilyBarGalleryItemViewModel>();
		}

		/// <summary>
		/// Gets or sets the text label to display.
		/// </summary>
		/// <value>The text label to display.</value>
		/// <remarks>If the label is not explicitly defined, the current <see cref="Name"/> value will be used as the label.</remarks>
		public override string Label {
			get => base.Label ?? Name;
			set => base.Label = value;
		}

		/// <summary>
		/// Gets or sets the font family name.
		/// </summary>
		/// <value>The font family name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				if (name != value) {
					name = value;
					this.NotifyPropertyChanged(nameof(Name));
					this.NotifyPropertyChanged(nameof(Label));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Name={this.Name}]";
		}

	}

}
