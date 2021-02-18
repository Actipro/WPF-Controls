using ActiproSoftware.Windows.Controls.Gauge;

namespace ActiproSoftware.ProductSamples.GaugeSamples.QuickStart.LinearGaugeRollingScale {

	/// <summary>
	/// Represents major tick label that normalizes heading directions between <c>0</c> and <c>359</c>.
	/// </summary>
	public class HeadingLinearTickLabel : LinearTickLabelMajor {

		private HeadingConverter converter = new HeadingConverter();
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the text for the specified value.
		/// </summary>
		/// <param name="value">The value to examine.</param>
		/// <returns>The text for the specified value.</returns>
		protected override string GetValueText(double value) {
			return converter.Convert(value, typeof(string), null, null) as string;
		}

	}
}

