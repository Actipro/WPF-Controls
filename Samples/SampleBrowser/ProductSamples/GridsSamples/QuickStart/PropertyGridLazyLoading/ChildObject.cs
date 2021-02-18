using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridLazyLoading {
	
	/// <summary>
	/// Represents a child object, which exposes it's parent object as a property.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ChildObject : BaseObject {

		private ParentObject parent;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ChildObject</c> class.
		/// </summary>
		/// <param name="parent">The parent.</param>
		public ChildObject(ParentObject parent) {
			this.parent = parent;
			this.ResetName();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the parent object of this child object.
		/// </summary>
		/// <value>The parent object of this child object.</value>
		[Description("The parent object, which has a reference back to this object.")]
		public ParentObject Parent {
			get {
				return this.parent;
			}
		}

		/// <summary>
		/// Resets the <see cref="Name"/> property.
		/// </summary>
		protected override void ResetName() {
			this.Name = "Child";
		}

		/// <summary>
		/// Determines if the <see cref="Name"/> property should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="Name"/> property should be serialized; otherwise <c>false</c>.</returns>
		protected override bool ShouldSerializeName() {
			return (this.Name != "Child");
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </returns>
		public override string ToString() {
			return "Child Object";
		}

	}

}
