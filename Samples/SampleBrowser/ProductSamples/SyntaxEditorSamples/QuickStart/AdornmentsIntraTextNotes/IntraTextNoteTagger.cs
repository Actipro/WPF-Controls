using System;
using System.Collections.Generic;
using ActiproSoftware.Text;
using ActiproSoftware.Text.Implementation;
using ActiproSoftware.Text.Tagging;
using ActiproSoftware.Text.Tagging.Implementation;

namespace ActiproSoftware.ProductSamples.SyntaxEditorSamples.QuickStart.AdornmentsIntraTextNotes {
    
	/// <summary>
	/// Provides <see cref="IntraTextNoteTag"/> objects over text ranges.
	/// </summary>
	public class IntraTextNoteTagger : CollectionTagger<IIntraTextSpacerTag>, ITagger<IClassificationTag> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the <c>IntraTextNoteTagger</c> class.
		/// </summary>
		/// <param name="document">The document to which this tagger is attached.</param>
		public IntraTextNoteTagger(ICodeDocument document) : base("IntraTextNoteTagger", null, document, true) {}
		
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
			// We implement ITagger<IIntraTextSpacerTag> explicitly so that the core CollectionTagger can
			//   return tags of type IIntraTextSpacerTag which SyntaxEditor uses to add intra-text spacing and this
			//   method can return IClassificationTag tags that the core SyntaxEditor rendering procedures 
			//   can update syntax highlighting over the marked ranges
			foreach (TagSnapshotRange<IIntraTextSpacerTag> tagRange in this.GetTags(snapshotRanges, parameter))
				yield return new TagSnapshotRange<IClassificationTag>(tagRange.SnapshotRange, (IClassificationTag)tagRange.Tag);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Raises the <see cref="TagsChanged"/> event.
		/// </summary>
		/// <param name="e">A <c>TagsChangedEventArgs</c> that contains the event data.</param>
		public void RaiseTagsChanged(TagsChangedEventArgs e) {
			this.OnTagsChanged(e);
		}

	}

}
