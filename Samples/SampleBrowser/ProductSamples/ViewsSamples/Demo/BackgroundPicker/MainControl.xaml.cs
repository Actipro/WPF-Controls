using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.BackgroundPicker;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private Color _baseColor;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		BaseColor = UIColor.FromWebColor("#ffbf40").ToColor();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Update all the brushes.
	/// </summary>
	private void UpdateBrushes() {
		if (listBox is null)
			return;

		// Calculate light/dark colors
		var lightUIColor = UIColor.FromColor(_baseColor);
		lightUIColor.HlsLightness = Math.Min(1.0, lightUIColor.HlsLightness + 0.25);
		var darkUIColor = UIColor.FromColor(_baseColor);
		darkUIColor.HlsLightness = Math.Max(0.0, darkUIColor.HlsLightness - 0.25);

		// Get all colors used
		var lightColor = lightUIColor.ToColor();
		var mediumColor = _baseColor;
		var darkColor = darkUIColor.ToColor();

		// Update brushes
		solidBrushData.Brush = new SolidColorBrush(mediumColor);

		var angle = 90.0;
		vertLightToMediumBrushData.Brush = new LinearGradientBrush(lightColor, mediumColor, angle);
		vertLightToDarkBrushData.Brush = new LinearGradientBrush(lightColor, darkColor, angle);
		vertMediumToLightBrushData.Brush = new LinearGradientBrush(mediumColor, lightColor, angle);
		vertMediumToDarkBrushData.Brush = new LinearGradientBrush(mediumColor, darkColor, angle);
		vertDarkToLightBrushData.Brush = new LinearGradientBrush(darkColor, lightColor, angle);
		vertDarkToMediumBrushData.Brush = new LinearGradientBrush(darkColor, mediumColor, angle);

		angle = 0.0;
		horizLightToMediumBrushData.Brush = new LinearGradientBrush(lightColor, mediumColor, angle);
		horizLightToDarkBrushData.Brush = new LinearGradientBrush(lightColor, darkColor, angle);
		horizMediumToLightBrushData.Brush = new LinearGradientBrush(mediumColor, lightColor, angle);
		horizMediumToDarkBrushData.Brush = new LinearGradientBrush(mediumColor, darkColor, angle);
		horizDarkToLightBrushData.Brush = new LinearGradientBrush(darkColor, lightColor, angle);
		horizDarkToMediumBrushData.Brush = new LinearGradientBrush(darkColor, mediumColor, angle);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The base color.
	/// </summary>
	public Color BaseColor {
		get => _baseColor;
		set {
			_baseColor = value;
			UpdateBrushes();
		}
	}

}
