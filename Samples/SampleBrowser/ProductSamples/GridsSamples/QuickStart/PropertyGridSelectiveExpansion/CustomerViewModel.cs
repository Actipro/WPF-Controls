using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion;

/// <summary>
/// Represents a customer view model object.
/// </summary>
public class CustomerViewModel : ObservableObjectBase {

	private AddressViewModel _address = new();
	private string? _customerName;
	private PhoneNumbersViewModel _phoneNumbers = new();

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The address.
	/// </summary>
	[DisplayName("Address")]
	[Display(Order = 2)]
	[ReadOnly(true)]
	public AddressViewModel Address {
		get => _address;
		set => SetProperty(ref _address, value);
	}

	/// <summary>
	/// The customer name.
	/// </summary>
	[DisplayName("Customer name")]
	[Display(Order = 1)]
	public string? CustomerName {
		get => _customerName;
		set => SetProperty(ref _customerName, value);
	}

	/// <summary>
	/// The phone numbers.
	/// </summary>
	[DisplayName("Phone numbers")]
	[Display(Order = 3)]
	[ReadOnly(true)]
	public PhoneNumbersViewModel PhoneNumbers {
		get => _phoneNumbers;
		set => SetProperty(ref _phoneNumbers, value);
	}

}
