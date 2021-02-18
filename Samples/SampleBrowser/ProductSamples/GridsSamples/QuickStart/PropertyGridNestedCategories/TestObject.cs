using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNestedCategories {

	/// <summary>
	/// Represents a test object.
	/// </summary>
	public class TestObject : ObservableObjectBase {

		private string one = string.Empty;
		private string two = string.Empty;
		private string three = string.Empty;
		private string four = string.Empty;
		private string five = string.Empty;
		private string six = string.Empty;
		private string seven = string.Empty;
		private string eight = string.Empty;
		private string nine = string.Empty;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the first property.
		/// </summary>
		/// <value>The first property.</value>
		[Category(@"Odd")]
		[DefaultValue("")]
		[Description("The first property.")]
		public string One {
			get {
				return this.one;
			}
			set {
				if (value != this.one) {
					this.one = value;
					this.NotifyPropertyChanged("One");
				}
			}
		}

		/// <summary>
		/// Gets or sets the second property.
		/// </summary>
		/// <value>The second property.</value>
		[Category(@"Even")]
		[DefaultValue("")]
		[Description("The second property.")]
		public string Two {
			get {
				return this.two;
			}
			set {
				if (value != this.two) {
					this.two = value;
					this.NotifyPropertyChanged("Two");
				}
			}
		}

		/// <summary>
		/// Gets or sets the third property.
		/// </summary>
		/// <value>The third property.</value>
		[Category(@"Odd\Multiples of 3")]
		[DefaultValue("")]
		[Description("The third property.")]
		public string Three {
			get {
				return this.three;
			}
			set {
				if (value != this.three) {
					this.three = value;
					this.NotifyPropertyChanged("Three");
				}
			}
		}

		/// <summary>
		/// Gets or sets the fourth property.
		/// </summary>
		/// <value>The fourth property.</value>
		[Category(@"Even\Multiples of 4")]
		[DefaultValue("")]
		[Description("The fourth property.")]
		public string Four {
			get {
				return this.four;
			}
			set {
				if (value != this.four) {
					this.four = value;
					this.NotifyPropertyChanged("Four");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the fifth property.
		/// </summary>
		/// <value>The fifth property.</value>
		[Category(@"Odd")]
		[DefaultValue("")]
		[Description("The fifth property.")]
		public string Five {
			get {
				return this.five;
			}
			set {
				if (value != this.five) {
					this.five = value;
					this.NotifyPropertyChanged("Five");
				}
			}
		}

		/// <summary>
		/// Gets or sets the sixth property.
		/// </summary>
		/// <value>The sixth property.</value>
		[Category(@"Even")]
		[DefaultValue("")]
		[Description("The sixth property.")]
		public string Six {
			get {
				return this.six;
			}
			set {
				if (value != this.six) {
					this.six = value;
					this.NotifyPropertyChanged("Six");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the seventh property.
		/// </summary>
		/// <value>The seventh property.</value>
		[Category(@"Odd")]
		[DefaultValue("")]
		[Description("The seventh property.")]
		public string Seven {
			get {
				return this.seven;
			}
			set {
				if (value != this.seven) {
					this.seven = value;
					this.NotifyPropertyChanged("Seven");
				}
			}
		}

		/// <summary>
		/// Gets or sets the eighth property.
		/// </summary>
		/// <value>The eighth property.</value>
		[Category(@"Even\Multiples of 4")]
		[DefaultValue("")]
		[Description("The eighth property.")]
		public string Eight {
			get {
				return this.eight;
			}
			set {
				if (value != this.eight) {
					this.eight = value;
					this.NotifyPropertyChanged("Eight");
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the ninth property.
		/// </summary>
		/// <value>The ninth property.</value>
		[Category(@"Odd\Multiples of 3")]
		[DefaultValue("")]
		[Description("The ninth property.")]
		public string Nine {
			get {
				return this.nine;
			}
			set {
				if (value != this.nine) {
					this.nine = value;
					this.NotifyPropertyChanged("Nine");
				}
			}
		}

	}

}
