namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridItemMenuCustomization;

/// <summary>
/// Represents a test object for demonstration purposes.
/// </summary>
public class TestObject : ObservableObjectBase {

	private Color _color1 = Colors.Red;
	private Color _color2 = Colors.White;
	private string _string1 = "Some text";
	private string _string2 = "Some more text";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The first color.
	/// </summary>
	[Category("Colors")]
	[Description("The first color, which defaults to red.")]
	public Color Color1 {
		get => _color1;
		set => SetProperty(ref _color1, value);
	}

	/// <summary>
	/// The second color.
	/// </summary>
	[Category("Colors")]
	[Description("The second color, which defaults to white.")]
	public Color Color2 {
		get => _color2;
		set => SetProperty(ref _color2, value);
	}

	/// <summary>
	/// The first string.
	/// </summary>
	[Category("Strings")]
	[Description("The first string.")]
	public string String1 {
		get => _string1;
		set => SetProperty(ref _string1, value);
	}

	/// <summary>
	/// The second string.
	/// </summary>
	[Category("Strings")]
	[Description("The second string.")]
	public string String2 {
		get => _string2;
		set => SetProperty(ref _string2, value);
	}

}
