using System;
using System.Collections.Generic;
using System.Text;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted15 {

	/// <summary>
	/// Represents a navigable symbol provider for the <c>Simple</c> language.
	/// </summary>
	public class SimpleNavigableSymbolProvider : INavigableSymbolProvider {
	
		private IComparer<INavigableSymbol>		navigationSymbolComparer		= new NavigableSymbolContentProviderComparer();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Looks into an AST for function declaration symbols to add.
		/// </summary>
		/// <param name="symbols">The list to update.</param>
		/// <param name="snapshot">The <see cref="ITextSnapshot"/> to examine.</param>
		/// <param name="compilationUnit">The <see cref="IAstNode"/> to examine.</param>
		private void AddFunctionDeclarationSymbolsFromAst(IList<INavigableSymbol> symbols, ITextSnapshot snapshot, CompilationUnit compilationUnit) {
			if ((compilationUnit != null) && (compilationUnit.HasMembers)) {
				// Loop through the AST nodes
				foreach (FunctionDeclaration functionAstNode in compilationUnit.Members) {
					// If the function declaration has a name and body...
					if ((!string.IsNullOrEmpty(functionAstNode.Name)) && (functionAstNode.Body != null) && 
						(functionAstNode.StartOffset.HasValue) && (functionAstNode.EndOffset.HasValue)) {

						// Build the content
						var htmlSnippet = new StringBuilder("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> ");
						htmlSnippet.Append(HtmlContentProvider.Escape(functionAstNode.Name));

						// Create the symbol
						var symbol = new NavigableSymbol() {
							ContentProvider = new HtmlContentProvider(htmlSnippet.ToString()),
							SnapshotRange = new TextSnapshotRange(snapshot, functionAstNode.StartOffset.Value, functionAstNode.EndOffset.Value)
						};

						symbols.Add(symbol);
					}
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns a set of <see cref="INavigableSymbol"/> objects that are accessible within the optional parent <see cref="INavigableSymbol"/>.
		/// </summary>
		/// <param name="context">An <see cref="INavigableRequestContext"/> that provides the context of the request.</param>
		/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> that contains the <see cref="ITextSnapshot"/> to examine, along with a contextual offset.</param>
		/// <param name="parentSymbol">The optional parent <see cref="INavigableSymbol"/> to examine, or a null value if the root symbols are requested.</param>
		/// <returns>An <see cref="INavigableSymbolSet"/> containing the <see cref="INavigableSymbol"/> objects that were located.</returns>
		public INavigableSymbolSet GetSymbols(INavigableRequestContext context, TextSnapshotOffset snapshotOffset, INavigableSymbol parentSymbol) {
			if (context == null)
				throw new ArgumentNullException("context");
			if (snapshotOffset.IsDeleted)
				throw new ArgumentOutOfRangeException("snapshotOffset");

			if (context == NavigableRequestContexts.NavigableSymbolSelector) {
				var document = snapshotOffset.Snapshot.Document as ICodeDocument;
				if (document != null) {
					// If there is AST data...
					var parseData = document.ParseData as ILLParseData;
					if ((parseData != null) && (parseData.Ast != null)) {
						// Recurse into the AST
						var symbols = new List<INavigableSymbol>();

						// NOTE: Normally here you would return either root symbols or member symbols depending on if a parentSymbol is passed...
						//   In this Simple language though, we only ever need root symbols
						if (parentSymbol == null)
							this.AddFunctionDeclarationSymbolsFromAst(symbols, parseData.Snapshot ?? snapshotOffset.Snapshot, parseData.Ast as CompilationUnit);

						// Sort
						symbols.Sort(navigationSymbolComparer);

						return new NavigableSymbolSet(symbols);
					}
				}
			}

			// No results
			return new NavigableSymbolSet(null);
		}

	}

}
