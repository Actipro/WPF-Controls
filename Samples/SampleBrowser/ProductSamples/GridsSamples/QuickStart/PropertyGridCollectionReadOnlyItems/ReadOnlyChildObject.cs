namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems;

/// <summary>
/// Represents a child object.
/// </summary>
/// <param name="name">The optional string name.</param>
[ReadOnly(true)]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ReadOnlyChildObject(string? name = null) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	[NotifyParentProperty(true)]
	public string Name { get; set; } = name ?? "Read-Only Child";

	/// <inheritdoc/>
	public override string ToString()
		=> Name;

}
