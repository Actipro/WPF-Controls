namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.Gallery;

/// <summary>
/// Stores data about a store.
/// </summary>
public class StoreData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The name.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The net profit.
	/// </summary>
	public double NetProfit { get; set; }

	/// <summary>
	/// The collection of sales data.
	/// </summary>
	public ICollection<double>? Sales { get; set; }

	/// <summary>
	/// The target sales amount.
	/// </summary>
	public double TargetSales { get; set; }

}
