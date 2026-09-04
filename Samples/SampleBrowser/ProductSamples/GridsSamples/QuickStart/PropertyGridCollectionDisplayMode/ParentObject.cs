namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode;

/// <summary>
/// Represents a parent object which has several collections of child objects.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class ParentObject : BaseObject {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ParentObject() {
		// Array
		ChildrenArray = [
			new ChildObject(),
			new ChildObject(),
		];

		// Dictionary
		ChildrenDictionary = new Dictionary<string, ChildObject> {
			{ "One", new ChildObject() },
			{ "Two", new ChildObject() }
		};

		// List
		ChildrenList = [
			new ChildObject(),
			new ChildObject()
		];

		// ObservableCollection
		ChildrenObservableCollection = [
			new ChildObject(),
			new ChildObject()
		];

		ResetName();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// An array of children objects.
	/// </summary>
	[Category("Standard Collections")]
	[Description("An array of child objects (i.e. ChildObject[]).")]
	public ChildObject[] ChildrenArray { get; }

	/// <summary>
	/// A dictionary of children objects.
	/// </summary>
	[Category("Standard Collections")]
	[Description("A dictionary of child objects (i.e. Dictionary<string, ChildObject>).")]
	public Dictionary<string, ChildObject> ChildrenDictionary { get; }

	/// <summary>
	/// Gets a list of children objects.
	/// </summary>
	/// <value>A list of children objects.</value>
	[Category("Standard Collections")]
	[Description("A list of child objects (i.e. List<ChildObject>).")]
	public List<ChildObject> ChildrenList { get; }

	/// <summary>
	/// An observable collection of children objects.
	/// </summary>
	[Category("Standard Collections")]
	[Description("An observable collection of child objects (i.e. ObservableCollection<ChildObject>).")]
	public ObservableCollection<ChildObject> ChildrenObservableCollection { get; }

	/// <inheritdoc/>
	protected override void ResetName()
		=> Name = "Parent";

	/// <inheritdoc/>
	protected override bool ShouldSerializeName()
		=> Name != "Parent";

	/// <inheritdoc/>
	public override string ToString()
		=> "Parent Object";

}
