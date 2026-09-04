using ActiproSoftware.Extensions;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Extensions;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TimecardSystem;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		var comparer = new EmployeeComparer();
		ActiveEmployees = new DeferrableObservableCollection<Employee>(comparer);
		InactiveEmployees = new DeferrableObservableCollection<Employee>(comparer);

		foreach (var person in People.All)
			InactiveEmployees.Add(new Employee() { Person = person });

		InitializeComponent();

		Dispatcher.BeginInvoke(() => {
			inactiveListBox.SelectedIndex = 0;
		});
	}

	// --------------------------------------------------------------------------------------------------
	// NESTED TYPES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Represents a comparer for the <see cref="Employee"/> type.
	/// </summary>
	private class EmployeeComparer : IComparer<Employee> {

		/// <inheritdoc cref="IComparer{T}.Compare"/>
		public int Compare(Employee? x, Employee? y) {
			if (x?.Person is null && y?.Person is null)
				return 0;
			else if (x?.Person is null)
				return -1;
			else if (y?.Person is null)
				return 1;

			// Sort by last name, then first name
			var sort = x.Person.LastName.CompareTo(y.Person.LastName);
			if (sort != 0)
				return sort;

			return x.Person.FirstName.CompareTo(x.Person.FirstName);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnToggleClockStateButtonClick(object sender, RoutedEventArgs e) {
		var item = (sender as DependencyObject)?.FindAncestorOfType<ListBoxItem>(includeSelf: true);
		var employee = item?.DataContext as Employee;
		if (employee is not null) {
			if (employee.ClockState == ClockInOutState.Out) {
				if (InactiveEmployees.Remove(employee)) {
					ActiveEmployees.Add(employee);
					employee.ClockState = ClockInOutState.In;
					activeListBox.SelectedItem = employee;
				}
			}
			else {
				if (ActiveEmployees.Remove(employee)) {
					InactiveEmployees.Add(employee);
					employee.ClockState = ClockInOutState.Out;
					inactiveListBox.SelectedItem = employee;
				}
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The sorted list of active employees.
	/// </summary>
	public DeferrableObservableCollection<Employee> ActiveEmployees { get; }

	/// <summary>
	/// The sorted list of inactive employees.
	/// </summary>
	public DeferrableObservableCollection<Employee> InactiveEmployees { get; }

}
