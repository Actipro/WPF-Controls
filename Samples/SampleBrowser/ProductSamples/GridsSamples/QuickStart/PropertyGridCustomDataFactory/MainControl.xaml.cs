namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		var customer = new CustomerViewModel() {
			CustomerName = "ABC Machinery, Inc.",
			CountryName = "United States",
			PhoneNumbers = new PhoneNumbersViewModel {
				Voice = "491-198-1285",
				Fax = "491-294-1356"
			},
		};
		customer.AvailableReferrals.Add(new() { Id = 1, Name = "Unknown" });
		customer.AvailableReferrals.Add(new() { Id = 2, Name = "Sales Associate" });
		customer.AvailableReferrals.Add(new() { Id = 3, Name = "Print Advertising" });
		customer.AvailableReferrals.Add(new() { Id = 4, Name = "On-line Advertising" });
		customer.AvailableReferrals.Add(new() { Id = 99, Name = "Other" });
		customer.DataItems["Last contact"] = DateTime.Now.Subtract(TimeSpan.FromDays(20)).ToShortDateString();
		customer.DataItems["Total YTD sales"] = "$19,064";
		customer.ReferredBy = customer.AvailableReferrals[1];

		DataContext = customer;
	}

}
