using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Utility;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions;

/// <summary>
/// Implements a custom parser that wraps the default parser and returns additional parse data results.
/// </summary>
/// <param name="parser">The original parser.</param>
public class CustomParser(IParser parser) : IParser {

	private readonly IParser _wrappedParser = parser ?? throw new ArgumentNullException(nameof(parser));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IKeyedObject.Key"/>
	public string Key
		=> "Custom";

	/// <inheritdoc cref="IParser.Parse"/>
	public IParseData? Parse(IParseRequest request) {
		var wrappedParseData = _wrappedParser.Parse(request) as IDotNetParseData;
		if (wrappedParseData?.Snapshot is not null) {
			var parseData = new CustomDotNetParseData(wrappedParseData) {
				// NOTE: Normally you would place code here that inspects the parsed snapshot text or its AST and determines unused regions...
				//   However this sample is showing off the visual feature itself so the text ranges use hard-coded offset values below

				UnusedRanges = new NormalizedTextSnapshotRangeCollection([
					new TextSnapshotRange(wrappedParseData.Snapshot, 14, 47),
					new TextSnapshotRange(wrappedParseData.Snapshot, 454, 473),
					new TextSnapshotRange(wrappedParseData.Snapshot, 476, 502)
				])
			};

			return parseData;
		}

		return null;
	}

}
