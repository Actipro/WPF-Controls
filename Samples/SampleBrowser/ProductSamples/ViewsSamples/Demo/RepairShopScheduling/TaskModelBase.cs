#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.RepairShopScheduling {

	/// <summary>
	/// Provides an abstract base class for an employee task.
	/// </summary>
	public abstract class TaskModelBase : ObservableObjectBase {

		private int			hours;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the <c>TaskModelBase</c> class.
		/// </summary>
		/// <param name="hours">The number of hours for the task to complete.</param>
		protected TaskModelBase(int hours) {
			this.Hours = hours;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the number of hours that this task requires.
		/// </summary>
		/// <value>The number of hours that this task requires.</value>
		public int Hours {
			get {
				return hours;
			}
			set {
				if (hours != value) {
					hours = value;
					this.NotifyPropertyChanged("Hours");
				}
			}
		}

		/// <summary>
		/// Gets the name of the task.
		/// </summary>
		/// <value>The name of the task.</value>
		public abstract string Name { get; }
		
	}

}
