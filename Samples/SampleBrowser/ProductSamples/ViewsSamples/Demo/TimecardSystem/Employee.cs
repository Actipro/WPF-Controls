using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TimecardSystem;

/// <summary>
/// Represents an employee.
/// </summary>
public class Employee : ObservableObjectBase {

	private ClockInOutState _clockState = ClockInOutState.Out;
	private Person? _person;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The clocked in/out state of the employee.
	/// </summary>
	public ClockInOutState ClockState {
		get => _clockState;
		set {
			if (SetProperty(ref _clockState, value))
				OnPropertyChanged(nameof(InverseClockState));
		}
	}

	/// <summary>
	/// The inverse clocked in/out state of the employee.
	/// </summary>
	public ClockInOutState InverseClockState
		=> (ClockState == ClockInOutState.In ? ClockInOutState.Out : ClockInOutState.In);

	/// <summary>
	/// The personal information.
	/// </summary>
	public Person? Person {
		get => _person;
		set => SetProperty(ref _person, value);
	}

}
