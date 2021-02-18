using System;
using System.Collections.ObjectModel;
using ActiproSoftware.ProductSamples.Charts.Common;
#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.SeriesVisibility {

	/// <summary>
	/// The view model for the Series Visibility QuickStart.
	/// </summary>
	public class SeriesVisibilityViewModel : ObservableObjectBase {

		private readonly Random random = new Random();
		private readonly ObservableCollection<MonthlySalesData> salesDataDetroit = new ObservableCollection<MonthlySalesData>();
		private readonly ObservableCollection<MonthlySalesData> salesDataLosAngeles = new ObservableCollection<MonthlySalesData>();
		private readonly ObservableCollection<MonthlySalesData> salesDataNewYork = new ObservableCollection<MonthlySalesData>();
		private readonly ObservableCollection<MonthlySalesData> salesDataSeattle = new ObservableCollection<MonthlySalesData>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="SeriesVisibilityViewModel" /> class.
		/// </summary>
		public SeriesVisibilityViewModel() {
			InitializeSalesData();	
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the sales data.
		/// </summary>
		private void InitializeSalesData() {
			DateTime now = DateTime.Now;
			for(int month = 1; month <= 12; month++) {
				var monthDate = new DateTime(now.Year, month, 1);
				SalesDataDetroit.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
				SalesDataNewYork.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
				SalesDataSeattle.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
				SalesDataLosAngeles.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
			}		
		}

		/// <summary>
		/// Gets a random sales value.
		/// </summary>
		/// <returns>A random sales value.</returns>
		private double GetRandomSalesValue() {
			return random.Next(10000, 400000);
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the sales data for Detroit.
		/// </summary>
		/// <value>The sales data for Detroit.</value>
		public ObservableCollection<MonthlySalesData> SalesDataDetroit {
			get { return salesDataDetroit; }
		}

		/// <summary>
		/// Gets the sales data for Los Angeles.
		/// </summary>
		/// <value>The sales data for Los Angeles.</value>
		public ObservableCollection<MonthlySalesData> SalesDataLosAngeles {
			get { return salesDataLosAngeles; }
		}

		/// <summary>
		/// Gets the sales data for New York.
		/// </summary>
		/// <value>The sales data for New York.</value>
		public ObservableCollection<MonthlySalesData> SalesDataNewYork {
			get { return salesDataNewYork; }
		}

		/// <summary>
		/// Gets the sales data for Seattle.
		/// </summary>
		/// <value>The sales data for Seattle.</value>
		public ObservableCollection<MonthlySalesData> SalesDataSeattle {
			get { return salesDataSeattle; }
		}

		#endregion PUBLIC PROCEDURES
	}
}
