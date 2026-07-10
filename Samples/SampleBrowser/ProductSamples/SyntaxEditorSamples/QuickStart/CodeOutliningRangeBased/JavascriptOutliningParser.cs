using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningRangeBased;

/// <summary>
/// Represents a <c>Javascript</c> parser that performs code outlining.
/// </summary>
public class JavascriptOutliningParser() : ParserBase("OutliningParser") {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IParseData? Parse(IParseRequest request) {
		if (request.Snapshot is { } snapshot) {
			// Since the parser may be delayed a bit before this is called (due to threading),
			//   base the outlining on the most current snapshot for the document, which may be
			//   newer than the one passed in the parse request
			return new JavascriptOutliningSource(snapshot.Document.CurrentSnapshot);
		}

		return null;
	}

}
