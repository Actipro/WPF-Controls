using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.MicroCharts;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.DataAggregation;

/// <summary>
/// Provides the main user control for this sample.
/// </summary>
public partial class MainControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		var generator = new TimeAggregatedDataGenerator() {
			RandomSeed = 67344234,
			AllowNegativeNumbers = true,
			DataPointCount = 500,
			StartAmount = 0,
			StepRange = 10,
		};
		generator.Generate();

		Items = generator;
		MaximumAmount = Items.Max(x => x.Amount);
		MinimumAmount = Items.Min(x => x.Amount);

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> AverageSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.50 },
		];
	}

	/// <summary>
	/// The sales data.
	/// </summary>
	public IEnumerable<TimeAggregatedData> Items { get; }

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> FirstSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.50 },
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> LastSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.50 },
		];
	}

	/// <summary>
	/// The maximum sale amount.
	/// </summary>
	public double MaximumAmount { get; }

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> MaximumSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.50 },
		];
	}

	/// <summary>
	/// The minimum sale amount.
	/// </summary>
	public double MinimumAmount { get; }

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> MinimumSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.50 },
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> NoneSettings {
		get => [
			new() { IsEnabled = false },
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> SignedMaximumSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMaximum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMaximum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMaximum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMaximum, Factor = 0.50 },
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> SignedMinimumSettings {
		get => [
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMinimum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMinimum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMinimum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = MicroAggregationKind.SignedMinimum, Factor = 0.50 },
		];
	}

}
