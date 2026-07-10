namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Stores sales-related data, and is used by various samples for this product.
/// Any similar custom data objects could be used to generate chart data.
/// </summary>
/// <param name="date">The date for which the amount is specified.</param>
/// <param name="amount">The sales amount.</param>
public class SalesData(DateTime date, decimal amount) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The sales amount.
	/// </summary>
	public decimal Amount { get; } = amount;

	/// <summary>
	/// The date for which the amount is specified.
	/// </summary>
	public DateTime Date { get; } = date;

}
