using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyEvents;

/// <summary>
/// Represents a type converter for the children collection on a <see cref="Person"/>.
/// </summary>
public class ChildrenCollectionConverter : ExpandableCollectionConverter {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ChildrenCollectionConverter() {
		NoItemsText = "No children";
		OneItemText = "1 child";
		MultipleItemsFormat = "{0} children";
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object CreateItem(IPropertyModel propertyModel) {
		var parent = propertyModel.Target as Person;

		var names = new string[] { "Noah", "Liam", "Mason", "Ethan", "Michael", "Jacob", "Alexander", "Emma", "Olivia", "Sophia", "Isabella", "Ava", "Mia", "Emily", "Abigail" };
		var rand = new Random();

		var child = new Person {
			Birthday = DateTime.Today,
			LastName = parent?.LastName,
			FirstName = names[rand.Next(names.Length)]
		};

		return child;
	}

}
