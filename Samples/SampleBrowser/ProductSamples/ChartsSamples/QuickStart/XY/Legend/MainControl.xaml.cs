using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.Charts;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Legend;

/// <summary>
/// A legend can help make sense out of multiple series. Our chart offers a wide array of legend customization.
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

		ChartData1 = new TimeAggregatedDataGenerator([42.3, 52.0, 43.2, 37.0, 33.2, 43.0, 51.7, 41.6, 42.2, 37.0, 41.3, 50.4, 57.9, 55.5, 56.9, 60.1, 66.2, 59.6, 60.6]);
		ChartData2 = new TimeAggregatedDataGenerator([39.6, 30.3, 20.7, 23.8, 27.5, 35.8, 40.3, 38.1, 43.8, 44.6, 41.7, 49.0, 49.3, 48.6, 53.6, 50.6, 49.6, 42.8, 51.2]);
		ChartData3 = new TimeAggregatedDataGenerator([25.5, 21.2, 11.3, 17.3, 11.9, 20.7, 12.7, 21.8, 30.2, 22.0, 19.7, 10.5, 12.9, 10.9, 14.6, 20.2, 12.7, 14.7, 20.7]);
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
		=> chart.ClearValue(XYChart.LegendStyleProperty);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The chart data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> ChartData1 { get; }

	/// <summary>
	/// The chart data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> ChartData2 { get; }

	/// <summary>
	/// The chart data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> ChartData3 { get; }

}
