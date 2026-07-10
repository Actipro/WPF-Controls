namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability;

/// <summary>
/// Represents a base object for the parent and child objects.
/// </summary>
public abstract class BaseObject : ObservableObjectBase {

	private string? _name;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	[Description("The name of the object.")]
	public string? Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// Resets the <see cref="Name"/> property.
	/// </summary>
	protected abstract void ResetName();

	/// <summary>
	/// Determines if the <see cref="Name"/> property should be serialized.
	/// </summary>
	protected abstract bool ShouldSerializeName();

}
