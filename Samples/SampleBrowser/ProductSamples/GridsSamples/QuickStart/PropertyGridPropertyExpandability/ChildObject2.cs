using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability {

	/// <summary>
	/// Represents a child object that is expandable using <c>ExpandableObjectConverter</c>.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ChildObject2 : BaseObject {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>ChildObject2</c> class.
		/// </summary>
		public ChildObject2() {
			this.ResetName();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Resets the <see cref="Name"/> property.
		/// </summary>
		protected override void ResetName() {
			this.Name = "Child2";
		}

		/// <summary>
		/// Determines if the <see cref="Name"/> property should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="Name"/> property should be serialized; otherwise <c>false</c>.</returns>
		protected override bool ShouldSerializeName() {
			return (0 != string.Compare(this.Name, "Child2"));
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </returns>
		public override string ToString() {
			return "Expandable";
		}

	}

}
