using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridLazyLoading {
	
	/// <summary>
	/// Represents a parent object which has several child objects.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ParentObject : BaseObject {
		
		private ChildObject child;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ParentObject</c> class.
		/// </summary>
		public ParentObject() {
			this.child = new ChildObject(this);
			this.ResetName();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the child object.
		/// </summary>
		/// <value>The child object.</value>
		[Description("The child object, which has a reference back to this object.")]
		public ChildObject Child {
			get {
				return this.child;
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
			return (this.Name != "Parent");
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
