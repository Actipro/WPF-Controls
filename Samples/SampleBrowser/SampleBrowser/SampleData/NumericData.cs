namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Stores simple numeric data.
/// </summary>
/// <param name="amount">The sales amount.</param>
public class NumericData(double amount) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The amount.
	/// </summary>
	public double Amount { get; } = amount;

}
