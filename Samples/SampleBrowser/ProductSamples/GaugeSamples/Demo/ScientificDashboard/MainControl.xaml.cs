using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.Demo.ScientificDashboard;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private DispatcherTimer? _timer;

	private const int count = 20;
	private int _index = 0;
	
	private static readonly double[] _psiValues =
		[ 250, 255, 260, 270, 275, 270, 275, 280, 285, 290, 100, 105, 125, 145, 165, 185, 205, 225, 245, 255 ];

	private static readonly double[] _gpmValues =
		[ 130, 135, 140, 145, 150, 155, 160, 165, 170, 165, 160, 155, 160, 155, 152, 150, 145, 142, 140, 135 ];

	private static readonly double[] _fahrenheitValues =
		[ 70, 71, 72, 75, 80, 77, 80, 82, 84, 85, 90, 91, 88, 84, 80, 82, 78, 74, 72, 68 ];

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();

		_timer = new DispatcherTimer {
			Interval = new TimeSpan(0, 0, 0, 0, 500)
		};
		_timer.Tick += OnTimerTick;
		_timer.IsEnabled = true;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnTimerTick(object? sender, EventArgs e) {
		psiNeedle.Value = _psiValues[_index];
		gpmNeedle.Value = _gpmValues[_index];
		fahrenheitBar.Value = _fahrenheitValues[_index];

		_index++;
		if (_index >= count)
			_index = 0;
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
