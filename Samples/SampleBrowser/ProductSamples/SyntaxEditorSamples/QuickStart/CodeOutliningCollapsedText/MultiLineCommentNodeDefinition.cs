using ActiproSoftware.Text;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Outlining.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CodeOutliningCollapsedText;

/// <summary>
/// Implements a multi-line comment <see cref="IOutliningNodeDefinition"/> that renders some of
/// a collapsed node's inner text.
/// </summary>
public class MultiLineCommentNodeDefinition : OutliningNodeDefinition {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public MultiLineCommentNodeDefinition() : base("MultiLineComment") {
		DefaultCollapsedContent = "/**/";
		IsDefaultCollapsed = true;
		IsImplementation = true;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override object? GetCollapsedContent(IOutliningNode node) {
		// Get the node's snapshot range
		var snapshotRange = node.SnapshotRange;

		// If the comment is over multiple lines...
		if (snapshotRange.StartPosition.Line < snapshotRange.EndPosition.Line) {
			// Use the text in the first line
			var lineEndOffset = snapshotRange.StartLine.EndOffset;
			return snapshotRange.Snapshot.GetSubstring(new TextRange(snapshotRange.StartOffset, lineEndOffset)) + "...";
		}
		else {
			// On a single line... use default collapsed content
			return DefaultCollapsedContent;
		}
	}

}
