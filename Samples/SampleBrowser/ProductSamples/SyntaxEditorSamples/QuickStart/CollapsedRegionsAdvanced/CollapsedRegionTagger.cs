using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.CollapsedRegionsAdvanced;

/// <summary>
/// Provides <see cref="CollapsedRegionTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this tagger is attached.</param>
public class CollapsedRegionTagger(ICodeDocument document)
	: CollectionTagger<ICollapsedRegionTag>("CollapsedRegionTagger", orderings: null, document, isForLanguage: true), ITagger<IIntraTextSpacerTag> {

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	IEnumerable<TagSnapshotRange<IIntraTextSpacerTag>> ITagger<IIntraTextSpacerTag>.GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		foreach (var tagRange in GetTags(snapshotRanges, parameter)) {
			if (tagRange.Tag is CollapsedRegionTag tag)
				yield return tag.ToIntraTextSpacerTagRange(tagRange.SnapshotRange);
		}
	}

}
