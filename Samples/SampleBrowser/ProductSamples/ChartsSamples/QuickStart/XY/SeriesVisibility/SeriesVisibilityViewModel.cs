using ActiproSoftware.ProductSamples.Charts.Common;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.SeriesVisibility;

/// <summary>
/// The view model for the Series Visibility QuickStart.
/// </summary>
public class SeriesVisibilityViewModel : ObservableObjectBase {

	private readonly Random _random = new();

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SeriesVisibilityViewModel() {
		InitializeSalesData();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the sales data.
	/// </summary>
	private void InitializeSalesData() {
		var now = DateTime.Now;
		for (var month = 1; month <= 12; month++) {
			var monthDate = new DateTime(now.Year, month, 1);
			SalesDataDetroit.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
			SalesDataNewYork.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
			SalesDataSeattle.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
			SalesDataLosAngeles.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
		}
	}

	/// <summary>
	/// Returns a random sales value.
	/// </summary>
	private double GetRandomSalesValue()
		=> _random.Next(10000, 400000);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The sales data for Detroit.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataDetroit { get; } = [];

	/// <summary>
	/// The sales data for Los Angeles.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataLosAngeles { get; } = [];

	/// <summary>
	/// The sales data for New York.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataNewYork { get; } = [];

	/// <summary>
	/// The sales data for Seattle.
	/// </summary>
	public ObservableCollection<MonthlySalesData> SalesDataSeattle { get; } = [];

}
