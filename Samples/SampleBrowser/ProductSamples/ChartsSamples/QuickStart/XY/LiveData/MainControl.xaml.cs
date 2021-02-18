using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ActiproSoftware.Compatibility;
using ActiproSoftware.SampleBrowser.SampleData;
#if WINRT
using ActiproSoftware.UI.Xaml;
using Windows.UI.Xaml;
#else
using System.Windows;
using System.Windows.Threading;
using ActiproSoftware.ProductSamples.Charts.Common;
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.LiveData {

	/// <summary>
	///	A chart's data can be fixed or based on a live feed, such as a stock ticker. 
	/// This example shows two data sources that are updated at a specified interval using random data.
	/// </summary>
	public partial class MainControl {

		private const int StockPriceMin = 10;
		private const int StockPriceMax = 70;
		private const int StockPriceDelta = 10;

		private readonly DeferrableObservableCollection<TimeAggregatedData> chartData1 = new DeferrableObservableCollection<TimeAggregatedData>();
		private readonly DeferrableObservableCollection<TimeAggregatedData> chartData2 = new DeferrableObservableCollection<TimeAggregatedData>();
		private static readonly Random random = new Random();
		private DispatcherTimer timer;

		#region Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Interval"/> dependency property.  This field is read-only.
		/// </summary>
		/// <value>The identifier for the <see cref="Interval"/> dependency property.</value>
		public static readonly DependencyProperty IntervalProperty = DependencyPropertyEx.Register("Interval",
			typeof(double), typeof(MainControl), new FrameworkPropertyMetadata(500.0, OnIntervalPropertyValueChanged));

		#endregion // Dependency Properties

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeSampleData();
			InitializeAndStartTimer();

			InitializeComponent();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the next value.
		/// </summary>
		/// <returns>The next value.</returns>
		private double GetRandomStockPriceNear(double previousValue) {
			int lowValue = Math.Max(StockPriceMin, (int)previousValue - StockPriceDelta);
			int highValue = Math.Min(StockPriceMax, (int)previousValue + StockPriceDelta);
			return random.Next(lowValue, highValue) + random.NextDouble();
		}

		/// <summary>
		/// Gets a random stock price.
		/// </summary>
		/// <returns></returns>
		private double GetRandomStockPrice() {
			return random.Next(StockPriceMin, StockPriceMax) + random.NextDouble();
		}

		/// <summary>
		/// Initializes and starts the timer to update data.
		/// </summary>
		private void InitializeAndStartTimer() {
			timer = new DispatcherTimer();
			timer.Tick += OnTimerTick;
			timer.Interval = TimeSpan.FromMilliseconds(Interval);
			timer.Start();
		}

		/// <summary>
		/// Initializes the sample data.
		/// </summary>
		private void InitializeSampleData() {

			double lastData1Value = GetRandomStockPrice();
			double lastData2Value = GetRandomStockPrice();

			for (int i = 0; i < 100; i++) {
				chartData1.Add(new TimeAggregatedData(i, TimePeriod.Year, DateTime.Now, lastData1Value));
				chartData2.Add(new TimeAggregatedData(i, TimePeriod.Year, DateTime.Now, lastData2Value));
				lastData1Value = GetRandomStockPriceNear(lastData1Value);
				lastData2Value = GetRandomStockPriceNear(lastData2Value);
			}
		}

		/// <summary>
		/// Occurs when the <see cref="IntervalProperty"/> value is changed.
		/// </summary>
		/// <param name="d">The <see cref="DependencyObject"/> whose property is changed.</param>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private static void OnIntervalPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var control = d as MainControl;
			if (control != null)
				control.timer.Interval = TimeSpan.FromMilliseconds(control.Interval);
		}

		/// <summary>
		/// Occurs when the timer ticks.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The event data.</param>
		#if WINRT
		private void OnTimerTick(object sender, object args) {
		#else
		private void OnTimerTick(object sender, EventArgs args) {
		#endif
			UpdateLiveData();
		}

		/// <summary>
		/// Updates the live data.
		/// </summary>
		private void UpdateLiveData() {
			SuspendDataUpdatesWhile(() => {
				chartData1.RemoveAt(0);
				var lastData1 = chartData1.Last();
				var newSalesData1 = new TimeAggregatedData(lastData1.Index + 1, TimePeriod.Year, DateTime.Now, GetRandomStockPriceNear(lastData1.Amount));
				chartData1.Add(newSalesData1);

				chartData2.RemoveAt(0);
				var lastData2 = chartData2.Last();
				var newSalesData2 = new TimeAggregatedData(lastData2.Index + 1, TimePeriod.Year, DateTime.Now, GetRandomStockPriceNear(lastData2.Amount));
				chartData2.Add(newSalesData2);
			});
		}

		/// <summary>
		/// Suspends the data updates while a given action occurs.
		/// </summary>
		/// <param name="action">The action.</param>
		private void SuspendDataUpdatesWhile(Action action) {
			chartData1.BeginUpdate();
			chartData2.BeginUpdate();
			try {
				action();
			}
			finally {
				chartData1.EndUpdate();
				chartData2.EndUpdate();
			}
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the data used by this sample.
		/// </summary>
		public DeferrableObservableCollection<TimeAggregatedData> ChartData1 {
			get {
				return chartData1;
			}
		}

		/// <summary>
		/// Gets the data used by this sample.
		/// </summary>
		public DeferrableObservableCollection<TimeAggregatedData> ChartData2 {
			get {
				return chartData2;
			}
		}

		/// <summary>
		/// Gets or sets the interval at which the data is updated.
		/// This is a dependency property.
		/// </summary>
		/// <value>
		/// The interval at which the data is updated.
		/// The default value is <c>500.0</c> ms.
		/// </value>
		public double Interval {
			get { return (double)GetValue(IntervalProperty); }
			set { SetValue(IntervalProperty, value); }
		}

		/// <summary>
		/// Notifies the sample that it has been unloaded.
		/// </summary>
		public override void NotifyUnloaded() {
			timer.Stop();
		}

		#endregion PUBLIC PROCEDURES
	}
}
