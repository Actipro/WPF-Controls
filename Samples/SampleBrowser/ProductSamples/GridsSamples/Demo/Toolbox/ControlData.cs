namespace ActiproSoftware.ProductSamples.GridsSamples.Demo.Toolbox;

/// <summary>
/// Provides control data information.
/// </summary>
/// <param name="fullName">The full name of the control (e.g., <c>"CompanyName.Namespace.ControlName"</c>).</param>
/// <param name="category">The category for the control.</param>
public class ControlData(string fullName, string category) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The full name of the control (e.g., <c>"CompanyName.Namespace.ControlName"</c>).
	/// </summary>
	public string FullName { get; } = fullName ?? throw new ArgumentNullException(nameof(fullName));

	/// <summary>
	/// The category for the control.
	/// </summary>
	public string Category { get; } = category ?? throw new ArgumentNullException(nameof(category));

}
