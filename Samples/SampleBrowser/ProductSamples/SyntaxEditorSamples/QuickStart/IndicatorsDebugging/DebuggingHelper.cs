using System.Windows;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Languages.DotNet;
using ActiproSoftware.Text.Languages.DotNet.Ast.Implementation;
using ActiproSoftware.Text.Parsing;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {
	
	/// <summary>
	/// Provides some helper methods for working with debugging features in this sample.
	/// </summary>
	internal static class DebuggingHelper {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Finds the statement AST node that contains the specified offset.
		/// </summary>
		/// <param name="parseData">The parse data.</param>
		/// <param name="snapshotOffset">The target snapshot offset.</param>
		/// <returns>The statement AST node that contains the specified offset.</returns>
		private static IAstNode FindContainingStatement(IDotNetParseData parseData, TextSnapshotOffset snapshotOffset) {
			// Get the offset relative to the AST's snapshot
			var offset = snapshotOffset.Offset;
			if (parseData.Snapshot != null)
				offset = snapshotOffset.TranslateTo(parseData.Snapshot, TextOffsetTrackingMode.Negative);

			// Loop upwards through the AST to find a containing statement
			var node = parseData.Ast.FindDescendantNode(offset);
			Statement statmentNode = null;
			while (node != null) {
				statmentNode = node as Statement;
				if (statmentNode != null)
					return statmentNode;

				node = node.Parent;
			}

			return null;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Sets the current statement indicator, by finding the next breakpoint after the specified snapshot offset.
		/// </summary>
		/// <param name="document">The editor document.</param>
		/// <param name="startSnapshotOffset">The starting snapshot offset to examine.</param>
		/// <returns>The current statement snapshot offset.</returns>
		public static TextSnapshotOffset SetCurrentStatement(IEditorDocument document, TextSnapshotOffset startSnapshotOffset) {
			if (!startSnapshotOffset.IsDeleted) {
				// Create search options (only allow enabled breakpoints)
				var options = new TagSearchOptions<BreakpointIndicatorTag>();
				options.Filter = (tr => tr.Tag.IsEnabled);

				// Find the next breakpoint
				var tagRange = document.IndicatorManager.Breakpoints.FindNext(startSnapshotOffset, options);
				if (tagRange != null) {
					// Get the snapshot range of the breakpoint
					var snapshotRange = tagRange.VersionRange.Translate(startSnapshotOffset.Snapshot);
					var currentStatementSnapshotOffset = new TextSnapshotOffset(snapshotRange.Snapshot, snapshotRange.EndOffset);

					// Set the current statement indicator range
					document.IndicatorManager.CurrentStatement.SetInstance(snapshotRange);

					return currentStatementSnapshotOffset;
				}
			}

			// Remove any current statement indicator
			document.IndicatorManager.CurrentStatement.Clear();

			return TextSnapshotOffset.Deleted;
		}
		
		/// <summary>
		/// Toggles a breakpoint.
		/// </summary>
		/// <param name="snapshotOffset">The <see cref="TextSnapshotOffset"/> of the indicator.</param>
		/// <param name="isEnabled">Whether a new breakpoint should be enabled.</param>
		public static void ToggleBreakpoint(TextSnapshotOffset snapshotOffset, bool isEnabled) {
			var document = snapshotOffset.Snapshot.Document as IEditorDocument;
			if (document == null)
				return;

			var parseData = document.ParseData as IDotNetParseData;
			if (parseData == null)
				return;

			// Find the containing statement
			var node = FindContainingStatement(parseData, snapshotOffset);
			if ((node == null) || (!node.StartOffset.HasValue) || (!node.EndOffset.HasValue)) {
				MessageBox.Show("Please move the caret inside of a valid C# statement.", "Toggle Breakpoint", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				return;
			}

			// Get the snapshot range of the statement
			var snapshotRange = new TextSnapshotRange(parseData.Snapshot ?? snapshotOffset.Snapshot, node.StartOffset.Value, node.EndOffset.Value);

			// Create a breakpoint tag
			var tag = new BreakpointIndicatorTag();
			tag.IsEnabled = isEnabled;

			// Toggle the indicator
			var tagRange = document.IndicatorManager.Breakpoints.Toggle(snapshotRange, tag);

			// Set the tag's content provider (quick info for the glyph) if a tag was added
			if (tagRange != null)
				tag.ContentProvider = new BreakpointIndicatorTagContentProvider(tagRange);
		}
		
	}

}

