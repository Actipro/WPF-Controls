using ActiproSoftware.Products.Bars.Mvvm;
using System;
using System.Windows.Media;

namespace ActiproSoftware.Windows.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a text style (font, color, weight, etc.).
	/// </summary>
	public class TextStyleBarGalleryItemViewModel : BarGalleryItemViewModelBase {

		private bool bold;
		private Color color;
		private string fontFamilyName;
		private double fontSize = FontSizeBarGalleryItemViewModel.DefaultFontSize;
		private bool italic;
		private bool underline;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public TextStyleBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(
				SR.GetString(SRName.UINamedTextStyleNormalText), 
				FontFamilyBarGalleryItemViewModel.DefaultFontFamilyName, 
				FontSizeBarGalleryItemViewModel.DefaultFontSize, 
				Colors.Black
			) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified category, font family name, font size, and color.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		/// <param name="fontFamilyName">The font family name.</param>
		/// <param name="fontSize">The font size.</param>
		/// <param name="color">The text color.</param>
		public TextStyleBarGalleryItemViewModel(string label, string fontFamilyName, double fontSize, Color color)
			: base(SR.GetString(SRName.UIGalleryItemCategoryTextStylesText)) {

			if (string.IsNullOrEmpty(label))
				throw new ArgumentNullException(nameof(label));
			if (string.IsNullOrEmpty(fontFamilyName))
				throw new ArgumentNullException(nameof(fontFamilyName));

			this.Label = label;
			this.fontFamilyName = fontFamilyName;
			this.fontSize = fontSize;
			this.color = color;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether the text should be bold.
		/// </summary>
		/// <value>
		/// <c>true</c> if the text should be bold; otherwise, <c>false</c>.
		/// </value>
		public bool Bold {
			get {
				return bold;
			}
			set {
				if (bold != value) {
					bold = value;
					this.NotifyPropertyChanged(nameof(Bold));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the text color.
		/// </summary>
		/// <value>The text color.</value>
		public Color Color {
			get {
				return color;
			}
			set {
				if (color != value) {
					color = value;
					this.NotifyPropertyChanged(nameof(Color));
				}
			}
		}

		/// <summary>
		/// Gets or sets the font family name.
		/// </summary>
		/// <value>The font family name.</value>
		public string FontFamilyName {
			get {
				return fontFamilyName;
			}
			set {
				if (fontFamilyName != value) {
					fontFamilyName = value;
					this.NotifyPropertyChanged(nameof(FontFamilyName));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the font size.
		/// </summary>
		/// <value>The font size.</value>
		public double FontSize {
			get {
				return fontSize;
			}
			set {
				if (fontSize != value) {
					fontSize = value;
					this.NotifyPropertyChanged(nameof(FontSize));
				}
			}
		}
		
		/// <summary>
		/// Gets or sets whether the text should be italic.
		/// </summary>
		/// <value>
		/// <c>true</c> if the text should be italic; otherwise, <c>false</c>.
		/// </value>
		public bool Italic {
			get {
				return italic;
			}
			set {
				if (italic != value) {
					italic = value;
					this.NotifyPropertyChanged(nameof(Italic));
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Label='{this.Label}']";
		
		/// <summary>
		/// Gets or sets whether the text should be underlined.
		/// </summary>
		/// <value>
		/// <c>true</c> if the text should be underlined; otherwise, <c>false</c>.
		/// </value>
		public bool Underline {
			get {
				return underline;
			}
			set {
				if (underline != value) {
					underline = value;
					this.NotifyPropertyChanged(nameof(Underline));
				}
			}
		}
		
	}

}
