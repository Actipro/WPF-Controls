using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionReadOnlyItems {
	
	/// <summary>
	/// Represents a child object.
	/// </summary>
	[ReadOnly(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ReadOnlyChildObject {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ReadOnlyChildObject</c> class.
		/// </summary>
		/// <param name="name">The optional string name.</param>
		public ReadOnlyChildObject(string name = null) {
			this.Name = name ?? "Read-Only Child";
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
