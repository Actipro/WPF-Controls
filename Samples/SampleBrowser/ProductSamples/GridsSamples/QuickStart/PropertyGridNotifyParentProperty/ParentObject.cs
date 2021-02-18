namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridNotifyParentProperty {
	
	/// <summary>
	/// Represents a parent object with and expandable child property that will notify the parent property of changes.
	/// </summary>
	public class ParentObject {

		private ChildObject child = new ChildObject();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the child.
		/// </summary>
		/// <value>The child.</value>
		public ChildObject Child {
			get {
				return this.child;
			}
		}

	}

}
