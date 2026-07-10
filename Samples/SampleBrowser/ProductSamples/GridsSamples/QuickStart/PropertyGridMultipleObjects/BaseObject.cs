namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects;

/// <summary>
/// Represents a base object.
/// </summary>
public abstract class BaseObject : ObservableObjectBase {

	private string? _derivedOnly;
	private string? _name;
	private int _number;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	[DefaultValue("")]
	[Description("A property that is defined on BaseObject and will appear on derived objects. Changes in one entry will be reflected all duplicate entries.")]
	public string? DerivedOnly {
		get => _derivedOnly;
		set => SetProperty(ref _derivedOnly, value);
	}

	/// <summary>
	/// The name.
	/// </summary>
	[DefaultValue("")]
	[Description("The name of the object, which can appear more than once in this sample. Changes in one entry will be reflected all duplicate entries.")]
	public string? Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// The number.
	/// </summary>
	[DefaultValue(0)]
	[Description("The number of the object, which can appear more than once in this sample. Changes in one entry will be reflected all duplicate entries.")]
	public int Number {
		get => _number;
		set => SetProperty(ref _number, value);
	}

}
