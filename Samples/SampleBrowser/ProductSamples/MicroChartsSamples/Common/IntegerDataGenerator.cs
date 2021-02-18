using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common {

	/// <summary>
	/// Dynamically generates random data to be used with various samples.
	/// Normally, data would come from sources such as database instead.
	/// </summary>
	public class IntegerDataGenerator : DataGeneratorBase<IntegerDataOptions, IntegerData> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Generates a single data set based on the current options.
		/// </summary>
		/// <returns>The collection of generated data.</returns>
		protected override ICollection<IntegerData> Generate() {
			// Create the results
			List<IntegerData> results = new List<IntegerData>();

			if (this.Options != null) {
				// Initialize the date
				DateTime date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-this.Options.Count);

				// Initialize the results with the first data item
				results.Add(new IntegerData(date, this.Options.StartValue));
				date.AddMonths(1);

				double delta = this.Options.TrendPercentage * this.Options.StepRange;
				for (int index = 1; index < this.Options.Count; index++) {
					double step = this.Random.NextDouble() * this.Options.StepRange;
					int count = (int)Math.Round(results[index - 1].Value + step - delta);

					if (!this.AllowNegativeNumbers)
						count = Math.Max(0, count);

					results.Add(new IntegerData(date, count));
					date = date.AddMonths(1);
				}
			}

			return results;
		}

	}

}
