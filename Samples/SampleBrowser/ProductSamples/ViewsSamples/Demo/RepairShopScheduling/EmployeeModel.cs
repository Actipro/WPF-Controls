using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Stores information about an employee.
	/// </summary>
	public class EmployeeModel : ObservableObjectBase {
		
		private string										name;
		private EmployeeStatus								status					= EmployeeStatus.Unavailable;
		private ObservableCollection<TaskModelBase>			tasks					= new ObservableCollection<TaskModelBase>();
		private int											totalTaskHours;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>EmployeeModel</c> class.
		/// </summary>
		/// <param name="name">The employee name.</param>
		public EmployeeModel(string name) {
			this.Name = name;

			tasks.CollectionChanged += OnTasksCollectionChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the collection is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> containing data related to this event.</param>
		private void OnTasksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			this.UpdateTotalTaskHours();

			if (e != null) {
				switch (e.Action) {
					case NotifyCollectionChangedAction.Remove:
						if ((tasks.Count == 0) && (this.Status == EmployeeStatus.Working))
							this.Status = EmployeeStatus.Idle;
						break;
				}
			}
		}
		
		/// <summary>
		/// Updates the <see cref="TotalTaskHours"/> property.
		/// </summary>
		private void UpdateTotalTaskHours() {
			this.TotalTaskHours = tasks.OfType<ServiceModel>().Sum(o => o.Hours);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the employee name.
		/// </summary>
		/// <value>The employee name.</value>
		public string Name {
			get {
				return name;
			}
			set {
				if (name != value) {
					name = value;
					this.NotifyPropertyChanged("Name");
				}
			}
		}

		/// <summary>
		/// Gets or sets the employee status.
		/// </summary>
		/// <value>The employee status.</value>
		public EmployeeStatus Status {
			get {
				return status;
			}
			set {
				if (status != value) {
					status = value;
					this.NotifyPropertyChanged("Status");
					this.NotifyPropertyChanged("StatusNumber");
				}
			}
		}

		/// <summary>
		/// Gets the number related to the <see cref="Status"/>.
		/// </summary>
		/// <value>The number related to the <see cref="Status"/>.</value>
		public double StatusNumber {
			get {
				switch (status) {
					case EmployeeStatus.Unavailable:
						return 0;
					case EmployeeStatus.Idle:
						return 1;
					default:
						return 2;
				}
			}
		}
		
		/// <summary>
		/// Gets the collection of tasks scheduled for the employee.
		/// </summary>
		/// <value>The collection of tasks scheduled for the employee.</value>
		public ObservableCollection<TaskModelBase> Tasks {
			get {
				return tasks;
			}
		}
		
		/// <summary>
		/// Gets or sets the total count of task hours.
		/// </summary>
		/// <value>The total count of task hours.</value>
		public int TotalTaskHours {
			get {
				return totalTaskHours;
			}
			private set {
				if (totalTaskHours != value) {
					totalTaskHours = value;
					this.NotifyPropertyChanged("TotalTaskHours");
				}
			}
		}

	}

}
