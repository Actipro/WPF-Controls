using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability {
	
	/// <summary>
	/// Represents a parent object which has several child objects.
	/// </summary>
	public class TestObject {
		
		private ChildObject1 child1;
		private ChildObject2 child2;
		private ChildObject3 child3;
		private ChildObject4 child4;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>TestObject</c> class.
		/// </summary>
		public TestObject() {
			this.child1 = new ChildObject1();
			this.child2 = new ChildObject2();
			this.child3 = new ChildObject3();
			this.child4 = new ChildObject4();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the child object.
		/// </summary>
		/// <value>The child object.</value>
		[Description("A child object that does not define a TypeConverter, so it is not expandable by default.")]
		public ChildObject1 Child1 {
			get {
				return this.child1;
			}
		}

		/// <summary>
		/// Gets the child object.
		/// </summary>
		/// <value>The child object.</value>
		[Description("A child object that uses ExpandableObjectConverter, so it is expandable by default.")]
		public ChildObject2 Child2 {
			get {
				return this.child2;
			}
		}

		/// <summary>
		/// Gets the child object.
		/// </summary>
		/// <value>The child object.</value>
		[Description("A child object that uses a custom TypeConverter that derives from TypeConverter, so it is not expandable by default.")]
		public ChildObject3 Child3 {
			get {
				return this.child3;
			}
		}

		/// <summary>
		/// Gets the child object.
		/// </summary>
		/// <value>The child object.</value>
		[Description("A child object that uses a custom TypeConverter that derives from ExpandableObjectConverter, so it is expandable by default.")]
		public ChildObject4 Child4 {
			get {
				return this.child4;
			}
		}

	}

}
