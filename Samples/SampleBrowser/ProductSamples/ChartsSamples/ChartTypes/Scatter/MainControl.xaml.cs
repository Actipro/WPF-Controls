namespace ActiproSoftware.ProductSamples.ChartsSamples.ChartTypes.Scatter;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	private readonly Random _random = new();

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		DataContext = this;

		InitializeComponent();
		InitializeSampleDataContext();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the sample data context.
	/// </summary>
	private void InitializeSampleDataContext() {
		for (var i = 0; i < 1000; i++) {
			var modulus = i % 2;
			var xm = i / (20.0d + 3 * modulus);
			var ym = 10.0d + 2 * modulus;
			var x = _random.NextDouble() * xm + 1;
			var y = Math.Log(ym * (x - 1.0) + 1.0) * (_random.NextDouble() + 0.9);

			if (modulus == 0)
				PrimaryChartPoints1.Add(new Point(x, y));
			else
				PrimaryChartPoints2.Add(new Point(x, y));
		}
	}


	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The primary chart's points for series 1.
	/// </summary>
	public ObservableCollection<Point> PrimaryChartPoints1 { get; } = [];

	/// <summary>
	/// The primary chart's points for series 2.
	/// </summary>
	public ObservableCollection<Point> PrimaryChartPoints2 { get; } = [];

}
