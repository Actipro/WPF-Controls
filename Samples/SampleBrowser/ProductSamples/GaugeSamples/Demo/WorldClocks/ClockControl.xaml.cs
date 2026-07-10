using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.GaugeSamples.Demo.WorldClocks;

/// <summary>
/// Interaction logic for ClockControl.xaml
/// </summary>
public partial class ClockControl : UserControl, INotifyPropertyChanged {

	private double _utcMinutesOffset = double.NaN;
	private static readonly DispatcherTimer _timer;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static ClockControl() {
		_timer = new DispatcherTimer {
			Interval = new TimeSpan(0, 0, 0, 0, 500),
			IsEnabled = true
		};
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public ClockControl() {
		InitializeComponent();
		_timer.Tick += OnTimerTick;

		Unloaded += OnUnloaded;
	}

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	#region INotifyPropertyChanged Members

	/// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
	public event PropertyChangedEventHandler? PropertyChanged;

	#endregion


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current date time.
	/// </summary>
	private DateTime CurrentDateTime
		=> (double.IsNaN(_utcMinutesOffset)) ? DateTime.Now : DateTime.UtcNow.AddMinutes(_utcMinutesOffset);

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="propertyName">Name of the property that changed.</param>
	private void OnPropertyChanged(string? propertyName)
		=> OnPropertyChanged(new PropertyChangedEventArgs(propertyName));

	private void OnTimerTick(object? sender, EventArgs e) {
		OnPropertyChanged(nameof(CurrentHour));
		OnPropertyChanged(nameof(CurrentMinute));
		OnPropertyChanged(nameof(CurrentSecond));
	}

	private void OnUnloaded(object sender, RoutedEventArgs e)
		=> _timer.Tick -= OnTimerTick;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The current hour as a number between 0 (inclusive) and 12 (exclusive).
	/// </summary>
	public double CurrentHour {
		get {
			var now = CurrentDateTime;
			return ((now.Hour) % 12.0) + ((now.Minute) % 60.0) / 60.0;
		}
	}

	/// <summary>
	/// The current minute as a number between 0 (inclusive) and 12 (exclusive).
	/// </summary>
	public double CurrentMinute {
		get {
			var now = CurrentDateTime;
			return ((now.Minute) % 60.0) / 60.0 * 12.0;
		}
	}

	/// <summary>
	/// The current second as a number between 0 (inclusive) and 12 (exclusive).
	/// </summary>
	public double CurrentSecond {
		get {
			var now = CurrentDateTime;
			return ((now.Second) % 60.0) / 60.0 * 12.0;
		}
	}

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="e">The event data.</param>
	protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		=> PropertyChanged?.Invoke(this, e);

	/// <summary>
	/// The UTC minutes offset.
	/// </summary>
	public double UtcMinutesOffset {
		get => _utcMinutesOffset;
		set {
			_utcMinutesOffset = value;

			OnPropertyChanged(nameof(CurrentHour));
			OnPropertyChanged(nameof(CurrentMinute));
			OnPropertyChanged(nameof(CurrentSecond));
		}
	}

}
