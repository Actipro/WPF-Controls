using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeDampening;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private int _counter;
	private readonly Random _random;
	private DispatcherTimer? _timer;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		_counter = 0;
		_random = new Random();

		_timer = new DispatcherTimer {
			Interval = new TimeSpan(0, 0, 0, 0, 200)
		};
		_timer.Tick += OnTimerTick;
		_timer.IsEnabled = true;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnTimerTick(object? sender, EventArgs e) {
		var delta = (_random.NextDouble() * 6) - 3;
		if ((_counter++ % 10) == 0)
			delta *= 3;
		marker.Value += delta;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void NotifyUnloaded() {
		base.NotifyUnloaded();

		if (_timer is not null) {
			_timer.IsEnabled = false;
			_timer.Tick -= OnTimerTick;
			_timer = null;
		}
	}


}
