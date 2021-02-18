using System;
using System.Collections.ObjectModel;
using System.Linq;
#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using System.Collections.Specialized;
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial {

	/// <summary>
	/// View model for financial demo.
	/// </summary>
	public class FinancialViewModel : ObservableObjectBase {

		private const decimal UpdateThreshold = 1.0m;

		private readonly ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
		private readonly ObservableCollection<StockMarket> stockMarkets = new ObservableCollection<StockMarket>();

		private Stock selectedStock;
		private decimal? yAxisMinimum;
		private decimal? yAxisMaximum;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>FinancialViewModel</c> class.
		/// </summary>
		public FinancialViewModel() {
			InitializeStocks();
			InitializeStockMarkets();
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the stock markets.
		/// </summary>
		private void InitializeStockMarkets() {
			var dow = new StockMarket("Dow Jones", 10000.0m, 15000.0m, 70.0m);
			var nas = new StockMarket("NASDAQ", 2000.0m, 3000.0m, 20.0m);
			var sp = new StockMarket("S&P 500", 1000.0m, 2000.0m, 10.0m);
			StockMarkets.Add(dow);
			StockMarkets.Add(nas);
			StockMarkets.Add(sp);

			foreach(var stock in StockMarkets)
				stock.StartUpdatingPriceData();
		}

		/// <summary>
		/// Initializes the stocks.
		/// </summary>
		private void InitializeStocks() {
			var msft = new Stock("Microsoft", "MSFT");
			var appl = new Stock("Apple", "AAPL");
			var tsla = new Stock("Tesla", "TSLA");
			var goog = new Stock("Google", "GOOG");
			Stocks.Add(msft);
			Stocks.Add(appl);
			Stocks.Add(tsla);
			Stocks.Add(goog);
			SelectedStock = Stocks[0];

			foreach(var stock in Stocks)
				stock.StartUpdatingPriceData();
		}

		/// <summary>
		/// Called when stock prices change.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
		private void OnStockPricesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			UpdateAxisMinAndMaxIfPastThreshold();
		}

		/// <summary>
		/// Updates the axis min and max.
		/// </summary>
		private void UpdateAxisMinAndMax() {
			decimal min = SelectedStock.StockPrices.Min(priceData => priceData.Price);
			decimal max = SelectedStock.StockPrices.Max(priceData => priceData.Price);
			YAxisMinimum = (decimal?)Math.Floor((double)(min - UpdateThreshold));
			YAxisMaximum = max + UpdateThreshold;
		}

		/// <summary>
		/// Updates the axis min and max if past threshold.
		/// </summary>
		private void UpdateAxisMinAndMaxIfPastThreshold() {
			decimal min = SelectedStock.StockPrices.Min(priceData => priceData.Price);
			decimal max = SelectedStock.StockPrices.Max(priceData => priceData.Price);

			if (!YAxisMinimum.HasValue || YAxisMinimum.Value >= min)
				YAxisMinimum = min - UpdateThreshold;

			if (!YAxisMaximum.HasValue || YAxisMaximum.Value <= max)
				YAxisMaximum = max + UpdateThreshold;
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize() {
			foreach (var stock in Stocks)
				stock.StartUpdatingPriceData();
			foreach (var stock in StockMarkets)
				stock.StartUpdatingPriceData();
		}

		/// <summary>
		/// Gets or sets the selected stock.
		/// </summary>
		/// <value>The selected stock.</value>
		public Stock SelectedStock {
			get { return selectedStock; }
			set {
				if (selectedStock != null)
					selectedStock.StockPrices.CollectionChanged -= OnStockPricesChanged;

				selectedStock = value;
				NotifyPropertyChanged("SelectedStock");
				UpdateAxisMinAndMax();
				UpdateAxisMinAndMaxIfPastThreshold();

				if (selectedStock != null)
					selectedStock.StockPrices.CollectionChanged += OnStockPricesChanged;
			}
		}

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
		/// Teardowns this instance.
		/// </summary>
		public void Teardown() {
			foreach (var stock in Stocks)
				stock.StopUpdatingPriceData();
			foreach (var stock in StockMarkets)
				stock.StopUpdatingPriceData();
		}

		/// <summary>
		/// Gets or sets the Y axis minimum.
		/// </summary>
		/// <value>The Y axis minimum.</value>
		public decimal? YAxisMinimum {
			get { return yAxisMinimum; }
			set {
				yAxisMinimum = value;
				NotifyPropertyChanged("YAxisMinimum");
			}
		}

		/// <summary>
		/// Gets or sets the Y axis maximum.
		/// </summary>
		/// <value>The Y axis maximum.</value>
		public decimal? YAxisMaximum {
			get { return yAxisMaximum; }
			set {
				yAxisMaximum = value;
				NotifyPropertyChanged("YAxisMaximum");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
