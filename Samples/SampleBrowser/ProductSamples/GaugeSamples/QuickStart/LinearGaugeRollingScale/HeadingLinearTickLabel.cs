using ActiproSoftware.Windows.Controls.Gauge;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale;

/// <summary>
/// Represents major tick label that normalizes heading directions between <c>0</c> and <c>359</c>.
/// </summary>
public class HeadingLinearTickLabel : LinearTickLabelMajor {

	private readonly HeadingConverter _converter = new();

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override string? GetValueText(double value)
		=> _converter.Convert(value, typeof(string), parameter: null, culture: null) as string;

}
