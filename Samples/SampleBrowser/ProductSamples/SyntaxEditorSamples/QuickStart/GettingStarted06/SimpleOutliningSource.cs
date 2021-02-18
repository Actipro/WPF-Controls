using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;
using Step4d = ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted06 {

	/// <summary>
	/// Represents a <c>Simple</c> language range-based outlining source.
	/// </summary>
	internal class SimpleOutliningSource : RangeOutliningSourceBase {

		private static OutliningNodeDefinition functionDefinition;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleOutliningSource</c> class.
		/// </summary>
		/// <param name="snapshot">The <see cref="ITextSnapshot"/> to use for this outlining source.</param>
		/// <param name="parseData">The <see cref="ILLParseData"/> containing AST data.</param>
		public SimpleOutliningSource(ITextSnapshot snapshot, ILLParseData parseData) : base(snapshot) {
			if (parseData == null)
				throw new ArgumentNullException("parseData");

			// Create a 'Function' outlining node definition if one hasn't yet been created
			if (functionDefinition == null) {
				functionDefinition = new OutliningNodeDefinition("Function");
				functionDefinition.IsImplementation = true;
			}

			Step4d.CompilationUnit compilationUnit = parseData.Ast as Step4d.CompilationUnit;
			if ((compilationUnit != null) && (compilationUnit.HasMembers)) {
				// Loop through AST nodes
				foreach (Step4d.FunctionDeclaration functionAstNode in compilationUnit.Members) {
					// If the function declaration has a body with a text range...
					if ((functionAstNode.Body != null) && (functionAstNode.Body.StartOffset.HasValue) && (functionAstNode.Body.EndOffset.HasValue)) {
						// Add an outlining node
						this.AddNode(new TextRange(functionAstNode.Body.StartOffset.Value, functionAstNode.Body.EndOffset.Value), functionDefinition);
					}
				}
			}
		}
	
	}

}
