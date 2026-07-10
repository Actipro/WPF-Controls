namespace ActiproSoftware.Windows.Controls.Bars.Mvvm;

/// <summary>
/// Represents style attributes of text.
/// </summary>
public class TextStyle : ICloneable {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class with default values.
	/// </summary>
	public TextStyle() { }

	/// <summary>
	/// Initializes an instance of the class with the specified font family name, font size, and font color.
	/// </summary>
	/// <param name="fontFamilyName">The font family name.</param>
	/// <param name="fontSize">The font size.</param>
	/// <param name="textColor">The text color.</param>
	public TextStyle(string fontFamilyName, double fontSize, Color textColor) {
		FontFamilyName = fontFamilyName;
		FontSize = fontSize;
		TextColor = textColor;
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	object ICloneable.Clone()
		=> Clone();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates if bold is active.
	/// </summary>
	public bool Bold { get; set; }

	/// <inheritdoc cref="ICloneable.Clone"/>
	public TextStyle Clone() {
		return new() {
			Bold = this.Bold,
			FontFamilyName = this.FontFamilyName,
			FontSize = this.FontSize,
			Italic = this.Italic,
			TextColor = this.TextColor,
			Underline = this.Underline,
		};
	}

	/// <inheritdoc />
	#if NET
	public override bool Equals([NotNullWhen(true)] object? obj) {
	#else
	public override bool Equals(object? obj) {
	#endif
		return obj is TextStyle other
			&& Bold == other.Bold
			&& FontFamilyName == other.FontFamilyName
			&& FontSize == other.FontSize
			&& Italic == other.Italic
			&& TextColor == other.TextColor
			&& Underline == other.Underline;
	}

	/// <inheritdoc />
	public override int GetHashCode() {
		#if NET
		return HashCode.Combine(Bold, FontFamilyName, FontSize, Italic, TextColor, Underline);
		#else
		// NOTE: 17 and 31 are prime numbers used for hash collision avoidance
		var hash = 17;
		hash = (hash * 31) + Bold.GetHashCode();
		hash = (hash * 31) + (FontFamilyName ?? string.Empty).GetHashCode();
		hash = (hash * 31) + FontSize.GetHashCode();
		hash = (hash * 31) + Italic.GetHashCode();
		hash = (hash * 31) + TextColor.GetHashCode();
		hash = (hash * 31) + Underline.GetHashCode();
		return hash;
		#endif
	}

	/// <summary>
	/// The font family name.
	/// </summary>
	/// <value>A string value, or <c>null</c> for the default font.</value>
	public string? FontFamilyName { get; set; } = FontFamilyBarGalleryItemViewModel.DefaultFontFamilyName;

	/// <summary>
	/// The font size.
	/// </summary>
	public double FontSize { get; set; } = FontSizeBarGalleryItemViewModel.DefaultFontSize;

	/// <summary>
	/// Indicates if italic is active.
	/// </summary>
	public bool Italic { get; set; }

	/// <summary>
	/// The text color.
	/// </summary>
	public Color TextColor { get; set; } = Colors.Black;

	/// <summary>
	/// Indicates if underline is active.
	/// </summary>
	public bool Underline { get; set; }

}
