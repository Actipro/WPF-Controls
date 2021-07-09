using System;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Reflection;
using ActiproSoftware.Text.Languages.DotNet.Resolution;
using ActiproSoftware.Text.Languages.CSharp.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.DotNetAddonCompletionOverride {
	
	/// <summary>
	/// Overrides the <c>C#</c> language's completion provider to filter results.
	/// </summary>
	public class CustomCSharpCompletionProvider : CSharpCompletionProvider {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Allows an inheritor to modify, filter and sort automatically-generated items before the specified <see cref="ICompletionSession"/> 
		/// is opened and displayed to the end user.  The session may also be cancelled.
		/// </summary>
		/// <param name="session">The <see cref="ICompletionSession"/> about to be opened.</param>
		/// <returns>
		/// <c>true</c> if the session is allowed to open; otherwise, <c>false</c>.
		/// </returns>
		/// <remarks>
		/// The default implementation of this method sorts the items in the session.
		/// </remarks>
		protected override bool OnSessionOpening(ICompletionSession session) {
			if (session == null)
				throw new ArgumentNullException("session");

			// Items from the default provider will have their Tag property filled in with an object that can be:
			//   1) INamespaceResolverResult - A namespace
			//   2) ITypeResolverResult - A type
			//   3) ITypeMemberResolverResult - A type member (method, property, etc.)
			//   4) IParameterResolverResult - A parameter
			//   5) IVariableResolverResult - A variable
			//   6) ICodeSnippetMetadata - A code snippet
			//   7) null - A language keyword

			// Loop through items from the base completion provider
			for (var index = session.Items.Count - 1; index >= 0; index--) {
				// If the item is a namespace, parameter, or code snippet (we filter out these as an example)...
				var tag = session.Items[index].Tag;
				if (
					(tag is INamespaceResolverResult) ||
					(tag is IParameterResolverResult) ||
					(tag is ICodeSnippetMetadata)
					) {

					// Remove the item
					session.Items.RemoveAt(index);
				}
				else {
					// For types, use the namespace as the inline description
					var typeResult = tag as ITypeResolverResult;
					if (typeResult != null) {
						var typeDef = typeResult.Type as ITypeDefinition;
						if (typeDef != null)
							((CompletionItem)session.Items[index]).InlineDescription = typeDef.Namespace;
					}
				}
			}

			// Remove all filters
			session.Filters.Clear();

			// Sort the items
			session.SortItems();

			// Get the context for which the session was created
			var context = session.Context as IDotNetContext;
			if ((context != null) && (context.TargetExpression == null)) {
				// Add in a custom item at the top of the list if there is no target expression (meaning doing Ctrl+Space in whitespace)...
				//   Note this could be done before the SortItems() call above to sort it properly
				session.Items.Insert(0, new CompletionItem("customItem", new CommonImageSourceProvider(CommonImageKind.FieldPublic),
					new HtmlContentProvider("<b>Custom Item</b><br/>This item was inserted by the custom completion provider.")));
			}

			return true;
		}
		
	}

}
