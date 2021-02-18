using System;
using System.Windows.Controls;

namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDataValidation {
	
	/// <summary>
	/// Represents a validation rule that ensures an <see cref="Int32"/> value is positive.
	/// </summary>
	public class PositiveInt32ValidationRule : ValidationRule {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// When overridden in a derived class, performs validation checks on a value.
		/// </summary>
		/// <param name="value">The value from the binding target to check.</param>
		/// <param name="cultureInfo">The culture to use in this rule.</param>
		/// <returns>
		/// A <see cref="ValidationResult"/> object.
		/// </returns>
		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo) {
			try {
				int i = Convert.ToInt32(value);
				if (i >= 0)
					return new ValidationResult(true, null);
				else
					return new ValidationResult(false, "Value is not positive");
			}
			catch (Exception) {
				return new ValidationResult(false, string.Format("{0} is not a valid value for Int32.", value));
			}
		}

	}

}
