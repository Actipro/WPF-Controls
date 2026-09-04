namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Stores options for <see cref="IntegerData"/> generation.
/// </summary>
public class IntegerDataOptions {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The number of data objects to generate.
	/// </summary>
	public int Count { get; set; } = 12;

	/// <summary>
	/// The description.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// The start value.
	/// </summary>
	[TypeConverter(typeof(ConvertibleTypeConverter<int>))]
	public int StartValue { get; set; } = 10;

	/// <summary>
	/// The range over which any amount can change from the previous amount.
	/// </summary>
	[TypeConverter(typeof(ConvertibleTypeConverter<int>))]
	public int StepRange { get; set; } = 8;

	/// <summary>
	/// The sets the step range adjustment so that steps can trend up/down.
	/// </summary>
	/// <remarks>
	/// <c>0.5</c> means trend evenly.  Low numbers means trend toward higher amounts.
	/// </remarks>
	public double TrendPercentage { get; set; } = 0.5;

}
