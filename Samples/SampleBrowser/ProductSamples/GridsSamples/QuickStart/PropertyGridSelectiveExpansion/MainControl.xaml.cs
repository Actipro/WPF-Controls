namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion {

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

			var customer = new CustomerViewModel();
			customer.CustomerName = "ABC Machinery, Inc.";
			customer.PhoneNumbers.Voice = "491-198-1285";
			customer.PhoneNumbers.Fax = "491-294-1356";
			customer.Address.Address1 = "581 Main St.";
			customer.Address.City = "New York";
			customer.Address.State = "NY";
			customer.Address.PostalCode = "10010";

			this.DataContext = customer;
		}

	}

}
