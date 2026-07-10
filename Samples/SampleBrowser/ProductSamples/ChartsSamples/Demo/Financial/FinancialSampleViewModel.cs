namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// A sample view model for the Financial demo.
/// </summary>
public class FinancialSampleViewModel {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The selected stock.
	/// </summary>
	public Stock? SelectedStock { get; set; }

	/// <summary>
	/// The selected stock deferred.
	/// </summary>
	public Stock? SelectedStockDeferred { get; set; }

	/// <summary>
	/// The stocks.
	/// </summary>
	public ObservableCollection<Stock> Stocks { get; } = [];

	/// <summary>
	/// The stock markets.
	/// </summary>
	public ObservableCollection<StockMarket> StockMarkets { get; } = [];

	/// <summary>
	/// The Y axis minimum.
	/// </summary>
	public decimal? YAxisMinimum { get; set; }

	/// <summary>
	/// The Y axis maximum.
	/// </summary>
	public decimal? YAxisMaximum { get; set; }

}
