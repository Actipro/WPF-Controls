using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems {
	
	/// <summary>
	/// Represents a parent object which has several collections of child objects.
	/// </summary>
	public class ParentObject {

		private List<ChildObject> list1;
		private List<ChildObject> list2;
		private List<ChildObject> list3;
		private List<ChildObject> list4;
		private List<ReadOnlyChildObject> list5;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ParentObject</c> class.
		/// </summary>
		public ParentObject() {
			this.list1 = new List<ChildObject>();
			this.list1.Add(new ChildObject());
			this.list1.Add(new ChildObject());
			this.list1.Add(new ChildObject());

			this.list2 = new List<ChildObject>();
			this.list2.Add(new ChildObject());
			this.list2.Add(new ChildObject());
			this.list2.Add(new ChildObject());

			this.list3 = new List<ChildObject>();
			this.list3.Add(new ChildObject());
			this.list3.Add(new ChildObject());
			this.list3.Add(new ChildObject());

			this.list4 = new List<ChildObject>();
			this.list4.Add(new ChildObject());
			this.list4.Add(new ChildObject());
			this.list4.Add(new ChildObject());

			this.list5 = new List<ReadOnlyChildObject>();
			this.list5.Add(new ReadOnlyChildObject());
			this.list5.Add(new ReadOnlyChildObject());
			this.list5.Add(new ReadOnlyChildObject());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an list of children objects.
		/// </summary>
		/// <value>An list of children objects.</value>
		[Description("A list of child objects (i.e. List<ChildObject>), which allows the individual items to be set (the default behavior).")]
		public List<ChildObject> List1 {
			get {
				return this.list1;
			}
		}

		/// <summary>
		/// Gets an list of children objects.
		/// </summary>
		/// <value>An list of children objects.</value>
		[Description("A list of child objects (i.e. List<ChildObject>), which uses a custom type converter to make the individual items read-only.")]
		[TypeConverter(typeof(ReadOnlyItemsCollectionConverter))]
		public List<ChildObject> List2 {
			get {
				return this.list2;
			}
		}

		/// <summary>
		/// Gets an list of children objects.
		/// </summary>
		/// <value>An list of children objects.</value>
		[Description("A list of child objects (i.e. List<ChildObject>), which uses a custom type converter to make the first two items read-only and to prevent their removal.")]
		[TypeConverter(typeof(CustomListConverter))]
		public List<ChildObject> List3 {
			get {
				return this.list3;
			}
		}

		/// <summary>
		/// Gets an list of children objects.
		/// </summary>
		/// <value>An list of children objects.</value>
		[Description("A read-only list of child objects (i.e. ReadOnlyCollection<ChildObject>), which does not allow the individual items to be set or for items to be added/removed.")]
		public ReadOnlyCollection<ChildObject> List4 {
			get {
				return this.list4.AsReadOnly();
			}
		}

		/// <summary>
		/// Gets an list of children objects.
		/// </summary>
		/// <value>An list of children objects.</value>
		[Description("A list of child objects marked with ReadOnlyAttribute (i.e. List<ChildObject>), which does not allow the individual items to be set.")]
		public List<ReadOnlyChildObject> List5 {
			get {
				return this.list5;
			}
		}

	}

}
