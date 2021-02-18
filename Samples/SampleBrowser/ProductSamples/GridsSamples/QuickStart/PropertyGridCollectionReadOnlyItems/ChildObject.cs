using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems {
	
	/// <summary>
	/// Represents a child object.
	/// </summary>
	[TypeConverter(typeof(ChildObjectConverter))]
	public class ChildObject {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ChildObject</c> class.
		/// </summary>
		public ChildObject() {
			this.Name = "Child";
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[NotifyParentProperty(true)]
		public string Name {
			get;
			set;
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </returns>
		public override string ToString() {
			return this.Name;
		}

	}

}
