using System.ComponentModel;
using System.Collections.Generic;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory {

	/// <summary>
	/// Represents a customer view model object.
	/// </summary>
	public class CustomerViewModel : ObservableObjectBase {

		private IList<ReferralSourceViewModel> availableReferrals = new List<ReferralSourceViewModel>();
		private string countryName;
		private string customerName;
		private PhoneNumbersViewModel phoneNumbers;
		private ReferralSourceViewModel referredBy;

		private string secretData = "This data should not appear in the PropertyGrid.";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the list of available referrals.
		/// </summary>
		/// <value>The list of available referrals.</value>
		public IList<ReferralSourceViewModel> AvailableReferrals {
			get {
				return availableReferrals;
			}
		}
		
		/// <summary>
		/// Gets or sets the country name.
		/// </summary>
		/// <value>The country name.</value>
		[DisplayName("Country")]
		public string CountryName {
			get {
				return countryName;
			}
			set {
				countryName = value;
				this.NotifyPropertyChanged("CountryName");
			}
		}
		
		/// <summary>
		/// Gets or sets the customer name.
		/// </summary>
		/// <value>The customer name.</value>
		[DisplayName("Customer name")]
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
		public PhoneNumbersViewModel PhoneNumbers {
			get {
				return phoneNumbers;
			}
			set {
				phoneNumbers = value;
				this.NotifyPropertyChanged("PhoneNumbers");
			}
		}
		
		/// <summary>
		/// Gets or sets the referral.
		/// </summary>
		/// <value>The referral.</value>
		[DisplayName("Referred by")]
		public ReferralSourceViewModel ReferredBy {
			get {
				return referredBy;
			}
			set {
				referredBy = value;
				this.NotifyPropertyChanged("ReferredBy");
			}
		}
		
		/// <summary>
		/// Gets or sets secret data that will be filtered out by the custom data factory.
		/// </summary>
		/// <value>Secret data.</value>
		[DisplayName("Secret data")]
		public string SecretData {
			get {
				return secretData;
			}
			set {
				secretData = value;
				this.NotifyPropertyChanged("SecretData");
			}
		}

	}

}
