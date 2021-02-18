using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraLineCodeLens {
    
	/// <summary>
	/// Represents a parser for the <c>C#</c> language with custom parse data results.
	/// </summary>
	public class CodeLensCSharpParser : CSharpParser {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates an <see cref="IParseData"/> for the specified <see cref="IParserState"/>.
		/// </summary>
		/// <param name="request">The <see cref="IParseRequest"/> that contains data about the requested parsing operation.</param>
		/// <param name="state">The <see cref="IParserState"/> to examine.</param>
		/// <returns>The <see cref="IParseData"/> that was created.</returns>
		protected override IParseData CreateParseData(IParseRequest request, IParserState state) {
			var parseData = base.CreateParseData(request, state) as IDotNetParseData;
			if (parseData != null)
				return new CodeLensParseData(parseData);
			else
				return null;
		}
		
	}
	
}
