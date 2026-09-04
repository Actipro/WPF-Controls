namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridLazyLoading;

/// <summary>
/// Represents a child object, which exposes it's parent object as a property.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ChildObject : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="parent">The parent.</param>
	public ChildObject(ParentObject parent) {
		Parent = parent;
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The parent object of this child object.
	/// </summary>
	[Description("The parent object, which has a reference back to this object.")]
	public ParentObject Parent { get; }

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
