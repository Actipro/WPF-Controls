using ActiproSoftware.Windows.Controls.Charts;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.DataAggregation;

/// <summary>
/// Stores an aggregation setting.
/// </summary>
public class AggregationSetting {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The aggregation factor used.
	/// </summary>
	public double Factor { get; set; }

	/// <summary>
	/// Indicates whether aggregation is enabled.
	/// </summary>
	public bool IsEnabled { get; set; }

	/// <summary>
	/// The kind of aggregation used.
	/// </summary>
	public AggregationKind Kind { get; set; }

	/// <inheritdoc/>
	public override string ToString() {
		return !IsEnabled
			? "No aggregation"
			: string.Format("Factor {0:F2}", Factor);
	}

}
