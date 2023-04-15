using System;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents style attributes of text.
	/// </summary>
	public class TextStyle : ICloneable {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the text style with default values.
		/// </summary>
		public TextStyle() { }

		/// <summary>
		/// Initializes a new instance of the text style with the specified font family name, font size, and font color.
		/// </summary>
		/// <param name="fontFamilyName">The font family name.</param>
		/// <param name="fontSize">The font size.</param>
		/// <param name="textColor">The text color.</param>
		public TextStyle(string fontFamilyName, double fontSize, Color textColor) {
			this.FontFamilyName = fontFamilyName;
			this.FontSize = fontSize;
			this.TextColor = textColor;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		object ICloneable.Clone() => this.Clone();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if bold is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public bool Bold { get; set; }

		/// <inheritdoc cref="ICloneable.Clone"/>
		public TextStyle Clone() {
			return new TextStyle() {
				Bold = this.Bold,
				FontFamilyName = this.FontFamilyName,
				FontSize = this.FontSize,
				Italic = this.Italic,
				TextColor = this.TextColor,
				Underline = this.Underline,
			};
		}

		/// <inheritdoc />
		public override bool Equals(object obj) {
			if (obj is TextStyle other) {
				return this.Bold == other.Bold
					&& this.FontFamilyName == other.FontFamilyName
					&& this.FontSize == other.FontSize
					&& this.Italic == other.Italic
					&& this.TextColor == other.TextColor
					&& this.Underline == other.Underline;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode() {
			#if NETCOREAPP
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
		/// Gets or sets the font family name.
		/// </summary>
		/// <value>A string value, or <c>null</c> for the default font.</value>
		public string FontFamilyName { get; set; } = FontFamilyBarGalleryItemViewModel.DefaultFontFamilyName;

		/// <summary>
		/// Gets or sets the font size.
		/// </summary>
		/// <value>A double value.</value>
		public double FontSize { get; set; } = FontSizeBarGalleryItemViewModel.DefaultFontSize;

		/// <summary>
		/// Gets or sets if italic is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public bool Italic { get; set; }

		/// <summary>
		/// Gets or sets the text color.
		/// </summary>
		/// <value>A <see cref="Color"/>.</value>
		public Color TextColor { get; set; } = Colors.Black;

		/// <summary>
		/// Gets or sets if underline is active.
		/// </summary>
		/// <value><c>true</c> when active; otherwise, <c>false</c>.</value>
		public bool Underline { get; set; }

	}

}
