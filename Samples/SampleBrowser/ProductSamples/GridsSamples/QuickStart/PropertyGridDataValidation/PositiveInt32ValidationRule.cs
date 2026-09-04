namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDataValidation;

/// <summary>
/// Represents a validation rule that ensures an <see cref="Int32"/> value is positive.
/// </summary>
public class PositiveInt32ValidationRule : ValidationRule {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
		try {
			var i = Convert.ToInt32(value);
			return (i >= 0)
				? new ValidationResult(isValid: true, errorContent: null)
				: new ValidationResult(isValid: false, errorContent: "Value is not positive");
		}
		catch (Exception) {
			return new ValidationResult(isValid: false, errorContent: string.Format("{0} is not a valid value for Int32.", value));
		}
	}

}
