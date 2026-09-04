namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability;

/// <summary>
/// Represents a child object that is expandable using <c>ChildObject3TypeConverter</c>.
/// </summary>
[TypeConverter(typeof(ChildObject3TypeConverter))]
public class ChildObject3 : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ChildObject3() {
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Child3";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Child3";

	/// <inheritdoc/>
	public override string ToString()
		=> "Not Expandable (Custom)";

}
