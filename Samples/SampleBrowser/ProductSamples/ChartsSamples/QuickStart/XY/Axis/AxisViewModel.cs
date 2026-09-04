using ActiproSoftware.ProductSamples.Charts.Common;
using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Axis;

/// <summary>
/// A view model for the Axis Quick Start.
/// </summary>
public class AxisViewModel : ObservableObjectBase {

	private readonly Random _random = new();

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public AxisViewModel() {
		var now = DateTime.Now;
		for (var month = 1; month <= 12; month++) {
			var monthDate = new DateTime(now.Year, month, 1);
			SalesData.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
		}

		SalesData2 = new TimeAggregatedDataGenerator([10.5, 19.5, 14.3, 4.8, 8.4, -1.3, 7.7, 1.8, -1.8, -9.4, -9.7, -6.2, 2.0, 11.2, 18.6, 27.4, 18.7, 11.3, 9.2]);
		SalesData3 = new TimeAggregatedDataGenerator([-1800, -6200, 11300, 9200, 9500, 18500, 14300, 4800, 8400, -1300, 9700, 4000, 10200, 19000, 12000, 11000, 9000, 100, 1800]);

		AxisLabelFunc = GetAxisLabel;
	}


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Returns the axis label.
	/// </summary>
	/// <param name="slotStart">The slot start.</param>
	/// <param name="slotEnd">The slot end.</param>
	private static string GetAxisLabel(DateTime slotStart, DateTime slotEnd) {
		int quarter;
		var year = slotStart.Year;
		for (quarter = 1; quarter <= 4; quarter++) {
			if (
				slotStart >= new DateTime(year, (quarter * 3) - 2, 1)
				&& slotStart < new DateTime(year, quarter * 3, 1)
			) {
				break;
			}
		}

		return string.Format("Q{0}", quarter);
	}

	/// <summary>
	/// Returns a random sales value.
	/// </summary>
	private double GetRandomSalesValue()
		=> _random.Next(10, 40);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The sales data.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesData { get; } = [];

	/// <summary>
	/// The sales data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> SalesData2 { get; }

	/// <summary>
	/// The sales data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> SalesData3 { get; }

	/// <summary>
	/// The axis label func.
	/// </summary>
	public Func<DateTime, DateTime, string> AxisLabelFunc { get; }

}
