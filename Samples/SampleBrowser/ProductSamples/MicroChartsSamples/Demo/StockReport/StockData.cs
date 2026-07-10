using ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.StockReport;

/// <summary>
/// Stores data about a stock.
/// </summary>
public class StockData {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public StockData(string symbol, ICollection<SalesData> prices) {
		Symbol = symbol;
		Prices = prices;

		StartPrice = prices!.First().Amount;
		LowPriceData = prices!.OrderBy(d => d.Amount).First();
		HighPriceData = prices!.OrderByDescending(d => d.Amount).First();
		EndPrice = prices!.Last().Amount;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The delta in price.
	/// </summary>
	public decimal DeltaPrice
		=> EndPrice - StartPrice;

	/// <summary>
	/// The end price.
	/// </summary>
	public decimal EndPrice { get; }

	/// <summary>
	/// The high price data.
	/// </summary>
	public SalesData HighPriceData { get; }

	/// <summary>
	/// The low price data.
	/// </summary>
	public SalesData LowPriceData { get; }

	/// <summary>
	/// The start price.
	/// </summary>
	public decimal StartPrice { get; }

	/// <summary>
	/// The ticker symbol.
	/// </summary>
	public string Symbol { get; }

	/// <summary>
	/// The collection of prices.
	/// </summary>
	public ICollection<SalesData> Prices { get; }

}
