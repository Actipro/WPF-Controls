namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability;

/// <summary>
/// Represents a child object that is expandable using <c>ExpandableObjectConverter</c>.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ChildObject2 : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ChildObject2() {
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Child2";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Child2";

	/// <inheritdoc/>
	public override string ToString()
		=> "Expandable";

}
