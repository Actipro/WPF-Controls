﻿namespace ActiproSoftware.SampleBrowser.SampleData {

	/// <summary>
	/// Stores simple numeric data.
	/// </summary>
	public class NumericData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <c>NumericData</c> class.
		/// </summary>
		/// <param name="amount">The sales amount.</param>
		public NumericData(double amount) {
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

	}

}
