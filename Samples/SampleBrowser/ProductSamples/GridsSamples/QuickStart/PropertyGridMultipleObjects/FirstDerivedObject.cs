using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects {
	
	/// <summary>
	/// Represents the first derived object.
	/// </summary>
	public class FirstDerivedObject : BaseObject {

		private string firstOnly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the first-only value.
		/// </summary>
		/// <value>The first-only value.</value>
		[DefaultValue("")]
		[Description("A string value that only appears in the first derived object class.")]
		public string FirstOnly {
			get {
				return this.firstOnly;
			}
			set {
				if (this.firstOnly != value) {
					this.firstOnly = value;
					this.NotifyPropertyChanged("FirstOnly");
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
			return "First Object (derived from Base Object)";
		}

	}

}
