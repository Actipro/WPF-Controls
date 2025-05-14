using System;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging {
    
	/// <summary>
	/// Provides <see cref="ElapsedTimeTag"/> objects over text ranges.
	/// </summary>
	public class ElapsedTimeTagger : CollectionTagger<IIntraTextSpacerTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="document">The document to which this tagger is attached.</param>
		public ElapsedTimeTagger(ICodeDocument document) : base(nameof(ElapsedTimeTagger), null, document, true) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns whether the specified tag's snapshot range intersects with the requested snapshot range, used to determine if results for the <see cref="GetTags"/> method.
		/// </summary>
		/// <param name="requestedSnapshotRange">The requested range's <see cref="TextSnapshotRange"/>.</param>
		/// <param name="tag">The tag to examine.</param>
		/// <param name="tagSnapshotRange">The tag's <see cref="TextSnapshotRange"/>.</param>
		/// <returns>
		/// <c>true</c> if there is intersection within the requested snapshot range; otherwise, <c>false</c>.
		/// </returns>
		protected override bool IntersectsWith(TextSnapshotRange requestedSnapshotRange, IIntraTextSpacerTag tag, TextSnapshotRange tagSnapshotRange) {
			// If the tag's spacer is after the text range, also allow intersection at the end offset for scenarios where the tag ranges ends at the start of a new line
			return base.IntersectsWith(requestedSnapshotRange, tag, tagSnapshotRange)
				|| (!tag.IsSpacerBefore && (tagSnapshotRange.EndOffset == requestedSnapshotRange.StartOffset));
		}
		
	}

}
