using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion;

/// <summary>
/// Represents an address view model object.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class AddressViewModel : ObservableObjectBase {

	private string? _address1;
	private string? _address2;
	private string? _city;
	private string? _postalCode;
	private string? _state;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The address line 1.
	/// </summary>
	[DisplayName("Address 1")]
	[Display(Order = 1)]
	public string? Address1 {
		get => _address1;
		set => SetProperty(ref _address1, value);
	}

	/// <summary>
	/// The address line 2.
	/// </summary>
	[DisplayName("Address 2")]
	[Display(Order = 2)]
	public string? Address2 {
		get => _address2;
		set => SetProperty(ref _address2, value);
	}

	/// <summary>
	/// The city.
	/// </summary>
	[Display(Order = 3)]
	public string? City {
		get => _city;
		set => SetProperty(ref _city, value);
	}

	/// <summary>
	/// The postal code.
	/// </summary>
	[DisplayName("Postal code")]
	[Display(Order = 5)]
	public string? PostalCode {
		get => _postalCode;
		set => SetProperty(ref _postalCode, value);
	}

	/// <summary>
	/// The state.
	/// </summary>
	[Display(Order = 4)]
	public string? State {
		get => _state;
		set => SetProperty(ref _state, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> "(address)";

}
