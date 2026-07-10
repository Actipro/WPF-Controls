namespace ActiproSoftware.SampleBrowser.SampleData;

/// <summary>
/// Stores time-aggregated data, and is used by various samples for this product.
/// Any similar custom data objects could be used to generate chart data.
/// </summary>
/// <param name="index">The data item index.</param>
/// <param name="timePeriod">The time period.</param>
/// <param name="date">The time period start date for which the amount is specified.</param>
/// <param name="amount">The sales amount.</param>
public class TimeAggregatedData(int index, TimePeriod timePeriod, DateTime date, double amount) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The amount.
	/// </summary>
	public double Amount { get; } = amount;

	/// <summary>
	/// The time period start date for which the amount is specified.
	/// </summary>
	public DateTime Date { get; } = date;

	/// <summary>
	/// The text label for the <see cref="Date"/>.
	/// </summary>
	public string DateLabel {
		get => TimePeriod switch {
			TimePeriod.Month => Date.ToString("MMM"),
			TimePeriod.Week => Date.ToString("MMM dd"),
			TimePeriod.Year or _ => Date.ToString("yyyy")
		};
	}

	/// <summary>
	/// The data item index.
	/// </summary>
	public int Index { get; } = index;

	/// <summary>
	/// The partitions of the data.
	/// </summary>
	public IList<NumericData>? Partitions { get; set; }

	/// <summary>
	/// The time period.
	/// </summary>
	public TimePeriod TimePeriod { get; } = timePeriod;

	/// <summary>
	/// The title.
	/// </summary>
	public string? Title { get; set; }

}
