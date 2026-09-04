namespace ActiproSoftware.ProductSamples.Charts.Common;

/// <summary>
/// Represents sales data for a given month.
/// </summary>
/// <param name="sales">The sales.</param>
/// <param name="month">The month.</param>
public class MonthlySalesData(double sales, DateTime month) : ObservableObjectBase {

	private DateTime _month = month;
	private double _sales = sales;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The month.
	/// </summary>
	public DateTime Month {
		get => _month;
		private set => SetProperty(ref _month, value);
	}

	/// <summary>
	/// The sales.
	/// </summary>
	public double Sales {
		get => _sales;
		private set => SetProperty(ref _sales, value);
	}

}
