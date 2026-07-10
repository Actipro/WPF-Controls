namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode;

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
	[Description("The name of the object, which can appear more than once in this sample. Changes in one entry will be reflected all duplicate entries.")]
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
