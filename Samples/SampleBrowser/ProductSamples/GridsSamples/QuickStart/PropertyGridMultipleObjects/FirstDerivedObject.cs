namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects;

/// <summary>
/// Represents the first derived object.
/// </summary>
public class FirstDerivedObject : BaseObject {

	private string? _firstOnly;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The first-only value.
	/// </summary>
	[DefaultValue("")]
	[Description("A string value that only appears in the first derived object class.")]
	public string? FirstOnly {
		get => _firstOnly;
		set => SetProperty(ref _firstOnly, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> "First Object (derived from Base Object)";

}
