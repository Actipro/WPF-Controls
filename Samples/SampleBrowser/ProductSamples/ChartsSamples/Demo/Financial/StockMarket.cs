namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// A stock marker.
/// </summary>
public class StockMarket : Stock {

	private readonly decimal _stockPriceMin;
	private readonly decimal _stockPriceMax;
	private readonly decimal _stockPriceDelta;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="name">The name.</param>
	/// <param name="priceMin">The minimum price.</param>
	/// <param name="priceMax">The maximum price.</param>
	/// <param name="priceDelta">The price delta.</param>
	public StockMarket(string name, decimal priceMin, decimal priceMax, decimal priceDelta)
		: base(name, symbol: null) {

		_stockPriceMin = priceMin;
		_stockPriceMax = priceMax;
		_stockPriceDelta = priceDelta;

		InitializeSampleData();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override decimal StockPriceDelta
		=> _stockPriceDelta;

	/// <inheritdoc/>
	protected override decimal StockPriceMax
		=> _stockPriceMax;

	/// <inheritdoc/>
	protected override decimal StockPriceMin
		=> _stockPriceMin;

}
