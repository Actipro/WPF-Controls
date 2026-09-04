namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNestedCategories;

/// <summary>
/// Represents a test object.
/// </summary>
public class TestObject : ObservableObjectBase {

	private string _one = string.Empty;
	private string _two = string.Empty;
	private string _three = string.Empty;
	private string _four = string.Empty;
	private string _five = string.Empty;
	private string _six = string.Empty;
	private string _seven = string.Empty;
	private string _eight = string.Empty;
	private string _nine = string.Empty;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The first property.
	/// </summary>
	[Category(@"Odd")]
	[DefaultValue("")]
	[Description("The first property.")]
	public string One {
		get => _one;
		set => SetProperty(ref _one, value);
	}

	/// <summary>
	/// The second property.
	/// </summary>
	[Category(@"Even")]
	[DefaultValue("")]
	[Description("The second property.")]
	public string Two {
		get => _two;
		set => SetProperty(ref _two, value);
	}

	/// <summary>
	/// The third property.
	/// </summary>
	[Category(@"Odd\Multiples of 3")]
	[DefaultValue("")]
	[Description("The third property.")]
	public string Three {
		get => _three;
		set => SetProperty(ref _three, value);
	}

	/// <summary>
	/// The fourth property.
	/// </summary>
	[Category(@"Even\Multiples of 4")]
	[DefaultValue("")]
	[Description("The fourth property.")]
	public string Four {
		get => _four;
		set => SetProperty(ref _four, value);
	}

	/// <summary>
	/// The fifth property.
	/// </summary>
	[Category(@"Odd")]
	[DefaultValue("")]
	[Description("The fifth property.")]
	public string Five {
		get => _five;
		set => SetProperty(ref _five, value);
	}

	/// <summary>
	/// The sixth property.
	/// </summary>
	[Category(@"Even")]
	[DefaultValue("")]
	[Description("The sixth property.")]
	public string Six {
		get => _six;
		set => SetProperty(ref _six, value);
	}

	/// <summary>
	/// The seventh property.
	/// </summary>
	[Category(@"Odd")]
	[DefaultValue("")]
	[Description("The seventh property.")]
	public string Seven {
		get => _seven;
		set => SetProperty(ref _seven, value);
	}

	/// <summary>
	/// The eighth property.
	/// </summary>
	[Category(@"Even\Multiples of 4")]
	[DefaultValue("")]
	[Description("The eighth property.")]
	public string Eight {
		get => _eight;
		set => SetProperty(ref _eight, value);
	}

	/// <summary>
	/// The ninth property.
	/// </summary>
	[Category(@"Odd\Multiples of 3")]
	[DefaultValue("")]
	[Description("The ninth property.")]
	public string Nine {
		get => _nine;
		set => SetProperty(ref _nine, value);
	}

}
