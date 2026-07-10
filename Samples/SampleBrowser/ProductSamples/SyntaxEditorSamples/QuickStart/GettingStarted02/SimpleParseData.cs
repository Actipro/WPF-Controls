using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted02;

/// <summary>
/// Represents the parsing results for a <see cref="SimpleParser"/>.
/// </summary>
public class SimpleParseData : IParseData {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The list of available functions.
	/// </summary>
	/// <value>The list of available functions.</value>
	public IList<TextSnapshotRange> Functions { get; } = [];

}
