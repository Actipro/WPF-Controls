using System.ComponentModel;
using System.Windows.Media;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridItemMenuCustomization {

	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject : ObservableObjectBase {

		private Color color1 = Colors.Red;
		private Color color2 = Colors.White;
		private string string1 = "Some text";
		private string string2 = "Some more text";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the first color.
		/// </summary>
		/// <value>The first color.</value>
		[Category("Colors")]
		[Description("The first color, which defaults to red.")]
		public Color Color1 {
			get {
				return this.color1;
			}
			set {
				if (this.color1 != value) {
					this.color1 = value;
					this.NotifyPropertyChanged("Color1");
				}
			}
		}

		/// <summary>
		/// Gets or sets the second color.
		/// </summary>
		/// <value>The second color.</value>
		[Category("Colors")]
		[Description("The second color, which defaults to white.")]
		public Color Color2 {
			get {
				return this.color2;
			}
			set {
				if (this.color2 != value) {
					this.color2 = value;
					this.NotifyPropertyChanged("Color2");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the first string.
		/// </summary>
		/// <value>The first string.</value>
		[Category("Strings")]
		[Description("The first string.")]
		public string String1 {
			get {
				return this.string1;
			}
			set {
				if (this.string1 != value) {
					this.string1 = value;
					this.NotifyPropertyChanged("String1");
				}
			}
		}

		/// <summary>
		/// Gets or sets the second string.
		/// </summary>
		/// <value>The second string.</value>
		[Category("Strings")]
		[Description("The second string.")]
		public string String2 {
			get {
				return this.string2;
			}
			set {
				if (this.string2 != value) {
					this.string2 = value;
					this.NotifyPropertyChanged("String2");
				}
			}
		}

	}

}
