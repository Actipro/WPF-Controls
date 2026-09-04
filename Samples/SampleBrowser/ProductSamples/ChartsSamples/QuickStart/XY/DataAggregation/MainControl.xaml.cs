using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.Charts;
using ActiproSoftware.Windows.Extensions;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.DataAggregation;

/// <summary>
/// This QuickStart shows that the data rendered in a chart can be aggregated to reduce the number of data points, improving performance and readability.
/// Several built-in aggregation functions are provided, which include average, first, last, maximum, and minimum.
/// </summary>
public partial class MainControl {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="IsAggregationEnabled"/> property.
	/// </summary>
	public static readonly DependencyProperty IsAggregationEnabledProperty
		= DependencyProperty.Register(nameof(IsAggregationEnabled), typeof(bool), typeof(MainControl), new PropertyMetadata(defaultValue: false, OnIsAggregationEnabledChanged));

	/// <summary>
	/// Defines the <see cref="SelectedAggregationKind"/> property.
	/// </summary>
	public static readonly DependencyProperty SelectedAggregationKindProperty
		= DependencyProperty.Register(nameof(SelectedAggregationKind), typeof(AggregationKind), typeof(MainControl), new PropertyMetadata(defaultValue: AggregationKind.Average, OnSelectedAggregationKindChanged));

	/// <summary>
	/// Defines the <see cref="SelectedSettings"/> property.
	/// </summary>
	public static readonly DependencyProperty SelectedSettingsProperty
		= DependencyProperty.Register(nameof(SelectedSettings), typeof(IEnumerable<AggregationSetting>), typeof(MainControl), new PropertyMetadata(defaultValue: NoneSettings));

	#endregion

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MainControl() {
		var generator = new TimeAggregatedDataGenerator {
			AllowNegativeNumbers = true,
			DataPointCount = 500,
			StartAmount = 0,
			StepRange = 10
		};
		generator.Generate();

		Items = generator;
		MaximumAmount = (decimal)Items.Max(x => x.Amount);
		MinimumAmount = (decimal)Items.Min(x => x.Amount);

		InitializeComponent();
	}


	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void OnIsAggregationEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
		var mainControl = (MainControl)sender;
		mainControl.RefreshSelectedSettings(args.GetNewValue<bool>(), mainControl.SelectedAggregationKind);
	}

	private static void OnSelectedAggregationKindChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
		var mainControl = (MainControl)sender;
		mainControl.RefreshSelectedSettings(mainControl.IsAggregationEnabled, args.GetNewValue<AggregationKind>());
	}

	/// <summary>
	/// Refreshes the selected settings.
	/// </summary>
	/// <param name="isAggregationEnabled">Indicates if aggregation is enabled.</param>
	/// <param name="aggregationKind">The aggregation kind.</param>
	private void RefreshSelectedSettings(bool isAggregationEnabled, AggregationKind aggregationKind) {
		if (!isAggregationEnabled)
			SelectedSettings = NoneSettings;
		else {
			SelectedSettings = aggregationKind switch {
				AggregationKind.Average => AverageSettings,
				AggregationKind.First => FirstSettings,
				AggregationKind.Last => LastSettings,
				AggregationKind.Maximum => MaximumSettings,
				AggregationKind.Minimum => MinimumSettings,
				AggregationKind.SignedMaximum => SignedMaximumSettings,
				AggregationKind.SignedMinimum => SignedMinimumSettings,
				_ => throw new NotImplementedException()
			};
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates whether aggregation is enabled.
	/// </summary>
	public bool IsAggregationEnabled {
		get => (bool)GetValue(IsAggregationEnabledProperty);
		set => SetValue(IsAggregationEnabledProperty, value);
	}

	/// <summary>
	/// The selected aggregation kind.
	/// </summary>
	public AggregationKind SelectedAggregationKind {
		get => (AggregationKind)GetValue(SelectedAggregationKindProperty);
		set => SetValue(SelectedAggregationKindProperty, value);
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> AverageSettings {
		get => [
			new() { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.Average, Factor = 0.50 }
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
			new() { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.First, Factor = 0.50 }
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> LastSettings {
		get => [
			new() { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.Last, Factor = 0.50 }
		];
	}

	/// <summary>
	/// The maximum sale amount.
	/// </summary>
	public decimal MaximumAmount { get; }

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> MaximumSettings {
		get => [
			new() { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.Maximum, Factor = 0.50 }
		];
	}

	/// <summary>
	/// The minimum sale amount.
	/// </summary>
	public decimal MinimumAmount { get; }

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> MinimumSettings {
		get => [
			new() { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.Minimum, Factor = 0.50 }
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> NoneSettings {
		get => [
			new() { IsEnabled = false }
		];
	}

	/// <summary>
	/// The selected settings.
	/// </summary>
	public IEnumerable<AggregationSetting>? SelectedSettings {
		get => (IEnumerable<AggregationSetting>)GetValue(SelectedSettingsProperty);
		set => SetValue(SelectedSettingsProperty, value);
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> SignedMaximumSettings {
		get => [
			new() { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.SignedMaximum, Factor = 0.50 }
		];
	}

	/// <summary>
	/// The aggregation settings for the sample.
	/// </summary>
	public static IEnumerable<AggregationSetting> SignedMinimumSettings {
		get => [
			new() { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.05 },
			new() { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.10 },
			new() { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.25 },
			new() { IsEnabled = true, Kind = AggregationKind.SignedMinimum, Factor = 0.50 }
		];
	}

}
