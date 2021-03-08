using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.UnusedRegions {

	/// <summary>
	/// Provides <see cref="IUnusedRegionTag"/> objects over text ranges.
	/// </summary>
	public class CustomUnusedRegionTagger : TaggerBase<IUnusedRegionTag> {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomUnusedRegionTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this manager is attached.</param>
		public CustomUnusedRegionTagger(ICodeDocument document) : base("UnusedRegion", null, document, true) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<IUnusedRegionTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			var parseData = this.Document.ParseData as CustomDotNetParseData;
			if ((parseData != null) && (parseData.UnusedRanges != null)) {
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

}
