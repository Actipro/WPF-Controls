namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.Gallery;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private List<StoreData>? _storeDataSet;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		InitializeComponent();
		DataContext = this;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The collection of store data.
	/// </summary>
	public IList<StoreData> StoreDataSet {
		get {
			return _storeDataSet ??= [
				new StoreData() {
					Name = "New York",
					NetProfit = 1855176,
					Sales = [581415, 591966, 492003, 460123, 523962, 622962, 649196, 789123, 800124, 750126, 741612, 720556],
					TargetSales = 600000,
				},
				new StoreData() {
					Name = "Chicago",
					NetProfit = 1094124,
					Sales = [318624, 358185, 381725, 310128, 251929, 370125, 380120, 354501, 263105, 492031, 423123, 380125],
					TargetSales = 370000,
				}
			];
		}
	}

}
