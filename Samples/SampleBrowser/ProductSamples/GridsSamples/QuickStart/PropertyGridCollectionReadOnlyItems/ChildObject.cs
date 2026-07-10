namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems;

/// <summary>
/// Represents a child object.
/// </summary>
/// <param name="name">The optional string name.</param>
[TypeConverter(typeof(ChildObjectConverter))]
public class ChildObject(string? name = null) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	[NotifyParentProperty(true)]
	public string Name { get; set; } = name ?? "Child";

	/// <inheritdoc/>
	public override string ToString()
		=> Name;

}
