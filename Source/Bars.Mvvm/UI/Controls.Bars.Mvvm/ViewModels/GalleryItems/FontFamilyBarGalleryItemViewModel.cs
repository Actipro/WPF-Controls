using ActiproSoftware.Products.Bars.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a font family.
	/// </summary>
	public class FontFamilyBarGalleryItemViewModel : BarGalleryItemViewModel<string> {

		private const string CalibriFontFamilyName = "Calibri";
		internal const string DefaultFontFamilyName = CalibriFontFamilyName;

		private static ReadOnlyCollection<FontFamilyBarGalleryItemViewModel> cachedDefaultCollection;
		private static int[] knownFontWeights;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default font family name and category.
		/// </summary>
		public FontFamilyBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(DefaultFontFamilyName) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name and a default category.
		/// </summary>
		/// <param name="value">The font family name.</param>
		public FontFamilyBarGalleryItemViewModel(string value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name and category.
		/// </summary>
		/// <param name="value">The font family name.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public FontFamilyBarGalleryItemViewModel(string value, string category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name, category, and label.
		/// </summary>
		/// <param name="value">The font family name.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public FontFamilyBarGalleryItemViewModel(string value, string category, string label)
			: base(ValidateFontFamilyName(value, nameof(value)), category, label) { }

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

		/// <summary>
		/// Tests if the given font family name is valid and throws <see cref="ArgumentException"/> if invalid.
		/// </summary>
		/// <param name="name">The value to be tested.</param>
		/// <param name="paramName">The name of the parameter which defined the value.</param>
		/// <exception cref="ArgumentException">Throw if the given name is not valid.</exception>
		/// <returns>Returns the font family name if an exception is not thrown.</returns>
		private static string ValidateFontFamilyName(string name, string paramName) {
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Font family name cannot be null, empty, or filled with white space.", paramName);

			return name;
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
							list.AddRange(fontFamilyTypeFaceNames.Select(fontName => new FontFamilyBarGalleryItemViewModel(fontName, allFontsCategory)));
						}
						else {
							// Ignore additional typeface variants if one of those was not "regular"
							list.Add(new FontFamilyBarGalleryItemViewModel(fontFamily.Source, allFontsCategory));
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
		/// Creates a <see cref="CollectionViewSource"/> of gallery item view models representing the installed font families, allowing for possible categorization and filtering.
		/// </summary>
		/// <param name="categorize">Whether the collection view source should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
		/// <returns>The <see cref="CollectionViewSource"/> of gallery item view models that was created.</returns>
		public static CollectionViewSource CreateDefaultCollectionViewSource(bool categorize)
			=> BarGalleryViewModel.CreateCollectionViewSource(CreateDefaultCollection(), categorize);

		/// <summary>
		/// Gets the localizable default category to be used for view models of this type.
		/// </summary>
		/// <value>The string category name.</value>
		public static string DefaultCategory => SR.GetString(SRName.UIGalleryItemCategoryAllFontsText);

		/// <inheritdoc/>
		public override string Value {
			get => base.Value;
			set => base.Value = ValidateFontFamilyName(value, nameof(value));
		}

	}

}
