using System;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.Demo.ScientificDashboard {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private DispatcherTimer timer;

		private const int count = 20;
		private int index = 0;
		private static readonly double[] psiValues = new double[] { 250, 255, 260, 270, 275, 270, 275, 280, 285, 290,
																	100, 105, 125, 145, 165, 185, 205, 225, 245, 255 };
		private static readonly double[] gpmValues = new double[] { 130, 135, 140, 145, 150, 155, 160, 165, 170, 165,
																	160, 155, 160, 155, 152, 150, 145, 142, 140, 135 };
		private static readonly double[] fahrenheitValues = new double[] { 70, 71, 72, 75, 80, 77, 80, 82, 84, 85,
																			90, 91, 88, 84, 80, 82, 78, 74, 72, 68 };

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			timer = new DispatcherTimer();
			timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
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
			psiNeedle.Value = MainControl.psiValues[index];
			gpmNeedle.Value = MainControl.gpmValues[index];
			fahrenheitBar.Value = MainControl.fahrenheitValues[index];

			index++;
			if (index >= MainControl.count)
				index = 0;
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