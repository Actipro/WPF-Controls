using System;
using System.Text;
using System.Windows.Media;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09 {
	
	/// <summary>
	/// Provides IntelliPrompt popup content for a function.
	/// </summary>
	public class FunctionContentProvider : IParameterizedContentProvider {

		private Color						backgroundColorHint;
		private FunctionDeclaration			functionDecl;
		private IHighlightingStyleRegistry	highlightingStyleRegistry;
		private bool						includeImage;
		private int?						parameterIndex;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>FunctionContentProvider</c> class.
		/// </summary>
		/// <param name="highlightingStyleRegistry">The <see cref="IHighlightingStyleRegistry"/> to use for finding highlighting styles.</param>
		/// <param name="functionDecl">The function declaration.</param>
		/// <param name="includeImage">Whether to include images in the output.</param>
		/// <param name="backgroundColorHint">The content host's background color.</param>
		public FunctionContentProvider(IHighlightingStyleRegistry highlightingStyleRegistry, FunctionDeclaration functionDecl, bool includeImage, Color backgroundColorHint) {
			if (functionDecl == null)
				throw new ArgumentNullException("functionDecl");

			// Initialize
			this.highlightingStyleRegistry = highlightingStyleRegistry;
			this.functionDecl = functionDecl;
			this.includeImage = includeImage;
			this.backgroundColorHint = backgroundColorHint;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the content to use in various IntelliPrompt popups.
		/// </summary>
		/// <returns>The content to use in various IntelliPrompt popups.</returns>
		public object GetContent() {
			var htmlSnippet = new StringBuilder();
			
			if (includeImage) {
				// Append icon
				htmlSnippet.Append("<img src=\"resource:");
				htmlSnippet.Append(HtmlContentProvider.Escape(CommonImageKind.MethodPublic.ToString()));
				htmlSnippet.Append("\" align=\"absbottom\" /> ");
			}
				
			// Append function name
			htmlSnippet.Append("<span style=\"color: " + HtmlContentProvider.GetKeywordForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">function</span> ");
			htmlSnippet.Append(HtmlContentProvider.Escape(functionDecl.Name));

			// Append parameters
			htmlSnippet.Append("(");
			for (int index = 0; index < functionDecl.Parameters.Count; index++) {
				if (index > 0)
					htmlSnippet.Append(", ");

				if (index == parameterIndex)
					htmlSnippet.Append("<b>");
				htmlSnippet.Append(functionDecl.Parameters[index]);
				if (index == parameterIndex)
					htmlSnippet.Append("</b>");
			}
			htmlSnippet.Append(")");

			// Append description
			htmlSnippet.AppendFormat("<br/><span style=\"color: " + HtmlContentProvider.GetCommentForegroundColor(highlightingStyleRegistry).ToWebColor() + ";\">This function has {0} parameter{1}.</span>", 
				functionDecl.Parameters.Count, (functionDecl.Parameters.Count == 1 ? String.Empty : "s"));

			return new HtmlContentProvider(htmlSnippet.ToString(), backgroundColorHint).GetContent();
		}

		/// <summary>
		/// Gets or sets the index of the parameter for which to provide additional detail, if known.
		/// </summary>
		/// <value>The index of the parameter for which to provide additional detail, if known.</value>
		public int? ParameterIndex { 
			get {
				return parameterIndex;
			}
			set {
				parameterIndex = value;
			}
		}

	}

}

