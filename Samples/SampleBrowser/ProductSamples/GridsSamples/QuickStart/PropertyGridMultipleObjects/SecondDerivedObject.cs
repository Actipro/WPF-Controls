using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridMultipleObjects {
	
	/// <summary>
	/// Represents the second derived object.
	/// </summary>
	public class SecondDerivedObject : BaseObject {

		private string secondOnly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the second-only value.
		/// </summary>
		/// <value>The second-only value.</value>
		[DefaultValue("")]
		[Description("A string value that only appears in the second derived object class.")]
		public string SecondOnly {
			get {
				return this.secondOnly;
			}
			set {
				if (this.secondOnly != value) {
					this.secondOnly = value;
					this.NotifyPropertyChanged("SecondOnly");
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
			return "Second Object (derived from Base Object)";
		}

	}

}
