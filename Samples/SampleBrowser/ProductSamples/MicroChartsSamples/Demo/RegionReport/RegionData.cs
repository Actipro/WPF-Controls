using System;
using System.Collections.Generic;
using ActiproSoftware.SampleBrowser.SampleData;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.RegionReport {

	/// <summary>
	/// Stores data about a region.
	/// </summary>
	public class RegionData {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the average sales.
		/// </summary>
		/// <value>The average sales.</value>
		public double AverageSales { get; set; }

		/// <summary>
		/// Gets or sets the average units sold.
		/// </summary>
		/// <value>The average units sold.</value>
		public int AverageUnitsSold { get; set; }

		/// <summary>
		/// Gets or sets max sales.
		/// </summary>
		/// <value>The max sales.</value>
		public double MaxSales { get; set; }

		/// <summary>
		/// Gets or sets the min sales.
		/// </summary>
		/// <value>The min sales.</value>
		public double MinSales { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the collection of sales data.
		/// </summary>
		/// <value>The collection of sales data.</value>
		public ICollection<TimeAggregatedData> Sales { get; set; }

		/// <summary>
		/// Gets or sets the collection of units sold data.
		/// </summary>
		/// <value>The collection of units sold data.</value>
		public ICollection<TimeAggregatedData> UnitsSold { get; set; }

	}

}
