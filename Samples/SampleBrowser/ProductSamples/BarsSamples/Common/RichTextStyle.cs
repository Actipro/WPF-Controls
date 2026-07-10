namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents style attributes of rich text.
/// </summary>
public class RichTextStyle : ICloneable {

	/// <summary>
	/// Indicates if bold is active.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool Bold { get; set; }

	/// <summary>
	/// The font color.
	/// </summary>
	/// <value>
	/// The default value is black.
	/// </value>
	public Color FontColor { get; set; } = Colors.Black;

	/// <summary>
	/// The font family name, or <c>null</c> to use the default.
	/// </summary>
	public string? FontFamilyName { get; set; }

	/// <summary>
	/// The font size, or <see cref="double.NaN"/> for the default.
	/// </summary>
	public double FontSize { get; set; } = double.NaN;

	/// <summary>
	/// Indicates if italic is active.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool Italic { get; set; }

	/// <summary>
	/// Indicates if strikethrough is active.
	/// </summary>
	/// <value>
	/// The default value is <c>false</c>.
	/// </value>
	public bool Strikethrough { get; set; }

	/// <summary>
	/// The text alignment.
	/// </summary>
	/// <value>
	/// The default value is <see cref="TextAlignment.Left"/>.
	/// </value>
	public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

	/// <summary>
	/// The text highlight color.
	/// </summary>
	/// <value>
	/// The default color is white.
	/// </value>
	public Color TextHighlightColor { get; set; } = Colors.White;

	/// <summary>
	/// The kind of underline that is active.
	/// </summary>
	/// <value>
	/// The default value is <see cref="UnderlineKind.None"/>.
	/// </value>
	public UnderlineKind Underline { get; set; } = UnderlineKind.None;

	object ICloneable.Clone()
		=> Clone();

	/// <inheritdoc cref="ICloneable.Clone"/>
	public RichTextStyle Clone() {
		return new RichTextStyle() {
			Bold = Bold,
			FontColor = FontColor,
			FontFamilyName = FontFamilyName,
			FontSize = FontSize,
			Italic = Italic,
			Strikethrough = Strikethrough,
			TextAlignment = TextAlignment,
			TextHighlightColor = TextHighlightColor,
			Underline = Underline,
		};
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj) {
		return obj is RichTextStyle other
			&& Bold == other.Bold
			&& FontColor == other.FontColor
			&& FontFamilyName == other.FontFamilyName
			&& FontSize == other.FontSize
			&& Italic == other.Italic
			&& Strikethrough == other.Strikethrough
			&& TextAlignment == other.TextAlignment
			&& TextHighlightColor == other.TextHighlightColor
			&& Underline == other.Underline;
	}

	/// <inheritdoc/>
	public override int GetHashCode() {
		#if NETCOREAPP
		return HashCode.Combine(Bold, FontColor, FontFamilyName, FontSize, Italic, Strikethrough, TextAlignment, HashCode.Combine(TextHighlightColor, Underline));
		#else
		// NOTE: 13 and 29 are prime numbers used for hash collision avoidance
		var hash = 13;
		hash = (hash * 29) + Bold.GetHashCode();
		hash = (hash * 29) + FontColor.GetHashCode();
		hash = (hash * 29) + (FontFamilyName ?? string.Empty).GetHashCode();
		hash = (hash * 29) + FontSize.GetHashCode();
		hash = (hash * 29) + Italic.GetHashCode();
		hash = (hash * 29) + Strikethrough.GetHashCode();
		hash = (hash * 29) + TextAlignment.GetHashCode();
		hash = (hash * 29) + TextHighlightColor.GetHashCode();
		hash = (hash * 29) + Underline.GetHashCode();
		return hash;
		#endif
	}

}
