namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridSelectiveExpansion;

/// <summary>
/// Represents a phone numbers view model object.
/// </summary>
[TypeConverter(typeof(ExpandableObjectConverter))]
public class PhoneNumbersViewModel : ObservableObjectBase {

	private string? _fax;
	private string? _voice;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The fax number.
	/// </summary>
	public string? Fax {
		get => _fax;
		set => SetProperty(ref _fax, value);
	}

	/// <summary>
	/// The voice number.
	/// </summary>
	public string? Voice {
		get => _voice;
		set => SetProperty(ref _voice, value);
	}

	/// <inheritdoc/>
	public override string ToString()
		=> "(phone numbers)";

}
