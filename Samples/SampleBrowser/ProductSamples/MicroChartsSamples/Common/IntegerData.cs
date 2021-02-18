using System;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Stores integer-related data, and is used by various samples for this product.
	/// Any similar custom data objects could be used to generate chart data.
	/// </summary>
	public class IntegerData {

		private DateTime date;
		private int value;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>IntegerData</c> class.
		/// </summary>
		/// <param name="date">The date for which the amount is specified.</param>
		/// <param name="value">The count.</param>
		public IntegerData(DateTime date, int value) {
			this.date = date;
			this.value = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the date for which the amount is specified.
		/// </summary>
		/// <value>The date for which the amount is specified.</value>
		public DateTime Date { 
			get {
				return date;
			}
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		public int Value { 
			get {
				return value;
			}
		}

	}

}
