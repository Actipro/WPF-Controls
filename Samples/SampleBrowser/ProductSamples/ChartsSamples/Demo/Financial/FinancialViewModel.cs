namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// View model for financial demo.
/// </summary>
public class FinancialViewModel : ObservableObjectBase {

	private const decimal UpdateThreshold = 1.0m;

	private Stock? _selectedStock;
	private decimal? _yAxisMinimum;
	private decimal? _yAxisMaximum;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public FinancialViewModel() {
		InitializeStocks();
		InitializeStockMarkets();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the stock markets.
	/// </summary>
	private void InitializeStockMarkets() {
		StockMarkets.Add(new StockMarket("Dow Jones", 10000.0m, 15000.0m, 70.0m));
		StockMarkets.Add(new StockMarket("NASDAQ", 2000.0m, 3000.0m, 20.0m));
		StockMarkets.Add(new StockMarket("S&P 500", 1000.0m, 2000.0m, 10.0m));

		foreach (var stock in StockMarkets)
			stock.StartUpdatingPriceData();
	}

	/// <summary>
	/// Initializes the stocks.
	/// </summary>
	private void InitializeStocks() {
		Stocks.Add(new Stock("Microsoft", "MSFT"));
		Stocks.Add(new Stock("Apple", "AAPL"));
		Stocks.Add(new Stock("Tesla", "TSLA"));
		Stocks.Add(new Stock("Google", "GOOG"));
		SelectedStock = Stocks[0];

		foreach (var stock in Stocks)
			stock.StartUpdatingPriceData();
	}

	/// <summary>
	/// Called when stock prices change.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The event data.</param>
	private void OnStockPricesChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		=> UpdateAxisMinAndMaxIfPastThreshold();

	/// <summary>
	/// Updates the axis min and max.
	/// </summary>
	private void UpdateAxisMinAndMax() {
		if (SelectedStock is not { } selectedStock)
			return;

		decimal min = selectedStock.StockPrices.Min(priceData => priceData.Price);
		decimal max = selectedStock.StockPrices.Max(priceData => priceData.Price);
		YAxisMinimum = (decimal)Math.Floor((double)(min - UpdateThreshold));
		YAxisMaximum = max + UpdateThreshold;
	}

	/// <summary>
	/// Updates the axis min and max if past threshold.
	/// </summary>
	private void UpdateAxisMinAndMaxIfPastThreshold() {
		if (SelectedStock is not { } selectedStock)
			return;

		decimal min = selectedStock.StockPrices.Min(priceData => priceData.Price);
		decimal max = selectedStock.StockPrices.Max(priceData => priceData.Price);

		if (!YAxisMinimum.HasValue || YAxisMinimum.Value >= min)
			YAxisMinimum = min - UpdateThreshold;

		if (!YAxisMaximum.HasValue || YAxisMaximum.Value <= max)
			YAxisMaximum = max + UpdateThreshold;
	}


	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes this instance.
	/// </summary>
	public void Initialize() {
		foreach (var stock in Stocks)
			stock.StartUpdatingPriceData();
		foreach (var market in StockMarkets)
			market.StartUpdatingPriceData();
	}

	/// <summary>
	/// The selected stock.
	/// </summary>
	public Stock? SelectedStock {
		get { return _selectedStock; }
		set {
			if (_selectedStock is not null)
				_selectedStock.StockPrices.CollectionChanged -= OnStockPricesChanged;

			_selectedStock = value;

			OnPropertyChanged();

			UpdateAxisMinAndMax();
			UpdateAxisMinAndMaxIfPastThreshold();

			if (_selectedStock is not null)
				_selectedStock.StockPrices.CollectionChanged += OnStockPricesChanged;
		}
	}

	/// <summary>
	/// The stocks.
	/// </summary>
	public ObservableCollection<Stock> Stocks { get; } = [];

	/// <summary>
	/// The stock markets.
	/// </summary>
	public ObservableCollection<StockMarket> StockMarkets { get; } = [];

	/// <summary>
	/// Tears down this instance.
	/// </summary>
	public void Teardown() {
		foreach (var stock in Stocks)
			stock.StopUpdatingPriceData();
		foreach (var market in StockMarkets)
			market.StopUpdatingPriceData();
	}

	/// <summary>
	/// The Y axis minimum.
	/// </summary>
	public decimal? YAxisMinimum {
		get => _yAxisMinimum;
		set => SetProperty(ref _yAxisMinimum, value);
	}

	/// <summary>
	/// The Y axis maximum.
	/// </summary>
	public decimal? YAxisMaximum {
		get => _yAxisMaximum;
		set => SetProperty(ref _yAxisMaximum, value);
	}

}
