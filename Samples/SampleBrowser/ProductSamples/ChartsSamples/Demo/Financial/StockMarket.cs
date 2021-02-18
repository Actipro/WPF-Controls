namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial {

	/// <summary>
	/// A stock marker.
	/// </summary>
	public class StockMarket : Stock {

		private decimal stockPriceMin;
		private decimal stockPriceMax;
		private decimal stockPriceDelta;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>StockMarket</c> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="priceMin">The minimum price.</param>
		/// <param name="priceMax">The maximum price.</param>
		/// <param name="priceDelta">The price delta.</param>
		public StockMarket(string name, decimal priceMin, decimal priceMax, decimal priceDelta)
			: base(name, null) {
			stockPriceMin = priceMin;
			stockPriceMax = priceMax;
			stockPriceDelta = priceDelta;
			InitializeSampleData();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the stock price delta.
		/// </summary>
		/// <value>The stock price delta.</value>
		protected override decimal StockPriceDelta {
			get {
				return stockPriceDelta;
			}
		}

		/// <summary>
		/// Gets the stock price max.
		/// </summary>
		/// <value>The stock price max.</value>
		protected override decimal StockPriceMax {
			get {
				return stockPriceMax;
			}
		}

		/// <summary>
		/// Gets the stock price min.
		/// </summary>
		/// <value>The stock price min.</value>
		protected override decimal StockPriceMin {
			get {
				return stockPriceMin;
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
