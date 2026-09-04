using ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted04d;  // For AST nodes
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.GettingStarted09;

/// <summary>
/// Provides IntelliPrompt popup content for a function.
/// </summary>
public class FunctionContentProvider : IParameterizedContentProvider {

	private readonly Color _backgroundColorHint;
	private readonly FunctionDeclaration _functionDecl;
	private readonly IHighlightingStyleRegistry _highlightingStyleRegistry;
	private readonly bool _includeImage;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	/// <param name="highlightingStyleRegistry">The <see cref="IHighlightingStyleRegistry"/> to use for finding highlighting styles.</param>
	/// <param name="functionDecl">The function declaration.</param>
	/// <param name="includeImage">Whether to include images in the output.</param>
	/// <param name="backgroundColorHint">The content host's background color.</param>
	public FunctionContentProvider(IHighlightingStyleRegistry highlightingStyleRegistry, FunctionDeclaration functionDecl, bool includeImage, Color backgroundColorHint) {
		_highlightingStyleRegistry = highlightingStyleRegistry;
		_functionDecl = functionDecl ?? throw new ArgumentNullException(nameof(functionDecl));
		_includeImage = includeImage;
		_backgroundColorHint = backgroundColorHint;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IContentProvider.GetContent"/>
	public object? GetContent() {
		var htmlSnippet = new StringBuilder();

		if (_includeImage) {
			// Append icon
			htmlSnippet.Append("<img src=\"resource:")
				.Append(HtmlContentProvider.Escape(CommonImageKind.MethodPublic.ToString()))
				.Append("\" align=\"absbottom\" /> ");
		}

		// Append function name
		htmlSnippet.Append("<span style=\"color: ")
			.Append(HtmlContentProvider.GetKeywordForegroundColor(_highlightingStyleRegistry).ToWebColor())
			.Append(";\">function</span> ")
			.Append(HtmlContentProvider.Escape(_functionDecl.Name));

		// Append parameters
		htmlSnippet.Append('(');
		for (var index = 0; index < _functionDecl.Parameters.Count; index++) {
			if (index > 0)
				htmlSnippet.Append(", ");

			if (index == ParameterIndex)
				htmlSnippet.Append("<b>");
			htmlSnippet.Append(_functionDecl.Parameters[index]);
			if (index == ParameterIndex)
				htmlSnippet.Append("</b>");
		}
		htmlSnippet.Append(')');

		// Append description
		htmlSnippet.Append("<br/><span style=\"color: ")
			.Append(HtmlContentProvider.GetCommentForegroundColor(_highlightingStyleRegistry).ToWebColor())
			.AppendFormat(";\">This function has {0} parameter{1}.</span>", _functionDecl.Parameters.Count, (_functionDecl.Parameters.Count == 1 ? string.Empty : "s"));

		return new HtmlContentProvider(htmlSnippet.ToString(), _backgroundColorHint).GetContent();
	}

	/// <inheritdoc cref="IParameterizedContentProvider.ParameterIndex"/>
	public int? ParameterIndex { get; set; }

}
