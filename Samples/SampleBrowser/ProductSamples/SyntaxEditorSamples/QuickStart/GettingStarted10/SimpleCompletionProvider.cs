using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Parsing.LLParser;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted08;  // For context-related types
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09;  // For FunctionContentProvider

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted10 {
	
	/// <summary>
	/// Provides IntelliPrompt completion data for the <c>Simple</c> language.
	/// </summary>
	public class SimpleCompletionProvider : CompletionProviderBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>SimpleCompletionProvider</c> class.
		/// </summary>
		public SimpleCompletionProvider() : base("Simple") {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Requests that an <see cref="ICompletionSession"/> be opened for the specified <see cref="IEditorView"/>.
		/// </summary>
		/// <param name="view">The <see cref="IEditorView"/> that will host the session.</param>
		/// <param name="canCommitWithoutPopup">Whether the session can immediately commit if a single match is made when the session is opened, commonly known as "complete word" functionality.</param>
		/// <returns>
		/// <c>true</c> if a session was opened; otherwise, <c>false</c>.
		/// </returns>
		public override bool RequestSession(IEditorView view, bool canCommitWithoutPopup) {
			// Get the context factory service
			SimpleContextFactory contextFactory = view.SyntaxEditor.Document.Language.GetService<SimpleContextFactory>();
			if (contextFactory != null) {
				// Get a context
				SimpleContext context = contextFactory.CreateContext(view.Selection.EndSnapshotOffset, false);
				
				// Create a session
				CompletionSession session = new CompletionSession();
				session.CanCommitWithoutPopup = canCommitWithoutPopup;

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
						ILLParseData parseData = view.SyntaxEditor.Document.ParseData as ILLParseData;
						if (parseData != null) {
							CompilationUnit compilationUnit = parseData.Ast as CompilationUnit;
							if ((compilationUnit != null) && (compilationUnit.HasMembers)) {
								// Loop through the AST nodes
								foreach (FunctionDeclaration functionAstNode in compilationUnit.Members) {
									session.Items.Add(new CompletionItem(functionAstNode.Name, new CommonImageSourceProvider(CommonImageKind.MethodPublic),
										new FunctionContentProvider(view.HighlightingStyleRegistry, functionAstNode, false, view.DefaultBackgroundColor)));
								}
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

}

