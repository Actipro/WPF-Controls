using System;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeDampening {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private int counter;
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

			counter = 0;
			random = new Random();

			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
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
			double delta = (random.NextDouble() * 6) - 3;
			if (0 == (counter++ % 10))
				delta *= 3;
			marker.Value += delta;
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