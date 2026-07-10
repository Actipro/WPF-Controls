using ActiproSoftware.Windows.Media;
using ActiproSoftware.Windows.Themes;

namespace ActiproSoftware.ProductSamples.SharedSamples.QuickStart.DynamicImageIntro;

/// <summary>
/// Provides access to <see cref="ImageProvider"/> objects for this sample.
/// </summary>
public static class ImageProviders {

	private static ImageProvider? _highDpiWithAnyDarkThemeVariation;
	private static ImageProvider? _highDpiWithDarkThemeChromaticAdaptation;
	private static ImageProvider? _highDpiWithChromaticAdaptation;
	private static ImageProvider? _highDpiWithSpecificDarkThemeVariation;
	private static ImageProvider? _foregroundChanging;
	private static ImageProvider? _normal;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Implements an <see cref="ImageProvider"/>
	/// </summary>
	private class HighDpiWithAnyDarkThemeVariationImageProvider : ImageProvider {

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public HighDpiWithAnyDarkThemeVariationImageProvider() {
			Scales.Add(2.0);  // 200%
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc/>
		protected override string GetThemeNamePathPart(string? themeName) {
			if (HasThemeVariation(themeName))
				return "Theme-AnyDark";

			return string.Empty;
		}

		/// <inheritdoc/>
		protected override bool HasThemeVariation(string? themeName) {
			// This provider example doesn't include specific theme names and instead, routes to a specific "AnyDark" variation if the theme's intent is dark/black
			return ThemeManager.IsDarkTheme(themeName);
		}

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="ImageProvider"/> that supports high DPI images and pre-defined variations for any dark theme.
	/// </summary>
	public static ImageProvider HighDpiWithAnyDarkThemeVariation
		=> _highDpiWithAnyDarkThemeVariation ??= new HighDpiWithAnyDarkThemeVariationImageProvider();

	/// <summary>
	/// The <see cref="ImageProvider"/> that supports high DPI images and chromatic adaptation for dark themes.
	/// </summary>
	public static ImageProvider HighDpiWithDarkThemeChromaticAdaptation {
		get => _highDpiWithDarkThemeChromaticAdaptation ??= new ImageProvider() {
			ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes,
			Scales = { 2.0 }  // 200%
		};
	}

	/// <summary>
	/// The <see cref="ImageProvider"/> that supports high DPI images and chromatic adaptation.
	/// </summary>
	public static ImageProvider HighDpiWithChromaticAdaptation {
		get => _highDpiWithChromaticAdaptation ??= new ImageProvider() {
			ChromaticAdaptationMode = ImageChromaticAdaptationMode.Always,
			Scales = { 2.0 }  // 200%
		};
	}

	/// <summary>
	/// The <see cref="ImageProvider"/> that supports high DPI images and pre-defined variations for the "Dark" theme only.
	/// </summary>
	public static ImageProvider HighDpiWithSpecificDarkThemeVariation {
		get => _highDpiWithSpecificDarkThemeVariation ??= new ImageProvider() {
			ThemeNames = { ThemeNames.Dark },
			Scales = { 2.0 }  // 200%
		};
	}

	/// <summary>
	/// The <see cref="ImageProvider"/> that can be used on images with a single color to adjust their foreground.
	/// </summary>
	public static ImageProvider ForegroundChanging
		=> _foregroundChanging ??= new ImageProvider() { DesignForegroundColor = Colors.Black };

	/// <summary>
	/// The <see cref="ImageProvider"/> that has no special options set.
	/// </summary>
	public static ImageProvider Normal
		=> _normal ??= new ImageProvider();

}
