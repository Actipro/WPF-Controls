using System;
using System.Windows;
using System.Windows.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Represents style attributes of rich text.
	/// </summary>
	public class RichTextStyle : ICloneable {

		/// <summary>
		/// Gets or sets if bold is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public bool Bold { get; set; }

		/// <summary>
		/// Gets or sets the font color.
		/// </summary>
		/// <value>A <see cref="Color"/>.</value>
		public Color FontColor { get; set; } = Colors.Black;

		/// <summary>
		/// Gets or sets the font family name.
		/// </summary>
		/// <value>A string value, or <c>null</c> for the default font.</value>
		public string FontFamilyName { get; set; }

		/// <summary>
		/// Gets or sets the font size.
		/// </summary>
		/// <value>A double value, or <see cref="double.NaN"/> for the default.</value>
		public double FontSize { get; set; } = double.NaN;

		/// <summary>
		/// Gets or sets if italic is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public bool Italic { get; set; }

		/// <summary>
		/// Gets or sets if strikethrough is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public bool Strikethrough { get; set; }

		/// <summary>
		/// Gets or sets the text alignment.
		/// </summary>
		/// <value>One of the <see cref="TextAlignment"/> values.</value>
		public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

		/// <summary>
		/// Gets or sets the text highlight color.
		/// </summary>
		/// <value>A <see cref="Color"/>.</value>
		public Color TextHighlightColor { get; set; } = Colors.White;

		/// <summary>
		/// Gets or sets if underline is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public UnderlineKind Underline { get; set; }

		object ICloneable.Clone() => this.Clone();

		/// <inheritdoc cref="ICloneable.Clone"/>
		public RichTextStyle Clone() {
			return new RichTextStyle() {
				Bold = this.Bold,
				FontColor = this.FontColor,
				FontFamilyName = this.FontFamilyName,
				FontSize = this.FontSize,
				Italic = this.Italic,
				Strikethrough = this.Strikethrough,
				TextAlignment = this.TextAlignment,
				TextHighlightColor = this.TextHighlightColor,
				Underline = this.Underline,
			};
		}

		/// <inheritdoc />
		public override bool Equals(object obj) {
			if (obj is RichTextStyle other) {
				return this.Bold == other.Bold
					&& this.FontColor == other.FontColor
					&& this.FontFamilyName == other.FontFamilyName
					&& this.FontSize == other.FontSize
					&& this.Italic == other.Italic
					&& this.Strikethrough == other.Strikethrough
					&& this.TextAlignment == other.TextAlignment
					&& this.TextHighlightColor == other.TextHighlightColor
					&& this.Underline == other.Underline;
			}
			return false;
		}

		/// <inheritdoc />
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

}
