using System;
using System.Collections.ObjectModel;
using System.Globalization;
using ActiproSoftware.ProductSamples.Charts.Common;
#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.BarChartSlotting {

	/// <summary>
	/// A view model for the Bar Chart Slotting QuickStart.
	/// </summary>
	public class BarChartSlottingViewModel : ObservableObjectBase {

		private readonly Random random = new Random();
		private readonly ObservableCollection<MonthlySalesData> salesDataDetroit = new ObservableCollection<MonthlySalesData>();
		private readonly ObservableCollection<MonthlySalesData> salesDataLosAngeles = new ObservableCollection<MonthlySalesData>();
		private readonly ObservableCollection<MonthlySalesData> salesDataForQuarters = new ObservableCollection<MonthlySalesData>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BarChartSlottingViewModel" /> class.
		/// </summary>
		public BarChartSlottingViewModel() {
			InitializeSalesData();
			LabelFunc = GetAxisLabel;
		}

		private static string GetAxisLabel(DateTime slotStart, DateTime slotEnd) {
			if(slotStart.Year == slotEnd.Year && slotStart.Month == slotEnd.Month)
				return slotStart.ToString("MMM", CultureInfo.CurrentCulture);

			return slotStart.ToString("MMM", CultureInfo.CurrentCulture) + " - " + slotEnd.ToString("MMM", CultureInfo.CurrentCulture);
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a random sales value.
		/// </summary>
		/// <returns>A random sales value.</returns>
		private double GetRandomSalesValue() {
			return random.Next(1, 5);
		}

		/// <summary>
		/// Initializes the sales data.
		/// </summary>
		private void InitializeSalesData() {
			DateTime now = DateTime.Now;
			for (int month = 1; month <= 12; month++) {
				var monthDate = new DateTime(now.Year, month, 1);
				SalesDataDetroit.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
				SalesDataLosAngeles.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));

				var quarterMonthDate = new DateTime(now.Year, month, random.Next(1, 27));
				SalesDataForQuarters.Add(new MonthlySalesData(GetRandomSalesValue(), quarterMonthDate));
			}
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public Func<DateTime, DateTime, string> LabelFunc { get; private set; } 

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
		/// Gets the sales data for quarters.
		/// </summary>
		/// <value>The sales data for quarters.</value>
		public ObservableCollection<MonthlySalesData> SalesDataForQuarters {
			get { return salesDataForQuarters; }
		}

		#endregion PUBLIC PROCEDURES
	}
}