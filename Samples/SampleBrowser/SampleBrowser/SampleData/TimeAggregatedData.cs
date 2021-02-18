using System;
using System.Collections.Generic;

namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Stores time-aggregated data, and is used by various samples for this product.
	/// Any similar custom data objects could be used to generate chart data.
	/// </summary>
	public class TimeAggregatedData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>TimeAggregatedData</c> class.
		/// </summary>
		/// <param name="index">The data item index.</param>
		/// <param name="timePeriod">The time period.</param>
		/// <param name="date">The time period start date for which the amount is specified.</param>
		/// <param name="amount">The sales amount.</param>
		public TimeAggregatedData(int index, TimePeriod timePeriod, DateTime date, double amount) {
			this.Index = index;
			this.TimePeriod = timePeriod;
			this.Date = date;
			this.Amount = amount;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the amount.
		/// </summary>
		/// <value>The amount.</value>
		public double Amount { get; private set; }
		
		/// <summary>
		/// Gets the time period start date for which the amount is specified.
		/// </summary>
		/// <value>The time period start date for which the amount is specified.</value>
		public DateTime Date { get; private set; }

		/// <summary>
		/// Gets the text label for the <see cref="Date"/>.
		/// </summary>
		/// <value>The text label for the <see cref="Date"/>.</value>
		public string DateLabel {
			get {
				switch (this.TimePeriod) {
					case TimePeriod.Month:
						return this.Date.ToString("MMM");
					case TimePeriod.Week:
						return this.Date.ToString("MMM dd");
					default:  // Year
						return this.Date.ToString("yyyy");
				}
			}
		}

		/// <summary>
		/// Gets the data item index.
		/// </summary>
		/// <value>The data item index.</value>
		public int Index { get; private set; }

		/// <summary>
		/// Gets or sets the partitions of the data.
		/// </summary>
		/// <value>The partitions of the data.</value>
		public IList<NumericData> Partitions { get; set; }
		
		/// <summary>
		/// Gets or sets the time period.
		/// </summary>
		/// <value>The time period.</value>
		public TimePeriod TimePeriod { get; private set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title { get; set; }

	}

}
