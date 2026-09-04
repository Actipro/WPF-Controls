using ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.StockReport;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		DataContext = this;

		// Build the data
		var random = new Random(3);
		StockDataSet = [
			new("AAPL", CreatePriceDataGenerator(130.2m, random).DataSets[0]),
			new("BAC", CreatePriceDataGenerator(8.2m, random).DataSets[0]),
			new("F", CreatePriceDataGenerator(11.39m, random).DataSets[0]),
			new("GE", CreatePriceDataGenerator(19.54m, random).DataSets[0]),
			new("GOOG", CreatePriceDataGenerator(101.4m, random).DataSets[0]),
			new("IBM", CreatePriceDataGenerator(99.1m, random).DataSets[0]),
			new("MMI", CreatePriceDataGenerator(38.14m, random).DataSets[0]),
			new("NFLX", CreatePriceDataGenerator(87.88m, random).DataSets[0]),
			new("T", CreatePriceDataGenerator(31.72m, random).DataSets[0]),
			new("XOM", CreatePriceDataGenerator(86.31m, random).DataSets[0]),
		];
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a price data generator.
	/// </summary>
	/// <param name="startPrice">The start price.</param>
	/// <param name="random">The random number generator.</param>
	private static SalesDataGenerator CreatePriceDataGenerator(decimal startPrice, Random random) {
		return new() {
			Options = new() {
				Count = 30,
				StartAmount = startPrice,
				StepRange = startPrice / 5 * Convert.ToDecimal(random.NextDouble()),
				TrendPercentage = 0.4 + 0.2 * random.NextDouble()
			}
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The high price.
	/// </summary>
	public double HighPrice
		=> Convert.ToDouble(StockDataSet.Max(d => d.HighPriceData.Amount));

	/// <summary>
	/// The collection of stock data.
	/// </summary>
	public IList<StockData> StockDataSet { get; }

}
