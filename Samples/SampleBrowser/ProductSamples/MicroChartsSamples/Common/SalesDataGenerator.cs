using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Dynamically generates random data to be used with various samples.
	/// Normally, data would come from sources such as database instead.
	/// </summary>
	public class SalesDataGenerator : DataGeneratorBase<SalesDataOptions, SalesData> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Generates a single data set based on the current options.
		/// </summary>
		/// <returns>The collection of generated data.</returns>
		protected override ICollection<SalesData> Generate() {
			// Create the results
			List<SalesData> results = new List<SalesData>();

			if (this.Options != null) {
				// Initialize the date
				DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-this.Options.Count);

				// Initialize the results with the first data item
				results.Add(new SalesData(date, this.Options.StartAmount));
				date.AddMonths(1);

				decimal delta = Convert.ToDecimal(this.Options.TrendPercentage) * this.Options.StepRange;
				for (int index = 1; index < this.Options.Count; index++) {
					decimal step = Convert.ToDecimal(this.Random.NextDouble()) * this.Options.StepRange;
					decimal amount = results[index - 1].Amount + step - delta;

					if (!this.AllowNegativeNumbers)
						amount = Math.Max(0, amount);

					results.Add(new SalesData(date, amount));
					date = date.AddMonths(1);
				}
			}

			return results;
		}

	}

}
