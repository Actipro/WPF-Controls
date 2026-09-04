namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomTypeConverters;

/// <summary>
/// Represents a test object for demonstration purposes.
/// </summary>
public class TestObject : ObservableObjectBase {

	private double _myDoubleWith;
	private double _myDoubleWithout;
	private MyEnum _myEnumWith;
	private MyEnum _myEnumWithout;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Double property with custom type converter.
	/// </summary>
	[Category("With Custom TypeConverter")]
	[DefaultValue(0.0)]
	[Description("A double property that uses a custom TypeConverter, which presents the value as degrees on the Fahrenheit scale. The TypeConverter also accepts input in Celsius and automatically convert it to Fahrenheit.")]
	[DisplayName("MyDouble")]
	[TypeConverter(typeof(TemperatureTypeConverter))]
	public double MyDoubleWith {
		get => _myDoubleWith;
		set => SetProperty(ref _myDoubleWith, value);
	}

	/// <summary>
	/// Double property with default type converter.
	/// </summary>
	[Category("Without Custom TypeConverter")]
	[DefaultValue(0.0)]
	[Description("A double property that uses the default TypeConverter for doubles.")]
	[DisplayName("MyDouble")]
	public double MyDoubleWithout {
		get => _myDoubleWithout;
		set => SetProperty(ref _myDoubleWithout, value);
	}

	/// <summary>
	/// Enum property with custom type converter.
	/// </summary>
	[Category("With Custom TypeConverter")]
	[DefaultValue(MyEnum.FirstEnum)]
	[Description("An enumeration property that uses a custom TypeConverter, which presents the value using a description string specified using DescriptionAttribute. The TypeConverter also accepts input using the enumeration descriptions or field names.")]
	[DisplayName("MyEnum")]
	[TypeConverter(typeof(EnumDescriptionTypeConverter))]
	public MyEnum MyEnumWith {
		get => _myEnumWith;
		set => SetProperty(ref _myEnumWith, value);
	}

	/// <summary>
	/// Enum property with default type converter.
	/// </summary>
	[Category("Without Custom TypeConverter")]
	[DefaultValue(MyEnum.FirstEnum)]
	[Description("An enumeration property that uses the default TypeConverter for enumerations.")]
	[DisplayName("MyEnum")]
	public MyEnum MyEnumWithout {
		get => _myEnumWithout;
		set => SetProperty(ref _myEnumWithout, value);
	}

}
