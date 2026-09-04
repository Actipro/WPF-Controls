namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode;

/// <summary>
/// Represents a child object.
/// </summary>
[ReadOnly(true)]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ChildObject : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ChildObject() {
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Child";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Child";

	/// <inheritdoc/>
	public override string ToString()
		=> "Child Object";

}
