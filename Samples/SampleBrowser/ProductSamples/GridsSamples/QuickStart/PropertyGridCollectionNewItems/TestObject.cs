namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionNewItems;

/// <summary>
/// Represents a parent object which has several collections of child objects.
/// </summary>
public class TestObject {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A list of strings.
	/// </summary>
	[Description("A list of strings (i.e., List<string>), which uses the default type converter that allows empty strings to be added.")]
	public List<string> Strings1 { get; } = ["One", "Two", "Three"];

	/// <summary>
	/// A list of strings.
	/// </summary>
	[Description("A list of strings (i.e. List<string>), which uses a custom type converter that allows null values to be added.")]
	[TypeConverter(typeof(NullStringListConverter))]
	public List<string> Strings2 { get; } = ["One", "Two", "Three"];

	/// <summary>
	/// A list of strings.
	/// </summary>
	[Description("A list of strings (i.e. List<string>), which uses a custom type converter that allows custom strings to be added.")]
	[TypeConverter(typeof(CustomStringListConverter))]
	public List<string> Strings3 { get; } = ["One", "Two", "Three"];

}
