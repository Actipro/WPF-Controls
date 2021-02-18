using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects {
	
	/// <summary>
	/// Represents a third object.
	/// </summary>
	public class ThirdObject : ObservableObjectBase {

		private string name;
		private string thirdOnly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[Description("The name of the object, which is defined in a separate class but will still be merged.")]
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
		/// Gets or sets the third-only value.
		/// </summary>
		/// <value>The third-only value.</value>
		[Description("A string value that only appears in the third object class.")]
		public string ThirdOnly {
			get {
				return this.thirdOnly;
			}
			set {
				if (this.thirdOnly != value) {
					this.thirdOnly = value;
					this.NotifyPropertyChanged("ThirdOnly");
				}
			}
		}

		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="String"/> that represents the current <see cref="Object"/>.
		/// </returns>
		public override string ToString() {
			return "Third Object";
		}

	}

}
