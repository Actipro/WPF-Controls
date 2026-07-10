using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes;

/// <summary>
/// Provides <see cref="IntraTextNoteTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this tagger is attached.</param>
public class IntraTextNoteTagger(ICodeDocument document) : CollectionTagger<IIntraTextSpacerTag>("IntraTextNoteTagger", orderings: null, document, isForLanguage: true), ITagger<IClassificationTag> {

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	IEnumerable<TagSnapshotRange<IClassificationTag>> ITagger<IClassificationTag>.GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		// We implement ITagger<IIntraTextSpacerTag> explicitly so that the core CollectionTagger can
		//   return tags of type IIntraTextSpacerTag which SyntaxEditor uses to add intra-text spacing and this
		//   method can return IClassificationTag tags that the core SyntaxEditor rendering procedures
		//   can update syntax highlighting over the marked ranges
		foreach (var tagRange in GetTags(snapshotRanges, parameter))
			yield return new TagSnapshotRange<IClassificationTag>(tagRange.SnapshotRange, (IClassificationTag)tagRange.Tag);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool IsTagIncluded(IIntraTextSpacerTag tag, TextSnapshotRange tagSnapshotRange, TextSnapshotRange targetSnapshotRange) {
		// If the tag's spacer is after the text range, also allow intersection at the end offset for scenarios where the tag ranges ends at the start of a new line
		return base.IsTagIncluded(tag, tagSnapshotRange, targetSnapshotRange)
			|| (!tag.IsSpacerBefore && (tagSnapshotRange.EndOffset == targetSnapshotRange.StartOffset));
	}

	/// <summary>
	/// Raises the <see cref="TagsChanged"/> event.
	/// </summary>
	/// <param name="e">The event data.</param>
	public void RaiseTagsChanged(TagsChangedEventArgs e)
		=> OnTagsChanged(e);

}
