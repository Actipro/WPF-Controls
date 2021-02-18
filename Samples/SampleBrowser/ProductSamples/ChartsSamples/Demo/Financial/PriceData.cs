using System;

namespace ActiproSoftware.ProductSamples.ChartsSamples.Demo.Financial {

	/// <summary>
	/// Represents a price on a specific date.
	/// </summary>
	public class PriceData {

		private DateTime date;
		private decimal price;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the <c>PriceData</c> class.
		/// </summary>
		/// <param name="price">The price.</param>
		/// <param name="date">The date.</param>
		public PriceData(decimal price, DateTime date) {
			this.price = price;
			this.date = date;
		}

		#endregion OBJECT

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		#region PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the date.
		/// </summary>
		/// <value>The date.</value>
		public DateTime Date {
			get { return date; }
		}

		/// <summary>
		/// Gets the price.
		/// </summary>
		/// <value>The price.</value>
		public decimal Price {
			get { return price; }
		}

		#endregion PUBLIC PROCEDURES

	}
}
