using ActiproSoftware.SampleBrowser;
using System.Windows.Threading;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SharedSamples.Demo.RadialCountdown;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl : ProductItemControl {

	private TimeSpan _duration;
	private DateTimeOffset _startTime;
	private DispatcherTimer? _timer;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		// Set an initial value for the sample
		minutesSlider.Value = 1620.0;
		secondsSlider.Value = 270;

		UpdateSliceUI(_duration);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether the timer is running.
	/// </summary>
	private bool IsTimerRunning
		=> _timer?.IsEnabled == true;

	private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
		if (!IsTimerRunning) {
			var hours = (int)(minutesSlider.Value / 360.0);
			var minutes = (int)((minutesSlider.Value % 360.0) / 360.0 * 60);
			var seconds = (int)(secondsSlider.Value / 360.0 * 60);
			_duration = new TimeSpan(hours, minutes, seconds);

			// Set this manually since it's not tied to a slider
			hoursSlice.EndAngle = 360 * hours / 24;

			durationTextBlock.Text = _duration.ToString(@"hh\:mm\:ss");
		}
	}

	private void OnStartStopButtonClick(object sender, RoutedEventArgs e)
		=> ToggleTimerRunning();

	private void OnTimerTick(object? sender, object e) {
		var elapsedDuration = (DateTimeOffset.Now - _startTime);
		if (elapsedDuration < _duration)
			UpdateSliceUI(_duration - elapsedDuration);
		else if (IsTimerRunning) {
			ToggleTimerRunning();
			UpdateSliceUI(TimeSpan.Zero);
			_duration = TimeSpan.Zero;

			MessageBox.Show("Zero reached.", "Timer", MessageBoxButton.OK);
		}
	}

	/// <summary>
	/// Toggles whether the timer is running.
	/// </summary>
	private void ToggleTimerRunning() {
		if (_timer is null) {
			_timer = new DispatcherTimer();
			_timer.Tick += OnTimerTick;
			_timer.Interval = TimeSpan.FromMilliseconds(100);
		}

		if (_timer.IsEnabled) {
			startStopButton.Content = "\uE102";
			_timer.Stop();
		}
		else {
			minutesSlider.Value = minutesSlice.EndAngle;
			secondsSlider.Value = secondsSlice.EndAngle;
			_startTime = DateTimeOffset.Now;

			startStopButton.Content = "\uE103";
			_timer.Start();
		}

		minutesSlider.Opacity = (_timer.IsEnabled ? 0.0 : 1.0);
		secondsSlider.Opacity = (_timer.IsEnabled ? 0.0 : 1.0);
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

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		if (IsTimerRunning)
			ToggleTimerRunning();
	}

}
