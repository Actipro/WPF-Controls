using System;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Stores sales-related data, and is used by various samples for this product.
	/// Any similar custom data objects could be used to generate chart data.
	/// </summary>
	public class SalesData {

		private decimal amount;
		private DateTime date;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>SalesData</c> class.
		/// </summary>
		/// <param name="date">The date for which the amount is specified.</param>
		/// <param name="amount">The sales amount.</param>
		public SalesData(DateTime date, decimal amount) {
			this.date = date;
			this.amount = amount;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the sales amount.
		/// </summary>
		/// <value>The sales amount.</value>
		public decimal Amount { 
			get {
				return amount;
			}
		}

		/// <summary>
		/// Gets the date for which the amount is specified.
		/// </summary>
		/// <value>The date for which the amount is specified.</value>
		public DateTime Date { 
			get {
				return date;
			}
		}

	}

}
