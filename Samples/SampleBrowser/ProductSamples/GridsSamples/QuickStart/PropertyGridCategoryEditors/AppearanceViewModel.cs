namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCategoryEditors;

/// <summary>
/// Provides information about the appearance of controls in this sample.
/// </summary>
public class AppearanceViewModel : ObservableObjectBase {

	private Color _backgroundColor = Color.FromArgb(0xff, 0xd4, 0x04, 0x04);
	private Color _borderColor = Color.FromArgb(0xff, 0x6c, 0x27, 0x27);
	private FontFamily _fontFamily = new("Verdana");
	private int _fontSize = 16;
	private FontStyle _fontStyle = FontStyles.Normal;
	private FontWeight _fontWeight = FontWeights.Normal;
	private Color _foregroundColor = Colors.White;
	private string _text = "Change properties to alter this control's appearance.";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The background color.
	/// </summary>
	[Category("Colors")]
	[Description("The background color.")]
	public Color BackgroundColor {
		get => _backgroundColor;
		set => SetProperty(ref _backgroundColor, value);
	}

	/// <summary>
	/// The border color.
	/// </summary>
	[Category("Colors")]
	[Description("The border color.")]
	public Color BorderColor {
		get => _borderColor;
		set => SetProperty(ref _borderColor, value);
	}

	/// <summary>
	/// The font family.
	/// </summary>
	[Category("Font/Text")]
	[Description("The font family.")]
	public FontFamily FontFamily {
		get => _fontFamily;
		set => SetProperty(ref _fontFamily, value);
	}

	/// <summary>
	/// The size of the font.
	/// </summary>
	[Category("Font/Text")]
	[Description("The size of the font.")]
	public int FontSize {
		get => _fontSize;
		set => SetProperty(ref _fontSize, value);
	}

	/// <summary>
	/// The font style.
	/// </summary>
	[Category("Font/Text")]
	[Description("The font style.")]
	public FontStyle FontStyle {
		get => _fontStyle;
		set => SetProperty(ref _fontStyle, value);
	}

	/// <summary>
	/// The font weight.
	/// </summary>
	[Category("Font/Text")]
	[Description("The font weight.")]
	public FontWeight FontWeight {
		get => _fontWeight;
		set => SetProperty(ref _fontWeight, value);
	}

	/// <summary>
	/// The foreground color.
	/// </summary>
	[Category("Colors")]
	[Description("The foreground color.")]
	public Color ForegroundColor {
		get => _foregroundColor;
		set => SetProperty(ref _foregroundColor, value);
	}

	/// <summary>
	/// The text.
	/// </summary>
	[Category("Font/Text")]
	[Description("The text.")]
	public string Text {
		get => _text;
		set => SetProperty(ref _text, value);
	}

}
