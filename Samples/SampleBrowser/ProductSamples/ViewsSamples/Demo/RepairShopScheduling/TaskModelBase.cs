namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling;

/// <summary>
/// Provides an abstract base class for an employee task.
/// </summary>
/// <param name="hours">The number of hours for the task to complete.</param>
public abstract class TaskModelBase(int hours) : ObservableObjectBase {

	private int _hours = hours;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The number of hours that this task requires.
	/// </summary>
	public int Hours {
		get => _hours;
		set => SetProperty(ref _hours, value);
	}

	/// <summary>
	/// The name of the task.
	/// </summary>
	public abstract string Name { get; }

}
