using ActiproSoftware.Compatibility;
using ActiproSoftware.Windows;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.LiveData;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly Random _random = new(Environment.TickCount);
	private readonly DispatcherTimer _timer;

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Interval"/> property.
	/// </summary>
	public static readonly DependencyProperty IntervalProperty
		= DependencyPropertyEx.Register(nameof(Interval), typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(defaultValue: 500.0, OnIntervalPropertyValueChanged));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		for (var i = 0; i < 100; i++) {
			Data1.Add(GetNextValue());
			Data2.Add(GetNextValue());
		}

		_timer = new DispatcherTimer();
		_timer.Tick += OnTimerTick;
		_timer.Interval = TimeSpan.FromMilliseconds(Interval);
		_timer.Start();

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the next value.
	/// </summary>
	private double GetNextValue()
		=> 100 + _random.NextDouble() * 10;

	private static void OnIntervalPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is MainControl control)
			control._timer.Interval = TimeSpan.FromMilliseconds(control.Interval);
	}

	private void OnTimerTick(object? sender, EventArgs e) {
		Data1.BeginUpdate();
		Data2.BeginUpdate();
		try {
			Data1.RemoveAt(0);
			Data1.Add(GetNextValue());

			Data2.RemoveAt(0);
			Data2.Add(GetNextValue());
		}
		finally {
			Data1.EndUpdate();
			Data2.EndUpdate();
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The data used by this sample.
	/// </summary>
	public DeferrableObservableCollection<double> Data1 { get; } = [];

	/// <summary>
	/// The data used by this sample.
	/// </summary>
	public DeferrableObservableCollection<double> Data2 { get; } = [];

	/// <summary>
	/// The interval at which the data is updated.
	/// </summary>
	/// <value>
	/// The default value is <c>500.0</c> ms.
	/// </value>
	public double Interval {
		get => (double)GetValue(IntervalProperty);
		set => SetValue(IntervalProperty, value);
	}

	/// <inheritdoc/>
	public override void NotifyUnloaded()
		=> _timer.Stop();

}
