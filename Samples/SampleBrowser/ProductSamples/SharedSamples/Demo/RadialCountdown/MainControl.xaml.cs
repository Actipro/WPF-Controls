using System;
using ActiproSoftware.SampleBrowser;

#if WINRT
using ActiproSoftware.SampleBrowser.Controls;
using ActiproSoftware.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Threading;
#endif

#if WPF
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;
#endif

namespace ActiproSoftware.ProductSamples.SharedSamples.Demo.RadialCountdown {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl : ProductItemControl {

		private TimeSpan		duration;
		private DateTimeOffset	startTime;
		private DispatcherTimer timer;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Set an initial value for the sample
			minutesSlider.Value = 1620.0;
			secondsSlider.Value = 270;

			this.UpdateSliceUI(duration);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets whether the timer is running.
		/// </summary>
		/// <value>
		/// <c>true</c> if the timer is running; otherwise, <c>false</c>.
		/// </value>
		private bool IsTimerRunning {
			get {
				return ((timer != null) && (timer.IsEnabled));
			}
		}

		/// <summary>
		/// Occurs when a slider value is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (!this.IsTimerRunning) {
				var hours = (int)(minutesSlider.Value / 360.0);
				var minutes = (int)((minutesSlider.Value % 360.0) / 360.0 * 60);
				var seconds = (int)(secondsSlider.Value / 360.0 * 60);
				duration = new TimeSpan(hours, minutes, seconds);
				
				// Set this manually since it's not tied to a slider
				hoursSlice.EndAngle = 360 * hours / 24;

				durationTextBlock.Text = duration.ToString(@"hh\:mm\:ss");
			}
		}

		/// <summary>
		/// Occurs when the button is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnStartStopButtonClick(object sender, RoutedEventArgs e) {
			this.ToggleTimerRunning();
		}

		/// <summary>
		/// Occurs when the timer ticks.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="Object"/> that contains the event data.</param>
		private void OnTimerTick(object sender, object e) {
			var elapsedDuration = DateTimeOffset.Now - startTime;
			if (elapsedDuration < duration)
				this.UpdateSliceUI(duration - elapsedDuration);
			else if (this.IsTimerRunning) { 
				this.ToggleTimerRunning();
				this.UpdateSliceUI(TimeSpan.Zero);
				duration = TimeSpan.Zero;

				MessageBox.Show("Zero reached.", "Timer", MessageBoxButton.OK);
			}
		}
		
		/// <summary>
		/// Toggles whether the timer is running.
		/// </summary>
		private void ToggleTimerRunning() {
			if (timer == null) {
				timer = new DispatcherTimer();
				timer.Tick += OnTimerTick;
				timer.Interval = TimeSpan.FromMilliseconds(100);
			}

			if (timer.IsEnabled) {
				startStopButton.Content = "\uE102";
				timer.Stop();
			}
			else {
				minutesSlider.Value = minutesSlice.EndAngle;
				secondsSlider.Value = secondsSlice.EndAngle;
				startTime = DateTimeOffset.Now;

				startStopButton.Content = "\uE103";
				timer.Start();
			}

			minutesSlider.Opacity = (timer.IsEnabled ? 0.0 : 1.0);
			secondsSlider.Opacity = (timer.IsEnabled ? 0.0 : 1.0);
		}

		/// <summary>
		/// Updates the slice UI.
		/// </summary>
		/// <param name="remainingDuration">The remaining duration.</param>
		private void UpdateSliceUI(TimeSpan remainingDuration) {
			hoursSlice.EndAngle = 360.0 * remainingDuration.Hours / 24;
			minutesSlice.EndAngle = 360.0 * remainingDuration.Hours + 360.0 * remainingDuration.Minutes / 60;
			secondsSlice.EndAngle = 360.0 * (remainingDuration.Seconds + remainingDuration.Milliseconds / 1000.0) / 60;

			minutesSlider.IntermediateValue = minutesSlice.EndAngle;
			secondsSlider.IntermediateValue = secondsSlice.EndAngle;

			durationTextBlock.Text = remainingDuration.ToString(@"hh\:mm\:ss");
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
			
		/// <summary>
		/// Notifies the UI that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			if (this.IsTimerRunning)
				this.ToggleTimerRunning();
		}

	}
}