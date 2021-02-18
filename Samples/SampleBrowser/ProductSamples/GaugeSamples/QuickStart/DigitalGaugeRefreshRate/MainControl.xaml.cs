using System;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.DigitalGaugeRefreshRate {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private Random random;
		private DispatcherTimer timer;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			random = new Random();

			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
			timer.Tick += this.OnTimerTick;
			timer.IsEnabled = true;
		}

		#endregion // OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Handles the <c>Tick</c> event of the <see cref="timer"/> object.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void OnTimerTick(object sender, EventArgs e) {
			gauge.Value = (double)random.Next(10000);
		}

		#endregion // NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			base.NotifyUnloaded();

			if (timer != null) {
				timer.IsEnabled = false;
				timer.Tick -= this.OnTimerTick;
				timer = null;
			}
		}

		#endregion // PUBLIC PROCEDURES

	}

}