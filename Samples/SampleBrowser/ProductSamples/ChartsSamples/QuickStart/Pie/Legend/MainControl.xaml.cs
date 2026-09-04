using ActiproSoftware.Windows.Controls.Charts;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.Pie.Legend;

/// <summary>
/// A legend can help make sense out of multiple slices. Our chart offers a wide array of legend customization.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the custom style checkbox is checked.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The event data.</param>
	private void CustomStyleChecked(object sender, RoutedEventArgs args)
		=> chart.LegendStyle = (Style)Resources["CustomStyle"];

	/// <summary>
	/// Occurs when the custom style checkbox is unchecked.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The event data.</param>
	private void CustomStyleUnchecked(object sender, RoutedEventArgs args)
		=> chart.ClearValue(PieChart.LegendStyleProperty);

}
