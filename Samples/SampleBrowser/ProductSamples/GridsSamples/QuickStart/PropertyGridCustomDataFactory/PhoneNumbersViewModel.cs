using System.ComponentModel;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory {

	/// <summary>
	/// Represents a phone numbers view model object.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PhoneNumbersViewModel : ObservableObjectBase {

		private string fax;
		private string voice;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the fax number.
		/// </summary>
		/// <value>The fax number.</value>
		public string Fax {
			get {
				return fax;
			}
			set {
				fax = value;
				this.NotifyPropertyChanged("Fax");
			}
		}
		
		/// <summary>
		/// Gets or sets the voice number.
		/// </summary>
		/// <value>The voice number.</value>
		public string Voice {
			get {
				return voice;
			}
			set {
				voice = value;
				this.NotifyPropertyChanged("Voice");
			}
		}
		
		/// <summary>
		/// Returns a <c>String</c> that represents the current <c>Object</c>.
		/// </summary>
		/// <returns>A <c>String</c> that represents the current <c>Object</c>.</returns>
		public override string ToString() {
			return "(phone numbers)";
		}

	}

}
