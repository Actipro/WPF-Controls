using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.Demo.WorldClocks {
	/// <summary>
	/// Interaction logic for ClockControl.xaml
	/// </summary>
	public partial class ClockControl : UserControl, INotifyPropertyChanged {
		
		private double utcMinutesOffset = double.NaN;
		private static DispatcherTimer timer;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the <see cref="ClockControl"/> class.
		/// </summary>
		static ClockControl() {
			ClockControl.timer = new DispatcherTimer();
			ClockControl.timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
			ClockControl.timer.IsEnabled = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ClockControl"/> class.
		/// </summary>
		public ClockControl() {
			InitializeComponent();
			ClockControl.timer.Tick += this.OnTimerTick;

			this.Unloaded += this.OnUnloaded;
		}

		#endregion // OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the current date time.
		/// </summary>
		/// <value>The current date time.</value>
		private DateTime CurrentDateTime {
			get {
				return (double.IsNaN(this.utcMinutesOffset)) ? DateTime.Now : DateTime.UtcNow.AddMinutes(this.utcMinutesOffset);
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">Name of the property that changed.</param>
		private void OnPropertyChanged(string propertyName) {
			this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Handles the <c>Tick</c> event of the <see cref="timer"/> object.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void OnTimerTick(object sender, EventArgs e) {
			OnPropertyChanged("CurrentHour");
			OnPropertyChanged("CurrentMinute");
			OnPropertyChanged("CurrentSecond");
		}
		
		/// <summary>
		/// Occurs when the control is unloaded.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private void OnUnloaded(object sender, RoutedEventArgs e) {
			ClockControl.timer.Tick -= this.OnTimerTick;
		}

		#endregion // NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the current hour as a number between 0 (inclusive) and 12 (exclusive).
		/// </summary>
		/// <value>The current hour.</value>
		public double CurrentHour {
			get {
				DateTime now = this.CurrentDateTime;
				return (((double)now.Hour) % 12.0) + (((double)now.Minute) % 60.0) / 60.0;
			}
		}

		/// <summary>
		/// Gets the current minute as a number between 0 (inclusive) and 12 (exclusive).
		/// </summary>
		/// <value>The current minute.</value>
		public double CurrentMinute {
			get {
				DateTime now = this.CurrentDateTime;
				return (((double)now.Minute) % 60.0) / 60.0 * 12.0;
			}
		}

		/// <summary>
		/// Gets the current second as a number between 0 (inclusive) and 12 (exclusive).
		/// </summary>
		/// <value>The current second.</value>
		public double CurrentSecond {
			get {
				DateTime now = this.CurrentDateTime;
				return (((double)now.Second) % 60.0) / 60.0 * 12.0;
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
			PropertyChangedEventHandler eventHandlers = this.PropertyChanged;
			if (null != eventHandlers)
				eventHandlers(this, e);
		}

		/// <summary>
		/// Gets or sets the UTC minutes offset.
		/// </summary>
		/// <value>The UTC minutes offset.</value>
		public double UtcMinutesOffset {
			get {
				return this.utcMinutesOffset;
			}
			set {
				this.utcMinutesOffset = value;
				OnPropertyChanged("CurrentHour");
				OnPropertyChanged("CurrentMinute");
				OnPropertyChanged("CurrentSecond");
			}
		}

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion // INotifyPropertyChanged Members

		#endregion // PUBLIC PROCEDURES
	}
}
