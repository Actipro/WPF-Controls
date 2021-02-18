using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCategoryEditors {
	
	/// <summary>
	/// Provides information about the appearance of controls in this sample.
	/// </summary>
	public class AppearanceViewModel : ObservableObjectBase {
		
		private Color backgroundColor = Color.FromArgb(0xff, 0xd4, 0x04, 0x04);
		private Color borderColor = Color.FromArgb(0xff, 0x6c, 0x27, 0x27);
		private FontFamily fontFamily = new FontFamily("Verdana");
		private int fontSize = 16;
		private FontStyle fontStyle = FontStyles.Normal;
		private FontWeight fontWeight = FontWeights.Normal;
		private Color foregroundColor = Colors.White;
		private string text = "Change properties to alter this control's appearance.";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the background color.
		/// </summary>
		/// <value>The background color.</value>
		[Category("Colors")]
		[Description("The background color.")]
		public Color BackgroundColor {
			get {
				return this.backgroundColor;
			}
			set {
				if (this.backgroundColor != value) {
					this.backgroundColor = value;
					this.NotifyPropertyChanged("BackgroundColor");
				}
			}
		}

		/// <summary>
		/// Gets or sets the border color.
		/// </summary>
		/// <value>The border color.</value>
		[Category("Colors")]
		[Description("The border color.")]
		public Color BorderColor {
			get {
				return this.borderColor;
			}
			set {
				if (this.borderColor != value) {
					this.borderColor = value;
					this.NotifyPropertyChanged("BorderColor");
				}
			}
		}

		/// <summary>
		/// Gets or sets the font family.
		/// </summary>
		/// <value>The font family.</value>
		[Category("Font/Text")]
		[Description("The font family.")]
		public FontFamily FontFamily {
			get {
				return this.fontFamily;
			}
			set {
				if (this.fontFamily != value) {
					this.fontFamily = value;
					this.NotifyPropertyChanged("FontFamily");
				}
			}
		}

		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		[Category("Font/Text")]
		[Description("The size of the font.")]
		public int FontSize {
			get {
				return this.fontSize;
			}
			set {
				if (this.fontSize != value) {
					this.fontSize = value;
					this.NotifyPropertyChanged("FontSize");
				}
			}
		}

		/// <summary>
		/// Gets or sets the font style.
		/// </summary>
		/// <value>The font style.</value>
		[Category("Font/Text")]
		[Description("The font style.")]
		public FontStyle FontStyle {
			get {
				return this.fontStyle;
			}
			set {
				if (this.fontStyle != value) {
					this.fontStyle = value;
					this.NotifyPropertyChanged("FontStyle");
				}
			}
		}

		/// <summary>
		/// Gets or sets the font weight.
		/// </summary>
		/// <value>The font weight.</value>
		[Category("Font/Text")]
		[Description("The font weight.")]
		public FontWeight FontWeight {
			get {
				return this.fontWeight;
			}
			set {
				if (this.fontWeight != value) {
					this.fontWeight = value;
					this.NotifyPropertyChanged("FontWeight");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the foreground color.
		/// </summary>
		/// <value>The foreground color.</value>
		[Category("Colors")]
		[Description("The foreground color.")]
		public Color ForegroundColor {
			get {
				return this.foregroundColor;
			}
			set {
				if (this.foregroundColor != value) {
					this.foregroundColor = value;
					this.NotifyPropertyChanged("ForegroundColor");
				}
			}
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		[Category("Font/Text")]
		[Description("The text.")]
		public string Text {
			get {
				return this.text;
			}
			set {
				if (this.text != value) {
					this.text = value;
					this.NotifyPropertyChanged("Text");
				}
			}
		}

	}

}
