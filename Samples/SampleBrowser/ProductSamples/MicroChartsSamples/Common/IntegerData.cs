namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Stores integer-related data, and is used by various samples for this product.
/// Any similar custom data objects could be used to generate chart data.
/// </summary>
/// <param name="date">The date for which the amount is specified.</param>
/// <param name="value">The count.</param>
public class IntegerData(DateTime date, int value) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The date for which the amount is specified.
	/// </summary>
	public DateTime Date { get; } = date;

	/// <summary>
	/// The value.
	/// </summary>
	public int Value { get; } = value;

}
