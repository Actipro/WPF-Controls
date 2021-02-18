using System.Collections.ObjectModel;

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial {

	/// <summary>
	/// A sample view model for the Financial demo.
	/// </summary>
	public class FinancialSampleViewModel {

		private readonly ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
		private readonly ObservableCollection<StockMarket> stockMarkets = new ObservableCollection<StockMarket>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the selected stock.
		/// </summary>
		/// <value>The selected stock.</value>
		public Stock SelectedStock { get; set; }

		/// <summary>
		/// Gets or sets the selected stock deferred.
		/// </summary>
		/// <value>The selected stock deferred.</value>
		public Stock SelectedStockDeferred { get; set; }

		/// <summary>
		/// Gets the stocks.
		/// </summary>
		/// <value>The stocks.</value>
		public ObservableCollection<Stock> Stocks {
			get { return stocks; }
		}

		/// <summary>
		/// Gets the stock markets.
		/// </summary>
		/// <value>The stock markets.</value>
		public ObservableCollection<StockMarket> StockMarkets {
			get { return stockMarkets; }
		}

		/// <summary>
		/// Gets or sets the Y axis minimum.
		/// </summary>
		/// <value>The Y axis minimum.</value>
		public decimal? YAxisMinimum { get; set; }

		/// <summary>
		/// Gets or sets the Y axis maximum.
		/// </summary>
		/// <value>The Y axis maximum.</value>
		public decimal? YAxisMaximum { get; set; }

		#endregion PUBLIC PROCEDURES
	}
}
