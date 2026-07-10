namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability;

/// <summary>
/// Represents a child object that is not expandable because it doesn't specify a type converter.
/// </summary>
public class ChildObject1 : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ChildObject1() {
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Child1";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Child1";

	/// <inheritdoc/>
	public override string ToString()
		=> "Not Expandable";

}
