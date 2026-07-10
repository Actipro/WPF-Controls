using ActiproSoftware.Windows.Controls.MicroCharts;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.DataAggregation;

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
	public MicroAggregationKind Kind { get; set; }

	/// <inheritdoc/>
	public override string ToString() {
		return IsEnabled
			? string.Format("Factor {0:F2}", Factor)
			: string.Empty;
	}

}
