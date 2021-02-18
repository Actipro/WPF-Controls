using System;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionDescriptionTips {

	/// <summary>
	/// Represents an implementation of an IntelliPrompt content provider for a <see cref="Type"/>.
	/// </summary>
	public class CustomContentProvider : IContentProvider {

		private IHighlightingStyleRegistry	highlightingStyleRegistry;
		private Type						type;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomContentProvider</c> class.
		/// </summary>
		/// <param name="highlightingStyleRegistry">The <see cref="IHighlightingStyleRegistry"/> to use for finding highlighting styles.</param>
		/// <param name="type">The <see cref="Type"/> for which to provide content.</param>
		public CustomContentProvider(IHighlightingStyleRegistry highlightingStyleRegistry, Type type) {
			// Initialize
			this.highlightingStyleRegistry = highlightingStyleRegistry;
			this.type = type;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the content to use in the quick info popup.
		/// </summary>
		/// <returns>The content to use in the quick info popup.</returns>
		public object GetContent() {
			string htmlSnippet = String.Format(
				"<span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">This description tip is for the Type:</span><br/><b>{0}</b><br/><i style=\"color: " + 
					HtmlContentProvider.GetNeutralForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">Generated at {1}</i>", 
				HtmlContentProvider.Escape(type.FullName), DateTime.Now);
			return new HtmlContentProvider(htmlSnippet).GetContent();
		}

	}
}