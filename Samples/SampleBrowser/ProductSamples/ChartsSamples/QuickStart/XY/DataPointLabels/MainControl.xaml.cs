using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.DataPointLabels;

/// <summary>
/// Data point labels can be used to easily identify the value of a particular data point.
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

		ChartData = new TimeAggregatedDataGenerator([19.5, 4.8, -1.3, 1.8, -9.4, -6.2, 11.2, 27.4, 11.3]);
		ChartData2 = new TimeAggregatedDataGenerator([-6200, 9200, 18500, 4800, -1300, 4000, 12000, 9000, 1800]);
		CustomLabelFunc = GetCustomLabel;
	}


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the custom label.
	/// </summary>
	/// <param name="primaryValue">The primary value.</param>
	/// <param name="secondaryValue">The secondary value.</param>
	/// <param name="xValue">The x value.</param>
	/// <param name="yValue">The y value.</param>
	/// <param name="originalValue">The original value.</param>
	public static string GetCustomLabel(object primaryValue, object secondaryValue, object xValue, object yValue, object originalValue)
		=> string.Format("X: {0}{1}Y: {2}", xValue, Environment.NewLine, yValue);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The chart data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> ChartData { get; }

	/// <summary>
	/// The chart data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> ChartData2 { get; }

	/// <summary>
	/// The custom label function.
	/// </summary>
	public Func<object, object, object, object, object, string> CustomLabelFunc { get; }

}
