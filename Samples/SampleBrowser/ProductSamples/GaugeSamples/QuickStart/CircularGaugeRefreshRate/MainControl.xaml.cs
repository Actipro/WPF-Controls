using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.CircularGaugeRefreshRate;

/// <summary>
/// Interaction logic for MainControl.xaml
/// </summary>
public partial class MainControl {

	private readonly Random _random;
	private DispatcherTimer? _timer;

	////////////////////////////////////////////////////////////////////////
	// OBJECT
	////////////////////////////////////////////////////////////////////////

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		_random = new Random();

		_timer = new DispatcherTimer {
			Interval = new TimeSpan(0, 0, 0, 0, 100)
		};
		_timer.Tick += OnTimerTick;
		_timer.IsEnabled = true;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnTimerTick(object? sender, EventArgs e)
		=> needle.Value = _random.Next(100);

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
