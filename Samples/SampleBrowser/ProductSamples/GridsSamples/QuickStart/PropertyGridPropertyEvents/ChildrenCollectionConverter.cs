using System;
using ActiproSoftware.Windows.Controls.Grids.PropertyData;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyEvents {

	/// <summary>
	/// Represents a type converter for the children collection on a <see cref="Person"/>.
	/// </summary>
	public class ChildrenCollectionConverter : ExpandableCollectionConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ChildrenCollectionConverter"/> class.
		/// </summary>
		public ChildrenCollectionConverter() {
			this.NoItemsText = "No children";
			this.OneItemText = "1 child";
			this.MultipleItemsFormat = "{0} children";
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a new item that can be added into to the collection.
		/// </summary>
		/// <param name="propertyModel">The <see cref="IPropertyModel"/> associated with the collection.</param>
		/// <returns>The item that was created.</returns>
		public override object CreateItem(IPropertyModel propertyModel) {
			var parent = propertyModel.Target as Person;

			var names = new string[] { "Noah", "Liam", "Mason", "Ethan", "Michael", "Jacob", "Alexander", "Emma", "Olivia", "Sophia", "Isabella", "Ava", "Mia", "Emily", "Abigail" };
			var rand = new Random();

			var child = new Person();
			child.Birthday = DateTime.Today;
			child.LastName = parent.LastName;
			child.FirstName = names[rand.Next(names.Length)];

			return child;
		}

	}

}
