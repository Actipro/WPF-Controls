namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial;

/// <summary>
/// Represents a price on a specific date.
/// </summary>
/// <param name="price">The price.</param>
/// <param name="date">The date.</param>
public class PriceData(decimal price, DateTime date) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The date.
	/// </summary>
	public DateTime Date { get; } = date;

	/// <summary>
	/// The price.
	/// </summary>
	public decimal Price { get; } = price;

}
