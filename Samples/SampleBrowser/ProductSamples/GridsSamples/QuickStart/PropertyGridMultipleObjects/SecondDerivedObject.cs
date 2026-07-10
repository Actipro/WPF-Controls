namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects;

/// <summary>
/// Represents the second derived object.
/// </summary>
public class SecondDerivedObject : BaseObject {

	private string? _secondOnly;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The second-only value.
	/// </summary>
	[DefaultValue("")]
	[Description("A string value that only appears in the second derived object class.")]
	public string? SecondOnly {
		get => _secondOnly;
		set => SetProperty(ref _secondOnly, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> "Second Object (derived from Base Object)";

}
