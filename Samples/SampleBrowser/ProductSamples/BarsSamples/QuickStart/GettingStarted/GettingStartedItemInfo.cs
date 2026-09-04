namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStart.GettingStarted;

/// <summary>
/// Defines the information for a step in a Getting Started series.
/// </summary>
/// <param name="stepNumber">The number of the step.</param>
/// <param name="path">The path fo the class which defines the sample.</param>
/// <param name="summary">The summary of the step.</param>
public class GettingStartedItemInfo(int stepNumber, string path, string summary) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The path of the class which defines the sample.
	/// </summary>
	public string Path { get; } = path;

	/// <summary>
	/// The number of the step.
	/// </summary>
	public int StepNumber { get; } = stepNumber;

	/// <summary>
	/// The summary of the step.
	/// </summary>
	public string Summary { get; } = summary;

}
