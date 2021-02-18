using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode {
	
	/// <summary>
	/// Represents a parent object which has several collections of child objects.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ParentObject : BaseObject {

		private ChildObject[] childrenArray;
		private Dictionary<string, ChildObject> childrenDictionary;
		private List<ChildObject> childrenList;
		private ObservableCollection<ChildObject> childrenObservableCollection;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ParentObject</c> class.
		/// </summary>
		public ParentObject() {
			// Array
			this.childrenArray = new ChildObject[] {
				new ChildObject(),
				new ChildObject(),
			};

			// Dictionary
			this.childrenDictionary = new Dictionary<string, ChildObject>();
			this.childrenDictionary.Add("One", new ChildObject());
			this.childrenDictionary.Add("Two", new ChildObject());

			// List
			this.childrenList = new List<ChildObject>();
			this.childrenList.Add(new ChildObject());
			this.childrenList.Add(new ChildObject());

			// ObservableCollection
			this.childrenObservableCollection = new ObservableCollection<ChildObject>();
			this.childrenObservableCollection.Add(new ChildObject());
			this.childrenObservableCollection.Add(new ChildObject());

			this.ResetName();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an array of children objects.
		/// </summary>
		/// <value>An array of children objects.</value>
		[Category("Standard Collections")]
		[Description("An array of child objects (i.e. ChildObject[]).")]
		public ChildObject[] ChildrenArray {
			get {
				return this.childrenArray;
			}
		}

		/// <summary>
		/// Gets a dictionary of children objects.
		/// </summary>
		/// <value>A dictionary of children objects.</value>
		[Category("Standard Collections")]
		[Description("A dictionary of child objects (i.e. Dictionary<string, ChildObject>).")]
		public Dictionary<string, ChildObject> ChildrenDictionary {
			get {
				return this.childrenDictionary;
			}
		}

		/// <summary>
		/// Gets a list of children objects.
		/// </summary>
		/// <value>A list of children objects.</value>
		[Category("Standard Collections")]
		[Description("A list of child objects (i.e. List<ChildObject>).")]
		public List<ChildObject> ChildrenList {
			get {
				return this.childrenList;
			}
		}

		/// <summary>
		/// Gets an observable collection of children objects.
		/// </summary>
		/// <value>An observable collection of children objects.</value>
		[Category("Standard Collections")]
		[Description("An observable collection of child objects (i.e. ObservableCollection<ChildObject>).")]
		public ObservableCollection<ChildObject> ChildrenObservableCollection {
			get {
				return this.childrenObservableCollection;
			}
		}


		/// <summary>
		/// Resets the <see cref="Name"/> property.
		/// </summary>
		protected override void ResetName() {
			this.Name = "Parent";
		}

		/// <summary>
		/// Determines if the <see cref="Name"/> property should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="Name"/> property should be serialized; otherwise <c>false</c>.</returns>
		protected override bool ShouldSerializeName() {
			return (0 != string.Compare(this.Name, "Parent"));
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </returns>
		public override string ToString() {
			return "Parent Object";
		}

	}

}
