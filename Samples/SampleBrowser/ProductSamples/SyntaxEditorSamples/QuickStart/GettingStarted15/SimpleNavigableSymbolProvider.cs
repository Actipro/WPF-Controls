using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted15;

/// <summary>
/// Represents a navigable symbol provider for the <c>Simple</c> language.
/// </summary>
public class SimpleNavigableSymbolProvider : INavigableSymbolProvider {

	private readonly IComparer<INavigableSymbol> _navigationSymbolComparer = new NavigableSymbolContentProviderComparer();

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Looks into an AST for function declaration symbols to add.
	/// </summary>
	/// <param name="symbols">The list to update.</param>
	/// <param name="snapshot">The <see cref="ITextSnapshot"/> to examine.</param>
	/// <param name="compilationUnit">The <see cref="IAstNode"/> to examine.</param>
	private static void AddFunctionDeclarationSymbolsFromAst(List<INavigableSymbol> symbols, ITextSnapshot snapshot, CompilationUnit? compilationUnit) {
		if (compilationUnit is { HasMembers: true }) {
			// Loop through the AST nodes
			foreach (var functionAstNode in compilationUnit.Members) {
				// If the function declaration has a name and body...
				if (functionAstNode is {
					Name: { Length: > 0 } name,
					Body: not null,
					StartOffset: { } startOffset,
					EndOffset: { } endOffset
				}) {
					// Build the content
					var htmlSnippet = new StringBuilder("<img src=\"resource:MethodPublic\" align=\"absbottom\" /> ")
						.Append(HtmlContentProvider.Escape(name));

					// Create the symbol
					var symbol = new NavigableSymbol(new TextSnapshotRange(snapshot, startOffset, endOffset)) {
						ContentProvider = new HtmlContentProvider(htmlSnippet.ToString())
					};

					symbols.Add(symbol);
				}
			}
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="INavigableSymbolProvider.GetSymbols"/>
	public INavigableSymbolSet GetSymbols(INavigableRequestContext context, TextSnapshotOffset snapshotOffset, INavigableSymbol? parentSymbol) {
		#if NET
		ArgumentNullException.ThrowIfNull(context);
		#else
		if (context is null)
			throw new ArgumentNullException(nameof(context));
		#endif

		// If there is AST data...
		if (
			context == NavigableRequestContexts.NavigableSymbolSelector
			&& snapshotOffset.Snapshot.Document is ICodeDocument { ParseData: ILLParseData parseData }
		) {
			// Recurse into the AST
			var symbols = new List<INavigableSymbol>();

			// NOTE: Normally here you would return either root symbols or member symbols depending on if a parentSymbol is passed...
			//   In this Simple language though, we only ever need root symbols
			if (parentSymbol is null)
				AddFunctionDeclarationSymbolsFromAst(symbols, parseData.Snapshot ?? snapshotOffset.Snapshot, parseData.Ast as CompilationUnit);

			// Sort
			symbols.Sort(_navigationSymbolComparer);

			return new NavigableSymbolSet(symbols);
		}

		// No results
		return new NavigableSymbolSet(symbols: null);
	}

}
