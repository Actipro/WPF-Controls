namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability;

/// <summary>
/// Represents a parent object which has several child objects.
/// </summary>
public class TestObject {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The child object.
	/// </summary>
	[Description("A child object that does not define a TypeConverter, so it is not expandable by default.")]
	public ChildObject1 Child1 { get; } = new();

	/// <summary>
	/// The child object.
	/// </summary>
	[Description("A child object that uses ExpandableObjectConverter, so it is expandable by default.")]
	public ChildObject2 Child2 { get; } = new();

	/// <summary>
	/// The child object.
	/// </summary>
	[Description("A child object that uses a custom TypeConverter that derives from TypeConverter, so it is not expandable by default.")]
	public ChildObject3 Child3 { get; } = new();

	/// <summary>
	/// The child object.
	/// </summary>
	[Description("A child object that uses a custom TypeConverter that derives from ExpandableObjectConverter, so it is expandable by default.")]
	public ChildObject4 Child4 { get; } = new();

}
