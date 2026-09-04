using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningCollapsedText;

/// <summary>
/// Represents a <c>Javascript</c> language token-based outlining source.
/// </summary>
/// <param name="snapshot">The <see cref="ITextSnapshot"/> to use for this outlining source.</param>
public class JavascriptOutliningSource(ITextSnapshot snapshot) : TokenOutliningSourceBase(snapshot) {

	private static readonly OutliningNodeDefinition _curlyBraceDefinition;
	private static readonly MultiLineCommentNodeDefinition _multiLineCommentDefinition;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static JavascriptOutliningSource() {
		// Create the outlining node definitions that will be used by this outlining source to
		//   tell the document's outlining manager how to create new outlining nodes...

		_curlyBraceDefinition = new OutliningNodeDefinition("CurlyBrace") {
			IsImplementation = true
		};

		_multiLineCommentDefinition = new MultiLineCommentNodeDefinition();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override OutliningNodeAction GetNodeActionForToken(IToken token, out IOutliningNodeDefinition? definition) {
		switch (token.Key) {
			case "MultiLineCommentStartDelimiter":
				definition = _multiLineCommentDefinition;
				return OutliningNodeAction.Start;
			case "MultiLineCommentEndDelimiter":
				definition = _multiLineCommentDefinition;
				return OutliningNodeAction.End;
			case "OpenCurlyBrace":
				definition = _curlyBraceDefinition;
				return OutliningNodeAction.Start;
			case "CloseCurlyBrace":
				definition = _curlyBraceDefinition;
				return OutliningNodeAction.End;
			default:
				definition = null;
				return OutliningNodeAction.None;
		}
	}

}
