namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNotifyParentProperty;

/// <summary>
/// Represents a parent object with and expandable child property that will notify the parent property of changes.
/// </summary>
public class ParentObject {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The child.
	/// </summary>
	public ChildObject Child { get; } = new();

}
