namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory;

/// <summary>
/// Represents a customer view model object.
/// </summary>
public class CustomerViewModel : ObservableObjectBase {

	private string? _countryName;
	private string? _customerName;
	private PhoneNumbersViewModel? _phoneNumbers;
	private ReferralSourceViewModel? _referredBy;

	private string _secretData = "This data should not appear in the PropertyGrid.";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The list of available referrals.
	/// </summary>
	[DisplayName("Available referrals")]
	public IList<ReferralSourceViewModel> AvailableReferrals { get; } = [];

	/// <summary>
	/// The country name.
	/// </summary>
	[DisplayName("Country")]
	public string? CountryName {
		get => _countryName;
		set => SetProperty(ref _countryName, value);
	}

	/// <summary>
	/// The customer name.
	/// </summary>
	[DisplayName("Customer name")]
	public string? CustomerName {
		get => _customerName;
		set => SetProperty(ref _customerName, value);
	}

	/// <summary>
	/// The list of data items.
	/// </summary>
	[DisplayName("Data items")]
	public IDictionary<string, string> DataItems { get; } = new Dictionary<string, string>();

	/// <summary>
	/// The phone numbers.
	/// </summary>
	[DisplayName("Phone numbers")]
	public PhoneNumbersViewModel? PhoneNumbers {
		get => _phoneNumbers;
		set => SetProperty(ref _phoneNumbers, value);
	}

	/// <summary>
	/// The referral.
	/// </summary>
	[DisplayName("Referred by")]
	public ReferralSourceViewModel? ReferredBy {
		get => _referredBy;
		set => SetProperty(ref _referredBy, value);
	}

	/// <summary>
	/// Secret data that will be filtered out by the custom data factory.
	/// </summary>
	[DisplayName("Secret data")]
	public string SecretData {
		get => _secretData;
		set => SetProperty(ref _secretData, value);
	}

}
