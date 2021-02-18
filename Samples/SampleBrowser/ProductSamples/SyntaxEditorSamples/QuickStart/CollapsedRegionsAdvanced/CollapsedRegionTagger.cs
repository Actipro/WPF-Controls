using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced {
    
	/// <summary>
	/// Provides <see cref="CollapsedRegionTag"/> objects over text ranges.
	/// </summary>
	public class CollapsedRegionTagger : CollectionTagger<ICollapsedRegionTag>, ITagger<IIntraTextSpacerTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CollapsedRegionTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this tagger is attached.</param>
		public CollapsedRegionTagger(ICodeDocument document) : base("CollapsedRegionTagger", null, document, true) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		IEnumerable<TagSnapshotRange<IIntraTextSpacerTag>> ITagger<IIntraTextSpacerTag>.GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			foreach (TagSnapshotRange<ICollapsedRegionTag> tagRange in this.GetTags(snapshotRanges, parameter)) {
				CollapsedRegionTag tag = tagRange.Tag as CollapsedRegionTag;
				if (tag != null)
					yield return tag.ToIntraTextSpacerTagRange(tagRange.SnapshotRange);
			}
		}

	}

}
