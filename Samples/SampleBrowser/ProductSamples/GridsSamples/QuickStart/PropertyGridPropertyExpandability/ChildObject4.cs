namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability;

/// <summary>
/// Represents a child object that is expandable using <c>ChildObject4TypeConverter</c>.
/// </summary>
[TypeConverter(typeof(ChildObject4TypeConverter))]
public class ChildObject4 : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ChildObject4() {
		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Child4";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Child4";

	/// <inheritdoc/>
	public override string ToString()
		=> "Expandable (Custom)";

}
