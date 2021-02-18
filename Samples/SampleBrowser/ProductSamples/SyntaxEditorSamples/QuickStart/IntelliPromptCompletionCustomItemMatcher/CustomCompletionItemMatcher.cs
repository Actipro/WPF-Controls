using System;
using System.Text.RegularExpressions;
using ActiproSoftware.Windows.Controls.SyntaxEditor.IntelliPrompt.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IntelliPromptCompletionCustomItemMatcher {

	/// <summary>
	/// A completion item matcher that matches items in which supplied text appears after a <c>.</c> or <c>_</c>.
	/// </summary>
	public class CustomCompletionItemMatcher : RegexCompletionItemMatcherBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the <see cref="Regex"/> to use for matching based on supplied text.
		/// </summary>
		/// <param name="text">The text for which to return a <see cref="Regex"/>.</param>
		/// <returns>The <see cref="Regex"/> that was created.</returns>
		protected override Regex GetRegex(string text) {
			// Make sure the text to highlight is surrounded with parenthesis so that SyntaxEditor can locate the captures
			return new Regex(String.Format("[\\._]({0})", Regex.Escape(text)), RegexOptions.IgnoreCase | RegexOptions.Singleline);
		}

		/// <summary>
		/// Gets the string-based key that identifies the object.
		/// </summary>
		/// <value>The string-based key that identifies the object.</value>
		public override string Key { 
			get {
				return "Custom";
			}
		}

	}

}