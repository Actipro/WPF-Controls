using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsCustomDecorator {
    
	/// <summary>
	/// Provides <see cref="CustomTag"/> objects over text ranges that contain the specified regex pattern text.
	/// </summary>
	public class CustomTagger : TaggerBase<CustomTag> {

		private string pattern;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>CustomTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this manager is attached.</param>
		public CustomTagger(ICodeDocument document) : 
			base("CustomTagger", null, document, true) {

			this.pattern = @"\bActipro\b";
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		public override IEnumerable<TagSnapshotRange<CustomTag>> GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			if (snapshotRanges != null) {
				// Loop through the snapshot ranges
				foreach (TextSnapshotRange snapshotRange in snapshotRanges) {
					// Get the text of the snapshot range
					string text = snapshotRange.Text;

					// Look for a regex pattern match
					MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);
					if (matches.Count > 0) {
						// Loop through the matches
						foreach (Match match in matches) {
							// Create a tag
							CustomTag tag = new CustomTag();

							// Yield the tag
							yield return new TagSnapshotRange<CustomTag>(
								TextSnapshotRange.FromSpan(snapshotRange.Snapshot, snapshotRange.StartOffset + match.Index, match.Length), tag);
						}
					}
				}
			}
		}

	}

}
