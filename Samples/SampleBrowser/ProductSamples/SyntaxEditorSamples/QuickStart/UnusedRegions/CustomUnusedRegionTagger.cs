using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions;

/// <summary>
/// Provides <see cref="IUnusedRegionTag"/> objects over text ranges.
/// </summary>
/// <param name="document">The document to which this manager is attached.</param>
public class CustomUnusedRegionTagger(ICodeDocument document) : TaggerBase<IUnusedRegionTag>("UnusedRegion", orderings: null, document, isForLanguage: true) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<IUnusedRegionTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		var parseData = Document?.ParseData as CustomDotNetParseData;
		if (parseData?.UnusedRanges is not null) {
			var count = parseData.UnusedRanges.Count;
			if (count > 0) {
				// Return the intersecting snapshot ranges specified in the parse data
				foreach (var snapshotRange in snapshotRanges) {
					var index = parseData.UnusedRanges.BinarySearch(new TextSnapshotOffset(snapshotRange.Snapshot, snapshotRange.StartOffset));
					if (index < 0)
						index = ~index;

					while (index < count) {
						var unusedSnapshotRange = parseData.UnusedRanges[index];
						if (snapshotRange.OverlapsWith(unusedSnapshotRange))
							yield return new TagSnapshotRange<IUnusedRegionTag>(unusedSnapshotRange, new UnusedRegionTag());
						else if (unusedSnapshotRange.StartOffset >= snapshotRange.EndOffset)
							break;

						index++;
					}
				}
			}
		}
	}

}
