using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion {

	/// <summary>
	/// Represents an address view model object.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AddressViewModel : ObservableObjectBase {

		private string address1;
		private string address2;
		private string city;
		private string postalCode;
		private string state;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the address line 1.
		/// </summary>
		/// <value>The address line 1.</value>
		[DisplayName("Address 1")]
		[Display(Order = 1)]
		public string Address1 {
			get {
				return address1;
			}
			set {
				address1 = value;
				this.NotifyPropertyChanged("Address1");
			}
		}
		
		/// <summary>
		/// Gets or sets the address line 2.
		/// </summary>
		/// <value>The address line 2.</value>
		[DisplayName("Address 2")]
		[Display(Order = 2)]
		public string Address2 {
			get {
				return address2;
			}
			set {
				address2 = value;
				this.NotifyPropertyChanged("Address2");
			}
		}
		
		/// <summary>
		/// Gets or sets the city.
		/// </summary>
		/// <value>The city.</value>
		[Display(Order = 3)]
		public string City {
			get {
				return city;
			}
			set {
				city = value;
				this.NotifyPropertyChanged("City");
			}
		}
		
		/// <summary>
		/// Gets or sets the postal code.
		/// </summary>
		/// <value>The postal code.</value>
		[DisplayName("Postal code")]
		[Display(Order = 5)]
		public string PostalCode {
			get {
				return postalCode;
			}
			set {
				postalCode = value;
				this.NotifyPropertyChanged("PostalCode");
			}
		}
		
		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>The state.</value>
		[Display(Order = 4)]
		public string State {
			get {
				return state;
			}
			set {
				state = value;
				this.NotifyPropertyChanged("State");
			}
		}
		
		/// <summary>
		/// Returns a <c>String</c> that represents the current <c>Object</c>.
		/// </summary>
		/// <returns>A <c>String</c> that represents the current <c>Object</c>.</returns>
		public override string ToString() {
			return "(address)";
		}

	}

}
