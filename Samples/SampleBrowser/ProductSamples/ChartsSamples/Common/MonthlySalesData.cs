using System;
#if WINRT
using ActiproSoftware.UI.Xaml;
#else
using ActiproSoftware.Windows;
#endif

namespace ActiproSoftware.ProductSamples.Charts.Common {

	/// <summary>
	/// Represents sales data for a given month.
	/// </summary>
	public class MonthlySalesData : ObservableObjectBase {

		private DateTime month;
		private double sales;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="MonthlySalesData" /> class.
		/// </summary>
		/// <param name="sales">The sales.</param>
		/// <param name="month">The month.</param>
		public MonthlySalesData(double sales, DateTime month) {
			Sales = sales;
			Month = month;
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the month.
		/// </summary>
		/// <value>The month.</value>
		public DateTime Month {
			get { return month; }
			private set {
				month = value;
				NotifyPropertyChanged("Month");
			}
		}

		/// <summary>
		/// Gets the sales.
		/// </summary>
		/// <value>The sales.</value>
		public double Sales {
			get { return sales; }
			private set {
				sales = value;
				NotifyPropertyChanged("Sales");
			}
		}

		#endregion PUBLIC PROCEDURES
	}
}
