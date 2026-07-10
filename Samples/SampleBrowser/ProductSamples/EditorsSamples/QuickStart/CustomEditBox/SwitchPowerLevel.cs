using ActiproSoftware.Extensions;

namespace ActiproSoftware.ProductSamples.EditorsSamples.QuickStart.CustomEditBox;

/// <summary>
/// Indicates whether a switch is on, and its power level when on.
/// </summary>
public class SwitchPowerLevel : ObservableObjectBase {

	private bool _isOn;
	private int _powerLevel = 5;

	public const int MinPowerLevel = 1;
	public const int MaxPowerLevel = 10;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the switch is on.
	/// </summary>
	public bool IsOn {
		get => _isOn;
		set => SetProperty(ref _isOn, value);
	}

	/// <summary>
	/// The power level, between <c>1</c> and <c>10</c>.
	/// </summary>
	public int PowerLevel {
		get => _powerLevel;
		set => SetProperty(ref _powerLevel, value.ClampToRange(MinPowerLevel, MaxPowerLevel));
	}

}
