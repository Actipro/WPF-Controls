using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.RegionReport;

/// <summary>
/// Stores data about a region.
/// </summary>
public class RegionData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The average sales.
	/// </summary>
	public double AverageSales { get; set; }

	/// <summary>
	/// The average units sold.
	/// </summary>
	public int AverageUnitsSold { get; set; }

	/// <summary>
	/// Gets or sets max sales.
	/// </summary>
	public double MaxSales { get; set; }

	/// <summary>
	/// The min sales.
	/// </summary>
	public double MinSales { get; set; }

	/// <summary>
	/// The name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The collection of sales data.
	/// </summary>
	public ICollection<TimeAggregatedData>? Sales { get; set; }

	/// <summary>
	/// The collection of units sold data.
	/// </summary>
	public ICollection<TimeAggregatedData>? UnitsSold { get; set; }

}
