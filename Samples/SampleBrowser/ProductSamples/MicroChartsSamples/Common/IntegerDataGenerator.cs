using ActiproSoftware.Extensions;

namespace ActiproSoftware.ProductSamples.MicroChartsSamples.Common;

/// <summary>
/// Dynamically generates random data to be used with various samples.
/// Normally, data would come from sources such as database instead.
/// </summary>
public class IntegerDataGenerator : DataGeneratorBase<IntegerDataOptions, IntegerData> {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override ICollection<IntegerData> Generate() {
		// Create the results
		var results = new List<IntegerData>();

		if (Options is { } options) {
			// Initialize the date
			var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-options.Count);

			// Initialize the results with the first data item
			results.Add(new IntegerData(date, options.StartValue));
			date.AddMonths(1);

			var delta = options.TrendPercentage * options.StepRange;
			for (var index = 1; index < options.Count; index++) {
				var step = Random.NextDouble() * options.StepRange;
				var count = (int)Math.Round(results[index - 1].Value + step - delta);

				if (!AllowNegativeNumbers)
					count = count.ClampToNonnegative();

				results.Add(new IntegerData(date, count));
				date = date.AddMonths(1);
			}
		}

		return results;
	}

}
