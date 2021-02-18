using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion {

	/// <summary>
	/// Represents a customer view model object.
	/// </summary>
	public class CustomerViewModel : ObservableObjectBase {

		private AddressViewModel address = new AddressViewModel();
		private string customerName;
		private PhoneNumbersViewModel phoneNumbers = new PhoneNumbersViewModel();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the address.
		/// </summary>
		/// <value>The address.</value>
		[DisplayName("Address")]
		[Display(Order = 2)]
		[ReadOnly(true)]
		public AddressViewModel Address {
			get {
				return address;
			}
			set {
				address = value;
				this.NotifyPropertyChanged("Address");
			}
		}
		
		/// <summary>
		/// Gets or sets the customer name.
		/// </summary>
		/// <value>The customer name.</value>
		[DisplayName("Customer name")]
		[Display(Order = 1)]
		public string CustomerName {
			get {
				return customerName;
			}
			set {
				customerName = value;
				this.NotifyPropertyChanged("CustomerName");
			}
		}
		
		/// <summary>
		/// Gets or sets the phone numbers.
		/// </summary>
		/// <value>The phone numbers.</value>
		[DisplayName("Phone numbers")]
		[Display(Order = 3)]
		[ReadOnly(true)]
		public PhoneNumbersViewModel PhoneNumbers {
			get {
				return phoneNumbers;
			}
			set {
				phoneNumbers = value;
				this.NotifyPropertyChanged("PhoneNumbers");
			}
		}
		
	}

}
