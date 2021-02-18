using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridPropertyExpandability {

	/// <summary>
	/// Represents a base object for the parent and child objects.
	/// </summary>
	public abstract class BaseObject : ObservableObjectBase {

		private string name;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[Description("The name of the object.")]
		public string Name {
			get {
				return this.name;
			}
			set {
				if (this.name != value) {
					this.name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}

		/// <summary>
		/// Resets the <see cref="Name"/> property.
		/// </summary>
		protected abstract void ResetName();

		/// <summary>
		/// Determines if the <see cref="Name"/> property should be serialized.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="Name"/> property should be serialized; otherwise <c>false</c>.</returns>
		protected abstract bool ShouldSerializeName();

	}

}
