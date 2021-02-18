using System;
using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.ProductSamples.MicroChartsSamples.Common;
using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.RegionReport {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private List<RegionData> regionDataSet;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>MainControl</c> class.
		/// </summary>
		public MainControl() {
			InitializeComponent();
			this.DataContext = this;

			// Build the data
			Random rand = new Random();
			regionDataSet = new List<RegionData>();
			regionDataSet.Add(new RegionData() { 
				Name = "Midwest",
				Sales = CreateSalesDataGenerator(Trend.Upward, rand),
				UnitsSold = CreateIntegerDataGenerator(Trend.Upward, rand),
			});
			regionDataSet.Add(new RegionData() { 
				Name = "Northeast",
				Sales = CreateSalesDataGenerator(Trend.Random, rand),
				UnitsSold = CreateIntegerDataGenerator(Trend.Random, rand),
			});
			regionDataSet.Add(new RegionData() { 
				Name = "Southern",
				Sales = CreateSalesDataGenerator(Trend.Upward, rand),
				UnitsSold = CreateIntegerDataGenerator(Trend.Upward, rand),
			});
			regionDataSet.Add(new RegionData() { 
				Name = "Western",
				Sales = CreateSalesDataGenerator(Trend.Downward, rand),
				UnitsSold = CreateIntegerDataGenerator(Trend.Downward, rand),
			});
			foreach (RegionData data in regionDataSet) {
				data.AverageSales = data.Sales.Average(d => d.Amount);
				data.AverageUnitsSold = Convert.ToInt32(data.UnitsSold.Average(d => d.Amount));
				data.MaxSales = data.Sales.Max(d => d.Amount);
				data.MinSales = data.Sales.Min(d => d.Amount);
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a integer data generator.
		/// </summary>
		/// <param name="trend">The trend.</param>
		/// <param name="rand">The random number generator.</param>
		/// <returns>The data generator that was created.</returns>
		private static TimeAggregatedDataGenerator CreateIntegerDataGenerator(Trend trend, Random rand) {
			var generator = new TimeAggregatedDataGenerator() { 
				DataPointCount = 12, 
				StartAmount = 100000 + Convert.ToInt32(400000 * rand.NextDouble()), 
				StepRange = 30000, 
				Trend = trend 
			};
			generator.Generate();
			return generator;
		}

		/// <summary>
		/// Creates a sales data generator.
		/// </summary>
		/// <param name="trend">The trend.</param>
		/// <param name="rand">The random number generator.</param>
		/// <returns>The data generator that was created.</returns>
		private static TimeAggregatedDataGenerator CreateSalesDataGenerator(Trend trend, Random rand) {
			var generator = new TimeAggregatedDataGenerator() { 
				DataPointCount = 12, 
				StartAmount = 2000000 + 3000000 * rand.NextDouble(), 
				StepRange = 100000 * rand.NextDouble(), 
				Trend = trend 
			};
			generator.Generate();
			return generator;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of region data.
		/// </summary>
		/// <value>The collection of region data.</value>
		public IList<RegionData> RegionDataSet {
			get {
				return regionDataSet;
			}
		}

	}
}