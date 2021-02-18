using System.Collections.Generic;
using System.Windows.Media;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;
using ActiproSoftware.Text.Utility;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting;
using ActiproSoftware.Windows.Controls.SyntaxEditor.Highlighting.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.ReadOnlyRegions {

	/// <summary>
	/// Provides a custom implementation of a tagger that can mark text ranges as read-only within a text buffer.
	/// </summary>
	public class CustomReadOnlyRegionTagger : CollectionTagger<IReadOnlyRegionTag>, ITagger<IClassificationTag> {

		private bool						highlightReadOnlyRegions	= true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes the <c>CustomReadOnlyRegionTagger</c> class.
		/// </summary>
		static CustomReadOnlyRegionTagger() {
			// Register the classification type with a grayed-out background
			IHighlightingStyle style = new HighlightingStyle(null, Color.FromArgb(0x40, 0xB0, 0xB0, 0xB0));
			AmbientHighlightingStyleRegistry.Instance.Register(ClassificationTypes.ReadOnlyRegion, style);
		}
			
		/// <summary>
		/// Initializes a new instance of the <c>CustomReadOnlyRegionTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this tagger is attached.</param>
		public CustomReadOnlyRegionTagger(ICodeDocument document) : base("Custom", new Ordering[] { new Ordering(TaggerKeys.Token, OrderPlacement.Before) }, document, true) {}
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns the tag ranges that intersect with the specified normalized snapshot ranges.
		/// </summary>
		/// <param name="snapshotRanges">The collection of normalized snapshot ranges.</param>
		/// <param name="parameter">An optional parameter that provides contextual information about the tag request.</param>
		/// <returns>The tag ranges that intersect with the specified normalized snapshot ranges.</returns>
		IEnumerable<TagSnapshotRange<IClassificationTag>> ITagger<IClassificationTag>.GetTags(NormalizedTextSnapshotRangeCollection snapshotRanges, object parameter) {
			// We implement ITagger<IClassificationTag> explicitly so that the core CollectionTagger can
			//   return tags of type IReadOnlyRegionTag.  This method can return IClassificationTag tags so that the core 
			//   SyntaxEditor rendering procedures can update syntax highlighting over the marked ranges
			if (!highlightReadOnlyRegions)
				yield break;

			foreach (TagSnapshotRange<IReadOnlyRegionTag> tagRange in this.GetTags(snapshotRanges, parameter))
				yield return new TagSnapshotRange<IClassificationTag>(tagRange.SnapshotRange, (IClassificationTag)tagRange.Tag);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets whether to highlight read-only regions.
		/// </summary>
		/// <value>
		/// <c>true</c> if read-only regions should be highlighted; otherwise, <c>false</c>.
		/// </value>
		public bool HighlightReadOnlyRegions { 
			get {
				return highlightReadOnlyRegions;
			}
			set {
				if (highlightReadOnlyRegions == value)
					return;

				highlightReadOnlyRegions = value;

				// Raise an event so that the entire document is reclassified
				ITextSnapshot snapshot = this.Document.CurrentSnapshot;
				this.OnTagsChanged(new TagsChangedEventArgs(new TextSnapshotRange(snapshot, snapshot.TextRange)));
			}
		}

	}
}