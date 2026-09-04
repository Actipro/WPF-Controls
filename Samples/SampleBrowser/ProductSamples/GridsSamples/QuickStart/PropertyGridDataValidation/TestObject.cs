namespace ActiproSoftware.ProductSamples.GridsSamples.QuickStart.PropertyGridDataValidation;

/// <summary>
/// Represents a test object for demonstration purposes.
/// </summary>
public class TestObject : ObservableObjectBase, IDataErrorInfo {

	private int _businessLogic1;
	private int _businessLogic2;
	private int _businessLogic3;
	private int _errorReporting1;
	private int _errorReporting2;
	private int _errorReporting3;

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	#region IDataErrorInfo

	string IDataErrorInfo.Error
		=> null!;

	string IDataErrorInfo.this[string columnName] {
		get {
			if ((columnName == "BusinessLogic3") && (_businessLogic3 < 0))
				return "Value is not positive";
			return null!;
		}
	}

	#endregion

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The first business logic integer.
	/// </summary>
	[Category("Custom Business Logic")]
	[DefaultValue((int)0)]
	[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and a custom ValidationRule to prevent values <= 0.")]
	public int BusinessLogic1 {
		get => _businessLogic1;
		set => SetProperty(ref _businessLogic1, value);
	}

	/// <summary>
	/// The second business logic integer.
	/// </summary>
	[Category("Custom Business Logic")]
	[DefaultValue((int)0)]
	[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and includes data validation in the property setter to prevent values <= 0.")]
	public int BusinessLogic2 {
		get => _businessLogic2;
		set {
			if (value < 0)
				throw new ArgumentOutOfRangeException(nameof(value), "Value is not positive");

			SetProperty(ref _businessLogic2, value);
		}
	}

	/// <summary>
	/// The third business logic integer.
	/// </summary>
	[Category("Custom Business Logic")]
	[DefaultValue((int)0)]
	[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and includes data validation using IDataErrorInfo to prevent values <= 0.")]
	public int BusinessLogic3 {
		get => _businessLogic3;
		set => SetProperty(ref _businessLogic3, value);
	}

	/// <summary>
	/// The first error reporting integer.
	/// </summary>
	[Category("Custom Error Reporting")]
	[DefaultValue((int)0)]
	[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline.")]
	public int ErrorReporting1 {
		get => _errorReporting1;
		set => SetProperty(ref _errorReporting1, value);
	}

	/// <summary>
	/// The second error reporting integer.
	/// </summary>
	[Category("Custom Error Reporting")]
	[DefaultValue((int)0)]
	[Description("This integer property uses a custom Validation.ErrorTemplate, which is a pulsing red overlay.")]
	public int ErrorReporting2 {
		get => _errorReporting2;
		set => SetProperty(ref _errorReporting2, value);
	}

	/// <summary>
	/// The third error reporting integer.
	/// </summary>
	[Category("Custom Error Reporting")]
	[DefaultValue((int)0)]
	[Description("This integer property uses the default Validation.ErrorTemplate, which is a red outline, and a custom dialog.")]
	public int ErrorReporting3 {
		get => _errorReporting3;
		set => SetProperty(ref _errorReporting3, value);
	}

}
