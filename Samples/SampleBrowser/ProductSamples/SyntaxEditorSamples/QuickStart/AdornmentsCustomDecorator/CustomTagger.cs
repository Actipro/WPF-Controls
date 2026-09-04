using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using System.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsCustomDecorator;

/// <summary>
/// Provides <see cref="CustomTag"/> objects over text ranges that contain the specified regex pattern text.
/// </summary>
/// <param name="document">The document to which this manager is attached.</param>
public class CustomTagger(ICodeDocument document) : TaggerBase<CustomTag>("CustomTagger", orderings: null, document, isForLanguage: true) {

	private readonly string _pattern = /*lang=regex*/ @"\bActipro\b";

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<CustomTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		if (snapshotRanges is not null) {
			// Loop through the snapshot ranges
			foreach (var snapshotRange in snapshotRanges) {
				// Get the text of the snapshot range
				var text = snapshotRange.Text;

				// Look for a regex pattern match
				var matches = Regex.Matches(text, _pattern, RegexOptions.IgnoreCase);
				if (matches.Count > 0) {
					// Loop through the matches
					foreach (Match match in matches) {
						// Create a tag
						var tag = new CustomTag();

						// Yield the tag
						yield return new TagSnapshotRange<CustomTag>(
							TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartOffset + match.Index, match.Length),
							tag
						);
					}
				}
			}
		}
	}

}
