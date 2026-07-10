using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.IndicatorsDebugging;

/// <summary>
/// Provides <see cref="ElapsedTimeTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this tagger is attached.</param>
public class ElapsedTimeTagger(ICodeDocument document) : CollectionTagger<IIntraTextSpacerTag>(nameof(ElapsedTimeTagger), orderings: null, document, isForLanguage: true) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override bool IsTagIncluded(IIntraTextSpacerTag tag, TextSnapshotRange tagSnapshotRange, TextSnapshotRange targetSnapshotRange) {
		// If the tag's spacer is after the text range, also allow intersection at the end offset for scenarios where the tag ranges ends at the start of a new line
		return base.IsTagIncluded(tag, tagSnapshotRange, targetSnapshotRange)
			|| (!tag.IsSpacerBefore && (tagSnapshotRange.EndOffset == targetSnapshotRange.StartOffset));
	}

}
