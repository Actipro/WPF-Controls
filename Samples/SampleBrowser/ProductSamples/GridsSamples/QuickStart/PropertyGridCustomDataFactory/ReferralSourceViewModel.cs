namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomDataFactory;

/// <summary>
/// Represents a referral source view model object.
/// </summary>
public class ReferralSourceViewModel : ObservableObjectBase {

	private int _id;
	private string? _name;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The ID.
	/// </summary>
	public int Id {
		get => _id;
		set => SetProperty(ref _id, value);
	}

	/// <summary>
	/// The name.
	/// </summary>
	public string? Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

}
