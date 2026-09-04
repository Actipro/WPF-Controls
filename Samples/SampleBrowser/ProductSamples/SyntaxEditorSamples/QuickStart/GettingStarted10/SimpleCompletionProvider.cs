using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;  // For context-related types
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09;  // For FunctionContentProvider
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted10;

/// <summary>
/// Provides IntelliPrompt completion data for the <c>Simple</c> language.
/// </summary>
public class SimpleCompletionProvider() : CompletionProviderBase("Simple") {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup) {
		// Get the context factory service
		var contextFactory = view.SyntaxEditor.Document.Language.GetService<SimpleContextFactory>();

		// Get a context
		var context = contextFactory?.CreateContext(view.Selection.EndSnapshotOffset, includeArgumentInfo: false);
		if (context is not null) {

			// Create a session
			var session = new CompletionSession {
				CanCommitWithoutPopup = canCommitWithoutPopup
			};

			switch (context.Type) {
				case SimpleContextType.Default:
					// Add items for keywords
					session.Items.Add(new CompletionItem("function", new CommonImageSourceProvider(CommonImageKind.Keyword),
						new PlainTextContentProvider("Declares a function.")));
					break;
				case SimpleContextType.FunctionDeclarationBlock:
				case SimpleContextType.FunctionReference: {
					// Add items for keywords
					session.Items.Add(new CompletionItem("var", new CommonImageSourceProvider(CommonImageKind.Keyword),
						new PlainTextContentProvider("Declares a variable.")));
					session.Items.Add(new CompletionItem("return", new CommonImageSourceProvider(CommonImageKind.Keyword),
						new PlainTextContentProvider("Returns a value.")));

					// Add items (one for each function name)
					if (view.SyntaxEditor.Document.ParseData is ILLParseData { Ast: CompilationUnit { HasMembers: true } compilationUnit }) {
						// Loop through the AST nodes
						foreach (var functionAstNode in compilationUnit.Members) {
							session.Items.Add(new CompletionItem(functionAstNode.Name, new CommonImageSourceProvider(CommonImageKind.MethodPublic),
								new FunctionContentProvider(view.HighlightingStyleRegistry, functionAstNode, includeImage: false, view.DefaultBackgroundColor)));
						}
					}
					break;
				}
			}

			if (session.Items.Count > 0) {
				// Ensure the caret is visible
				view.Scroller.ScrollToCaret();

				// Ensure the items are sorted and open the session
				session.SortItems();
				session.Open(view);
				return true;
			}
		}
		return false;
	}

}
