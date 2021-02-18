namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			var phoneNumbers = new PhoneNumbersViewModel();
			phoneNumbers.Voice = "491-198-1285";
			phoneNumbers.Fax = "491-294-1356";
			
			var customer = new CustomerViewModel();
			customer.AvailableReferrals.Add(new ReferralSourceViewModel() { Id = 1, Name = "Unknown" });
			customer.AvailableReferrals.Add(new ReferralSourceViewModel() { Id = 2, Name = "Sales Associate" });
			customer.AvailableReferrals.Add(new ReferralSourceViewModel() { Id = 3, Name = "Print Advertising" });
			customer.AvailableReferrals.Add(new ReferralSourceViewModel() { Id = 4, Name = "On-line Advertising" });
			customer.AvailableReferrals.Add(new ReferralSourceViewModel() { Id = 99, Name = "Other" });
			customer.CustomerName = "ABC Machinery, Inc.";
			customer.CountryName = "United States";
			customer.PhoneNumbers = phoneNumbers;
			customer.ReferredBy = customer.AvailableReferrals[0];

			this.DataContext = customer;
		}

	}

}
