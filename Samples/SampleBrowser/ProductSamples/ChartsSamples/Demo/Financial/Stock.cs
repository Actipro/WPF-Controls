using ActiproSoftware.Windows;
using System.Windows.Threading;

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// Represents a stock.
/// </summary>
public class Stock : ObservableObjectBase {

	private const int StockUpdateInterval = 2500;
	private static readonly TimeSpan StockUpdateTimeSpan = new(0, 0, 05, 0, 0);
	private static readonly Random _random = new();

	private DispatcherTimer? _timer;
	private string? _symbol;
	private string _name;
	private PriceData? _currentPrice;
	private PriceData? _yesterdayPrice;
	private bool _isPriceUp;
	private decimal _change;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The name.</param>
	/// <param name="symbol">The symbol.</param>
	public Stock(string name, string? symbol) {
		_name = name;
		_symbol = symbol;
		InitializeSampleData();
		InitializeTimer();
	}


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Calculates the change in prices.
	/// </summary>
	private void CalculateChange() {
		if (
			CurrentPrice?.Price is { } currentPrice
			&& YesterdayPrice?.Price is { } yesterdayPrice
		) {
			Change = currentPrice - yesterdayPrice;
			IsPriceUp = Change >= 0;
		}
	}

	/// <summary>
	/// Returns a random stock price near a given value.
	/// </summary>
	/// <param name="previousValue">The previous value.</param>
	private decimal GetRandomStockPriceNear(decimal previousValue) {
		decimal lowValue = Math.Max(StockPriceMin, previousValue - StockPriceDelta);
		decimal highValue = Math.Min(StockPriceMax, previousValue + StockPriceDelta);
		return RandomNext(lowValue, highValue);
	}

	/// <summary>
	/// Returns a random stock price.
	/// </summary>
	private decimal GetRandomStockPrice()
		=> RandomNext(StockPriceMin, StockPriceMax);

	/// <summary>
	/// Initializes and starts the timer.
	/// </summary>
	private void InitializeTimer() {
		_timer = new DispatcherTimer();
		_timer.Tick += OnTimerTick;
		_timer.Interval = TimeSpan.FromMilliseconds(StockUpdateInterval);
	}

	/// <summary>
	/// Called when the timer ticks.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The event data.</param>
	private void OnTimerTick(object? sender, EventArgs args) {
		UpdateLiveData();
		CalculateChange();
	}

	/// <summary>
	/// Returns the next random number between a minimum and maximum value.
	/// </summary>
	/// <param name="minimum">The minimum value.</param>
	/// <param name="maximum">The maximum value.</param>
	private static decimal RandomNext(decimal minimum, decimal maximum) {
		var randomNumber = (decimal)(_random.Next() + _random.NextDouble());
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

			for (var i = 0; i < 6; i++)
				StockPrices.RemoveAt(0);

			for (var i = 0; i < 6; i++) {
				var lastStockData = StockPrices.Last();
				var stockDate = lastStockData.Date.Add(StockUpdateTimeSpan);
				var stockPrice = GetRandomStockPriceNear(lastStockData.Price);
				StockPrices.Add(new PriceData(stockPrice, stockDate));
			}

			CurrentPrice = StockPrices.Last();
		});
	}


	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The change.
	/// </summary>
	public decimal Change {
		get => _change;
		set => SetProperty(ref _change, value);
	}

	/// <summary>
	/// The current price.
	/// </summary>
	public PriceData? CurrentPrice {
		get => _currentPrice;
		private set => SetProperty(ref _currentPrice, value);
	}

	/// <summary>
	/// Initializes the sample data.
	/// </summary>
	protected void InitializeSampleData() {
		StockPrices.Clear();
		var now = DateTime.Now;
		now = now.AddMinutes(-(now.Minute % 30));

		var lastStockDate = now;
		var lastStockValue = GetRandomStockPrice();
		var lastStockData = new PriceData(lastStockValue, lastStockDate);
		for (var i = 0; i < 61; i++) {
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
	/// Indicates whether this instance is price up.
	/// </summary>
	public bool IsPriceUp {
		get => _isPriceUp;
		set => SetProperty(ref _isPriceUp, value);
	}

	/// <summary>
	/// The name.
	/// </summary>
	public string Name {
		get => _name;
		set => SetProperty(ref _name, value);
	}

	/// <summary>
	/// Starts updating price data.
	/// </summary>
	/// <remarks>
	/// Must call stop when finished to avoid memory leak.
	/// </remarks>
	public void StartUpdatingPriceData()
		=> _timer?.Start();

	/// <summary>
	/// Stops updating price data.
	/// </summary>
	public void StopUpdatingPriceData()
		=> _timer?.Stop();

	/// <summary>
	/// The stock price delta.
	/// </summary>
	protected virtual decimal StockPriceDelta
		=> 0.2m;

	/// <summary>
	/// The stock price min.
	/// </summary>
	protected virtual decimal StockPriceMin
		=> 1.0m;

	/// <summary>
	/// The stock price max.
	/// </summary>
	protected virtual decimal StockPriceMax
		=> 400.0m;

	/// <summary>
	/// The stock prices.
	/// </summary>
	public DeferrableObservableCollection<PriceData> StockPrices { get; } = [];

	/// <summary>
	/// The symbol.
	/// </summary>
	public string? Symbol {
		get => _symbol;
		set => SetProperty(ref _symbol, value);
	}

	/// <summary>
	/// The yesterday price.
	/// </summary>
	public PriceData? YesterdayPrice {
		get => _yesterdayPrice;
		private set => SetProperty(ref _yesterdayPrice, value);
	}

}
