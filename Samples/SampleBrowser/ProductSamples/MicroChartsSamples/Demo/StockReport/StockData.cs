using System;
using System.Collections.Generic;
using ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Demo.StockReport {

	/// <summary>
	/// Stores data about a stock.
	/// </summary>
	public class StockData {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets or sets the delta in price.
		/// </summary>
		/// <value>The delta in price.</value>
		public decimal DeltaPrice { 
			get {
				return this.EndPrice - this.StartPrice;
			}
		}
		
		/// <summary>
		/// Gets or sets the end price.
		/// </summary>
		/// <value>The end price.</value>
		public decimal EndPrice { get; set; }
		
		/// <summary>
		/// Gets or sets the high price data.
		/// </summary>
		/// <value>The high price data.</value>
		public SalesData HighPriceData { get; set; }

		/// <summary>
		/// Gets or sets the low price data.
		/// </summary>
		/// <value>The low price data.</value>
		public SalesData LowPriceData { get; set; }

		/// <summary>
		/// Gets or sets the start price.
		/// </summary>
		/// <value>The start price.</value>
		public decimal StartPrice { get; set; }

		/// <summary>
		/// Gets or sets the ticker symbol.
		/// </summary>
		/// <value>The ticker symbol.</value>
		public string Symbol { get; set; }

		/// <summary>
		/// Gets or sets the collection of prices.
		/// </summary>
		/// <value>The collection of prices.</value>
		public ICollection<SalesData> Prices { get; set; }

	}

}
