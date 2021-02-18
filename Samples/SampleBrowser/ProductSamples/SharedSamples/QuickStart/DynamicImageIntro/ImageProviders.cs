using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.DynamicImageIntro {

	/// <summary>
	/// Provides access to <see cref="ImageProvider"/> objects for this sample.
	/// </summary>
	public static class ImageProviders {

		private static ImageProvider highDpiWithAnyDarkThemeVariation;
		private static ImageProvider highDpiWithDarkThemeChromaticAdaptation;
		private static ImageProvider highDpiWithChromaticAdaptation;
		private static ImageProvider highDpiWithSpecificDarkThemeVariation;
		private static ImageProvider foregroundChanging;
		private static ImageProvider normal;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Implements an <see cref="ImageProvider"/>
		/// </summary>
		private class HighDpiWithAnyDarkThemeVariationImageProvider : ImageProvider {

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Initializes a new instance of the <c>HighDpiWithAnyDarkThemeVariationImageProvider</c> class. 
			/// </summary>
			public HighDpiWithAnyDarkThemeVariationImageProvider() {
				this.Scales.Add(2.0);  // 200%
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// Creates a theme name-oriented path part as appropriate for raster images whose sources have a <see cref="Uri"/>.
			/// </summary>
			/// <param name="themeName">The theme name to examine.</param>
			/// <returns>The theme name-oriented path part that was generated.</returns>
			/// <remarks>
			/// This method is called by <see cref="TransformBaseImageUriSource"/> to assist in building the <see cref="Uri"/> for locating a theme name-oriented variant of an image.
			/// The default implementation of this method returns an empty string if the <paramref name="themeName"/> value is not within the <see cref="ThemeNames"/> collection.
			/// If the <paramref name="themeName"/> is found within the collection, a string such as "Theme-Dark" would be returned for a theme name "Dark".
			/// Note that spaces in the theme name are removed.
			/// </remarks>
			/// <seealso cref="ThemeNames"/>
			/// <seealso cref="TransformBaseImageUriSource"/>
			protected override string GetThemeNamePathPart(string themeName) {
				if (this.HasThemeVariation(themeName))
					return "Theme-AnyDark";

				return string.Empty;
			}

			/// <summary>
			/// Returns whether the provider has any raster image variations for the specified theme name.
			/// </summary>
			/// <param name="themeName">The theme name to examine.</param>
			/// <returns>
			/// <c>true</c> if the provider has any raster image variations for the specified theme name; otherwise, <c>false</c>.
			/// </returns>
			protected override bool HasThemeVariation(string themeName) {
				// This provider example doesn't include specific theme names and instead, routes to a specific "AnyDark" variation if the theme's intent is dark/black
				return ThemeManager.IsDarkTheme(themeName);
			}

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the <see cref="ImageProvider"/> that supports high DPI images and pre-defined variations for any dark theme.
		/// </summary>
		/// <value>The <see cref="ImageProvider"/> that supports high DPI images and pre-defined variations for any dark theme.</value>
		public static ImageProvider HighDpiWithAnyDarkThemeVariation {
			get {
				if (highDpiWithAnyDarkThemeVariation == null)
					highDpiWithAnyDarkThemeVariation = new HighDpiWithAnyDarkThemeVariationImageProvider();

				return highDpiWithAnyDarkThemeVariation;
			}
		}

		/// <summary>
		/// Gets the <see cref="ImageProvider"/> that supports high DPI images and chromatic adaptation for dark themes.
		/// </summary>
		/// <value>The <see cref="ImageProvider"/> that supports high DPI images and chromatic adaptation for dark themes.</value>
		public static ImageProvider HighDpiWithDarkThemeChromaticAdaptation {
			get {
				if (highDpiWithDarkThemeChromaticAdaptation == null) {
					highDpiWithDarkThemeChromaticAdaptation = new ImageProvider() {
						ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes,
						Scales = { 2.0 }  // 200%
					};
				}

				return highDpiWithDarkThemeChromaticAdaptation;
			}
		}

		/// <summary>
		/// Gets the <see cref="ImageProvider"/> that supports high DPI images and chromatic adaptation.
		/// </summary>
		/// <value>The <see cref="ImageProvider"/> that supports high DPI images and chromatic adaptation.</value>
		public static ImageProvider HighDpiWithChromaticAdaptation {
			get {
				if (highDpiWithChromaticAdaptation == null) {
					highDpiWithChromaticAdaptation = new ImageProvider() {
						ChromaticAdaptationMode = ImageChromaticAdaptationMode.Always,
						Scales = { 2.0 }  // 200%
					};
				}

				return highDpiWithChromaticAdaptation;
			}
		}

		/// <summary>
		/// Gets the <see cref="ImageProvider"/> that supports high DPI images and pre-defined variations for the "Dark" theme only.
		/// </summary>
		/// <value>The <see cref="ImageProvider"/> that supports high DPI images and pre-defined variations for the "Dark" theme only.</value>
		public static ImageProvider HighDpiWithSpecificDarkThemeVariation {
			get {
				if (highDpiWithSpecificDarkThemeVariation == null) {
					highDpiWithSpecificDarkThemeVariation = new ImageProvider() {
						ThemeNames = { ThemeNames.Dark },
						Scales = { 2.0 }  // 200%
					};
				}

				return highDpiWithSpecificDarkThemeVariation;
			}
		}

		/// <summary>
		/// Gets the <see cref="ImageProvider"/> that can be used on images with a single color to adjust their foreground.
		/// </summary>
		/// <value>The <see cref="ImageProvider"/> that can be used on images with a single color to adjust their foreground.</value>
		public static ImageProvider ForegroundChanging {
			get {
				if (foregroundChanging == null) {
					foregroundChanging = new ImageProvider() {
						DesignForegroundColor = Colors.Black
					};
				}

				return foregroundChanging;
			}
		}

		/// <summary>
		/// Gets the <see cref="ImageProvider"/> that has no special options set.
		/// </summary>
		/// <value>The <see cref="ImageProvider"/> that has no special options set.</value>
		public static ImageProvider Normal {
			get {
				if (normal == null)
					normal = new ImageProvider();

				return normal;
			}
		}

	}

}
