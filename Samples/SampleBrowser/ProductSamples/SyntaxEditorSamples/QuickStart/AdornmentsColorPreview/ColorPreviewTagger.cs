using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Media;
using System.Text.RegularExpressions;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsColorPreview;

/// <summary>
/// Provides <see cref="ColorPreviewTag"/> objects over text ranges that contain the color specifications.
/// </summary>
/// <param name="document">The document to which this manager is attached.</param>
public class ColorPreviewTagger(ICodeDocument document) : TaggerBase<ColorPreviewTag>("ColorPreview", orderings: null, document, isForLanguage: true) {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IEnumerable<TagSnapshotRange<ColorPreviewTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object? parameter) {
		if (snapshotRanges is not null) {
			// Loop through the snapshot ranges
			foreach (var snapshotRange in snapshotRanges) {
				// Get the text of the snapshot range
				var text = snapshotRange.Text;

				// Look for a regex pattern match
				var matches = Regex.Matches(text, Pattern, RegexOptions.IgnoreCase);
				if (matches.Count > 0) {
					// Loop through the matches
					foreach (Match match in matches) {
						// Create a tag
						var tag = new ColorPreviewTag {
							Color = UIColor.FromWebColor(match.Value).ToColor()
						};

						// Ensure full alpha
						if (tag.Color.A < 255)
							tag.Color = Color.FromArgb(255, tag.Color.R, tag.Color.G, tag.Color.B);

						// Yield the tag
						yield return new TagSnapshotRange<ColorPreviewTag>(
							TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartOffset + match.Index, match.Length),
							tag
						);
					}
				}
			}
		}
	}

	/// <summary>
	/// The regex pattern used to match colors.
	/// </summary>
	protected virtual string Pattern
		=> /*lang=regex*/ @"(\#([a-f0-9]{6}|[a-f0-9]{3}|[a-f0-9]{8})\b)|(rgb\(\s*(\d+\%?)\s*,\s*(\d+\%?)\s*,\s*(\d+\%?)\s*\))|(rgba\(\s*(\d+\%?)\s*,\s*(\d+\%?)\s*,\s*(\d+\%?)\s*,\s*(\d(\.\d+)?)\s*\))";

}
