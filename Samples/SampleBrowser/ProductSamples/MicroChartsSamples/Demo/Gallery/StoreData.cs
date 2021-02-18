using System;
using System.Collections.Generic;
using ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.Gallery {

	/// <summary>
	/// Stores data about a store.
	/// </summary>
	public class StoreData {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the net profit.
		/// </summary>
		/// <value>The net profit.</value>
		public double NetProfit { get; set; }

		/// <summary>
		/// Gets or sets the collection of sales data.
		/// </summary>
		/// <value>The collection of sales data.</value>
		public ICollection<double> Sales { get; set; }
		
		/// <summary>
		/// Gets or sets the target sales amount.
		/// </summary>
		/// <value>The target sales amount.</value>
		public double TargetSales { get; set; }

	}

}
