using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Parsing.LLParser.Implementation;
using Step3c = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted03c;  // For SimpleLexer

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04a;

/// <summary>
/// Represents a parser for the <c>Simple</c> language.
/// </summary>
/// <param name="grammar">The <see cref="Grammar"/> to use, or <c>null</c> to use a default grammar.</param>
public class SimpleParser(SimpleGrammar? grammar = null) : LLParserBase(grammar ?? new SimpleGrammar()) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override ITokenReader CreateTokenReader(ITextBufferReader reader)
		=> new SimpleTokenReader(reader, new Step3c.SimpleLexer(caseSensitive: true));

}
