using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Ranges;

/// <summary>
/// The chart control supports any number of ranges, which can be used to highlight areas of interest along its associated series.
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

		ChartData = new TimeAggregatedDataGenerator([-3.8, -9.7, -11.9, -10.0, -11.5, -5.8, -5.5, -6.4, -2.5, -4.5, 4.0, 9.2, 7.1, -3.0, 7.0, 2.8, 11.2, 12.6, 11.8]);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The chart data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> ChartData { get; }

}
