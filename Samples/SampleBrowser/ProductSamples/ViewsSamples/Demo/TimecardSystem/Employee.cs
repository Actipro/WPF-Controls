using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows;

namespace ActiproSoftware.ProductSamples.ViewsSamples.Demo.TimecardSystem {

	/// <summary>
	/// Represents an employee.
	/// </summary>
	public class Employee : ObservableObjectBase {

		private ClockInOutState clockState = ClockInOutState.Out;
		private Person person;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the clocked in/out state of the employee.
		/// </summary>
		/// <value>The clocked in/out state of the employee.</value>
		public ClockInOutState ClockState {
			get {
				return this.clockState;
			}
			set {
				if (this.clockState != value) {
					this.clockState = value;
					this.NotifyPropertyChanged("ClockState");
					this.NotifyPropertyChanged("InverseClockState");
				}
			}
		}

		/// <summary>
		/// Gets the inverse clocked in/out state of the employee.
		/// </summary>
		/// <value>The inverse clocked in/out state of the employee.</value>
		public ClockInOutState InverseClockState {
			get {
				return (clockState == ClockInOutState.In ? ClockInOutState.Out : ClockInOutState.In);
			}
		}

		/// <summary>
		/// Gets or sets the personal information.
		/// </summary>
		/// <value>The personal information.</value>
		public Person Person {
			get {
				return this.person;
			}
			set {
				if (this.person != value) {
					this.person = value;
					this.NotifyPropertyChanged("Person");
				}
			}
		}

	}
}
