namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects;

/// <summary>
/// Represents a third object.
/// </summary>
public class ThirdObject : ObservableObjectBase {

	private string? _name;
	private int _number;
	private string? _thirdOnly;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	[Description("The name of the object, which is defined in a separate class but will still be merged.")]
	public string? Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// The number.
	/// </summary>
	[DefaultValue(0)]
	[Description("The number of the object, which is defined in a separate class but will still be merged.")]
	public int Number {
		get => _number;
		set => SetProperty(ref _number, value);
	}

	/// <summary>
	/// The third-only value.
	/// </summary>
	[Description("A string value that only appears in the third object class.")]
	public string? ThirdOnly {
		get => _thirdOnly;
		set => SetProperty(ref _thirdOnly, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> "Third Object";

}
