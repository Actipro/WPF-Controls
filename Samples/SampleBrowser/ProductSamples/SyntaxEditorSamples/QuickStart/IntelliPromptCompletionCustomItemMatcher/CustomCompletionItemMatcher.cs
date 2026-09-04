using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;
using System.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionCustomItemMatcher;

/// <summary>
/// A completion item matcher that matches items in which supplied text appears after a <c>.</c> or <c>_</c>.
/// </summary>
public class CustomCompletionItemMatcher : RegexCompletionItemMatcherBase {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override Regex GetRegex(string text, bool captureMatches) {
		if (captureMatches) {
			// Make sure the text to highlight is surrounded with parenthesis so that SyntaxEditor can locate the captures
			return new Regex(string.Format("[\\._]({0})", Regex.Escape(text)), RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}
		else {
			// No parenthesis are necessary and can be excluded to improve performance
			return new Regex(string.Format("[\\._]{0}", Regex.Escape(text)), RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}
	}

	/// <inheritdoc/>
	public override string Key
		=> "Custom";

}
