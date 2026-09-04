using ActiproSoftware.Windows.Themes;
using ActiproSoftware.Windows.Themes.Generation;

namespace ActiproSoftware.ProductSamples.RibbonSamples.Demo.DocumentEditor;

/// <summary>
/// Represents the <see cref="UserControl"/> that encompasses a color picker.
/// </summary>
public partial class ColorPicker : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ColorPicker() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnApplyClick(object sender, RoutedEventArgs e) {
		// Build a color-tinted theme
		var definition = ThemeManager.GetThemeDefinition(ThemeNames.Custom);
		definition.BaseColorBlue = spectrumColorPicker.SelectedColor;
		definition.PrimaryAccentColorFamilyName = ColorFamilyName.Blue;
		definition.WindowColorFamilyName = ColorFamilyName.Blue;
		ThemeManager.RegisterThemeDefinition(definition);

		// Ensure the theme is selected
		ThemeManager.UnregisterAutomaticThemes();
		ThemeManager.CurrentTheme = ThemeNames.Custom;
	}

}
