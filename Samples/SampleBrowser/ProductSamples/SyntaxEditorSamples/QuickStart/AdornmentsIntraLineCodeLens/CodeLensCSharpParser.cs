using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens;

/// <summary>
/// Represents a parser for the <c>C#</c> language with custom parse data results.
/// </summary>
public class CodeLensCSharpParser : CSharpParser {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IParseData? CreateParseData(IParseRequest request, IParserState state) {
		var parseData = base.CreateParseData(request, state) as IDotNetParseData;
		return (parseData is not null)
			? new CodeLensParseData(parseData)
			: null;
	}

}
