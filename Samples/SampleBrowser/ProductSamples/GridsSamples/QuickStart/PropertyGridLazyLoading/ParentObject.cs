namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridLazyLoading;

/// <summary>
/// Represents a parent object which has several child objects.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ParentObject : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ParentObject() {
		Child = new ChildObject(this);
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The child object.
	/// </summary>
	[Description("The child object, which has a reference back to this object.")]
	public ChildObject Child { get; }

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Parent";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Parent";

	/// <inheritdoc/>
	public override string ToString()
		=> "Parent Object";

}
