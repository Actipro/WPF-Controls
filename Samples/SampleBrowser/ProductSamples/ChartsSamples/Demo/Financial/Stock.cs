using System;
using System.Linq;
#if WINRT
using ActiproSoftware.UI.Xaml;
using Windows.UI.Xaml;
#else
using System.Windows.Threading;
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial {

	/// <summary>
	/// Represents a stock.
	/// </summary>
	public class Stock : ObservableObjectBase {

		private const int StockUpdateInterval = 2500;
		private static readonly TimeSpan StockUpdateTimeSpan = new TimeSpan(0, 0, 05, 0, 0);
		private static readonly Random random = new Random();
		private readonly DeferrableObservableCollection<PriceData> stockPrices = new DeferrableObservableCollection<PriceData>();

		private DispatcherTimer timer;
		private string symbol;
		private string name;
		private PriceData currentPrice;
		private PriceData yesterdayPrice;
		private bool isPriceUp;
		private decimal change;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>Stock</c> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="symbol">The symbol.</param>
		public Stock(string name, string symbol) {
			Name = name;
			Symbol = symbol;
			InitializeSampleData();
			InitializeTimer();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Calculates the change in prices.
		/// </summary>
		private void CalculateChange() {
			Change = CurrentPrice.Price - YesterdayPrice.Price;
			IsPriceUp = Change >= 0;
		}

		/// <summary>
		/// Gets the random stock price near a given value.
		/// </summary>
		/// <param name="previousValue">The previous value.</param>
		/// <returns>A random stock price.</returns>
		private decimal GetRandomStockPriceNear(decimal previousValue) {
			decimal lowValue = Math.Max(StockPriceMin, previousValue - StockPriceDelta);
			decimal highValue = Math.Min(StockPriceMax, previousValue + StockPriceDelta);
			return RandomNext(lowValue, highValue);
		}

		/// <summary>
		/// Gets a random stock price.
		/// </summary>
		/// <returns>A random stock price.</returns>
		private decimal GetRandomStockPrice() {
			return RandomNext(StockPriceMin, StockPriceMax);
		}

		/// <summary>
		/// Initializes and starts the timer.
		/// </summary>
		private void InitializeTimer() {
			timer = new DispatcherTimer();
			timer.Tick += OnTimerTick;
			timer.Interval = TimeSpan.FromMilliseconds(StockUpdateInterval);
		}

		/// <summary>
		/// Called when the timer ticks.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The instance containing the event data.</param>
		#if WINRT
		private void OnTimerTick(object sender, object args) {
		#else
		private void OnTimerTick(object sender, EventArgs args) {
		#endif
			UpdateLiveData();
			CalculateChange();
		}

		/// <summary>
		/// Gets the next random number between a minimum and maximum value.
		/// </summary>
		/// <param name="minimum">The minimum value.</param>
		/// <param name="maximum">The maximum value.</param>
		/// <returns>A random number.</returns>
		private decimal RandomNext(decimal minimum, decimal maximum) {
			decimal randomNumber = (decimal)(random.Next() + random.NextDouble());
			decimal diff = maximum - minimum;
			if (diff == 0)
				diff = 1;

			decimal rnd = randomNumber % diff;
			return minimum + rnd;
		}

		/// <summary>
		/// Suspends the data updates while a given action occurs.
		/// </summary>
		/// <param name="action">The action.</param>
		private void SuspendDataUpdatesWhile(Action action) {
			StockPrices.BeginUpdate();
			try {
				action();
			}
			finally {
				StockPrices.EndUpdate();
			}
		}

		/// <summary>
		/// Updates the live data.
		/// </summary>
		private void UpdateLiveData() {
			SuspendDataUpdatesWhile(() => {

				for (int i = 0; i < 6; i++) {
					StockPrices.RemoveAt(0);
				}

				PriceData lastStockData;
				for (int i = 0; i < 6; i++) {
					lastStockData = StockPrices.Last();
					DateTime stockDate = lastStockData.Date.Add(StockUpdateTimeSpan);
					decimal stockPrice = GetRandomStockPriceNear(lastStockData.Price);
					StockPrices.Add(new PriceData(stockPrice, stockDate));
				}

				CurrentPrice = StockPrices.Last();
			});
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the change.
		/// </summary>
		/// <value>The change.</value>
		public decimal Change {
			get { return change; }
			set {
				change = value;
				NotifyPropertyChanged("Change");
			}
		}

		/// <summary>
		/// Gets the current price.
		/// </summary>
		/// <value>The current price.</value>
		public PriceData CurrentPrice {
			get { return currentPrice; }
			private set {
				currentPrice = value;
				NotifyPropertyChanged("CurrentPrice");
			}
		}

		/// <summary>
		/// Initializes the sample data.
		/// </summary>
		protected void InitializeSampleData() {
			StockPrices.Clear();
			DateTime now = DateTime.Now;
			now = now.AddMinutes(-(now.Minute % 30));
			DateTime lastStockDate = now;
			decimal lastStockValue = GetRandomStockPrice();
			var lastStockData = new PriceData(lastStockValue, lastStockDate);
			for (int i = 0; i < 61; i++) {
				StockPrices.Insert(0, lastStockData);
				lastStockDate = lastStockDate.Subtract(StockUpdateTimeSpan);
				lastStockValue = GetRandomStockPriceNear(lastStockValue);
				lastStockData = new PriceData(lastStockValue, lastStockDate);
			}

			YesterdayPrice = StockPrices.First();
			CurrentPrice = StockPrices.Last();
			CalculateChange();
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is price up.
		/// </summary>
		/// <value><c>true</c> if this instance is price up; otherwise, <c>false</c>.</value>
		public bool IsPriceUp {
			get { return isPriceUp; }
			set {
				isPriceUp = value;
				NotifyPropertyChanged("IsPriceUp");
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name {
			get { return name; }
			set {
				name = value;
				NotifyPropertyChanged("Name");
			}
		}

		/// <summary>
		/// Starts updating price data.
		/// Must call stop when finished to avoid memory leak.
		/// </summary>
		public void StartUpdatingPriceData() {
			timer.Start();
		}

		/// <summary>
		/// Stops updating price data.
		/// </summary>
		public void StopUpdatingPriceData() {
			timer.Stop();
		}

		/// <summary>
		/// Gets the stock price delta.
		/// </summary>
		/// <value>The stock price delta.</value>
		protected virtual decimal StockPriceDelta { get { return 0.2m; } }

		/// <summary>
		/// Gets the stock price min.
		/// </summary>
		/// <value>The stock price min.</value>
		protected virtual decimal StockPriceMin { get { return 1.0m; } }

		/// <summary>
		/// Gets the stock price max.
		/// </summary>
		/// <value>The stock price max.</value>
		protected virtual decimal StockPriceMax { get { return 400.0m; } }

		/// <summary>
		/// Gets the stock prices.
		/// </summary>
		/// <value>The stock prices.</value>
		public DeferrableObservableCollection<PriceData> StockPrices {
			get { return stockPrices; }
		}

		/// <summary>
		/// Gets or sets the symbol.
		/// </summary>
		/// <value>The symbol.</value>
		public string Symbol {
			get { return symbol; }
			set {
				symbol = value;
				NotifyPropertyChanged("Symbol");
			}
		}

		/// <summary>
		/// Gets the yesterday price.
		/// </summary>
		/// <value>The yesterday price.</value>
		public PriceData YesterdayPrice {
			get { return yesterdayPrice; }
			private set {
				yesterdayPrice = value;
				NotifyPropertyChanged("YesterdayPrice");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
