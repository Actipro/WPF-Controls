namespace ActiproSoftware.ProductSamples.EditorsSamples.Common;

/// <summary>
/// Represents a predefined format.
/// </summary>
/// <param name="name">The name.</param>
/// <param name="format">The format.</param>
public class PredefinedFormat(string name, string format) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The predefined format.
	/// </summary>
	public string Format { get; } = format;

	/// <summary>
	/// The name.
	/// </summary>
	public string Name { get; } = name;

	/// <inheritdoc/>
	public override string ToString()
		=> Format;

}
