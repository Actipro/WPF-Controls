namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridCustomPropertyEditor;

/// <summary>
/// Stores home automation settings for demonstration purposes.
/// </summary>
public class HomeAutomationSettings : ObservableObjectBase {

	private OnOffAuto _alarm;
	private OnOffAuto _familyRoomLights;
	private OnOffAuto _foyerLights;
	private OnOffAuto _kitchenLights;
	private string? _profileName;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The security alarm.
	/// </summary>
	[Category("Security")]
	[Description("The security alarm.")]
	public OnOffAuto Alarm {
		get => _alarm;
		set => SetProperty(ref _alarm, value);
	}

	/// <summary>
	/// The family room lights.
	/// </summary>
	[Category("Lighting")]
	[Description("The family room lights.")]
	public OnOffAuto FamilyRoomLights {
		get => _familyRoomLights;
		set => SetProperty(ref _familyRoomLights, value);
	}

	/// <summary>
	/// The foyer lights.
	/// </summary>
	[Category("Lighting")]
	[Description("The foyer lights.")]
	public OnOffAuto FoyerLights {
		get => _foyerLights;
		set => SetProperty(ref _foyerLights, value);
	}

	/// <summary>
	/// The kitchen lights.
	/// </summary>
	[Category("Lighting")]
	[Description("The kitchen lights.")]
	public OnOffAuto KitchenLights {
		get => _kitchenLights;
		set => SetProperty(ref _kitchenLights, value);
	}

	/// <summary>
	/// The profile name.
	/// </summary>
	[Category("General")]
	[Description("The profile name.")]
	public string? ProfileName {
		get => _profileName;
		set => SetProperty(ref _profileName, value);
	}

}
