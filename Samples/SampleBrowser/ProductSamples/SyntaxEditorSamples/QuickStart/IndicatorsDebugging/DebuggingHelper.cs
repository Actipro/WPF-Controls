using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Tagging.Implementation;
using MessageBox = ActiproSoftware.Windows.Controls.ThemedMessageBox;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging;

/// <summary>
/// Provides some helper methods for working with debugging features in this sample.
/// </summary>
internal static class DebuggingHelper {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Finds the statement AST node that contains the specified offset.
	/// </summary>
	/// <param name="parseData">The parse data.</param>
	/// <param name="snapshotOffset">The target snapshot offset.</param>
	private static Statement? FindContainingStatement(IDotNetParseData parseData, TextSnapshotOffset snapshotOffset) {
		// Get the offset relative to the AST's snapshot
		var offset = snapshotOffset.Offset;
		if (parseData.Snapshot is not null)
			offset = snapshotOffset.TranslateTo(parseData.Snapshot, TextOffsetTrackingMode.Negative);

		// Loop upwards through the AST to find a containing statement
		var node = parseData.Ast?.FindDescendantNode(offset);
		while (node is not null) {
			if (node is Statement statementNode)
				return statementNode;

			node = node.Parent;
		}

		return null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Sets the current statement indicator, by finding the next breakpoint after the specified snapshot offset.
	/// </summary>
	/// <param name="document">The editor document.</param>
	/// <param name="startSnapshotOffset">The starting snapshot offset to examine.</param>
	/// <returns>The current statement snapshot offset.</returns>
	public static TextSnapshotOffset? SetCurrentStatement(IEditorDocument document, TextSnapshotOffset? startSnapshotOffset) {
		if (startSnapshotOffset.HasValue) {
			// Create search options (only allow enabled breakpoints)
			var options = new TagSearchOptions<BreakpointIndicatorTag> {
				Filter = (tr => tr.Tag.IsEnabled)
			};

			// Find the next breakpoint
			var tagRange = document.IndicatorManager.Breakpoints.FindNext(startSnapshotOffset.Value, options);
			if (tagRange is not null) {
				// Get the snapshot range of the breakpoint
				var snapshotRange = tagRange.VersionRange.Translate(startSnapshotOffset.Value.Snapshot);
				if (snapshotRange.HasValue) {
					var currentStatementSnapshotOffset = new TextSnapshotOffset(snapshotRange.Value.Snapshot, snapshotRange.Value.EndOffset);

					// Set the current statement indicator range
					document.IndicatorManager.CurrentStatement.SetInstance(snapshotRange.Value);

					return currentStatementSnapshotOffset;
				}
			}
		}

		// Remove any current statement indicator
		document.IndicatorManager.CurrentStatement.Clear();

		return null;
	}

	/// <summary>
	/// Toggles a breakpoint.
	/// </summary>
	/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> of the indicator.</param>
	/// <param name="isEnabled">Whether a new breakpoint should be enabled.</param>
	public static void ToggleBreakpoint(TextSnapshotOffset snapshotOffset, bool isEnabled) {
		if (snapshotOffset.Snapshot.Document is not IEditorDocument { ParseData: IDotNetParseData parseData } document)
			return;

		// Find the containing statement
		var statement = FindContainingStatement(parseData, snapshotOffset);
		if (statement is not { StartOffset: not null, EndOffset: not null }) {
			MessageBox.Show("Please move the caret inside of a valid C# statement.", "Toggle Breakpoint", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}

		// Get the snapshot range of the statement
		var snapshotRange = new TextSnapshotRange(parseData.Snapshot ?? snapshotOffset.Snapshot, statement.StartOffset.Value, statement.EndOffset.Value);

		// Create a breakpoint tag
		var tag = new BreakpointIndicatorTag {
			IsEnabled = isEnabled
		};

		// Toggle the indicator
		var tagRange = document.IndicatorManager.Breakpoints.Toggle(snapshotRange, tag);

		// Set the tag's content provider (quick info for the glyph) if a tag was added
		if (tagRange is not null)
			tag.ContentProvider = new BreakpointIndicatorTagContentProvider(tagRange);
	}

}
