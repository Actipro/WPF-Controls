using System.Collections.Specialized;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling;

/// <summary>
/// Stores information about an employee.
/// </summary>
public class EmployeeModel : ObservableObjectBase {

	private string _name;
	private EmployeeStatus _status = EmployeeStatus.Unavailable;
	private int _totalTaskHours;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The employee name.</param>
	public EmployeeModel(string name) {
		_name = name;

		Tasks.CollectionChanged += OnTasksCollectionChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnTasksCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		UpdateTotalTaskHours();

		switch (e.Action) {
			case NotifyCollectionChangedAction.Remove:
				if ((Tasks.Count == 0) && (Status == EmployeeStatus.Working))
					Status = EmployeeStatus.Idle;
				break;
		}
	}

	/// <summary>
	/// Updates the <see cref="TotalTaskHours"/> property.
	/// </summary>
	private void UpdateTotalTaskHours()
		=> TotalTaskHours = Tasks.OfType<ServiceModel>().Sum(m => m.Hours);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The employee name.
	/// </summary>
	public string Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// The employee status.
	/// </summary>
	public EmployeeStatus Status {
		get => _status;
		set {
			if (SetProperty(ref _status, value))
				OnPropertyChanged(nameof(StatusNumber));
		}
	}

	/// <summary>
	/// The number related to the <see cref="Status"/>.
	/// </summary>
	public double StatusNumber {
		get {
			return Status switch {
				EmployeeStatus.Unavailable => 0,
				EmployeeStatus.Idle => 1,
				EmployeeStatus.Working or _ => 2
			};
		}
	}

	/// <summary>
	/// The collection of tasks scheduled for the employee.
	/// </summary>
	public ObservableCollection<TaskModelBase> Tasks { get; } = [];

	/// <summary>
	/// The total count of task hours.
	/// </summary>
	public int TotalTaskHours {
		get => _totalTaskHours;
		private set => SetProperty(ref _totalTaskHours, value);
	}

}
