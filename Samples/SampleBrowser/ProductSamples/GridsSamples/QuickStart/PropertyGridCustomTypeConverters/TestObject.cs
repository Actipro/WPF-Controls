using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters {
	
	/// <summary>
	/// Represents a test object for demonstration purposes.
	/// </summary>
	public class TestObject : ObservableObjectBase {

		private double myDoubleWith;
		private double myDoubleWithout;
		private MyEnum myEnumWith;
		private MyEnum myEnumWithout;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the first business logic integer.
		/// </summary>
		/// <value>The first business logic integer.</value>
		[Category("With Custom TypeConverter")]
		[DefaultValue(0.0)]
		[Description("A double property that uses a custom TypeConverter, which presents the value as degrees on the Fahrenheit scale. The TypeConverter also accepts input in Celsius and automatically convert it to Fahrenheit.")]
		[DisplayName("MyDouble")]
		[TypeConverter(typeof(TemperatureTypeConverter))]
		public double MyDoubleWith {
			get {
				return this.myDoubleWith;
			}
			set {
				if (value != this.myDoubleWith) {
					this.myDoubleWith = value;
					this.NotifyPropertyChanged("MyDoubleWith");
				}
			}
		}

		/// <summary>
		/// Gets or sets the first business logic integer.
		/// </summary>
		/// <value>The first business logic integer.</value>
		[Category("Without Custom TypeConverter")]
		[DefaultValue(0.0)]
		[Description("A double property that uses the default TypeConverter for doubles.")]
		[DisplayName("MyDouble")]
		public double MyDoubleWithout {
			get {
				return this.myDoubleWithout;
			}
			set {
				if (value != this.myDoubleWithout) {
					this.myDoubleWithout = value;
					this.NotifyPropertyChanged("MyDoubleWithout");
				}
			}
		}

		/// <summary>
		/// Gets or sets the first business logic integer.
		/// </summary>
		/// <value>The first business logic integer.</value>
		[Category("With Custom TypeConverter")]
		[DefaultValue(MyEnum.FirstEnum)]
		[Description("An enumeration property that uses a custom TypeConverter, which presents the value using a description string specified using DescriptionAttribute. The TypeConverter also accepts input using the enumeration descriptions or field names.")]
		[DisplayName("MyEnum")]
		[TypeConverter(typeof(EnumDescriptionTypeConverter))]
		public MyEnum MyEnumWith {
			get {
				return this.myEnumWith;
			}
			set {
				if (value != this.myEnumWith) {
					this.myEnumWith = value;
					this.NotifyPropertyChanged("MyEnumWith");
				}
			}
		}

		/// <summary>
		/// Gets or sets the first business logic integer.
		/// </summary>
		/// <value>The first business logic integer.</value>
		[Category("Without Custom TypeConverter")]
		[DefaultValue(MyEnum.FirstEnum)]
		[Description("An enumeration property that uses the default TypeConverter for enumerations.")]
		[DisplayName("MyEnum")]
		public MyEnum MyEnumWithout {
			get {
				return this.myEnumWithout;
			}
			set {
				if (value != this.myEnumWithout) {
					this.myEnumWithout = value;
					this.NotifyPropertyChanged("MyEnumWithout");
				}
			}
		}

	}

}
