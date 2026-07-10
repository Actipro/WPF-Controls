using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionDescriptionTips;

/// <summary>
/// Represents an implementation of an IntelliPrompt content provider for a <see cref="Type"/>.
/// </summary>
/// <param name="highlightingStyleRegistry">The <see cref="IHighlightingStyleRegistry"/> to use for finding highlighting styles.</param>
/// <param name="type">The <see cref="Type"/> for which to provide content.</param>
public class CustomContentProvider(IHighlightingStyleRegistry highlightingStyleRegistry, Type type) : IContentProvider {

	private readonly IHighlightingStyleRegistry _highlightingStyleRegistry = highlightingStyleRegistry;
	private readonly Type _type = type;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IContentProvider.GetContent"/>
	public object? GetContent() {
		var htmlSnippet = string.Format(
			"<span style=\"color: {0};\">This description tip is for the Type:</span><br/><b>{1}</b><br/><i style=\"color: {2};\">Generated at {3}</i>",
			HtmlContentProvider.GetCommentForegroundColor(_highlightingStyleRegistry).ToWebColor(),
			HtmlContentProvider.Escape(_type.FullName),
			HtmlContentProvider.GetSecondaryTextForegroundColor(_highlightingStyleRegistry).ToWebColor(),
			DateTime.Now
		);
		return new HtmlContentProvider(htmlSnippet).GetContent();
	}

}
