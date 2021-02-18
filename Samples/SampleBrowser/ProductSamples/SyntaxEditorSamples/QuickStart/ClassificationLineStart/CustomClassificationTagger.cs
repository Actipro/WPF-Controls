using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.SyntaxEditor;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ClassificationLineStart {

	/// <summary>
	/// Provides a custom implementation of a tagger that can classify ranges of text within a text buffer.
	/// </summary>
	public class CustomClassificationTagger : TaggerBase<IClassificationTag> {

		private static IClassificationType commentCT = new ClassificationType("Comment");
		private static IClassificationType errorCT = new ClassificationType("Error");

		private static HighlightingStyleRegistry styleRegistry;
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomClassificationTagger</c> class.
		/// </summary>
		static CustomClassificationTagger() {
			// Create a custom IHighlightingStyleRegistry, meaning that the classification types defined
			//   in this sample and related styles will not be included in the AmbientHighlightingStyleRegistry...
			//   Normally the AmbientHighlightingStyleRegistry is used but in this sample we wanted to also show
			//   how you can use custom IHighlightingStyleRegistry instances with classification tags
			styleRegistry = new HighlightingStyleRegistry();
			styleRegistry.Register(commentCT, new HighlightingStyle(Colors.Green));
			styleRegistry.Register(errorCT, new HighlightingStyle(Colors.Maroon) { Bold = true });
		}
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomClassificationTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this manager is attached.</param>
		public CustomClassificationTagger(ICodeDocument document) : base("Custom", null, document) {}
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<IClassificationTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			// Loop through the requested snapshot ranges...
			foreach (TextSnapshotRange snapshotRange in snapshotRanges) {
				// If the snapshot range is not zero-length...
				if (!snapshotRange.IsZeroLength) {
					// Get a snapshot reader
					ITextSnapshotReader reader = snapshotRange.Snapshot.GetReader(snapshotRange.StartOffset);

					// If not already at the start of a line, back up to the start
					if (!reader.IsAtSnapshotLineStart)
						reader.GoToCurrentSnapshotLineStart();

					// Read through the snapshot until the end of the target range is reached
					while ((!reader.IsAtSnapshotEnd) && (reader.Offset < snapshotRange.EndOffset)) {
						// Save the start of the line offset
						int lineStartOffset = reader.Offset;

						// Get the line start text (we need at most 6 chars for this sample)
						string lineStartText = reader.PeekText(6);

						// Go to the end of the line
						reader.GoToCurrentSnapshotLineEnd();

						// Add a range for the line if it starts with one of the defined strings... 
						//   The StyleRegistryClassificationTag is a special ClassificationTag that allows you to indicate
						//   an alternate IHighlightingStyleRegistry to use for syntax highlighting... if using the 
						//   normal AmbientHighlightingStyleRegistry, you'd just use a regular ClassificationTag instead
						if (lineStartText.StartsWith("---")) {
							// Apply green to lines that start with "---"
							yield return new TagSnapshotRange<IClassificationTag>(
								new TextSnapshotRange(snapshotRange.Snapshot, new TextRange(lineStartOffset, reader.Offset)),
								new StyleRegistryClassificationTag(commentCT, styleRegistry)
								);
						}
						else if (lineStartText.StartsWith("Error:")) {
							// Apply maroon to lines that start with "Error:"
							yield return new TagSnapshotRange<IClassificationTag>(
								new TextSnapshotRange(snapshotRange.Snapshot, new TextRange(lineStartOffset, reader.Offset)),
								new StyleRegistryClassificationTag(errorCT, styleRegistry)
								);
						}

						// Consume the newline
						reader.GoToNextSnapshotLineStart();
					}
				}
			}
		}
	
	}
}