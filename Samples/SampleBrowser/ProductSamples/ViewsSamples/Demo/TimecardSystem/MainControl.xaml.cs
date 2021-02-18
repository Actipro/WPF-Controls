using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows;
using ActiproSoftware.Windows.Media;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TimecardSystem {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private DeferrableObservableCollection<Employee> activeEmployees;
		private DeferrableObservableCollection<Employee> inactiveEmployees;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			EmployeeComparer comparer = new EmployeeComparer();
			this.activeEmployees = new DeferrableObservableCollection<Employee>(comparer);
			this.inactiveEmployees = new DeferrableObservableCollection<Employee>(comparer);

			foreach (var person in People.All)
				this.inactiveEmployees.Add(new Employee() { Person = person });

			InitializeComponent();

			this.Dispatcher.BeginInvoke((Action)(() => {
				inactiveListBox.SelectedIndex = 0;
			}));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Represents a comparer for the <see cref="Employee"/> type.
		/// </summary>
		private class EmployeeComparer : IComparer<Employee> {

			/// <summary>
			/// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
			/// </summary>
			/// <param name="x">The first object to compare.</param>
			/// <param name="y">The second object to compare.</param>
			/// <returns>
			/// Value					Condition
			/// Less than zero			<paramref name="x"/> is less than <paramref name="y"/>.
			/// Zero					<paramref name="x"/> equals <paramref name="y"/>.
			/// Greater than zero		<paramref name="x"/> is greater than <paramref name="y"/>.
			/// </returns>
			public int Compare(Employee x, Employee y) {
				if (null == x && null == y)
					return 0;
				else if (null == x)
					return -1;
				else if (null == y)
					return 1;

				// Sort by last name, then first name
				int sort = x.Person.LastName.CompareTo(y.Person.LastName);
				if (0 != sort)
					return sort;

				return x.Person.FirstName.CompareTo(x.Person.FirstName);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Handles the <c>Click</c> event of the toggle clock state button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnToggleClockStateButtonClick(object sender, RoutedEventArgs e) {
			ListBoxItem item = VisualTreeHelperExtended.GetCurrentOrAncestor(sender as DependencyObject, typeof(ListBoxItem)) as ListBoxItem;
			if (null != item) {
				Employee employee = item.DataContext as Employee;
				if (null != employee) {
					if (employee.ClockState == ClockInOutState.Out) {
						if (this.InactiveEmployees.Remove(employee)) {
							this.ActiveEmployees.Add(employee);
							employee.ClockState = ClockInOutState.In;
							this.activeListBox.SelectedItem = employee;
						}
					}
					else {
						if (this.ActiveEmployees.Remove(employee)) {
							this.InactiveEmployees.Add(employee);
							employee.ClockState = ClockInOutState.Out;
							this.inactiveListBox.SelectedItem = employee;
						}
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the sorted list of active employees.
		/// </summary>
		/// <value>The sorted list of active employees.</value>
		public DeferrableObservableCollection<Employee> ActiveEmployees {
			get {
				return this.activeEmployees;
			}
		}

		/// <summary>
		/// Gets the sorted list of inactive employees.
		/// </summary>
		/// <value>The sorted list of inactive employees.</value>
		public DeferrableObservableCollection<Employee> InactiveEmployees {
			get {
				return this.inactiveEmployees;
			}
		}

	}
}