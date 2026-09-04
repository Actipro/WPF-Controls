using ActiproSoftware.ProductSamples.Charts.Common;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.BarChartSlotting;

/// <summary>
/// A view model for the Bar Chart Slotting QuickStart.
/// </summary>
public class BarChartSlottingViewModel : ObservableObjectBase {

	private readonly Random _random = new();

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BarChartSlottingViewModel() {
		InitializeSalesData();
		LabelFunc = GetAxisLabel;
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
		if (slotStart.Year == slotEnd.Year && slotStart.Month == slotEnd.Month)
			return slotStart.ToString("MMM", CultureInfo.CurrentCulture);

		return slotStart.ToString("MMM", CultureInfo.CurrentCulture) + " - " + slotEnd.ToString("MMM", CultureInfo.CurrentCulture);
	}

	/// <summary>
	/// Returns a random sales value.
	/// </summary>
	private double GetRandomSalesValue()
		=> _random.Next(1, 5);

	/// <summary>
	/// Initializes the sales data.
	/// </summary>
	private void InitializeSalesData() {
		var now = DateTime.Now;
		for (var month = 1; month <= 12; month++) {
			var monthDate = new DateTime(now.Year, month, 1);
			SalesDataDetroit.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
			SalesDataLosAngeles.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));

			var quarterMonthDate = new DateTime(now.Year, month, _random.Next(1, 27));
			SalesDataForQuarters.Add(new MonthlySalesData(GetRandomSalesValue(), quarterMonthDate));
		}
	}


	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public Func<DateTime, DateTime, string> LabelFunc { get; }

	/// <summary>
	/// The sales data for Detroit.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataDetroit { get; } = [];

	/// <summary>
	/// The sales data for Los Angeles.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataLosAngeles { get; } = [];

	/// <summary>
	/// The sales data for quarters.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataForQuarters { get; } = [];

}
