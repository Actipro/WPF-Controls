using System;
using System.Collections.Generic;
using System.Linq;
using ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

#if WINRT
using Windows.UI.Xaml.Controls;
#else
using System.Windows.Controls;
#endif

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.StockReport {

	/// <summary>
	/// Provides the main user control for this sample.
	/// </summary>
	public partial class MainControl {

		private List<StockData> stockDataSet;

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
			Random rand = new Random(3);
			stockDataSet = new List<StockData>();
			stockDataSet.Add(new StockData() { Symbol = "AAPL", Prices = CreatePriceDataGenerator(130.2m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "BAC", Prices = CreatePriceDataGenerator(8.2m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "F", Prices = CreatePriceDataGenerator(11.39m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "GE", Prices = CreatePriceDataGenerator(19.54m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "GOOG", Prices = CreatePriceDataGenerator(101.4m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "IBM", Prices = CreatePriceDataGenerator(99.1m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "MMI", Prices = CreatePriceDataGenerator(38.14m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "NFLX", Prices = CreatePriceDataGenerator(87.88m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "T", Prices = CreatePriceDataGenerator(31.72m, rand).DataSets[0] });
			stockDataSet.Add(new StockData() { Symbol = "XOM", Prices = CreatePriceDataGenerator(86.31m, rand).DataSets[0] });
			foreach (StockData data in stockDataSet) {
				data.StartPrice = data.Prices.First().Amount;
				data.LowPriceData = data.Prices.OrderBy(d => d.Amount).First();
				data.HighPriceData = data.Prices.OrderByDescending(d => d.Amount).First();
				data.EndPrice = data.Prices.Last().Amount;
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a price data generator.
		/// </summary>
		/// <param name="startPrice">The start price.</param>
		/// <param name="rand">The random number generator.</param>
		/// <returns>The data generator that was created.</returns>
		private static SalesDataGenerator CreatePriceDataGenerator(decimal startPrice, Random rand) {
			return new SalesDataGenerator() { 
				Options = new SalesDataOptions() { 
					Count = 30, 
					StartAmount = startPrice, 
					StepRange = startPrice / 5 * Convert.ToDecimal(rand.NextDouble()), 
					TrendPercentage = 0.4 + 0.2 * rand.NextDouble() 
				} 
			};
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the high price.
		/// </summary>
		/// <value>The high price.</value>
		public double HighPrice {
			get {
				return Convert.ToDouble(stockDataSet.Max(d => d.HighPriceData.Amount));
			}
		}
		
		/// <summary>
		/// Gets the collection of stock data.
		/// </summary>
		/// <value>The collection of stock data.</value>
		public IList<StockData> StockDataSet {
			get {
				return stockDataSet;
			}
		}

	}
}