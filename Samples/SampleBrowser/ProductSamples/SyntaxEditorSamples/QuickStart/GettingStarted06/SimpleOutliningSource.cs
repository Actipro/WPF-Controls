using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;
using Step4d = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted06;

/// <summary>
/// Represents a <c>Simple</c> language range-based outlining source.
/// </summary>
internal class SimpleOutliningSource : RangeOutliningSourceBase {

	private static OutliningNodeDefinition? _functionDefinition;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="snapshot">The <see cref="ITextSnapshot"/> to use for this outlining source.</param>
	/// <param name="parseData">The <see cref="ILLParseData"/> containing AST data.</param>
	public SimpleOutliningSource(ITextSnapshot snapshot, ILLParseData parseData) : base(snapshot) {
		#if NET
		ArgumentNullException.ThrowIfNull(parseData);
		#else
		if (parseData is null)
			throw new ArgumentNullException(nameof(parseData));
		#endif

		// Create a 'Function' outlining node definition if one hasn't yet been created
		_functionDefinition ??= new OutliningNodeDefinition("Function") { IsImplementation = true };

		var compilationUnit = parseData.Ast as Step4d.CompilationUnit;
		if (compilationUnit?.HasMembers == true) {
			// Loop through AST nodes
			foreach (var functionAstNode in compilationUnit.Members) {
				// If the function declaration has a body with a text range...
				if (functionAstNode.Body is { StartOffset: { } startOffset, EndOffset: { } endOffset }) {
					// Add an outlining node
					AddNode(new TextRange(startOffset, endOffset), _functionDefinition);
				}
			}
		}
	}

}
