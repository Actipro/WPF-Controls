using ActiproSoftware.Text;
using ActiproSoftware.Text.Lexing;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningTokenBased;

/// <summary>
/// Represents a <c>Javascript</c> language token-based outlining source.
/// </summary>
/// <param name="snapshot">The <see cref="ITextSnapshot"/> to use for this outlining source.</param>
public class JavascriptOutliningSource(ITextSnapshot snapshot) : TokenOutliningSourceBase(snapshot) {

	private static readonly OutliningNodeDefinition _curlyBraceDefinition;
	private static readonly OutliningNodeDefinition _multiLineCommentDefinition;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes the class.
	/// </summary>
	static JavascriptOutliningSource() {
		// Create the outlining node definitions that will be used by this outlining source to
		//   tell the document's outlining manager how to create new outlining nodes...
		//
		// Each definition can indicate options such as:
		//   1) Whether the node is an implementation and will be collapsed when "Collapse to Definitions" is clicked
		//   2) The default collapsed content for the node that appears in the in-line collapsed node box
		//   3) If the node should be collapsed by default when loading a file, such as for #region type nodes
		//   4) If the node is collapsible... when false, no UI appears for the node in the margin

		_curlyBraceDefinition = new OutliningNodeDefinition("CurlyBrace") {
			IsImplementation = true
		};

		_multiLineCommentDefinition = new OutliningNodeDefinition("MultiLineComment") {
			DefaultCollapsedContent = "/**/",
			IsImplementation = true
		};
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
