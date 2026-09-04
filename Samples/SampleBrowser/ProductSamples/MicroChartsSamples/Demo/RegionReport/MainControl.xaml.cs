using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.RegionReport;

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
		var random = new Random();
		RegionDataSet = [
			new RegionData() {
				Name = "Midwest",
				Sales = CreateSalesDataGenerator(Trend.Upward, random),
				UnitsSold = CreateIntegerDataGenerator(Trend.Upward, random),
			},
			new RegionData() {
				Name = "Northeast",
				Sales = CreateSalesDataGenerator(Trend.Random, random),
				UnitsSold = CreateIntegerDataGenerator(Trend.Random, random),
			},
			new RegionData() {
				Name = "Southern",
				Sales = CreateSalesDataGenerator(Trend.Upward, random),
				UnitsSold = CreateIntegerDataGenerator(Trend.Upward, random),
			},
			new RegionData() {
				Name = "Western",
				Sales = CreateSalesDataGenerator(Trend.Downward, random),
				UnitsSold = CreateIntegerDataGenerator(Trend.Downward, random),
			},
		];
		foreach (var data in RegionDataSet) {
			data.AverageSales = data.Sales!.Average(d => d.Amount);
			data.AverageUnitsSold = Convert.ToInt32(data.UnitsSold!.Average(d => d.Amount));
			data.MaxSales = data.Sales!.Max(d => d.Amount);
			data.MinSales = data.Sales!.Min(d => d.Amount);
		}
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a integer data generator.
	/// </summary>
	/// <param name="trend">The trend.</param>
	/// <param name="random">The random number generator.</param>
	private static TimeAggregatedDataGenerator CreateIntegerDataGenerator(Trend trend, Random random) {
		var generator = new TimeAggregatedDataGenerator() {
			DataPointCount = 12,
			StartAmount = 100000 + Convert.ToInt32(400000 * random.NextDouble()),
			StepRange = 30000,
			Trend = trend
		};
		generator.Generate();
		return generator;
	}

	/// <summary>
	/// Creates a sales data generator.
	/// </summary>
	/// <param name="trend">The trend.</param>
	/// <param name="random">The random number generator.</param>
	private static TimeAggregatedDataGenerator CreateSalesDataGenerator(Trend trend, Random random) {
		var generator = new TimeAggregatedDataGenerator() {
			DataPointCount = 12,
			StartAmount = 2000000 + 3000000 * random.NextDouble(),
			StepRange = 100000 * random.NextDouble(),
			Trend = trend
		};
		generator.Generate();
		return generator;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of region data.
	/// </summary>
	public IList<RegionData> RegionDataSet { get; }

}
