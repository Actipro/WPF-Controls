using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCollectionDisplayMode {
	
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
		[Description("The name of the object, which can appear more than once in this sample. Changes in one entry will be reflected all duplicate entries.")]
		public string Name {
			get {
				return this.name;
			}
			set {
				if (value != this.name) {
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
