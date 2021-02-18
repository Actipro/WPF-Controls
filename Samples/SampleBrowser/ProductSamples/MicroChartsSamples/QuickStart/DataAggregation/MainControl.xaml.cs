using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows.Controls.MicroCharts;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.QuickStart.DataAggregation {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
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

			this.Items = generator;
			this.MaximumAmount = this.Items.Max(x => x.Amount);
			this.MinimumAmount = this.Items.Min(x => x.Amount);

			InitializeComponent();
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> AverageSettings {
			get {
				return new AggregationSetting[] {
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.05 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.10 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.25 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Average, Factor = 0.50 },
				};
			}
		}

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		public IEnumerable<TimeAggregatedData> Items {
			get;
			private set;
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> FirstSettings {
			get {
				return new AggregationSetting[] {
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.05 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.10 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.25 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.First, Factor = 0.50 },
				};
			}
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> LastSettings {
			get {
				return new AggregationSetting[] {
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.05 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.10 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.25 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Last, Factor = 0.50 },
				};
			}
		}

		/// <summary>
		/// Gets the maximum sale amount.
		/// </summary>
		public double MaximumAmount {
			get;
			private set;
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> MaximumSettings {
			get {
				return new AggregationSetting[] {
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.05 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.10 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.25 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Maximum, Factor = 0.50 },
				};
			}
		}

		/// <summary>
		/// Gets the minimum sale amount.
		/// </summary>
		public double MinimumAmount {
			get;
			private set;
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> MinimumSettings {
			get {
				return new AggregationSetting[] {
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.05 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.10 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.25 },
					new AggregationSetting() { IsEnabled = true, Kind = MicroAggregationKind.Minimum, Factor = 0.50 },
				};
			}
		}

		/// <summary>
		/// Gets the aggregation settings for the sample.
		/// </summary>
		/// <value>The aggregation settings for the sample.</value>
		public static IEnumerable<AggregationSetting> NoneSettings {
			get {
				return new AggregationSetting[] {
					new AggregationSetting() { IsEnabled = false },
				};
			}
		}

	}
}