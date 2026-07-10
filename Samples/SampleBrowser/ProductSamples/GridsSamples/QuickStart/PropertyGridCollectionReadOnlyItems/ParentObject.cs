namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems;

/// <summary>
/// Represents a parent object which has several collections of child objects.
/// </summary>
public class ParentObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ParentObject() {
		List1 = [
			new ChildObject("Child 1-1"),
			new ChildObject("Child 1-2"),
			new ChildObject("Child 1-3"),
		];

		List2 = [
			new ChildObject("Child 2-1"),
			new ChildObject("Child 2-2"),
			new ChildObject("Child 2-3"),
		];

		List3 = [
			new ChildObject("Child 3-1"),
			new ChildObject("Child 3-2"),
			new ChildObject("Child 3-3"),
		];

		List4 = new ReadOnlyCollection<ChildObject>([
			new ChildObject("Child 4-1"),
			new ChildObject("Child 4-2"),
			new ChildObject("Child 4-3"),
		]);

		List5 = [
			new ReadOnlyChildObject("Read-Only Child 5-1"),
			new ReadOnlyChildObject("Read-Only Child 5-2"),
			new ReadOnlyChildObject("Read-Only Child 5-3"),
		];
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A list of children objects.
	/// </summary>
	[Description("A list of child objects (i.e. List<ChildObject>), which allows the individual items to be set (the default behavior).")]
	public List<ChildObject> List1 { get; }

	/// <summary>
	/// A list of children objects.
	/// </summary>
	[Description("A list of child objects (i.e. List<ChildObject>), which uses a custom type converter to make the individual items read-only.")]
	[TypeConverter(typeof(ReadOnlyItemsCollectionConverter))]
	public List<ChildObject> List2 { get; }

	/// <summary>
	/// A list of children objects.
	/// </summary>
	[Description("A list of child objects (i.e. List<ChildObject>), which uses a custom type converter to make the first two items read-only and to prevent their removal.")]
	[TypeConverter(typeof(CustomListConverter))]
	public List<ChildObject> List3 { get; }

	/// <summary>
	/// A list of children objects.
	/// </summary>
	[Description("A read-only list of child objects (i.e. ReadOnlyCollection<ChildObject>), which does not allow the individual items to be set or for items to be added/removed.")]
	public ReadOnlyCollection<ChildObject> List4 { get; }

	/// <summary>
	/// A list of children objects.
	/// </summary>
	[Description("A list of child objects marked with ReadOnlyAttribute (i.e. List<ChildObject>), which does not allow the individual items to be set.")]
	public List<ReadOnlyChildObject> List5 { get; }

}
