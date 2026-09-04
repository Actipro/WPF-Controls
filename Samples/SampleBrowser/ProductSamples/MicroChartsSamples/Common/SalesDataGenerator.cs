namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Dynamically generates random data to be used with various samples.
/// Normally, data would come from sources such as database instead.
/// </summary>
public class SalesDataGenerator : DataGeneratorBase<SalesDataOptions, SalesData> {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override ICollection<SalesData> Generate() {
		// Create the results
		var results = new List<SalesData>();

		if (Options is { } options) {
			// Initialize the date
			var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-options.Count);

			// Initialize the results with the first data item
			results.Add(new SalesData(date, options.StartAmount));
			date.AddMonths(1);

			var delta = Convert.ToDecimal(options.TrendPercentage) * options.StepRange;
			for (var index = 1; index < options.Count; index++) {
				var step = Convert.ToDecimal(Random.NextDouble()) * options.StepRange;
				var amount = results[index - 1].Amount + step - delta;

				if (!AllowNegativeNumbers)
					amount = Math.Max(0, amount);

				results.Add(new SalesData(date, amount));
				date = date.AddMonths(1);
			}
		}

		return results;
	}

}
