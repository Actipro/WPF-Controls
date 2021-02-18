using ActiproSoftware.ProductSamples.Charts.Common;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActiproSoftware.ProductSamples.ChartsSamples.QuickStart.XY.Axis {

	/// <summary>
	/// A view model for the Axis Quick Start.
	/// </summary>
	public class AxisViewModel : ObservableObjectBase {

		private readonly Random random = new Random();
		private readonly ObservableCollection<MonthlySalesData> salesData = new ObservableCollection<MonthlySalesData>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="AxisViewModel" /> class.
		/// </summary>
		public AxisViewModel() {
			InitializeSalesData();
			AxisLabelFunc = GetAxisLabel;
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the axis label.
		/// </summary>
		/// <param name="slotStart">The slot start.</param>
		/// <param name="slotEnd">The slot end.</param>
		/// <returns>The axis label.</returns>
		private static string GetAxisLabel(DateTime slotStart, DateTime slotEnd) {
			int quarter;
			int year = slotStart.Year;
			for(quarter = 1; quarter <= 4; quarter++) {
				if(slotStart >= new DateTime(year, (quarter*3) - 2, 1) &&
					slotStart < new DateTime(year, quarter*3, 1))
					break;
			}

			return string.Format("Q{0}", quarter);
		}

		/// <summary>
		/// Gets a random sales value.
		/// </summary>
		/// <returns>A random sales value.</returns>
		private double GetRandomSalesValue() {
			return random.Next(10, 40);
		}

		/// <summary>
		/// Initializes the sales data.
		/// </summary>
		private void InitializeSalesData() {
			DateTime now = DateTime.Now;
			for(int month = 1; month <= 12; month++) {
				var monthDate = new DateTime(now.Year, month, 1);
				SalesData.Add(new MonthlySalesData(GetRandomSalesValue(), monthDate));
			}

			this.SalesData2 = new TimeAggregatedDataGenerator(new double[] { 10.5, 19.5, 14.3, 4.8, 8.4, -1.3, 7.7, 1.8, -1.8, -9.4, -9.7, -6.2, 2.0, 11.2, 18.6, 27.4, 18.7, 11.3, 9.2 });
			this.SalesData3= new TimeAggregatedDataGenerator(new double[] { -1800, -6200, 11300, 9200, 9500, 18500, 14300, 4800, 8400, -1300, 9700, 4000, 10200, 19000, 12000, 11000, 9000, 100, 1800 });
		}

		#endregion NON-PUBLIC PROCEDURES

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		/// <value>The sales data.</value>
		public ObservableCollection<MonthlySalesData> SalesData {
			get { return salesData; }
		}

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		/// <value>The sales data.</value>
		public IEnumerable<TimeAggregatedData> SalesData2 { get; set; }

		/// <summary>
		/// Gets the sales data.
		/// </summary>
		/// <value>The sales data.</value>
		public IEnumerable<TimeAggregatedData> SalesData3 { get; set; }

		/// <summary>
		/// Gets the axis label func.
		/// </summary>
		/// <value>The axis label func.</value>
		public Func<DateTime, DateTime, string> AxisLabelFunc { get; private set; }

		#endregion PUBLIC PROCEDURES
	}
}
