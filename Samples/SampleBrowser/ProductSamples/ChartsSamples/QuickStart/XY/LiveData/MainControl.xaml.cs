using ActiproSoftware.Compatibility;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.LiveData;

/// <summary>
/// A chart's data can be fixed or based on a live feed, such as a stock ticker.
/// This example shows two data sources that are updated at a specified interval using random data.
/// </summary>
public partial class MainControl {

	private const int StockPriceMin = 10;
	private const int StockPriceMax = 70;
	private const int StockPriceDelta = 10;

	private static readonly Random _random = new();

	private DispatcherTimer? _timer;

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
		InitializeSampleData();
		InitializeAndStartTimer();

		InitializeComponent();
	}


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the next value.
	/// </summary>
	private static double GetRandomStockPriceNear(double previousValue) {
		int lowValue = Math.Max(StockPriceMin, (int)previousValue - StockPriceDelta);
		int highValue = Math.Min(StockPriceMax, (int)previousValue + StockPriceDelta);
		return _random.Next(lowValue, highValue) + _random.NextDouble();
	}

	/// <summary>
	/// Returns a random stock price.
	/// </summary>
	private static double GetRandomStockPrice()
		=> _random.Next(StockPriceMin, StockPriceMax) + _random.NextDouble();

	/// <summary>
	/// Initializes and starts the timer to update data.
	/// </summary>
	private void InitializeAndStartTimer() {
		_timer = new DispatcherTimer();
		_timer.Tick += OnTimerTick;
		_timer.Interval = TimeSpan.FromMilliseconds(Interval);
		_timer.Start();
	}

	/// <summary>
	/// Initializes the sample data.
	/// </summary>
	private void InitializeSampleData() {
		var lastData1Value = GetRandomStockPrice();
		var lastData2Value = GetRandomStockPrice();

		for (var i = 0; i < 100; i++) {
			ChartData1.Add(new TimeAggregatedData(i, TimePeriod.Year, DateTime.Now, lastData1Value));
			lastData1Value = GetRandomStockPriceNear(lastData1Value);

			ChartData2.Add(new TimeAggregatedData(i, TimePeriod.Year, DateTime.Now, lastData2Value));
			lastData2Value = GetRandomStockPriceNear(lastData2Value);
		}
	}

	/// <summary>
	/// Occurs when the <see cref="IntervalProperty"/> value is changed.
	/// </summary>
	/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
	/// <param name="e">The event data.</param>
	private static void OnIntervalPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
		if (d is MainControl { _timer: not null } control)
			control._timer.Interval = TimeSpan.FromMilliseconds(control.Interval);
	}

	/// <summary>
	/// Occurs when the timer ticks.
	/// </summary>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="args">The event data.</param>
	private void OnTimerTick(object? sender, EventArgs args)
		=> UpdateLiveData();

	/// <summary>
	/// Updates the live data.
	/// </summary>
	private void UpdateLiveData() {
		SuspendDataUpdatesWhile(() => {
			ChartData1.RemoveAt(0);
			var lastData1 = ChartData1.Last();
			var newSalesData1 = new TimeAggregatedData(lastData1.Index + 1, TimePeriod.Year, DateTime.Now, GetRandomStockPriceNear(lastData1.Amount));
			ChartData1.Add(newSalesData1);

			ChartData2.RemoveAt(0);
			var lastData2 = ChartData2.Last();
			var newSalesData2 = new TimeAggregatedData(lastData2.Index + 1, TimePeriod.Year, DateTime.Now, GetRandomStockPriceNear(lastData2.Amount));
			ChartData2.Add(newSalesData2);
		});
	}

	/// <summary>
	/// Suspends the data updates while a given action occurs.
	/// </summary>
	/// <param name="action">The action.</param>
	private void SuspendDataUpdatesWhile(Action action) {
		ChartData1.BeginUpdate();
		ChartData2.BeginUpdate();
		try {
			action();
		}
		finally {
			ChartData1.EndUpdate();
			ChartData2.EndUpdate();
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The data used by this sample.
	/// </summary>
	public DeferrableObservableCollection<TimeAggregatedData> ChartData1 { get; } = [];

	/// <summary>
	/// The data used by this sample.
	/// </summary>
	public DeferrableObservableCollection<TimeAggregatedData> ChartData2 { get; } = [];

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
		=> _timer?.Stop();

}
