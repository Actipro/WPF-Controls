using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Windows.Media;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsColorPreview {
    
	/// <summary>
	/// Provides <see cref="ColorPreviewTag"/> objects over text ranges that contain the color specifications.
	/// </summary>
	public class ColorPreviewTagger : TaggerBase<ColorPreviewTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>ColorPreviewTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this manager is attached.</param>
		public ColorPreviewTagger(ICodeDocument document) : base("ColorPreview", null, document, true) {}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<ColorPreviewTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			if (snapshotRanges != null) {
				// Loop through the snapshot ranges
				foreach (TextSnapshotRange snapshotRange in snapshotRanges) {
					// Get the text of the snapshot range
					string text = snapshotRange.Text;

					// Look for a regex pattern match
					MatchCollection matches = Regex.Matches(text, this.Pattern, RegexOptions.IgnoreCase);
					if (matches.Count > 0) {
						// Loop through the matches
						foreach (Match match in matches) {
							// Create a tag
							ColorPreviewTag tag = new ColorPreviewTag();
							tag.Color = UIColor.FromWebColor(match.Value).ToColor();

							// Ensure full alpha
							if (tag.Color.A < 255)
								tag.Color = Color.FromArgb(255, tag.Color.R, tag.Color.G, tag.Color.B);

							// Yield the tag
							yield return new TagSnapshotRange<ColorPreviewTag>(
								TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartOffset + match.Index, match.Length), tag);
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets the regex pattern used to match colors.
		/// </summary>
		/// <value>The regex pattern used to match colors.</value>
		protected virtual string Pattern { 
			get {
				return @"(\#([a-f0-9]{6}|[a-f0-9]{3}|[a-f0-9]{8})\b)|(rgb\(\s*(\d+\%?)\s*,\s*(\d+\%?)\s*,\s*(\d+\%?)\s*\))|(rgba\(\s*(\d+\%?)\s*,\s*(\d+\%?)\s*,\s*(\d+\%?)\s*,\s*(\d(\.\d+)?)\s*\))";
			}
		}

	}

}
